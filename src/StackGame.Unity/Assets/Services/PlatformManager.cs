using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class PlatformManager : IPlatformManager
    {
        public int PlatformsCount { get; private set; }

        public PlatformManager()
        {
            PlatformsCount = 0;
        }

        #region Public Methods

        public MovingPlatform CreatePlatform(GameObject platform)
        {
            var previousPlatform = GetCurrentPlatform();
            var initialPosition = GetPlatformInitialPosition(PlatformsCount);
            var currentPlatform = Object.Instantiate(platform, initialPosition, Quaternion.identity)
                .GetComponent<MovingPlatform>();
            currentPlatform.isSpeedAxisZ = IsAxisZPlatform(PlatformsCount);
            currentPlatform.initialSpeed = Constants.MovingPlatform.InitialSpeed;
            var currentPlatformTransform = currentPlatform.transform;
            currentPlatformTransform.localScale = previousPlatform != null
                ? previousPlatform.transform.localScale
                : currentPlatformTransform.localScale;
            PlatformsCount++;
            return currentPlatform;
        }

        //TODO: s: refactor this please
        //TODO: s: write test for this
        public void CutPlatform()
        {
            var currentPlatform = GetCurrentPlatform();
            var lastPlatform = GetPreviousPlatform();
            var main = GameObject.Find("MainCube");
            if (PlatformsCount == 1)
            {
                var hangoverMain = currentPlatform.transform.position.z - main.transform.position.z;
                var newMainSize = main.transform.localScale.z - Mathf.Abs(hangoverMain);
                var newMainPos = main.transform.position.z + (hangoverMain / 2);
                currentPlatform.transform.localScale = new Vector3(currentPlatform.transform.localScale.x,
                    currentPlatform.transform.localScale.y, newMainSize);
                currentPlatform.transform.position = new Vector3(currentPlatform.transform.position.x,
                    currentPlatform.transform.position.y, newMainPos);
            }
            else if (IsAxisZPlatform(PlatformsCount))
            {
                var hangoverZ = currentPlatform.transform.position.z - lastPlatform.transform.position.z;
                var newZSize = lastPlatform.transform.localScale.z - Mathf.Abs(hangoverZ);
                var newZPos = lastPlatform.transform.position.z + (hangoverZ / 2);
                currentPlatform.transform.localScale = new Vector3(currentPlatform.transform.localScale.x,
                    currentPlatform.transform.localScale.y, newZSize);
                currentPlatform.transform.position = new Vector3(currentPlatform.transform.position.x,
                    currentPlatform.transform.position.y, newZPos);

                var hangoverX = currentPlatform.transform.position.x - lastPlatform.transform.position.x;
                var newXSize = lastPlatform.transform.localScale.x - Mathf.Abs(hangoverX);
                var newXPos = lastPlatform.transform.position.x + (hangoverX / 2);
                currentPlatform.transform.localScale = new Vector3(newXSize, currentPlatform.transform.localScale.y,
                    currentPlatform.transform.localScale.z);
                currentPlatform.transform.position = new Vector3(newXPos, currentPlatform.transform.position.y,
                    currentPlatform.transform.position.z);
            }
            else if (!IsAxisZPlatform(PlatformsCount))
            {
                var hangoverZ1 = currentPlatform.transform.position.z - lastPlatform.transform.position.z;
                var newZSize1 = lastPlatform.transform.localScale.z - Mathf.Abs(hangoverZ1);
                var newZPos1 = lastPlatform.transform.position.z + (hangoverZ1 / 2);
                currentPlatform.transform.localScale = new Vector3(currentPlatform.transform.localScale.x,
                    currentPlatform.transform.localScale.y, newZSize1);
                currentPlatform.transform.position = new Vector3(currentPlatform.transform.position.x,
                    currentPlatform.transform.position.y, newZPos1);

                var hangoverX1 = currentPlatform.transform.position.x - lastPlatform.transform.position.x;
                var newXSize1 = lastPlatform.transform.localScale.x - Mathf.Abs(hangoverX1);
                var newXPos1 = lastPlatform.transform.position.x + (hangoverX1 / 2);
                currentPlatform.transform.localScale = new Vector3(newXSize1, currentPlatform.transform.localScale.y,
                    currentPlatform.transform.localScale.z);
                currentPlatform.transform.position = new Vector3(newXPos1, currentPlatform.transform.position.y,
                    currentPlatform.transform.position.z);
            }
        }

        public bool PlatformMissed()
        {
            var platforms = GetPlatforms();
            var current = GetCurrentPlatform().transform;
            var currentPosition = current.position;
            var currentScale = current.localScale;
            bool isAxisXMissed;
            bool isAxisZMissed;

            if (platforms?.Count() == 1)
            {
                isAxisZMissed = IsAxisMissed(currentPosition.z, Constants.Cube.InitialPosZ,
                    currentScale.z, Constants.Cube.InitialScaleZ);
                isAxisXMissed = IsAxisMissed(currentPosition.x, Constants.Cube.InitialPosX,
                    currentScale.x, Constants.Cube.InitialScaleX);
            }
            else
            {
                var platform = GetPreviousPlatform()?.transform;

                if (platform == null)
                    return false;

                var previousPosition = platform.position;
                var previousScale = platform.localScale;
                isAxisZMissed = IsAxisMissed(currentPosition.z, previousPosition.z,
                    currentScale.z, previousScale.z);
                isAxisXMissed = IsAxisMissed(currentPosition.x, previousPosition.x,
                    currentScale.x, previousScale.x);
            }

            return isAxisXMissed || isAxisZMissed;
        }

        public void StopPlatform(int? platformNumber = null)
        {
            var platform = platformNumber == null
                ? GetCurrentPlatform()
                : GetPlatforms().ElementAtOrDefault(platformNumber.Value);

            if (platform == null)
                return;

            platform.initialSpeed = 0;
        }

        #endregion

        #region Private Methods

        private static MovingPlatform GetCurrentPlatform()
        {
            return GetPlatforms().FirstOrDefault();
        }

        private static MovingPlatform GetPreviousPlatform()
        {
            return GetPlatforms().ElementAtOrDefault(1);
        }

        private static Vector3 GetPlatformInitialPosition(int platformNumber)
        {
            var previousPlatform = Object.FindObjectsOfType<MovingPlatform>()?.FirstOrDefault();
            if (previousPlatform == null)
                return IsAxisZPlatform(platformNumber)
                    ? new Vector3(Constants.MovingPlatform.InitialPosX,
                        Constants.MovingPlatform.InitialPosY + platformNumber * Constants.MovingPlatform.InitialScaleY,
                        Constants.MovingPlatform.InitialPosZ)
                    : new Vector3(Constants.MovingPlatform.InitialPosZ,
                        Constants.MovingPlatform.InitialPosY + platformNumber * Constants.MovingPlatform.InitialScaleY,
                        Constants.MovingPlatform.InitialPosX);

            var previousPlatformPosition = previousPlatform.transform.position;
            return IsAxisZPlatform(platformNumber)
                ? new Vector3(previousPlatformPosition.x,
                    Constants.MovingPlatform.InitialPosY + platformNumber * Constants.MovingPlatform.InitialScaleY,
                    Constants.MovingPlatform.InitialPosZ)
                : new Vector3(Constants.MovingPlatform.InitialPosZ,
                    Constants.MovingPlatform.InitialPosY + platformNumber * Constants.MovingPlatform.InitialScaleY,
                    previousPlatformPosition.z);
        }

        private static bool IsAxisZPlatform(int platformNumber)
        {
            return platformNumber % 2 == 0;
        }

        private static bool IsAxisMissed(float currentPosition, float previousPosition, float currentScale,
            float previousScale)
        {
            var currentMin = currentPosition - currentScale / 2;
            var currentMax = currentPosition + currentScale / 2;
            var previousMin = previousPosition - previousScale / 2;
            var previousMax = previousPosition + previousScale / 2;

            return previousMin > currentMax || previousMax < currentMin;
        }

        private static IEnumerable<MovingPlatform> GetPlatforms()
        {
            return Object.FindObjectsOfType<MovingPlatform>();
        }

        #endregion
    }
}