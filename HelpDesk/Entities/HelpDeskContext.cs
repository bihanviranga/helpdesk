using System;
using HelpDesk.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDesk.Entities
{
    public partial class HelpDeskContext : DbContext
    {

        public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options) { }

        public virtual DbSet<ArticleModel> ArticleModel { get; set; }
        public virtual DbSet<CategoryModel> CategoryModel { get; set; }
        public virtual DbSet<CompanyModel> CompanyModel { get; set; }
        public virtual DbSet<CompanyBrandModel> CompanyBrandModel { get; set; }
        public virtual DbSet<ConversationModel> ConversationModel { get; set; }
        public virtual DbSet<ModuleModel> ModuleModel { get; set; }
        public virtual DbSet<NotificationModel> NotificationModel { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ResTemplateModel> ResTemplateModel { get; set; }
        public virtual DbSet<TicketModel> TicketModel { get; set; }
        public virtual DbSet<TicketOperatorModel> TicketOperatorModel { get; set; }
        public virtual DbSet<TicketTimelineModel> TicketTimelineModel { get; set; }
        public virtual DbSet<UserModel> UserModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleModel>(entity =>
            {
                entity.HasKey(e => e.ArticleId);

                entity.ToTable("Tkt_Article");

                entity.Property(e => e.ArticleId)
                    .HasColumnName("ArticleID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AcceptedBy).HasMaxLength(20);

                entity.Property(e => e.AcceptedDate).HasColumnType("datetime");

                entity.Property(e => e.ArticleContent).IsRequired();

                entity.Property(e => e.ArticleTitle).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastEditedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CategoryModel>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.CompanyId });

                entity.ToTable("Tkt_Category");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(20);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CategoryName).IsRequired();
            });

            modelBuilder.Entity<CompanyModel>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("Tkt_Company");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CompanyBrandModel>(entity =>
            {
                entity.HasKey(e => new { e.BrandId, e.CompanyId });

                entity.ToTable("Tkt_CompanyBrand");

                entity.Property(e => e.BrandId)
                    .HasColumnName("BrandID")
                    .HasMaxLength(20);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ConversationModel>(entity =>
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

                entity.Property(e => e.CvContent).IsRequired();

                entity.Property(e => e.CvSendDate).HasColumnType("datetime");

                entity.Property(e => e.CvSender)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CvSenderType)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ModuleModel>(entity =>
            {
                entity.HasKey(e => new { e.ModuleId, e.CompanyId });

                entity.ToTable("Tkt_Module");

                entity.Property(e => e.ModuleId)
                    .HasColumnName("ModuleID")
                    .HasMaxLength(20);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModuleName).IsRequired();
            });

            modelBuilder.Entity<NotificationModel>(entity =>
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
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CompanyId });

                entity.ToTable("Tkt_Product");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProductName).IsRequired();
            });

            modelBuilder.Entity<ResTemplateModel>(entity =>
            {
                entity.HasKey(e => e.TemplateId);

                entity.ToTable("Tkt_ResTemplate");

                entity.Property(e => e.TemplateId).HasColumnName("TemplateID");

                entity.Property(e => e.TemplateAddedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TemplateAddedDate).HasColumnType("datetime");

                entity.Property(e => e.TemplateContent).IsRequired();

                entity.Property(e => e.TemplateName).IsRequired();
            });

            modelBuilder.Entity<TicketModel>(entity =>
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
                    .HasMaxLength(20);

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnName("CategoryID")
                    .HasMaxLength(20);

                entity.Property(e => e.CompanyId)
                    .IsRequired()
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ModuleId)
                    .IsRequired()
                    .HasColumnName("ModuleID")
                    .HasMaxLength(20);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnName("ProductID")
                    .HasMaxLength(50);

                entity.Property(e => e.TktAssignedTo).HasMaxLength(20);

                entity.Property(e => e.TktClosedDate).HasColumnType("datetime");

                entity.Property(e => e.TktContent).IsRequired();

                entity.Property(e => e.TktCreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TktCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TktFirstResponseDate).HasColumnType("datetime");

                entity.Property(e => e.TktPriority).HasMaxLength(20);

                entity.Property(e => e.TktRating).HasMaxLength(20);

                entity.Property(e => e.TktReopenedDate).HasColumnType("datetime");

                entity.Property(e => e.TktStatus).HasMaxLength(20);

                entity.Property(e => e.TktSubject).IsRequired();
            });

            modelBuilder.Entity<TicketOperatorModel>(entity =>
            {
                entity.HasKey(e => new { e.TktOperator, e.TicketId, e.SeqNo });

                entity.ToTable("Tkt_TicketOperator");

                entity.Property(e => e.TktOperator).HasMaxLength(20);

                entity.Property(e => e.TicketId)
                    .HasColumnName("TicketID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SeqNo).HasColumnName("Seq_no");

                entity.Property(e => e.AssignedBy).HasColumnType("datetime");

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TicketTimelineModel>(entity =>
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

                entity.Property(e => e.TxnUserId)
                    .IsRequired()
                    .HasColumnName("TxnUserID")
                    .HasMaxLength(20);

                entity.Property(e => e.TxnValues).IsRequired();
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.UserName })
                    .HasName("PK_Tkt_userz");

                entity.ToTable("Tkt_User");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserName).HasMaxLength(20);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
