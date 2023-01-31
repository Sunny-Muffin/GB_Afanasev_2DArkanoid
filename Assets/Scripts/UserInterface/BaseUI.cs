using UnityEngine;

namespace Arkanoid
{
    internal abstract class BaseUI : MonoBehaviour
    {
        public abstract void Execute();
        public abstract void Cancel();
    }
}
