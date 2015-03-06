using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float moveSpeed = 100;
    public float minTouchMove = 50;
    public const float laneOffest = 14;
    float horizontalMoveDistance;
    EnvGenerator envGenerator;
    Vector3 lastTouchPos;
    int laneIndex = 1;
    int targetLaneIndex;
    float[] laneOffsetX = new float[] { -laneOffest, 0, laneOffest };
    float horizontalMoveSpeed = 6;
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
                        return TouchDirection.UP;
                    }
                    else
                    {
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
            //envGenerator.curForest.wayPoint.transform.position += new Vector3(laneOffsetX[laneIndex], 0, 0);
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
        switch (touchDir)
        {
            case TouchDirection.LEFT:               

                break;
            case TouchDirection.RIGHT:
                break;
            case TouchDirection.UP:
                break;
            case TouchDirection.DOWN:
                break;
            default:
                break;
        }
    }
}
