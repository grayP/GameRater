using System.Collections.Generic;
using GameRaterData.Model;

namespace GameRaterServiceLayer.Models
{
    public interface IGameManager
    {
        List<KeyValuePair<string, string>> ValidationErrors { get; set; }

        bool Delete(OnLineGame entity);
        OnLineGame Find(int GameId);
        List<OnLineGame> GetAllGames(OnLineGame entity);
        List<OnLineGame> GetAllGamesByRating(int order);
        bool Insert(OnLineGame entity);
        bool Update(OnLineGame entity);
        bool Validate(OnLineGame entity);
    }
}