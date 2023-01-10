namespace Arkanoid
{
    public interface IDamagable
    {
        float HealthPoints { get; }
        void TakeDamage(float damage);
    }
}
