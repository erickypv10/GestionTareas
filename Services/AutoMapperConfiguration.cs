
using AutoMapper;
using Gestion_de_Tareas.DTOs;
using Gestion_de_Tareas.Models;

namespace Gestion_de_Tareas.Services
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<TareaCracionDto, Tarea>();
            CreateMap<Tarea, TareaDto>();
        }
    }
}
