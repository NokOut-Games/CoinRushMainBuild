using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string _playerID;
    public string _playerName;
    public int _playerCurrentLevel = 1;
    public int islandNumber = 1;
    public int _coins = 1000;
    public int _energy = 25;
    public int _numberOfTimesGotAttacked;
    public int Behaviour;
    public Player(string inPlayerID, string inPlayerName = "Guest")
    {
        _playerID = inPlayerID;       
        _playerName = inPlayerName;
    }
}

public enum PlayerBehaviour
{
    Conservative, Aggressive
}
