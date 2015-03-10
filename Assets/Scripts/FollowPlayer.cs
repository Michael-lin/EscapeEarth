using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public float followSpeed;

    Transform player;
    Vector3 offset = Vector3.zero;
	void Awake () {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);
	}
}
