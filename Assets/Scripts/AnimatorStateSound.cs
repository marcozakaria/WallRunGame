using UnityEngine;

public class AnimatorStateSound : StateMachineBehaviour
{
    public string soundName;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.instance.Play(soundName);
    }
}
