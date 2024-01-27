
public interface IHealthBehavior
{
    public void Damage(int damage, bool finishInstantly = false);
    public void FinishDamage();
}