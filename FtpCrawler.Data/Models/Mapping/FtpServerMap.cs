using System.Data.Entity.ModelConfiguration;

namespace FtpCrawler.Data.Models.Mapping
{
    public class FtpServerMap : EntityTypeConfiguration<FtpServer>
    {
        public FtpServerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.HostName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.PassWord)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.StartingDir)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.FileList)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Comment)
               .IsRequired()
               .HasMaxLength(250);

            this.Property(t => t.EditableBy)
               .IsRequired()
               .HasMaxLength(250);

            this.Property(t => t.Port).IsRequired();


            // Table & Column Mappings
            this.ToTable("FtpServer");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Created).HasColumnName("Created");
            this.Property(t => t.Modified).HasColumnName("Modified");
            this.Property(t => t.HostName).HasColumnName("HostName");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.PassWord).HasColumnName("PassWord");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.StartingDir).HasColumnName("StartingDir");
            this.Property(t => t.FileList).HasColumnName("FileList");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.EditableBy).HasColumnName("EditableBy");

        }
    }
}
