using System;
using System.Collections.Generic;

namespace ProjectPitch4.models
{
    public partial class User
    {
        public User()
        {
            QuizResults = new HashSet<QuizResult>();
        }

        public int Userid { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<QuizResult> QuizResults { get; set; }
    }
}
