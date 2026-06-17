using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    private const string TUTORIAL_KEY = "TUTORIAL";

    private void Start()
    {


        int isTutorialFinished = PlayerPrefs.GetInt(TUTORIAL_KEY, 0);

        if (isTutorialFinished == 0)
        {
            Debug.Log("튜토리얼 시작");
        }
        else
        {
            Debug.Log("튜토리얼 스킵");
        }
    }

    public void FinishTutorial()
    {
        PlayerPrefs.SetInt(TUTORIAL_KEY, 1);
        PlayerPrefs.Save();
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