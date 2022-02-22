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

    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mLevelLoadManager = mGameManager.gameObject.GetComponent<LevelLoadManager>();
        _uiSpinButton.onClick.AddListener(()=>
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
                _elementsName.Add(reel);
                ResultChecker(); 
            });
            reel.mdisableRoll = false;
        }
        for (int i = 0; i < _reels.Length; i++) 
        {
            yield return new WaitForSeconds(Random.Range(1f,2.5f));
            if(i==2)
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
            if(_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name && _elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
            {
                switch (_elementsName[i]._slotElementGameObject.name)
                {
                    case "TradingCards":
                        // _rewardText.text = "Trading Card Pack";
                        _rewardPanel.SetTitle("LUCKY !");
                        _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                        _rewardPanel.ShowResultTotal(0, "1");

                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "FreeSpins":
                        spin += 5;
                        _rewardPanel.SetTitle("IN LUCK !");
                        _rewardPanel.ShowMultiplierDetails(2, 0, "No.of spins", spin.ToString());
                        if (spin == 0)
                        {
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        }
                        break;
                    case "Coins":
                        coinShower.SetActive(true);

                        if (GameManager.Instance._MultiplierValue <= 1)
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowResultTotal(0, "5000");
                            mGameManager._coins += 5000;
                        }
                        else
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowMultiplierDetails(1, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
                            _rewardPanel.ShowResultTotal(0, (5000 * GameManager.Instance._MultiplierValue).ToString());
                            mGameManager._coins += 5000* GameManager.Instance._MultiplierValue;
                        }
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "Energy":
                        energyShower.SetActive(true);
                        if (GameManager.Instance._MultiplierValue <= 1)
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowResultTotal(0, "10");
                            mGameManager._energy += 10;
                        }
                        else
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowMultiplierDetails(1, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
                            _rewardPanel.ShowResultTotal(0, (10 * GameManager.Instance._MultiplierValue).ToString());
                            mGameManager._energy += 10 * GameManager.Instance._MultiplierValue;
                        }
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    default:
                        break;
                }
            }
            else if(_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name || _elementsName[i]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
            {
                switch (_elementsName[i]._slotElementGameObject.name)
                {
                    case "TradingCards":
                        //_rewardText.text = "Oops Try Again";
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "FreeSpins":
                        spin += 3;
                        _rewardPanel.SetTitle("IN LUCK !");
                        _rewardPanel.ShowMultiplierDetails(2, 0, "No.of spins", spin.ToString());
                        if (spin == 0)
                        {
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        }
                        break;
                    case "Coins":
                        if(_elementsName[i]._slotElementGameObject.name == _elementsName[i + 1]._slotElementGameObject.name)
                        {
                            _reels[i].coinParticle.SetActive(true);
                            _reels[i+1].coinParticle.SetActive(true);
                        }else if(_elementsName[i]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i].coinParticle.SetActive(true);
                            _reels[i + 2].coinParticle.SetActive(true);
                        }


                        if (GameManager.Instance._MultiplierValue <= 1)
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowResultTotal(0, "3000");
                            mGameManager._coins += 3000;
                        }
                        else
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowMultiplierDetails(1, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
                            _rewardPanel.ShowResultTotal(0, (3000 * GameManager.Instance._MultiplierValue).ToString());
                            mGameManager._coins += 3000 * GameManager.Instance._MultiplierValue;
                        }
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
                        if (GameManager.Instance._MultiplierValue <= 1)
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowResultTotal(0, "5");
                            mGameManager._energy += 5;
                        }
                        else
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowMultiplierDetails(1, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
                            _rewardPanel.ShowResultTotal(0, (5 * GameManager.Instance._MultiplierValue).ToString());
                            mGameManager._energy += 5 * GameManager.Instance._MultiplierValue;
                        }
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    default:
                        break;
                }
            }
            else if( _elementsName[i + 1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
            {
                switch (_elementsName[i + 1]._slotElementGameObject.name)
                {
                    case "TradingCards":
                        //_rewardText.text = "Oops Try Again";
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "FreeSpins":                       
                        spin += 3;
                        _rewardPanel.SetTitle("IN LUCK !");
                        _rewardPanel.ShowMultiplierDetails(2, 0, "No.of spins", spin.ToString());
                        if (spin == 0)
                        {
                            Invoke(nameof(RewardPanelInvoke), 2f);
                        }
                        break;
                    case "Coins":
                        if (_elementsName[i+1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i+1].coinParticle.SetActive(true);
                            _reels[i + 1].coinParticle.SetActive(true);
                        }
                        else if (_elementsName[i+1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i+1].coinParticle.SetActive(true);
                            _reels[i + 2].coinParticle.SetActive(true);
                        }

                        if (GameManager.Instance._MultiplierValue <= 1)
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowResultTotal(0, "3000");
                            mGameManager._coins += 3000;
                        }
                        else
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowMultiplierDetails(1, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
                            _rewardPanel.ShowResultTotal(0, (3000 * GameManager.Instance._MultiplierValue).ToString());
                            mGameManager._coins += 3000 * GameManager.Instance._MultiplierValue;
                        }
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    case "Energy":
                        if (_elementsName[i+1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i+1].energyPaticle.SetActive(true);
                            _reels[i + 2].energyPaticle.SetActive(true);
                        }
                        else if (_elementsName[i+1]._slotElementGameObject.name == _elementsName[i + 2]._slotElementGameObject.name)
                        {
                            _reels[i+1].energyPaticle.SetActive(true);
                            _reels[i + 2].energyPaticle.SetActive(true);
                        }
                        if (GameManager.Instance._MultiplierValue <= 1)
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowResultTotal(0, "5");
                            mGameManager._energy += 5;
                        }
                        else
                        {
                            _rewardPanel.ShowMultiplierDetails(0, 0, "Cucu Bonus", GameManager.Instance.cucuMultiplier.ToString());
                            _rewardPanel.ShowMultiplierDetails(1, 0, "Multiplier", GameManager.Instance._MultiplierValue.ToString());
                            _rewardPanel.ShowResultTotal(0, (5 * GameManager.Instance._MultiplierValue).ToString());
                            mGameManager._energy += 5 * GameManager.Instance._MultiplierValue;
                        }
                        Invoke(nameof(RewardPanelInvoke), 2f);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //_rewardText.text = "Oopsie Nothing is Identical";
                Invoke(nameof(RewardPanelInvoke), 2f);
            }
        }
        
    }

    public void RewardPanelInvoke()
    {
        if (spin == 0 && _reels[2].mSpinOver == true)
        {
            _uiSpinButton.interactable = false;
            _rewardPanel.gameObject.SetActive(true);
        }
    }
    public void ActiveLevelInvoke()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}
