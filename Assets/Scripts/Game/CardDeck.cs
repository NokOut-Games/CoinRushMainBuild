using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum DrawButtonState
{
    NormalState,
    OpenCardState
}

public class CardDeck : MonoBehaviour
{
    [Header("Grabbing Other GameObject References:")]
    //[SerializeField] private GameManager mGameManager;
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

    public List<GameObject> _openCardPrefabs;
    public int _OpenCardNumberIndex;
    public int positionNumber = 0;
    public int _openedCardIndex;
    public int _openCardSlot;
    public List<int> _OpenCardSlotFilled;
    private int mCardsOpened;
    public List<Transform> _OpenCardTransformPoint;

    int newCardIndex = 0;

    public GameObject cardDeckAnimation2D;
    public GameObject cardDeckAnimation3D;
    public GameObject backToDeckAnimation3D;
    public GameObject blackOutScreen;
    public GameObject threeCardEffect;

    bool mMakeDrawBtnEnable = true;

    public LevelManager _levelManager;
    public BuildingManager _buildingManagerRef;
    public MultiplayerPlayerData mMultiplayerPlayerData;

    ScriptedCards mCards;
    public bool mHasThreeCardMatch;
    int mThreeCardMatchIndex;
    bool mHasJoker;
    int mNumOfPairCards;
    public bool mJokerFindWithMultiCardPair;
    bool take_Multi_Card_Joker_Pair_Input;
    int[] mSelectionCards = new int[2];

    GameObject mFlotingJoker;

    private OpenCards mOpenCards;
    private bool mOpenCardTakenAlready;

    [SerializeField] int mJokerProbability;

    private DrawButtonState mDrawButtonState;

    public Multiplier _Multiplier;
    Tutorial tutorial;
    [SerializeField] Camera uIcam;
    [SerializeField] ParticleSystem drawBtnParticle;
    bool drawButtonClick => RectTransformUtility.RectangleContainsScreenPoint(_drawButtonRectTransform, Input.mousePosition, uIcam) && !TutorialManager.Instance.isPopUpRunning;

    [SerializeField] Transform[] jokerPairCardTransforms = new Transform[4];



