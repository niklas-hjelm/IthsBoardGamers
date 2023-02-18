using IthsBoardGamers.Shared;
using IthsBoardGamers.Shared.DTOs;

namespace IthsBoardGamers.DataAccess.Services;

public interface IRepository<T>
{
    Task<ServiceResponse<T?>> AddAsync(T? item);
    Task<ServiceResponse<T?[]?>> AddManyAsync(T?[]? items);
    Task<ServiceResponse<T?[]?>> GetAllAsync();
    Task<ServiceResponse<T?>> GetAsync(object id);
    Task<ServiceResponse<T?>> UpdateAsync(T? item, object id);
    Task<ServiceResponse<T?>> DeleteAsync(object id);
    Task<ServiceResponse<T?[]?>> DeleteManyAsync(T?[]? items);
}