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
        private const float PosX = 0.01000071f;
        private const float PosY = -0.549999f;
        private const float PosZ = -7.929996f;
        private const float Height = 0.27313f;

        public void StopPlatform(int counter)
        {
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            platform.platformSpeed = 0;
        }

        public void CreatePlatform(GameObject platform, int platformNumber)
        {
            var position = GetPlatformInitialPosition(platformNumber);
            var instance = Object.Instantiate(platform, position, Quaternion.identity).GetComponent<MovingPlatform>();
            instance.platformSpeed = GetPlatformInitialSpeed(platformNumber);
        }

        private int GetPlatformInitialSpeed(int platformNumber)
        {
            if(platformNumber % 2 == 0)
                return 4;
            
            return -4;
        }
        private Vector3 GetPlatformInitialPosition(int platformNumber)
        {
            if(platformNumber % 2 == 0)
                return new Vector3(PosX, PosY + platformNumber * Height, PosZ);
            
            return new Vector3(PosX, PosY + platformNumber * Height, -1 * PosZ);

        }
    }
}