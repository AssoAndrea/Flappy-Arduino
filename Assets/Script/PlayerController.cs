using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 6;
    public TMP_Text pointstxt;
    public GameObject GameOver;
    public GameObject shield;

    public int maxInput = 40;
    public int minInput = 8;
    public float lerpSpeed;

    [Header("AUDIO")]
    public AudioSource PU;
    public AudioSource Damage;

    SerialPort sp;
    int dir = 0;
    string the_com;
    int serialInput;
    float screenH;
    float minY, maxY;
    public float newval = 0;
    public bool USE_ARDUINO_CONTROLLER = true;

    float points;
    float prevVal;
    int misurazioni;
    int somma;
    bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        screenH = 2f * cam.orthographicSize;
        minY = screenH / 2 + 1;
        maxY = screenH / 2 - 1;


        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM1") { 
                the_com = mysps;
                //USE_ARDUINO_CONTROLLER = true;
                break;
            }
        }

        //if (USE_ARDUINO_CONTROLLER)
        //{
            sp = new SerialPort("\\\\.\\" + the_com, 9600);
            if (!sp.IsOpen)
            {
                print("Opening " + the_com + ", baud 9600");
                sp.Open();
                sp.ReadTimeout = 100;
                sp.Handshake = Handshake.None;
                if (sp.IsOpen) { print("Open"); }
            }  
        //}
    }

    // Update is called once per frame
    void Update()
    {

        points += Time.deltaTime;
        pointstxt.text = ((int)points).ToString();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newval += Time.deltaTime * speed;
            dir = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            newval -= Time.deltaTime * speed;

            dir = -1;
        }
        else dir = 0;

        if (sp.BytesToRead > 0)
        {
            int.TryParse(sp.ReadLine(),out serialInput);
            Debug.Log(serialInput);
            serialInput = Mathf.Clamp(serialInput, minInput, maxInput);

            newval = serialInput - minInput;
            if (prevVal != 0)
            {
                newval = (newval + newval + prevVal) / 3f;
            }
            prevVal = newval;
            newval = newval / (maxInput - minInput);
        }
        //if (Math.Abs(transform.position.y - (newval * screenH) - screenH / 2) > 0.05f)
        //{
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (newval * screenH) - screenH / 2), lerpSpeed * Time.deltaTime);
        //}

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HeartManager.instance.Damage())
        {
            Die();
        }
        else
        {
            Debug.Log("aoo");
            Damage.Play();
            Shield();
        }
    }
    private void Shield()
    {
        shield.SetActive(true);
        invincible = true;
        GetComponent<CircleCollider2D>().isTrigger = true;
        StartCoroutine(ShieldProtection());
    }
    IEnumerator ShieldProtection()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("coroutine");
        invincible = false;
        shield.SetActive(false);
        GetComponent<CircleCollider2D>().isTrigger = false;

        
    }
    private void Die()
    {
        GameOver.SetActive(true);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        sp.Close();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Heart")
        {
            HeartManager.instance.Heal();
            PU.Play();
            Destroy(collision.gameObject);
        }
    }
}
