using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevengeAttackPanel : MonoBehaviour
{
    public GameObject _revengePanel;
    public List<RectTransform> TransformPoints;
    public List<GameObject> EnemyList = new List<GameObject>();
    public List<string> attackedplayerIDList = new List<string>();

    public List<string> RawAttackedPlayerID;
    public List<string> FilteredAttackedPlayerID;
    public List<string> FilteredAttackedName = new List<string>();
    Text enemyName;

    public GameObject _ENemy;

    public GameObject FBFriendsPanel;

    public void Start()
    {    /*&& attackedplayerNameList.Contains(MultiplayerManager.Instance.attackedplayerNameList[i])*/
        // attackedplayerNameList.Add(MultiplayerManager.Instance.attackedplayerNameList[i]);
        RawAttackedPlayerID=MultiplayerManager.Instance.attackedplayerIDList;

        Debug.Log(RawAttackedPlayerID.Count);
        for (int i = 0; i < RawAttackedPlayerID.Count; i++)
        {
            Debug.Log("Dum"+RawAttackedPlayerID[i]);
            if (FilteredAttackedPlayerID.Contains(RawAttackedPlayerID[i])) continue;

            FilteredAttackedPlayerID.Add(RawAttackedPlayerID[i]);
            FilteredAttackedName.Add(MultiplayerManager.Instance.attackedplayerNameList[i]);
            Debug.Log("Dum2" + RawAttackedPlayerID[i]);
        }

    }
    public void OpenRevengePanel()
    {
        _revengePanel.SetActive(true);

        Debug.Log("Im here");
        EnemyList.Clear();
        for (int i = 0; i < FilteredAttackedPlayerID.Count; i++)
        {
            GameObject EnemySlot = Instantiate(_ENemy, TransformPoints[i].position, TransformPoints[i].rotation, TransformPoints[i].parent);
            EnemyList.Add(EnemySlot);

            EnemyList[i].transform.GetChild(0).GetComponent<Text>().text = FilteredAttackedName[i];
            EnemyList[i].transform.GetChild(2).GetComponent<Text>().text = FilteredAttackedPlayerID[i];
        }

    }
    public void GoBackFromRevengePanel()
    {
        _revengePanel.SetActive(false);
    }
    public void RevengeFBFriends()
    {
        FBFriendsPanel.SetActive(true);
    }

    public void GoBackFromRevengeFBPanel()
    {
        FBFriendsPanel.SetActive(false);

    }
    public void BackToGame()
    {
        LevelLoadManager.instance.BacktoHome();
    }
}
