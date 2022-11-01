

using Prueba.Biblioteca.Application;
using Microsoft.AspNetCore.Mvc;

namespace Prueba.Biblioteca.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class LibroController : ControllerBase
{

    private readonly ILibroAppService libroAppService;

    public LibroController(ILibroAppService libroAppService)
    {
        this.libroAppService = libroAppService;
    }

    [HttpGet]
    public ListaPaginada<LibroDto> GetAll(int limit=10,int offset=0)
    {

        return libroAppService.GetAll(limit,offset);

    }

    [HttpPost]
    public async Task<LibroDto> CreateAsync(LibroCrearActualizarDto libro)
    {

        return await libroAppService.CreateAsync(libro);

    }

    [HttpPut]
    public async Task UpdateAsync(int id, LibroCrearActualizarDto libro)
    {

        await libroAppService.UpdateAsync(id, libro);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(int libroId)
    {

        return await libroAppService.DeleteAsync(libroId);

    }

}
