using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorModeTests
{
    public class GameManagerTests : TestClassBase
    {
        #region StartGame

        [Test]
        public void StartGame_CorrectStartGame_PlatformManagerNotNull()
        {
            GameManager.StartNewGame();
            
            Assert.NotNull(GameManager.PlatformManager);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_ScoreManagerNotNull()
        {
            GameManager.StartNewGame();
            
            Assert.NotNull(GameManager.ScoreManager);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_ScoreIsZero()
        {
            GameManager.StartNewGame();
            
            Assert.Zero(GameManager.ScoreManager.Score);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_FirstPlatformNotNull()
        {
            GameManager.StartNewGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            
            Assert.NotNull(platform);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_FirstPlatformMoving()
        {
            GameManager.StartNewGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            
            Assert.AreEqual(platform.initialSpeed, Constants.MovingPlatform.InitialSpeed);
        }

        #endregion
    }
}
