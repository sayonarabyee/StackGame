using System.Linq;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class PlatformManager : IPlatformManager
    {
        private const float PosX = 0.01000071f;
        private const float PosY = -0.549999f;
        private const float PosZ = -7.929996f;
        private const float Height = 0.27313f;
        private const int DefaultSpeed = 4;

        public void StopPlatform()
        {
            var platform = Object.FindObjectsOfType<MovingPlatform>().First();
            platform.initialSpeed = 0;
        }

        public void CreatePlatform(GameObject platform, int platformNumber)
        {
            var position = GetPlatformInitialPosition(platformNumber);
            var instance = Object.Instantiate(platform, position, Quaternion.identity).GetComponent<MovingPlatform>();
            instance.isSpeedAxisZ = platformNumber % 2 == 0;
            instance.initialSpeed = 4;
        }

        
        private Vector3 GetPlatformInitialPosition(int platformNumber)
        {
            if(platformNumber % 2 == 0)
                return new Vector3(PosX, PosY + platformNumber * Height, PosZ);
            
            return new Vector3(PosZ, PosY + platformNumber * Height,  PosX);

        }
    }
}