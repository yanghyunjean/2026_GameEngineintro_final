using UnityEngine;

[CreateAssetMenu(menuName = "Item/Health Item")]
public class HealthItemData : ScriptableObject
{
    public int healAmount = 1;
    public int maxHpBonus = 5;
}