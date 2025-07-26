using _Project.Develop.Runtime.Data.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class GameModel
    {
        private readonly int _figuresToSpawn;

        private int _score;
        private int _playerLives;
        private int _figuresLeft;

        public GameModel(GameConfig gameConfig)
        {
            _playerLives = gameConfig.StartPlayerLives;
            _figuresToSpawn = Random.Range(gameConfig.FiguresCountRange.x, gameConfig.FiguresCountRange.y + 1);
            _figuresLeft = _figuresToSpawn;

        }

        public int GetFiguresToSpawn()
        {
            return _figuresToSpawn;
        }

        public void AddScore(int amount)
        {
            _score += amount;
        }

        public int GetScore()
        {
            return _score;
        }

        public void LoseLife(int amount)
        {
            _playerLives -= amount;
        }

        public int GetPlayerLives()
        {
            return _playerLives >= 0 ? _playerLives : 0;
        }

        public bool IsPlayerDead()
        {
            return _playerLives <= 0;
        }

        public void ProcessFigure()
        {
            _figuresLeft--;
        }

        public bool IsFiguresLeft()
        {
            return _figuresLeft > 0;
        }
    }
}