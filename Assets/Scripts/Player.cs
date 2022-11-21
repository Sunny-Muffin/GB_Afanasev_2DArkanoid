using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid
{
    internal sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _hp = 100;
        [SerializeField] private Rigidbody2D _bullet;
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;
        private Camera _camera;
        private Ship _ship;
        private Gun _gun;
        private Health _health;
        private void Start()
        {
            _camera = Camera.main;
            var moveTransform = new AccelerationMove(transform, _speed, _acceleration);
            var rotation = new RotateTransform(transform);
            _ship = new Ship(moveTransform, rotation);
            _gun = new Gun(_force, _barrel);
            _health = new Health(_hp);
        }
        
        // ������ �� ������ �� ��� ����������, ����� ����� ������� ��������, � ��� ������� �����-���� ���� ����������, ����� �� �� �����������
        // �� ��� ��������� � ���, ��� ����� ������ ������ ������� � �������� ������������ ��������� �������, ��������� � ���� (������ ����� ��� � ������)

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
                var temAmmunition = Instantiate(_bullet, _barrel.position, _barrel.rotation);
                _gun.Shoot(temAmmunition);
                Destroy(temAmmunition.gameObject, 1f);
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
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            // ����� ������� ����� ������� (���������, ����), � ������� ����� �������� ����� � ��� ������������ ���������� ���� ���� ����
            // ������ ���������� �����, ��, ����, �����
            _health.TakeDamage(10);

            if (_health.HealthPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

