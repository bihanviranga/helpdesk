using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDesk.Model
{
    public partial class helpdeskContext : DbContext
    {
        public helpdeskContext() { }

        public helpdeskContext(DbContextOptions<helpdeskContext> options) : base(options) { }

        public virtual DbSet<TktArticle> TktArticle { get; set; }
        public virtual DbSet<TktCategory> TktCategory { get; set; }
        public virtual DbSet<TktCompany> TktCompany { get; set; }
        public virtual DbSet<TktCompanyBrand> TktCompanyBrand { get; set; }
        public virtual DbSet<TktConversation> TktConversation { get; set; }
        public virtual DbSet<TktModule> TktModule { get; set; }
        public virtual DbSet<TktNotification> TktNotification { get; set; }
        public virtual DbSet<TktProduct> TktProduct { get; set; }
        public virtual DbSet<TktResTemplate> TktResTemplate { get; set; }
        public virtual DbSet<TktTicketMaster> TktTicketMaster { get; set; }
        public virtual DbSet<TktTicketOperator> TktTicketOperator { get; set; }
        public virtual DbSet<TktTicketTimeline> TktTicketTimeline { get; set; }
        public virtual DbSet<TktUser> TktUser { get; set; }
        public virtual DbSet<TktUserToken> TktUserToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=helpdesk;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TktArticle>(entity =>
            {
                entity.HasKey(e => e.ArticleId);

                entity.ToTable("Tkt_Article");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("ArticleID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AcceptedBy)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditedBy).HasColumnType("datetime");

                entity.Property(e => e.LastEditedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktCategory>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.CompanyId });

                entity.ToTable("Tkt_Category");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("Tkt_Company");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktCompanyBrand>(entity =>
            {
                entity.HasKey(e => new { e.BrandId, e.CompanyId });

                entity.ToTable("Tkt_CompanyBrand");

                entity.Property(e => e.BrandId)
                    .HasColumnName("BrandID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktConversation>(entity =>
            {
                entity.HasKey(e => new { e.CvId, e.TicketId });

                entity.ToTable("Tkt_Conversation");

                entity.Property(e => e.CvId)
                    .HasColumnName("CvID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CvSendDate).HasColumnType("datetime");

                entity.Property(e => e.CvSender)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CvSenderType).IsRequired();
            });

            modelBuilder.Entity<TktModule>(entity =>
            {
                entity.HasKey(e => new { e.ModuleId, e.CompanyId });

                entity.ToTable("Tkt_Module");

                entity.Property(e => e.ModuleId)
                    .HasColumnName("ModuleID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktNotification>(entity =>
            {
                entity.HasKey(e => new { e.NotifId, e.TicketId });

                entity.ToTable("Tkt_Notification");

                entity.Property(e => e.NotifId)
                    .HasColumnName("NotifID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NotifContent).IsRequired();

                entity.Property(e => e.NotifDate).HasColumnType("datetime");

                entity.Property(e => e.NotifUrl)
                    .IsRequired()
                    .HasColumnName("NotifURL");

                entity.Property(e => e.NotifUser)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CompanyId });

                entity.ToTable("Tkt_Product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktResTemplate>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("Tkt_ResTemplate");

                entity.Property(e => e.TemplateId)
                    .HasColumnName("TemplateID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TemplateAddedBy)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TemplateAddedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TktTicketMaster>(entity =>
            {
                entity.HasKey(e => e.TicketId);

                entity.ToTable("Tkt_TicketMaster");

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.BrandId)
                    .HasColumnName("BrandID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModuleId)
                    .HasColumnName("ModuleID")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TktAssignedTo)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TktClosedDate).HasColumnType("datetime");

                entity.Property(e => e.TktCreatedBy).HasMaxLength(36);

                entity.Property(e => e.TktCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TktFirstResponseDate).HasColumnType("datetime");

                entity.Property(e => e.TktReopenedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TktTicketOperator>(entity =>
            {
                entity.HasKey(e => new { e.TktOperator, e.TiketId, e.SeqNo });

                entity.ToTable("Tkt_TicketOperator");

                entity.Property(e => e.TktOperator)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TiketId)
                    .HasColumnName("TiketID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SeqNo)
                    .HasColumnName("Seq_no")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AssignedBy).HasColumnType("datetime");

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TktTicketTimeline>(entity =>
            {
                entity.HasKey(e => new { e.TicketId, e.TxnDateTime })
                    .HasName("PK_Tkt_TicketTimeline_1");

                entity.ToTable("Tkt_TicketTimeline");

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TxnDateTime).HasColumnType("datetime");

                entity.Property(e => e.TktEvent).IsRequired();

                entity.Property(e => e.TxnUser)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TxnUserId)
                    .IsRequired()
                    .HasColumnName("TxnUserID")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TxnValues).IsRequired();
            });

            modelBuilder.Entity<TktUser>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.UserName })
                    .HasName("PK_Tkt_userz");

                entity.ToTable("Tkt_user");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserName)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TktUserToken>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Token });

                entity.ToTable("Tkt_userToken");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Token)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
