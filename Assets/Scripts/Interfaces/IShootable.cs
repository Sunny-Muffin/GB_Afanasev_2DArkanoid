using System.Collections;
using UnityEngine;

namespace Arkanoid
{
    public interface IShootable
    {
        float Force { get; }
        //void Shoot();
        IEnumerator Shoot();
    }
}
