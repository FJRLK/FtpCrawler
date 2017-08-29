using System.Data.Entity.ModelConfiguration;

namespace FtpCrawler.Data.Models.Mapping
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.Key)
               .IsRequired()
               .HasMaxLength(250);
            this.Property(t => t.Type)
              .IsRequired()
              .HasMaxLength(2500);
            this.Property(t => t.Value)
               .IsRequired()
               .HasMaxLength(2500);

            // Table & Column Mappings
            this.ToTable("Setting");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Modified).HasColumnName("Modified");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Type).HasColumnName("Password");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}