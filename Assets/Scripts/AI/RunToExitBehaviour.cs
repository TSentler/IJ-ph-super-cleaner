using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class RunToExitBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnRunEnd, OnRunUpdate;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            OnRunUpdate?.Invoke();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            OnRunEnd?.Invoke();
        }
    }
}
