using System.Collections.Generic;
using UnityEngine;

namespace Runner.SpawnObstacles
{
    public class SpawnObstacles : MonoBehaviour
    {
        #region SerializeField
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private Transform _parent;
        #endregion

        #region Enemies
        [SerializeField]
        private List<GameObject> _obstaclePrefabs = new List<GameObject>();
        [SerializeField]
        private GameObject _enemyPrefab;

        private List<GameObject> _obstacles = new List<GameObject>();
        private List<GameObject> _enemies = new List<GameObject>();
        #endregion

        public float maxX;
        public float minX;
        public float maxY;
        public float minY;
        public float timeBetweenSpawn;
        private float _nextSpawnTime;

        #region UnityMethods
        private void Awake()
        {
            InitializeObjectPooling();
        }

        private void Update()
        {
            if (Time.time > _nextSpawnTime && _player != null)
            {
                Spawn();
                _nextSpawnTime = Time.time + timeBetweenSpawn;
            }
        }
        #endregion

        #region PrivateMethods
        private void Spawn()
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            var rndmNum = Random.Range(0, 3);
            if (rndmNum <= 1)
            {
                SpawnFromPool(_obstacles, new Vector3(randomX, randomY, 0));
            }
            else
            {
                SpawnFromPool(_enemies, new Vector3(randomX, 0, 0));
            }
        }

        private void SpawnFromPool(List<GameObject> pool, Vector3 positionOffset)
        {
            foreach (var item in pool)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = _player.position + positionOffset;
                    item.SetActive(true);
                    return;
                }
            }
        }

        private void InitializeObjectPooling()
        {
            for (int i = 0; i < 15; i++)
            {
                var index = i <= 5 ? i : Mathf.Abs(i - 9);
                var obj = Instantiate(_obstaclePrefabs[index], _parent);
                _obstacles.Add(obj);
                var enemy = Instantiate(_enemyPrefab, _parent);
                _enemies.Add(enemy);
            }
        }
        #endregion
    }
}