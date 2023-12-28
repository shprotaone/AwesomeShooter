using Settings;

namespace Infrastructure.CommonSystems
{
    public class LevelSettingsContainer
    {
        private PlayerSettingsSO _playerSettings;
        private PlayerLevelProgress _levelSettings;

        public PlayerSettingsSO PlayerSettings => _playerSettings;
        public PlayerLevelProgress PlayerLevelProgress => _levelSettings;

        public void SetPlayerSettings(PlayerSettingsSO playerSettings) =>
            _playerSettings = playerSettings;

        public void SetPlayerLevelSettings(PlayerLevelProgress levelProgress) =>
            _levelSettings = levelProgress;

    }
}