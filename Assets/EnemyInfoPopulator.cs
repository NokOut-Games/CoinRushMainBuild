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

        nameTxt.text = MultiplayerManager.Instance._enemyName;// .GetComponent<MultiplayerPlayerData>()._enemyName;
        picture.sprite = null;
        Action<Sprite> OnGettingPicture = (Pic) =>
        {
            picture.sprite = Pic;
        };

        FacebookManager.Instance.GetProfilePictureWithId(MultiplayerManager.Instance._enemyPlayerID, OnGettingPicture);


    }



}
