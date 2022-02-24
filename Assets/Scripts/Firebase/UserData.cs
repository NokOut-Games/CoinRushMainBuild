using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public List<AttackedPlayer> AttackedPlayer;
    public List<Building> Buildings;
    public MapData MapData;
    public List<OpenCard> OpenCards;
    public List<int> SaveCards;
    public UserDetails UserDetails;
}

[System.Serializable]
public class AttackedPlayer
{
    public string _attackedPlayerID;
    public string _attackedPlayerName;
    public string _attackedBuildingName;
}
[System.Serializable]
public class MapData
{
    public List<int> LevelsInSet;
    public int SetIndex; 
}
[System.Serializable]
public class OpenCard
{
    public int _openedCardSlot;
    public string _openedPlayerName;
    public string _openedPlayerID;
    public int _openedCardSelectedCard;
}


[System.Serializable]
public class UserDetails
{
    public string LogOutTime;
    public int _coins;
    public int _energy;
    public int _numberOfTimesGotAttacked;
    public int _playerCurrentLevel;
    public int islandNumber=1;
    public string _playerID;
    public string _playerName;
}



