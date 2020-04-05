﻿using System;

namespace RedSpartan.IntervalTraining.Repository.Data.Entities
{
    public class History
    {
        public int Id { get; set; }
        public IntervalTemplate Template { get; set; }
        public int TemplateId { get; set; }
        public string Intervals { get; set; }
        public int TimeActiveSeconds { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}