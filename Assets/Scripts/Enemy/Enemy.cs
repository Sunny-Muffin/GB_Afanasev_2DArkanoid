using ObjectPool;
using UnityEngine;

namespace Arkanoid
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _hp = 100;

        [SerializeField] private Rigidbody2D _bullet;
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;

        private Ship _ship;
        private Gun _gun;
        private Health _health;
        private IViewServices _viewServices;
        private Player _player;

        private void Start()
        {
            var moveTransform = new AccelerationMove(transform, _speed, _acceleration);
            var rotation = new RotateTransform(transform);
            _ship = new Ship(moveTransform, rotation);
            _viewServices = new ViewServices();
            _gun = new Gun(_bullet, _viewServices, _force, _barrel);
            _health = new Health(_maxHp, _hp);
            _player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            var _playerPosition = _player.transform.position;
            _ship.Rotate(_playerPosition);
            
            // make enemy ship move ramndomly in screen bounds

            // make ship shoot randomly

            //_ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Time.deltaTime);

            /*
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _ship.AddAcceleration();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _ship.RemoveAcceleration();
            }
            */
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(_gun.Shoot());
            }
            
        }
    }
}

