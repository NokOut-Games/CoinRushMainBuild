using UnityEngine;

public class RayCastThroughPanel : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

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
        //        Debug.Log("Mouse Is Inside");
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
        //}

    }
}
