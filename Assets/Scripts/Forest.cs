using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

    public GameObject[] obstacles;
    public float startLength = 50;
    public float minLength = 100;
    public float maxLength = 200;

    Transform player;
    public WayPoint wayPoint;
    int wayPointsIndex;
    EnvGenerator envGenerator;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        wayPoint = transform.Find("wayPoints").GetComponent<WayPoint>();
        wayPointsIndex = wayPoint.points.Length - 2;
        envGenerator = Camera.main.GetComponent<EnvGenerator>();
    }

	void Start () {
        GenerateObstacle();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (player.position.z > transform.position.z + 100)
        {
            Camera.main.SendMessage("GenerateForest");
            GameObject.Destroy(this.gameObject);
        }
         * */
	}

    void GenerateObstacle()
    {
        float startZ = transform.position.z - 3000;
        float endZ = transform.position.z;
        float z = startZ + startLength;
        while (true)
        {
            z += Random.Range(minLength, maxLength);
            if (z > endZ)
            {
                break;
            }
            else
            {
                Vector3 pos = GetPosByZ(z);
                int index = Random.Range(0, obstacles.Length);
                GameObject go = GameObject.Instantiate(obstacles[index], pos, Quaternion.identity) as GameObject;
                go.transform.parent = transform;
            }
        }
    }

    Vector3 GetPosByZ(float z)
    {
        Transform[] points = wayPoint.points;
        int index = 0;
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (z <= points[i].position.z && z >= points[i + 1].position.z)
            {
                index = i;
                break;
            }
        }
        return Vector3.Lerp(points[index].position, points[index + 1].position, (z - points[index].position.z) / (points[index + 1].position.z - points[index].position.z));
    }

    public Vector3 GetNextTargetPos()
    {        
        while (true)
        {
            if ((wayPoint.points[wayPointsIndex].position.z - player.position.z) < 10)
            {
                wayPointsIndex--;
                if (wayPointsIndex < 0)
                {
                    Destroy(this.gameObject, 2);
                    envGenerator.GenerateForest();
                    return envGenerator.curForest.GetNextTargetPos();
                }
            }
            else
            {
                return wayPoint.points[wayPointsIndex].position;
            }
        }
    }
}
