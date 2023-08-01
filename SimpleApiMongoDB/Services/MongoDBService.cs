using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleApiMongoDB.Models;
using SimpleApiMongoDB.Entities;
using SimpleApiMongoDB.Services.Interfaces;

namespace SimpleApiMongoDB.Services
{
    public class MongoDBService: IMongoDBService
    {
        private readonly IMongoCollection<PlaylistDto> _playlistCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<PlaylistDto>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<PlaylistDto>> GetAllAsync() =>
            await _playlistCollection.Find(new BsonDocument()).ToListAsync();

        public async Task<PlaylistDto> GetAsync(string id)
        {
            FilterDefinition<PlaylistDto> filter = Builders<PlaylistDto>.Filter.Eq("Id", id);
            return await _playlistCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(PlaylistDto playList) =>
             await _playlistCollection.InsertOneAsync(playList);

        public async Task AddToPlayListAsync(string id, string movieId)
        {
            FilterDefinition<PlaylistDto> filter = Builders<PlaylistDto>.Filter.Eq("Id", id);
            UpdateDefinition<PlaylistDto> update = Builders<PlaylistDto>.Update
                .AddToSet<string>("movieIds", movieId)
                .Set("updateAt", DateTime.Now);
            await _playlistCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<PlaylistDto> filter = Builders<PlaylistDto>.Filter.Eq("Id", id);
            await _playlistCollection.DeleteOneAsync(filter);
        }
    }
}
