using System.Data.Entity.ModelConfiguration;

namespace FtpCrawler.Data.Models.Mapping
{
    public class WebUserMap : EntityTypeConfiguration<WebUser>
    {
        public WebUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.UserName)
               .IsRequired()
               .HasMaxLength(250);
            this.Property(t => t.Password)
               .IsRequired()
               .HasMaxLength(2500);

            // Table & Column Mappings
            this.ToTable("WebUser");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Modified).HasColumnName("Modified");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.UserType).HasColumnName("UserType");

            // Relationships
        }
    }
}