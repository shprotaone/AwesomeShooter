using System;
using UnityEngine.Rendering;

namespace Infrastructure.ECS.Components
{
    public struct ExperienceComponent
    {
        public Action<int> OnLevelUp;
        public Action<float> OnExperienceAdd;
        public int Level;
        public int Experience;
    }
}