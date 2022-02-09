using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyInfoPopulator : MonoBehaviour
{
    [SerializeField] Image picture;
    [SerializeField] TMP_Text nameTxt;


    private void Start()
    {

        nameTxt.text = MultiplayerManager.Instance.GetComponent<MultiplayerPlayerData>()._enemyName;

        Action<Sprite> OnGettingPicture = (Pic) =>
        {


            picture.sprite = Pic;

        };

        FacebookManager.Instance.GetProfilePictureWithId(MultiplayerManager.Instance._enemyPlayerID, OnGettingPicture);


    }



}
