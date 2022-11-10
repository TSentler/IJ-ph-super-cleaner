using System;
using TMPro;
using UnityEngine;

namespace Trash.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DoubleTrashText : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        
        [SerializeField] private TrashText _trashText;

        private void OnValidate()
        {
            if (_trashText == null)
                Debug.LogWarning("TrashText was not found!", this);
        }
        
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _trashText.OnChange += ChangeHndler;
        }

        private void OnDisable()
        {
            _trashText.OnChange -= ChangeHndler;
        }

        private void ChangeHndler(string text)
        {
            _text.SetText(text);
        }
    }
}
