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
        private float _coolDown; // realize it later, maybe via coroutines

        public float Force { get; protected set; }

        public Gun (Rigidbody2D bulletPrefab, IViewServices viewServices, float force, Transform barrel)
        {
            _bulletPrefab = bulletPrefab;
            _viewServices = viewServices;
            Force = force;
            _barrel = barrel;
        }

        /*
        public void Shoot()
        {
            var bullet = _viewServices.Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrel);
            bullet.AddForce(_barrel.up * Force);
            _viewServices.Destroy(bullet.gameObject);
        }
        */

        public IEnumerator Shoot ()
        {
            var bullet = _viewServices.Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrel);
            bullet.AddForce(_barrel.up * Force);
            yield return new WaitForSeconds(2f);
            _viewServices.Destroy(bullet.gameObject);
        }
    }
}

