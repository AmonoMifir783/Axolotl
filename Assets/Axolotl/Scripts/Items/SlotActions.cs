using UnityEngine;
using UnityEngine.UI;

public class SlotActions : MonoBehaviour
{
    public Slot slot;
    
    public bool AddItem(string itemTag)
    {
        if ((itemTag != null) && (!slot.IsEmpty))
        {
            slot.Tag = itemTag;
            slot.IsEmpty = true;
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
        }
    }

    // Дополнительный метод для получения текущего предмета
    // public ItemInform GetCurrentItem()
    // {
    //     return slot.currentItem;
    // }

    // // Дополнительный метод для проверки пустоты
    // public bool IsSlotEmpty()
    // {
    //     return slot == null || slot.IsEmpty;
    // }
}