using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreApi.Models
{
    public partial class WISHContext : DbContext
    {
        public WISHContext()
        {
        }

        public WISHContext(DbContextOptions<WISHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccoutingRecord> AccoutingRecord { get; set; }
        public virtual DbSet<AccoutingRecordV> AccoutingRecordV { get; set; }
        public virtual DbSet<AdditionalData> AdditionalData { get; set; }
        public virtual DbSet<CodeList> CodeList { get; set; }
        public virtual DbSet<DefauleAmount> DefauleAmount { get; set; }
        public virtual DbSet<DropDownData> DropDownData { get; set; }
        public virtual DbSet<HtmlCssSet> HtmlCssSet { get; set; }
        public virtual DbSet<HtmlCssSetDetail> HtmlCssSetDetail { get; set; }
        public virtual DbSet<HtmlSet> HtmlSet { get; set; }
        public virtual DbSet<PageData> PageData { get; set; }
        public virtual DbSet<UserAdditional> UserAdditional { get; set; }
        public virtual DbSet<UserData> UserData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Persist Security Info=true;Integrated Security=false;User ID=webapi;Password=test123;Initial Catalog=WISH;Server=WISH\\SQLEXPRESS;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccoutingRecord>(entity =>
            {
                entity.HasKey(e => e.AllotNo);

                entity.Property(e => e.AllotNo)
                    .HasColumnName("ALLOT_NO")
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AccoutingDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1900)/(1))/(1))")
                    .HasComment("記帳時間");

                entity.Property(e => e.Additional)
                    .IsRequired()
                    .HasColumnName("ADDITIONAL")
                    .HasMaxLength(50);

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.ChildIndexPk).HasColumnName("ChildIndexPK");

                entity.Property(e => e.Day).HasColumnName("DAY");

                entity.Property(e => e.IndexPk)
                    .HasColumnName("IndexPK")
                    .HasDefaultValueSql("('')")
                    .HasComment("記帳類別 (AccoutingRecord表中Level 1 或Leavel2資料)");

                entity.Property(e => e.Time)
                    .HasColumnName("TIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("UID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccoutingRecordV>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccoutingRecord_V");

                entity.Property(e => e.AccoutingDate).HasColumnType("datetime");

                entity.Property(e => e.Additional)
                    .IsRequired()
                    .HasColumnName("ADDITIONAL")
                    .HasMaxLength(50);

                entity.Property(e => e.AdditionalName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AllotNo)
                    .IsRequired()
                    .HasColumnName("ALLOT_NO")
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("AMOUNT")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.ChildIndexPk).HasColumnName("ChildIndexPK");

                entity.Property(e => e.Day).HasColumnName("DAY");

                entity.Property(e => e.DefaultColor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IndexPk).HasColumnName("IndexPK");

                entity.Property(e => e.Time)
                    .HasColumnName("TIME")
                    .HasColumnType("datetime");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("UID")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdditionalData>(entity =>
            {
                entity.HasKey(e => e.IndexPk);

                entity.HasComment("附加標籤檔");

                entity.Property(e => e.IndexPk).HasColumnName("IndexPK");

                entity.Property(e => e.AdditionalName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("標籤名稱");

                entity.Property(e => e.CreaterUid)
                    .IsRequired()
                    .HasColumnName("CreaterUID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("標籤創造者");

                entity.Property(e => e.DefaultAmountColor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultColor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("此項目出現在網頁上chip顏色");

                entity.Property(e => e.IndexLevel).HasComment("層次");

                entity.Property(e => e.IsDefault)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");
            });

            modelBuilder.Entity<CodeList>(entity =>
            {
                entity.HasKey(e => e.CodeIndex);

                entity.Property(e => e.CodeIndex)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("代碼");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("代碼");

                entity.Property(e => e.CodeName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.IndexName)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<DefauleAmount>(entity =>
            {
                entity.HasKey(e => e.Srno);

                entity.HasComment("預設金額設定檔");

                entity.Property(e => e.Srno)
                    .HasColumnName("SRNO")
                    .HasComment("顯示順序")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 6)");
            });

            modelBuilder.Entity<DropDownData>(entity =>
            {
                entity.HasKey(e => new { e.SourceCode, e.Value, e.Value2 });

                entity.HasComment("選項資料表");

                entity.Property(e => e.SourceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("資料代碼");

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("值");

                entity.Property(e => e.Value2)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreaterDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreaterId)
                    .IsRequired()
                    .HasColumnName("CreaterID")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('System')");

                entity.Property(e => e.SourceText)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("資料代碼名稱");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("名稱");

                entity.Property(e => e.Text2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("名稱");

                entity.Property(e => e.Text3)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')")
                    .HasComment("名稱");

                entity.Property(e => e.Value3)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<HtmlCssSet>(entity =>
            {
                entity.HasKey(e => new { e.PageCode, e.CssTag, e.Width, e.Height });

                entity.HasComment("設定網頁CSS主檔");

                entity.Property(e => e.PageCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁代碼");

                entity.Property(e => e.CssTag)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CssTagName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')")
                    .HasComment("CssTag名稱");
            });

            modelBuilder.Entity<HtmlCssSetDetail>(entity =>
            {
                entity.HasKey(e => new { e.Uid, e.PageCode, e.CssTag, e.Css, e.Width, e.Height, e.IsSet });

                entity.HasComment("設定網頁CSS內容");

                entity.Property(e => e.Uid)
                    .HasColumnName("UID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PageCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CssTag)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Css)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.IsSet)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Y')");

                entity.Property(e => e.CssName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HtmlSet>(entity =>
            {
                entity.HasKey(e => new { e.PageCode, e.Id })
                    .HasName("PK_HtmlSet_1");

                entity.HasComment("設定動態網頁資料表");

                entity.Property(e => e.PageCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁代碼");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("對應Html ID");

                entity.Property(e => e.Describe)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.GearingId)
                    .IsRequired()
                    .HasColumnName("GearingID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("欄位");

                entity.Property(e => e.Html)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("欄位");

                entity.Property(e => e.Js)
                    .IsRequired()
                    .HasColumnName("JS")
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RwdSet)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .HasComment("是否在Rwd設定頁中可以設定");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("欄位");
            });

            modelBuilder.Entity<PageData>(entity =>
            {
                entity.HasKey(e => e.PageCode);

                entity.Property(e => e.PageCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁名稱(en)");

                entity.Property(e => e.PageArchitecture)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁架構");

                entity.Property(e => e.PageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁名稱");

                entity.Property(e => e.PageUrl)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁網址");

                entity.Property(e => e.RwdUrl)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("網頁網址");
            });

            modelBuilder.Entity<UserAdditional>(entity =>
            {
                entity.HasKey(e => e.IndexPk);

                entity.HasComment("使用者建立項目資料檔");

                entity.Property(e => e.IndexPk).HasColumnName("IndexPK");

                entity.Property(e => e.AdditionalName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("標籤名稱");

                entity.Property(e => e.CreaterUid)
                    .IsRequired()
                    .HasColumnName("CreaterUID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("標籤創造者");
            });

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("IX_UserData");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50);

                entity.Property(e => e.Authority)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('2')")
                    .HasComment("權限 0:管理員;1開發者;2Demo訪客;3:使用者");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LateLoginTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(50);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("SEX")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("UID")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.VaildId)
                    .IsRequired()
                    .HasColumnName("VaildID")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
