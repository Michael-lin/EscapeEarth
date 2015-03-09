using UnityEngine;
using System.Collections;

public class SlideCollider : MonoBehaviour {

    PlayerAnimation playerAnimation;

    void Awake()
    {
        playerAnimation = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAnimation>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameController.gameState == GameController.GameState.Playing && playerAnimation.animationState == PlayerAnimation.AnimationState.Slide)
        {
            if (other.tag == Tags.obstacles)
            {
                playerAnimation.animationState = PlayerAnimation.AnimationState.Death;
                GameController.gameState = GameController.GameState.End;
            }
        }
    }
}
