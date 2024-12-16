using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_de_Tareas.Data;
using Gestion_de_Tareas.Models;
using Gestion_de_Tareas.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Reflection.Metadata.Ecma335;
using static Gestion_de_Tareas.Models.Tarea;


namespace Gestion_de_Tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public TareasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        //Listado de tareas
        [HttpGet]
        public async Task<IEnumerable<TareaDto>> GetTareas()
        {
          return await _context.Tareas
                .ProjectTo<TareaDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            
        }

        //Buscar una tarea por su Id
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetTarea(int id)
        {
            var tarea = await _context.Tareas
                .ProjectTo<TareaDto>(mapper.ConfigurationProvider)
                .Where(t => t.id == id)
                .ToListAsync();
                

            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        //Buscar las tareas de un usuario por su Id
        [HttpGet("buscarPorUsuario")]
        public async Task<IEnumerable<TareaDto>> GetTareaUsuarioId(int id)
        {
            return await _context.Tareas
                .ProjectTo<TareaDto>(mapper.ConfigurationProvider)
                .Where(t => t.UsuarioId == id)
                .ToListAsync();
          
        }



        //Crear Tarea
        [HttpPost]
        public async Task<ActionResult<TareaDto>> Post(TareaDto tareaDto)
        {
            var tarea = new Tarea
            {
                Descripcion = tareaDto.Descripcion,
                UsuarioId = tareaDto.UsuarioId
            };
            
                await _context.Tareas.AddAsync(tarea);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTarea), new { Id = tarea.Id }, tareaDto);
        
        }

            // Eliminar una Tarea
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


        //Actualizar una Tarea por su Id
        [HttpPut("{id=int}")]
        public async Task<ActionResult> Put(int id, TareaCracionDto tareaCracionDto)
        {
            var NuevaTarea = mapper.Map<Tarea>(tareaCracionDto);
            NuevaTarea.Id = id;
            _context.Update(NuevaTarea);
            await _context.SaveChangesAsync();
            return Ok();
            
        }

        [HttpGet("porCompletadas")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetCompletadas(EstadoTarea estado)
        {
           return await _context.Tareas
                .Where(x => x.Estado == estado)
                .ToListAsync();
        }

        
    }


}
