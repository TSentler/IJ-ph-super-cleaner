using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class FlipToExitBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnRunStart, OnRunEnd;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            OnRunStart?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            OnRunEnd?.Invoke();
        }
    }
}
