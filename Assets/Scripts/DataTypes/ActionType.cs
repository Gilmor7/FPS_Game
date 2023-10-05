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
            ProvokedByShoot,
            ProvokedByRange,
            Chase,
            Attack,
            Die,
            CreepyLaugh,
        }
        public enum WeaponAction
        {
            Fire,
            Reload,
            FireNoAmmo
        }
    }
}