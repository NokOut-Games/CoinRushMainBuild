using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

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
    [Range(1, 20)] public int _reelRollDuration = 4;
    public bool _roll = false;

    public bool mSpinOver = false;
    public float accumalatedY;

    private double mTotalToughnessMeter;
    private System.Random mRandomValue = new System.Random();

    [SerializeField]
    public Transform mReelsRollerParent;
    
    [SerializeField]
    private int mSpeed = 5000;  //Will use it later instead of 700 down in update function
    public bool mdisableRoll = false;

    private UnityAction<ReelElement> mOnReelRollEndEvent;
    public int[] _imageFillPosition = new int[4];

    public GameObject coinParticle;
    public GameObject energyPaticle;

    private void Start()
    {
        mSpeed = Random.Range(80, 150);
        for (int i = 0; i < _reelElements.Length; i++)
        {
            accumalatedY += _reelElements[i]._slotElementGameObject.GetComponent<RectTransform>().sizeDelta.y;
        }



        CalculateIndexAndTotalToughness();
    }
    
    void Update()
    {
        if (_roll)
        {
            if (!mdisableRoll)
            {
                for (int i = _reelElements.Length - 1; i >= 0; i--)
                {
                    //Time.timeScale = 0.01f;                                                                          //700 Down is the speed it needs to roll
                    _reelElements[i]._slotElementGameObject.transform.Translate(Vector3.down * Time.smoothDeltaTime * mSpeed, Space.World);
                    if (_reelElements[i]._slotElementGameObject.transform.localPosition.y < -600)
                    {
                        _reelElements[i]._slotElementGameObject.transform.localPosition = new Vector3(_reelElements[i]._slotElementGameObject.transform.localPosition.x, _reelElements[i]._slotElementGameObject.transform.localPosition.y + accumalatedY, _reelElements[i]._slotElementGameObject.transform.localPosition.z);
                        _reelElements[i]._slotElementGameObject.transform.SetSiblingIndex(i);
                    }
                }
            }
        }
    }

    void DoFinalSet(int inIndex)
    {
        int preIndex = inIndex - 1;
        int postIndex = inIndex + 1;

        //Adjust the position the one before and one after the choosen reel
        if (inIndex == 0)
            preIndex = _reelElements.Length - 1;
        if (inIndex == _reelElements.Length - 1)
            postIndex = 0;

        Debug.Log(preIndex + "   " + postIndex);
        Transform preIndexTransform = _reelElements[preIndex]._slotElementGameObject.transform;
        Transform postIndexTransform = _reelElements[postIndex]._slotElementGameObject.transform;

        // 300 being the interval between the icons.
        preIndexTransform.localPosition = new Vector3(preIndexTransform.localPosition.x, _reelElements[inIndex]._slotElementGameObject.transform.localPosition.y - 200, preIndexTransform.localPosition.z);
        postIndexTransform.localPosition = new Vector3(postIndexTransform.localPosition.x, _reelElements[inIndex]._slotElementGameObject.transform.localPosition.y + 200, postIndexTransform.localPosition.z);
    }

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
    /// Finds a Gameobject based on probability and stop the reel at appropriate spot 
    /// </summary>
    public void Spin()
    {
        energyPaticle.SetActive(false);
        coinParticle.SetActive(false);


        int index = GetRandomEnergyIndexBasedOnProbability();
        //int index = RNG.instance.RandomChoose(RNG.instance.SloatMachineSceneProbability);
        ReelElement mReel = _reelElements[index];
        float TargetPosition = -(mReel._slotElementGameObject.transform.localPosition.y);
        mdisableRoll = true;
        DoFinalSet(index);

        mReelsRollerParent.DOLocalMoveY(TargetPosition, _reelRollDuration, false)
        .OnComplete(() =>
        {
            mSpinOver = true;
            _roll = false;

            if (mOnReelRollEndEvent != null)
            {
                mOnReelRollEndEvent(mReel);
            }
            mOnReelRollEndEvent = null;
        });
    }

}