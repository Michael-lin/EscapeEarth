using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    enum AnimationState
    {
        Idle,
        Run,

    }

    Animation animation;
    AnimationState animationState;
	void Awake () {
        animation = transform.Find("Prisoner").animation;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.gameState == GameController.GameState.Menu)
        {
            animationState = AnimationState.Idle;
        }
        else if (GameController.gameState == GameController.GameState.Playing)
        {
            animationState = AnimationState.Run;
        }
	}

    void LateUpdate()
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                PlayIdle();
                break;
            case AnimationState.Run:
                PlayAnimation("run");
                break;
            default:
                break;
        }
    }

    void PlayIdle()
    {
        if (!animation.IsPlaying("Idle_1") && !animation.IsPlaying("Idle_2"))
        {
            animation.Play("Idle_1");
            animation.PlayQueued("Idle_2");
        }
    }

    void PlayAnimation(string aniName)
    {
        if (!animation.IsPlaying(aniName))
        {
            animation.Play(aniName);
        }
    }
}
