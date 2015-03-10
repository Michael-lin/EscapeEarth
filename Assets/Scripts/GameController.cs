using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public static GameState gameState;
    public GameObject tapToStart;
    public GameObject gameOver;
	public enum GameState {
        Menu,
        Playing,
        End
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState == GameState.Menu)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("GameControl button down");
                gameState = GameState.Playing;
                tapToStart.SetActive(false);
            }
        }
        else if (gameState == GameState.End)
        {
            gameOver.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Menu;
                Application.LoadLevel(0);
            }
        }
	}
}
