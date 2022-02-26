using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevengeAttackPanel : MonoBehaviour
{
    public Button _backButton;
    public GameObject _revengePanel;
    public List<RectTransform> TransformPoints;
    public List<GameObject> EnemyList = new List<GameObject>();
    public List<string> attackedplayerIDList = new List<string>();

    public List<string> RawAttackedPlayerID;
    public List<string> FilteredAttackedPlayerID;
    public List<string> FilteredAttackedName = new List<string>();
    public List<string> FilteredAttackedBuilding = new List<string>();
    Text enemyName;

    public GameObject _ENemy;

    public GameObject FBFriendsPanel;
    public List<RectTransform> FBTransformPoints;
    public GameObject _FBFriends;
    public List<GameObject> FBEnemyList = new List<GameObject>();
    [SerializeField]Transform ContentOfRevenge;

    public void Start()
    {
        RawAttackedPlayerID =MultiplayerManager.Instance.attackedplayerIDList;
        for (int i = 0; i < RawAttackedPlayerID.Count; i++)
        {
            if (FilteredAttackedPlayerID.Contains(RawAttackedPlayerID[i])) continue;
            FilteredAttackedPlayerID.Add(RawAttackedPlayerID[i]);
            FilteredAttackedName.Add(MultiplayerManager.Instance.attackedplayerNameList[i]);
        }
    }
    public void OpenRevengePanel()
    {
        _revengePanel.SetActive(true);
        EnemyList.Clear();
        //FirebaseManager.Instance.AttackedData.Clear();

        for (int i = 0; i < FilteredAttackedPlayerID.Count; i++)
        {
            //GameObject EnemySlot = Instantiate(_ENemy, TransformPoints[i].position, TransformPoints[i].rotation, TransformPoints[i].parent);
            GameObject EnemySlot = Instantiate(_ENemy, ContentOfRevenge);
            EnemyList.Add(EnemySlot);

            EnemyList[i].transform.GetChild(0).GetComponent<Text>().text = FilteredAttackedName[i];
            EnemyList[i].transform.GetChild(2).GetComponent<RevengeButton>().EnemyID = FilteredAttackedPlayerID[i];
            System.Action<Sprite,int> ONGettingProfilePic = (Pic,index) =>
            {
                EnemyList[index].transform.GetChild(3).GetComponent<Image>().sprite = Pic;
            };
            FacebookManager.Instance.GetProfilePictureWithId(FilteredAttackedPlayerID[i], ONGettingProfilePic, i);
        }
    }
    public void GoBackFromRevengePanel()
    {
        _revengePanel.SetActive(false);
    }
    public void RevengeFBFriends()
    {
        FBFriendsPanel.SetActive(true);
        for (int i = 0; i < FacebookManager.Instance.FBFriendsIDList.Count; i++)
        {
            GameObject FBEnemySlot = Instantiate(_FBFriends, FBTransformPoints[i].position, FBTransformPoints[i].rotation, FBTransformPoints[i].parent);
            FBEnemyList.Add(FBEnemySlot);

            FBEnemyList[i].transform.GetChild(0).GetComponent<Text>().text = FacebookManager.Instance.FBFriendsNameList[i];
            FBEnemyList[i].transform.GetChild(2).GetComponent<RevengeButton>().EnemyID = FacebookManager.Instance.FBFriendsIDList[i];
           // FacebookManager.Instance.GetFriendsPicture();
            /*FB.API("https" + "://graph.facebook.com/" + FacebookManager.Instance.FBFriendsIDList[i] + "/picture?type=large", HttpMethod.GET, delegate (IGraphResult result)
            {
                Debug.Log(FacebookManager.Instance.FBFriendsIDList[i] + "Pic ID");
                FBEnemyList[i].transform.GetChild(3).GetComponent<Image>().sprite = Sprite.Create(result.Texture, new Rect(0, 0, 200, 125), new Vector2(0.5f, 0.5f), 100);
            });*/
        }
    }

    public void GoBackFromRevengeFBPanel()
    {
        FBFriendsPanel.SetActive(false);
    }
    public void BackToGame()
    {
        _backButton.interactable = false;
        LevelLoadManager.instance.BacktoHome();
    }
}
