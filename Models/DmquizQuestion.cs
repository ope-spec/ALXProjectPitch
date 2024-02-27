using System;
using System.Collections.Generic;

namespace ProjectPitch4.models
{
    public partial class DmquizQuestion
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;
        public string Option1 { get; set; } = null!;
        public string Option2 { get; set; } = null!;
        public string Option3 { get; set; } = null!;
        public string Option4 { get; set; } = null!;
        public int CorrectOption { get; set; }
    }
}
