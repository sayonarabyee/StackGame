using System.Collections.Generic;
using System.Linq;
using Interfaces;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorModeTests
{
    public class PlatformManagerTests : TestClassBase
    {
        #region CreatePlatform

        [Test]
        public void CreatePlatform_CorrectState_CreatedPlatformNotNull()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();

            Assert.NotNull(platform);
        }

        [Test]
        public void CreatePlatform_CorrectState_CreatedOnlyOnePlatform()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().Count();

            Assert.AreEqual(1, platform);
        }

        [Test]
        public void CreatePlatform_FirstPlatform_CorrectSpeed()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();

            Assert.AreEqual(Constants.MovingPlatform.InitialSpeed, platform.initialSpeed);
        }

        [Test]
        public void CreatePlatform_FirstPlatform_CorrectSpeedDirection()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();

            Assert.True(platform.isSpeedAxisZ);
        }

        [Test]
        public void CreatePlatform_FirstPlatform_CorrectPosition()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();
            var position = new Vector3(Constants.MovingPlatform.InitialPosX, Constants.MovingPlatform.InitialPosY,
                Constants.MovingPlatform.InitialPosZ);
            Assert.AreEqual(position, platform.transform.position);
        }

        [Test]
        public void CreatePlatform_FirstPlatform_CorrectScale()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();
            var scale = new Vector3(Constants.MovingPlatform.InitialScaleX, Constants.MovingPlatform.InitialScaleY,
                Constants.MovingPlatform.InitialScaleZ);
            Assert.AreEqual(scale, platform.transform.localScale);
        }

        [Test]
        public void CreatePlatform_SecondPlatform_CorrectSpeed()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            PlatformManager.CreatePlatform(GameManager.prefab);
            
            var platform = GetPlatforms().First();

            Assert.GreaterOrEqual(platform.initialSpeed, Constants.MovingPlatform.InitialSpeed);
        }

        [Test]
        public void CreatePlatform_SecondPlatform_CorrectScale()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();

            Assert.LessOrEqual(platform.transform.localScale.x, Constants.MovingPlatform.InitialScaleX);
            Assert.LessOrEqual(platform.transform.localScale.y, Constants.MovingPlatform.InitialScaleY);
            Assert.LessOrEqual(platform.transform.localScale.z, Constants.MovingPlatform.InitialScaleZ);
        }

        [Test]
        public void CreatePlatform_SecondPlatform_CorrectSpeedDirection()
        {
            PlatformManager.CreatePlatform(GameManager.prefab);
            PlatformManager.CreatePlatform(GameManager.prefab);
            var platform = GetPlatforms().First();

            Assert.False(platform.isSpeedAxisZ);
        }

        #endregion

        private IEnumerable<MovingPlatform> GetPlatforms()
        {
            return Object.FindObjectsOfType<MovingPlatform>();
        }
    }
}