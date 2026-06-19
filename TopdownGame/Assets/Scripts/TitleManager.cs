using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject shopPanel;

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
        if (GameDataManager.Instance.saveData.money < 5)
        {
            Debug.Log("골드 부족");
            return;
        }

        GameDataManager.Instance.saveData.money -= 5;
        GameDataManager.Instance.saveData.healItemCount++;

        GameDataManager.Instance.SaveJsonData();

        Debug.Log("회복약 구매");
    }

    // =====================
    // 속도 아이템 구매
    // =====================

    public void BuySpeedItem()
    {
        if (GameDataManager.Instance.saveData.money < 8)
        {
            Debug.Log("골드 부족");
            return;
        }

        GameDataManager.Instance.saveData.money -= 8;
        GameDataManager.Instance.saveData.speedItemCount++;

        GameDataManager.Instance.SaveJsonData();

        Debug.Log("속도 아이템 구매");
    }

    // =====================
    // 무적 아이템 구매
    // =====================

    public void BuyInvincibleItem()
    {
        if (GameDataManager.Instance.saveData.money < 10)
        {
            Debug.Log("골드 부족");
            return;
        }

        GameDataManager.Instance.saveData.money -= 10;
        GameDataManager.Instance.saveData.invincibleItemCount++;

        GameDataManager.Instance.SaveJsonData();

        Debug.Log("무적 아이템 구매");
    }
}