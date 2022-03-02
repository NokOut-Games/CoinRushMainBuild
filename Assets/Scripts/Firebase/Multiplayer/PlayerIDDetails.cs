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
    string mGuestPlayerUserId;
    public string _randomEnemyID;
    public string _randomOpencardID;

    void Start()
    {
        mAuth = FirebaseAuth.DefaultInstance;
        mReference = FirebaseDatabase.DefaultInstance.RootReference;
        Invoke(nameof(FetchDetails),1f);
    }

   public void FetchDetails()
   {
        mReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Debug.Log("FB Count : "+snapshot.Child("Facebook Users").ChildrenCount);
                _playerList = new List<string>();

                             
                foreach (var dataSnapshot in snapshot.Child("Facebook Users").Children)
                {

                    if (dataSnapshot.Key.Length < 18)
                    {
                        mPlayerUserId = dataSnapshot.Key;

                        if (snapshot.Child("Facebook Users").HasChild(mPlayerUserId))
                        {
                           // _playerList.Add(mPlayerUserId);
                            _fbPlayerList.Add(mPlayerUserId);
                        }
                    }
                }

                foreach (var dataSnapshot2 in snapshot.Child("Guest Users").Children)
                {
                    mGuestPlayerUserId = dataSnapshot2.Key;
                    if (snapshot.Child("Guest Users").HasChild(mGuestPlayerUserId))
                    {
                      //  _playerList.Add(mGuestPlayerUserId);
                    }
                }

                   // AddUsersToList(snapshot, "Facebook Users", _fbPlayerList);
            }
        });
    }



    //void AddUsersToList(DataSnapshot snapshot, string userTitle, List<string> playerList)
    //{
    //    using (var sequenceThroughFacebookChildren = snapshot.Child(userTitle).Children.GetEnumerator())
    //    {
    //        for (int i = 0; i < snapshot.Child(userTitle).ChildrenCount; i++)
    //        {
    //            while (sequenceThroughFacebookChildren.MoveNext())
    //            {
    //                if (sequenceThroughFacebookChildren.Current.Key.Length < 18)
    //                {
    //                    string facebookUserIds = sequenceThroughFacebookChildren.Current.Key;
    //                    playerList.Add(facebookUserIds);
    //                }
    //            }
    //        }
    //    }
    //}
    public void GetRandomEnemyID(string inCurrentPlayerID)
    {
        _playerList.Remove(inCurrentPlayerID); _fbPlayerList.Remove(inCurrentPlayerID);//FirebaseManager.Instance.CurrentPlayerID);
       // _randomEnemyID = _playerList[UnityEngine.Random.Range(0, _playerList.Count)]; 
        _randomEnemyID = _fbPlayerList[UnityEngine.Random.Range(0, _fbPlayerList.Count)];
        _randomOpencardID= _fbPlayerList[UnityEngine.Random.Range(0, _fbPlayerList.Count)];
    }

}
