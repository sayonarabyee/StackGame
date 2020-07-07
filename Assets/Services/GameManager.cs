using Interfaces;
using UnityEngine;

namespace Services
{
    public class GameManager : IGameManager
    {
        private const string MovingPlatform = "MovingPlatform"; 
        public void StopMovingPlatform()
        {
            var platform = GameObject.Find(MovingPlatform).GetComponentInChildren<MovingPlatform>();
            platform.platformSpeed = 0;
        }
    }
}