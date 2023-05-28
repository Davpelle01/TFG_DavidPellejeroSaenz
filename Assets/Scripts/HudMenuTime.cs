using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudMenuTime : MonoBehaviour
{
    public GameObject player;
    public TMP_Text timerText;
    private float startTime;
    public bool isGreed=false;
    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float elapsedTime = Time.time - startTime;

        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");

        string timeString = minutes + ":" + seconds;

        timerText.SetText(timeString);
        if (timeString == "05:00" && isGreed)
        {
            player.SendMessageUpwards("AddDamage", 3);
        }
    }
}

