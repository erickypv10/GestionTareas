namespace Gestion_de_Tareas.DTOs
{
    public class TareaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Completada { get; set; }
        public int UsuarioId { get; set; } // Solo el ID del usuario
    }
}
