using UnityEngine;
using UnityEngine.Events;

namespace AI
{
    public class SuccessfulTheftBehaviour : StateMachineBehaviour
    {
        public event UnityAction OnStart;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            OnStart?.Invoke();
        }
    }
}
