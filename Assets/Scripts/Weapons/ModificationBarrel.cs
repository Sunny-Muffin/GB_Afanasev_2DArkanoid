using UnityEngine;

namespace Arkanoid
{
    internal sealed class ModificationBarrel : ModificationWeapon
    {
        private readonly Transform _barrelPosition;
        private readonly IBarrel _barrel;
        private readonly AudioSource _barrelSound;

        public ModificationBarrel(Transform barrelPosition, IBarrel barrel, AudioSource barrelSound)
        {
            _barrelPosition = barrelPosition;
            _barrel = barrel;
            _barrelSound = barrelSound;
        }

        public override Gun AddModification(Gun gun)
        {
            var barrel = Object.Instantiate(_barrel.BarrelInstance, _barrelPosition.position, _barrelPosition.rotation);
            barrel.transform.parent = _barrelPosition;
            gun.SetBarrelPosition(_barrel.BarrelPosition);
            gun.SetAudioClip(_barrel.BarrelAudioClip);
            return gun;
        }
    }
}
