using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_de_Tareas.Data;
using Gestion_de_Tareas.Models;
using Gestion_de_Tareas.DTOs;

namespace Gestion_de_Tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaDto>>> GetTareas()
        {
            return await _context.Tareas.Select(t => new TareaDto
            {
                Id = t.Id,
                Descripcion = t.Descripcion,
                Completada = t.Completada,
                UsuarioId = t.UsuarioId
            }).ToListAsync();
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetTarea(int id)
        {
            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
            {
                return NotFound();
            }

            var tareaDto = new TareaDto
            {
                Id = tarea.Id,
                Descripcion = tarea.Descripcion,
                UsuarioId = tarea.UsuarioId // Solo el ID del usuario
            };

            return Ok(tareaDto);
        }


        // PUT: api/Tareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, TareaDto tareaDto)
        {
            // Verificar si el ID de la tarea en la URL coincide con el ID del objeto recibido
            if (id != tareaDto.Id)
            {
                return BadRequest(new { mensaje = "El ID de la tarea no coincide." });
            }

            // Buscar la tarea existente en la base de datos
            var tareaExistente = await _context.Tareas.FindAsync(id);
            if (tareaExistente == null)
            {
                return NotFound(new { mensaje = "La tarea no existe." });
            }

            // Actualizar las propiedades de la tarea existente
            tareaExistente.Descripcion = tareaDto.Descripcion;
            tareaExistente.Completada = tareaDto.Completada;
            tareaExistente.UsuarioId = tareaDto.UsuarioId; // Asegúrate de que el UsuarioId sea válido

            // Marcar la entrada como modificada
            _context.Entry(tareaExistente).State = EntityState.Modified;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar excepciones de concurrencia
                if (!TareaExists(id))
                {
                    return NotFound(new { mensaje = "La tarea no existe." });
                }
                else
                {
                    throw; // Re-lanzar la excepción si hay otro problema
                }
            }

            return NoContent(); // Retornar 204 No Content si la actualización fue exitosa
        }

        // Método auxiliar para verificar si una tarea existe
       

        // POST: api/Tareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TareaDto>> PostTarea(TareaDto tareaDto)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(tareaDto.UsuarioId);
            if (usuarioExistente == null)
            {
                return NotFound(new { mensaje = "El usuario no existe." });
            }
            var tarea = new Tarea
            {
                Descripcion = tareaDto.Descripcion,
                Completada = tareaDto.Completada,
                UsuarioId = tareaDto.UsuarioId
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTareas), new { id = tarea.Id }, tareaDto);
        }

            // DELETE: api/Tareas/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<TareaDto>>> GetTareasPorUsuario(int usuarioId)
        {
            // Verificar si el usuario existe
            var usuarioExistente = await _context.Usuarios.FindAsync(usuarioId);
            if (usuarioExistente == null)
            {
                return NotFound(new { mensaje = "El usuario no existe." });
            }

            // Obtener las tareas asociadas al usuario
            var tareas = await _context.Tareas
                .Where(t => t.UsuarioId == usuarioId)
                .Select(t => new TareaDto
                {
                    Id = t.Id,
                    Descripcion = t.Descripcion,
                    Completada = t.Completada,
                    UsuarioId = t.UsuarioId
                })
                .ToListAsync();

            if (!tareas.Any())
            {
                return NotFound(new { mensaje = "No se encontraron tareas para este usuario." });
            }

            return Ok(tareas); // Retornar 200 OK con la lista de tareas
        }

        [HttpPost("usuario/{usuarioId}")]
        public async Task<ActionResult<TareaDto>> AsignarTareaAUsuario(int usuarioId, TareaDto tareaDto)
        {
            // Verificar si el usuario existe
            var usuarioExistente = await _context.Usuarios.FindAsync(usuarioId);
            if (usuarioExistente == null)
            {
                return NotFound(new { mensaje = "El usuario no existe." });
            }

            // Crear una nueva tarea y asignarla al usuario
            var tarea = new Tarea
            {
                Descripcion = tareaDto.Descripcion,
                Completada = tareaDto.Completada,
                UsuarioId = usuarioId // Asignar el ID del usuario
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            // Retornar la tarea creada como respuesta
            return CreatedAtAction(nameof(GetTareasPorUsuario), new { usuarioId = usuarioId }, tareaDto);
        }
    }


}
