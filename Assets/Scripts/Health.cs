using UnityEngine;

namespace Arkanoid
{
    internal class Health : IDamagable
    {
        public float HealthPoints { get; protected set; }

        public Health(float healthPoints)
        {
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

        /*
        public float UpdateHealth()
        {
            return HealthPoints;
        }
        */
    }
}
