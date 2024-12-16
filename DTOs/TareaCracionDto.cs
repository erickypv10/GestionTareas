using System.ComponentModel.DataAnnotations;
using static Gestion_de_Tareas.Models.Tarea;

namespace Gestion_de_Tareas.DTOs
{
    public class TareaCracionDto
    {
        [MaxLength(500)]
        public string? descripcion {  get; set; }
        public EstadoTarea Estado { get; set; }
        public int UsuarioId { get; set; }
    }
}