    private void Awake()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "OPENCARD")
        {
            mDrawButtonState = DrawButtonState.OpenCardState;
            Invoke(nameof(PopulateFriendsOpenCardSlotsFromFirebase), .5f);
        }
        else
        {
            mDrawButtonState = DrawButtonState.NormalState;
            PopulateOpenedCardSlotsFromFireBase(); //Removed Comment
        }
        //PopulateOpenedCardSlotsFromFireBase();
        //PopulateFriendsOpenCardSlotsFromFirebase();
    }

    private void Start()
    {
        mMultiplayerPlayerData = GameObject.Find("MultiplayerManager").GetComponent<MultiplayerPlayerData>();
        mOpenCards = GameObject.Find("OpenHandPointsParent").GetComponent<OpenCards>();

        if (mDrawButtonState == DrawButtonState.NormalState)
        {
            onceDonee = false;
            canClick = true;
            DrawButton.sprite = drawNormal;
            SpawnOpenCards();
            //mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (GameManager.Instance._SavedCardTypes.Count > 0)
            {
                //Camera.main.GetComponent<CameraController>().DrawButtonClicked();
                foreach (int cardType in GameManager.Instance._SavedCardTypes)
                {
                    InstantiateCard(GetScriptedCardWithCardType((CardType)cardType), true);
                }
            }
        }
        else
        {
            mOpenCardTakenAlready = false;
            Invoke(nameof(SpawnFriendsOpenCards),.5f);
        }
        
            
        

        //Invoke("OpenCard", .5f);
    }

    private void PopulateOpenedCardSlotsFromFireBase()
    {
        _OpenCardSlotFilled.Clear();
        for (int i = 0; i < GameManager.Instance.OpenCardDetails.Count; i++)
        {
            _OpenCardSlotFilled.Add(GameManager.Instance.OpenCardDetails[i]._openedCardSlot);
        }
    }

    private void PopulateFriendsOpenCardSlotsFromFirebase()
    {
        _OpenCardSlotFilled.Clear();
        for (int i = 0; i < mMultiplayerPlayerData.OpenCardDetails.Count; i++)
        {
            _OpenCardSlotFilled.Add(mMultiplayerPlayerData.OpenCardDetails[i]._openedCardSlot);
        }
    }

    private void SpawnOpenCards()
    {
        for (int i = 0; i < GameManager.Instance.OpenCardDetails.Count; i++)
        {
            GameObject savedOpenCard = Instantiate(_openCardPrefabs[GameManager.Instance.OpenCardDetails[i]._openedCardSelectedCard], mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].position, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].rotation, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]]);
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardSelectedCard = GameManager.Instance.OpenCardDetails[i]._openedCardSelectedCard;
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardPosition = GameManager.Instance.OpenCardDetails[i]._openedCardSlot;
        }
    }

    private void SpawnFriendsOpenCards()
    {
        for (int i = 0; i < mMultiplayerPlayerData.OpenCardDetails.Count; i++)
        {
            GameObject savedOpenCard = Instantiate(_openCardPrefabs[mMultiplayerPlayerData.OpenCardDetails[i]._openedCardSelectedCard], mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].position, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].rotation, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]]);
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardSelectedCard = mMultiplayerPlayerData.OpenCardDetails[i]._openedCardSelectedCard;
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardPosition = mMultiplayerPlayerData.OpenCardDetails[i]._openedCardSlot;
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
        //OpenCard
        //_OpenCardNumberIndex = mMultiplayerPlayerData._openCardInfo;
        //_OpenCardSlotFilled = MultiplayerManager.Instance.OpenedCardSlot;


        Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);
        if (mDrawButtonState == DrawButtonState.OpenCardState)
        {
            if (!mOpenCardTakenAlready)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_drawButtonRectTransform.rect.Contains(localMousePosition))
                    {
                        //mMakeDrawBtnEnable = false;

                        //time = 0;

                        OpenHandCardAdder();
                        mOpenCardTakenAlready = true;
                    }
                }
            }
        }
        if (mDrawButtonState == DrawButtonState.NormalState)
        {
            if (clicks == 8 && !mHasThreeCardMatch && !mJokerFindWithMultiCardPair)
            {
                clicks = 0;
                Invoke(nameof(DestroyCardList), 2f);
            }

            time = Mathf.Clamp(time, 0f, mMaxHoldTime);

            if (take_Multi_Card_Joker_Pair_Input)
            {
              /*  Vector2 selectionCardPosOne = _CardList[mSelectionCards[0]].gameObject.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
                Vector2 selectionCardPosTwo = _CardList[mSelectionCards[1]].gameObject.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);*/
                if (Input.GetMouseButtonDown(0))
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(_CardList[mSelectionCards[0]].gameObject.GetComponent<RectTransform>(), Input.mousePosition, uIcam))
                    {
                        SelectCardPairOfIndex(0, 1);
                    }
                    else if (RectTransformUtility.RectangleContainsScreenPoint(_CardList[mSelectionCards[1]].gameObject.GetComponent<RectTransform>(), Input.mousePosition, uIcam))
                    {
                        SelectCardPairOfIndex(1, 0);
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && drawButtonClick && DrawButton.gameObject.activeInHierarchy == true) drawBtnParticle.Play();

            if (GameManager.Instance._energy > 0)
            {
                if (canClick)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (drawButtonClick && DrawButton.gameObject.activeInHierarchy == true && mMakeDrawBtnEnable && !mAutoCardDraw)
                        {
                            mMakeDrawBtnEnable = false;
                            time = 0;
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
                            if (drawButtonClick)
                            {
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
                        if (drawButtonClick)
                        {
                            time = 0;
                        }

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

    private IEnumerator AutomaticCardDrawing()
    {
        while (mAutoCardDraw)
        {
            if (canClick && mMakeDrawBtnEnable)
            {
                mMakeDrawBtnEnable = false;
                DrawCard();
            }
            yield return new WaitForSeconds(timeForCardAnimation);
        }
    }

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

    public void OpenHandCardAdder()
    {
        Camera.main.GetComponent<CameraController>().DrawButtonClicked();
        if (positionNumber == 4)
        {
            positionNumber = 0;
        }
        if (_OpenCardSlotFilled.Count != 5)
        {
            positionNumber = 0;
            while (_OpenCardSlotFilled.Contains(positionNumber))
            {
                positionNumber += 1;
            }
            if (mCardsOpened < 1)
            {
                // int RandomCard = Random.Range(0, _openCardPrefabs.Count);
                _openedCardIndex = Random.Range(0, _openCardPrefabs.Count);
                GameObject OpenCards = Instantiate(_openCardPrefabs[_openedCardIndex], mOpenCards._OpenCardTransformPoint[positionNumber].position, mOpenCards._OpenCardTransformPoint[positionNumber].rotation, mOpenCards._OpenCardTransformPoint[positionNumber]);
                OpenCards.GetComponent<OpenCardSelector>()._OpenCardSelectedCard = _openedCardIndex;
                OpenCards.GetComponent<OpenCardSelector>()._OpenCardPosition = positionNumber;
                _openCardSlot = positionNumber;
                _OpenCardNumberIndex += 1;
                _OpenCardSlotFilled.Add(positionNumber);
                mCardsOpened += 1;
            }
        }
        else
        {
            Debug.Log("Open Hand Card Slot Filled");
        }
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

            LevelLoadManager.instance.LoadLevelASyncOf(inType.ToString(), waitTime);
        }
    }
    public void AssignTutorial(Tutorial tutorial, CardType card)
    {
        this.tutorial = tutorial;
        Camera.main.GetComponent<CameraController>().DrawButtonClicked();
        Camera.main.GetComponent<CameraController>()._buildButtonClicked = false;
        Camera.main.GetComponent<CameraController>()._inBetweenConstructionProcess = false;
        mCardHolderParent.SetActive(true);
        if (card == CardType.JOKER) return;
        ScriptedCards[] newCards = new ScriptedCards[2];
        newCards[0] = GetScriptedCardWithCardType(CardType.JOKER);
        newCards[1] = GetScriptedCardWithCardType(card);
        mScriptedCards.Clear();
        mScriptedCards.Add(newCards[0]);
        mScriptedCards.Add(newCards[1]);
        mJokerProbability = 0;
    }
}


//else
//{
//    type = _CardList[j]._cardType;
//    count1 = 1;
//}

//if (_jokerList.Count == 1)
//{
//    if (onceDonee == true)
//    {
//        return;
//    }
//    CardType type = _CardList[0]._cardType;
//    int count = 1;
//    for (int j = 1; j < _CardList.Count; j++)
//    {
//        if (_CardList[j]._cardType == type)
//        {
//            count++;
//            if (count == 2)
//            {
//                mHowManyCardSetsAreActive += 1;
//                _cardsThatCanBeReplacedByJoker.Add(_CardList[j]);
//                onceDonee = true;
//            }
//        }
//        else
//        {
//            type = _CardList[j]._cardType;
//            count = 1;
//        }
//    }
//    JokerChecking(mHowManyCardSetsAreActive);
//}

//void JokerChecking(int inHowManyCardSets)
//{
//    switch (inHowManyCardSets)
//    {
//        case 1: //If Only One Set of similar Cards at that time
//            for (int i = 0; i < _cardsThatCanBeReplacedByJoker.Count; i++)
//            {
//                _jokerList[0]._cardType = _cardsThatCanBeReplacedByJoker[0]._cardType;
//                //AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject);
//                ReplacementOfCards();
//                CardCheckingFunction();
//            }
//            break;
//        case 2: //If Two Sets of Similar Card are active at that time
//            int j = 0;
//            for (int i = 0; i < _cardsThatCanBeReplacedByJoker.Count; i++, j += 400)
//            {
//                Cards twoSets = Instantiate(_cardsThatCanBeReplacedByJoker[i], _playerHandPoints[0].position + new Vector3(j, 500, 0), Quaternion.identity, mCardHolderParent.transform);
//                twoSets.transform.gameObject.AddComponent<Button>();
//                twoSets.transform.gameObject.GetComponent<Button>().onClick.AddListener(() => { _jokerList[0]._cardType = twoSets._cardType; /*AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject);*/ ReplacementOfCards(); CardCheckingFunction(); });
//            }
//            break;
//        case 3: //If Three Sets of Similar CardType are Active at that time
//            int k = 0;
//            for (int i = 0; i < _cardsThatCanBeReplacedByJoker.Count; i++, k += 300)
//            {
//                Cards threeSets = Instantiate(_cardsThatCanBeReplacedByJoker[i], _playerHandPoints[0].position + new Vector3(k, 500, 0), Quaternion.identity, mCardHolderParent.transform);
//                threeSets.transform.gameObject.AddComponent<Button>();
//                threeSets.transform.gameObject.GetComponent<Button>().onClick.AddListener(() => { _jokerList[0]._cardType = threeSets._cardType; /*AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject);*/ ReplacementOfCards(); CardCheckingFunction(); });
//            }
//            break;
//        #region FutureCase
//        //case 4:
//        //    int l = 0;
//        //    for (int i = 0; i < _cardsThatCanBeReplacedByJoker.Count; i++, l += 300)
//        //    {
//        //        Cards fourSets = Instantiate(_cardsThatCanBeReplacedByJoker[i], _playerHandPoints[1].position + new Vector3(l, 400, 0), Quaternion.identity, mCardHolderParent.transform);
//        //        fourSets.transform.gameObject.AddComponent<Button>();
//        //        fourSets.transform.gameObject.GetComponent<Button>().onClick.AddListener(() => { _jokerList[0]._cardType = fourSets._cardType; AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject); ReplacementOfCards(); CardCheckingFunction(); });
//        //    }
//        //    break;
//        #endregion
//        default:
//            break;
//    }
//}

#region Old CardDeck
//[Header ("Grabbing Other GameObject References:")]
//[SerializeField] private GameManager mGameManager;
//[SerializeField] private GameObject mCardHolderParent;
//private int clicks = 0;
//public List<Transform> _playerHandPoints;

//[Space(10)]
//[Header ("Cards And Related Lists:")]
//[SerializeField] private List<ScriptedCards> mScriptedCards;
//[SerializeField] private List<Cards> _CardList = new List<Cards>();
//[SerializeField] private List<GameObject> mCardListGameObject;
//private List<Vector3> _PositionList = new List<Vector3>();
//private List<Quaternion> _RotationList = new List<Quaternion>();

//[Space(10)]
//[Header ("Draw Button And Its States Images with conditions:")]
//[SerializeField] private Image DrawButton;
//[SerializeField] private Sprite drawNormal, drawAutomatic;
//[SerializeField] private RectTransform _drawButtonRectTransform;
//[Space(10)]
//[SerializeField] private int mMaxHoldTime = 5;
//[SerializeField] private float timeForCardAnimation = 2f;
//private float time = 0;
//private bool mAutoCardDraw = false;
//private bool mAutomaticDrawModeOn = false;
//private bool mOnceDone = false;

//[Space(10)]
//[Header ("Joker and related things")]
//public List<Cards> _jokerList;
//public bool onceDonee = false;


//private void Start()
//{
//    onceDonee = false;
//    DrawButton.sprite = drawNormal;
//    mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//}

//private void Update()
//{
//    if (clicks == 8)
//    {
//        clicks = 0;
//        foreach (GameObject card in mCardListGameObject)
//        {
//            Destroy(card);
//        }
//        _CardList.Clear();
//        mCardListGameObject.Clear();
//    }

//    if(_jokerList.Count >= 1)
//    {
//        if(onceDonee == true)
//        {
//            return;
//        }
//        CardType type = _CardList[0]._cardType;
//        int count = 1;
//        for (int j = 1; j < _CardList.Count; j++)
//        {
//            if (_CardList[j]._cardType == type)
//            {
//                count++;
//                if (count == 2)
//                {
//                    JokerStuff(j);
//                    onceDonee = true;
//                }
//            }
//            else
//            {
//                type = _CardList[j]._cardType;
//                count = 1;
//            }
//        }
//    }
//    #region Button Function
//    time = Mathf.Clamp(time,0f,5f);
//    Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);

//    if (Input.GetMouseButtonDown(0))
//    {
//        if (_drawButtonRectTransform.rect.Contains(localMousePosition))
//        {
//            BackToNormalState();
//            time = 0;
//            DrawCard();
//        }
//    }

//    if (!mOnceDone)
//    {
//        if (Input.GetMouseButton(0))
//        {
//            if (_drawButtonRectTransform.rect.Contains(localMousePosition))
//            {
//                time += Time.fixedDeltaTime;
//                if (time >= mMaxHoldTime)
//                {
//                    mOnceDone = true;
//                    mAutomaticDrawModeOn = true;
//                    mAutoCardDraw = true;
//                    ChangeSprites();
//                    StartCoroutine(AutomaticCardDrawing());
//                }
//            }
//        }
//    }

//    if (Input.GetMouseButtonUp(0))
//    {
//        if (_drawButtonRectTransform.rect.Contains(localMousePosition))
//        {
//            time = 0;
//        }
//    }
//    #endregion
//}

//void JokerStuff(int j)
//{
//    _jokerList[0]._cardType = _CardList[j]._cardType;
//    AddNewCard(_jokerList[0].transform.GetComponent<Cards>(), _jokerList[0].transform.gameObject);
//    ReplacementOfCards();
//    CardCheckingFunction();
//}
//public void BackToNormalState()
//{
//    if (mAutomaticDrawModeOn)
//    {
//        mAutomaticDrawModeOn = false;
//        ChangeSprites();
//        mOnceDone = false;
//        mAutoCardDraw = false;
//        StopCoroutine(AutomaticCardDrawing());
//    }
//}

//private void ChangeSprites()
//{
//    if (DrawButton.sprite == drawNormal)
//    {
//        DrawButton.sprite = drawAutomatic;
//    }
//    else if (DrawButton.sprite == drawAutomatic)
//    {
//        DrawButton.sprite = drawNormal;
//    }
//}

//private void DrawCard()
//{
//    if (_CardList.Count >= 8)
//    {
//        return;
//    }
//    mGameManager._energy -= 1;

//    Camera.main.GetComponent<CameraController>().DrawButtonClicked();

//    ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

//    GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
//    Cards cardDetails = card.GetComponent<Cards>();

//    cardDetails._cardType = cards._cardType;
//    cardDetails._cardID = cards._cardID;
//    cardDetails._Position = card.transform.position;

//    clicks += 1;
//    AddNewCard(card.GetComponent<Cards>(),card);
//    ReplacementOfCards();
//    CardCheckingFunction();
//}

//private IEnumerator AutomaticCardDrawing()
//{
//    while (mAutoCardDraw)
//    {
//        DrawCard();
//        yield return new WaitForSeconds(timeForCardAnimation);
//    }
//}

//private void AddNewCard(Cards inNewCard , GameObject inCard)
//{
//    mCardListGameObject.Add(inCard);
//    for (int i = 0; i < _CardList.Count; i++)
//    {

//        if (_CardList[i]._cardType == inNewCard._cardType)
//        {
//            _CardList.Insert(i, inNewCard);
//            return;
//        }

//    }
//    if (inNewCard._cardType == CardType.JOKER)
//    {
//        _jokerList.Add(inNewCard);
//    }
//    _CardList.Add(inNewCard);
//}

//private void ReplacementOfCards()
//{
//    int medianIndex = _playerHandPoints.Count / 2;

//    int incrementValue = 0;
//    _PositionList.Clear();
//    _RotationList.Clear();

//    List<int> drawOrderArrange = new List<int>();

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        if (i % 2 == 0 || i == 0)
//        {
//            drawOrderArrange.Add(medianIndex + incrementValue);
//            incrementValue++;
//        }
//        else
//        {
//            drawOrderArrange.Add(medianIndex - incrementValue);
//        }
//    }

//    drawOrderArrange.Sort();

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.position);
//        _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.rotation);
//    }

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        _CardList[i]._Position = _PositionList[i];
//        _CardList[i].transform.position = _PositionList[i];
//        _CardList[i].transform.rotation = _RotationList[i];
//        _CardList[i].transform.SetSiblingIndex(i + 1);
//    }
//}

//private void CardCheckingFunction()
//{
//    for (int i = 0; i < _CardList.Count - 2; i++)
//    {
//        if (_CardList[i]._cardType == _CardList[i + 1]._cardType && _CardList[i + 1]._cardType == _CardList[i + 2]._cardType)
//        {
//            StartCoroutine(DelayedSceneLoader(_CardList[i]._cardType));
//        }
//    }
//}

//private IEnumerator DelayedSceneLoader(CardType inType)
//{
//    yield return new WaitForSeconds(2);
//    SceneManager.LoadScene(inType.ToString());
//}
#endregion


//void UnJokerVersion()
//{
//    [Header("Grabing Other GameObject References")]
//    [SerializeField] private GameManager mGameManager;
//    [SerializeField] private GameObject mCardHolderParent;
//    [SerializeField] private List<ScriptedCards> mScriptedCards;
//    [SerializeField] private List<GameObject> mCardListGameObject;
//    [SerializeField] private int mMaxHoldTime = 5;
//    [SerializeField] private float time = 0;
//    [SerializeField] private Image DrawButton;
//    [SerializeField] private Sprite drawNormal, drawAutomatic;
//    [SerializeField] private RectTransform _drawButtonRectTransform;
//    [SerializeField] private float timeForCardAnimation = 2f;

//    private List<Vector3> _PositionList = new List<Vector3>();
//private List<Quaternion> _RotationList = new List<Quaternion>();
//private bool mAutoCardDraw = false;
//private bool mAutomaticDrawModeOn = false;
//private bool mOnceDone = false;
//private int clicks = 0;

//public List<Cards> _CardList = new List<Cards>();
//public List<Transform> _playerHandPoints;





//private void Start()
//{
//    DrawButton.sprite = drawNormal;
//    mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//}

//private void Update()
//{
//    if (clicks == 8)
//    {
//        clicks = 0;
//        foreach (GameObject card in mCardListGameObject)
//        {
//            Destroy(card);
//        }
//        _CardList.Clear();
//        mCardListGameObject.Clear();
//    }

//    time = Mathf.Clamp(time, 0f, 5f);
//    Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);

//    if (Input.GetMouseButtonDown(0))
//    {
//        if (_drawButtonRectTransform.rect.Contains(localMousePosition))
//        {
//            BackToNormalState();
//            time = 0;
//            DrawCard();
//        }
//    }

//    if (!mOnceDone)
//    {
//        if (Input.GetMouseButton(0))
//        {
//            if (_drawButtonRectTransform.rect.Contains(localMousePosition))
//            {
//                time += Time.fixedDeltaTime;
//                if (time >= mMaxHoldTime)
//                {
//                    mOnceDone = true;
//                    mAutomaticDrawModeOn = true;
//                    mAutoCardDraw = true;
//                    ChangeSprites();
//                    StartCoroutine(AutomaticCardDrawing());
//                }
//            }
//        }
//    }

//    if (Input.GetMouseButtonUp(0))
//    {
//        if (_drawButtonRectTransform.rect.Contains(localMousePosition))
//        {
//            time = 0;
//        }
//    }
//}

//public void BackToNormalState()
//{
//    if (mAutomaticDrawModeOn)
//    {
//        mAutomaticDrawModeOn = false;
//        ChangeSprites();
//        mOnceDone = false;
//        mAutoCardDraw = false;
//        StopCoroutine(AutomaticCardDrawing());
//    }
//}

//private void ChangeSprites()
//{
//    if (DrawButton.sprite == drawNormal)
//    {
//        DrawButton.sprite = drawAutomatic;
//    }
//    else if (DrawButton.sprite == drawAutomatic)
//    {
//        DrawButton.sprite = drawNormal;
//    }
//}

//private void DrawCard()
//{
//    if (_CardList.Count >= 8)
//    {
//        return;
//    }
//    mGameManager._energy -= 1;

//    Camera.main.GetComponent<CameraController>().DrawButtonClicked();

//    ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

//    GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
//    Cards cardDetails = card.GetComponent<Cards>();

//    cardDetails._cardType = cards._cardType;
//    cardDetails._cardID = cards._cardID;
//    cardDetails._Position = card.transform.position;

//    clicks += 1;
//    AddNewCard(card.GetComponent<Cards>(), card);
//    ReplacementOfCards();
//    //CardCheckingFunction();
//}

//private IEnumerator AutomaticCardDrawing()
//{
//    while (mAutoCardDraw)
//    {
//        DrawCard();
//        yield return new WaitForSeconds(timeForCardAnimation);
//    }
//}

//private void AddNewCard(Cards inNewCard, GameObject inCard)
//{
//    mCardListGameObject.Add(inCard);
//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        if (_CardList[i]._cardType == inNewCard._cardType)
//        {
//            _CardList.Insert(i, inNewCard);
//            return;
//        }
//    }
//    _CardList.Add(inNewCard);
//}

//private void ReplacementOfCards()
//{
//    int medianIndex = _playerHandPoints.Count / 2;

//    int incrementValue = 0;
//    _PositionList.Clear();
//    _RotationList.Clear();

//    List<int> drawOrderArrange = new List<int>();

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        if (i % 2 == 0 || i == 0)
//        {
//            drawOrderArrange.Add(medianIndex + incrementValue);
//            incrementValue++;
//        }
//        else
//        {
//            drawOrderArrange.Add(medianIndex - incrementValue);
//        }
//    }

//    drawOrderArrange.Sort();

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.position);
//        _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.rotation);
//    }

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        _CardList[i]._Position = _PositionList[i];
//        _CardList[i].transform.position = _PositionList[i];
//        _CardList[i].transform.rotation = _RotationList[i];
//        _CardList[i].transform.SetSiblingIndex(i + 1);
//    }
//}

