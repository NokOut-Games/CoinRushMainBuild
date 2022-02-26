using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SpinWheelSpin : MonoBehaviour
{
    public Button _uiSpinButton;
    public ResultPanelUI _uiReturnToGame;
    public TextMeshProUGUI _uiFreeSpinCountValue;

    public Sprite _uiCoinSprite;
    public Sprite _uiFreeSpinSprite;
    public Sprite _uiEnergySprite;
    public Sprite _uiCardSprite;

    public int coin = 0;
    public int Energy = 0;
    public int FreeSpins = 0;
    public int cardCount = 0;
    public int totalFreeSpin;

    public bool DoFreeSpins = false;
    public bool disablePanel = false;

    [SerializeField] private SpinWheel spinWheel;
    private GameManager mGameManager;

    [SerializeField] GameObject CoinParticle;
    [SerializeField] GameObject EnergyParticle;
    [SerializeField] Animator chestAnimation;

    [SerializeField] GameObject endParticle;
    private void Start()
    {
         mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _uiSpinButton.onClick.AddListener(() =>
        {
            CoinParticle.SetActive(false);
            EnergyParticle.SetActive(false);
            _uiSpinButton.interactable = false;

            spinWheel.OnSpinEnd(wheelPiece =>
            {
                endParticle.SetActive(true);
                if (FreeSpins == 0)
                {
                    DoFreeSpins = false;
                }
                
                switch (DoFreeSpins)
                {
                    case false:
                        if (wheelPiece._Icon == _uiCoinSprite)
                        {
                            chestAnimation.SetBool("Open", true);
                            Invoke(nameof(PlayCoinParticle), .2f);
                           
                            if (GameManager.Instance._MultiplierValue <= 1)
                            {
                                coin += wheelPiece._Amount;
                                mGameManager._coins += wheelPiece._Amount;
                            }
                            else
                            {
                                coin += wheelPiece._Amount*GameManager.Instance._MultiplierValue;
                                mGameManager._coins += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                            }

                        }else
                        if (wheelPiece._Icon == _uiEnergySprite)
                        {
                            chestAnimation.SetBool("Open", true);
                            Invoke(nameof(PlayEnergyParticle), .2f);

                            if (GameManager.Instance._MultiplierValue <= 1)
                            {
                                Energy += wheelPiece._Amount;
                                mGameManager._energy += wheelPiece._Amount;
                            }
                            else
                            {
                                Energy += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                                mGameManager._energy += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                            }
                        }else
                        if (wheelPiece._Icon == _uiFreeSpinSprite)
                        {
                            FreeSpins += wheelPiece._Amount;
                            totalFreeSpin += wheelPiece._Amount;
                        }
                        else if (wheelPiece._Icon == _uiCardSprite)
                        {
                            cardCount++;
                        }
                        break;


                    case true:
                        _uiSpinButton.interactable = true;
                        if (wheelPiece._Icon == _uiCoinSprite)
                        {
                            disablePanel = true;
                            chestAnimation.SetBool("Open", true);
                            Invoke(nameof(PlayCoinParticle), .2f);

                            if (GameManager.Instance._MultiplierValue <= 1)
                            {
                                coin += wheelPiece._Amount;
                                mGameManager._coins += wheelPiece._Amount;
                            }
                            else
                            {
                                coin += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                                mGameManager._coins += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                            }
                        }
                        if (wheelPiece._Icon == _uiEnergySprite)
                        {
                            disablePanel = true;
                            chestAnimation.SetBool("Open", true);
                            Invoke(nameof(PlayEnergyParticle), .2f);

                            if (GameManager.Instance._MultiplierValue <= 1)
                            {
                                Energy += wheelPiece._Amount;
                                mGameManager._energy += wheelPiece._Amount;
                            }
                            else
                            {
                                Energy += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                                mGameManager._energy += wheelPiece._Amount * GameManager.Instance._MultiplierValue;
                            }
                        }
                        if (wheelPiece._Icon == _uiFreeSpinSprite)
                        {
                            FreeSpins += wheelPiece._Amount;
                            totalFreeSpin += wheelPiece._Amount;
                        }
                        else if (wheelPiece._Icon == _uiCardSprite)
                        {
                            cardCount++;
                        }
                        break;
                }
                _uiSpinButton.interactable = true;
            });
            spinWheel.Spin();

        });
    }
    public void BackToGameScene()
    {
        LevelLoadManager.instance.BacktoHome();
    }


    void PlayCoinParticle()
    {
        CoinParticle.SetActive(true);
        Invoke(nameof(ShowResultPopUP), 1f);

    }
    void PlayEnergyParticle()
    {
        EnergyParticle.SetActive(true);
        Invoke(nameof(ShowResultPopUP),1f);
    }

    void ShowResultPopUP()
    {
        if (FreeSpins > 0)
        {
            chestAnimation.SetBool("Open", false);
            CoinParticle.SetActive(true);
            EnergyParticle.SetActive(true);
            endParticle.SetActive(true);
        }
        else
        {
            _uiReturnToGame.ShowMultiplierDetails(0, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
            _uiReturnToGame.ShowMultiplierDetails(1, 1, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());

            List<int[]> result = CheckTheResult(coin, Energy, cardCount);
            List<int> iconIndex = new List<int>();
            List<string> value = new List<string>();
            foreach (var item in result)
            {
                iconIndex.Add(item[0]);
                if (item[0] != 2)
                    value.Add((item[1] * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier).ToString());
                else
                    value.Add((item[1]).ToString());
            }        
            _uiReturnToGame.ShowResultTotal(iconIndex.ToArray(), value.ToArray());
            if (totalFreeSpin !=0)
                _uiReturnToGame.ShowMultiplierDetails(2, 2, "No. of spins", totalFreeSpin.ToString());
            _uiReturnToGame.gameObject.SetActive(true);

            disablePanel = DoFreeSpins;
        }
       
    }

    private void Update()
    {    if(FreeSpins<10)
        {
            _uiFreeSpinCountValue.text = FreeSpins.ToString();
        }
        else
        {
            _uiFreeSpinCountValue.text = FreeSpins.ToString();
        }
      
        if(disablePanel)
        {
            disablePanel = false;
        }
    }
    List<int[]> CheckTheResult(int inCoin, int inEnergy, int inCard)
    {
       List<int[]> havingElementIndex = new List<int[]>();
        if (inCoin > 0)
        {
            havingElementIndex.Add(new int[] { 0, inCoin });
        }
        if (inEnergy > 0)
        {
            havingElementIndex.Add(new int[] { 1, inEnergy });
        }
        if (inCard > 0)
        {
            havingElementIndex.Add(new int[] { 2, inCard });
        }
        return havingElementIndex;
    }

}
