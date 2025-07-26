using UnityEngine;

namespace _Project.Develop.Runtime.Data.Configs
{
    [CreateAssetMenu(menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _figuresCountRange;
        [SerializeField] private Vector2 _spawnDelayRange;
        [SerializeField] private Vector2 _speedRange;
        [SerializeField] private int _startPlayerLives;
        [SerializeField] private Figure[] _figures;

        public Vector2Int FiguresCountRange => _figuresCountRange;
        public Vector2 SpawnDelayRange => _spawnDelayRange;
        public Vector2 SpeedRange => _speedRange;
        public int StartPlayerLives => _startPlayerLives;
        public Figure[] Figures => _figures;
    }
}