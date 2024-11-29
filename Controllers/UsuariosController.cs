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
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                
            };

            return Ok(usuarioDto);
        }


        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDto usuarioDto)
        {
            // Verificar si el ID del usuario en la URL coincide con el ID del objeto recibido
            if (id != usuarioDto.Id)
            {
                return BadRequest(new { mensaje = "El ID del usuario no coincide." });
            }

            // Buscar el usuario existente en la base de datos
            var usuarioExistente = await _context.Usuarios.FindAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound(new { mensaje = "El usuario no existe." });
            }

            // Actualizar las propiedades del usuario existente
            usuarioExistente.Nombre = usuarioDto.Nombre;

            // Marcar la entrada como modificada
            _context.Entry(usuarioExistente).State = EntityState.Modified;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar excepciones de concurrencia
                if (!UsuarioExists(id))
                {
                    return NotFound(new { mensaje = "El usuario no existe." });
                }
                else
                {
                    throw; // Re-lanzar la excepción si hay otro problema
                }
            }

            return NoContent(); // Retornar 204 No Content si la actualización fue exitosa
        }

        // Método auxiliar para verificar si un usuario existe
        

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioDto usuarioDto)
        {
            var usuario = new Usuario
            {
                Nombre = usuarioDto.Nombre
               
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuarioDto.Id = usuario.Id;
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioDto.Id }, usuarioDto);
        }
    

    // DELETE: api/Usuarios/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

    }
}
