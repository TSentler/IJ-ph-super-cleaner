using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class SuckBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnSuckStart;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            OnSuckStart?.Invoke();
        }
    }
}
