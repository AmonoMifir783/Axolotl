using UnityEngine;

public class CharacterParams : MonoBehaviour
{
    public int HP;
    public int ST;
    public int HU;

    public int MaxHP;
    public int MaxST;
    public int MaxHU;

    public void ParamChange(int HPChange = 0, int STChange = 0, int HUChange = 0) 
    {
        if(HP + HPChange >= MaxHP) 
        {   
            HP = MaxHP;
        }
        else if (HP + HPChange <= 0) 
        {
            HP = 0;
        }
        else
        {
            HP += HPChange;
        }




        if(ST + STChange >= MaxST) 
        {   
            ST = MaxST;
        }
        else if (ST + STChange <= 0)
        {
            ST = 0;
        }
        else
        {
            ST += STChange;
        }




        if(HU + HUChange >= MaxHU) 
        {   
            HU = MaxHU;
        }
        else if (HU + HUChange <= 0)
        {
            HU = 0;
        }
        else 
        {
            HU += HUChange;  
        }
    }
}
