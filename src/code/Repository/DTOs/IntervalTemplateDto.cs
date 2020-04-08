using System.Collections.Generic;

namespace RedSpartan.IntervalTraining.Repository.DTOs
{
    public class IntervalTemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TimeSeconds { get; set; }
        public int? Iterations { get; set; }
        public IList<IntervalDto> Intervals { get; set; } = new List<IntervalDto>();

        public IList<HistoryDto> History { get; set; } = new List<HistoryDto>();
    }
}