//private void CardCheckingFunction()
//{
//    for (int i = 0; i < _CardList.Count - 2; i++)
//    {
//        if (_CardList[i]._cardType == _CardList[i + 1]._cardType && _CardList[i + 1]._cardType == _CardList[i + 2]._cardType)
//        {
//            StartCoroutine(DelayedSceneLoader(_CardList[i]._cardType));
//        }
//    }
//}

//private IEnumerator DelayedSceneLoader(CardType inType)
//{
//    yield return new WaitForSeconds(2);
//    SceneManager.LoadScene(inType.ToString());
//}
//}























//void OldVersion()
//{
//    [SerializeField] private GameManager mGameManager;
//    [SerializeField] private GameObject mCardHolderParent;
//    private int clicks = 0;
//private int mk;

//[SerializeField] public List<ScriptedCards> mScriptedCards;
//public List<Cards> _CardList = new List<Cards>();
//public List<Transform> _playerHandPoints;
//[HideInInspector] public List<Vector3> _PositionList = new List<Vector3>();
//[HideInInspector] public List<Quaternion> _RotationList = new List<Quaternion>();

//#region CardMarch3 Version-1
//public List<Cards> AttackList;
//public List<Cards> StealList;
//public List<Cards> ShieldList;
//public List<Cards> JokerList;
//public List<Cards> EnergyList;
//public List<Cards> CoinsList;
//public List<Cards> FortuneList;
//public List<Cards> SpinList;
//#endregion



