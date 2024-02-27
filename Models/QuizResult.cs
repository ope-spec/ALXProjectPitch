using System;
using System.Collections.Generic;

namespace ProjectPitch4.models
{
    public partial class QuizResult
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public float? Score { get; set; }
        public DateTime? QuizDate { get; set; }
        public string? QuizIdentifier { get; set; }

        public virtual User? User { get; set; }
    }
}
