using Settings;

namespace Infrastructure.CommonSystems
{
    public class LevelSettingsContainer
    {
        private PlayerSettingsSO _playerSettings;
        private PlayerLevelSettingsSO _levelSettings;

        public PlayerSettingsSO PlayerSettings => _playerSettings;
        public PlayerLevelSettingsSO PlayerLevelSettingsSo => _levelSettings;

        public void SetPlayerSettings(PlayerSettingsSO playerSettings) =>
            _playerSettings = playerSettings;

        public void SetPlayerLevelSettings(PlayerLevelSettingsSO levelSettingsSo) =>
            _levelSettings = levelSettingsSo;

    }
}