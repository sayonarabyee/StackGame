using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorModeTests
{
    public class GameManagerTests
    {
        private GameManager _gameManager;
        [SetUp]
        public void Setup()
        {
            _gameManager = GetGameManager();
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void StartGame_CorrectStartGame_PlatformManagerNotNull()
        {
            _gameManager.StartGame();
            
            Assert.NotNull(_gameManager.PlatformManager);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_FirstPlatformNotNull()
        {
            _gameManager.StartGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            
            Assert.NotNull(platform);
        }
        
        [Test]
        public void StartGame_CorrectStartGame_FirstPlatformMoving()
        {
            _gameManager.StartGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            
            Assert.Greater(platform.initialSpeed, 0);
        }
        
        [Test]
        public void CreateNewPlatform_CorrectData_IncreasePlatformsCount()
        {
            _gameManager.StartGame();
            var initialCount = Object.FindObjectsOfType<MovingPlatform>().Length;
            
            _gameManager.CreateNewPlatform();
            var count = Object.FindObjectsOfType<MovingPlatform>().Length;

            Assert.AreEqual(count, initialCount+1);
        }
        
        [Test]
        public void CreateNewPlatform_CorrectData_CurrentPlatformMoving()
        {
            _gameManager.StartGame();
            _gameManager.CreateNewPlatform();
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();

            Assert.Greater(platform.initialSpeed, 0);
        }
        
        [Test]
        public void CreateNewPlatform_CorrectData_PreviousPlatformStopped()
        {
            _gameManager.StartGame();
            _gameManager.CreateNewPlatform();
            var firstPlatform = Object.FindObjectsOfType<MovingPlatform>().ElementAtOrDefault(1);

            Assert.AreEqual(0, firstPlatform.initialSpeed);
        }
        
        [Test]
        public void CreateNewPlatform_TenTimes_AllPlatformsNotNull()
        {
            _gameManager.StartGame();
            var expectedPlatformCount = 10;

            for (var i = 1; i < expectedPlatformCount; i++)
            {
                _gameManager.CreateNewPlatform();
            }

            var platforms = Object.FindObjectsOfType<MovingPlatform>();
            var count = platforms.Length;
            var nullPlatforms = platforms.Count(x => x == null);

            Assert.AreEqual(expectedPlatformCount, count);
            Assert.AreEqual(0, nullPlatforms);
        }
        
        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_gameManager.gameObject);
            foreach (var platform in Object.FindObjectsOfType<MovingPlatform>())
            {
                Object.DestroyImmediate(platform);
            }
        }
        
        private GameManager GetGameManager()
        {
            var gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            return gameGameObject.GetComponent<GameManager>();
        }
        
        
    }
}
