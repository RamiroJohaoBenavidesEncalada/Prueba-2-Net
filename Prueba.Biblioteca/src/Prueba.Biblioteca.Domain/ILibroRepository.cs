namespace Prueba.Biblioteca.Domain;

public interface ILibroRepository :  IRepository<Libro> {


    Task<bool> ExisteNombre(string nombre);

    Task<bool> ExisteNombre(string nombre, int idExcluir);


}


