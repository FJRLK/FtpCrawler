using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Data.Models.Mapping
{
   public  class FtpFileMap : EntityTypeConfiguration<FtpFile>
    {
        public FtpFileMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            this.Property(t => t.ShortName)
               .IsRequired()
               .HasMaxLength(250);
            this.Property(t => t.FullName)
               .IsRequired()
               .HasMaxLength(2500);
            this.Property(t => t.Extension)
               .IsRequired()
               .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("FtpFile");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Modified).HasColumnName("Modified");
            this.Property(t => t.ServerId).HasColumnName("ServerId");
            this.Property(t => t.FolderId).HasColumnName("FolderId");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Extension).HasColumnName("Extension");
            this.Property(t => t.FileSize).HasColumnName("FileSize");
            this.Property(t => t.FileDate).HasColumnName("FileDate");

            // Relationships
            this.HasRequired(t => t.Folder)
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.FolderId);

            this.HasRequired(t => t.Server)
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.ServerId);
        }
    }
}
