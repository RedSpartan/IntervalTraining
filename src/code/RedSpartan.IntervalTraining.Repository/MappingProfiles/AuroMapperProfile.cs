using AutoMapper;
using Newtonsoft.Json;
using RedSpartan.IntervalTraining.Repository.Data.Entities;
using RedSpartan.IntervalTraining.Repository.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace RedSpartan.IntervalTraining.Repository.MappingProfiles
{
    public class AuroMapperProfile : Profile
    {
        public AuroMapperProfile()
        {
            CreateMap<IntervalTemplate, IntervalTemplateDto>()
                .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<IntervalDto>>(src.Intervals).OrderByDescending(x => x.Order)));

            CreateMap<IntervalTemplateDto, IntervalTemplate>()
                .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Intervals)));

            CreateMap<History, HistoryDto>()
                .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<IntervalDto>>(src.Intervals).OrderByDescending(x => x.Order)));

            CreateMap<HistoryDto, History>()
                .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Intervals)));
        }
    }
}
