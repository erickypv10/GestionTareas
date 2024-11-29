using Gestion_de_Tareas.Models;
using static Gestion_de_Tareas.Models.Tarea;
namespace Gestion_de_Tareas.DTOs
{
    public class TareaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; } // Solo el ID del usuario
        public EstadoTarea Estado { get; set; }

       
    }
}
