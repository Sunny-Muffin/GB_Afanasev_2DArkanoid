using ObjectPool;
using System.Collections;
using UnityEngine;

namespace Arkanoid
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _hp = 100;

        [SerializeField] private float topPoint = 3;
        [SerializeField] private float bottomPoint = -3;

        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;
        [SerializeField] private float _gunCoolDown;

        [SerializeField] private Sprite _bulletSprite;
        [SerializeField] private float _bulletMass;
        [SerializeField] private int _bulletLayer = 3;
        [SerializeField] private float _bulletLifeTime = 2f;

        [SerializeField] private Vector2 _shootingPoint;


        private Ship _ship;
        private Gun _gun;
        private Health _health;
        private Player _player;

        private bool _moveUp = true;
        private bool _isShooting = false;
        private bool _onPosition = false;

        private void Start()
        {
            var moveTransform = new AccelerationMove(transform, _speed, _acceleration);
            var rotation = new RotateTransform(transform);
            _ship = new Ship(moveTransform, rotation);
            _gun = new Gun(_bulletLayer, _barrel, _bulletMass, _bulletSprite, _force);
            _health = new Health(_maxHp, _hp);
            _player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            var _playerPosition = _player.transform.position;
            _ship.Rotate(_playerPosition - transform.position);

            /*
             * 
             * у врага отнимается здоровье при попадании пули
             * у игрока отнимается здоровье при попадании вражеской пули
             * 
             done * враг спавнится за пределами экрана, поссле того как предыдущий враг был уничтожен
             done * переделать стрельбу, потому что эта работает неправильно =(
             done * заелтает в координаты 5;0
             done * начинает двигаться вверх-вниз
             done * стреляет раз в какое то время
            */

            if (!_onPosition && transform.position.x < _shootingPoint.x)
            {
                _onPosition = true;
            }

            if (!_onPosition)
            {
                _ship.Move(-_speed, 0, Time.deltaTime);
            }
            else
            {
                if (_moveUp)
                {
                    _ship.Move(0, _speed, Time.deltaTime);
                    if (transform.position.y >= topPoint)
                    {
                        _moveUp = false;
                    }
                }
                else
                {
                    _ship.Move(0, -_speed, Time.deltaTime);
                    if (transform.position.y <= bottomPoint)
                    {
                        _moveUp = true;
                    }
                }
            }


            if (!_isShooting)
            {
                StartCoroutine(Fire());
            }
            
        }

        private IEnumerator Fire()
        {
            _isShooting = true;
            var bullet = _gun.AltShoot();
            Destroy(bullet, _bulletLifeTime);
            yield return new WaitForSeconds(_gunCoolDown);
            _isShooting = false;
        }
    }
}

