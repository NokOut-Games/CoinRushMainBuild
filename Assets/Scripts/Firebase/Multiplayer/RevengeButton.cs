using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevengeButton : MonoBehaviour
{
    public string EnemyID;
    [SerializeField] Button revengeButton;
    // Start is called before the first frame update
    void Start()
    {

    }

   public  void onClickRevenge()
    {
        revengeButton.interactable = false;
        MultiplayerManager.Instance._enemyPlayerID= EnemyID;
        MultiplayerManager.Instance.isRevenging = true;
        MultiplayerManager.Instance.OnGettingAttackCard();   
    }
}
