using UnityEngine;

public class ItemsActions : MonoBehaviour
{
    public CharacterParams characterParams;
    public ItemsBase itemsBase;
    
    public void DestroyObject(GameObject _object)
    {
        Destroy(_object);
    }
    
    public bool AplicationObject(string _objectTag)
    {
        if (_objectTag == "Seed")
        {
            ItemInform seedInfo = GetItemInform(ItemTag.Seed);
            if (seedInfo != null && HasParentClass(seedInfo, ParentClass.Edible))
            {
                changeCharacterParams((int)seedInfo.hp, (int)seedInfo.st, (int)seedInfo.hu);
                Debug.Log(seedInfo.hu);
                return true;
            }
        }
        
        return false;
    }

    private void changeCharacterParams(int HPChange = 0, int STChange = 0, int HUChange = 0)
    {
        characterParams.ParamChange(HPChange, STChange, HUChange);
    }
    
    // Поиск ItemInform по enum тегу
    private ItemInform GetItemInform(ItemTag tag)
    {
        if (itemsBase == null) return null;
        return itemsBase.Items.Find(item => item.tag == tag);
    }

    // Проверка, принадлежит ли предмет указанному родительскому классу
    private bool HasParentClass(ItemInform info, ParentClass parent)
    {
        if (info.parentClasses == null) return false;
        foreach (ParentClass pc in info.parentClasses)
            if (pc == parent) return true;
        return false;
    }
}
