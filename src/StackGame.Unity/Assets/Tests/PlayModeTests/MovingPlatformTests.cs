using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayModeTests
{
    public class MovingPlatformTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PositionOnAxisXChangesWithTime()
        {
            var platform = GetMovingPlatform();
            var init = platform.transform.position.x;

            yield return new WaitForSeconds(0.1f);
            
            Assert.Less(init, platform.transform.position.x);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PositionOnAxisZChangesWithTime()
        {
            var platform = GetMovingPlatform();
            platform.isSpeedAxisZ = true;
            var init = platform.transform.position.z;

            yield return new WaitForSeconds(0.1f);
            
            Assert.Less(init, platform.transform.position.z);
            yield return null;
        }

        private MovingPlatform GetMovingPlatform()
        {
            var platform = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/MovingPlatform"))
                .GetComponent<MovingPlatform>();

            return platform;
        }
    }
}
