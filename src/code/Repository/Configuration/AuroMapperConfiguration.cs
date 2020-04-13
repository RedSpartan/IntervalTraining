using AutoMapper;
using Newtonsoft.Json;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Internal.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RedSpartan.IntervalTraining.Repository.MappingProfiles
{
    public static class AuroMapperConfiguration
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IntervalTemplate, IntervalTemplateDto>()
                    .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<IntervalDto>>(src.Intervals).OrderByDescending(x => x.Order)));

                cfg.CreateMap<IntervalTemplateDto, IntervalTemplate>()
                    .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Intervals)));

                cfg.CreateMap<History, HistoryDto>()
                    .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<IntervalDto>>(src.Intervals).OrderByDescending(x => x.Order)));

                cfg.CreateMap<HistoryDto, History>()
                    .ForMember(dst => dst.Intervals, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Intervals)));
            });
        }
    }
}
