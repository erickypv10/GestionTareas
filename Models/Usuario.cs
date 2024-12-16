using System.Text.Json.Serialization;

namespace Gestion_de_Tareas.Models
{
    public class Usuario
    {
            public int Id { get; set; }               // Identificador único del usuario
            public string Nombre { get; set; } = null!;      // Nombre del usuario

            public HashSet<Tarea> Tareas { get; set; }// Relación con las tareas del usuario
        

    }
}
