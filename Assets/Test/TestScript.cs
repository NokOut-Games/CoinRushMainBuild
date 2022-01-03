using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        anim.Play();
    }
}
















//public float PanSpeed = 20f;

//private Vector3 lastPanPosition;
//private int panFingerId;

//void Update()
//{
//    if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
//    {
//        HandleTouch();
//    }
//    else
//    {
//        HandleMouse();
//    }
//}

//void HandleTouch()
//{
//    Touch touch = Input.GetTouch(0);
//    if (touch.phase == TouchPhase.Began)
//    {
//        lastPanPosition = touch.position;
//        panFingerId = touch.fingerId;
//    }
//    else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
//    {
//        PanCamera(touch.position);
//    }
//}

//void HandleMouse()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        lastPanPosition = Input.mousePosition;
//    }
//    else if (Input.GetMouseButton(0))
//    {
//        PanCamera(Input.mousePosition);
//    }
//}

//void PanCamera(Vector3 newPanPosition)
//{
//    Vector3 offset = this.gameObject.GetComponent<Camera>().ScreenToViewportPoint(lastPanPosition - newPanPosition);
//    Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);


//    transform.Translate(move, Space.World);

//    Vector3 pos = transform.position;
//    pos.x = Mathf.Clamp(transform.position.x, -230, 230);
//    pos.z = Mathf.Clamp(transform.position.z, -550, -350);
//    transform.position = pos;

//    lastPanPosition = newPanPosition;
//}