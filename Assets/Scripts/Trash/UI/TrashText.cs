using System;
using TMPro;
using UnityEngine;

namespace Trash.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TrashText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(int count) 
        {
            _text.SetText(count.ToString());
        }
    }
}
