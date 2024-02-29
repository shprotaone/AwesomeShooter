using System;
using System.Collections.Generic;
using System.Text;

namespace Scripts.Test
{
    public class TestService : ITestService
    {
        private event Action<string,int> OnChanged;

        private Dictionary<string, int> _testDict;
        private TestServiceView _testServiceView;

        public Dictionary<string, int> Services => _testDict;

        public TestService()
        {
            _testDict = new Dictionary<string, int>();
        }

        public void AddField(string name)
        {
            _testDict.Add(name,0);
        }

        public void Increase<T>()
        {
            string key = typeof(T).ToString();
            _testDict[key]++;
            var line = new KeyValuePair<string,int>(key,_testDict[key]);
            OnChanged?.Invoke(line.Key,line.Value);
        }

        public void Decrease<T>()
        {
            string key = typeof(T).ToString();
            _testDict[key]--;
            var line = new KeyValuePair<string,int>(key,_testDict[key]);
            OnChanged?.Invoke(line.Key,line.Value);
        }

        public void SetView(TestServiceView testServiceView)
        {
            _testServiceView = testServiceView;
            OnChanged += _testServiceView.OnUpdateValue;
        }
    }
}