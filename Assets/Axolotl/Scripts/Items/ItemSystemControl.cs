using UnityEngine;

public class ItemSystemControl : MonoBehaviour
{
    public ItemsActions itemsActions;
    public Object_Identification objectIdentification;
    public SlotActions slotActions;
    public Drop drop;
    public GameObject targetObject;
    public bool checkAdded;
    public CraftsSystem craftsSystem;


    void Update()
    {
        UpdateTargetObject();

        if (Input.GetKeyDown(KeyCode.E) && targetObject != null)
        {
            TransferItemDataToSlotActions(targetObject);

            if (checkAdded)
            {
                objectIdentification.RemoveObject(targetObject);
                itemsActions.DestroyObject(targetObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItemDataToSlotActions();
            ClearItemDataToSlotActions();
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            AplicationItemToSlotActions(slotActions.GetCurrentItem());
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            drop.IdentificationDrop(targetObject);
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if(craftsSystem.CraftItem(slotActions.GetCurrentItem(), targetObject))
            {
                objectIdentification.RemoveObject();
                ClearItemDataToSlotActions();
            }
        }
    }

    private void UpdateTargetObject()
    {
        if (objectIdentification != null && 
            objectIdentification.detectedObjects.Count > 0)
        {
            targetObject = objectIdentification.detectedObjects[0];
        }
        else
        {
            targetObject = null;
        }
    }

    private void TransferItemDataToSlotActions(GameObject obj)
    {
        checkAdded = slotActions.AddItem(obj);
    }

    private void DropItemDataToSlotActions()
    {
        //drop.DropSingleItem(transform.position, drop.GetItemTagFromObject(targetObject));
        slotActions.DropItem();
        ClearItemDataToSlotActions();
    }
    
    private void ClearItemDataToSlotActions()
    {
        slotActions.ClearSlot();
    }

    private void AplicationItemToSlotActions(GameObject _objectTag)
    {
        if(itemsActions.AplicationObject(_objectTag.tag))
        {
            ClearItemDataToSlotActions();
        }
    }
}