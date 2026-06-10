using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class TimeManager : MonoBehaviour
{
    public float timeLimit = 120f; 
    public TMP_Text timerText;

    private float currentTime;
    private bool isGameOver = false;

    void Start()
    {
        currentTime = timeLimit;
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("퇴근까지 남은 시간 : {0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        isGameOver = true;

        Debug.Log("시간 종료");

        SceneManager.LoadScene("ReadyScene");
    }
}