using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpinWheelSpin : MonoBehaviour
{
    public Button _uiSpinButton;
    public GameObject _uiReturnToGame;
    public TextMeshProUGUI _uiCoinValue;
    public TextMeshProUGUI _uiFreeSpinValue;
    public TextMeshProUGUI _uiEnergyValue;
    public Sprite _uiCoinSprite;
    public Sprite _uiFreeSpinSprite;
    public Sprite _uiEnergySprite;
    public GameObject _uiCoinReward;
    public GameObject _uiEnergyReward;
    public GameObject _uiFreeSpinReward;
    public int coin = 0;
    public int Energy = 0;
    public int FreeSpins = 0;
    public bool DoFreeSpins = false;
    [SerializeField] private SpinWheel spinWheel;
    private GameManager mGameManager;
    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _uiSpinButton.onClick.AddListener(() =>
        {
            _uiSpinButton.interactable = false;

            spinWheel.OnSpinEnd(wheelPiece =>
            {
                if (FreeSpins == 0)
                {
                    DoFreeSpins = false;
                }

                switch (DoFreeSpins)
                {
                    case false:
                        _uiReturnToGame.SetActive(true);
                        if (wheelPiece._Icon == _uiCoinSprite)
                        {
                            Debug.Log(wheelPiece._Icon.name);
                            _uiCoinReward.SetActive(true);
                            coin += wheelPiece._Amount;
                            mGameManager._coins += coin;
                            _uiCoinValue.text = wheelPiece._Amount.ToString();
                        }
                        if (wheelPiece._Icon == _uiEnergySprite)
                        {
                            Debug.Log(wheelPiece._Icon.name);
                            _uiEnergyReward.SetActive(true);
                            Energy += wheelPiece._Amount;
                            mGameManager._energy += Energy;
                            _uiEnergyValue.text = wheelPiece._Amount.ToString();
                        }
                        if (wheelPiece._Icon == _uiFreeSpinSprite)
                        {
                            Debug.Log(wheelPiece._Icon.name);
                            _uiFreeSpinReward.SetActive(true);
                            FreeSpins += wheelPiece._Amount;
                            _uiFreeSpinValue.text = wheelPiece._Amount.ToString();
                            Invoke("BackToSpinWheel", 1.5f);
                        }
                        break;


                    case true:
                        _uiSpinButton.interactable = true;
                        if (wheelPiece._Icon == _uiCoinSprite)
                        {
                            coin += wheelPiece._Amount;
                            mGameManager._coins += coin;
                            _uiCoinValue.text = wheelPiece._Amount.ToString();
                        }
                        if (wheelPiece._Icon == _uiEnergySprite)
                        {
                            Energy += wheelPiece._Amount;
                            mGameManager._energy += Energy;
                            _uiEnergyValue.text = wheelPiece._Amount.ToString();
                        }
                        if (wheelPiece._Icon == _uiFreeSpinSprite)
                        {
                            FreeSpins += wheelPiece._Amount;
                            _uiFreeSpinValue.text = wheelPiece._Amount.ToString();
                        }
                        break;
                }

                _uiSpinButton.interactable = true;
            });
            spinWheel.Spin();

        });
    }
    void BackToSpinWheel()
    {
        _uiReturnToGame.SetActive(false);
        _uiFreeSpinReward.SetActive(false);
    }

    public void BackToGameScene()
    {
        SceneManager.LoadScene("Level1");
    }
}
