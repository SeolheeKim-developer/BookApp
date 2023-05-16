using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    /// <summary>
    /// [6] Repository Class: ADO.NET or Dapper or Entity Framework Core
    /// ~Repository, ~Provider, ~Data
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly BookAppDbContext _context;
        private readonly ILogger _logger;

        public BookRepository(BookAppDbContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger(nameof(BookRepository));
        }
        #region AddAsync
        public async Task<Book> AddAsync(Book model)
        {
            try
            {
                _context.Books.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                _logger?.LogError($"ERROR({nameof(AddAsync)}): {e.Message}");
            }
            return model;
        }
        #endregion
        #region GetAllAsync

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books.OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        #endregion

        #region detail : GetByIdAsync

        public async Task<Book> GetByIdAsync(int id)
        {
            var model = await _context.Books
                //.Include(m => m.BooksLinks)
                .SingleOrDefaultAsync(m => m.Id == id);
            return model;
        }
        #endregion
        #region Update : UpdateAsync

        public async Task<bool> UpdateAsync(Book model)
        {
            try
            {
                _context.Update(model);
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger?.LogError($"ERROR({nameof(UpdateAsync)}): {e.Message}");
            }
            return false;
        }
        #endregion
        #region Delete : DeleteAsync
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var model = await _context.Books.FindAsync(id);
                _context.Remove(model);
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger?.LogError($"ERROR({nameof(DeleteAsync)}): {e.Message}");
            }
            return false;
        } 
        #endregion




    }
}
