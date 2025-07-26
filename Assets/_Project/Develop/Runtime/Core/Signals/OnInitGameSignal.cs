namespace _Project.Develop.Runtime.Core.Signals
{
    public class OnInitGameSignal
    {
        public int PlayerLives;
        public int Score;
        public int FiguresToSpawn;

        public OnInitGameSignal(int playerLives, int score, int figuresToSpawn)
        {
            PlayerLives = playerLives;
            Score = score;
            FiguresToSpawn = figuresToSpawn;
        }
    }
}