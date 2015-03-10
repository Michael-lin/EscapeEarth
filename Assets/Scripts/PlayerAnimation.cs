using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    public enum AnimationState
    {
        Idle,
        Run,
        TurnLeft,
        TurnRight,
        Jump,
        Slide,
        Death
    }

    Animation playerAnimation;
    public AnimationState animationState;
    PlayerMove playerMove;

    public AudioSource footStepSound;
    
	void Awake () {
        playerAnimation = transform.Find("Prisoner").animation;
        playerMove = this.GetComponent<PlayerMove>();
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
            if (playerMove.isJumping)
            {
                animationState = AnimationState.Jump;
            }
            if (playerMove.targetLaneIndex > playerMove.laneIndex)
            {
                animationState = AnimationState.TurnRight;
            }
            else if (playerMove.targetLaneIndex < playerMove.laneIndex)
            {
                animationState = AnimationState.TurnLeft;
            }
            if (playerMove.isSliding)
            {
                animationState = AnimationState.Slide;
            }

            if (animationState == AnimationState.Run)
            {
                if (!footStepSound.isPlaying)
	            {
		             footStepSound.Play();
	            }
                
            }
            else
            {
                if (footStepSound.isPlaying)
                {
                    footStepSound.Stop();
                }               
            }
        }
        else if (GameController.gameState == GameController.GameState.End)
        {
            animationState = AnimationState.Death;
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
            case AnimationState.TurnLeft:
                playerAnimation["left"].speed = 2;
                PlayAnimation("left");
                break;
            case AnimationState.TurnRight:
                playerAnimation["right"].speed = 2;
                PlayAnimation("right");
                break;
            case AnimationState.Jump:
                PlayAnimation("jump");
                break;
            case AnimationState.Slide:
                PlayAnimation("slide");
                break;
            case AnimationState.Death:
                PlayDeath();
                break;
            default:
                break;
        }
    }

    void PlayIdle()
    {
        if (!playerAnimation.IsPlaying("Idle_1") && !playerAnimation.IsPlaying("Idle_2"))
        {
            playerAnimation.Play("Idle_1");
            playerAnimation.PlayQueued("Idle_2");
        }
    }

    void PlayAnimation(string aniName)
    {
        if (!playerAnimation.IsPlaying(aniName))
        {
            playerAnimation.Play(aniName);
        }
    }

    bool isDeathPlayed;
    void PlayDeath()
    {
        if (!isDeathPlayed)
        {
            playerAnimation.Play("death");
            isDeathPlayed = true;
        }
    }

}
