using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    [SerializeField] private GameManager mGameManager;
    [SerializeField] private GameObject mCardHolderParent;
    private int clicks = 0;
    private int mk;

    [SerializeField] public List<ScriptedCards> mScriptedCards;
    public List<Cards> _CardList = new List<Cards>();
    public List<Transform> _playerHandPoints;
    [HideInInspector] public List<Vector3> _PositionList = new List<Vector3>();
    [HideInInspector] public List<Quaternion> _RotationList = new List<Quaternion>();

    #region CardMarch3 Version-1
    //public List<Cards> AttackList;
    //public List<Cards> StealList;
    //public List<Cards> ShieldList;
    //public List<Cards> JokerList;
    //public List<Cards> EnergyList;
    //public List<Cards> CoinsList;
    //public List<Cards> FortuneList;
    //public List<Cards> SpinList;
    #endregion
    
  

    private void Start()
    {
       
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }



    /// <summary>
    /// This function is responsible for the camera to zoom in to the playing space and the card draw functionality
    /// 1.Reduce the Energy.
    /// 2.Zoom to the gameplay location
    /// 3.Have a way to access the card location and spawn card at their respective positions in an inverted U-Shape
    /// </summary>
    public void DrawCard()
    {
        //ChangeSprites();
        if (_CardList.Count >= 8)
            return;

        mGameManager._energy -= 1;

        Camera.main.GetComponent<CameraController>().DrawButtonClicked();

        ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

        GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
        Cards cardDetails = card.GetComponent<Cards>();

        cardDetails._cardType = cards._cardType;
        cardDetails._cardID = cards._cardID;
        cardDetails._Position = card.transform.position;

        clicks += 1;
        AddNewCard(card.GetComponent<Cards>());
        ReplacementOfCards();
        CardCheckingFunction();
    }

    private void Update()
    {
        if (clicks == 8)
        {
            clicks = 0;
        }
    }

    /// <summary>
    /// 
    /// </This function is used to allign the picked cards in sorted order>
    public void AddNewCard(Cards inNewCard)
    {
        for (int i = 0; i < _CardList.Count; i++)
        {
            if (_CardList[i]._cardType == inNewCard._cardType)
            {
                _CardList.Insert(i, inNewCard);
                return;
            }
        }
        _CardList.Add(inNewCard);
    }

    public void ReplacementOfCards()
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

    void CardCheckingFunction()
    {
        #region Try-1 "Gives Output And throws Error too"
        if (_CardList.Count > 2)
        {
            for (int i = 0; i < _CardList.Count; i++) // 0 //3
            {
                int j = i; //0
                int k = i + 1; //1
                int l = i + 2; //2
                if(j > _CardList.Count || k > _CardList.Count || l > _CardList.Count)
                {
                    return;
                }
                if (_CardList[j]._cardType == _CardList[k]._cardType && _CardList[k]._cardType == _CardList[l]._cardType)
                {
                    SceneManager.LoadScene(_CardList[i]._cardType.ToString());
                }
            }
        }
        #endregion

        //#region Try-2 "Took from internet but understood the code"
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

        //    }
        //}
        //#endregion

    }

    IEnumerator DelayedSceneLoader(CardType inType)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(inType.ToString());
    }
}








// Algorithm for auto draw
// Method - 1
// have a while which when true does the draw card continously

// But before that we should write an algorithm on how to get the hold working for our button and tap for our button
// And have a timer which when holded down for more than a particular second do auto and if not do normal.

//void drawButtonChange()
//{
//      public Image DrawButton;
//public Sprite drawNormal, drawAutomatic;

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
