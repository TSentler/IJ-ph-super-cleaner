using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class RunToTargetBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnRunStart, OnRunUpdate;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            OnRunStart?.Invoke();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            OnRunUpdate?.Invoke();
        }
    }
}
