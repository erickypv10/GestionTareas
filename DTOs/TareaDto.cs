using Gestion_de_Tareas.Models;
using static Gestion_de_Tareas.Models.Tarea;
namespace Gestion_de_Tareas.DTOs
{
    public class TareaDto
    {
        public int id { get; set; }
        public string Descripcion { get; set; } = null!;
        public int UsuarioId { get; set; } // Solo el ID del usuario
        public EstadoTarea Estado { get; set; }

        public DateTime FechaOrden { get; set; }

       
    }
}
