
using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;



public class EditorialAppService : IEditorialAppService
{
    private readonly IEditorialRepository repository;
    private readonly IUnitOfWork unitOfWork;

    public EditorialAppService(IEditorialRepository repository, IUnitOfWork unitOfWork)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<EditorialDto> CreateAsync(EditorialCrearActualizarDto editorialDto)
    {
        
        //Reglas Validaciones... 
        var existeNombreEditorial = await repository.ExisteNombre(editorialDto.Nombre);
        if (existeNombreEditorial){
            throw new ArgumentException($"Ya existe una marca con el nombre {editorialDto.Nombre}");
        }
 
        //Mapeo Dto => Entidad
        var editorial = new Editorial();
        editorial.Nombre = editorialDto.Nombre;
 
        //Persistencia objeto
        editorial = await repository.AddAsync(editorial);
        await repository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var editorialCreada = new EditorialDto();
        editorialCreada.Nombre = editorial.Nombre;
        editorialCreada.Id = editorial.Id;

        //TODO: Enviar un correo electronica... 

        return editorialCreada;
    }

    public async Task UpdateAsync(int id, EditorialCrearActualizarDto editorialDto)
    {
        var editorial = await repository.GetByIdAsync(id);
        if (editorial == null){
            throw new ArgumentException($"La marca con el id: {id}, no existe");
        }
        
        var existeNombreEditorial = await repository.ExisteNombre(editorialDto.Nombre,id);
        if (existeNombreEditorial){
            throw new ArgumentException($"Ya existe una marca con el nombre {editorialDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        editorial.Nombre = editorialDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(editorial);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int editorialId)
    {
        //Reglas Validaciones... 
        var editorial = await repository.GetByIdAsync(editorialId);
        if (editorial == null){
            throw new ArgumentException($"La marca con el id: {editorialId}, no existe");
        }

        repository.Delete(editorial);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<EditorialDto> GetAll()
    {
        var editorialList = repository.GetAll();

        var editorialListDto =  from m in editorialList
                            select new EditorialDto(){
                                Id = m.Id,
                                Nombre = m.Nombre
                            };

        return editorialListDto.ToList();
    }

    
}
