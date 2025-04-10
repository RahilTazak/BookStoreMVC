using BookStoreMvc.Models.Domain;
using BookStoreMvc.Models.DTO;

namespace BookStoreMvc.Repositories.Abstract
{
    public interface IGenreService
    {
       bool Add(Genre model);
       bool Update(Genre model);
       Genre GetById(int id);
       bool Delete(int id);
       IQueryable<Genre> List();

    }
}
