using UnityEngine;

namespace Arkanoid
{
    public interface ICanShoot
    {
        float Force { get; }
        void Shoot(Rigidbody2D bullet);
    }
}
