using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestion_de_Tareas.Models.Configuraciones
{
    public class TareaConfig : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.Property(prop => prop.Descripcion).HasMaxLength(500);
        }
    }
    
}
