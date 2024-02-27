using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectPitch4.models
{
    public partial class MySqlContext : DbContext
    {
        public MySqlContext()
        {
        }

        public MySqlContext(DbContextOptions<MySqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CcquizQuestion> CcquizQuestions { get; set; } = null!;
        public virtual DbSet<CsyquizQuestion> CsyquizQuestions { get; set; } = null!;
        public virtual DbSet<DmquizQuestion> DmquizQuestions { get; set; } = null!;
        public virtual DbSet<DsnquizQuestion> DsnquizQuestions { get; set; } = null!;
        public virtual DbSet<PmquizQuestion> PmquizQuestions { get; set; } = null!;
        public virtual DbSet<QuizResult> QuizResults { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WdquizQuestion> WdquizQuestions { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySql("server=localhost;user=root;password=Ademidun98!;database=quiz_master", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<CcquizQuestion>(entity =>
            {
                entity.ToTable("ccquiz_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.Option1)
                    .HasColumnType("text")
                    .HasColumnName("option1");

                entity.Property(e => e.Option2)
                    .HasColumnType("text")
                    .HasColumnName("option2");

                entity.Property(e => e.Option3)
                    .HasColumnType("text")
                    .HasColumnName("option3");

                entity.Property(e => e.Option4)
                    .HasColumnType("text")
                    .HasColumnName("option4");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");
            });

            modelBuilder.Entity<CsyquizQuestion>(entity =>
            {
                entity.ToTable("csyquiz_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.Option1)
                    .HasColumnType("text")
                    .HasColumnName("option1");

                entity.Property(e => e.Option2)
                    .HasColumnType("text")
                    .HasColumnName("option2");

                entity.Property(e => e.Option3)
                    .HasColumnType("text")
                    .HasColumnName("option3");

                entity.Property(e => e.Option4)
                    .HasColumnType("text")
                    .HasColumnName("option4");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");
            });

            modelBuilder.Entity<DmquizQuestion>(entity =>
            {
                entity.ToTable("dmquiz_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.Option1)
                    .HasColumnType("text")
                    .HasColumnName("option1");

                entity.Property(e => e.Option2)
                    .HasColumnType("text")
                    .HasColumnName("option2");

                entity.Property(e => e.Option3)
                    .HasColumnType("text")
                    .HasColumnName("option3");

                entity.Property(e => e.Option4)
                    .HasColumnType("text")
                    .HasColumnName("option4");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");
            });

            modelBuilder.Entity<DsnquizQuestion>(entity =>
            {
                entity.ToTable("dsnquiz_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.Option1)
                    .HasColumnType("text")
                    .HasColumnName("option1");

                entity.Property(e => e.Option2)
                    .HasColumnType("text")
                    .HasColumnName("option2");

                entity.Property(e => e.Option3)
                    .HasColumnType("text")
                    .HasColumnName("option3");

                entity.Property(e => e.Option4)
                    .HasColumnType("text")
                    .HasColumnName("option4");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");
            });

            modelBuilder.Entity<PmquizQuestion>(entity =>
            {
                entity.ToTable("pmquiz_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.Option1)
                    .HasColumnType("text")
                    .HasColumnName("option1");

                entity.Property(e => e.Option2)
                    .HasColumnType("text")
                    .HasColumnName("option2");

                entity.Property(e => e.Option3)
                    .HasColumnType("text")
                    .HasColumnName("option3");

                entity.Property(e => e.Option4)
                    .HasColumnType("text")
                    .HasColumnName("option4");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");
            });

            modelBuilder.Entity<QuizResult>(entity =>
            {
                entity.ToTable("quiz_results");

                entity.HasIndex(e => e.QuizDate, "quiz_date")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.QuizDate)
                    .HasColumnType("datetime")
                    .HasColumnName("quiz_date");

                entity.Property(e => e.QuizIdentifier)
                    .HasMaxLength(255)
                    .HasColumnName("quiz_identifier");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.QuizResults)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("quiz_results_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<WdquizQuestion>(entity =>
            {
                entity.ToTable("wdquiz_questions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CorrectOption).HasColumnName("correct_option");

                entity.Property(e => e.Option1)
                    .HasColumnType("text")
                    .HasColumnName("option1");

                entity.Property(e => e.Option2)
                    .HasColumnType("text")
                    .HasColumnName("option2");

                entity.Property(e => e.Option3)
                    .HasColumnType("text")
                    .HasColumnName("option3");

                entity.Property(e => e.Option4)
                    .HasColumnType("text")
                    .HasColumnName("option4");

                entity.Property(e => e.QuestionText)
                    .HasColumnType("text")
                    .HasColumnName("question_text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
