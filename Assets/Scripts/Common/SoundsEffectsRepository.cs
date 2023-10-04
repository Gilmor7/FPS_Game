using System.Collections.Generic;
using DataTypes;

namespace Common
{
    public static class SoundsEffectsRepository
    {
        private static readonly Dictionary<ActionType.PlayerAction, SoundType> _playerSoundTypesMap =
            new Dictionary<ActionType.PlayerAction, SoundType>
            {
                { ActionType.PlayerAction.GetHurt, SoundType.PlayerGetHurt },
                { ActionType.PlayerAction.ItemPickup, SoundType.ItemPickup }
            };
        
        private static readonly Dictionary<ActionType.EnemyAction, SoundType> _enemySoundTypesMap = 
            new Dictionary<ActionType.EnemyAction, SoundType>
            {
                { ActionType.EnemyAction.GetHurt, SoundType.EnemyGetHurt },
                { ActionType.EnemyAction.Attack, SoundType.EnemyAttack },
                { ActionType.EnemyAction.Chase, SoundType.EnemyChasing },
                { ActionType.EnemyAction.Die, SoundType.EnemyDie },
                { ActionType.EnemyAction.CreepyLaugh, SoundType.EnemyCreepyLaugh }
            };
        
        private static readonly Dictionary<(WeaponType, ActionType.WeaponAction), SoundType> _weaponSoundTypesMap =
            new Dictionary<(WeaponType, ActionType.WeaponAction), SoundType>
            {
                { (WeaponType.M4, ActionType.WeaponAction.Fire), SoundType.M4Fire },
                { (WeaponType.M4, ActionType.WeaponAction.Reload), SoundType.M4Reload },
                { (WeaponType.M4, ActionType.WeaponAction.FireNoAmmo), SoundType.M4NoAmmo },
                { (WeaponType.Shotgun, ActionType.WeaponAction.Fire), SoundType.ShotgunFire },
                { (WeaponType.Shotgun, ActionType.WeaponAction.Reload), SoundType.ShotgunReload },
                { (WeaponType.Shotgun, ActionType.WeaponAction.FireNoAmmo), SoundType.ShotgunNoAmmo },
                { (WeaponType.Pistol, ActionType.WeaponAction.Fire), SoundType.PistolFire },
                { (WeaponType.Pistol, ActionType.WeaponAction.Reload), SoundType.PistolReload },
                { (WeaponType.Pistol, ActionType.WeaponAction.FireNoAmmo), SoundType.PistolNoAmmo }
            };
        
        public static SoundType? GetPlayerSoundEffect(ActionType.PlayerAction playerAction)
        {
            _playerSoundTypesMap.TryGetValue(playerAction, out var playerSoundType);
            return playerSoundType;
        }
        
        public static SoundType? GetEnemySoundEffect(ActionType.EnemyAction enemyAction)
        {
            _enemySoundTypesMap.TryGetValue(enemyAction, out var enemySoundType);
            return enemySoundType;
        }
        
        public static SoundType? GetWeaponSoundEffect(WeaponType weaponType, ActionType.WeaponAction weaponAction)
        {
            _weaponSoundTypesMap.TryGetValue((weaponType, weaponAction), out var enemySoundType);
            return enemySoundType;
        }
    }
}