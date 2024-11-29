

namespace Gestion_de_Tareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }   
        public EstadoTarea Estado { get; set; }

        public enum EstadoTarea
        {
            pendiente,
            completada
        }


        
    }
}
