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
            GameManager.StartGame();
            
            Assert.NotNull(GameManager.PlatformManager);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_ScoreManagerNotNull()
        {
            GameManager.StartGame();
            
            Assert.NotNull(GameManager.ScoreManager);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_ScoreIsZero()
        {
            GameManager.StartGame();
            
            Assert.Zero(GameManager.ScoreManager.Score);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_FirstPlatformNotNull()
        {
            GameManager.StartGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            
            Assert.NotNull(platform);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_FirstPlatformMoving()
        {
            GameManager.StartGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            
            Assert.AreEqual(platform.initialSpeed, Constants.MovingPlatform.InitialSpeed);
        }

        #endregion
    }
}
