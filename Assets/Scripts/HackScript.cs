using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackScript : MonoBehaviour
{
    public void AddEnergy(int energy)
    {
        GameManager.Instance._energy += energy;
    }
    public void AddCoin(int coin)
    {
        GameManager.Instance._coins += coin;
    }
}
