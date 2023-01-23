using UnityEngine;
using ObjectPool;
using System.Collections;

namespace Arkanoid
{
    public class Gun : IShootable
    {
        private Rigidbody2D _bulletPrefab; 
        private IViewServices _viewServices;
        private Transform _barrelPosition;
        private float _bulletLifeTime = 2f;

        private int _bulletLayer;
        private float _bulletMass;
        private Sprite _bulletSprite;

        private AudioClip _audioClip;
        private readonly AudioSource _audioSource;


        public float Force { get; protected set; }

        public Gun (Rigidbody2D bulletPrefab, IViewServices viewServices, float force, Transform barrelPosition, AudioSource audioSource, AudioClip audioClip)
        {
            _bulletPrefab = bulletPrefab;
            _viewServices = viewServices;
            Force = force;
            _barrelPosition = barrelPosition;
            _audioSource = audioSource;
            _audioClip = audioClip;
        }
        public Gun(int bulletLayer, Transform barrelPosition, float bulletMass, Sprite bulletSprite, float force)
        {
            _bulletLayer = bulletLayer;
            _barrelPosition = barrelPosition;
            _bulletMass = bulletMass;
            _bulletSprite = bulletSprite;
            Force = force;
        }

        public void SetBarrelPosition(Transform barrelPosition)
        {
            _barrelPosition = barrelPosition;
        }
        public void SetAudioClip(AudioClip audioClip)
        {
            _audioClip = audioClip;
        }


        public IEnumerator Shoot ()
        {
            var bullet = _viewServices.Instantiate<Rigidbody2D>(_bulletPrefab.gameObject, _barrelPosition);
            bullet.AddForce(_barrelPosition.up * Force);
            _audioSource.PlayOneShot(_audioClip);
            yield return new WaitForSeconds(_bulletLifeTime);
            _viewServices.Destroy(bullet.gameObject);
        }

        public GameObject AltShoot ()
        {
            var altBullet = new GameObject().
                SetName("AltBullet").
                SetLayer(_bulletLayer).
                SetTransform(_barrelPosition).
                AddBoxCollider2D().
                AddRigidbody2D(_bulletMass).
                AddSprite(_bulletSprite).
                AddForce(Force, _barrelPosition);
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

