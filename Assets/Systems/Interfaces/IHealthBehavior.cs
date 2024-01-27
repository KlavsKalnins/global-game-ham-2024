
public interface IHealthBehavior
{
    public void Damage(int damage, bool finishInstantly = false, bool? shouldCallStun = null);
    public void FinishDamage();
}