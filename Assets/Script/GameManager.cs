using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    public ParallaxSpeed speeds;

    float speedOffset;



    public delegate void OnLevelSpeedChanged();
    public static OnLevelSpeedChanged OnLevelSpeedChangedEvent;


    public static void ChangeLevelSpeed(float speedToAdd)
    {
        inst.speeds.AddSpeed(speedToAdd);
        OnLevelSpeedChangedEvent?.Invoke();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (inst == null)
        {
            inst = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

[Serializable]
public class ParallaxSpeed
{
    public float Floor = 5;
    public float Mountain_1 = 4f;
    public float Mountain_2 = 3;
    public float Sky_1 = 2.5f;
    public float Sky_2 = 2;

    public float GetSpeed(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Floor:
                return Floor;
            case ObjectType.Mountain_1:
                return Mountain_1;
            case ObjectType.Mountain_2:
                return Mountain_2;
            case ObjectType.Sky_1:
                return Sky_1;
            case ObjectType.Sky_2:
                return Sky_2;
            case ObjectType.Last:
                return 0;
            default:
                return 0;
        }
    }
    public void AddSpeed(float speed)
    {
        Floor += speed;
        Mountain_1 += speed;
        Mountain_2 += speed;
        Sky_1 += speed;
        Sky_2 += speed;
    }
}
public enum ObjectType
{
    Floor,Mountain_1,Mountain_2,Sky_1,Sky_2,Last
}
