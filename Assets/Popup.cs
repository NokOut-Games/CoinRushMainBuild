using UnityEngine.UI;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public enum PopUp { Coin, Energy }
    [SerializeField] Sprite[] icons;
    PopUp type;
    [SerializeField] string[] titles;

    [SerializeField] Image popUpicon;
    [SerializeField] TMPro.TMP_Text popUpCoinValue;
    [SerializeField] TMPro.TMP_Text popUpBuyText;
    [SerializeField] TMPro.TMP_Text popUpTitleText;


    public void AwakePopUp(PopUp _type,int value)
    {
        GameManager.Instance._PauseGame=true;
        this.gameObject.SetActive(true);
        popUpicon.sprite = icons[(int)_type];
        popUpTitleText.text =  titles[(int)_type];
        popUpCoinValue.text = value.ConvertToText() + " " + _type.ToString();
        popUpBuyText.text = "$ 200";
    }
    public void ClosePopUp()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance._PauseGame = false;

    }


}
