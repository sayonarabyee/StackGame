using Interfaces;
using UnityEngine;

namespace Services
{
    public class CameraManager : ICameraManager
    {
        private readonly Camera _camera;

        public CameraManager(Camera camera)
        {
            _camera = camera;
        }

        public void MoveUp(float distance)
        {
            var cameraPosition = _camera.gameObject.transform.position;
            cameraPosition.y += distance;
            _camera.gameObject.transform.position = cameraPosition;
        }
    }
}