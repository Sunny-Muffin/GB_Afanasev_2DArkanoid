using UnityEngine;

namespace Arkanoid
{
    public sealed class Health : IDamagable
    {
        public float MaxHealth { get; }
        public float HealthPoints { get; private set; }

        public Health(float max, float healthPoints)
        {
            MaxHealth = max;
            HealthPoints = healthPoints;
        }

        public void TakeDamage (float damage)
        {
            HealthPoints -= damage;
        }

        public void AddHealth(float aid)
        {
            HealthPoints += aid;
        }

        public void UpdateHealth(float hp)
        {
            HealthPoints = hp;
        }
    }
}
