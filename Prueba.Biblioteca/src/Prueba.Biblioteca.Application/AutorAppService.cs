
using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;



public class AutorAppService : IAutorAppService
{
    private readonly IAutorRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public AutorAppService(IAutorRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<AutorDto> CreateAsync(AutorCrearActualizarDto autorDto)
    {
        
        //Reglas Validaciones... 
        var existeNombreAutor = await repository.ExisteNombre(autorDto.Nombre);
        if (existeNombreAutor){
            throw new ArgumentException($"Ya existe un autor con el nombre {autorDto.Nombre}");
        }
 
        //Mapeo Dto => Entidad
        var autor = new Autor();
        autor.Nombre = autorDto.Nombre;
        autor.Apellido = autorDto.Apellido;
 
        //Persistencia objeto
        autor = await repository.AddAsync(autor);
        await repository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var autorCreado = new AutorDto();
        autorCreado.Nombre = autor.Nombre;
        autorCreado.Id = autor.Id;

        //TODO: Enviar un correo electronica... 

        return autorCreado;
    }

    public async Task UpdateAsync(int id, AutorCrearActualizarDto autorDto)
    {
        var autor = await repository.GetByIdAsync(id);
        if (autor == null){
            throw new ArgumentException($"La autor con el id: {id}, no existe");
        }
        
        var existeNombreAutor = await repository.ExisteNombre(autorDto.Nombre,id);
        if (existeNombreAutor){
            throw new ArgumentException($"Ya existe un autor con el nombre {autorDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        autor.Nombre = autorDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(autor);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int autorId)
    {
        //Reglas Validaciones... 
        var autor = await repository.GetByIdAsync(autorId);
        if (autor == null){
            throw new ArgumentException($"El autor con el id: {autorId}, no existe");
        }

        repository.Delete(autor);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<AutorDto> GetAll()
    {
        var autorList = repository.GetAll();

        var autorListDto =  from m in autorList
                            select new AutorDto(){
                                Id = m.Id,
                                Nombre = m.Nombre
                            };

        return autorListDto.ToList();
    }

    
}
 