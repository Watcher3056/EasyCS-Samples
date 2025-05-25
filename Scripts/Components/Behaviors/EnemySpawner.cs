using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public class EnemySpawner : EasyCSBehavior, IUpdate
    {
        [SerializeField, Required]
        private PrefabRootData _enemyPrefab;
        [SerializeField]
        private float _spawnInterval = 2f;
        [SerializeField]
        private float _areaSize = 10f;

        private float _spawnTimer = 0f;

        public void OnUpdate(float deltaTime)
        {
            _spawnTimer -= deltaTime;
            if (_spawnTimer <= 0f)
            {
                SpawnEnemy();
                _spawnTimer = _spawnInterval;
            }
        }

        private void SpawnEnemy()
        {
            Vector3 spawnPos = GetRandomBorderPosition();

            EasyCsContainer.InstantiateWithEntity(_enemyPrefab, spawnPos, Quaternion.identity);
        }

        private Vector3 GetRandomBorderPosition()
        {
            float halfSize = _areaSize * 0.5f;
            float x = 0f, z = 0f;

            int edge = Random.Range(0, 4);
            switch (edge)
            {
                case 0: // Top
                    x = Random.Range(-halfSize, halfSize);
                    z = halfSize;
                    break;
                case 1: // Bottom
                    x = Random.Range(-halfSize, halfSize);
                    z = -halfSize;
                    break;
                case 2: // Left
                    x = -halfSize;
                    z = Random.Range(-halfSize, halfSize);
                    break;
                case 3: // Right
                    x = halfSize;
                    z = Random.Range(-halfSize, halfSize);
                    break;
            }

            return transform.position + new Vector3(x, 0f, z);
        }
    }

}
