using _Project.Develop.Runtime.Data.Configs;
using UnityEngine;

namespace _Project.Develop.Runtime.Domain.Models
{
    public class SpawnZoneModel
    {
        private readonly Vector2 _spawnDelayRange;
        private readonly Vector2 _speedRange;

        private int _figuresToSpawn;

        public SpawnZoneModel(GameConfig gameConfig)
        {
            _spawnDelayRange = gameConfig.SpawnDelayRange;
            _speedRange = gameConfig.SpeedRange;
        }

        public Vector2 GetSpawnDelayRange()
        {
            return _spawnDelayRange;
        }

        public Vector2 GetSpeedRange()
        {
            return _speedRange;
        }

        public void SetFiguresToSpawn(int figuresToSpawn)
        {
            _figuresToSpawn = figuresToSpawn;
        }

        public int GetFiguresToSpawn()
        {
            return _figuresToSpawn;
        }
    }
}