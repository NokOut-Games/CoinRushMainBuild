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

    void Start()
    {
        mAuth = FirebaseAuth.DefaultInstance;
        mReference = FirebaseDatabase.DefaultInstance.RootReference;
        Invoke("FetchDetails",2f);
    }

   public void FetchDetails()
   {
        mReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                _playerList = new List<string>();

                if (snapshot.Child("Facebook Users").ChildrenCount > 0)
                {
                    //Show Results in a list
                    foreach (var dataSnapshot in snapshot.Child("Facebook Users").Children)
                    {
                        mPlayerUserId = dataSnapshot.Key;
                        _playerList.Add(mPlayerUserId);
                    }
                }
                foreach(var dataSnapshot2 in snapshot.Child("Guest Users").Children)
                {
                    mPlayerUserId = dataSnapshot2.Key;
                    _playerList.Add(mPlayerUserId);
                }
                
            }
        });
    }

}
