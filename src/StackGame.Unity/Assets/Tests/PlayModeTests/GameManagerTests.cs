using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
            var platforms = SceneManager.GetActiveScene()
                .GetRootGameObjects()
                .Where(x=>x.CompareTag("movingPlatform"));
            foreach (var platform in platforms)
            {
                Object.Destroy(platform);
            }
            Object.Destroy(_gameManager.gameObject);
        }

        #endregion

        #region UpdateGameState

        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_IncreasePlatformsCount()
        {
            var initialCount = Object.FindObjectsOfType<MovingPlatform>().Length;

            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var count = Object.FindObjectsOfType<MovingPlatform>().Length;

            Assert.AreEqual(count, initialCount + 1);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_ScoreGreaterThanZero()
        {
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();

            Assert.Greater(_gameManager.ScoreManager.Score, 0);
        }

        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_CurrentPlatformMoving()
        {
            var platform = Object.FindObjectsOfType<MovingPlatform>().Last();

            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            platform = Object.FindObjectsOfType<MovingPlatform>().First();

            Assert.GreaterOrEqual(platform.initialSpeed, Constants.MovingPlatform.InitialSpeed);
        }

        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_PreviousPlatformStopped()
        {
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var firstPlatform = Object.FindObjectsOfType<MovingPlatform>().ElementAtOrDefault(1);

            Assert.AreEqual(0, firstPlatform.initialSpeed);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_SecondPlatformPositionIsCorrect()
        {
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var platformPosition = Object.FindObjectsOfType<MovingPlatform>().FirstOrDefault().transform.position;

            Assert.AreEqual(Constants.MovingPlatform.InitialPosZ, platformPosition.x, Constants.Delta);
            Assert.AreEqual(Constants.MovingPlatform.InitialPosY + Constants.MovingPlatform.InitialScaleY,
                platformPosition.y, Constants.Delta);
            Assert.AreEqual(Constants.MovingPlatform.InitialPosX, platformPosition.z, Constants.Delta);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_SecondPlatformSpeedIsCorrect()
        {
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var platform = Object.FindObjectsOfType<MovingPlatform>().FirstOrDefault();

            Assert.LessOrEqual(Constants.MovingPlatform.InitialSpeed, platform.initialSpeed);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_SecondPlatformSpeedVectorIsCorrect()
        {
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            var platform = Object.FindObjectsOfType<MovingPlatform>().FirstOrDefault();

            Assert.False(platform.isSpeedAxisZ);
        }

        [UnityTest]
        public IEnumerator UpdateGameState_TenTimesSuccessfullyStoppedPlatform_AllPlatformsNotNull()
        {
            var expectedPlatformCount = 10;

            for (var i = 1; i < expectedPlatformCount; i++)
            {
                var prevPlatform = Object.FindObjectsOfType<MovingPlatform>()?.ElementAtOrDefault(1);
                yield return WaitForNextPlatform(prevPlatform);
                _gameManager.UpdateGameState();
            }

            var platforms = Object.FindObjectsOfType<MovingPlatform>();
            var count = platforms.Length;
            var nullPlatforms = platforms.Count(x => x == null);

            Assert.AreEqual(expectedPlatformCount, count);
            Assert.AreEqual(0, nullPlatforms);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_CameraYPositionIncreased()
        {
            var cameraPos = _gameManager.gameCamera.gameObject.transform.position;
            yield return WaitForNextPlatform();

            _gameManager.UpdateGameState();
            
            Assert.Greater(_gameManager.gameCamera.transform.position.y, cameraPos.y);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_UnsuccessfullyStoppedPlatform_CameraYPositionNotIncreased()
        {
            var cameraPos = _gameManager.gameCamera.gameObject.transform.position;
            
            yield return null;
            _gameManager.UpdateGameState();

            Assert.AreEqual(cameraPos.y, _gameManager.gameCamera.transform.position.y, Constants.Delta);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_SuccessfullyStoppedPlatform_GameIsNotOver()
        {
            yield return WaitForNextPlatform();
            _gameManager.UpdateGameState();

            Assert.False(_gameManager.isGameOver);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_UnsuccessfullyStoppedPlatform_GameIsOver()
        {
            yield return null;
            _gameManager.UpdateGameState();

            Assert.True(_gameManager.isGameOver);
        }
        
        [UnityTest]
        public IEnumerator UpdateGameState_UnsuccessfullyStoppedPlatform_PlatformIsNotCreated()
        {
            var initialCount = Object.FindObjectsOfType<MovingPlatform>().Length;            
            
            yield return null;
            _gameManager.UpdateGameState();
            var actualCount = Object.FindObjectsOfType<MovingPlatform>().Length;            

            Assert.AreEqual(initialCount, actualCount);
        }

        #endregion

        #region Private Methods

        private GameManager GetGameManager()
        {
            var gameGameObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            return gameGameObject.GetComponent<GameManager>();
        }

        private static WaitForSeconds WaitForNextPlatform(MovingPlatform platform = null)
        {
            float previousPosition;
            if (platform == null)
                previousPosition = Constants.Cube.InitialPosZ;
            else
            {
                var previousPlatformPosition = platform.transform.position;
                previousPosition = Object.FindObjectsOfType<MovingPlatform>().Length % 2 == 0
                    ? previousPlatformPosition.z
                    : previousPlatformPosition.x;
            }

            var currentPlatform = Object.FindObjectsOfType<MovingPlatform>().First();
            var distance = previousPosition - currentPlatform.transform.position.z;
            if (distance < 1)
                distance = previousPosition - currentPlatform.transform.position.x;
            var waitTime = distance / currentPlatform.initialSpeed;
            return new WaitForSeconds(waitTime);
        }

        #endregion
    }
}