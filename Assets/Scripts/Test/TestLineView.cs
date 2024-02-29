using TMPro;
using UnityEngine;

namespace Scripts.Test
{
    public class TestLineView : MonoBehaviour
    {
        private string _lineName;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _value;

        public string LineName => _lineName;
        public void ChangeName(string name)
        {
            _name.SetText(name);
            _lineName = name;
        }

        public void ChangeValue(string value)
        {
            _value.SetText(value);
        }
    }
}

