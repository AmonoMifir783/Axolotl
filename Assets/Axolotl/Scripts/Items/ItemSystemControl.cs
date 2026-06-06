using UnityEngine;

public class ItemSystemControl : MonoBehaviour
{
    public ItemsActions itemsActions;
    public Object_Identification objectIdentification;
    public SlotActions slotActions;
    public Drop drop;
    public GameObject targetObject;
    public bool checkAdded;


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
            ClearItemDataToSlotActions();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            drop.IdentificationDrop(targetObject);
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
        checkAdded = slotActions.AddItem(obj.tag);
    }

    private void ClearItemDataToSlotActions()
    {
        slotActions.ClearSlot();
    }
}