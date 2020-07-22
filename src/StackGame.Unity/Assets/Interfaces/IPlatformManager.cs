using UnityEngine;

namespace Interfaces
{
    public interface IPlatformManager
    {
        int PlatformsCount { get; }
        MovingPlatform CreatePlatform(GameObject platform);
        void CutPlatform();
        bool PlatformMissed();
        void StopPlatform(int? platformNumber = null);
    }
}