using UnityEngine;
using System;
using UnityEngine.UI;

public class SlotActions : MonoBehaviour
{
    public Slot slot;
    public ItemsBase itemsBase;
    public Transform spawnPoint;
    private GameObject currentItemObject;
    
    public bool AddItem(GameObject itemObject)
    {
        if ((itemObject != null) && (!slot.IsEmpty))
        {
            slot.Tag = itemObject.tag;
            
            slot.IsEmpty = true;
            
            string objectTagString = itemObject.tag;
            if (!System.Enum.TryParse(objectTagString, true, out ItemTag itemEnum))
            {
                Debug.LogError($"Невозможно преобразовать тег '{objectTagString}' в ItemTag");
                return false;
            }
            
            ItemInform info = itemsBase.Items.Find(i => i.tag == itemEnum);
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
            currentItemObject = Instantiate(info.prefabItem, spawnPos, Quaternion.identity);
            currentItemObject.transform.SetParent(transform);
            
            Rigidbody rb = currentItemObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;   // или rb.useGravity = false; rb.isKinematic = true;
                // полное отключение симуляции физики
                rb.detectCollisions = false;
            }
            
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void ClearSlot()
    {
        if (slot.IsEmpty)
        {
            slot.Tag = null;
            slot.IsEmpty = false;
            currentItemObject = null;
        }
    }
    
    public bool DropItem()
    {
        if (!slot.IsEmpty)  // если слот пуст (IsEmpty == false), выходим
        {
            Debug.LogWarning("Слот пуст, нечего выбрасывать");
            return false;
        }

        if (currentItemObject == null) return false;

        currentItemObject.transform.SetParent(null);
        currentItemObject.transform.position = spawnPoint.position;

        Rigidbody rb = currentItemObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
        
        slot.Tag = null;
        slot.IsEmpty = false;   // теперь пуст
        currentItemObject = null;

        return true;
    }
    

    //Дополнительный метод для получения текущего предмета
    public GameObject GetCurrentItem()
    {
        return currentItemObject;
    }

    // // Дополнительный метод для проверки пустоты
    // public bool IsSlotEmpty()
    // {
    //     return slot == null || slot.IsEmpty;
    // }
}