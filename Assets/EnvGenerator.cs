using UnityEngine;
using System.Collections;

public class EnvGenerator : MonoBehaviour {

    public Forest curForest;
    public Forest nextForest;
    public GameObject[] forests;
    int forestCount = 2;

    public void GenerateForest()
    {
        forestCount++;
        int type = Random.Range(0, 3);
        GameObject newForest = GameObject.Instantiate(forests[type], new Vector3(0, 0, 3000 * forestCount), Quaternion.identity) as GameObject;

        curForest = nextForest;
        nextForest = newForest.GetComponent<Forest>();
    }
}
