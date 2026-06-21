using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public GameObject[] hearts;

    public void UpdateHeart(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < hp);
        }
    }

}