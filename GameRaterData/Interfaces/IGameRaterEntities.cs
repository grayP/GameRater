using GameRaterData.Model;
using System.Data.Entity;

namespace GameRaterData.Interfaces
{
    public interface IGameRaterEntities
    {
        DbSet<OnLineGame> OnLineGames { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}