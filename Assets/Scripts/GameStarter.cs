using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Arkanoid
{
    internal sealed class GameStarter : MonoBehaviour
    {
        // this is a game manager now

        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _hp = 100;

        [SerializeField] private float _minAsteroidForce = 100;
        [SerializeField] private float _maxAsteroidForce = 1000;
        [SerializeField] private float _asteroidsCount = 5;
        [SerializeField] private float _spawnRadius = 25;

        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private float _enemySpawnTime;

        [SerializeField] private LayerMask hitLayer;
        [SerializeField] private float hitRadius = 0.7f;

        private Camera _camera;
        private Transform _player;
        private List<Asteroid> _asteroids = new List<Asteroid>();
        private IViewServices _viewServices;
        private Enemy _enemy;
        private bool _enemyExists = false;
        private void Start()
        {
            _camera = Camera.main;
            _player = FindObjectOfType<Player>().transform;
            float width = _camera.pixelWidth;
            float height = _camera.pixelHeight;
            Vector2 topRight = _camera.ScreenToWorldPoint(new Vector2(width, height));

            // ниже две строки создания врагов через создание фабрики, я решил им не пользоваться, но оставил в комментах, потому что есть такое задание
            /*
            IEnemyFactory factory = new AsteroidFactory();
            factory.Create(new Health(_maxHp, _hp));
            */
            _viewServices = new ViewServices();
            //_enemy = _viewServices.Instantiate<Rigidbody2D>(_enemyPrefab.gameObject, _enemySpawnPoint);
            //SpawnEnemy();
        }

        private void Update()
        {
            if (_asteroids.Count < _asteroidsCount)
            {
                CreateAsteroid();
            }
            else
            {
                DestroyAsteroid();
            }

            if (!_enemyExists)
            {
                CreateEnemy();
            }
            /*
            if (Input.GetButtonDown("Fire3"))
            {
                StartCoroutine(DestroyEnemy());
            }
            */
        }

        // мне не нравится, что метод создания астероидов находится в этом классе, но уже нет времени придумывать, как его перенести, оставлю тут
        void CreateAsteroid()
        {
            float force = Random.Range(_minAsteroidForce, _maxAsteroidForce);
            Vector3 position = new Vector2(SpawnPoint(_spawnRadius).x, SpawnPoint(_spawnRadius).y);
            Vector3 direction = _player.position - position;
            var enemy = EnemySpawn.CreateAsteroidWithPosition(new Health(_maxHp, _hp), force, position, direction);
            _asteroids.Add(enemy);
        }

        void DestroyAsteroid()
        {
            for (int i = 0; i < _asteroids.Count; ++i)
            {
                var distance = (_asteroids[i].transform.position - _player.position).magnitude;
                var ast = _asteroids[i];
                if (distance > _spawnRadius)
                {
                    _asteroids.Remove(_asteroids[i]);
                    Destroy(ast.gameObject);
                }

                bool isHit = Physics2D.OverlapCircle(ast.gameObject.transform.position, hitRadius, hitLayer);
                if (isHit)
                {
                    _asteroids.Remove(_asteroids[i]);
                    Destroy(ast.gameObject);
                }
            }
        }

        void CreateEnemy()
        {
            _enemy = EnemySpawn.CreateEnemy();
            _enemyExists = true;
        }

        IEnumerator DestroyEnemy()
        {
            Destroy(_enemy.gameObject);
            yield return new WaitForSeconds(_enemySpawnTime);
            _enemyExists = false;
        }


        Vector2 SpawnPoint(float radius)
        {
            float randAng = Random.Range(0, Mathf.PI * 2);
            return new Vector2(Mathf.Cos(randAng) * radius, Mathf.Sin(randAng) * radius);
        }
    }
}

