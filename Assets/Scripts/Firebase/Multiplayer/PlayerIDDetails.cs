using UnityEngine;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;


public class PlayerIDDetails : MonoBehaviour
{
    DatabaseReference mReference;
    FirebaseAuth mAuth;
    public List<string> _playerList;
    private string mPlayerUserId;
    public string _randomEnemyID;

    void Start()
    {
        mAuth = FirebaseAuth.DefaultInstance;
        mReference = FirebaseDatabase.DefaultInstance.RootReference;
        Invoke("FetchDetails",1f);
    }

   public void FetchDetails()
   {
        mReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                _playerList = new List<string>();

                //Show Results in a list
                foreach (var dataSnapshot in snapshot.Child("Facebook Users").Children)
                {
                    mPlayerUserId = dataSnapshot.Key;
                    _playerList.Add(mPlayerUserId);
                    // _playerList.Remove("Facebook Users"); _playerList.Remove("Guest Users"); _playerList.Remove("Timestamp");
                }

                foreach (var dataSnapshot2 in snapshot.Child("Guest Users").Children)
                {
                    mPlayerUserId = dataSnapshot2.Key;
                    _playerList.Add(mPlayerUserId);
                }

            }
        });
    }
  
    public void GetRandomEnemyID(string inCurrentPlayerID)
    {
        _playerList.Remove("7wJq25iCVsdchgQFfJv9Qhrp0At2"); _playerList.Remove("sYdMBt1kxaRtQ1oQkAoumilB5Bj1"); _playerList.Remove("xi1RMoQJWPMSxiThnmWTTs6XofP2"); 
        _playerList.Remove(inCurrentPlayerID); //FirebaseManager.Instance.CurrentPlayerID);
        _randomEnemyID = _playerList[UnityEngine.Random.Range(0, _playerList.Count)];
    }

}
