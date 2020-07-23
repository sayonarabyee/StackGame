namespace Interfaces
{
    public interface IScoreManager
    {
        int Score { get; }
        void UpdateScore(MovingPlatform current, MovingPlatform previous);
    }
}