//private void Start()
//{
//    mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//}



///// <summary>
///// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
///// 1.Reduce the Energy.
///// 2.Zoom to the gameplay location
///// 3.Have a way to access the card location and spawn card at their respective positions in an inverted U-Shape
///// </summary>
//public void DrawCard()
//{
//    //ChangeSprites();
//    if (_CardList.Count >= 8)
//        return;

//    mGameManager._energy -= 1;

//    Camera.main.GetComponent<CameraController>().DrawButtonClicked();

//    ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

//    GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
//    Cards cardDetails = card.GetComponent<Cards>();

//    cardDetails._cardType = cards._cardType;
//    cardDetails._cardID = cards._cardID;
//    cardDetails._Position = card.transform.position;

//    clicks += 1;
//    AddNewCard(card.GetComponent<Cards>());
//    ReplacementOfCards();
//    CardCheckingFunction();
//}

//private void Update()
//{
//    if (clicks == 8)
//    {
//        clicks = 0;
//    }
//}

///// <summary>
///// </This function is used to allign the picked cards in sorted order>
//public void AddNewCard(Cards inNewCard)
//{
//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        if (_CardList[i]._cardType == inNewCard._cardType)
//        {
//            _CardList.Insert(i, inNewCard);
//            return;
//        }
//    }
//    _CardList.Add(inNewCard);
//}

