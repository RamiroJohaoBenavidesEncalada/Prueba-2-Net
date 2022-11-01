using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;



public interface ILibroAppService
{

    ListaPaginada<LibroDto> GetAll(int limit=10,int offset=0);

    Task<LibroDto> CreateAsync(LibroCrearActualizarDto libro);

    Task UpdateAsync (int id, LibroCrearActualizarDto libro);

    Task<bool> DeleteAsync(int libroId);
}

