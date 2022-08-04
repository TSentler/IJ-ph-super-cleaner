using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class CollectedText : MonoBehaviour
    {
        private readonly string _separator = "/";
    
        private TMP_Text _text;
    
        private string Template => $"{{0}}{_separator}{{1}}";

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            SetText("0", "0");
        }

        private void SetText(string collected, string count)
        {
            _text.SetText(string.Format(Template, collected, count));
        }

        public void SetCount(int count)
        {
            var collected = _text.text.Split(_separator)[0];
            SetText(collected, count.ToString());
        }
    
        public void SetCollected(int collected)
        {
            var count = _text.text.Split(_separator)[1];
            SetText(collected.ToString(), count);
        }
    }
}
