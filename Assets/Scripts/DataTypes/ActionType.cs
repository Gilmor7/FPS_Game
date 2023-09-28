namespace DataTypes
{
    public static class ActionType
    {
        public enum PlayerAction
        {
            GetHurt,
            ItemPickup,
        }
        public enum EnemyAction
        {
            GetHurt,
            Chase,
            Attack,
            Die,
        }
        public enum WeaponAction
        {
            Fire,
            Reload,
            FireNoAmmo
        }
    }
}