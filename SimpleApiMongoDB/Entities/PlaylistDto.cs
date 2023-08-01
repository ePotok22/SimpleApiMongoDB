namespace SimpleApiMongoDB.Entities
{
    public class PlaylistDto: Playlist
    {
        private PlaylistDto (Playlist playlist)
        {
            Id = playlist.Id;
            username = playlist.username;
            movieIds = playlist.movieIds;
            createAt = DateTime.Now;
        }

        public static PlaylistDto CreateDto(Playlist playlist) =>
            new PlaylistDto(playlist);

        public DateTime createAt { get; set; }

        public DateTime? updateAt { get; set; } = null!;

    }
}
