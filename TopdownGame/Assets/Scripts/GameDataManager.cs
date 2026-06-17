using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    // ===== 런 데이터 =====
    public int score;
    public int killCount;
    public float survivalTime;

    // ===== 재화 / 성장 =====
    public int gold;
    public int bestScore;

    // ===== 저장 데이터 =====
    public SaveData saveData;

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Application.persistentDataPath + "/saveData.json";

            LoadJsonData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // =========================
    // 점수 추가
    // =========================
    public void AddScore(int amount)
    {
        score += amount;
    }

    // =========================
    // 보상 계산 (게임 종료 시)
    // =========================
    public void CalculateReward()
    {
        int reward =
            (score / 10) +
            (killCount * 5) +
            Mathf.FloorToInt(survivalTime);

        gold += reward;
    }

    // =========================
    // 게임 종료 처리
    // =========================
    public void SaveGameResult()
    {
        if (score > bestScore)
            bestScore = score;

        SaveJsonData();
    }

    // =========================
    // 게임 시작/재시작 초기화
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
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);

        Debug.Log("저장 완료: " + savePath);
    }

    public void LoadJsonData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
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