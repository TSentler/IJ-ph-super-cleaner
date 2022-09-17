using TMPro;
using UnityEngine;

namespace LevelLoader.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelNumberText : MonoBehaviour
    {
        private TMP_Text _text;
        private string _template = $"LEVEL {{0}}";

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetNumber(string number)
        {
            _text.SetText(string.Format(_template, number));
        }
    }
}
