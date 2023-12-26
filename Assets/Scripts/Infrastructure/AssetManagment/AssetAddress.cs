using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagment
{
    public static class AssetAddress
    {
        public const string CommonBulletPrefabPath = "CommonBullet";
        public const string WeaponBaseModelPath = "BaseWeapon";
        public const string CommonEnemyPrefabPath = "CommonEnemy";

        //---Scenes---

        public const string GameplayScenePath = "GameplayScene";
        public const string MainMenuScenePath = "MainMenuScene";

        //---Player---

        public const string PlayerPrefabPath = "Player";
        public const string PlayerLevelSettingsSOPath = "Player Level Settings SO";
        public const string PlayerSettingsSOPath = "PlayerSettingsSO";

        //---LevelSettings---

        public const string FirstLevelGameSceneDataPath = "InitialPointsLevel1";

        //---CommonSystems---

        public const string LoadingCurtainPath = "CurtainCanvas";
        public const string GameBootstrapperPath = "GameBootstrapper";

        //---UI---

        public const string MainMenuCanvasPath = "MainMenuCanvas";

        public const string GameplayHUDPath = "MainGameplayCanvas";
        public const string PlayerInfoPanelPath = "PlayerInfoPanel";
        public const string LevelInfoPanelPath = "LevelInfoPanel";
        public const string WeaponPanelPath = "WeaponPanel";
    }
}