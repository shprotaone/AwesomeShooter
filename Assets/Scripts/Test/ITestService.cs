using System.Collections.Generic;

namespace Scripts.Test
{
    public interface ITestService
    {
        Dictionary<string, int> Services { get; }
        void AddField(string name);
        void Increase<T>();
        void Decrease<T>();
        void SetView(TestServiceView testServiceView);
    }
}