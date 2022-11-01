using Prueba.Biblioteca.Domain;
using Prueba.Biblioteca.Infraestructure;

namespace Prueba.Biblioteca.Application;



public class LibroAppService : ILibroAppService
{
    private readonly ILibroRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly BibliotecaDbContext dbContext;

    public LibroAppService(ILibroRepository repository, IUnitOfWork unitOfWork, BibliotecaDbContext dbContext)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.dbContext = dbContext;
    }

    public async Task<LibroDto> CreateAsync(LibroCrearActualizarDto libroDto)
    {
        
        //Reglas Validaciones... 
        var existeNombreLibro = await repository.ExisteNombre(libroDto.Nombre);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un libro con el nombre {libroDto.Nombre}");
        }
 
        //Mapeo Dto => Entidad
        var libro = new Libro();
        libro.Nombre = libroDto.Nombre;
        libro.AutorId = libroDto.AutorId;
        libro.EditorialId = libroDto.EditorialId;

        //Persistencia objeto
        libro = await repository.AddAsync(libro);
        await repository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var libroCreado = new LibroDto();
        libroCreado.Nombre = libro.Nombre;
        libroCreado.Id = libro.Id;

        //TODO: Enviar un correo electronica... 

        return libroCreado;
    }

    public async Task UpdateAsync(int id, LibroCrearActualizarDto libroDto)
    {
        var libro = await repository.GetByIdAsync(id);
        if (libro == null){
            throw new ArgumentException($"La libro con el id: {id}, no existe");
        }
        
        var existeNombreLibro = await repository.ExisteNombre(libroDto.Nombre,id);
        if (existeNombreLibro){
            throw new ArgumentException($"Ya existe un libro con el nombre {libroDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        libro.Nombre = libroDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(libro);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int libroId)
    {
        //Reglas Validaciones... 
        var libro = await repository.GetByIdAsync(libroId);
        if (libro == null){
            throw new ArgumentException($"El libro con el id: {libroId}, no existe");
        }

        repository.Delete(libro);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ListaPaginada<LibroDto> GetAll(int limit=10,int offset=0)
    {
        var axu = repository.GetAll().ToList();
        var ListaPaginada = repository.GetAll().Skip(offset).Take(limit).Select(x=>x).ToList();

        var resultado = new ListaPaginada<LibroDto>();

        var lista = new List<LibroDto>();

        ListaPaginada.ForEach(x=>lista.Add(new LibroDto(){
                            Autor=x.Autor,
                            AutorId=x.AutorId,
                            Editorial=x.Editorial,
                            EditorialId=x.EditorialId,
                            Id=x.Id,
                            Nombre=x.Nombre,
                            Observaciones=x.Observaciones,
                            Precio=x.Precio
        }));
        resultado.Lista = lista;
        resultado.Total=lista.Count;

        return resultado;
    }
}
