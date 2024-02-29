using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Test
{
    public class TestServiceView : MonoBehaviour,IView
    {
        [SerializeField] private List<TestLineView> _views;
        [SerializeField] private Transform parent;

        private ITestService _testService;
        private GameplayUIFactory _gameplayUIFactory;

        [Inject]
        public void Construct(ITestService testService,GameplayUIFactory gameplayUIFactory)
        {
            _testService = testService;
            _gameplayUIFactory = gameplayUIFactory;
        }

        public async UniTask Init()
        {
            for (int i = 0; i < _testService.Services.Count; i++)
            {
                TestLineView line = await _gameplayUIFactory.CreateTestLineView();
                line.transform.SetParent(parent);
                line.ChangeName(_testService.Services.ElementAt(i).Key);
                _views.Add(line);
            }

            _testService.SetView(this);
        }

        private void ToLeft(int obj)
        {
            _views[0].ChangeValue(obj.ToString());
        }

        public void OnUpdateValue(string name,int value)
        {
            var line = _views.Find(x => x.LineName == name);
            line.ChangeValue(value.ToString());
        }
    }
}