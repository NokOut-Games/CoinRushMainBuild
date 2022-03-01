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
    [SerializeField] public GameObject mCardHolderParent;
    int clicks = 0;
    public List<Transform> _playerHandPoints;

    [SerializeField] private List<ScriptedCards> mScriptedCards;
    private List<Cards> _CardList = new List<Cards>();
    [HideInInspector] public List<GameObject> mCardListGameObject;
    private List<Vector3> _PositionList = new List<Vector3>();
    private List<Vector3> _RotationList = new List<Vector3>();

    [SerializeField] private Image DrawButton;
    [SerializeField] private Sprite drawNormal, drawAutomatic;
    [SerializeField] private RectTransform _drawButtonRectTransform;

    [SerializeField] private int mMaxHoldTime = 5;
    [SerializeField] private float timeForCardAnimation = 2f;
    private float time = 0, maxTime = 5;

    private bool mAutoCardDraw = false;
    private bool mAutomaticDrawModeOn = false;
    private bool mOnceDone = false;
    private bool canClick = true;

    public Image _drawButtonFillerImage;


    public List<GameObject> _openCardPrefabs;
    List<GameObject> _openCardSpawnedObjects = new List<GameObject>();

    int positionNumber = 0;
    [HideInInspector]public int _openedCardIndex;
    [HideInInspector]public int _openCardSlot;
    [HideInInspector]public List<int> _OpenCardSlotFilled;
    private int mCardsOpened;

    int newCardIndex = 0;
    [SerializeField] Animator CardDeckAnimator;
    public GameObject cardDeckAnimation2D;
    public GameObject pickCard3D;
    public GameObject backToDeckAnimation3D;
    public GameObject blackOutScreen;
    public GameObject threeCardEffect;
    public Animator shieldAnimation;
    public ParticleSystem shieldParticle;

    bool mMakeDrawBtnEnable = true;

    public BuildingManager _buildingManagerRef;

    ScriptedCards mCards;
    [HideInInspector] public bool mHasThreeCardMatch;
    int mThreeCardMatchIndex;
    bool mHasJoker;
    int mNumOfPairCards;
    [HideInInspector] public bool mJokerFindWithMultiCardPair;
    bool take_Multi_Card_Joker_Pair_Input;
    int[] mSelectionCards = new int[2];

    GameObject mFlotingJoker;

    private OpenCards mOpenCards => GameObject.Find("OpenHandPointsParent").GetComponent<OpenCards>();

    [SerializeField] int mJokerProbability;

    [SerializeField] DrawButtonState mDrawButtonState;

    public Multiplier _Multiplier;
    Tutorial tutorial;
    [SerializeField] Camera uIcam;
    [SerializeField] ParticleSystem drawBtnParticle;
    bool drawButtonClick => RectTransformUtility.RectangleContainsScreenPoint(_drawButtonRectTransform, Input.mousePosition, uIcam) && !TutorialManager.Instance.isPopUpRunning;

    [SerializeField] Transform[] jokerPairCardTransforms = new Transform[4];


    Cards[] matchedCards = new Cards[3];
    [SerializeField] MenuUI menu;



    private void Awake()
    {
        GameManager.GotAnOpenCard += SpawnOpenCards;
    }
    public void OnDestroy()
    {
        GameManager.GotAnOpenCard -= SpawnOpenCards;
    }
    void ShieldAnimation()
    {
        shieldAnimation.gameObject.SetActive(true);
        shieldAnimation.Play("Anim");
        shieldParticle.Play();
    }

    private void Start()
    {
        if(mDrawButtonState == DrawButtonState.NormalState)
        {
            PopulateOpenedCardSlotsFromFireBase();     
        }
        else
        {
            Invoke(nameof(PopulateFriendsOpenCardSlotsFromFirebase), .5f);
        }

        if (mDrawButtonState == DrawButtonState.NormalState)
        {
            //onceDonee = false;
            canClick = true;
            if (GameManager.Instance._IsInAutoDraw)
            {
                Camera.main.GetComponent<CameraController>().DrawButtonClicked();
                DrawButton.sprite = drawAutomatic;
                mOnceDone = true;
                mAutomaticDrawModeOn = true;
                mAutoCardDraw = true;
                StartCoroutine(AutomaticCardDrawing(3.5f));
            }
            else
            {
                //_OpenCardTakenAlready = false;
                DrawButton.sprite = drawNormal;

            }
           // SpawnOpenCards();
            if (GameManager.Instance._SavedCardTypes.Count > 0)
            {
                foreach (int cardType in GameManager.Instance._SavedCardTypes)
                {
                    InstantiateCard(GetScriptedCardWithCardType((CardType)cardType), true);
                }
            }
        }
        else
        {
           // _OpenCardTakenAlready = false;
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
        for (int i = 0; i < MultiplayerManager.Instance.Enemydata.OpenCards.Count/* mMultiplayerPlayerData.OpenCardDetails.Count*/; i++)
        {
            _OpenCardSlotFilled.Add(MultiplayerManager.Instance.Enemydata.OpenCards[i]/* mMultiplayerPlayerData.OpenCardDetails[i]*/._openedCardSlot);
        }
    }

    private void SpawnOpenCards()
    {
        PopulateOpenedCardSlotsFromFireBase();
        foreach (var openCard in _openCardSpawnedObjects)
        {
            Destroy(openCard);
        }
        _openCardSpawnedObjects.Clear();
        for (int i = 0; i < GameManager.Instance.OpenCardDetails.Count; i++)
        {
            GameObject savedOpenCard = Instantiate(_openCardPrefabs[GameManager.Instance.OpenCardDetails[i]._openedCardSelectedCard], mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].position, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].rotation, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]]);
            
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardSelectedCard = GameManager.Instance.OpenCardDetails[i]._openedCardSelectedCard;
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardPosition = GameManager.Instance.OpenCardDetails[i]._openedCardSlot;
            _openCardSpawnedObjects.Add(savedOpenCard);

            System.Action<Sprite,int> OnGettingPicture = (pic,index) =>
            {
                _openCardSpawnedObjects[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = pic;
            };
            FacebookManager.Instance.GetProfilePictureWithId(GameManager.Instance.OpenCardDetails[i]._openedPlayerID, OnGettingPicture,i);
        }
    }

    private void SpawnFriendsOpenCards()
    {
        for (int i = 0; i < MultiplayerManager.Instance.Enemydata.OpenCards.Count; i++)
        {
            GameObject savedOpenCard = Instantiate(_openCardPrefabs[MultiplayerManager.Instance.Enemydata.OpenCards[i] ._openedCardSelectedCard], mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].position, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]].rotation, mOpenCards._OpenCardTransformPoint[_OpenCardSlotFilled[i]]);
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardSelectedCard = MultiplayerManager.Instance.Enemydata.OpenCards[i]._openedCardSelectedCard;
            savedOpenCard.GetComponent<OpenCardSelector>()._OpenCardPosition = MultiplayerManager.Instance.Enemydata.OpenCards[i]._openedCardSlot;
            System.Action<Sprite> OnGettingPicture = (pic) =>
            {
                savedOpenCard.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = pic;
            };
            FacebookManager.Instance.GetProfilePictureWithId(MultiplayerManager.Instance.Enemydata.OpenCards[i]._openedPlayerID, OnGettingPicture);
        }
    }

    private void DestroyCardList()
    {
        int i = 8;
        //backToDeckAnimation3D.SetActive(true);
        CardDeckAnimator.Play("Back");
        foreach (int cardType in GameManager.Instance._SavedCardTypes)
        {
            i--;
            Debug.Log(backToDeckAnimation3D.transform.GetChild(i).name);
            backToDeckAnimation3D.transform.GetChild(i).GetComponent<Renderer>().material.mainTexture = GetScriptedCardWithCardType((CardType)cardType)._cardTex;
        }

        foreach (GameObject card in mCardListGameObject)
        {
            Destroy(card);
        }
        _CardList.Clear();
        //_jokerList.Clear();
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
        if (GameManager.Instance._PauseGame) return;

        Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);
        if (mDrawButtonState == DrawButtonState.OpenCardState)
        {
            if (!MultiplayerManager.Instance.OpenedPlayerID.Contains(FirebaseManager.Instance._PlayerID))
            {
                if (Input.GetMouseButtonDown(0) && drawButtonClick)
                {
                    OpenHandCardAdder();
                }
            }
            else menu.UIElementActivate(5, false);
        }
        if (mDrawButtonState == DrawButtonState.NormalState)
        {
            if (clicks == 8 && !mHasThreeCardMatch && !mJokerFindWithMultiCardPair)
            {
                clicks = 0;
                Invoke(nameof(DestroyCardList), 1f);
            }

            time = Mathf.Clamp(time, 0f, mMaxHoldTime);

            if (take_Multi_Card_Joker_Pair_Input)
            {
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
                                    GameManager.Instance._IsInAutoDraw = true;
                                    StartCoroutine(AutomaticCardDrawing());
                                }
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
                if (Input.GetMouseButtonDown(0))
                {
                    if (drawButtonClick && mAutoCardDraw)
                    {
                        BackToNormalState();
                    }
                }
            }
            else BackToNormalState();
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
            Invoke(nameof(ShieldAnimation), 3.5f);
        }
        else
        {
            mCardHolderParent.transform.parent.SetAsLastSibling();
        }
        Invoke("ThreeCardEffectActivate", 3.2f);

        _CardList[mSelectionCards[inSelectedIndex] - 1].PlayThreeCardMatchAnim(-320);
        mFlotingJoker.GetComponent<Cards>().PlayThreeCardMatchAnim(0, _CardList[mSelectionCards[inSelectedIndex]].gameObject.GetComponent<Image>().sprite);
        _CardList[mSelectionCards[inSelectedIndex]].PlayThreeCardMatchAnim(320);


        matchedCards[0] = _CardList[mSelectionCards[inSelectedIndex] - 1];
        matchedCards[1] = mFlotingJoker.GetComponent<Cards>();
        matchedCards[2] = _CardList[mSelectionCards[inSelectedIndex]];

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
        if(_drawButtonFillerImage!=null)
           _drawButtonFillerImage.fillAmount = 0;

        if (mAutomaticDrawModeOn)
        {
            mAutomaticDrawModeOn = false;
            ChangeSprites();
            mOnceDone = false;
            mAutoCardDraw = false;
            GameManager.Instance._IsInAutoDraw = false;

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

    private IEnumerator AutomaticCardDrawing(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        while (mAutoCardDraw)
        {
            if (canClick && mMakeDrawBtnEnable && GameManager.Instance._energy > 0)
            {
                DrawCard();
            }
            yield return new WaitForSeconds(timeForCardAnimation);
        }
    }

    private void DrawCard()
    {
        if (CardDeckAnimator.GetCurrentAnimatorStateInfo(0).IsName("Back")) return;

        if (_CardList.Count >= 8)
        {
            return;
        }
        mMakeDrawBtnEnable = false;

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
        pickCard3D.GetComponent<Renderer>().material.mainTexture = mCards._cardTex;
        CardDeckAnimator.Play("Draw");
        //pickCard3D.SetActive(true);
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
                _openedCardIndex = Random.Range(0, _openCardPrefabs.Count);
                GameObject OpenCards = Instantiate(_openCardPrefabs[_openedCardIndex], mOpenCards._OpenCardTransformPoint[positionNumber].position, mOpenCards._OpenCardTransformPoint[positionNumber].rotation, mOpenCards._OpenCardTransformPoint[positionNumber]);
                OpenCards.GetComponent<OpenCardSelector>()._OpenCardSelectedCard = _openedCardIndex;
                OpenCards.GetComponent<OpenCardSelector>()._OpenCardPosition = positionNumber;
                
                System.Action<Sprite> OnGettingPicture = (pic) =>
                {
                    OpenCards.transform.localPosition = Vector3.zero;
                    OpenCards.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = pic;
                };
                FacebookManager.Instance.GetProfilePictureWithId(FirebaseManager.Instance._PlayerID, OnGettingPicture);
                _openCardSlot = positionNumber;
                //_OpenCardNumberIndex += 1;
                _OpenCardSlotFilled.Clear();
                _OpenCardSlotFilled.Add(positionNumber);
                mCardsOpened += 1;
                MultiplayerManager.Instance.isReWriting = true;
                
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
        //pickCard3D.SetActive(false);
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
            //_jokerList.Add(inNewCard);
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
            int jokerIndex = FindJokerIndex();
            _CardList[jokerIndex].PlayThreeCardMatchAnim(0, _CardList[newCardIndex].gameObject.GetComponent<Image>().sprite);
            _CardList[newCardIndex + 1].PlayThreeCardMatchAnim(320);
           


            matchedCards[0] = _CardList[newCardIndex];
            matchedCards[1] = _CardList[jokerIndex];
            matchedCards[2] = _CardList[newCardIndex + 1]; 


            if (_CardList[newCardIndex]._cardType == CardType.SHIELD)
            {
                Destroy(_CardList[newCardIndex].gameObject, 3.25f);
                Destroy(_CardList[newCardIndex + 1].gameObject, 3.25f);
                Destroy(_CardList[jokerIndex].gameObject, 3.25f);
                Invoke(nameof(DelayBalckOut), 3.25f);
                mHasJoker = false;
                Invoke(nameof(ShieldAnimation), 3.5f);
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
                cardDeckAnimation2D.GetComponent<CardDeckAnimation>().PlayOnDropAnimation(_PositionList[newCardIndex], _RotationList[newCardIndex].z,_CardList[newCardIndex]._cardType);
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

                matchedCards[0] = _CardList[i];
                matchedCards[1] = _CardList[i+1];
                matchedCards[2] = _CardList[i+2];

                Invoke(nameof(PlayThreeCardAnimation), 1.5f);
                StartCoroutine(DelayedSceneLoader(matchedCard, 5f));
            }
            else if (mHasJoker && (_CardList[i]._cardType == _CardList[i + 2]._cardType && _CardList[i + 1]._cardType == CardType.JOKER))
            {
                matchedCards[0] = _CardList[i];
                matchedCards[1] = _CardList[i + 1];
                matchedCards[2] = _CardList[i + 2];

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

        if (GameManager.Instance._shield < GameManager.Instance._maxShield)
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
        StartCoroutine(menu.UpDateShieldInUICoroutine(.5f));

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
            Invoke(nameof(ShieldAnimation), 3.5f);

        }
        else
        {
            mCardHolderParent.transform.parent.SetAsLastSibling();
        }
        Invoke(nameof(ThreeCardEffectActivate), 3.2f);

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
        //blackOutScreen.transform.SetAsLastSibling();

        if (inType == CardType.SHIELD)
        {
            Shield();
            canClick = true;
        }
        else
        {
            menu.MakeCanvasScreenIn(false);

            int waitTime = 3000;
            if (mHasJoker || mJokerFindWithMultiCardPair)
            {
                waitTime += 500;
            }
            if (GameManager.Instance._energy >= 3)
            {
                _drawButtonRectTransform.parent.SetAsFirstSibling();
                _Multiplier.gameObject.SetActive(true);
                _Multiplier.AssignTutorial(tutorial);
                _Multiplier.transform.SetAsLastSibling();
                _Multiplier.InitiateMulitiplier(inType);
            }
            else
            {
                if (inType == CardType.ATTACK)
                {
                    MultiplayerManager.Instance.OnGettingAttackCard();
                }
                else
                {
                    LevelLoadManager.instance.LoadLevelASyncOf(inType.ToString(), waitTime, inType.ToString());
                }
            }
           
            GameManager.Instance._IsBuildingFromFBase = true;
            foreach (Cards card in _CardList)
            {
                if (card._cardType != matchedCards[0]._cardType) card.gameObject.SetActive(false);
            }
            
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