//public void ReplacementOfCards()
//{
//    int medianIndex = _playerHandPoints.Count / 2;

//    int incrementValue = 0;
//    _PositionList.Clear();
//    _RotationList.Clear();

//    List<int> drawOrderArrange = new List<int>();

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        if (i % 2 == 0 || i == 0)
//        {
//            drawOrderArrange.Add(medianIndex + incrementValue);
//            incrementValue++;
//        }
//        else
//        {
//            drawOrderArrange.Add(medianIndex - incrementValue);
//        }
//    }

//    drawOrderArrange.Sort();

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.position);
//        _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.rotation);
//    }

//    for (int i = 0; i < _CardList.Count; i++)
//    {
//        _CardList[i]._Position = _PositionList[i];
//        _CardList[i].transform.position = _PositionList[i];
//        _CardList[i].transform.rotation = _RotationList[i];
//        _CardList[i].transform.SetSiblingIndex(i + 1);
//    }
//}

//void CardCheckingFunction()
//{
//    for (int i = 0; i < _CardList.Count - 2; i++)
//    {
//        if (_CardList[i]._cardType == _CardList[i + 1]._cardType && _CardList[i + 1]._cardType == _CardList[i + 2]._cardType)
//        {
//            StartCoroutine(DelayedSceneLoader(_CardList[i]._cardType));
//        }
//    }
//}

