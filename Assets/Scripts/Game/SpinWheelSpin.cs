using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpinWheelSpin : MonoBehaviour
{
    public Button _uiSpinButton;
    public Text _uiSpinButtonText;
    public GameObject _uiReturnToGame;
    public TextMeshProUGUI _uiCoinValue;
    public TextMeshProUGUI _uiEnergyValue;
    public GameObject _uiCoinReward;
    public GameObject _uiEnergyReward;
    public int coin = 0;
    public int Energy = 0;
    [SerializeField] private SpinWheel spinWheel;
    private GameManager mGameManager;
    private void Start()
    {
        //mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _uiSpinButton.onClick.AddListener(() => {
            _uiSpinButton.interactable = false;
            //_uiSpinButtonText.text = "Spinning";

            spinWheel.OnSpinEnd(wheelPiece => {
                _uiReturnToGame.SetActive(true);

                if (wheelPiece._Label == "Coin")
                {
                    _uiCoinReward.SetActive(true);
                    // coin += wheelPiece._Amount;
                    mGameManager._coins += wheelPiece._Amount;
                    _uiCoinValue.text = wheelPiece._Amount.ToString();
                }
                else
                {
                    _uiEnergyReward.SetActive(true);
                    // Energy += wheelPiece._Amount;
                    mGameManager._energy += wheelPiece._Amount;
                    _uiEnergyValue.text = wheelPiece._Amount.ToString();
                }

                _uiSpinButton.interactable = true;
                //_uiSpinButtonText.text = "Spin" ;
            });
            spinWheel.Spin();

        });
    }
    public void BackToGameScene()
    {
        SceneManager.LoadScene(0);
    }
}
