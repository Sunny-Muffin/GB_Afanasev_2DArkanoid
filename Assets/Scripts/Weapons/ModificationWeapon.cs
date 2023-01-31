using System.Collections;

namespace Arkanoid
{
    public abstract class ModificationWeapon : IShootable
    {
        private Gun _gun;
        public float Force { get; protected set; }
        public abstract Gun AddModification(Gun gun);
        public void ApplyModification(Gun gun)
        {
            _gun = AddModification(gun);
            Force = _gun.Force;
        }

        public IEnumerator Shoot()
        {
            return _gun.Shoot();
        }
    }
}

