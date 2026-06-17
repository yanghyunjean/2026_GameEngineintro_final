using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text resultScoreText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // =========================
    // 게임 시작
    // =========================
    public void StartGame()
    {
        if (GameDataManager.Instance != null)
        {
            GameDataManager.Instance.ResetRunData();
        }

        SceneManager.LoadScene("MainScene");
    }

    // =========================
    // 게임 오버
    // =========================
    public void GameOver()
    {
        Debug.Log("GameOver");

        if (GameDataManager.Instance == null)
        {
            Debug.LogError("GameDataManager가 없습니다!");
            return;
        }

        // 결과 저장 + 보상 계산
        GameDataManager.Instance.CalculateReward();
        GameDataManager.Instance.SaveGameResult();

        // UI 표시
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (resultScoreText != null)
        {
            resultScoreText.text =
                "Final Score : " + GameDataManager.Instance.score;
        }

        StartCoroutine(GameOverRoutine());
    }

    // =========================
    // 게임 오버 딜레이 후 타이틀 이동
    // =========================
    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(3f);
        GoTitle();
    }

    // =========================
    // 타이틀 이동
    // =========================
    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}