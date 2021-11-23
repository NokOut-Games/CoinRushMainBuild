using UnityEngine;

public class OpenCardSelector : MonoBehaviour
{
    [SerializeField]
    private CardDeck mcardDeck;

    [SerializeField]
    private CameraController mcameraController;

    public void OnMouseDown()
    {
        if (mcameraController._isCameraInGamePlayView)
        {
            ScriptedCards card = Resources.Load(this.gameObject.name) as ScriptedCards;
            //Debug.Log(card._cardModel);
            mcardDeck.InstantiateCard(card);
            Destroy(this.gameObject,.5f);
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