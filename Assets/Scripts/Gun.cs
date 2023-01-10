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

        public float Force { get; protected set; }
        public float Force2 { get; protected set; }

        public Gun (Rigidbody2D bulletPrefab, IViewServices viewServices, float force, Transform barrel)
        {
            _bulletPrefab = bulletPrefab;
            _viewServices = viewServices;
            Force = force;
            _barrel = barrel;
        }
        /*
        public IEnumerator Shoot ()
        {
            var bullet = _viewServices.Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrel);
            bullet.AddForce(_barrel.up * Force);
            yield return new WaitForSeconds(_bulletLifeTime);
            _viewServices.Destroy(bullet.gameObject);
        }
        */

        // вызов пула через сервис локатор (для домашки)
        public IEnumerator Shoot()
        {
            ServiceLocator.SetService<IViewServices>(new ViewServices());
            var bullet = ServiceLocator.Resolve<IViewServices>().Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrel);
            bullet.AddForce(_barrel.up * Force);
            yield return new WaitForSeconds(_bulletLifeTime);
            ServiceLocator.Resolve<IViewServices>().Destroy(bullet.gameObject);
        }
    }
}