//IEnumerator DelayedSceneLoader(CardType inType)
//{
//    yield return new WaitForSeconds(2);
//    SceneManager.LoadScene(inType.ToString());
//}
//}



// Algorithm for auto draw
// Method - 1
// have a while which when true does the draw card continously

// But before that we should write an algorithm on how to get the hold working for our button and tap for our button
// And have a timer which when holded down for more than a particular second do auto and if not do normal.

//void drawButtonChange()
//{
//      public Image DrawButton;
//      public Sprite drawNormal, drawAutomatic;

// //DrawButton.sprite = drawNormal;

//    //void ChangeSprites()
//    //{
//    //    if (DrawButton.sprite == drawNormal)
//    //    {
//    //        DrawButton.sprite = drawAutomatic;
//    //    }
//    //    else if (DrawButton.sprite == drawAutomatic)
//    //    {
//    //        DrawButton.sprite = drawNormal;
//    //    }
//    //}
//}


















//void Residue()
//{

/// <summary>
/// This Function will Trigger the Scene if 3 Cards Matches
/// </summary>
//void CardTrigger()
//{
//    switch (_CardList[mk]._cardType)
//    {
//        case CardType.ATTACK:
//            SceneManager.LoadScene(1);
//            break;
//        case CardType.COINS:
//            SceneManager.LoadScene(2);
//            break;
//        case CardType.ENERGY:
//            SceneManager.LoadScene(3);
//            break;
//        case CardType.FORTUNEWHEEL:
//            SceneManager.LoadScene(4);
//            break;
//        case CardType.SLOTMACHINE:
//            SceneManager.LoadScene(5);
//            break;
//        case CardType.JOKER:
//            Debug.Log("Joker");
//            break;
//        case CardType.SHIELD:
//            Debug.Log("Shield + 1");
//            break;
//    }
//}

