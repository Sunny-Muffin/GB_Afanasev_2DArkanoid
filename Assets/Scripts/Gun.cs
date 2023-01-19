using UnityEngine;
using ObjectPool;
using System.Collections;

namespace Arkanoid
{
    public class Gun : IShootable
    {
        private Rigidbody2D _bulletPrefab; 
        private IViewServices _viewServices;
        private Transform _barrel;
        private float _bulletLifeTime = 2f;

        private int _bulletLayer;
        private float _bulletMass;
        private Sprite _bulletSprite;

        public float Force { get; protected set; }
        public float Force2 { get; protected set; }

        public Gun (Rigidbody2D bulletPrefab, IViewServices viewServices, float force, Transform barrel)
        {
            _bulletPrefab = bulletPrefab;
            _viewServices = viewServices;
            Force = force;
            _barrel = barrel;
        }
        public Gun(int bulletLayer, Transform barrel, float bulletMass,Sprite bulletSprite, float force)
        {
            _bulletLayer = bulletLayer;
            _barrel = barrel;
            _bulletMass = bulletMass;
            _bulletSprite = bulletSprite;
            Force = force;
        }

        public IEnumerator Shoot ()
        {
            var bullet = _viewServices.Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrel);
            bullet.AddForce(_barrel.up * Force);
            yield return new WaitForSeconds(_bulletLifeTime);
            _viewServices.Destroy(bullet.gameObject);
        }

        public GameObject AltShoot ()
        {
            var altBullet = new GameObject().
                SetName("AltBullet").
                SetLayer(_bulletLayer).
                SetTransform(_barrel).
                AddBoxCollider2D().
                AddRigidbody2D(_bulletMass).
                AddSprite(_bulletSprite).
                AddForce(Force, _barrel);
            return altBullet;
        }
        

        // вызов пула через сервис локатор (для домашки)
        /*
        public IEnumerator Shoot()
        {
            ServiceLocator.SetService<IViewServices>(new ViewServices());
            var bullet = ServiceLocator.Resolve<IViewServices>().Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrel);
            bullet.AddForce(_barrel.up * Force);
            yield return new WaitForSeconds(_bulletLifeTime);
            ServiceLocator.Resolve<IViewServices>().Destroy(bullet.gameObject);
        }
        */
    }
}

