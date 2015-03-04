using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (player.position.z > transform.position.z + 100)
        {
            Camera.main.SendMessage("GenerateForest");
            GameObject.Destroy(this.gameObject);
        }
	}
}
