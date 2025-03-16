using AutoMapper;
using HumanResourcesSystem.Models;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using HumanResourcesSystemCore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HumanResourcesSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly IService<EventModel, EventDto> _eventService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public EventController(IService<EventModel, EventDto> eventService, IAuthService authService, IMapper mapper)
        {
            _eventService = eventService;
            _authService = authService;
            _mapper = mapper;
        }

        // Takvim sayfasını gösterir, kullanıcıya ait etkinlikleri alır
        public IActionResult Index()
        {


            // Kullanıcı bilgilerini JWT token'dan alıyoruz
            var accountDto = _authService.GetAccountDetailsFromToken();
            ViewData["UserId"] = accountDto.Id;

            // Kullanıcının etkinliklerini alıyoruz
            var events = _eventService.Where(x=>x.StartDate,x => x.UserId == accountDto.Id);

            // Etkinlikleri takvimde göstermek için dönüştürme
            var calendarEvents = events.Select(e => new CelendarEvent
            {
                Id = e.Id.ToString(),  // Id'yi string olarak veriyoruz
                Title = e.Title,
                Start = e.StartDate,  // ISO formatında
                End = e.EndDate// ISO formatında
            }).ToList();

            return View(calendarEvents);
        }
        [HttpPost]
        public IActionResult AddEvent([FromBody] EventDto eventDto)
        {
            if (eventDto == null)
            {
                return BadRequest("Geçersiz etkinlik verisi.");
            }
            eventDto.UserId = _authService.GetAccountDetailsFromToken().Id;
          

            // Etkinliği veritabanına ekleyin
            _eventService.AddAsync(eventDto);

            return Ok("Etkinlik başarıyla eklendi.");
        }

    }
}
