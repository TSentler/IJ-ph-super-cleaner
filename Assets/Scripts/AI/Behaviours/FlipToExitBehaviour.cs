using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class FlipToExitBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnFLipStart;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            OnFLipStart?.Invoke();
        }
    }
}
