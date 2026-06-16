using UnityEngine;

public enum ItemType
{
    Heal,
    Speed,
    Invincible
}

[CreateAssetMenu(menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;

    public Sprite icon;

    public ItemType itemType;

    [Header("Heal")]
    public int healAmount;

    [Header("Buff")]
    public float speedMultiplier = 1.5f;
    public float duration = 10f;

    [Header("Drop")]
    [Range(0, 100)]
    public int dropChance;
}