using System.Text.Json.Serialization;

namespace Gestion_de_Tareas.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public bool Completada { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }    


        
    }
}
