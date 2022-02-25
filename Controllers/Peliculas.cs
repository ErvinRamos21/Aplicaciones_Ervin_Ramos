using System;
namespace pelicula.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Pelis.Data.Interfaces;
    using Pelis.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class peliculaController : ControllerBase
    {
        private readonly IApiRepository Repo;


        public peliculaController(IApiRepository repo)
        {
            Repo = Repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetpeliculasAsync()
        {

            var peliculas = await Repo.GetPeliculasAsync();

            return Ok(peliculas);
        }
        [HttpPost]
        public async Task<IActionResult> Post(Pelicula peliculas)
        {

            peliculas.Fecha = DateTime.Now;

            Repo.Add(peliculas);
            if (await Repo.SaveAll())
                return Ok(peliculas);
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetpeliculasAsync(int id)
        {

            var peliculas = await Repo.GetPeliculaByIdAsync(id);
            if (peliculas == null)
                return NotFound("No existe el producto");

            return Ok(peliculas);
        }
        
        

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id){

            var peliculas = await Repo.GetPeliculaByIdAsync(id);
            if(peliculas == null)
                return NotFound("El Producto NO Se Encontro");

            Repo.Delete(peliculas);
            if(!await _repo.SaveAll())
                return BadRequest("El producto NO se puede eliminar");
            return Ok("producto borrado");
        }
        
        [HttpGet("nombre/{Titulo}")]
        public async Task<IActionResult> Get(string Titulo)
        {

            var peliculas = await Repo.GetPeliculaByNombreAsync(Titulo);
            if (peliculas == null)
                return NotFound("El producto No existe");

            return Ok(peliculas);
        }    
        [HttpGet("genero/{Genero}")]
        public async Task<IActionResult> GetGenero(string Genero)
        {

            var peliculas = await Repo.GetPeliculaByGeneroAsync(Genero);
            if (peliculas == null)
                return NotFound("El producto No existe");

            return Ok(peliculas);
        }  

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Pelicula peliculas)
        {

            if(id != peliculas.Id){
                return BadRequest("El id no coincide");
            }
            var peliculaToUpdate = await Repo.GetPeliculaByIdAsync(peliculas.Id);

            if (peliculaToUpdate == null)
                return BadRequest();

            
            peliculaToUpdate.Titulo = peliculas.Titulo;
            peliculaToUpdate.Director = peliculas.Director;
            peliculaToUpdate.Genero = peliculas.Genero;
            peliculaToUpdate.Puntuacion = peliculas.Puntuacion;
            peliculaToUpdate.Rating = peliculas.Rating;
            
            if (await Repo.SaveAll())
                return NoContent();

            return Ok(peliculaToUpdate);
        }  

    }
}