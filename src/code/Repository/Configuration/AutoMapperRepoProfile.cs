using AutoMapper;
using Newtonsoft.Json;
using RedSpartan.IntervalTraining.Repository.DTOs;
using RedSpartan.IntervalTraining.Repository.Internal.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RedSpartan.IntervalTraining.Repository.Configuration
{
    public class AutoMapperRepoProfile : Profile
    {
        public AutoMapperRepoProfile()
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
