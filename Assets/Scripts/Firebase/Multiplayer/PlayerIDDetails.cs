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
    public List<string> _fbPlayerList;
    private string mPlayerUserId;
    public string _randomEnemyID;
    public string _randomOpencardID;

    void Start()
    {
        mAuth = FirebaseAuth.DefaultInstance;
        mReference = FirebaseDatabase.DefaultInstance.RootReference;
        Invoke(nameof(FetchDetails),2f);
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

                AddUsersToList(snapshot, "Facebook Users", _fbPlayerList);
                //AddUsersToList(snapshot, "Guest Users", _playerList);

            }
        });
    }
  


    void AddUsersToList(DataSnapshot snapshot,string userTitle,List<string> playerList)
    {
        using (var sequenceThroughFacebookChildren = snapshot.Child(userTitle).Children.GetEnumerator())
        {
            for (int i = 0; i < snapshot.Child(userTitle).ChildrenCount; i++)
            {
                while (sequenceThroughFacebookChildren.MoveNext())
                {
                    string facebookUserIds = sequenceThroughFacebookChildren.Current.Key;
                    playerList.Add(facebookUserIds);
                }
            }
        }
    }
    public void GetRandomEnemyID(string inCurrentPlayerID)
    {
        _playerList.Remove(inCurrentPlayerID); _fbPlayerList.Remove(inCurrentPlayerID);//FirebaseManager.Instance.CurrentPlayerID);
        _randomEnemyID = _playerList[UnityEngine.Random.Range(0, _playerList.Count)];
        _randomOpencardID= _fbPlayerList[UnityEngine.Random.Range(0, _fbPlayerList.Count)];
    }

}
