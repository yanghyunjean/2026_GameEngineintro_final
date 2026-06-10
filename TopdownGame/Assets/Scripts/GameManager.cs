using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string titleScenename = "TitleScene";
    public string gameScenename = "MainScene";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GameOver()
        {
        GameDataManager.Instance.SaveGameResult();
        GoTitle();
    }
    // Update is called once per frame
    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
      
    }
