using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorModeTests
{
    public class ScoreManagerTests : TestClassBase
    {
        #region UpdateScore

        [Test]
        public void UpdateScore_FullStack_HundredScore()
        {
            var firstPlatform = GetPlatform(new Vector3(1, 0, 1));
            var secondPlatform = GetPlatform(new Vector3(1, Constants.MovingPlatform.InitialScaleY, 1));

            ScoreManager.UpdateScore(secondPlatform, firstPlatform);

            Assert.AreEqual(100, ScoreManager.Score);
        }

        [Test]
        public void UpdateScore_HalfStack_FiftyScore()
        {
            var firstPlatform = GetPlatform(new Vector3(Constants.MovingPlatform.InitialScaleX, 0, 1));
            var secondPlatform = GetPlatform(new Vector3(Constants.MovingPlatform.InitialScaleX / 2,
                Constants.MovingPlatform.InitialScaleY, 1));

            ScoreManager.UpdateScore(secondPlatform, firstPlatform);

            Assert.AreEqual(50, ScoreManager.Score);
        }

        #endregion

        private MovingPlatform GetPlatform(Vector3 scale)
        {
            var platform =
                Object.Instantiate(GameManager.prefab, new Vector3(0, 0, 0), Quaternion.identity)
                    .GetComponent<MovingPlatform>();
            platform.transform.localScale = scale;
            return platform;
        }
    }
}