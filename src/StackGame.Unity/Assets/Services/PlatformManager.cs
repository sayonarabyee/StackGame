using System.Linq;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class PlatformManager : IPlatformManager
    {
        public int PlatformsCount { get; private set; }
        private const float PosX = 0.01000071f;
        private const float PosY = -0.549999f;
        private const float PosZ = -7.929996f;
        private const float Height = 0.27313f;
        private const int DefaultSpeed = 4;
        
        public PlatformManager()
        {
            PlatformsCount = 0;
        }
        
        public void StopPlatform(int? platformNumber = null)
        {
            var allPlatforms = Object.FindObjectsOfType<MovingPlatform>();

            var platform = platformNumber == null
                ? allPlatforms.First()
                : allPlatforms.ElementAtOrDefault(platformNumber.Value);
            
            if(platform==null)
                return;
            
            platform.initialSpeed = 0;
        }

        public void CreatePlatform(GameObject platform)
        {
            var position = GetPlatformInitialPosition(PlatformsCount);
            var instance = Object.Instantiate(platform, position, Quaternion.identity).GetComponent<MovingPlatform>();
            instance.isSpeedAxisZ = IsAxisZPlatform(PlatformsCount);
            instance.initialSpeed = DefaultSpeed;
            PlatformsCount++;
        }

        
        private Vector3 GetPlatformInitialPosition(int platformNumber)
        {
            return IsAxisZPlatform(platformNumber)
                ? new Vector3(PosX, PosY + platformNumber * Height, PosZ)
                : new Vector3(PosZ, PosY + platformNumber * Height,  PosX);
        }

        private bool IsAxisZPlatform(int platformNumber)
        {
            return platformNumber % 2 == 0;
        }
    }
}