using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelManagerUI : MonoBehaviour
{
    public List<GameObject> _fiveThousandCoinList;
    public List<GameObject> _twentyFiveThousandCoinList;
    public List<GameObject> _hunderThousandCoinList;
    public List<GameObject> _fiveHundredThousandCoinList;
    public List<GameObject> _OneMillionJackPotCardList;

    public GameManager _gameManager;

    public TextMeshProUGUI _coinText;
    public TextMeshProUGUI _energyText;

    public bool isDone = false; 


    private void Update()
    {
        UpdateCoinAndEnergyTextFields();
        if (!isDone)
        {
            if (_fiveThousandCoinList.Count == 3)
            {
                _gameManager._coins += 5000;
                isDone = true;
                foreach (GameObject c in _fiveThousandCoinList)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                _fiveThousandCoinList.Clear();
                Invoke("IsDone", 1f);
            }
            if (_twentyFiveThousandCoinList.Count == 3)
            {
                _gameManager._coins += 25000;
                isDone = true;
                foreach (GameObject c in _twentyFiveThousandCoinList)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                _twentyFiveThousandCoinList.Clear();
                Invoke("IsDone", 1f);
            }
            if (_hunderThousandCoinList.Count == 3)
            {
                _gameManager._coins += 100000;
                isDone = true;
                foreach (GameObject c in _hunderThousandCoinList)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                _hunderThousandCoinList.Clear();
                Invoke("IsDone", 1f);
            }
            if (_fiveHundredThousandCoinList.Count == 3)
            {
                _gameManager._coins += 500000;
                isDone = true;
                foreach (GameObject c in _fiveHundredThousandCoinList)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                _fiveHundredThousandCoinList.Clear();
                Invoke("IsDone", 1f);
            }
            if (_OneMillionJackPotCardList.Count == 3)
            {
                _gameManager._coins += 1000000;
                isDone = true;
                foreach (GameObject c in _OneMillionJackPotCardList)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                _OneMillionJackPotCardList.Clear();
                Invoke("IsDone", 1f);
            }
        }
    }

    void IsDone()
    {
        isDone = false;
    }

    /// <summary>
    /// Keeps updating coin and energy text fields
    /// </summary>
    private void UpdateCoinAndEnergyTextFields()
    {
        _coinText.text = _gameManager._coins.ToString();
        _energyText.text = _gameManager._energy.ToString();
    }
}