//#region Try-2 "Understood the code. Seems a better choice(Code Took from Internet)"
//if (_CardList.Count > 2)
//{
//    CardType type = _CardList[0]._cardType;
//    int count = 1;
//    for (int i = 1; i < _CardList.Count; i++)
//    {
//        if (_CardList[i]._cardType == type)
//        {
//            count++;
//            if (count == 3)
//            {
//                StartCoroutine(DelayedSceneLoader(type));
//            }
//        }
//        else
//        {
//            type = _CardList[i]._cardType;
//            count = 1;
//        }
//     }
//}
//#endregion

//int j = i;
//int k = i + 1;
//int l = i + 2;
//if (j >= _CardList.Count || k >= _CardList.Count || l >= _CardList.Count)
//{
//    return;
//}

//#region CardMarch3 Version-1
//switch (inNewCard._cardType)
//{
//    case CardType.ATTACK: AttackList.Add(inNewCard);
//        break;
//    case CardType.STEAL: StealList.Add(inNewCard);
//        break;
//    case CardType.SHIELD: ShieldList.Add(inNewCard);
//        break;
//    case CardType.JOKER: JokerList.Add(inNewCard);
//        break;
//    case CardType.ENERGY: EnergyList.Add(inNewCard);
//        break;
//    case CardType.COINS: CoinsList.Add(inNewCard);
//        break;
//    case CardType.FORTUNEWHEEL: FortuneList.Add(inNewCard);
//        break;
//    case CardType.SLOTMACHINE: SpinList.Add(inNewCard);
//        break;
//    default:
//        break;

//}
//#endregion

/// <summary>
/// In this function Checking that any of  three cards are same then its name will be displayed
/// </summary>

