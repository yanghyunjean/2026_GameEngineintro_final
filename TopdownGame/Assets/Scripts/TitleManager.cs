using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    private void Start()
    {
        if (GameDataManager.Instance.isTutorialFinished == 0)
        {
            tutorialPanel.SetActive(true);
        }
        else
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);

        GameDataManager.Instance.isTutorialFinished = 1;
        GameDataManager.Instance.SavePlayerPrefs();
    }

    // 게임 시작
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 게임 종료
    public void ExitGame()
    {
        Debug.Log("게임 종료");

        Application.Quit();
    }
}