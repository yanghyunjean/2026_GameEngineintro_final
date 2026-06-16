using UnityEngine;



public class ItemObject : MonoBehaviour
{
    public ItemData itemData;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sr.sprite = itemData.icon;
    }
}