//if (_CardList[0]._cardID == _CardList[1]._cardID && _CardList[0]._cardID == _CardList[2]._cardID)
//{
//    mk = 1;
//    Invoke("CardTrigger", 1.5f);
//}
//else if (_CardList[1]._cardID == _CardList[2]._cardID && _CardList[1]._cardID == _CardList[3]._cardID)
//{
//    mk = 2;
//    Invoke("CardTrigger", 1.5f);
//}
//else if (_CardList[2]._cardID == _CardList[3]._cardID && _CardList[2]._cardID == _CardList[4]._cardID)
//{
//    mk = 3;
//    Invoke("CardTrigger", 1.5f);
//}
//else if (_CardList[3]._cardID == _CardList[4]._cardID && _CardList[3]._cardID == _CardList[5]._cardID)
//{
//    mk = 4;
//    Invoke("CardTrigger", 1.5f);
//}
//else if (_CardList[4]._cardID == _CardList[5]._cardID && _CardList[4]._cardID == _CardList[6]._cardID)
//{
//    mk = 5;
//    Invoke("CardTrigger", 1.5f);
//}
//else if (_CardList[5]._cardID == _CardList[6]._cardID && _CardList[5]._cardID == _CardList[7]._cardID)
//{
//    mk = 6;
//    Invoke("CardTrigger", 1.5f);
//}

//void CardTrigger()
//{
//    switch (_CardList[mk]._cardType)
//    {
//        case CardType.ATTACK:
//            SceneManager.LoadScene(1);
//            break;
//        case CardType.COINS:
//            SceneManager.LoadScene(2);
//            break;
//        case CardType.ENERGY:
//            SceneManager.LoadScene(3);
//            break;
//        case CardType.FORTUNEWHEEL:
//            SceneManager.LoadScene(4);
//            break;
//        case CardType.SLOTMACHINE:
//            SceneManager.LoadScene(5);
//            break;
//        case CardType.JOKER:
//            Debug.Log("Joker");
//            break;
//        case CardType.SHIELD:
//            Debug.Log("Shield + 1");
//            break;
//    }
//}
//void CardMatchThreeChecker(Cards inNewCard)
//{
//    //#region CardMarch3 Version-1
//    if (AttackList.Count == 3)
//    {
//        SceneManager.LoadScene(1);
//    }
//    if (CoinsList.Count == 3)
//    {
//        SceneManager.LoadScene(2);
//    }
//    if (EnergyList.Count == 3)
//    {
//        SceneManager.LoadScene(3);
//    }
//    if (FortuneList.Count == 3)
//    {
//        SceneManager.LoadScene(4);
//    }
//    if (SpinList.Count == 3)
//    {
//        SceneManager.LoadScene(5);
//    }
//    //#endregion
//    //if(_CardList.Contains(inNewCard))
//    //{

//    //}
//}

//void LoadScene()
//{
//    SceneManager.LoadScene(_CardList[i]._cardType.ToString());
//}




//[SerializeField] private GameObject mCam;
//[SerializeField] private Transform mDeckCardCamPosition;
//[SerializeField] private LevelManagerUI mlevelManagerUI;

//    [SerializeField] private Camera mCam;
//    [SerializeField] private Transform mDeckCardCamPosition;

//    /// <summary>
//    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
//    /// </summary>
//    public void DrawCard()
//    {
//        ZoomCameraToPlayArea();
//    }

//    private void ZoomCameraToPlayArea()
//    {
//        mCam.transform.position = mDeckCardCamPosition.position;
//        mCam.transform.rotation = mDeckCardCamPosition.transform.rotation;
//    }

//GameObject card = Instantiate(mCards.boosterCards[Random.Range(0, mCards.boosterCards.Count)]._cardModel, _handPoints[clicks].transform.position, Quaternion.Euler(0, 180f, 0f));
////_handPoints[clicks].isFilled = true;
//switch (card.tag)
//{
//    case "5K Coins":
//        mlevelManagerUI._fiveThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "25K Coins":
//        mlevelManagerUI._twentyFiveThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "100K Coins":
//        mlevelManagerUI._hunderThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "500K Coins":
//        mlevelManagerUI._fiveHundredThousandCoinList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "1M Coins":
//        mlevelManagerUI._OneMillionJackPotCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "10 EC":
//        mlevelManagerUI._TenEnergyCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "25 EC":
//        mlevelManagerUI._TwentyFiveEnergyCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "100 EC":
//        mlevelManagerUI._HundredEnergyCardList.Add(card);
//        //levelManagerUI.OverAllCards.Add(card);
//        break;
//    case "Attack":
//        mlevelManagerUI._AttackCardList.Add(card);
//        break;
//    case "Shield":
//        mlevelManagerUI._SheildCardList.Add(card);
//        break;
//    case "Steal":
//        mlevelManagerUI._StealCardList.Add(card);
//        break;
//}
//clicks += 1;
//}
