using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.IO;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    public TMP_Text text;
    public Image circle;

    public TMP_Text winnerText;
    public GameObject gameOverPanel;

    public bool victory = false;

    private float totalTime = 300.0f;
    private float timer = 0f;
    private float seconds = 0f;
    private float minutes = 0f;

    private int aZoneCounter = 0;
    private int bZoneCounter = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = totalTime;
        victory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.1f)
        {
            timer -= Time.deltaTime;
            minutes = Mathf.Floor(timer / 60f);
            seconds = Mathf.RoundToInt(timer % 60f);

            text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (timer <= 0.1f)
        {
            victory = true;

            if (!gameOverPanel.activeSelf)
            {
                foreach (var item in ResourceManager.instance.allZones)
                {
                    if (item == 1)
                    {
                        aZoneCounter++;
                    }
                    if (item == 2)
                    {
                        bZoneCounter++;
                    }
                }
            }

            gameOverPanel.SetActive(true);

            if (aZoneCounter > bZoneCounter)
            {
                winnerText.text = "You Are The Winner!";
            }
            if (bZoneCounter > aZoneCounter)
            {
                winnerText.text = "This Was Not Your Battle...You Lose.";
            }
            if (bZoneCounter == aZoneCounter)
            {
                winnerText.text = "Draw.";
            }
        }
    }
}
