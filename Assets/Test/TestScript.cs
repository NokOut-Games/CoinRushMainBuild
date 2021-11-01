using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    [Header("Grabbing Other GameObject References:")]
    [SerializeField] private GameManager mGameManager;
    [SerializeField] private GameObject mCardHolderParent;
    private int clicks = 0;
    public List<Transform> _playerHandPoints;

    [Space(10)]
    [Header("Cards And Related Lists:")]
    [SerializeField] private List<ScriptedCards> mScriptedCards;
    [SerializeField] private List<Cards> _CardList = new List<Cards>();
    [SerializeField] private List<GameObject> mCardListGameObject;
    private List<Vector3> _PositionList = new List<Vector3>();
    private List<Quaternion> _RotationList = new List<Quaternion>();

    [Space(10)]
    [Header("Draw Button And Its States Images with conditions:")]
    [SerializeField] private Image DrawButton;
    [SerializeField] private Sprite drawNormal, drawAutomatic;
    [SerializeField] private RectTransform _drawButtonRectTransform;
    [Space(10)]
    [SerializeField] private int mMaxHoldTime = 5;
    [SerializeField] private float timeForCardAnimation = 2f;
    private float time = 0, maxTime = 5;
    private bool mAutoCardDraw = false;
    private bool mAutomaticDrawModeOn = false;
    private bool mOnceDone = false;
    //public Image _buttonFillerImage;
    

    [Space(10)]
    [Header("Joker and related things")]
    public List<Cards> _jokerList;
    public bool onceDonee = false;

    public int HowManySets;
    public List<Cards> _jokerCheckerList;
    public GameObject Arrow;

    public Cards[] bla = new Cards[2];

    private void Start()
    {
        onceDonee = false;
        DrawButton.sprite = drawNormal;
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        for (int i = 0; i < mScriptedCards.Count; i++)
        {
            GameObject card = Instantiate(mScriptedCards[i]._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
            Cards cardDetails = card.GetComponent<Cards>();

            cardDetails._cardType = mScriptedCards[i]._cardType;
            cardDetails._cardID = mScriptedCards[i]._cardID;
            cardDetails._Position = card.transform.position;

            AddNewCard(card.GetComponent<Cards>(), card);
            ReplacementOfCards();
            //CardCheckingFunction();
        }
    }

    private void Update()
    {
        if (clicks == 8)
        {
            clicks = 0;
            foreach (GameObject card in mCardListGameObject)
            {
                Destroy(card);
            }
            _CardList.Clear();
            mCardListGameObject.Clear();
        }

        if (_jokerList.Count >= 1)
        {
            if (onceDonee == true)
            {
                return;
            }
            CardType type = _CardList[0]._cardType;
            int count = 1;
            for (int j = 1; j < _CardList.Count; j++)
            {
                if (_CardList[j]._cardType == type)
                {
                    count++;
                    if (count == 2)
                    {
                        HowManySets += 1;
                        _jokerCheckerList.Add(_CardList[j]);
                        onceDonee = true;
                    }
                }
                else
                {
                    type = _CardList[j]._cardType;
                    count = 1;
                }
            }
            JokerStuff(HowManySets);
        }
        #region Button Function
        //time = Mathf.Clamp(time, 0f, 5f);
        //Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (_drawButtonRectTransform.rect.Contains(localMousePosition))
        //    {
        //        BackToNormalState();
        //        time = 0;
        //        DrawCard();
        //    }
        //}

        //if (!mOnceDone)
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        if (_drawButtonRectTransform.rect.Contains(localMousePosition))
        //        {
        //            time += Time.fixedDeltaTime;
        //            //float lerpSpeed = 3 * Time.deltaTime;
        //            //_buttonFillerImage.fillAmount = Mathf.Lerp(_buttonFillerImage.fillAmount, time/maxTime, lerpSpeed);
        //            if (time >= mMaxHoldTime)
        //            {
        //                mOnceDone = true;
        //                mAutomaticDrawModeOn = true;
        //                mAutoCardDraw = true;
        //                ChangeSprites();
        //                StartCoroutine(AutomaticCardDrawing());
        //            }
        //        }
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (_drawButtonRectTransform.rect.Contains(localMousePosition))
        //    {
        //        time = 0;
        //    }
        //}
        #endregion
    }

    void JokerStuff(int inHowManySets)
    {
        switch (inHowManySets)
        {
            case 1: //If Only One Set of similar Cards
                for (int i = 0; i < _jokerCheckerList.Count; i++)
                {
                    _jokerList[0]._cardType = _jokerCheckerList[0]._cardType;
                    AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject);
                    ReplacementOfCards();
                    CardCheckingFunction();
                }
                break;
            case 2:
                int j = 0;
                
                for (int i = 0; i < _jokerCheckerList.Count; i++, j+= 400)
                {
                    bla[i] = Instantiate(_jokerCheckerList[i], _playerHandPoints[1].position + new Vector3(j , 400 , 0), Quaternion.identity, mCardHolderParent.transform);
                    bla[i].transform.gameObject.AddComponent<Button>();
                    bla[i].transform.gameObject.GetComponent<Button>().onClick.AddListener(() => { Debug.Log(bla[i].transform.gameObject.name); });
                    Debug.Log(bla[i]);
                }
                break;
                
            default:
                break;
        }
        //_jokerList[0]._cardType = _CardList[j]._cardType;
        AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject);
        ReplacementOfCards();
        CardCheckingFunction();
    }
    public void BackToNormalState()
    {
        if (mAutomaticDrawModeOn)
        {
            mAutomaticDrawModeOn = false;
            ChangeSprites();
            mOnceDone = false;
            mAutoCardDraw = false;
            StopCoroutine(AutomaticCardDrawing());
        }
    }

    private void ChangeSprites()
    {
        if (DrawButton.sprite == drawNormal)
        {
            DrawButton.sprite = drawAutomatic;
        }
        else if (DrawButton.sprite == drawAutomatic)
        {
            DrawButton.sprite = drawNormal;
        }
    }

    private void DrawCard()
    {
        if (_CardList.Count >= 8)
        {
            return;
        }
        mGameManager._energy -= 1;

        Camera.main.GetComponent<CameraController>().DrawButtonClicked();

        ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

        GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
        Cards cardDetails = card.GetComponent<Cards>();

        cardDetails._cardType = cards._cardType;
        cardDetails._cardID = cards._cardID;
        cardDetails._Position = card.transform.position;

        clicks += 1;
        AddNewCard(card.GetComponent<Cards>(), card);
        ReplacementOfCards();
        CardCheckingFunction();
    }

    private IEnumerator AutomaticCardDrawing()
    {
        while (mAutoCardDraw)
        {
            DrawCard();
            yield return new WaitForSeconds(timeForCardAnimation);
        }
    }

    private void AddNewCard(Cards inNewCard, GameObject inCard)
    {
        mCardListGameObject.Add(inCard);
        for (int i = 0; i < _CardList.Count; i++)
        {

            if (_CardList[i]._cardType == inNewCard._cardType)
            {
                _CardList.Insert(i, inNewCard);
                return;
            }

        }
        if (inNewCard._cardType == CardType.JOKER)
        {
            _jokerList.Add(inNewCard);
        }
        _CardList.Add(inNewCard);
    }

    private void ReplacementOfCards()
    {
        int medianIndex = _playerHandPoints.Count / 2;

        int incrementValue = 0;
        _PositionList.Clear();
        _RotationList.Clear();

        List<int> drawOrderArrange = new List<int>();

        for (int i = 0; i < _CardList.Count; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                drawOrderArrange.Add(medianIndex + incrementValue);
                incrementValue++;
            }
            else
            {
                drawOrderArrange.Add(medianIndex - incrementValue);
            }
        }

        drawOrderArrange.Sort();

        for (int i = 0; i < _CardList.Count; i++)
        {
            _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.position);
            _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.rotation);
        }

        for (int i = 0; i < _CardList.Count; i++)
        {
            _CardList[i]._Position = _PositionList[i];
            _CardList[i].transform.position = _PositionList[i];
            _CardList[i].transform.rotation = _RotationList[i];
            _CardList[i].transform.SetSiblingIndex(i + 1);
        }
    }

    private void CardCheckingFunction()
    {
        for (int i = 0; i < _CardList.Count - 2; i++)
        {
            if (_CardList[i]._cardType == _CardList[i + 1]._cardType && _CardList[i + 1]._cardType == _CardList[i + 2]._cardType)
            {
                StartCoroutine(DelayedSceneLoader(_CardList[i]._cardType));
            }
        }
    }

    private IEnumerator DelayedSceneLoader(CardType inType)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(inType.ToString());
    }
}
