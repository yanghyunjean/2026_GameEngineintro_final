using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    // ===== 현재 플레이 데이터 =====
    public int score;
    public int killCount;
    public float survivalTime;

    // ===== 저장 데이터 =====
    public SaveData saveData;

    private string savePath;

    public int isTutorialFinished;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath =
                Application.persistentDataPath +
                "/saveData.json";

            LoadJsonData();

            LoadPlayerPrefs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadPlayerPrefs()
    {
        isTutorialFinished =
            PlayerPrefs.GetInt("Tutorial", 0);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(
            "Tutorial",
            isTutorialFinished);

        PlayerPrefs.Save();
    }

    // =========================
    // 점수 추가
    // =========================

    public void AddScore(int amount)
    {
        score += amount;
    }

    // =========================
    // 적 처치
    // =========================

    public void AddKill()
    {
        killCount++;
    }

    // =========================
    // 골드 보상
    // =========================

    public void CalculateReward()
    {
        int reward =
            Mathf.FloorToInt(survivalTime / 10f);

        saveData.money += reward;

        Debug.Log("획득 골드 : " + reward);
    }

    // =========================
    // 게임 결과 저장
    // =========================

    public void SaveGameResult()
    {
        CalculateReward();

        if (score > saveData.bestScore)
        {
            saveData.bestScore = score;
        }

        if (survivalTime > saveData.bestTime)
        {
            saveData.bestTime = survivalTime;
        }

        saveData.totalPlay++;

        SaveJsonData();
    }

    // =========================
    // 런 데이터 초기화
    // =========================

    public void ResetRunData()
    {
        score = 0;
        killCount = 0;
        survivalTime = 0f;
    }

    // =========================
    // JSON 저장
    // =========================

    public void SaveJsonData()
    {
        string json =
            JsonUtility.ToJson(saveData, true);

        File.WriteAllText(savePath, json);

        Debug.Log("저장 완료");
    }

    public void LoadJsonData()
    {
        if (File.Exists(savePath))
        {
            string json =
                File.ReadAllText(savePath);

            saveData =
                JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();

            SaveJsonData();
        }
    }

    public void DeleteJsonData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        saveData = new SaveData();

        SaveJsonData();

        Debug.Log("저장 데이터 초기화");
    }
}