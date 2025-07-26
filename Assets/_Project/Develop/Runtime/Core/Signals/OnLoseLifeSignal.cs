namespace _Project.Develop.Runtime.Core.Signals
{
    public class OnLoseLifeSignal
    {
        public int PlayerLives;

        public OnLoseLifeSignal(int playerLives) => PlayerLives = playerLives;
    }
}