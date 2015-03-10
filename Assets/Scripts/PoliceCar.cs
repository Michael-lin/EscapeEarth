using UnityEngine;
using System.Collections;

public class PoliceCar : MonoBehaviour {

    public AudioSource brakeSound;
    bool hasPlayedSound;
	void Update () {
        if (!hasPlayedSound && GameController.gameState == GameController.GameState.End)
        {
            brakeSound.Play();
            hasPlayedSound = true;
        }
	}
}
