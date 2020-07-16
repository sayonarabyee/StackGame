using Interfaces;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorModeTests
{
    public class PlatformManagerTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void StartGame_CorrectStartGame_PlatformManagerNotNull()
        {
            var gameManager = GetPlatformManager();
           
        }
        
        private IPlatformManager GetPlatformManager()
        {
            var gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GameManager"));
            return gameGameObject.GetComponent<GameManager>().PlatformManager;
        }
    }
}