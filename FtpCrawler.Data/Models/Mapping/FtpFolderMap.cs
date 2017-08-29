using System.Data.Entity.ModelConfiguration;

namespace FtpCrawler.Data.Models.Mapping
{
    public class FtpFolderMap : EntityTypeConfiguration<FtpFolder>
    {
        public FtpFolderMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.ShortName)
               .IsRequired()
               .HasMaxLength(250);
            this.Property(t => t.FullName)
               .IsRequired()
               .HasMaxLength(2500);

            // Table & Column Mappings
            this.ToTable("FtpFolder");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Modified).HasColumnName("Modified");
            this.Property(t => t.ServerId).HasColumnName("ServerId");
            this.Property(t => t.FolderId).HasColumnName("FolderId");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.FullName).HasColumnName("FullName");

            // Relationships
            this.HasRequired(t => t.Server)
                .WithMany(t => t.Folders)
                .HasForeignKey(d => d.ServerId);

            this.HasOptional(t => t.ParentFolder)
                .WithMany(t => t.SubFolder)
                .HasForeignKey(d => d.FolderId);
        }
    }
}