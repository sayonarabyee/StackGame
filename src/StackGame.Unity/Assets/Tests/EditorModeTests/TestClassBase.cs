using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorModeTests
{
    [TestFixture]
    public abstract  class TestClassBase
    {
        protected GameManager GameManager;
        [SetUp]
        public void Setup()
        {
            GameManager = GetGameManager();
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