using UnityEngine;

public class PlayerMetal : MonoBehaviour
{
    public float curretMetal, maxMetal;
    public float metalToRestore, metalToUse;

    public void ChargeMetal()
    {
        if(curretMetal + metalToRestore > maxMetal)
        {
            curretMetal = maxMetal;
        }
        else
        {
            curretMetal += metalToRestore;
        }
    }

    public bool UseMetal()
    {
        if(curretMetal - metalToUse < 0)
        {
            return false;
        }
        else
        {
            curretMetal -= metalToUse;
            return true;
        }
    }
}
