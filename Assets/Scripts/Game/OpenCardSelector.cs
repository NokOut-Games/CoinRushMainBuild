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
    //            if (hitSpot.collider.isTrigger == true && hitSpot.collider.gameObject.tag == "Surfers") //If its a gameOBject names surfer move it to its targetPosition
    //            {
    //                var storeLenght = hitSpot.transform.gameObject.transform.childCount - 1f;
    //                for (int i = 0; i <= storeLenght; i++)
    //                {
    //                    if (hitSpot.transform.gameObject.transform.GetChild(i).GetComponent<PathToTravel>() != null)
    //                    {
    //                        hitSpot.transform.gameObject.transform.GetChild(i).GetComponent<PathToTravel>().move = true;
    //                    }
    //                }
    //                Destroy(hitSpot.collider);
    //            }
    //        }
    //    }

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
        GameManager.Instance.OpenCardDetails.RemoveAt(_OpenCardPosition);
        GameManager.Instance.OpenCardWritten = true;
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