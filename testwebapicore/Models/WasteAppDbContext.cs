using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace testwebapicore.Models
{
    public partial class WasteAppDbContext : DbContext
    {
        public WasteAppDbContext()
        {
        }

        public WasteAppDbContext(DbContextOptions<WasteAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientCategory> ClientCategory { get; set; }
        public virtual DbSet<ClientPromotions> ClientPromotions { get; set; }
        public virtual DbSet<ComapnyPromotion> ComapnyPromotion { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<FeedbackCategory> FeedbackCategory { get; set; }
        public virtual DbSet<PromotionCodes> PromotionCodes { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleCollector> ScheduleCollector { get; set; }
        public virtual DbSet<ServeyUsers> ServeyUsers { get; set; }
        public virtual DbSet<Survey> Survey { get; set; }
        public virtual DbSet<SurveyQuestions> SurveyQuestions { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Waste> Waste { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=WasteAppDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.RegionId)
                    .HasName("FK");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StreetNameArabic)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_Region");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.Mobile)
                    .HasName("FK")
                    .IsUnique();

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Address");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Client_ClientCategory");
            });

            modelBuilder.Entity<ClientCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientPromotions>(entity =>
            {
                entity.HasIndex(e => new { e.ClientId, e.PromotionId })
                    .HasName("FK");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ClientId).ValueGeneratedOnAdd();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPromotions)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientPromotions_Client");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.ClientPromotions)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_ClientPromotions_Promotions");
            });

            modelBuilder.Entity<ComapnyPromotion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasIndex(e => new { e.CategoryId, e.ClientId })
                    .HasName("FK");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FeedbackContent)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Feedback_FeedbackCategory");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Feedback_Client1");
            });

            modelBuilder.Entity<FeedbackCategory>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Pk");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PromotionCodes>(entity =>
            {
                entity.HasKey(e => new { e.PromtionId, e.Code });

                entity.HasIndex(e => new { e.ClientId, e.PromtionId })
                    .HasName("FK");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.PromotionCodes)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_PromotionCodes_Client");

                entity.HasOne(d => d.Promtion)
                    .WithMany(p => p.PromotionCodes)
                    .HasForeignKey(d => d.PromtionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PromotionCodes_Promotions");
            });

            modelBuilder.Entity<Promotions>(entity =>
            {
                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.Details)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Promotions)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Promotions_ComapnyPromotion");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameArabic)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasIndex(e => new { e.ClientId, e.ScheduleId, e.AddressId, e.CollectorId })
                    .HasName("FK");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Request_Address");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_Request_Client");

                entity.HasOne(d => d.Collector)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.CollectorId)
                    .HasConstraintName("FK_Request_User");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Schedule");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Role1)
                    .HasColumnName("Role")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_Schedule_User2");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Schedule_Region");
            });

            modelBuilder.Entity<ScheduleCollector>(entity =>
            {
                entity.HasKey(e => new { e.ScheduleId, e.CollectorId });

                entity.ToTable("Schedule_Collector");

                entity.HasOne(d => d.Collector)
                    .WithMany(p => p.ScheduleCollector)
                    .HasForeignKey(d => d.CollectorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Collector_User1");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ScheduleCollector)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Collector_Schedule");
            });

            modelBuilder.Entity<ServeyUsers>(entity =>
            {
                entity.HasKey(e => new { e.SurveyId, e.ClientId, e.QuestionId });

                entity.HasIndex(e => new { e.SurveyId, e.ClientId, e.QuestionId })
                    .HasName("PK,FK");

                entity.Property(e => e.Answer)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ServeyUsers)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServeyUsers_Client");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ServeyUsers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServeyUsers_SurveyQuestions");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.ServeyUsers)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServeyUsers_Survey");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.Property(e => e.GenerationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SurveyQuestions>(entity =>
            {
                entity.HasIndex(e => e.SurveyId)
                    .HasName("FK");

                entity.Property(e => e.ChoiceA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChoiceB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChoiceC)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ChoiceD)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Question).HasMaxLength(265);

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyQuestions)
                    .HasForeignKey(d => d.SurveyId)
                    .HasConstraintName("FK_SurveyQuestions_Survey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("FK");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Waste>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
