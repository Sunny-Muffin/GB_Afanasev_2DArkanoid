using UnityEngine;


namespace Arkanoid
{
    public class Gun : ICanShoot
    {
        private Transform _barrel;
        private float _coolDown; // realize it later, maybe via coroutines

        public float Force { get; protected set; }

        public Gun (float force, Transform barrel)
        {
            Force = force;
            _barrel = barrel;
        }

        public void Shoot(Rigidbody2D bullet)
        {
            if (bullet)
            {
                bullet.AddForce(_barrel.up * Force);
            }
        }
    }
}

