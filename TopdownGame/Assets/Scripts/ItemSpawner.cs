using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;

    public ItemData[] itemDatas;

    public float spawnInterval = 15f;

    public Vector2 minPos;
    public Vector2 maxPos;

    private void Start()
    {
        InvokeRepeating(
            nameof(SpawnItem),
            5f,
            spawnInterval);
    }

    private void SpawnItem()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(minPos.x, maxPos.x),
            Random.Range(minPos.y, maxPos.y));

        GameObject item =
            Instantiate(itemPrefab,
            spawnPos,
            Quaternion.identity);

        ItemObject itemObject =
            item.GetComponent<ItemObject>();

        itemObject.itemData = GetRandomItem();
    }

    private ItemData GetRandomItem()
    {
        int totalChance = 0;

        foreach (ItemData item in itemDatas)
        {
            totalChance += item.dropChance;
        }

        int random = Random.Range(0, totalChance);

        int currentChance = 0;

        foreach (ItemData item in itemDatas)
        {
            currentChance += item.dropChance;

            if (random < currentChance)
            {
                return item;
            }
        }

        return itemDatas[0];
    }
}