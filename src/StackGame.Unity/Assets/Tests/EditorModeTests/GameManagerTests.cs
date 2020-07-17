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
            
            Assert.Greater(platform.initialSpeed, 0);
        }

        #endregion

        #region CreateNewPlatform

        [Test]
        public void CreateNewPlatform_CorrectData_IncreasePlatformsCount()
        {
            GameManager.StartGame();
            var initialCount = Object.FindObjectsOfType<MovingPlatform>().Length;
            
            GameManager.CreateNewPlatform();
            var count = Object.FindObjectsOfType<MovingPlatform>().Length;

            Assert.AreEqual(count, initialCount+1);
        }
        
        [Test]
        public void CreateNewPlatform_CorrectData_CurrentPlatformMoving()
        {
            GameManager.StartGame();
            GameManager.CreateNewPlatform();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();

            Assert.Greater(platform.initialSpeed, 0);
        }
        
        [Test]
        public void CreateNewPlatform_CorrectData_PreviousPlatformStopped()
        {
            GameManager.StartGame();
            GameManager.CreateNewPlatform();
            var firstPlatform = Object.FindObjectsOfType<MovingPlatform>().ElementAtOrDefault(1);

            Assert.AreEqual(0, firstPlatform.initialSpeed);
        }
        
        [Test]
        public void CreateNewPlatform_TenTimes_AllPlatformsNotNull()
        {
            GameManager.StartGame();
            var expectedPlatformCount = 10;

            for (var i = 1; i < expectedPlatformCount; i++)
            {
                GameManager.CreateNewPlatform();
            }

            var platforms = Object.FindObjectsOfType<MovingPlatform>();
            var count = platforms.Length;
            var nullPlatforms = platforms.Count(x => x == null);

            Assert.AreEqual(expectedPlatformCount, count);
            Assert.AreEqual(0, nullPlatforms);
        }

        #endregion
    }
}
