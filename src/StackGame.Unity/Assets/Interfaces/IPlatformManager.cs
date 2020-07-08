using UnityEngine;

namespace Interfaces
{
    public interface IPlatformManager
    {
        void StopPlatform();
        void CreatePlatform(GameObject platform, int platformNumber);
    }
}