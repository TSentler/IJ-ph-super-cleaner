using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace Robber
{
    public class RunToTargetBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnRunStart, OnRunUpdate, OnRunEnd;

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

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller)
        {
            base.OnStateExit(animator, stateInfo, layerIndex, controller);
            OnRunEnd?.Invoke();
        }
    }
}
