using UnityEngine.UI;
using UnityEngine;

public class HackScript : MonoBehaviour
{
    [SerializeField] Button attackButton;
    public void AddEnergy(int energy)
    {
        GameManager.Instance._energy += energy;
    }
    public void AddCoin(int coin)
    {
        GameManager.Instance._coins += coin;
    }
    public void AttackScene()
    {
        attackButton.interactable = false;
        MultiplayerManager.Instance.OnGettingAttackCard();
    }
}
