using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;
    public GameObject[] Hearts;

    int Life = 3;

    private void Awake()
    {
        instance = this;
    }

    public bool Damage()
    {
        Life--;
        UpdateHeart();
        if (Life <= 0)
        {
            return true;
        }
        else return false;
    }
    public void Heal()
    {
        Life++;
        if (Life > 3)
        {
            Life = 3;
        }
        UpdateHeart();
    }
    public void UpdateHeart()
    {
        for (int i = 0; i < 3; i++)
        {
            Hearts[i].SetActive(true);
            if (i > Life -1)
            {
                Hearts[i].SetActive(false);
            }
        }
    }
}
