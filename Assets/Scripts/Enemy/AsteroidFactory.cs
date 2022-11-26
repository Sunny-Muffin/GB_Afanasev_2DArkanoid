using UnityEngine;

namespace Arkanoid
{
    internal sealed class AsteroidFactory : IEnemyFactory
    {
        public EnemySpawn Create (Health hp)
        {
            var enemy = Object.Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"));
            enemy.DependencyInjectHealth(hp);
            return enemy;
        }
    }
}
