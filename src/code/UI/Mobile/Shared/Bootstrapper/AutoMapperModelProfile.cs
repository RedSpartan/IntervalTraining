using AutoMapper;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.UI.Mobile.Shared.Models;

namespace RedSpartan.IntervalTraining.UI.Mobile.Shared.Bootstrapper
{
    public class AutoMapperModelProfile : Profile
    {
        public AutoMapperModelProfile()
        {
            CreateMap<IntervalTemplateDto, IntervalTemplate>(MemberList.Source).ReverseMap();

            CreateMap<HistoryDto, History>(MemberList.Source).ReverseMap();

            CreateMap<IntervalDto, Interval>(MemberList.Source).ReverseMap();
        }
    }
}
