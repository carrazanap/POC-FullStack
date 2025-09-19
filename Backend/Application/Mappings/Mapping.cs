using Application.DataTransferObjects;
using Application.DomainEvents;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    /// <summary>
    /// El mapeo entre objetos debe ir definido aqui
    /// </summary>
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DummyEntity, DummyEntityCreated>().ReverseMap();
            CreateMap<DummyEntity, DummyEntityUpdated>().ReverseMap();
            CreateMap<DummyEntity, DummyEntityDto>().ReverseMap();

            CreateMap<Alumno, AlumnoCreado>().ReverseMap();

            // Mapeos para Automóvil
            CreateMap<Automovil, AutomovilDto>().ReverseMap();
            CreateMap<CrearAutomovilDto, Automovil>()
                .ConstructUsing(dto => new Automovil(dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion, dto.NumeroMotor, dto.NumeroChasis));
            CreateMap<ActualizarAutomovilDto, Automovil>()
                .ConstructUsing(dto => new Automovil(dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion, dto.NumeroMotor, dto.NumeroChasis));
        }
    }
}

