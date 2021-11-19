using UnityEngine;

public class RayCastThroughPanel : MonoBehaviour
{
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit/*6*/))
        {
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Open Card Slot"))
            {
                Debug.Log(raycastHit.transform.gameObject.name);
            }
        }
        //}
    }

}
