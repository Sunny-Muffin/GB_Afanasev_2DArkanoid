using UnityEngine;

namespace Arkanoid
{
    internal interface IBarrel
    {
        AudioClip BarrelAudioClip { get; }
        Transform BarrelPosition { get; }
        GameObject BarrelInstance { get; }
    }
}
