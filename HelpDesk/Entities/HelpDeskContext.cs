using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HelpDesk.Entities.Models;

namespace HelpDesk.Entities
{
    public partial class HelpDeskContext : DbContext
    {

        public HelpDeskContext(DbContextOptions<HelpDeskContext> options)
            : base(options)
        {
        }

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

                entity.Property(e => e.BrandId)
                    .HasColumnName("BrandID")
                    .HasMaxLength(20);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasMaxLength(20);

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastEditedBy).HasMaxLength(20);

                entity.Property(e => e.LastEditedDate).HasColumnType("datetime");

                entity.Property(e => e.ModuleId)
                    .HasColumnName("ModuleID")
                    .HasMaxLength(20);

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TktArticle)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_CreatedBy");
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TktCategory)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Company");
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

                entity.Property(e => e.CompanyName).IsRequired();
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

                entity.Property(e => e.BrandName).IsRequired();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TktCompanyBrand)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Company");
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

                entity.HasOne(d => d.CvSenderNavigation)
                    .WithMany(p => p.TktConversation)
                    .HasForeignKey(d => d.CvSender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversation_Sender");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TktConversation)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversation_Ticket");
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TktModule)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Module_Company");
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

                entity.Property(e => e.NotifUrl).HasColumnName("NotifURL");

                entity.Property(e => e.NotifUser)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.NotifUserNavigation)
                    .WithMany(p => p.TktNotification)
                    .HasForeignKey(d => d.NotifUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TktNotification)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_Ticket");
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

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TktProduct)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Company");
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

                entity.Property(e => e.TemplateName).IsRequired();

                entity.HasOne(d => d.TemplateAddedByNavigation)
                    .WithMany(p => p.TktResTemplate)
                    .HasForeignKey(d => d.TemplateAddedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResTemplate_AddedBy");
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

                entity.Property(e => e.TicketCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TktAssignedTo).HasMaxLength(20);

                entity.Property(e => e.TktClosedDate).HasColumnType("datetime");

                entity.Property(e => e.TktContent).IsRequired();

                entity.Property(e => e.TktCreatedBy)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TktCreatedByCompany)
                    .IsRequired()
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TktCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.TktFirstResponseDate).HasColumnType("datetime");

                entity.Property(e => e.TktPriority).HasMaxLength(20);

                entity.Property(e => e.TktRating).HasMaxLength(20);

                entity.Property(e => e.TktReopenedDate).HasColumnType("datetime");

                entity.Property(e => e.TktStatus).HasMaxLength(20);

                entity.Property(e => e.TktSubject).IsRequired();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TktTicketMasterCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_Company");

                entity.HasOne(d => d.TktAssignedToNavigation)
                    .WithMany(p => p.TktTicketMasterTktAssignedToNavigation)
                    .HasForeignKey(d => d.TktAssignedTo)
                    .HasConstraintName("FK_Ticket_AssignedTo");

                entity.HasOne(d => d.TktCreatedByNavigation)
                    .WithMany(p => p.TktTicketMasterTktCreatedByNavigation)
                    .HasForeignKey(d => d.TktCreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_CreatedBy");

                entity.HasOne(d => d.TktCreatedByCompanyNavigation)
                    .WithMany(p => p.TktTicketMasterTktCreatedByCompanyNavigation)
                    .HasForeignKey(d => d.TktCreatedByCompany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ticket_CreatedByCompany");
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

                entity.Property(e => e.AssignedBy).HasMaxLength(20);

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.HasOne(d => d.AssignedByNavigation)
                    .WithMany(p => p.TktTicketOperatorAssignedByNavigation)
                    .HasForeignKey(d => d.AssignedBy)
                    .HasConstraintName("FK_Operator_AssignedBy");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TktTicketOperator)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operator_Ticket");

                entity.HasOne(d => d.TktOperatorNavigation)
                    .WithMany(p => p.TktTicketOperatorTktOperatorNavigation)
                    .HasForeignKey(d => d.TktOperator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operator_User");
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
                    .HasColumnName("TxnUserID")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TktTicketTimeline)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Timeline_Ticket");

                entity.HasOne(d => d.TxnUser)
                    .WithMany(p => p.TktTicketTimeline)
                    .HasForeignKey(d => d.TxnUserId)
                    .HasConstraintName("FK_Timeline_User");
            });

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK_Tkt_userz");

                entity.ToTable("Tkt_User");

                entity.Property(e => e.UserName).HasMaxLength(20);

                entity.Property(e => e.CompanyId)
                    .IsRequired()
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TktUser)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Company");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}