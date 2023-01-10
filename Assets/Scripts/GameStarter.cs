using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    internal sealed class GameStarter : MonoBehaviour
    {
        // тут уже не геймстаретер, а геймконтроллер получается
        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _hp = 100;

        [SerializeField] private float _minAsteroidForce = 100;
        [SerializeField] private float _maxAsteroidForce = 1000;

        [SerializeField] private float _asteroidsCount = 5;
        [SerializeField] private float _spawnRadius = 25;

        [SerializeField] private LayerMask hitLayer;
        [SerializeField] private float hitRadius = 0.7f;

        private Camera _camera;
        private Transform _player;
        private List<Asteroid> _asteroids = new List<Asteroid>();
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

        }

        private void Update()
        {
            if (_asteroids.Count < _asteroidsCount)
            {
                CreateAsteroid();
            }
            else
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
                // код ниже работает, но с ошибкой, не знаю почему
                /*
                foreach (var asteroid in _asteroids)
                {
                    var distance = (asteroid.transform.position - _player.position).magnitude;
                    if (distance > _spawnRadius)
                    {
                        _asteroids.Remove(asteroid);
                        Destroy(asteroid.gameObject);
                    }
                }
                */
            }
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

        Vector2 SpawnPoint(float radius)
        {
            float randAng = Random.Range(0, Mathf.PI * 2);
            return new Vector2(Mathf.Cos(randAng) * radius, Mathf.Sin(randAng) * radius);
        }
    }
}

