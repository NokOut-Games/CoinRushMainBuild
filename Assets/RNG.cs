using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RNG : MonoBehaviour
{

    [SerializeField] float PlayTime;
    public int NoOfBackToDeck;
    public int NoOfThreeCardMatch;

    public PlayerBehaviour Behaviour;

    public int MultiplierMaxingCount;
    public int EnergyEmptingCount;

    public static RNG instance;


    public CardProbability[] CardProbability;
    public Probability[] EnergySceneProbability;
    public Probability[] CoinSceneProbability;
    public Probability[] SpinWheelSceneProbability;
    public Probability[] AttackSceneProbability;
    public Probability[] SlotMachineSceneProbability;


    bool MakeMatch=> NoOfBackToDeck > 2;
    bool MakeMissMatch=> NoOfThreeCardMatch > 5;
    string savedPath => Application.persistentDataPath + "/Rng.NokOut";

    bool forOnce;
    private void Awake()
    {
        Load();
        StartCoroutine(CheckPlayerState(1200));

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    private void Update()
    {
        PlayTime = Time.time;
        AllocateToughnessAndIndex(CardProbability);
        AllocateToughnessAndIndex(EnergySceneProbability);
        AllocateToughnessAndIndex(CoinSceneProbability);
        AllocateToughnessAndIndex(AttackSceneProbability);
        AllocateToughnessAndIndex(SlotMachineSceneProbability);
        AllocateToughnessAndIndex(SpinWheelSceneProbability);

        if (PlayTime < 900)
        {
            if (MakeMatch)
            {
                MakeAThreeCardMatch();
            }
            if (MakeMissMatch)
            {
                MakeNothingMatch();
            }
        }
        if (MultiplierMaxingCount > 5 && EnergyEmptingCount > 3)
        {
            Behaviour = PlayerBehaviour.Aggressive;

        }
        else
        {
            Behaviour = PlayerBehaviour.Conservative;
        }

    }
    IEnumerator CheckPlayerState(float delayTime)
    {
        while (true)
        {
            if (GameManager.Instance._energy < 5)
            {
                CardProbability[GetCardIndexWithType(CardType.ENERGY)].Value = 50;
                ReArrangeTheRNgValues(CardProbability,50, GetCardIndexWithType(CardType.ENERGY));
            }
            else if(GameManager.Instance._coins < 1000 * GameManager.Instance.islandNumber)
            {
                CardProbability[GetCardIndexWithType(CardType.COINS)].Value = 50;
                ReArrangeTheRNgValues(CardProbability, 50, GetCardIndexWithType(CardType.COINS));
            }
            if(Behaviour == PlayerBehaviour.Aggressive)
            {
                
                ReArrangeTheRNgValues(CardProbability, ReduseValue(CardProbability, GetCardIndexWithType(CardType.ENERGY),-5), GetCardIndexWithType(CardType.ENERGY));
                ChangeValues(Reward.Energy, -5);
            }
            else
            {
                ReArrangeTheRNgValues(CardProbability, ReduseValue(CardProbability, GetCardIndexWithType(CardType.ENERGY), +5), GetCardIndexWithType(CardType.ENERGY));
                ChangeValues(Reward.Energy, 5);

            }
            yield return new WaitForSeconds(delayTime);
        }
    }

    void ChangeValues(Reward reward,int increaseValue)
    {
        int i = 0;
        foreach (var item in AttackSceneProbability)
        {
            if(item.Rewards== reward)
            {
                ReArrangeTheRNgValues(AttackSceneProbability, ReduseValue(AttackSceneProbability, i , increaseValue), i);
            }
            i++;
        }
        i = 0;
        foreach (var item in SpinWheelSceneProbability)
        {
            if (item.Rewards == reward)
            {
                ReArrangeTheRNgValues(SpinWheelSceneProbability, ReduseValue(SpinWheelSceneProbability, i , increaseValue), i);
            }
            i++;
        }
        i = 0;
        foreach (var item in SlotMachineSceneProbability)
        {
            if (item.Rewards == reward)
            {
                ReArrangeTheRNgValues(SlotMachineSceneProbability, ReduseValue(SlotMachineSceneProbability, i , increaseValue), i);
            }
            i++;
        }
    }
    int GetCardIndexWithType(CardType type)
    {
        int i = 0;
        foreach (var item in CardProbability)
        {
            if (item.CardType == type) return i;
            i++;
        }
        return 1;
    }

  /*  public int RandomChoose(Probability[] probs,bool NotCard=true)
    {
        float total = 0;

        foreach (Probability elem in probs)
        {
            total += elem.Value;
        }
        System.Random mRandomValue = new System.Random();
        double randomPoint = mRandomValue.NextDouble() * total;
        Debug.Log(randomPoint);
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i].Value)
            {
                if (!NotCard)
                {
                    if (GameManager.Instance.hasJoker && i == 0) return Random.Range(1, 7);
                }
                return i;
            }
            else
            {
                randomPoint -= probs[i].Value;
            }
        }
        int index = probs.Length - 1;
        if (!NotCard)
        {
            if (GameManager.Instance.hasJoker && index == 0) return Random.Range(1, 7);
        }
        return index;
    }*/
    void ReArrangeTheRNgValues(Probability[] probs, int ReducedValue,int index)
    {
        for (int i = 1; i < probs.Length; i++)
        {    
            if(i!=index)
            {
                int eachVal = Mathf.RoundToInt( ReducedValue / (probs.Length - 2)+.5f);
                probs[i].Value += eachVal<1?1:eachVal;
                if (probs[i].Value <= 0) probs[i].Value = 0;
            }
        }
    }

    int ReduseValue(Probability[] probs, int i,int ReducePercent= 50)
    {
        int redusedValue;
        if (i == 0)
        {
            redusedValue = probs[i].Value - 1;
        }
        else
        {
            redusedValue = Mathf.RoundToInt(probs[i].Value * ReducePercent / 100);
        }
        probs[i].Value -= redusedValue;
        return redusedValue;
    }

    public void Save()
    {
        SaveClass(CardProbability);
        SaveClass(EnergySceneProbability);
        SaveClass(CoinSceneProbability);
        SaveClass(SpinWheelSceneProbability);
        SaveClass(AttackSceneProbability);
        SaveClass(SlotMachineSceneProbability);

    }
    void SaveClass(Probability[] prob)
    {
        foreach (var item in prob)
        {
            PlayerPrefs.SetInt(item.Name, item.Value);
        }
    }
    void LoadClass(Probability[] prob)
    {
        foreach (var item in prob)
        {
            item.Value = PlayerPrefs.GetInt(item.Name, item.Value);
        }
    }
    private void Load()
    {
        LoadClass(CardProbability);
        LoadClass(EnergySceneProbability);
        LoadClass(CoinSceneProbability);
        LoadClass(SpinWheelSceneProbability);
        LoadClass(AttackSceneProbability);
        LoadClass(SlotMachineSceneProbability);
    }

    void ChangeCardProbability(CardType card, int ProbValue)
    {
        int i = 0;
        foreach (var CardProb in CardProbability)
        {
            if (CardProb.CardType == card)
            {
                ReArrangeTheRNgValues(CardProbability, ReduseValue(CardProbability, i, ProbValue), i);
            }
            i++;
        }
    }
    public void OnThreeCardMatch(CardType card)
    {
        NoOfBackToDeck = 0;
        NoOfThreeCardMatch++;
       // if (MakeMissMatch) return;
        ChangeCardProbability(card, 50);
    }

    public void OnBackToDeck()
    {
        NoOfThreeCardMatch = 0;
        NoOfBackToDeck++;
    }

    public void MakeAThreeCardMatch()
    {
        for (int i = 0; i < GameManager.Instance._SavedCardTypes.Count-1; i++)
        {
            if (GameManager.Instance._SavedCardTypes[i] == GameManager.Instance._SavedCardTypes[i + 1]) ChangeCardProbability((CardType)GameManager.Instance._SavedCardTypes[i], -50);
        }
    }

    public void MakeNothingMatch()
    {
        for (int i = 0; i < GameManager.Instance._SavedCardTypes.Count - 1; i++)
        {
            if (GameManager.Instance._SavedCardTypes[i] == GameManager.Instance._SavedCardTypes[i + 1]) ChangeCardProbability((CardType)GameManager.Instance._SavedCardTypes[i], 50);
            ChangeCardProbability(CardType.JOKER, 1);
        }
    }
    private void AllocateToughnessAndIndex(Probability[] prob)
    {
        double mTotalToughnessMeter = 0; 
        for (int i = 0; i < prob.Length; i++)
        {
            mTotalToughnessMeter += prob[i].Value;
            prob[i].RareNess = mTotalToughnessMeter;
        }
    }
    public int GetRandom(Probability[] prob, bool NotCard = true)
    {
        System.Random mRandomValue = new System.Random();
        double tempValue = mRandomValue.NextDouble() * CalculateRareness(prob);
        for (int i = 0; i < prob.Length; i++)
        {
            if (prob[i].RareNess >= tempValue)
            {
                if (!NotCard)
                {
                    if (GameManager.Instance.hasJoker && i == 0) return Random.Range(1, 7);
                }
               /* else
                    ReArrangeTheRNgValues(prob, ReduseValue(prob, i), i);*/
                return i;
            }
        }
        return 0;
    }
    double CalculateRareness(Probability[] prob)
    {
        double mTotalToughnessMeter = 0; 
        for (int i = 0; i < prob.Length; i++)
        {
            mTotalToughnessMeter += prob[i].Value;
        }
        return mTotalToughnessMeter;
    }
}
[System.Serializable]
public class Probability
{
    [HideInInspector]public string Name;
    [Range(0,100)]
    public int Value;
    [HideInInspector] public double RareNess;
    public Reward Rewards;
}
[System.Serializable]

public class CardProbability: Probability
{
    public CardType CardType;
}
public class SlotProbability : Probability
{
    public SlotReward slotReward;
}

[System.Serializable]
public class RNGData
{
    public List<Probability[]> list= new List<Probability[]>();
}
public enum Reward
{
    Coin,Energy,Card,FreeSpin
}
public enum SlotReward
{
    CoinJackPot,EnergyJackPot,FreeSpinJackPot,CardJackPot,CoinMidPot,EnergyMidPot,FreeMidPot,CardMidPot, CoinLowPot, EnergyLowPot, FreeLowPot, CardLowPot
}

