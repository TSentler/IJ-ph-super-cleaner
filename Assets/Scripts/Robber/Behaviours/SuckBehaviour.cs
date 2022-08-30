using UnityEngine;
using UnityEngine.Events;

namespace Robber
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
