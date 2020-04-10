using AutoMapper;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;
using System;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    public class AutoMapperModelProfile : Profile
    {
        public AutoMapperModelProfile()
        {
            CreateMap<IntervalTemplateDto, IntervalTemplate>(MemberList.Source).ReverseMap();

            CreateMap<HistoryDto, History>(MemberList.Source).ReverseMap();

            CreateMap<IntervalDto, Interval>(MemberList.Source)
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.TimeSeconds)));

            CreateMap<Interval, IntervalDto>(MemberList.Source)
                .ForMember(dst => dst.TimeSeconds, opt => opt.MapFrom(src => src.Time.TotalSeconds));
        }
    }
}
