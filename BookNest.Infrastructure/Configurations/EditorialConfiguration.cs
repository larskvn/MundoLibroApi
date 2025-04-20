using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookNest.Infrastructure.Configurations;

public class EditorialConfiguration: IEntityTypeConfiguration<Editorial>
{
    public void Configure(EntityTypeBuilder<Editorial> builder)
    {
        builder.ToTable("editoriales");  // Nombre real en la BD
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Code).HasColumnName("codigo");
        builder.Property(x => x.Name).HasColumnName("nombre");
        builder.Property(x => x.RegistrationDate).HasColumnName("fecha_registro");
        builder.Property(x => x.Status).HasColumnName("estado");

    }
}