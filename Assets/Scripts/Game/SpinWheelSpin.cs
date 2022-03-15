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
    public int FreeSpins = 1;
    public int cardCount = 0;
    public int totalFreeSpin;

    public bool DoFreeSpins = false;
    public bool disablePanel = false;
    bool onSpin;

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
            spinWheel.OnSpinEnd(wheelPiece =>
            {
                onSpin = false;
                endParticle.SetActive(true);
                if (FreeSpins == 0)
                {
                    DoFreeSpins = false;
                }

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
                if (wheelPiece._Icon == _uiCardSprite)
                {

                    chestAnimation.SetBool("Open", true);
                    Invoke(nameof(ShowResultPopUP), 1f);
                    cardCount++;
                    int SetsNumber = Random.Range(0, GameManager.Instance.UnLockedListAndIndex.Count);
                    int StickerIndex = Random.Range(0, 9);
                    GameManager.Instance.UnLockedListAndIndex[SetsNumber].StickerIndex.Add(StickerIndex);
                }

            });
            if (FreeSpins <= 0 && onSpin) return;
            onSpin = true;

            CoinParticle.SetActive(false);
            EnergyParticle.SetActive(false);
            chestAnimation.SetBool("Open", false);

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
        Invoke(nameof(ShowResultPopUP), 1f);
    }

    void ShowResultPopUP()
    {
        if (FreeSpins > 0)
        {
            chestAnimation.SetBool("Open", false);
            CoinParticle.SetActive(false);
            EnergyParticle.SetActive(false);
            endParticle.SetActive(false);
        }
        else
        {
            _uiReturnToGame.gameObject.SetActive(true);

            _uiReturnToGame.ShowMultiplierDetails(0, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
            _uiReturnToGame.ShowMultiplierDetails(1, 1, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());

            List<int[]> result = CheckTheResult(coin, Energy, cardCount);
            List<int> iconIndex = new List<int>();
            List<string> value = new List<string>();
            foreach (var item in result)
            {
                iconIndex.Add(item[0]);
                if (item[0] != 2)
                    value.Add(Mathf.Round(item[1] * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier).ToString("F0"));
                else
                    value.Add(Mathf.Round(item[1]).ToString("F0"));
            }
            _uiReturnToGame.ShowResultTotal(iconIndex.ToArray(), value.ToArray());
            if (totalFreeSpin != 0)
                _uiReturnToGame.ShowMultiplierDetails(2, 2, "No. of spins", totalFreeSpin.ToString());

            disablePanel = DoFreeSpins;
        }

    }

    private void Update()
    {
        if (FreeSpins < 10)
        {
            _uiFreeSpinCountValue.text = FreeSpins.ToString();
        }
        else
        {
            _uiFreeSpinCountValue.text = FreeSpins.ToString();
        }

        if (disablePanel)
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
