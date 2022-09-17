using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    #region Singelton Decleration
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion


    public string sceneType;
    private float height;
    public GameObject[] coins;
    public GameObject rightHand;
    public GameObject leftHand;
    public GameObject rightFoot;
    public GameObject leftFoot;
    public Transform cam;
    public GameObject pickUpParent;
    private int count = 0;
    public TextMeshProUGUI[] countTexts;
    public GameObject[] winTextObjects;
    public string userId = "test3";
    StreamWriter writer;

    void Start()
    {
        string path = $"Assets/Resources/{userId}.txt";
        writer = new StreamWriter(path, false);
        for (int i = 0; i < winTextObjects.Length; i++)
        {
            winTextObjects[i].SetActive(false);
        }
        
        SetCountText();

    }


    void Update()
    {

        if ( !EditorApplication.isPlaying)
        {
            writer.Close();
        }

        writer.WriteLine($"event: right hand position update; time: {Time.time}, position: {rightHand.transform.position}");
        writer.WriteLine($"event: left hand position update; time: { Time.time}, position: {leftHand.transform.position}");
        writer.WriteLine($"event: right foot position update; time: {Time.time}, position: {rightFoot.transform.position}");
        writer.WriteLine($"event: left foot position update; time: { Time.time}, position: {leftFoot.transform.position}");
        writer.WriteLine($"event: camera position update; time: { Time.time}, position: {cam.position}");

    }

    public void CalibrateHeight()
    {
        height = cam.position.y;
        SetCoinsHeight();
        writer.WriteLine($"event: Game Start {sceneType}; time: {Time.time}");
        writer.WriteLine($"event: Callibrate Height; time: {Time.time}, position: {cam.position}");
    }

    public void SetCoinsHeight()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            if (i % 3 == 0)
            {
                coins[i].transform.position = coins[i].transform.position + new Vector3(0, height - coins[i].transform.position.y);
            }
            if (i % 3 == 1)
            {
                coins[i].transform.position = coins[i].transform.position + new Vector3(0, (float)(height - coins[i].transform.position.y - (0.4 * height)));
            }
            if (i % 3 == 2)
            {
                coins[i].transform.position = coins[i].transform.position + new Vector3(0, (float)(height - coins[i].transform.position.y + (0.3 * height)));
            }
        }
        pickUpParent.SetActive(true);
    }


    public void IncrementCount(Collider coin)
    {
        count++;
        SetCountText();
        writer.WriteLine($"event: coin collected; time: { Time.time}, position: { coin.transform.position}");
    }

    void SetCountText()
    {
        for (int i = 0; i < countTexts.Length; ++i)
        {
            countTexts[i].text = "Count: " + count.ToString();
        }


        if (count >= 8)
        {
            for (int i = 0; i < winTextObjects.Length; i++)
            {
                winTextObjects[i].SetActive(false);
                writer.WriteLine($"event: Game end; time: {Time.time}");
                writer.Close();
            }
        }
    }
}
