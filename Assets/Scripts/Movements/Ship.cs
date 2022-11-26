
using UnityEngine;

namespace Arkanoid
{
    internal sealed class Ship : IMovable, IRotatable
    {
        private readonly IMovable _moveImplementation;
        private readonly IRotatable _rotationImplementation;
        public float Speed => _moveImplementation.Speed;

        public Ship(IMovable moveImplementation, IRotatable rotationImplementation)
        {
            _moveImplementation = moveImplementation;
            _rotationImplementation = rotationImplementation;
        }

        public void Move(float horizontal, float vertical, float deltaTime)
        {
            _moveImplementation.Move(horizontal, vertical, deltaTime);
        }
    
        public void Rotate(Vector3 direction)
        {
            _rotationImplementation.Rotate(direction);
        }
        public void AddAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.AddAcceleration();
            }
        }
        public void RemoveAcceleration()
        {
            if (_moveImplementation is AccelerationMove accelerationMove)
            {
                accelerationMove.RemoveAcceleration();
            }
        }
    }
}
