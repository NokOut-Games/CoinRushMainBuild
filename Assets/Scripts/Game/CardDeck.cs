using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    [Header("Grabbing Other GameObject References:")]
    [SerializeField] public GameObject mCardHolderParent;
    public int clicks = 0;
    public List<Transform> _playerHandPoints;

    [Space(10)]
    [Header("Cards And Related Lists:")]
    [SerializeField] private List<ScriptedCards> mScriptedCards;
    [SerializeField] private List<Cards> _CardList = new List<Cards>();
    [SerializeField] public List<GameObject> mCardListGameObject;
    private List<Vector3> _PositionList = new List<Vector3>();
    private List<Vector3> _RotationList = new List<Vector3>();

    [Space(10)]
    [Header("Draw Button And Its States Images with conditions:")]
    [SerializeField] private Image DrawButton;
    [SerializeField] private Sprite drawNormal, drawAutomatic;
    [SerializeField] private RectTransform _drawButtonRectTransform;
    [Space(10)]
    [SerializeField] private int mMaxHoldTime = 5;
    [SerializeField] private float timeForCardAnimation = 2f;
    [SerializeField] private float time = 0, maxTime = 5;
    private bool mAutoCardDraw = false;
    private bool mAutomaticDrawModeOn = false;
    private bool mOnceDone = false;
    private bool canClick = true;

    public Image _drawButtonFillerImage;

    [Space(10)]
    [Header("Joker and related things")]
    public List<Cards> _jokerList;
    public bool onceDonee = false;

    public int mHowManyCardSetsAreActive;
    public List<Cards> _cardsThatCanBeReplacedByJoker;

    public List<GameObject> dummyCards;
    int positionNumber = 0;
    int newCardIndex = 0;

    public GameObject cardDeckAnimation2D;
    public GameObject cardDeckAnimation3D;
    public GameObject backToDeckAnimation3D;
    public GameObject blackOutScreen;
    public GameObject threeCardEffect;

    bool mMakeDrawBtnEnable = true;

    public LevelManager _levelManager;
    public BuildingManager _buildingManagerRef;

    ScriptedCards mCards;
    public bool mHasThreeCardMatch;
    int mThreeCardMatchIndex;
    bool mHasJoker;
    int mNumOfPairCards;
    public bool mJokerFindWithMultiCardPair;
    bool take_Multi_Card_Joker_Pair_Input;
    int[] mSelectionCards = new int[2];

    GameObject mFlotingJoker;

    [SerializeField] int mJokerProbability;
    public Multiplier _Multiplier;
    Tutorial tutorial;
    [SerializeField] Camera uIcam;
    [SerializeField] ParticleSystem drawBtnParticle;
    bool drawButtonClick => RectTransformUtility.RectangleContainsScreenPoint(_drawButtonRectTransform, Input.mousePosition, uIcam);
   [SerializeField] Transform[] jokerPairCardTransforms = new Transform[4];
    

    private void Start()
    {
        onceDonee = false;
        canClick = true;
        DrawButton.sprite = drawNormal;

        if (GameManager.Instance._SavedCardTypes.Count > 0)
        {
            foreach (int cardType in GameManager.Instance._SavedCardTypes)
            {
                InstantiateCard(GetScriptedCardWithCardType((CardType)cardType), true);
            }
        }
    }

    private void DestroyCardList()
    {
        int i = 8;
        backToDeckAnimation3D.SetActive(true);
        foreach (int cardType in GameManager.Instance._SavedCardTypes)
        {
            i--;
            backToDeckAnimation3D.transform.GetChild(i).GetComponent<Renderer>().material.mainTexture = GetScriptedCardWithCardType((CardType)cardType)._cardTex;
        }

        foreach (GameObject card in mCardListGameObject)
        {
            Destroy(card);
        }
        _CardList.Clear();
        _jokerList.Clear();
        newCardIndex = 0;
        mNumOfPairCards = 0;
        cardDeckAnimation2D.SetActive(false);
        mCardListGameObject.Clear();
        mMakeDrawBtnEnable = true;
        Invoke("BackToAnimSetToFalse", 1.6f);
        GameManager.Instance._SavedCardTypes.Clear();//Clear the card type list in gameManager
    }

    void BackToAnimSetToFalse()
    {
        backToDeckAnimation3D.SetActive(false);
    }

    private void Update()
    {
        

        if (clicks == 8 && !mHasThreeCardMatch && !mJokerFindWithMultiCardPair)
        {
            clicks = 0;
            Invoke(nameof(DestroyCardList), 2f);
        }

        time = Mathf.Clamp(time, 0f, mMaxHoldTime);
        //Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);

        if (take_Multi_Card_Joker_Pair_Input)
        {
           // Vector2 selectionCardPosOne = _CardList[mSelectionCards[0]].gameObject.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
           // Vector2 selectionCardPosTwo = _CardList[mSelectionCards[1]].gameObject.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (/*_CardList[mSelectionCards[0]].gameObject.GetComponent<RectTransform>().rect.Contains(selectionCardPosOne)*/RectTransformUtility.RectangleContainsScreenPoint(_CardList[mSelectionCards[0]].gameObject.GetComponent<RectTransform>(),Input.mousePosition,uIcam))
                {
                    SelectCardPairOfIndex(0, 1);
                }
                else if (/*_CardList[mSelectionCards[1]].gameObject.GetComponent<RectTransform>().rect.Contains(selectionCardPosTwo)*/RectTransformUtility.RectangleContainsScreenPoint(_CardList[mSelectionCards[1]].gameObject.GetComponent<RectTransform>(), Input.mousePosition, uIcam))
                {
                    SelectCardPairOfIndex(1, 0);
                }
            }
        }

        if (GameManager.Instance._energy > 0)
        {
            if (canClick)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (/*_drawButtonRectTransform.rect.Contains(localMousePosition)*/ drawButtonClick && DrawButton.gameObject.activeInHierarchy == true && mMakeDrawBtnEnable && !mAutoCardDraw)
                    {
                        mMakeDrawBtnEnable = false;

                        time = 0;
                        drawBtnParticle.Play();

                        DrawCard();
                    }
                    else
                    {
                        BackToNormalState();
                    }

                }

                if (!mOnceDone)
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (/*_drawButtonRectTransform.rect.Contains(localMousePosition)*/drawButtonClick)
                        {
                            if (!mAutomaticDrawModeOn)
                            {
                                //DrawButton.color = new Color32(200, 200, 200, 255);
                            }

                            time += Time.deltaTime;
                            var displayValue = Mathf.Lerp(0, 1, time / mMaxHoldTime);
                            _drawButtonFillerImage.fillAmount = displayValue;//Mathf.Lerp(0, 1, 3f * Time.fixedDeltaTime);


                            if (time >= mMaxHoldTime)
                            {
                                ChangeSprites();

                                mOnceDone = true;
                                mAutomaticDrawModeOn = true;
                                mAutoCardDraw = true;
                                StartCoroutine(AutomaticCardDrawing());
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    _drawButtonFillerImage.fillAmount = 0;
                    DrawButton.color = new Color32(255, 255, 255, 255);
                    if (/*_drawButtonRectTransform.rect.Contains(localMousePosition)*/drawButtonClick)
                    {
                        time = 0;
                    }

                }
            }
        }
    }

    void SelectCardPairOfIndex(int inSelectedIndex, int inUnSelectedIndex)
    {
        CardType MatchCardType = _CardList[mSelectionCards[inSelectedIndex] - 1]._cardType;
        if (MatchCardType == CardType.SHIELD)
        {
            Destroy(_CardList[mSelectionCards[inSelectedIndex] - 1].gameObject, 3.25f);
            Destroy(_CardList[mSelectionCards[inSelectedIndex]].gameObject, 3.25f);
            Destroy(mFlotingJoker, 3.25f);
            Invoke(nameof(DelayBalckOut), 3.25f);
            mHasJoker = false;
            mNumOfPairCards--;
            mMakeDrawBtnEnable = true;
        }
        else
        {
            mCardHolderParent.transform.parent.SetAsLastSibling();
        }
        Invoke("ThreeCardEffectActivate", 3.2f);

        _CardList[mSelectionCards[inSelectedIndex] - 1].PlayThreeCardMatchAnim(-320);
        mFlotingJoker.GetComponent<Cards>().PlayThreeCardMatchAnim(0, _CardList[mSelectionCards[inSelectedIndex]].gameObject.GetComponent<Image>().sprite);
        _CardList[mSelectionCards[inSelectedIndex]].PlayThreeCardMatchAnim(320);

        _CardList[mSelectionCards[inUnSelectedIndex] - 1].PlayJokerSelectionPairGetBackAnim();
        _CardList[mSelectionCards[inUnSelectedIndex]].PlayJokerSelectionPairGetBackAnim();
        _CardList.RemoveAt(mSelectionCards[inSelectedIndex] - 1);
        _CardList.RemoveAt(mSelectionCards[inSelectedIndex] - 1);
        GameManager.Instance._SavedCardTypes.RemoveAt(mSelectionCards[inSelectedIndex] - 1);
        GameManager.Instance._SavedCardTypes.RemoveAt(mSelectionCards[inSelectedIndex] - 1);
        StartCoroutine(DelayedSceneLoader(MatchCardType));
        clicks -= 3;
        take_Multi_Card_Joker_Pair_Input = false;
        ReplacementOfCards(true);
    }

    /// <summary>
    /// Brings Back Draw Button To Normal State from Automatic State
    /// </summary>
    public void BackToNormalState()
    {
        if (mAutomaticDrawModeOn)
        {
            _drawButtonFillerImage.fillAmount = 0;
            mAutomaticDrawModeOn = false;
            ChangeSprites();
            mOnceDone = false;
            mAutoCardDraw = false;
            StopCoroutine(AutomaticCardDrawing());
        }
    }

    ///// <summary>
    ///// Changes the sprite of Draw Button
    ///// </summary>
    private void ChangeSprites()
    {
        if (DrawButton.sprite == drawNormal)
        {
            DrawButton.color = new Color32(255, 255, 255, 255);
            DrawButton.sprite = drawAutomatic;
        }
        else if (DrawButton.sprite == drawAutomatic)
        {
            DrawButton.sprite = drawNormal;
        }
    }

    /// <summary>
    /// Automates the Card Drawing
    /// </summary>
    /// <returns></returns>
    private IEnumerator AutomaticCardDrawing()
    {
        //while (mAutoCardDraw)
        //{
        //    DrawCard();
        //    yield return new WaitForSeconds(timeForCardAnimation);
        //}
        while (mAutoCardDraw)
        {
            if (canClick)
                DrawCard();
            yield return new WaitForSeconds(timeForCardAnimation);
        }
    }

    /// <summary>
    /// Card Drawing Function
    /// </summary>
    /// 
    private void DrawCard()
    {
        if (_CardList.Count >= 8)
        {
            return;
        }
        GameManager.Instance._energy -= 1;

        Camera.main.GetComponent<CameraController>().DrawButtonClicked();

        if (!mHasJoker && Random.Range(0, 100) < mJokerProbability)
        {
            mCards = mScriptedCards[0];//card will be joker if no joker is there and the chance of getting joker is with percentage            
        }
        else
        {
            mCards = mScriptedCards[Random.Range(1, mScriptedCards.Count)];

        }
        cardDeckAnimation3D.GetComponent<Renderer>().material.mainTexture = mCards._cardTex;
        cardDeckAnimation3D.SetActive(true);
        blackOutScreen.SetActive(true);
        Invoke(nameof(Instantiate2DCard), .8f);
    }

    void Instantiate2DCard()
    {
        InstantiateCard(mCards);
        cardDeckAnimation3D.SetActive(false);
    }

    void InvokeTutorialClick()
    {
        if (tutorial != null)
            tutorial.RegisterUserAction();
    }



    public void InstantiateCard(ScriptedCards inCard, bool isSavedCard = false)
    {
        GameObject card = Instantiate(inCard._cardModel, _playerHandPoints[clicks].localPosition + Vector3.left * 1200, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
        if (!isSavedCard)
        {
            cardDeckAnimation2D.SetActive(true);
            cardDeckAnimation2D.GetComponent<CardDeckAnimation>().cardSprite = card.GetComponent<Image>().sprite;
            cardDeckAnimation2D.GetComponent<CardDeckAnimation>().SpriteChange();
        }
        Cards cardDetails = card.GetComponent<Cards>();

        cardDetails._cardType = inCard._cardType;
        cardDetails._cardID = inCard._cardID;
        cardDetails._Position = card.transform.position;

        clicks += 1;
        AddNewCard(card.GetComponent<Cards>(), card, isSavedCard);
        ReplacementOfCards(isSavedCard, isSavedCard ? 0 : .5f);
        CardCheckingFunction();
    }
    private void AddNewCard(Cards inNewCard, GameObject inCard, bool isSavedCard = false)
    {
        Invoke(nameof(InvokeTutorialClick), 1);// tutorial
        mCardListGameObject.Add(inCard);
        for (int i = 0; i < _CardList.Count; i++)
        {
            if (_CardList[i]._cardType == inNewCard._cardType && _CardList[i]._cardType != CardType.JOKER)
            {
                if (isSavedCard) { mNumOfPairCards++; } else { Invoke(nameof(TwoMatchCardAnimation), 1.1f); }
                if (mHasJoker) canClick = false;
                _CardList.Insert(i, inNewCard);

                newCardIndex = i;
                if (!isSavedCard) GameManager.Instance._SavedCardTypes.Insert(i, (int)inNewCard._cardType);//inserting card data to game Manager
                return;
            }
        }
        if (inNewCard._cardType == CardType.JOKER)
        {
            _jokerList.Add(inNewCard);
            mHasJoker = true;
            if (mNumOfPairCards == 1)
            {
                int pairIndex = GetTwoPairCardIndex()[0];
                Debug.Log("One Card Pair is there");
                if (mHasJoker) canClick = false;
                _CardList.Insert(pairIndex + 1, inNewCard);
                newCardIndex = pairIndex + 1;
                if (!isSavedCard) GameManager.Instance._SavedCardTypes.Insert(newCardIndex, (int)inNewCard._cardType);
                return;
            }
            else if (mNumOfPairCards >= 2)
            {
                Debug.Log("Two Card Pair is there");
                mJokerFindWithMultiCardPair = true;
                cardDeckAnimation2D.GetComponent<CardDeckAnimation>().OnJokerChooseAnimation();
                inNewCard.gameObject.SetActive(false);
                inNewCard.gameObject.transform.localPosition = new Vector3(0, 950, 0);
                inNewCard.gameObject.transform.localEulerAngles = Vector3.zero;
                mFlotingJoker = inNewCard.gameObject;
                Invoke(nameof(TwoPairCardWithJoker), 1f);
                return;
            }
            else if (mNumOfPairCards == 3)
            {
                Debug.Log("Three Card Pair is there");
                mJokerFindWithMultiCardPair = true;
                GetTwoPairCardIndex();
                return;
            }

        }
        newCardIndex = _CardList.Count;
        _CardList.Add(inNewCard);
        if (!isSavedCard) GameManager.Instance._SavedCardTypes.Add((int)inNewCard._cardType);//adding new card to gameManager
    }

    void TwoPairCardWithJoker()
    {
        blackOutScreen.SetActive(true);
        blackOutScreen.GetComponent<Animator>().SetBool("BlackOut", true);

        mFlotingJoker.SetActive(true);
        cardDeckAnimation2D.SetActive(false);
        cardDeckAnimation2D.transform.SetAsLastSibling();

        mSelectionCards[0] = GetTwoPairCardIndex()[0] + 1;
        mSelectionCards[1] = GetTwoPairCardIndex()[1] + 1;

        /*_CardList[GetTwoPairCardIndex()[0]].PlayJokerSelectionPairAnim(true, 0);
        _CardList[GetTwoPairCardIndex()[0] + 1].PlayJokerSelectionPairAnim(true, 1);
        _CardList[GetTwoPairCardIndex()[1]].PlayJokerSelectionPairAnim(false, 1);
        _CardList[GetTwoPairCardIndex()[1] + 1].PlayJokerSelectionPairAnim(false, 0);*/
        _CardList[GetTwoPairCardIndex()[0]].PlayJokerSelectionPairAnim(jokerPairCardTransforms[0]);
        _CardList[GetTwoPairCardIndex()[0] + 1].PlayJokerSelectionPairAnim(jokerPairCardTransforms[1]);
        _CardList[GetTwoPairCardIndex()[1]].PlayJokerSelectionPairAnim(jokerPairCardTransforms[2]);
        _CardList[GetTwoPairCardIndex()[1] + 1].PlayJokerSelectionPairAnim(jokerPairCardTransforms[3]);
        canClick = false;
        take_Multi_Card_Joker_Pair_Input = true;

    }


    void ThreeCardEffectActivate()
    {
        threeCardEffect.SetActive(true);
        Invoke("ThreeCardEffectDeActivate", 1f);
    }
    void ThreeCardEffectDeActivate()
    {
        threeCardEffect.SetActive(false);

    }

    List<int> GetTwoPairCardIndex()
    {
        List<int> pairIndexList = new List<int>();
        for (int i = 0; i < _CardList.Count - 1; i++)
        {
            if (_CardList[i]._cardType == _CardList[i + 1]._cardType) pairIndexList.Add(i);
        }
        return pairIndexList;
    }


    void TwoMatchCardAnimation()
    {
        if (mHasJoker && mNumOfPairCards == 0)// Already have a joker and now we have 1 pair of card
        {
            blackOutScreen.SetActive(true);
            blackOutScreen.GetComponent<Animator>().SetBool("BlackOut", true);


            CardType matchCardType = _CardList[newCardIndex]._cardType;
            _CardList[newCardIndex].PlayThreeCardMatchAnim(-320);
            _CardList[newCardIndex + 1].PlayThreeCardMatchAnim(320);
            int jokerIndex = FindJokerIndex();
            _CardList[jokerIndex].PlayThreeCardMatchAnim(0, _CardList[newCardIndex].gameObject.GetComponent<Image>().sprite);
            if (_CardList[newCardIndex]._cardType == CardType.SHIELD)
            {
                Destroy(_CardList[newCardIndex].gameObject, 3.25f);
                Destroy(_CardList[newCardIndex + 1].gameObject, 3.25f);
                Destroy(_CardList[jokerIndex].gameObject, 3.25f);
                Invoke(nameof(DelayBalckOut), 3.25f);
                mHasJoker = false;
               
            }
            else
            {
                mCardHolderParent.transform.parent.SetAsLastSibling();
            }
            Invoke("ThreeCardEffectActivate", 3.2f);

            _CardList.RemoveAt(jokerIndex);
            GameManager.Instance._SavedCardTypes.RemoveAt(jokerIndex);
            mHasThreeCardMatch = true;

            if (jokerIndex > newCardIndex)
            {
                _CardList.RemoveRange(newCardIndex, 2);
                GameManager.Instance._SavedCardTypes.RemoveRange(newCardIndex, 2);
            }
            else
            {
                _CardList.RemoveRange(newCardIndex - 1, 2);
                GameManager.Instance._SavedCardTypes.RemoveRange(newCardIndex - 1, 2);

            }
            clicks -= 3;
            ReplacementOfCards(true);
            StartCoroutine(DelayedSceneLoader(matchCardType));
        }
        else if (!mHasThreeCardMatch)
        {
            _CardList[newCardIndex].PlayTwoCardMatchAnim();
            _CardList[newCardIndex + 1].PlayTwoCardMatchAnim();
            mNumOfPairCards++;
        }

    }

    //Find  the joker index and return
    int FindJokerIndex()
    {
        for (int i = 0; i < _CardList.Count; i++)
        {
            if (_CardList[i]._cardType == CardType.JOKER) return i;
        }
        return 0;
    }

    private void ReplacementOfCards(bool endShuffle = false, float endDelay = 0.5f)
    {
        int medianIndex = _playerHandPoints.Count / 2;

        int incrementValue = 0;
        _PositionList.Clear();
        _RotationList.Clear();

        List<int> drawOrderArrange = new List<int>();
        for (int i = 0; i < _CardList.Count; i++)
        {
            if ((i % 2 == 0 || i == 0) && (clicks % 2 == 0))
            {
                if (i < 2)
                {
                    drawOrderArrange.Add(medianIndex + incrementValue + 1);
                    incrementValue++;
                }
                else
                {
                    drawOrderArrange.Add(medianIndex + incrementValue + 2);
                    incrementValue += 2;
                }
            }
            else if ((i % 2 == 0 || i == 0) && (clicks % 2 == 1))
            {
                drawOrderArrange.Add(medianIndex + incrementValue);
                incrementValue += 2;
            }
            else
            {
                drawOrderArrange.Add(medianIndex - incrementValue);
            }
        }
        drawOrderArrange.Sort();
        for (int i = 0; i < _CardList.Count; i++)
        {
            _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.localPosition);
            _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.localEulerAngles);
        }
        if (!endShuffle)
        {
            if (!mJokerFindWithMultiCardPair)
            {
                cardDeckAnimation2D.GetComponent<CardDeckAnimation>().PlayOnDropAnimation(_PositionList[newCardIndex], _RotationList[newCardIndex].z);
                Invoke(nameof(CardShufflingDelay), .6f);
                Invoke(nameof(CardGenerationDelay), 1f);
            }
        }
        else
        {
            Invoke(nameof(EndCardShuffle), endDelay);
        }

    }

    void EndCardShuffle()
    {
        for (int i = 0; i < _CardList.Count; i++)
        {
            _CardList[i]._Position = _PositionList[i];
            _CardList[i].transform.localPosition = _PositionList[i];
            _CardList[i].transform.localEulerAngles = _RotationList[i];
            _CardList[i].transform.SetSiblingIndex(i + 1);
        }
    }
    void CardShufflingDelay()
    {
        for (int i = 0; i < _CardList.Count; i++)
        {
            if (i != newCardIndex)
            {
                _CardList[i]._Position = _PositionList[i];
                _CardList[i].transform.localPosition = _PositionList[i];
                _CardList[i].transform.localEulerAngles = _RotationList[i];
                _CardList[i].transform.SetSiblingIndex(i + 1);
            }
            else
            {
                cardDeckAnimation2D.transform.SetSiblingIndex(i + 1); ;
            }
        }
    }

    void CardGenerationDelay()
    {
        _CardList[newCardIndex]._Position = _PositionList[newCardIndex];
        _CardList[newCardIndex].transform.localPosition = _PositionList[newCardIndex];
        _CardList[newCardIndex].transform.localEulerAngles = _RotationList[newCardIndex];
        _CardList[newCardIndex].transform.SetSiblingIndex(newCardIndex + 1);
        cardDeckAnimation2D.SetActive(false);


        blackOutScreen.SetActive(false);
        mMakeDrawBtnEnable = true;
    }

    private void CardCheckingFunction()
    {
        for (int i = 0; i < _CardList.Count - 2; i++)
        {
            if (!mHasJoker && (_CardList[i]._cardType == _CardList[i + 1]._cardType && _CardList[i + 1]._cardType == _CardList[i + 2]._cardType))
            {
                mThreeCardMatchIndex = i;
                mHasThreeCardMatch = true;
                canClick = false;
                CardType matchedCard = _CardList[i]._cardType;
                Invoke(nameof(PlayThreeCardAnimation), 1.5f);
                StartCoroutine(DelayedSceneLoader(matchedCard, 5f));
            }
            else if (mHasJoker && (_CardList[i]._cardType == _CardList[i + 2]._cardType && _CardList[i + 1]._cardType == CardType.JOKER))
            {

                Debug.Log("Pair with joker");
                mThreeCardMatchIndex = i;
                mHasThreeCardMatch = true;
                canClick = false;
                CardType matchedCard = _CardList[i]._cardType;
                Invoke(nameof(PlayThreeCardAnimation), 1.5f);
                
                mNumOfPairCards--;
                StartCoroutine(DelayedSceneLoader(matchedCard, 5f));
            }
        }
    }

    private void Shield()
    {      
        if (GameManager.Instance._shield <= GameManager.Instance._maxShield - 1)
        {
            int randomNumber = Random.Range(0, _buildingManagerRef._buildingData.Count);
            Debug.Log("Random Number: " + randomNumber);
            while (_buildingManagerRef._shieldedBuildings.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, _buildingManagerRef._buildingData.Count);
            }
            GameManager.Instance._shield += 1;
            _buildingManagerRef._shieldedBuildings.Add(randomNumber);
            _buildingManagerRef._buildingData[randomNumber].isBuildingShielded = true;
            GameManager.Instance.AddShieldToBuilding(randomNumber);
        }
        else
        {
            GameManager.Instance._energy += 3;
        }
        mHasThreeCardMatch = false;
    }

    void PlayThreeCardAnimation()
    {

        blackOutScreen.SetActive(true);
        blackOutScreen.GetComponent<Animator>().SetBool("BlackOut", true);

        _CardList[mThreeCardMatchIndex].PlayThreeCardMatchAnim(-320);//-350
        _CardList[mThreeCardMatchIndex + 1].PlayThreeCardMatchAnim(0, mHasJoker ? _CardList[mThreeCardMatchIndex].gameObject.GetComponent<Image>().sprite : null);
        _CardList[mThreeCardMatchIndex + 2].PlayThreeCardMatchAnim(320);
        mHasJoker = false;
        GameManager.Instance._SavedCardTypes.RemoveRange(mThreeCardMatchIndex, 3);
        if (_CardList[mThreeCardMatchIndex]._cardType == CardType.SHIELD)
        {
            Destroy(_CardList[mThreeCardMatchIndex].gameObject, 3.5f);
            Destroy(_CardList[mThreeCardMatchIndex + 1].gameObject, 3.5f);
            Destroy(_CardList[mThreeCardMatchIndex + 2].gameObject, 3.5f);
            Invoke(nameof(DelayBalckOut), 3.25f);
        }
        else
        {
            mCardHolderParent.transform.parent.SetAsLastSibling();
        }
        Invoke("ThreeCardEffectActivate", 3.2f);

        _CardList.RemoveRange(mThreeCardMatchIndex, 3);
        clicks -= 3;
        ReplacementOfCards(true);

    }

    void DelayBalckOut()
    {
        blackOutScreen.SetActive(false);
        mJokerFindWithMultiCardPair = false;

    }

    ScriptedCards GetScriptedCardWithCardType(CardType inCardType)
    {
        foreach (ScriptedCards scriptedCard in mScriptedCards)
        {
            if (scriptedCard._cardType == inCardType) return scriptedCard;
        }
        return null;
    }

    private IEnumerator DelayedSceneLoader(CardType inType, float delayTime = 3f)
    {
        yield return new WaitForSeconds(delayTime);
        if (inType == CardType.SHIELD)
        {
            Shield();
            canClick = true;
        }
        else
        {
            int waitTime = 3000;
            if (GameManager.Instance._energy >= 3)
            {
                _drawButtonRectTransform.parent.SetAsFirstSibling();
                _Multiplier.gameObject.SetActive(true);
                _Multiplier.AssignTutorial(tutorial);
                _Multiplier.transform.SetAsLastSibling();
                _Multiplier.InitiateMulitiplier();
                Destroy(_Multiplier.gameObject, 3.1f);
                waitTime = 3000;
            }
            if (mHasJoker || mJokerFindWithMultiCardPair)
            {
                waitTime += 500;
            }
            GameManager.Instance._IsBuildingFromFBase = true;

            LevelLoadManager.instance.LoadLevelASyncOf(inType.ToString(),waitTime);
        }
    }

    public void OpenCardAdder()
    {
        if (positionNumber > 4)
        {
            return;
        }

        for (int i = 0; i < _levelManager.OpenHandCardsPositions.Length; i++)
        {
            if (_levelManager.OpenHandCardsPositions[positionNumber].GetComponent<OpenCardFilled>().isOpenCardSlotFilled == false)
            {
                Instantiate(dummyCards[Random.Range(0, dummyCards.Count)], _levelManager.OpenHandCardsPositions[positionNumber].position, _levelManager.OpenHandCardsPositions[positionNumber].rotation, _levelManager.OpenHandCardsPositions[positionNumber]);
                break;
            }
        }

        positionNumber += 1;
    }


    public void AssignTutorial(Tutorial tutorial,CardType card)
    {
        this.tutorial = tutorial;
        Camera.main.GetComponent<CameraController>().DrawButtonClicked();

        if (card != CardType.JOKER)
        {
            ScriptedCards[] newCards = new ScriptedCards[2];
            newCards[0] = GetScriptedCardWithCardType(CardType.JOKER);
            newCards[1] = GetScriptedCardWithCardType(card);
            mScriptedCards.Clear();
            mScriptedCards.Add(newCards[0]);
            mScriptedCards.Add(newCards[1]);
            mJokerProbability = 0;

        }
    }


}