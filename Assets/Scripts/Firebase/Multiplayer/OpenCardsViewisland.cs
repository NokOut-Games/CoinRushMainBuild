using UnityEngine.UI;
using UnityEngine;

public class OpenCardsViewisland : MonoBehaviour
{
    [SerializeField] Button mOpenCardButton;
    public void ViewIsland()
    {
        mOpenCardButton.interactable = false;
        MultiplayerManager.Instance.OnClickViewIslandToOpenCard();
    }
}
