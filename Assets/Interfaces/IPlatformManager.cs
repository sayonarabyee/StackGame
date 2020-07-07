using UnityEngine;

namespace Interfaces
{
    public interface IPlatformManager
    {
        void StopPlatform(int counter);
        void Create(GameObject platform, int counter);
    }
}