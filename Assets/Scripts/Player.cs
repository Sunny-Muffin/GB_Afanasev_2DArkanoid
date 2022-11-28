using ObjectPool;
using UnityEngine;


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

        [SerializeField] private Sprite _bulletSprite;
        [SerializeField] private float _bulletMass;
        [SerializeField] private int bulletLayer = 3;
        [SerializeField] private float bulletLifeTime = 2f;

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
                // да, для всего этого можно было создать новый метод в классе Gun
                // я сначала так и сделал, но не придумал, как вызывать Destroy
                // так что пусть будет тут
                var altBullet = new GameObject().
                    SetName("AltBullet").
                    SetLayer(bulletLayer).
                    SetTransform(_barrel).
                    AddBoxCollider2D().
                    AddRigidbody2D(_bulletMass).
                    AddSprite(_bulletSprite).
                    AddForce(_force, _barrel);
                Destroy(altBullet, bulletLifeTime);
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

