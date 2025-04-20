using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookNest.Infrastructure.Configurations;

public class ApplicantConfiguration:IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.ToTable("solicitantes");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.FullName).HasColumnName("nombre_completo");
        builder.Property(x => x.IdentityDocument).HasColumnName("documento_identidad");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Phone).HasColumnName("telefono");
        builder.Property(x => x.RegistrationDate).HasColumnName("fecha_registro");
        builder.Property(x => x.Status).HasColumnName("estado");

    }
}