using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickersMenu : MonoBehaviour
{
    public List<GameObject> _stickerSet;
    public Transform _parentGameObject;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _stickerSet.Count; i++)
        {
            GameObject stickerSet = Instantiate(_stickerSet[i], _parentGameObject);
        }
    }


   
}
