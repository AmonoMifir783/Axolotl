using UnityEngine;

public class SistemManager : MonoBehaviour
{
    public CursorController cursorController;

    private void Awake()
    {
        cursorController.ConfinedCursorLock();
        cursorController.CursorLock();

    }
}
