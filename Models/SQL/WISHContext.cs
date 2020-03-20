using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models.SQL
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
        public virtual DbSet<ComCard> ComCard { get; set; }
        public virtual DbSet<DefauleAmount> DefauleAmount { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<HtmlSet> HtmlSet { get; set; }
        public virtual DbSet<UserAdditional> UserAdditional { get; set; }
        public virtual DbSet<UserData> UserData { get; set; }
        public virtual DbSet<UserM> UserM { get; set; }

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
                entity.HasNoKey();

                entity.Property(e => e.AccoutingDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(((1900)/(1))/(1))")
                    .HasComment("記帳時間");

                entity.Property(e => e.Additional)
                    .IsRequired()
                    .HasColumnName("ADDITIONAL")
                    .HasMaxLength(50);

                entity.Property(e => e.AllotNo)
                    .IsRequired()
                    .HasColumnName("ALLOT_NO")
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

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
                entity.HasNoKey();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("代碼");

                entity.Property(e => e.CodeIndex)
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

            modelBuilder.Entity<ComCard>(entity =>
            {
                entity.HasKey(e => new { e.Round, e.Card })
                    .HasName("PK_ComCard_1");

                entity.Property(e => e.CardStatus).HasColumnName("Card_Status");
            });

            modelBuilder.Entity<DefauleAmount>(entity =>
            {
                entity.HasNoKey();

                entity.HasComment("預設金額設定檔");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Srno)
                    .HasColumnName("SRNO")
                    .HasDefaultValueSql("((0))")
                    .HasComment("顯示順序");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Action).HasComment("目前動作 1使用者回合 2 電腦回合 3使用者選卡");

                entity.Property(e => e.DecMemo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Executor)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("執行者 1：USER 2：COM 3：AUTO");

                entity.Property(e => e.OrderSrno).HasComment("動作順序");

                entity.Property(e => e.Round).HasComment("回合");

                entity.Property(e => e.UserPlay)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<HtmlSet>(entity =>
            {
                entity.HasNoKey();

                entity.HasComment("設定動態網頁資料表");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("對應Html ID");

                entity.Property(e => e.Lebel)
                    .IsRequired()
                    .HasColumnName("lebel")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("欄位");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .HasComment("欄位");
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

            modelBuilder.Entity<UserM>(entity =>
            {
                entity.HasKey(e => new { e.UserPlay, e.Round });

                entity.Property(e => e.UserPlay).HasMaxLength(50);

                entity.Property(e => e.Hp).HasDefaultValueSql("((-1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
