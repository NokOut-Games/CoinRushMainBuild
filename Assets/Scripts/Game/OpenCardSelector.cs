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

    public void OnMouseDown()
    {
        if (!mCameraController._isCameraInGamePlayView)
        {
            //mCameraController.DrawButtonClicked();
            Invoke(nameof(CardSpawnDelay), mCardSpawnDelay);
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
        mcardDeck._OpenCardSlotFilled.Remove(_OpenCardPosition);
        string cardName = this.gameObject.name;
        cardName = cardName.Remove(cardName.Length - 7);
        ScriptedCards card = Resources.Load(cardName) as ScriptedCards;
        mcardDeck.InstantiateCard(card);
        Destroy(this.gameObject, .5f);
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