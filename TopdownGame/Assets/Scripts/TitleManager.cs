using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject shopPanel;
    public GameObject recordPanel;
    

    public TMP_Text notificationText;

    public TMP_Text bestTimeText;
    public TMP_Text goldText;
    public TMP_Text totalPlayText;
    public TMP_Text bestScoreText;

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

        shopPanel.SetActive(false);
        recordPanel.SetActive(false);
    }

    // =====================
    // 게임 시작
    // =====================

    public void StartGame()
    {
        GameDataManager.Instance.ResetRunData();

        SceneManager.LoadScene("MainScene");
    }

    // =====================
    // 게임 종료
    // =====================

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // =====================
    // 튜토리얼
    // =====================

    public void FinishTutorial()
    {
        tutorialPanel.SetActive(false);

        GameDataManager.Instance.isTutorialFinished = 1;
        GameDataManager.Instance.SavePlayerPrefs();
    }

    // =====================
    // 상점 열기/닫기
    // =====================

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    // =====================
    // 회복약 구매
    // =====================

    public void BuyHealItem()
    {
        if (GameDataManager.Instance.saveData.money < 2)
        {
            notificationText.color = Color.red;
            ShowMessage("골드가 부족합니다.");
            return;
        }

        GameDataManager.Instance.saveData.money -= 2;
        GameDataManager.Instance.saveData.healItemCount++;

        GameDataManager.Instance.SaveJsonData();
        notificationText.color = Color.green;
        ShowMessage("회복약 구매 완료!");
    }

    // =====================
    // 속도 아이템 구매
    // =====================

    public void BuySpeedItem()
    {
        if (GameDataManager.Instance.saveData.money <5)
        {
            notificationText.color = Color.red;
            ShowMessage("골드가 부족합니다.");
            return;
        }

        GameDataManager.Instance.saveData.money -=5;
        GameDataManager.Instance.saveData.speedItemCount++;

        GameDataManager.Instance.SaveJsonData();
        notificationText.color = Color.green;
        ShowMessage("속도 아이템 구매 완료!");
    }

    // =====================
    // 무적 아이템 구매
    // =====================

    public void BuyInvincibleItem()
    {
        if (GameDataManager.Instance.saveData.money < 10)
        {
            notificationText.color = Color.red;
            ShowMessage("골드가 부족합니다.");
            return;
        }

        GameDataManager.Instance.saveData.money -= 10;
        GameDataManager.Instance.saveData.invincibleItemCount++;

        GameDataManager.Instance.SaveJsonData();

        notificationText.color = Color.green;
        ShowMessage("무적 아이템 구매 완료!");
    }
    // =====================
    // 기록 열기
    // =====================

    public void OpenRecord()
    {
        SaveData data =
            GameDataManager.Instance.saveData;

        bestTimeText.text =
            $"최고 생존 시간 : {Mathf.FloorToInt(data.bestTime)}초";

        goldText.text =
            $"보유 골드 : {data.money}";

        totalPlayText.text =
            $"총 플레이 횟수 : {data.totalPlay}회";

        bestScoreText.text =
            $"최고 점수 : {data.bestScore}";

        recordPanel.SetActive(true);
    }
    // =====================

    public void ShowMessage(string message)
    {
        StartCoroutine(MessageRoutine(message));
    }

    private IEnumerator MessageRoutine(string message)
    {
        notificationText.gameObject.SetActive(true);
        notificationText.text = message;

        yield return new WaitForSeconds(2f);

        notificationText.text = "";
        notificationText.gameObject.SetActive(false);
    }

    public void ResetData()
    {
        GameDataManager.Instance.DeleteJsonData();

        PlayerPrefs.DeleteAll();

        Debug.Log("모든 데이터 초기화 완료");
    }

    // =====================
    // 기록 닫기
    // =====================
    public void CloseRecord()
    {
        recordPanel.SetActive(false);
    }
}