using BookStoreMvc.Models.Domain;
using BookStoreMvc.Models.DTO;
using BookStoreMvc.Repositories.Abstract;

namespace BookStoreMvc.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext ctx;
        public BookService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Book model)
        {
            try
            {
                ctx.Books.Add(model);
                ctx.SaveChanges();
                foreach (int genreId in model.Genres)
                {
                    var BookGenres = new BookGenres
                    {
                        BookId = model.Id,
                        GenreId = genreId
                    };
                    ctx.BookGenres.Add(BookGenres);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            } 
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var BookGenress= ctx.BookGenres.Where(a => a.BookId == data.Id);
                foreach(var BookGenres in BookGenress)
                {
                    ctx.BookGenres.Remove(BookGenres);
                }
                ctx.Books.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Book GetById(int id)
        {
            var BookDetail = ctx.Books.Find(id);
            int BookId = BookDetail.Id;
            var genres = (from genre in ctx.Genres
                          join mg in ctx.BookGenres
                          on genre.Id equals mg.GenreId
                          where mg.BookId == BookId
                          select genre.GenreName
                              ).ToList();
            var genreNames = string.Join(", ", genres);
            BookDetail.GenreNames = genreNames;

            return BookDetail ;
        }

        public BookListVm List(string term="",bool paging=false, int currentPage=0)
        {
            var data = new BookListVm();
           
            var list = ctx.Books.ToList();
           
           
            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach (var Book in list)
            {
                var genres = (from genre in ctx.Genres
                              join mg in ctx.BookGenres
                              on genre.Id equals mg.GenreId
                              where mg.BookId == Book.Id
                              select genre.GenreName
                              ).ToList();
                var genreNames = string.Join(", ", genres);
                Book.GenreNames = genreNames;
            }
            data.BookList = list.AsQueryable();
            return data;
        }

        public bool Update(Book model)
        {
            try
            {
                // these genreIds are not selected by users and still present is BookGenres table corresponding to
                // this BookId. So these ids should be removed.
                var genresToDeleted = ctx.BookGenres.Where(a => a.BookId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
                foreach(var mGenre in genresToDeleted)
                {
                    ctx.BookGenres.Remove(mGenre);
                }
                foreach (int genId in model.Genres)
                {
                    var BookGenres = ctx.BookGenres.FirstOrDefault(a => a.BookId == model.Id && a.GenreId == genId);
                    if (BookGenres == null)
                    {
                        BookGenres = new BookGenres { GenreId = genId, BookId = model.Id };
                        // we have to add these genre ids in BookGenres table
                        ctx.BookGenres.Add(BookGenres);
                    }
                }

                ctx.Books.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetGenreByBookId(int BookId)
        {
            var genreIds = ctx.BookGenres.Where(a => a.BookId == BookId).Select(a => a.GenreId).ToList();
            return genreIds;
        }
       
    }
}

