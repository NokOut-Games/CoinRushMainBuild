using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[System.Serializable]
public class ReelElement
{
    public GameObject _slotElementGameObject;
    
    [Range(0, 100)] public float _chanceOfObtaining;
    [HideInInspector] public int _index;
    [HideInInspector] public double _toughnessMeter;
}

public class Reels : MonoBehaviour
{
    public ReelElement[] _reelElements; 
    [Range(1,20)] public int _reelRollDuration = 4;
    public bool _roll = false;  

    private double mTotalToughnessMeter;
    private System.Random mRandomValue = new System.Random();
    
    [SerializeField] 
    private Transform mReelsRollerParent;

    [SerializeField]
    private int mSpeed = 5000;  //Will use it later instead of 700 down in update function
    public bool mdisableRoll = false;

    private UnityAction<ReelElement> mOnReelRollEndEvent;
    
    private void Start()
    {
        //speed = Random.Range(700, 1200);
        CalculateIndexAndTotalToughness();
    }

    void Update()
    {
        

        if (_roll)
        {
            if (!mdisableRoll)
            {
                for (int i = 0; i < _reelElements.Length; i++)
                {                                                                                                    //700 Down is the speed it needs to roll
                    _reelElements[i]._slotElementGameObject.transform.Translate(Vector3.down * Time.smoothDeltaTime * mSpeed, Space.World);
                    if (_reelElements[i]._slotElementGameObject.transform.localPosition.y < -600)
                    {
                        _reelElements[i]._slotElementGameObject.transform.localPosition = new Vector3(_reelElements[i]._slotElementGameObject.transform.localPosition.x, _reelElements[i]._slotElementGameObject.transform.localPosition.y + 1200, _reelElements[i]._slotElementGameObject.transform.localPosition.z);
                    }
                    
                }
            }
        }
    }

    /// <summary>
    /// Event Function
    /// </summary>
    /// <param name="action"></param>
    public void OnReelRollEnd(UnityAction<ReelElement> action)
    {
        mOnReelRollEndEvent = action;
    }

    /// <summary>
    /// Calculates the accumulated overall weights / toughness for each slot elements in reels
    /// </summary>
    private void CalculateIndexAndTotalToughness()
    {
        for (int i = 0; i < _reelElements.Length; i++)
        {
            ReelElement mReel = _reelElements[i];
            mTotalToughnessMeter += mReel._chanceOfObtaining;
            mReel._toughnessMeter = mTotalToughnessMeter;

            mReel._index = i;
        }
    }

    /// <summary>
    /// Gets A random value with a given probability
    /// </summary>
    /// <returns></returns>
    private int GetRandomEnergyIndexBasedOnProbability()
    {
        double tempValue = mRandomValue.NextDouble() * mTotalToughnessMeter;
        for (int i = 0; i < _reelElements.Length; i++)
        {
            if (_reelElements[i]._toughnessMeter >= tempValue)
            {
                return i;
            }
        }
        return 0;
    }

    /// <summary>
    /// All spin functions for the reel to do when roll button is clicked
    /// </summary>
    public void Spin()
    {
        mdisableRoll = true;
        int index = GetRandomEnergyIndexBasedOnProbability();
        ReelElement mReel = _reelElements[index];
        float TargetPosition = -(mReel._slotElementGameObject.transform.localPosition.y);

        mReelsRollerParent.DOLocalMoveY(TargetPosition,_reelRollDuration,false)
        .OnComplete(() => 
        {
            for (int i = 0; i < _reelElements.Length; i++)
            {
                if (_reelElements[i]._slotElementGameObject.transform.localPosition.y < -305)
                {
                    _reelElements[i]._slotElementGameObject.transform.localPosition = new Vector3(_reelElements[i]._slotElementGameObject.transform.localPosition.x, _reelElements[i]._slotElementGameObject.transform.localPosition.y + 1200, _reelElements[i]._slotElementGameObject.transform.localPosition.z);
                }
            }
            _roll = false;
            if (mOnReelRollEndEvent != null)
            {
                mOnReelRollEndEvent(mReel);
            }
            mOnReelRollEndEvent = null;
        });

        //Should put another condition where if the selected element goes below a certain position in y while being chose by probability we need to make it to do 
        //another roll and chose the probability again
    }
}














//void Residue()
//{
//rowStopped = true;

//if (!neverDone)
//{

//}
////neverDone = true;

//else
//{
//    rowStopped = true;
//}
//Debug.Log(disableMove);

//Invoke("Targeter", Random.Range(2f,5f));
//ReelsRollerParent.DOMoveY(TargetPosition, reelRollDuration, true).OnComplete(() => { spin = false; rowStopped = true; });

//rowStopped = false;

//    //if (rowStopped == false)
//    //{

//    //}
//    //if (rowStopped == true) //Checking all If condition once the Spin has stopped
//    //{
//    //    if (Slotelements[0].GetComponent<RectTransform>().localPosition.y == 0)
//    //        stoppedSlot = Slotelements[0].name;
//    //       // Debug.Log(Slotelements[0].name);
//    //    if (Slotelements[1].GetComponent<RectTransform>().localPosition.y == 0)
//    //        stoppedSlot = Slotelements[1].name;
//    //       // Debug.Log(Slotelements[1].name);

//    //    if (Slotelements[2].GetComponent<RectTransform>().localPosition.y == 0)
//    //         stoppedSlot = Slotelements[2].name;
//    //      //  Debug.Log(Slotelements[2].name);

//    //    if (Slotelements[3].GetComponent<RectTransform>().localPosition.y == 0)
//    //          stoppedSlot = Slotelements[3].name;
//    //       // Debug.Log(Slotelements[3].name);

//    //    if (Slotelements[4].GetComponent<RectTransform>().localPosition.y == 0)
//    //         stoppedSlot = Slotelements[4].name;
//    //       //Debug.Log(Slotelements[4].name);

//    //    if (Slotelements[5].GetComponent<RectTransform>().localPosition.y == 0)
//    //          stoppedSlot = Slotelements[5].name;
//    //       // Debug.Log(Slotelements[5].name);

//    //    if (Slotelements[6].GetComponent<RectTransform>().localPosition.y == 0)
//    //        stoppedSlot = Slotelements[6].name;
//    //       // Debug.Log(Slotelements[6].name);

//    //}

//void Targeter()
//{

//}

////Once The Reel Finishes Spinning The Images Will Be Placed In A Random Position
//public void RandomPosition()
//{

//    List<int> parts = new List<int>();

//    //Add All Of The Values For The Original Y Postions  
//    parts.Add(200);
//    parts.Add(100);
//    parts.Add(0);
//    parts.Add(-100);
//    parts.Add(-200);
//    parts.Add(-300);
//    parts.Add(-400);


//    foreach (Transform image in transform)
//    {
//        int rand = Random.Range(0, parts.Count);

//        //The "transform.parent.GetComponent<RectTransform>().transform.position.y" Allows It To Adjust To The Canvas Y Position
//        image.transform.position = new Vector3(image.transform.position.x, parts[rand] + transform.parent.GetComponent<RectTransform>().transform.position.y, image.transform.position.z);

//        parts.RemoveAt(rand);
//    }
//}
//}