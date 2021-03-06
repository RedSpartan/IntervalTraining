﻿using System.Collections.Generic;

namespace RedSpartan.IntervalTraining.Repository.Internal.Entities
{
    public class IntervalTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TimeSeconds { get; set; }
        public int? Iterations { get; set; }
        public string Intervals { get; set; }

        public ICollection<History> History { get; set; }
    }
}
