using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    public interface ICrudRepositoryBase<T, TIdentifier>
    {
        Task<T> AddAsync(T model);//input
        Task<List<T>> GetAllAsync(); //output

        Task<T> GetByIdAsync(TIdentifier id); //details
        Task<bool> UpdateAsync(T model); //modify
        Task<bool> DeleteAsync(TIdentifier id); //delete
    }
    /// <summary>
    /// [3] Generic Repository Interface => ICrudRepositoryBase.cs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBookCrudRepository<T> : ICrudRepositoryBase<T, int>
    {
        //Empty
    }
}
