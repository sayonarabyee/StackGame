using UnityEngine;

namespace Interfaces
{
    public interface IPlatformManager
    {
        int PlatformsCount { get; }
        void StopPlatform(int? platformNumber = null);
        void CreatePlatform(GameObject platform);

        bool PlatformMissed();
        void CutPlatform();
    }
}