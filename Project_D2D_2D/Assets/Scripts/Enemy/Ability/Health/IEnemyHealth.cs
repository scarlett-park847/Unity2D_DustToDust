

public interface IEnemyHealth
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void TakeHit(int damage);
}
