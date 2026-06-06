using UnityEngine;

public class CursorController : MonoBehaviour
{
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void unCursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    public void ConfinedCursorLock()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
