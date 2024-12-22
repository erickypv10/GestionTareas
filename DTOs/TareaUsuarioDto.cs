namespace Gestion_de_Tareas.DTOs
{
    public class TareaUsuarioDto
    {
        public string NombreUsuario { get; set; }
        public EstadoTarea Estado { get; set; }
        public int Id { get; set; }
        public string Descripcion { get; set; } = null!;

        public enum EstadoTarea
        {
            pendiente,
            completada
        }
    }
}
