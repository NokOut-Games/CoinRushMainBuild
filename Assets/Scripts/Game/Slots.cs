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

    public int[] JackPotReward;
    public int[] twoMatchReward;
    public int[] singleReward;



    [SerializeField] int totalSpins;
    [SerializeField] int totalCoin;
    [SerializeField] int totalEnergy;
    [SerializeField] int totalCard;

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

        _rewardPanel.ShowMultiplierDetails(0, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
        _rewardPanel.ShowMultiplierDetails(1, 1, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());

        List<int[]> result = CheckTheResult(totalCoin, totalEnergy, totalCard);
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
        _rewardPanel.ShowResultTotal(iconIndex.ToArray(), value.ToArray());
        if (totalSpins != 0)
            _rewardPanel.ShowMultiplierDetails(2, 2, "No. of spins", totalSpins.ToString());
        _rewardPanel.gameObject.SetActive(true);
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
