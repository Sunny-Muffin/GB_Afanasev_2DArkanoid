using UnityEngine;

namespace Arkanoid
{
    public class Barrel : IBarrel
    {
        public AudioClip BarrelAudioClip { get; }
        public Transform BarrelPosition { get; }
        public GameObject BarrelInstance { get; }
        public Barrel(AudioClip barrelAudioClip, Transform barrelPosition, GameObject barrelInstance)
        {
            BarrelAudioClip = barrelAudioClip;
            BarrelPosition = barrelPosition;
            BarrelInstance = barrelInstance;
        }
    }
}

