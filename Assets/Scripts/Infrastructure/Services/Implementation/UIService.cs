using System.Collections.Generic;
using UI;
using Zenject;

namespace Infrastructure.Services
{
    public class UIService
    {
        private List<IView> _services;

        public UIService()
        {
            _services = new List<IView>();
        }

        public void AddService(IView view)
        {
            _services.Add(view);
        }

        public T GetView<T>()
        {
            return (T)_services.Find(x => x.GetType() == typeof(T));
        }
    }
}