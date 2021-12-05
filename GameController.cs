using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCountroller : MonoBehaviour
{ 
    public static GameCountroller instance;

    public GameObject hudCountainer, gameOverPanel;
    public Text timeCounter;
    public Text countdownText;
    public bool gamePlaying { get; private set; }
    public int countdownTime;

    private float startTime, elapsedTime;
    TimeSpan timePlaying;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        gamePlaying = false;

        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Endgame();
        }

    }

    private void Endgame()
    {
        gamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f);
    }

    /*private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        hudCountainer.SetActive(false);
    }*/

    IEnumerator CountdownToStart()
    {
        while( countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        BeginGame();
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }
}
