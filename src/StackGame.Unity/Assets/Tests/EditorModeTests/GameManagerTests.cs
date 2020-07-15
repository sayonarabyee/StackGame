using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorMode
{
    public class Test
    {
        // A Test behaves as an ordinary method
        [Test]
        public void StartGame_CorrectStartGame_PlatformManagerNotNull()
        {
            var gameManager = GetGameManager();
            
            gameManager.StartGame();
            
            Assert.NotNull(gameManager.PlatformManager);
        }
        
        private GameManager GetGameManager()
        {
            var gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            return gameGameObject.GetComponent<GameManager>();
        }
    }
}
