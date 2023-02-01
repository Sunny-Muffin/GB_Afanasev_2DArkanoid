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
        private Transform _playerTransform;
        private List<Asteroid> _asteroids = new List<Asteroid>();
        private IViewServices _viewServices;
        private Enemy _enemy;
        private bool _enemyExists = false;

        private Player _player;

        private void Start()
        {
            _camera = Camera.main;
            _player = FindObjectOfType<Player>();
            _playerTransform = _player.transform;
            float width = _camera.pixelWidth;
            float height = _camera.pixelHeight;
            Vector2 topRight = _camera.ScreenToWorldPoint(new Vector2(width, height));

            // ���� ��� ������ �������� ������ ����� �������� �������, � ����� �� �� ������������, �� ������� � ���������, ������ ��� ���� ����� �������
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


            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Applying modifications");
                var root = new PlayerModifier(_player);
                root.Add(new PlayerSpeedModifier(_player, 10));
                root.Add(new PlayerGunModifier(_player, "GeekBrains is shit"));
                root.Add(new PlayerHealthModifier(_player, 20));
                root.Handle();
                Debug.Log("Finished modifications");
            }
        }

        // ��� �� ��������, ��� ����� �������� ���������� ��������� � ���� ������, �� ��� ��� ������� �����������, ��� ��� ���������, ������� ���
        void CreateAsteroid()
        {
            float force = Random.Range(_minAsteroidForce, _maxAsteroidForce);
            Vector3 position = new Vector2(SpawnPoint(_spawnRadius).x, SpawnPoint(_spawnRadius).y);
            Vector3 direction = _playerTransform.position - position;
            var enemy = EnemySpawn.CreateAsteroidWithPosition(new Health(_maxHp, _hp), force, position, direction);
            _asteroids.Add(enemy);
        }

        void DestroyAsteroid()
        {
            for (int i = 0; i < _asteroids.Count; ++i)
            {
                var distance = (_asteroids[i].transform.position - _playerTransform.position).magnitude;
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

