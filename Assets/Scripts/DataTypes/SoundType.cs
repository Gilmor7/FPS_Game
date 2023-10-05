namespace DataTypes
{
    public enum SoundType
    {
        //Player
        PlayerGetHurt = 0,
        ItemPickup = 1,
        //Enemy
        EnemyGetHurt = 10,
        EnemyAttack = 11,
        EnemyChasing = 12,
        EnemyDie = 13,
        EnemyCreepyLaugh = 14,
        EnemyProvokedByShoot = 15,
        EnemyProvokedByRange = 16,
        //Weapons:
        //M-4
        M4Fire = 20,
        M4Reload = 21,
        M4NoAmmo = 22,
        //Shotgun
        ShotgunFire = 30,
        ShotgunReload = 31,
        ShotgunNoAmmo = 32,
        //Pistol
        PistolFire = 40,
        PistolReload = 41,
        PistolNoAmmo = 42,
        
        None
    }
}