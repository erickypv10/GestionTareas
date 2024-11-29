using System.Text.Json.Serialization;

namespace Gestion_de_Tareas.Models
{
    public class Usuario
    {
            public int Id { get; set; }               // Identificador único del usuario
            public string Nombre { get; set; }        // Nombre del usuario

            public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();// Relación con las tareas del usuario
        

    }
}
