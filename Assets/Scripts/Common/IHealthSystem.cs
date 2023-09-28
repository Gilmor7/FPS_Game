namespace Common
{
    public interface IHealthSystem
    {
        public bool TakeDamage(float damage);
        public void Die();
    }
}