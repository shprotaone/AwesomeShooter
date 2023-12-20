namespace Infrastructure.CommonSystems
{
    public class EcsWorldStartupProxy : IEcsStartupWorld
    {
        private IEcsStartupWorld _startupWorld;

        public void SetStartup(IEcsStartupWorld startup)
        {
            _startupWorld = startup;
        }
        
        public void StartSystems()
        {
            _startupWorld.StartSystems();;
        }
    }
}