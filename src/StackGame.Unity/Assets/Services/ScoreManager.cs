using Interfaces;

namespace Services
{
    public class ScoreManager : IScoreManager
    {
        public int Score { get; private set; }

        public void UpdateScore(MovingPlatform current, MovingPlatform previous)
        {
            var currentScale = current.transform.localScale;
            var currentSurface = currentScale.x * currentScale.z;

            float previousSurface;
            if (previous == null)
                previousSurface = Constants.Cube.InitialScaleX * Constants.Cube.InitialScaleZ;
            else
            {
                var previousScale = previous.transform.localScale;
                previousSurface = previousScale.x * previousScale.z;
            }

            Score += (int) (currentSurface / previousSurface * 100);
        }
    }
}