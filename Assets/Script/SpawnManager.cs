using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Obstacles;
    public GameObject[] PowerUp;
    public GameObject[] FloorDecoration;
    [Space(20)]
    //public int MaxObstacles;
    //public int MaxSkyDecoration, MaxFloorDecoration;
    public float minObstacleDelay;
    public float MaxObstacleDelay;

    public float minPUdelay;
    public float MaxPUdelay;

    float ObstacleDelay;
    float PUdelay;
    float ObstacleTimer = 0;
    float screenH;
    float screenW;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        screenH = 2f *cam.orthographicSize;
        screenW = screenH * cam.aspect;

        ObstacleDelay = Random.Range(minObstacleDelay, MaxObstacleDelay);
        StartCoroutine(SpawnPUtimer());

    }
    public void SpawnObstacle()
    {
        int randIndex = Random.Range(0, Obstacles.Length);
        GameObject objToSpawn = Obstacles[randIndex];
        float randH = Random.Range(-screenH/2 + 1.5f,screenH/2 -1.5f);
        GameObject obj = Instantiate(objToSpawn);
        obj.transform.position = new Vector3(screenW + 2, randH, 0);
    }
    public void SpawnPU()
    {
        int randIndex = Random.Range(0, PowerUp.Length);
        GameObject objToSpawn = PowerUp[randIndex];
        float randH = Random.Range(-screenH / 2 + 2f, screenH / 2 - 2f);
        GameObject obj = Instantiate(objToSpawn);
        obj.transform.position = new Vector3(screenW + 2, randH, 0);


    }
    IEnumerator SpawnPUtimer()
    {
        PUdelay = Random.Range(minPUdelay, MaxPUdelay);
        yield return new WaitForSeconds(PUdelay);
        SpawnPU();
        StartCoroutine(SpawnPUtimer());
    }
    // Update is called once per frame
    void Update()
    {
        ObstacleTimer += Time.deltaTime;
        if (ObstacleTimer > ObstacleDelay)
        {
            ObstacleTimer = 0;
            ObstacleDelay = Random.Range(minObstacleDelay, MaxObstacleDelay);
            SpawnObstacle();

        }
    }
}
