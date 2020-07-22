using NUnit.Framework;

namespace Tests.EditorModeTests
{
    public class CameraManagerTests : TestClassBase
    {
        #region MoveUp

        [Test]
        public void MoveUp_NotNullDistance_CameraMovedAxisY()
        {
            var initialPosition = GameManager.gameCamera.transform.position.y;
            var distance = 10;
            
            CameraManager.MoveUp(distance);
            
            Assert.AreEqual(initialPosition+distance, GameManager.gameCamera.transform.position.y);
        }
        
        [Test]
        public void MoveUp_NotNullDistance_CameraNotMovedAxisZAndAxisX()
        {
            var initialPositionX = GameManager.gameCamera.transform.position.x;
            var initialPositionZ = GameManager.gameCamera.transform.position.z;
            var distance = 10;
            
            CameraManager.MoveUp(distance);
            
            Assert.AreEqual(initialPositionX, GameManager.gameCamera.transform.position.x);
            Assert.AreEqual(initialPositionZ, GameManager.gameCamera.transform.position.z);
        }

        #endregion
    }
}