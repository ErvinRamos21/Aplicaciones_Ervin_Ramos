using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using peliculas.Data;
using Pelis.Data.Interfaces;
using Pelis.Models;

namespace Pelis.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly ApplicationDbContext Context;
        public ApiRepository(ApplicationDbContext context)
        {
        
        Context = context;
        }
    public void Add<P>(P entity) where P : class
    {
            Context.Add(entity);

        }

    public void Delete<P>(P entity) where P : class
    {
            Context.Remove(entity);
        }

        public async Task<List<Pelicula>> GetPeliculaByGeneroAsync(string Genero)
        {
           var pelicula = await Context.Peliculas.Where(x => x.Genero == Genero).ToListAsync();
            return pelicula;
        }

        public async Task<Pelicula> GetPeliculaByIdAsync(int Id)
    {
            var pelicula = await Context.Peliculas.FirstOrDefaultAsync(x => x.Id == Id);
            return pelicula;
        }

    public async Task<Pelicula> GetPeliculaByNombreAsync(string Titulo)
    {
            var pelicula = await Context.Peliculas.FirstOrDefaultAsync(a => a.Titulo == Titulo);
            return pelicula;
        }

    public async Task<IEnumerable<Pelicula>> GetPeliculasAsync()
    {
            var peliculas = await Context.Peliculas.ToListAsync();
            return peliculas;
        }

    public async  Task<IEnumerable<Usuario>> GetUsuarioAsync()
    {
       var usuario = await Context.Usuarios.ToListAsync();
            return usuario;
    }

    public async Task<Usuario> GetUsuarioByIdAsync(int Id)
    {
         var usuario = await Context.Usuarios.FirstOrDefaultAsync(x => x.Id == Id);
            return usuario;
    }

    public async Task<Usuario> GetUsuarioByNombreAsync(string Nombre)
    {
        var usuario = await Context.Usuarios.FirstOrDefaultAsync(a => a.Nombre == Nombre);
            return usuario;
        }

    public async Task<bool> SaveAll()
    {
            return await Context.SaveChangesAsync() > 0;
        }
}
}