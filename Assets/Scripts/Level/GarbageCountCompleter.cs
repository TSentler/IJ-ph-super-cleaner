using Trash;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Completer))]
    public class GarbageCountCompleter : MonoBehaviour
    {
        private Completer _completer;

        [SerializeField] private GarbageCounter _garbageCounter;
        
        private void OnValidate()
        {
            if (_garbageCounter == null)
                Debug.LogWarning("GarbageCounter was not found!", this);
        }
        
        private void Awake()
        {
            _completer = GetComponent<Completer>();
        }
        
        private void OnEnable()
        {
            _garbageCounter.OnCollect += CollectHandler;
        }

        private void OnDisable()
        {
            _garbageCounter.OnCollect -= CollectHandler;
        }
        
        private void CollectHandler(int collected)
        {
            if (_garbageCounter.Count == collected)
            {
                _completer.Complete();
            }
        }
    }
}
