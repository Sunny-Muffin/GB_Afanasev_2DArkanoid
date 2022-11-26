using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Arkanoid
{
    public abstract class EnemySpawn : MonoBehaviour
    {
        public static IEnemyFactory Factory;
        public Health Health { get; protected set; }

        public static Asteroid CreateAsteroidEnemy(Health hp)
        {
            var enemy = Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"));
            enemy.Health = hp;
            return enemy;
        }

        public static Asteroid CreateAsteroidWithPosition(Health hp, float force, Vector2 position, Vector2 direction)
        {
            var enemy = Instantiate(Resources.Load<Asteroid>("Enemy/Asteroid"), position, Quaternion.identity);
            enemy.Health = hp;
            enemy.GetComponent<Rigidbody2D>().AddForce(direction * force);
            return enemy;
        }

        public void DependencyInjectHealth(Health hp)
        {
            Health = hp;
        }
    }
}
