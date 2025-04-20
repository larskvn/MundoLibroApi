using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookNest.Infrastructure.Configurations;

public class BookConfiguration: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("libros");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Isbn).HasColumnName("isbn");
        builder.Property(x => x.Title).HasColumnName("titulo");
        builder.Property(x => x.Authors).HasColumnName("autores");
        builder.Property(x => x.Edition).HasColumnName("edicion");
        builder.Property(x => x.Year).HasColumnName("anio");
        builder.Property(x => x.EditorialId).HasColumnName("id_editorial");
        builder.Property(x => x.RegistrationDate).HasColumnName("fecha_registro");
        builder.Property(x => x.Status).HasColumnName("estado");

        builder.HasOne(x => x.Editorial)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.EditorialId)
            .HasConstraintName("fk_libros_id_editorial");

    }
}