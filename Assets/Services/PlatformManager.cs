using System;
using System.Linq;
using Interfaces;
using UnityEditor;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Services
{
    public class PlatformManager : IPlatformManager
    {
        private const string MovingPlatform = "MovingPlatform";
        private const float PosX = 0.01000071f;
        private const float PosY = -0.549999f;
        private const float PosZ = -7.929996f;

        public void StopPlatform(int counter)
        {
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            platform.platformSpeed = 0;
        }

        public void Create(GameObject platform, int counter)
        {
            var position = new Vector3(PosX, PosY + counter, PosZ);
            var instance = Object.Instantiate(platform, position, Quaternion.identity).GetComponent<MovingPlatform>();
            instance.platformSpeed = 4;
            instance.name = MovingPlatform + counter;
        }
    }
}