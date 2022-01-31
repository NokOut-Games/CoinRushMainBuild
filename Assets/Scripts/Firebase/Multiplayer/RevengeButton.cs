using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevengeButton : MonoBehaviour
{
    public string EnemyID;
    // Start is called before the first frame update
    void Start()
    {

    }

   public  void onClickRevenge()
    {
        MultiplayerManager.Instance._enemyPlayerID= EnemyID;
        MultiplayerManager.Instance.isRevenging = true;
        MultiplayerManager.Instance.OnGettingAttackCard();   
    }
}
