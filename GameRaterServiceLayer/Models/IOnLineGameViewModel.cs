using System.Collections.Generic;
using GameRaterData.Model;

namespace GameRaterServiceLayer.Models
{
    public interface IOnLineGameViewModel
    {
        OnLineGame Entity { get; set; }
        List<OnLineGame> OnLineGames { get; set; }
        OnLineGame SearchEntity { get; set; }

        void HandleRequest();
    }
}