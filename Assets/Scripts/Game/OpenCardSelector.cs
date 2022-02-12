using UnityEngine;

public class OpenCardSelector : MonoBehaviour
{
    [SerializeField]
    private CardDeck mcardDeck;

    [SerializeField]
    private CameraController mCameraController;

    [SerializeField]
    private float mCardSpawnDelay = 1f;

    public int _OpenCardSelectedCard;
    public int _OpenCardPosition;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "OPENCARD")
        {
            mcardDeck = GameObject.Find("CardDeck").GetComponent<CardDeck>();
            mCameraController = Camera.main.GetComponent<CameraController>();
        }
    }

    //public void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit hitSpot;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //See where the player clicks

    //        if (Physics.Raycast(ray, out hitSpot))
    //        {
    //            if (hitSpot.collider.gameObject.tag == "OpenCard") //If its a gameOBject names surfer move it to its targetPosition
    //            {
    //                if (!mCameraController._isCameraInGamePlayView)
    //                {
    //                    mCameraController.DrawButtonClicked();
    //                    Invoke(nameof(CardSpawnDelay), mCardSpawnDelay);
    //                }
    //                else 
    //                {
    //                    CardSpawnDelay();
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}
    public void OnMouseDown()
    {
        if (mCameraController == null) return;
        
        if (!mCameraController._isCameraInGamePlayView)
        {
            mCameraController.openCardSelected = true;
            mCameraController.DrawButtonClicked();
            //Debug.LogError(this.gameObject.name);
            Invoke(nameof(CardSpawnDelay), mCardSpawnDelay);
            mCameraController.openCardSelected = false;
            
        }
        else
        {
            CardSpawnDelay();
        }
    }

    //Future Case
    //public void OnMouseOver()
    //{
    //    mCameraController.DrawButtonClicked();
    //}

    private void CardSpawnDelay()
    {
        //Debug.LogError(this.gameObject.name);

        mcardDeck._OpenCardSlotFilled.Remove(_OpenCardPosition);
        string cardName = this.gameObject.name;
        cardName = cardName.Remove(cardName.Length - 7);
        ScriptedCards card = Resources.Load(cardName) as ScriptedCards;
        mcardDeck.InstantiateCard(card);
        Destroy(this.gameObject, .5f);
        //GameManager.Instance.OpenCardDetails.RemoveAt(_OpenCardPosition);

        CheckingCondition(_OpenCardPosition); //1
    }

    void CheckingCondition(int inPosition) //1
    {
        for (int i = 0; i < GameManager.Instance.OpenCardDetails.Count; i++) //3
        {
            if (GameManager.Instance.OpenCardDetails[i]._openedCardSlot == inPosition)
            {
                GameManager.Instance.OpenCardDetails.RemoveAt(i);
                GameManager.Instance.OpenCardWritten = true;
            }
            else
            {
                continue;
            }

            //GameManager.Instance.OpenCardDetails[_OpenCardPosition]._openedCardSlot;

            //GameManager.Instance.OpenCardDetails.RemoveAt(_OpenCardPosition);

            //}
        }
    }
}



//if (Input.GetMouseButtonDown(0))
//{
//RaycastHit raycastHit;
//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//if (Physics.Raycast(ray, out raycastHit/*6*/))
//{

//    if (raycastHit.collider.isTrigger)
//    {
//        if (cameraController._isCameraInGamePlayView)
//        {
//            Debug.Log("Mouse Is Inside");
//            
//           
//                if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("OpenCardSlot"))
//                {
//                    Debug.Log(raycastHit.transform.gameObject.name);
//                }
//            }
//        }
//        else
//        {
//            Debug.Log("Not In Gameplay View");
//        }
//        if (cameraController._isCameraInGamePlayView == true)
//        {
//            if (Input.GetMouseButtonDown(0))
//            {
//                if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Open Card Slot"))
//                {
//                    Debug.Log(raycastHit.transform.gameObject.name);
//                }
//            }
//        }
//    }
//    else
//    {
//        Debug.Log("Mouse is Outside");
//        if (Input.GetMouseButtonDown(0))
//        {
//            cameraController._isCameraInGamePlayView = false;

//        }
//    }
//}