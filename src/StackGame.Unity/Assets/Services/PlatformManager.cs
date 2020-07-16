using System.Diagnostics;
using System.Linq;
using Interfaces;
using UnityEngine;
using System.Collections;
using UnityEditor;
using Microsoft.Cci;
using Debug = UnityEngine.Debug;

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

        public void CutPlatform(int platformNumber)
        {
            var currentPlatform = Object.FindObjectsOfType<MovingPlatform>().First();
            var lastPlatform = Object.FindObjectsOfType<MovingPlatform>().ElementAtOrDefault(1);
            var main = GameObject.Find("MainCube");
            if (platformNumber % 2 == 0)
            {
                float hangoverZ = currentPlatform.transform.position.z - lastPlatform.transform.position.z;
                float newZSize = lastPlatform.transform.localScale.z - Mathf.Abs(hangoverZ);
                float newZPos = lastPlatform.transform.position.z + (hangoverZ / 2);
                currentPlatform.transform.localScale = new Vector3(currentPlatform.transform.localScale.x, currentPlatform.transform.localScale.y, newZSize);
                currentPlatform.transform.position = new Vector3(currentPlatform.transform.position.x, currentPlatform.transform.position.y, newZPos);

                float hangoverX = currentPlatform.transform.position.x - lastPlatform.transform.position.x;
                float newXSize = lastPlatform.transform.localScale.x - Mathf.Abs(hangoverX);
                float newXPos = lastPlatform.transform.position.x + (hangoverX / 2);
                currentPlatform.transform.localScale = new Vector3(newXSize, currentPlatform.transform.localScale.y, currentPlatform.transform.localScale.z);
                currentPlatform.transform.position = new Vector3(newXPos, currentPlatform.transform.position.y, currentPlatform.transform.position.z);
            }
            else if (platformNumber == 1)
            {
                float hangoverMain = currentPlatform.transform.position.z - main.transform.position.z;
                float newMainSize = main.transform.localScale.z - Mathf.Abs(hangoverMain);
                float newMainPos = main.transform.position.z + (hangoverMain / 2);
                currentPlatform.transform.localScale = new Vector3(currentPlatform.transform.localScale.x, currentPlatform.transform.localScale.y, newMainSize);
                currentPlatform.transform.position = new Vector3(currentPlatform.transform.position.x, currentPlatform.transform.position.y, newMainPos);
            }
            else if (platformNumber % 2 != 0)
            {
                float hangoverZ1 = currentPlatform.transform.position.z - lastPlatform.transform.position.z;
                float newZSize1 = lastPlatform.transform.localScale.z - Mathf.Abs(hangoverZ1);
                float newZPos1 = lastPlatform.transform.position.z + (hangoverZ1 / 2);
                currentPlatform.transform.localScale = new Vector3(currentPlatform.transform.localScale.x, currentPlatform.transform.localScale.y, newZSize1);
                currentPlatform.transform.position = new Vector3(currentPlatform.transform.position.x, currentPlatform.transform.position.y, newZPos1);

                float hangoverX1 = currentPlatform.transform.position.x - lastPlatform.transform.position.x;
                float newXSize1 = lastPlatform.transform.localScale.x - Mathf.Abs(hangoverX1);
                float newXPos1 = lastPlatform.transform.position.x + (hangoverX1 / 2);
                currentPlatform.transform.localScale = new Vector3(newXSize1, currentPlatform.transform.localScale.y, currentPlatform.transform.localScale.z);
                currentPlatform.transform.position = new Vector3(newXPos1, currentPlatform.transform.position.y, currentPlatform.transform.position.z);
            }
            
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