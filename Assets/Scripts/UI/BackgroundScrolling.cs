using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mImages;

    [SerializeField]
    public float mScrollSpeed;

    public float rePositionSpot;

    void Start()
    {
    }

    
    void Update()
    {
        for (int i = 0; i < mImages.Length; i++)
        {
            mImages[i].transform.Translate(Vector3.up * Time.smoothDeltaTime * mScrollSpeed, Space.World);
            if(mImages[i].transform.localPosition.y > 1500f)
            {
                mImages[i].transform.position = new Vector3(mImages[i].transform.position.x, mImages[i].transform.position.y - 1000, mImages[i].transform.position.z);
            }
            //if (_reelElements[i]._slotElementGameObject.transform.localPosition.y < -600)
            //{
            //    _reelElements[i]._slotElementGameObject.transform.localPosition = new Vector3(_reelElements[i]._slotElementGameObject.transform.localPosition.x, _reelElements[i]._slotElementGameObject.transform.localPosition.y + accumalatedY, _reelElements[i]._slotElementGameObject.transform.localPosition.z);
            //    _reelElements[i]._slotElementGameObject.transform.SetSiblingIndex(i);
            //}
        }
    }
}
