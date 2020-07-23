using Interfaces;
using NUnit.Framework;
using Services;
using UnityEngine;

namespace Tests.EditorModeTests
{
    [TestFixture]
    public abstract  class TestClassBase
    {
        protected GameManager GameManager;
        protected IPlatformManager PlatformManager;
        protected IScoreManager ScoreManager;
        protected ICameraManager CameraManager;
        
        [SetUp]
        public void Setup()
        {
            GameManager = GetGameManager();
            PlatformManager = new PlatformManager();
            ScoreManager = new ScoreManager();
            CameraManager = new CameraManager(GameManager.gameCamera);
        }
        
        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(GameManager.gameObject);
            foreach (var platform in Object.FindObjectsOfType<MovingPlatform>())
            {
                Object.DestroyImmediate(platform);
            }
        }
        
        private GameManager GetGameManager()
        {
            var gameGameObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            return gameGameObject.GetComponent<GameManager>();
        }
    }
}