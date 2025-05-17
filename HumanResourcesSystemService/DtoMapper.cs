using AutoMapper;
using HumanResourcesSystemCore.Dtos;
using HumanResourcesSystemCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemService
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<CompanyDto,Company>().ReverseMap();
            CreateMap<DepartmentDto,Department>().ReverseMap();
            CreateMap<LeaveRequestDto,LeaveRequest>().ReverseMap();
            CreateMap<PerformanceReviewDto,PerformanceReview>().ReverseMap();
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<WorkReportDto,WorkReport>().ReverseMap();
            CreateMap<EventDto, EventModel>().ReverseMap();
            CreateMap<AnnouncementDto,Announcement>().ReverseMap();
            CreateMap<DailyTaskDto,DailyTask>().ReverseMap();
        }
    }
}
