namespace Infrastructure.Services
{
    public class TempService : IGameService
    {
        public TempService(IServiceInitializer serviceInitializer,
            SkillGameService skillGameService)
        {
            serviceInitializer.Add(this);
        }
        public void Init()
        {
            
        }
    }
}