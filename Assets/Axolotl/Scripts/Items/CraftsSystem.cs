using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CraftsSystem : MonoBehaviour
{
    public ItemsBase itemsBase;
    public CraftBase craftBase;

    public bool CraftItem(GameObject item_1,  GameObject item_2)
    {
        if (item_1 == null || item_2 == null)
        {
            Debug.LogWarning("Один из предметов равен null");
            return false;
        }
        
        ItemTag tag1 = GetItemTagFromObject(item_1);
        ItemTag tag2 = GetItemTagFromObject(item_2);
        
        
        CraftInform recipe = craftBase.Crafts.Find(r => r.tag_1 == tag1 && r.tag_2 == tag2);
        if (recipe == null)
        {
            Debug.Log($"Крафт из {tag1} и {tag2} невозможен (рецепт не найден)");
            return false;
        }
        
        
        ItemInform resultInfo = itemsBase.Items.Find(i => i.tag == recipe.tag_result);
        if (resultInfo == null || resultInfo.prefabItem == null)
        {
            Debug.LogError($"Не найден префаб для результата крафта: {recipe.tag_result}");
            return false;
        }
        
        Vector3 spawnPosition = item_2.transform.position;
        GameObject resultObject = Instantiate(resultInfo.prefabItem, spawnPosition, Quaternion.identity);

        
        // Настраиваем физику для выброшенного предмета (опционально)
        SetupDropPhysics(resultObject);

        // Уничтожаем исходные предметы
        Destroy(item_1);
        Destroy(item_2);

        Debug.Log($"Крафт выполнен: {tag1} + {tag2} -> {recipe.tag_result}");
        return true;
    }
    
    private ItemTag GetItemTagFromObject(GameObject obj)
    {
        foreach (ItemTag tag in Enum.GetValues(typeof(ItemTag)))
        {
            if (obj.CompareTag(tag.ToString()))
                return tag;
        }

        Debug.LogWarning($"Не удалось определить тег для объекта {obj.name}, возвращаем Stone по умолчанию");
        return ItemTag.Stone;
    }
    
    private void SetupDropPhysics(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
            rb = obj.AddComponent<Rigidbody>();

        // Случайная сила для разлёта
        Vector3 randomDirection = new Vector3(
            Random.Range(-0.2f, 0.2f),
            Random.Range(0.1f, 0.2f),
            Random.Range(-0.2f, 0.2f)
        ).normalized;
        rb.AddForce(randomDirection * 5f, ForceMode.Impulse);

        // Случайное вращение
        rb.AddTorque(new Vector3(
            Random.Range(-0.02f, 0.02f),
            Random.Range(-0.02f, 0.02f),
            Random.Range(-0.02f, 0.02f)
        ), ForceMode.Impulse);

        // Добавляем коллайдер, если его нет
        if (obj.GetComponent<Collider>() == null)
            obj.AddComponent<BoxCollider>();
    }
}
