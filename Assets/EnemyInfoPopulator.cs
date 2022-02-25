using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyInfoPopulator : MonoBehaviour
{
    [SerializeField] Image picture;
    [SerializeField] TMP_Text nameTxt;

    private void Awake()
    {
        MultiplayerManager.GotEnemyName += ChangeProfile;
    }


    private void Start()
    {

//        nameTxt.text = MultiplayerManager.Instance.Enemydata.UserDetails._playerName;
        picture.sprite = null;
        Action<Sprite> OnGettingPicture = (Pic) =>
        {
            picture.sprite = Pic;
        };

        FacebookManager.Instance.GetProfilePictureWithId(MultiplayerManager.Instance.Enemydata.UserDetails._playerID, OnGettingPicture);
    }


    public void ChangeProfile(Sprite Pic,string name)
    {
        picture.sprite = Pic;
        nameTxt.text = name;
    }
    void ChangeProfile(string name)
    {
        nameTxt.text = name;

    }

}
