namespace Arkanoid
{
    public interface IMovable
    {
        float Speed { get; }
        void Move(float horizontal, float vertical, float deltaTime);
    }
}

