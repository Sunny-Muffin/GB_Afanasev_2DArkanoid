using UnityEngine;

namespace Arkanoid
{
    internal sealed class ModificationBarrel : ModificationWeapon
    {
        private readonly Vector3 _barrelPosition;
        private readonly IBarrel _barrel;
        private readonly AudioSource _barrelSound;

        public ModificationBarrel(Vector3 barrelPosition, IBarrel barrel, AudioSource barrelSound)
        {
            _barrelPosition = barrelPosition;
            _barrel = barrel;
            _barrelSound = barrelSound;
        }

        public override Gun AddModification(Gun gun)
        {
            var barrel = Object.Instantiate(_barrel.BarrelInstance, _barrelPosition, Quaternion.identity);
            gun.SetBarrelPosition(_barrel.BarrelPosition);
            gun.SetAudioClip(_barrel.BarrelAudioClip);
            return gun;
        }
    }
}
