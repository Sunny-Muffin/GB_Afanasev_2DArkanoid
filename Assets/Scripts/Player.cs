using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static System.Net.WebRequestMethods;

namespace Arkanoid
{
    internal sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _hp = 100;
        [SerializeField] private Rigidbody2D _bullet;
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;
        private Camera _camera;
        private Ship _ship;
        private Gun _gun;
        private Health _health;
        private IViewServices _viewServices;
        private void Start()
        {
            _camera = Camera.main;
            var moveTransform = new AccelerationMove(transform, _speed, _acceleration);
            var rotation = new RotateTransform(transform);
            _ship = new Ship(moveTransform, rotation);
            _viewServices = new ViewServices();
            _gun = new Gun(_bullet, _viewServices, _force, _barrel);
            _health = new Health(_maxHp, _hp);
        }
        
        // хорошо бы вообще всё это переписать, класс плеер сделать отдельно, а тут создать какой-нить гейм контроллер, чтобы он всё обрабатывал
        // ну или смириться с тем, что будет больше одного апдейта и отдельно обрабатывать вражеские корабли, метеориты и пули (скорее всего так и сделаю)

        private void Update()
        {
            var direction = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
            _ship.Rotate(direction);
            _ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _ship.AddAcceleration();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _ship.RemoveAcceleration();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(_gun.Shoot());
            }

            // just for tests
            if (Input.GetButtonDown("Jump"))
            {
                _health.TakeDamage(10);
                Debug.Log($"HIT! Health left: {_health.HealthPoints}");
                if (_health.HealthPoints <= 0)
                {
                    Destroy(gameObject);
                }
            }
            //_barrel.position = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
            
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Asteroid>(out var asteroid))
            {
                _health.TakeDamage(asteroid._damage);
                if (_health.HealthPoints <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

