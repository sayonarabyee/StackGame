using UnityEngine;

namespace Interfaces
{
    public interface IPlatformManager
    {
        void StopPlatform(int counter);
        void CreatePlatform(GameObject platform, int platformNumber);
    }
}