using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float halfSize;
    public ObjectType type;

    float speed;
    float distanceForDestroy;
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.inst.speeds.GetSpeed(type);
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        distanceForDestroy = width / 2 + halfSize + 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x < - distanceForDestroy)
        {
            Destroy(transform.gameObject);
        }

    }
}
