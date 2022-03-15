using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Slots : MonoBehaviour
{
    public Reels[] _reels;
    public Button _uiSpinButton;
    public TextMeshProUGUI _SlotDisplayHeadText;

    public ResultPanelUI _rewardPanel;

    public float spin = 1;

    private GameManager mGameManager;
    private LevelLoadManager mLevelLoadManager;

    public List<ReelElement> _elementsName;


    public GameObject coinShower;
    public GameObject energyShower;

    public List<int> JackPotReward= new List<int>();
    public List<int> twoMatchReward = new List<int>();
    public List<int> singleReward = new List<int>();



    [SerializeField] int totalSpins;
    [SerializeField] int totalCoin;
    [SerializeField] int totalEnergy;
    [SerializeField] int totalCard;

    void Awake()
    {
        PopulateProbability();
        AddElementToListFromAndTo(JackPotReward, 0, 4);
        AddElementToListFromAndTo(twoMatchReward, 4, 8);
        AddElementToListFromAndTo(singleReward, 8, 12);
    }
    void PopulateProbability()
    {
        SlotReward value = (SlotReward)RNG.instance.GetRandom(RNG.instance.SlotMachineSceneProbability);
        switch (value)
        {
            case SlotReward.CoinJackPot:
                foreach (var reel in _reels)
                {
                    reel._reelElements[3]._chanceOfObtaining = 94;
                }
                break;
            case SlotReward.EnergyJackPot:
                foreach (var reel in _reels)
                {
                    reel._reelElements[2]._chanceOfObtaining = 94;
                }
                break;
            case SlotReward.FreeSpinJackPot:
                foreach (var reel in _reels)
                {
                    reel._reelElements[1]._chanceOfObtaining = 94;
                }
                break;
            case SlotReward.CardJackPot:
                foreach (var reel in _reels)
                {
                    reel._reelElements[0]._chanceOfObtaining = 94;
                }
                break;
            case SlotReward.CoinMidPot:
                if (Random.Range(1, 100) > 70)
                {
                    _reels[0]._reelElements[3]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[3]._chanceOfObtaining = 4;
                    _reels[2]._reelElements[3]._chanceOfObtaining = 90;
                }
                else if (Random.Range(1, 100) > 30)
                {
                    _reels[0]._reelElements[3]._chanceOfObtaining = 4;
                    _reels[1]._reelElements[3]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[3]._chanceOfObtaining = 90;
                }
                else
                {
                    _reels[0]._reelElements[3]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[3]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[3]._chanceOfObtaining = 4;
                }              
                break;
            case SlotReward.EnergyMidPot:
                if (Random.Range(1, 100) > 70)
                {
                    _reels[0]._reelElements[2]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[2]._chanceOfObtaining = 4;
                    _reels[2]._reelElements[2]._chanceOfObtaining = 90;
                }
                else if (Random.Range(1, 100) > 30)
                {
                    _reels[0]._reelElements[2]._chanceOfObtaining = 4;
                    _reels[1]._reelElements[2]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[2]._chanceOfObtaining = 90;
                }
                else
                {
                    _reels[0]._reelElements[2]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[2]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[2]._chanceOfObtaining = 4;
                }
                break;
            case SlotReward.FreeMidPot:
                if (Random.Range(1, 100) > 70)
                {
                    _reels[0]._reelElements[1]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[1]._chanceOfObtaining = 4;
                    _reels[2]._reelElements[1]._chanceOfObtaining = 90;
                }
                else if (Random.Range(1, 100) > 60)
                {
                    _reels[0]._reelElements[1]._chanceOfObtaining = 4;
                    _reels[1]._reelElements[1]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[1]._chanceOfObtaining = 90;
                }
                else
                {
                    _reels[0]._reelElements[1]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[1]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[1]._chanceOfObtaining = 4;
                }
                break;
            case SlotReward.CardMidPot:
                if (Random.Range(1, 100) > 70)
                {
                    _reels[0]._reelElements[0]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[0]._chanceOfObtaining = 4;
                    _reels[2]._reelElements[0]._chanceOfObtaining = 90;
                }
                else if (Random.Range(1, 100) > 60)
                {
                    _reels[0]._reelElements[0]._chanceOfObtaining = 4;
                    _reels[1]._reelElements[0]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[0]._chanceOfObtaining = 90;
                }
                else
                {
                    _reels[0]._reelElements[0]._chanceOfObtaining = 90;
                    _reels[1]._reelElements[0]._chanceOfObtaining = 90;
                    _reels[2]._reelElements[0]._chanceOfObtaining = 4;
                }
                break;
            case SlotReward.CoinLowPot:
                _reels[0]._reelElements[3]._chanceOfObtaining = 90;
                _reels[1]._reelElements[3]._chanceOfObtaining = 4;
                _reels[2]._reelElements[3]._chanceOfObtaining = 10;
                break;
            case SlotReward.EnergyLowPot:
                _reels[0]._reelElements[2]._chanceOfObtaining = 90;
                _reels[1]._reelElements[2]._chanceOfObtaining = 4;
                _reels[2]._reelElements[2]._chanceOfObtaining = 10;
                break;
            case SlotReward.FreeLowPot:
                 _reels[0]._reelElements[1]._chanceOfObtaining = 90;
                 _reels[1]._reelElements[1]._chanceOfObtaining = 1;
                _reels[2]._reelElements[1]._chanceOfObtaining = 1;
                break;
            case SlotReward.CardLowPot:
                _reels[0]._reelElements[0]._chanceOfObtaining = 90;
                _reels[1]._reelElements[0]._chanceOfObtaining = 1;
                _reels[2]._reelElements[0]._chanceOfObtaining = 1;
                break;
        }
    }
    void AddElementToListFromAndTo(List<int> list,int fromIndex, int toIndex)
    {
        list.Clear();
        for (int i = fromIndex; i < toIndex; i++)
        {
            list.Add(int.Parse(GameManager.Instance.minigameEconomy.SlotReward[i]));
        }
    }
    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mLevelLoadManager = mGameManager.gameObject.GetComponent<LevelLoadManager>();
        _uiSpinButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < _reels.Length; i++)
            {
                _reels[i].mReelsRollerParent.DOLocalMoveY(0, .5f, false);
                _reels[i].mSpinOver = false;
            }
            if (spin > 0)
            {
                spin--;
                _uiSpinButton.interactable = false;
                StartCoroutine(DelayedSpin());
                _elementsName.Clear();
            }
        });
    }

    private void Update()
    {
        _SlotDisplayHeadText.text = spin.ToString();
        if (_reels[2].mSpinOver == true && spin != 0)
        {
            _uiSpinButton.interactable = true;
        }
    }

    private IEnumerator DelayedSpin()
    {
        foreach (Reels reel in _reels)
        {
            reel._roll = true;
            reel.OnReelRollEnd(reel =>
            {
                reel._slotElementGameObject.GetComponent<Animator>().Play("SlotReward");
                _elementsName.Add(reel);
                ResultChecker();
            });
            reel.mdisableRoll = false;
        }
        for (int i = 0; i < _reels.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            if (i == 2)
            {
                yield return new WaitForSeconds(1.5f);
            }
            _reels[i].Spin();
        }
    }

    private void ResultChecker()
    {
        coinShower.SetActive(false);
        energyShower.SetActive(false);
        for (int i = 0; i < _elementsName.Count - 2; i++)
        {
            if (_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name && _elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
            {
                switch (_elementsName[i]._slotElementGameObject.name)
                {
                    case "TradingCards":
                        totalCard += JackPotReward[2];
                        int SetsNumber = Random.Range(0, GameManager.Instance.UnLockedListAndIndex.Count);
                        int StickerIndex = Random.Range(4, 9);
                        GameManager.Instance.UnLockedListAndIndex[SetsNumber].StickerIndex.Add(StickerIndex);
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "FreeSpins":
                        spin += JackPotReward[3];
                        totalSpins += JackPotReward[3];

                        if (spin == 0)
                        {
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        }
                        break;
                    case "Coins":
                        coinShower.SetActive(true);
                        totalCoin += JackPotReward[0];
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "Energy":
                        energyShower.SetActive(true);
                        totalEnergy += JackPotReward[1];
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    default:
                        break;
                }
            }
            else if (_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name || _elementsName[i]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
            {
                switch (_elementsName[i]._slotElementGameObject.name)
                {
                    case "TradingCards":
                        totalCard += twoMatchReward[2];
                        int SetsNumber = Random.Range(0, GameManager.Instance.UnLockedListAndIndex.Count);
                        int StickerIndex = Random.Range(0, 5);
                        GameManager.Instance.UnLockedListAndIndex[SetsNumber].StickerIndex.Add(StickerIndex);
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "FreeSpins":
                        spin += twoMatchReward[3];
                        totalSpins += twoMatchReward[3];
                        if (spin == 0)
                        {
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        }
                        break;
                    case "Coins":
                        if (_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name)
                        {
                            _reels[i].coinParticle.SetActive(true);
                            _reels[i + 1].coinParticle.SetActive(true);
                        }
                        else if (_elementsName[i]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i].coinParticle.SetActive(true);
                            _reels[i + 2].coinParticle.SetActive(true);
                        }
                        totalCoin += twoMatchReward[0];
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "Energy":
                        if (_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name)
                        {
                            _reels[i].energyPaticle.SetActive(true);
                            _reels[i + 1].energyPaticle.SetActive(true);
                        }
                        else if (_elementsName[i]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i].energyPaticle.SetActive(true);
                            _reels[i + 2].energyPaticle.SetActive(true);
                        }
                        totalEnergy += twoMatchReward[1];
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    default:
                        break;
                }
            }
            else if (_elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
            {
                switch (_elementsName[i + 1]._slotElementGameObject.name)
                {
                    case "TradingCards":
                        totalCard += twoMatchReward[2];
                        int SetsNumber = Random.Range(0, GameManager.Instance.UnLockedListAndIndex.Count);
                        int StickerIndex = Random.Range(0, 5);
                        GameManager.Instance.UnLockedListAndIndex[SetsNumber].StickerIndex.Add(StickerIndex);
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "FreeSpins":
                        spin += singleReward[3];
                        totalSpins += singleReward[3];
                        if (spin == 0)
                        {
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        }
                        break;
                    case "Coins":
                        if (_elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i + 1].coinParticle.SetActive(true);
                            _reels[i + 1].coinParticle.SetActive(true);
                        }
                        else if (_elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i + 1].coinParticle.SetActive(true);
                            _reels[i + 2].coinParticle.SetActive(true);
                        }

                        totalCoin += twoMatchReward[0];
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "Energy":
                        if (_elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i + 1].energyPaticle.SetActive(true);
                            _reels[i + 2].energyPaticle.SetActive(true);
                        }
                        else if (_elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i + 1].energyPaticle.SetActive(true);
                            _reels[i + 2].energyPaticle.SetActive(true);
                        }
                        totalEnergy += twoMatchReward[1];
                        if (spin == 0)
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    default:
                        break;
                }
            }
            else
            {


                if (_elementsName[0]._slotElementGameObject.name == "Energy")
                {
                    totalEnergy += singleReward[1];
                }
                else if (_elementsName[0]._slotElementGameObject.name == "Coins")
                {
                    totalCoin += singleReward[0];
                }
                else if (_elementsName[0]._slotElementGameObject.name == "FreeSpins")
                {
                    totalSpins += singleReward[3];
                }
                else if (_elementsName[0]._slotElementGameObject.name == "TradingCards")
                {
                    totalCard += singleReward[2];
                    int SetsNumber = Random.Range(0, GameManager.Instance.UnLockedListAndIndex.Count);
                    int StickerIndex = Random.Range(0, 3);
                    GameManager.Instance.UnLockedListAndIndex[SetsNumber].StickerIndex.Add(StickerIndex);
                }
                if (spin == 0)
                    Invoke(nameof(RewardPanelInvoke), 2f);
            }
        }

    }

    public void RewardPanelInvoke()
    {
        if (spin == 0 && _reels[2].mSpinOver == true)
        {
            _uiSpinButton.interactable = false;
            ShowResultPopUP();
        }
    }
    void ShowResultPopUP()
    {
        GameManager.Instance._coins += totalCoin;
        GameManager.Instance._energy += totalEnergy;
        _rewardPanel.gameObject.SetActive(true);

        _rewardPanel.ShowMultiplierDetails(0, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
        _rewardPanel.ShowMultiplierDetails(1, 1, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());

        List<int[]> result = CheckTheResult(totalCoin, totalEnergy, totalCard);
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
        _rewardPanel.ShowResultTotal(iconIndex.ToArray(), value.ToArray());
        if (totalSpins != 0)
            _rewardPanel.ShowMultiplierDetails(2, 2, "No. of spins", totalSpins.ToString());
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
    public void BackToHome()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}
