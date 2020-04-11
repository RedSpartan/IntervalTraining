using System;
using System.Collections.Generic;

namespace RedSpartan.IntervalTraining.Repository.DTOs
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TemplateId { get; set; }
        public List<IntervalDto> Intervals { get; set; } = new List<IntervalDto>();
        public int TimeActiveSeconds { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}
