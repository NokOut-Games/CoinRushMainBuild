using UnityEngine;

public class RayCastThroughPanel : MonoBehaviour
{
    [SerializeField]
    private CardDeck mcardDeck;

    [SerializeField]
    private CameraController mcameraController;

    private void Start()
    {

    }

    private void Update()
    {
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
    }

    public void OnMouseDown()
    {
        if (mcameraController._isCameraInGamePlayView)
        {
            mcardDeck.OpenCardAdder(this.gameObject);
            Destroy(this.gameObject,1f);
        }
    }
}

