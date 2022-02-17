using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class UserData
//{
//    public class AttackedPlayer
//    {
//        public string _attackedPlayerID;
//        public string _attackedPlayerName;
//        public string _attackedBuildingName;
//    }

//    public class Building
//    {
//        public string _buildingName;
//        public int _buildingCurrentLevel;
//        public bool _isBuildingShielded;
//        public bool _isBuildingDestroyed;
//    }

//    public class MapData
//    {
//        public int SetIndex;
//        public List<int> LevelsInSet;
//    }

//    public class OpenCards
//    {
//        public int _openedCardSlot;
//        public string _openedPlayerName;
//        public string _openedPlayerID;
//        public int _openedCardSelectedCard;
//    }

//    public List<int> SaveCards;

//    public class UserDetails
//    {
//        public string _playerID;
//        public string _playerName;
//        public int _playerCurrentLevel = 1;
//        public int _coins = 1000;
//        public int _energy = 25;
//        public int _numberOfTimesGotAttacked;

//    }

//}

[System.Serializable]
public class UserData
{
    public List<AttackedPlayer> AttackedPlayer;
    public List<Building> Buildings;
    public MapData MapData;
    public List<OpenCard> OpenCards;
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
    public string _playerID;
    public string _playerName;
}



