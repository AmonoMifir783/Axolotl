using UnityEngine;
using System.Collections.Generic;

public class Drop : MonoBehaviour
{
    public ItemsBase itemsBase;
    public float dropForce = 5f;
    public float dropRadius = 2f;

    public void IdentificationDrop(GameObject targetObject)
    {
        // 1. Понять тэг targetObject
        ItemTag targetTag = GetItemTagFromObject(targetObject);
        Debug.Log("тэг - " + targetTag);

        // 2. Найти соответствующий ItemInform в базе
        ItemInform itemInfo = FindItemInfoByTag(targetTag);
        Debug.Log("тэг из базы - " + targetTag);
        // 3. Проверить можно ли объект собрать (Transform == true)
        if (!itemInfo.Transform)
        {
            Debug.Log($"Объект {targetTag} нельзя трансформировать (Transform = false)");
            return;
        }

        // 4. Получить позицию targetObject
        Vector3 dropPosition = targetObject.transform.position;

        // 5. Выбросить все префабы соответствующие тегам из массива DropedTag
        DropItemsFromPosition(dropPosition, itemInfo.DropedTag);
    }

    private ItemTag GetItemTagFromObject(GameObject obj)
    {
        foreach (ItemTag tag in System.Enum.GetValues(typeof(ItemTag)))
        {
            if (obj.CompareTag(tag.ToString()))
            {
                return tag;
            }
        }

        Debug.LogWarning($"Не удалось определить тег для объекта: {obj.name}");
        return ItemTag.Stone; // Тег по умолчанию
    }

    private ItemInform FindItemInfoByTag(ItemTag tag)
    {
        if (itemsBase == null || itemsBase.Items == null)
        {
            Debug.LogError("ItemsBase не назначен или пустой!");
            return null;
        }

        return itemsBase.Items.Find(item => item.tag == tag);
    }

    private void DropItemsFromPosition(Vector3 position, ItemTag[] tagsToDrop)
    {
        if (tagsToDrop == null || tagsToDrop.Length == 0)
        {
            Debug.LogWarning("Массив DropedTag пустой!");
            return;
        }

        foreach (ItemTag dropTag in tagsToDrop)
        {
            DropSingleItem(position, dropTag);
        }

        Debug.Log($"Выброшено {tagsToDrop.Length} предметов из позиции: {position}");
    }

    private void DropSingleItem(Vector3 position, ItemTag tag)
    {
        // Найти префаб для тега
        ItemInform dropItemInfo = FindItemInfoByTag(tag);
        if (dropItemInfo == null || dropItemInfo.prefabItem == null)
        {
            Debug.LogWarning($"Префаб для тега {tag} не найден!");
            return;
        }

        // Создать случайную позицию в радиусе
        Vector3 dropPosition = GetRandomDropPosition(position);
        
        // Создать объект
        GameObject droppedObject = Instantiate(dropItemInfo.prefabItem, dropPosition, Quaternion.identity);
        
        // Настроить физику
        SetupPhysics(droppedObject);

        Debug.Log($"Выброшен: {tag}");
    }

    private Vector3 GetRandomDropPosition(Vector3 centerPosition)
    {
        Vector2 randomCircle = Random.insideUnitCircle * dropRadius;
        return centerPosition + new Vector3(randomCircle.x, 0.5f, randomCircle.y);
    }

    private void SetupPhysics(GameObject obj)
    {
        // Добавить Rigidbody
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }

        // Применить случайную силу
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0.3f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        rb.AddForce(randomDirection * dropForce, ForceMode.Impulse);

        // Добавить случайное вращение
        rb.AddTorque(new Vector3(
            Random.Range(-0.1f, 0f),
            Random.Range(-0.1f, 0f),
            Random.Range(-0.1f, 0f)
        ), ForceMode.Impulse);

        // Добавить коллайдер если нет
        if (obj.GetComponent<Collider>() == null)
        {
            obj.AddComponent<BoxCollider>();
        }
    }

    // Визуализация в редакторе
    private void OnDrawGizmosSelected()
    {
        if (enabled)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, dropRadius);
        }
    }
}

// Вспомогательный компонент для хранения тега на GameObject
public class ItemTagComponent : MonoBehaviour
{
    public ItemTag itemTag;
}