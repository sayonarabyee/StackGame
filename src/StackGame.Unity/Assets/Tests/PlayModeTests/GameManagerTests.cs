using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Tests.PlayModeTests
{
    public class GameManagerTests
    {
        private GameManager _gameManager;

        #region Configuration

        [SetUp]
        public void Setup()
        {
            _gameManager = GetGameManager();
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

        #endregion

        #region UpdateGameState

        [UnityTest]
        public IEnumerator UpdateGameState_CorrectData_IncreasePlatformsCount()
        {
            _gameManager.StartGame();
            var initialCount = Object.FindObjectsOfType<MovingPlatform>().Length;

            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var count = Object.FindObjectsOfType<MovingPlatform>().Length;

            Assert.AreEqual(count, initialCount + 1);
        }

        [UnityTest]
        public IEnumerator UpdateGameState_CorrectData_CurrentPlatformMoving()
        {
            _gameManager.StartGame();
            var platform = Object.FindObjectsOfType<MovingPlatform>().Last();

            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            platform = Object.FindObjectsOfType<MovingPlatform>().First();

            Assert.GreaterOrEqual(platform.initialSpeed, Constants.MovingPlatform.InitialSpeed);
        }

        [UnityTest]
        public IEnumerator UpdateGameState_CorrectData_PreviousPlatformStopped()
        {
            _gameManager.StartGame();
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var firstPlatform = Object.FindObjectsOfType<MovingPlatform>().ElementAtOrDefault(1);

            Assert.AreEqual(0, firstPlatform.initialSpeed);
        }

        //TODO: v: fix it later
        /*[UnityTest]
        public IEnumerator UpdateGameState_TenTimes_AllPlatformsNotNull()
        {
            _gameManager.StartGame();
            var expectedPlatformCount = 10;

            for (var i = 1; i < expectedPlatformCount; i++)
            {
                var platform = Object.FindObjectsOfType<MovingPlatform>().ElementAtOrDefault(1);
                yield return WaitForNextPlatform(platform);

                _gameManager.UpdateGameState();
            }

            var platforms = Object.FindObjectsOfType<MovingPlatform>();
            var count = platforms.Length;
            var nullPlatforms = platforms.Count(x => x == null);

            Assert.AreEqual(expectedPlatformCount, count);
            Assert.AreEqual(0, nullPlatforms);
        }*/

        #endregion

        #region Private Methods

        private GameManager GetGameManager()
        {
            var gameGameObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            return gameGameObject.GetComponent<GameManager>();
        }

        private WaitForSeconds WaitForNextPlatform()
        {
            var previousPosition = Constants.Cube.InitialPosZ;
            var platform = Object.FindObjectsOfType<MovingPlatform>().Last();
            var distance = previousPosition - platform.transform.position.z;
            var waitTime = distance / platform.initialSpeed;
            return new WaitForSeconds(waitTime);
        }

        #endregion
    }
}