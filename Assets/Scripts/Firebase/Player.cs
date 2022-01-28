using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string _playerID;
    public string _playerName;
    public int _playerCurrentLevel = 1;
    public int _coins = 1000;
    public int _energy = 25;
    public string _playerPhotoURL;
    public int _openedCards;
    public Player(string inPlayerID, string inPlayerName = "Guest")
    {
        _playerID = inPlayerID;
        if(_playerName != "")
        {
            _playerName = inPlayerName;
        }
        else
        {
            _playerName = "Guest";
        }
        
    }
    public Player()
    {

    }
}
