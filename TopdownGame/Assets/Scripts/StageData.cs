using UnityEngine;

[CreateAssetMenu(menuName = "Stage/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public int enemyCount;
    public float spawnInterval;
}