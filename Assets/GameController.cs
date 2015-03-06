using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public static GameState gameState;
    public GameObject tapToStart;
	public enum GameState {
        Menu,
        Playing,
        End
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (gameState == GameState.Menu && Input.GetMouseButtonDown(0))
        {
            gameState = GameState.Playing;
            tapToStart.SetActive(false);
        }
	}
}
