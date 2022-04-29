using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // using SerializeField the var will not be shown in the inspector but it will still being public
    [SerializeField] public static int score;
    [SerializeField] public static int lives = 3;

    public GameObject ballPrefab;

    private float currentTime = 2;
    private int spawnTime = 2;
    private SpriteRenderer backgroundColor;

    void Start()
    {
        ChangeBackgroundColor();
    }

    void ChangeBackgroundColor()
    {
        backgroundColor = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        backgroundColor.color = ButtonsController.backgroundColorSelected;
    }

    public void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, new Vector3(Random.Range(-8, 8), 6, 0), Quaternion.identity);
        ball.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }

    void Update()
    {
        Timer();
        GameOver();
    }

    void Timer()
    {
        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            currentTime = spawnTime;
            SpawnBall();
        }
    }

    void GameOver()
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
