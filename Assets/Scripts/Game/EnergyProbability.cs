using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Energy
{
    public int _energyAmount;
    [Range(0, 100)] public float _chanceOfObtaining;
    public int _index;
    public double _toughnessMeter;

    public Energy(int energyAmount , float chanceOfObtaining , int index , double toughnessMeter)
    {
        _energyAmount = energyAmount;
        _chanceOfObtaining = chanceOfObtaining;
        _index = index;
        _toughnessMeter = toughnessMeter;
    }
}

public class EnergyProbability : MonoBehaviour
{
    public List<Energy> _energies;
    
    private double mTotalToughnessMeter;
    private System.Random mRandomValue = new System.Random();

    private float mChanceA = 100, mChanceB = 10, mChanceC = 1;

    private Energy mEnergy;

    private void Awake()
    {
       
    }

    public void Start()
    {
        _energies = new List<Energy>(3);
        _energies.Add(new Energy(int.Parse(GameManager.Instance.minigameEconomy.EnergyReward[0]), mChanceA, 0, 0));
        _energies.Add(new Energy(int.Parse(GameManager.Instance.minigameEconomy.EnergyReward[1]), mChanceB, 0, 0));
        _energies.Add(new Energy(int.Parse(GameManager.Instance.minigameEconomy.EnergyReward[2]), mChanceC, 0, 0));
        AllocateToughnessAndIndex();
    }
    private void AllocateToughnessAndIndex()
    {
        for (int i = 0; i < _energies.Count; i++)
        {
            mEnergy = _energies[i];
            mTotalToughnessMeter += mEnergy._chanceOfObtaining;
            mEnergy._toughnessMeter = mTotalToughnessMeter;

            mEnergy._index = i;
        }
    }

    public int DisplayTheFinalElementBasedOnRandomValueGenerated()
    {
        int index =  GetRandomEnergyIndexBasedOnProbability();
        mEnergy = _energies[index];
        return mEnergy._energyAmount;
    }

    private int GetRandomEnergyIndexBasedOnProbability()
    {
        double tempValue = mRandomValue.NextDouble() * mTotalToughnessMeter;
        for (int i = 0; i < _energies.Count; i++)
        {
            if (_energies[i]._toughnessMeter >= tempValue)
            {
                return i;
            }
        }
        return 0;
    }
}