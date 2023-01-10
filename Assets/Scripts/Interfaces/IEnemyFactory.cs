namespace Arkanoid
{
    public interface IEnemyFactory
    {
        EnemySpawn Create(Health hp);
    }
}

