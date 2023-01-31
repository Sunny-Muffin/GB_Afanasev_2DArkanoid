using ObjectPool;
using System;
using UnityEngine;


namespace Arkanoid
{
    internal sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _maxHp = 100;
        [SerializeField] private float _hp = 100;

        [Header("Standart Gun")]
        [SerializeField] private Rigidbody2D _bullet;
        [SerializeField] private Transform _barrelPosition;
        [SerializeField] private float _force;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        [Header("New barrel")]
        [SerializeField] private AudioClip _newBarrelAudioClip;
        [SerializeField] private GameObject _newBarrel;

        [Header("Alternative Gun")]
        [SerializeField] private Sprite _bulletSprite;
        [SerializeField] private float _bulletMass;
        [SerializeField] private int _bulletLayer = 3;
        [SerializeField] private float _bulletLifeTime = 2f;

        private Camera _camera;
        private Ship _ship;
        private Gun _gun;
        private Gun _altGun;
        private Health _health;
        private IViewServices _viewServices;
        private ModificationWeapon _modificationWeapon;
        private bool isModified = false;

        private void Start()
        {
            _camera = Camera.main;
            var moveTransform = new AccelerationMove(transform, _speed, _acceleration);
            var rotation = new RotateTransform(transform);
            _ship = new Ship(moveTransform, rotation);
            _viewServices = new ViewServices();
            _gun = new Gun(_bullet, _viewServices, _force, _barrelPosition, _audioSource, _audioClip);
            _altGun = new Gun(_bulletLayer, _barrelPosition, _bulletMass, _bulletSprite, _force);
            _health = new Health(_maxHp, _hp);
        }
        
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

            if (Input.GetButtonDown("Fire2"))
            {
                var altBullet = _altGun.AltShoot();
                Destroy(altBullet, _bulletLifeTime);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && !isModified)
            {
                var newBarrel = new Barrel(_newBarrelAudioClip, _barrelPosition, _newBarrel);
                _modificationWeapon = new ModificationBarrel(_barrelPosition, newBarrel, _audioSource);
                _modificationWeapon.ApplyModification(_gun);
                isModified = true;
            }



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

