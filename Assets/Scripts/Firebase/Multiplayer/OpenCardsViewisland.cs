using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCardsViewisland : MonoBehaviour
{
   
    public void ViewIsland()
    {
        MultiplayerManager.Instance.OnClickViewIslandToOpenCard();
    }
}
