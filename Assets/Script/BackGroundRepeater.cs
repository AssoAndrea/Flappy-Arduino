using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundRepeater : MonoBehaviour
{
    public float halfSize;
    public ObjectType type;
    public float speedMultiplier =1;

    float speed;
    [HideInInspector] public float distanceForReSpawn;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.inst.speeds.GetSpeed(type);

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        distanceForReSpawn = width/2;
        distanceForReSpawn += halfSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime * speedMultiplier, transform.position.y, transform.position.z);
        if(transform.position.x < -distanceForReSpawn)
        {
            transform.position = new Vector3(distanceForReSpawn, transform.position.y, transform.position.z);
        }
    }
}
