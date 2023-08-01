using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleApiMongoDB.Models;
using SimpleApiMongoDB.Entities;

namespace SimpleApiMongoDB.Services.Interfaces
{
    public interface IMongoDBService
    {
        Task<List<PlaylistDto>> GetAllAsync();

        Task<PlaylistDto> GetAsync(string id);

        Task CreateAsync(PlaylistDto playList);

        Task AddToPlayListAsync(string id, string movieId);

        Task DeleteAsync(string id);
    }
}
