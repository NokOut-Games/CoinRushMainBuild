using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _coins;
    public int _energy;
    public int _shield;
    public int _minutes;

    private int mMaxEnergy;
    private bool mIsFull = true;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); //Stays throughout the game
        StartCoroutine(AutomaticEnergyRefiller());
    }

    private void Update()
    {
        if(_energy > mMaxEnergy)
        {
            mIsFull = false;
            return;
        }
        else
        {
            mIsFull = true;
        }
    }

    private IEnumerator AutomaticEnergyRefiller()
    {
        while (mIsFull)
        {
            yield return new WaitForSeconds(MinutesToSecondsConverter(_minutes));
            _energy += 1;
        }
    }

    /// <summary>
    /// Converts the minutes given at Inspector into seconds and passes it to the coroutine function
    /// </summary>
    /// <param name="inMinutes"></param>
    /// <returns></returns>
    private int MinutesToSecondsConverter(int inMinutes) 
    {
        int seconds = inMinutes * 60;
        return seconds;
    }
}
