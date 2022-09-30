using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Trash.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TrashText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public event UnityAction<string> OnChange;
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(int count) 
        {
            if (_text == null)
                return;
            
            _text.SetText(count.ToString());
            OnChange?.Invoke(_text.text);
        }
    }
}
