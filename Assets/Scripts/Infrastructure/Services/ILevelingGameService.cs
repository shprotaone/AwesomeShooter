using System;

namespace Infrastructure.Services
{
    public interface ILevelingGameService
    {
        event Action<int> OnUpdateLevel;
        void CheckNextLevel(int experienceValue);
    }
}