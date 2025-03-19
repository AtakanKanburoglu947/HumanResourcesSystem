using AutoMapper;
using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HumanResourcesSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomSchemeAuthentication")]

    public class EventController : Controller
    {
        private readonly IService<EventModel, EventDto> _eventService;
        private readonly IAuthService _authService;

        public EventController(IService<EventModel, EventDto> eventService, IAuthService authService)
        {
            _eventService = eventService;
            _authService = authService;
        }

        public IActionResult Index()
        {
            var accountDto = _authService.GetAccountDetailsFromToken();
            ViewData["UserId"] = accountDto.Id;

            var events = _eventService.Where(x => x.StartDate, x => x.UserId == accountDto.Id);
            var celendarEvents = events.Select(x => new CelendarEvent()
            {
                End = x.EndDate,
                Start = x.StartDate,
                Id = x.Id,
                Title = x.Title,
                UserId = accountDto.Id,
            }).ToList();
           
            return View(celendarEvents);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CelendarEvent celendarEvent)
        {
            // Kullanıcı bilgilerini JWT token'dan alıyoruz
            var accountDto = _authService.GetAccountDetailsFromToken();

            // Başlangıç ve bitiş tarihlerini kontrol et
            if (celendarEvent.Start.Date != celendarEvent.End.Date)
            {
                TempData["ErrorMessage"] = "Başlangıç ve bitiş tarihi aynı gün olmalıdır.";
                return RedirectToAction("Index");
            }

            if (celendarEvent.Start >= celendarEvent.End)
            {
                var events = _eventService.Where(x => x.StartDate, x => x.UserId == accountDto.Id);
                var celendarEvents = events.Select(x => new CelendarEvent()
                {
                    End = x.EndDate,
                    Start = x.StartDate,
                    Id = x.Id,
                    Title = x.Title,
                    UserId = accountDto.Id,
                }).ToList();
                TempData["ErrorMessage"] = "Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.";

                return RedirectToAction("Index");
            }

            // Event'i kaydet
            celendarEvent.UserId = accountDto.Id;
            EventDto eventDto = new EventDto()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = accountDto.Id,
                StartDate = celendarEvent.Start,
                EndDate = celendarEvent.End,
                Title = celendarEvent.Title,
            };
            await _eventService.AddAsync(eventDto);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var eventModel = await _eventService.FindAsync(id);
            if (eventModel == null)
            {
                TempData["ErrorMessage"] = "Etkinlik silinemedi.";

                return RedirectToAction("Index");
            }
            await _eventService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(CelendarEvent celendarEvent)
        {
            var eventModel = await _eventService.FindAsync(celendarEvent.Id);
            if (eventModel == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek etkinlik bulunamadı.";
                return RedirectToAction("Index");
            }

            // Tarih kontrolleri
            if (celendarEvent.Start.Date != celendarEvent.End.Date)
            {
                TempData["ErrorMessage"] = "Başlangıç ve bitiş tarihi aynı gün olmalıdır.";
                return RedirectToAction("Index");
            }

            if (celendarEvent.Start >= celendarEvent.End)
            {
                TempData["ErrorMessage"] = "Bitiş tarihi, başlangıç tarihinden sonra olmalıdır.";
                return RedirectToAction("Index");
            }
            eventModel.Title = celendarEvent.Title;
            eventModel.StartDate = celendarEvent.Start;
            eventModel.EndDate = celendarEvent.End;

            await _eventService.UpdateAsync(eventModel);
            TempData["SuccessMessage"] = "Etkinlik başarıyla güncellendi.";

            return RedirectToAction("Index");
        }

    }

}
