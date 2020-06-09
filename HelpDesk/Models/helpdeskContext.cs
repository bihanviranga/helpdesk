using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HelpDesk.Model
{
    public partial class helpdeskContext : DbContext
    {
      
        public helpdeskContext(DbContextOptions<helpdeskContext> options) : base(options) { }

        public virtual DbSet<Article> TktArticle { get; set; }
        public virtual DbSet<Category> TktCategory { get; set; }
        public virtual DbSet<Company> TktCompany { get; set; }
        public virtual DbSet<CompanyBrand> TktCompanyBrand { get; set; }
        public virtual DbSet<Conversation> TktConversation { get; set; }
        public virtual DbSet<Module> TktModule { get; set; }
        public virtual DbSet<Notification> TktNotification { get; set; }
        public virtual DbSet<Product> TktProduct { get; set; }
        public virtual DbSet<ResTemplate> TktResTemplate { get; set; }
        public virtual DbSet<TicketMaster> TktTicketMaster { get; set; }
        public virtual DbSet<TicketOperator> TktTicketOperator { get; set; }
        public virtual DbSet<TicketTimeline> TktTicketTimeline { get; set; }
        public virtual DbSet<UserModel> TktUser { get; set; }
        public virtual DbSet<UserToken> TktUserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
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

            modelBuilder.Entity<Category>(entity =>
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

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("Tkt_Company");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CompanyBrand>(entity =>
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

            modelBuilder.Entity<Conversation>(entity =>
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

            modelBuilder.Entity<Module>(entity =>
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

            modelBuilder.Entity<Notification>(entity =>
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

            modelBuilder.Entity<Product>(entity =>
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

            modelBuilder.Entity<ResTemplate>(entity =>
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

            modelBuilder.Entity<TicketMaster>(entity =>
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

            modelBuilder.Entity<TicketOperator>(entity =>
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

            modelBuilder.Entity<TicketTimeline>(entity =>
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

            modelBuilder.Entity<UserModel>(entity =>
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

            modelBuilder.Entity<UserToken>(entity =>
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
