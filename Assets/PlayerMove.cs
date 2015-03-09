using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 100;
    public float minTouchMove = 50;
    public const float laneOffest = 14;
    float horizontalMoveDistance;
    EnvGenerator envGenerator;
    Vector3 lastTouchPos;
    public int laneIndex = 1;
    public int targetLaneIndex;
    float[] laneOffsetX = new float[] { -laneOffest, 0, laneOffest };
    float horizontalMoveSpeed = 6;
    public bool isSliding = false;
    public float slideTime = 1.4f;
    float hasSlidedTime;
    public bool isJumping = false;
    bool isUp = false;
    public float jumpSpeed = 50;
    float hasJumpedHeight;
    public float maxJumpHeight = 20;
    Transform player;
    public AudioSource landSound;
    enum TouchDirection
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
	void Awake () {
        envGenerator = Camera.main.GetComponent<EnvGenerator>();
        player = transform.Find("Prisoner").transform;
        targetLaneIndex = laneIndex;
	}
	
    void Update()
    {
        if (GameController.gameState == GameController.GameState.Playing)
        {
            Vector3 targetPos = envGenerator.curForest.GetNextTargetPos();
            Vector3 moveDirection = targetPos + new Vector3(laneOffsetX[targetLaneIndex], 0, 0) - transform.position;
            transform.position += moveDirection.normalized * Time.deltaTime * moveSpeed;
            MoveControl();
        }
        
    }

    TouchDirection GetTouchDiretion()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 offset = Input.mousePosition - lastTouchPos;
            if (Mathf.Abs(offset.x) > minTouchMove || Mathf.Abs(offset.y) > minTouchMove)
            {
                if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
                {
                    if(offset.x < 0)
                    {
                        if (targetLaneIndex > 0)
                        {
                            --targetLaneIndex;
                            horizontalMoveDistance = -laneOffest;
                        }
                        return TouchDirection.LEFT;
                    }
                    else
                    {
                        if (targetLaneIndex < 2)
                        {
                            ++targetLaneIndex;
                            horizontalMoveDistance = laneOffest;
                        }
                        return TouchDirection.RIGHT;
                    }                    
                }
                else
                {
                    if (offset.y > 0)
                    {
                        isJumping = true;
                        isUp = true;
                        hasJumpedHeight = 0;
                        return TouchDirection.UP;
                    }
                    else
                    {
                        isSliding = true;
                        return TouchDirection.DOWN;
                    } 
                }
            }
        }

        return TouchDirection.NONE;
    }

    void MoveControl()
    {
        TouchDirection touchDir = GetTouchDiretion();
        if (laneIndex != targetLaneIndex)
        {
            float moveLenght = Mathf.Lerp(0, horizontalMoveDistance, Time.deltaTime * horizontalMoveSpeed);

            transform.position += new Vector3(moveLenght, 0, 0);
            horizontalMoveDistance -= moveLenght;
            if (Mathf.Abs(horizontalMoveDistance) < 0.5)
            {
                transform.position -= new Vector3(horizontalMoveDistance, 0, 0);
                horizontalMoveDistance = 0;
                laneIndex = targetLaneIndex;
            }
        }

        if (isSliding)
        {
            hasSlidedTime += Time.deltaTime;
        }
        if (hasSlidedTime > slideTime)
        {
            hasSlidedTime = 0;
            isSliding = false;
        }

        if (isJumping)
        {
            if (isUp)
            {
                hasJumpedHeight += jumpSpeed * Time.deltaTime;
                player.position += new Vector3(0, jumpSpeed * Time.deltaTime, 0);
                if (hasJumpedHeight - maxJumpHeight > 0)
                {
                    player.position -= new Vector3(0, hasJumpedHeight - maxJumpHeight, 0);
                    hasJumpedHeight = 0;
                    isUp = false;
                }
            }
            else
            {
                hasJumpedHeight += jumpSpeed * Time.deltaTime;
                player.position -= new Vector3(0, jumpSpeed * Time.deltaTime, 0);
                if (hasJumpedHeight - maxJumpHeight > 0)
                {
                    player.position += new Vector3(0, hasJumpedHeight - maxJumpHeight, 0);
                    isJumping = false;
                    landSound.Play();
                }
            }
        }
    }
}
