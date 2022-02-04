using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform _CameraParent;

    [Header("Horizontal Panning")]
    //[SerializeField] private Transform mTargetToRotateAround;
    [SerializeField] private float mPanSpeed = 20f;
    [SerializeField] private float mAutoDragPanSpeed = 0f;
    [SerializeField] public float _CameraLeftBound = 0;
    [SerializeField] public float _CameraRightBound = 0;

    private Vector3 lastPanPosition;    //New Addition
    public float timeToAcceleration = 0; //New Addition
    [SerializeField] private float mTimeTakenToAutoPan;

    [Header("Vertical Zooming")]
    [SerializeField] private float mCameraNearBound;
    [SerializeField] private float mCameraFarBound;

    [Header("Camera Views & Transition")]
    public Transform _currentView;
    public Transform[] _views;
    public float _transitionSpeed;

    [Header("Grab Button Rect TRansforms")]
    [SerializeField] private RectTransform mDrawButtonRectTransform;
    [SerializeField] private RectTransform mOpenHandRectTransform;
    [SerializeField] private RectTransform mScrollViewRectTransform;
    [SerializeField] private GameObject mOpenCardRegion;

    //State Checkers
    public bool _DrawButtonClicked = false;
    public bool _isCameraInGamePlayView = false;

    public bool _buildButtonClicked = false;
    public bool _isCameraInConstructionView;
    public bool _inBetweenConstructionProcess = false;

    public bool _CameraFreeRoam = true;

    //Script References
    private CardDeck mCardDeck;
    public MenuUI mMenuUI;


    //New Changes
    Vector3 mPreTouchMovementVector;
    float touchMovedTime;
    float touchHoldTime;


    [SerializeField] Camera uIcam;
    bool drawButtonClick => RectTransformUtility.RectangleContainsScreenPoint(mDrawButtonRectTransform, Input.mousePosition, uIcam);
    bool OpenCardRegionClick => RectTransformUtility.RectangleContainsScreenPoint(mOpenHandRectTransform, Input.mousePosition, uIcam);
    bool BuildScrollViewClick =>!GameManager.Instance._PauseGame&& RectTransformUtility.RectangleContainsScreenPoint(mScrollViewRectTransform, Input.mousePosition, uIcam);
    bool GetToNormalView => !_buildButtonClicked && !_DrawButtonClicked && !mCardDeck.mHasThreeCardMatch && !mCardDeck.mJokerFindWithMultiCardPair && !TutorialManager.Instance.isPopUpRunning && !GameManager.Instance.isInTutorial;
    float TouchTime = 0.11f;

    public bool openCardSelected;

    private void Start()
    {
        mCardDeck = GameObject.Find("CardDeck").GetComponent<CardDeck>();
        mMenuUI = GameObject.Find("GameCanvas").GetComponent<MenuUI>();

        _CameraParent = transform.parent;
        _currentView = _views[0];
    }

    /// <summary>
    /// Makes _BuildButtonClicked = true & takes us to constructionview
    /// </summary>
    public void BuildButtonClicked()
    {
        _buildButtonClicked = true;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        _CameraFreeRoam = false;
    }

    /// <summary>
    /// Makes _DrawBUttonClicked = true & takes us to gameplay view
    /// </summary>
    public void DrawButtonClicked()
    {
        _DrawButtonClicked = true;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        _CameraFreeRoam = false;
    }

    public void SetCameraFreeRoam()
    {
        _CameraFreeRoam = true;
    }

    public void SetOpenCardSelectedFalse()
    {
        openCardSelected = false;
    }
    private void Update()
    {
        if(openCardSelected) return;

        if (_CameraFreeRoam) HandleTouch();
        if (!_inBetweenConstructionProcess&&!GameManager.Instance._PauseGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_isCameraInGamePlayView) //New Addition
                {
                    if (!drawButtonClick && !OpenCardRegionClick)
                    {
                        _DrawButtonClicked = false;
                        _isCameraInGamePlayView = false;
                       // Invoke("SetCameraFreeRoam", 0.11f);
                        mCardDeck.BackToNormalState();
                        TouchTime = .11f;

                    }
                }
                else if (_isCameraInConstructionView && !BuildScrollViewClick)
                {
                    _buildButtonClicked = false;
                    _isCameraInConstructionView = false;
                    //Invoke("SetCameraFreeRoam", 0.11f);
                    mMenuUI.CloseBuildButton();
                    TouchTime = .11f;

                }
            }
        }
        if (_CameraFreeRoam && !Input.GetMouseButton(0))
        {
            _CameraFreeRoam = false;
        }

        if (_buildButtonClicked && !_DrawButtonClicked)
        {
            mCardDeck.mCardHolderParent.SetActive(false);
            _isCameraInConstructionView = true;
            if (_CameraFreeRoam) return;
            ViewShifter(2, 0.1f); // 2 takes to construction view
        }
        else if (_DrawButtonClicked && !_buildButtonClicked)
        {
            mCardDeck.mCardHolderParent.GetComponent<Animator>().SetBool("Shrink", false);
            _isCameraInGamePlayView = true;
            mOpenCardRegion.SetActive(true);
            if (_CameraFreeRoam) return;

            ViewShifter(1, 0.1f);   // 1 takes to gameplay view //_currentView = _views[1];
        }
        if (GetToNormalView)
        {
            mCardDeck.mCardHolderParent.SetActive(true);
            mCardDeck.mCardHolderParent.GetComponent<Animator>().SetBool("Shrink", true);
            if (_CameraFreeRoam) return;
            mOpenCardRegion.SetActive(false);
            //  if (Mathf.Floor(_CameraParent.rotation.eulerAngles.x) != _views[0].rotation.eulerAngles.x)
            {
                ViewShifter(0, 0.01f/*Time.fixedDeltaTime * _transitionSpeed*/); // 0 takes to normal view //_currentView = _views[0];
            }

        }

    }


    private void ViewShifter(int inViewNumber, float inTransitionSpeed)
    {
        TouchTime -= Time.deltaTime;
        if (TouchTime <= 0)
        {
            _CameraFreeRoam = true;
        }
      //  Debug.Log(inViewNumber + "View");
        _currentView = _views[inViewNumber];
        //Debug.Log("Current View Changed To: " + _currentView);
        _CameraParent.position = Vector3.Lerp(_CameraParent.position, _currentView.position, 0.1f);// Time.deltaTime * _transitionSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, 0.1f),// Time.deltaTime * _transitionSpeed),
            Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, 0.1f),//Time.deltaTime * _transitionSpeed),
            Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, 0.1f));//Time.deltaTime * _transitionSpeed));

        _CameraParent.eulerAngles = currentAngle;

    }
    bool IsBoundary(Touch touch)
    {
        if (_CameraParent.transform.position.x > _CameraRightBound + 60 && -touch.deltaPosition.x > 0) return true;
        else if (_CameraParent.transform.position.x < _CameraLeftBound - 60 && -touch.deltaPosition.x < 0) return true;
        else if (_CameraParent.transform.position.z < mCameraFarBound - 60 && -touch.deltaPosition.y < 0) return true;
        else if (_CameraParent.transform.position.z > mCameraNearBound + 60 && -touch.deltaPosition.y > 0) return true;
        else return false;
    }
    bool IsInsideBoundry()
    {
        if (_CameraParent.transform.position.x < _CameraRightBound + 60 && _CameraParent.transform.position.x > _CameraLeftBound - 60 && _CameraParent.transform.position.z > mCameraFarBound - 60 && _CameraParent.transform.position.z < mCameraNearBound + 60) return true;
        else return false;
    }
    void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            _CameraFreeRoam = true;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                touchMovedTime += Time.deltaTime;
                //Debug.Log(touchMovedTime);
                if (IsBoundary(touch)) return;
                mPreTouchMovementVector = new Vector3(-touch.deltaPosition.x, 0, -touch.deltaPosition.y);
                _CameraParent.transform.position = _CameraParent.transform.position + mPreTouchMovementVector * 0.4f;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (IsBoundary(touch)) return;
                Vector3 moveTo = _CameraParent.transform.position + mPreTouchMovementVector.normalized * (10 / touchMovedTime);
                moveTo = new Vector3(Mathf.Clamp(moveTo.x, _CameraLeftBound - 60, _CameraRightBound + 60), moveTo.y, Mathf.Clamp(moveTo.z, mCameraFarBound - 60, mCameraNearBound + 60));
                if (touchMovedTime < 0.15f && touchMovedTime > 0.01f) _CameraParent.transform.DOMove(moveTo, .3f);
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                touchHoldTime += Time.deltaTime;
                if (IsInsideBoundry() && touchHoldTime > .3f) _CameraParent.transform.Translate(mPreTouchMovementVector.normalized * 5f, Space.World);
            }
        }
        else
        {
            touchMovedTime = 0;
            touchHoldTime = 0;
            if (_CameraParent.transform.position.x > _CameraRightBound)
            {
                _CameraParent.transform.Translate(Vector3.left * 5f, Space.World);
            }
            else if (_CameraParent.transform.position.x < _CameraLeftBound)
            {
                _CameraParent.transform.Translate(Vector3.right * 5f, Space.World);
            }
            else if (_CameraParent.transform.position.z > mCameraNearBound)
            {
                _CameraParent.transform.Translate(Vector3.back * 5f, Space.World);
            }
            else if (_CameraParent.transform.position.z < mCameraFarBound)
            {
                _CameraParent.transform.Translate(Vector3.forward * 15f, Space.World);
            }
        }
    } //New Addition
}




























#region "Camera EX New Pan"
//[Header("Camera")]
//[SerializeField] private Transform _CameraParent;

//[Header("Horizontal Panning")]
////[SerializeField] private Transform mTargetToRotateAround;
//[SerializeField] private float mPanSpeed = 20f;
//[SerializeField] private float mAutoDragPanSpeed = 0f;
//[SerializeField] private float mCameraLeftBound = 0;
//[SerializeField] private float mCameraRightBound = 0;

//private Vector3 lastPanPosition;    //New Addition
//public float timeToAcceleration = 0; //New Addition
//[SerializeField] private float mTimeTakenToAutoPan;

//[Header("Vertical Zooming")]
//[SerializeField] private float mCameraNearBound;
//[SerializeField] private float mCameraFarBound;

//[Header("Camera Views & Transition")]
//public Transform _currentView;
//public Transform[] _views;
//public float _transitionSpeed;

//[Header("Grab Button Rect TRansforms")]
//[SerializeField] private RectTransform mDrawButtonRectTransform;
//[SerializeField] private RectTransform mOpenHandRectTransform;
//[SerializeField] private RectTransform mScrollViewRectTransform;
//[SerializeField] private GameObject mOpenCardRegion;

////State Checkers
//public bool _DrawButtonClicked = false;
//public bool _isCameraInGamePlayView = false;

//public bool _buildButtonClicked = false;
//public bool _isCameraInConstructionView;
//public bool _inBetweenConstructionProcess = false;

//public bool _CameraFreeRoam = true;

////Script References
//private CardDeck mCardDeck;
//public MenuUI mMenuUI;

//private Vector3 initialVector = Vector3.forward;
//private Vector2 mMouseDownPosition = Vector2.zero;

//private void Start()
//{
//    mCardDeck = GameObject.Find("CardDeck").GetComponent<CardDeck>();
//    mMenuUI = GameObject.Find("GameCanvas").GetComponent<MenuUI>();

//    _CameraParent = transform.parent;
//    _currentView = _views[0];
//}

///// <summary>
///// Makes _BuildButtonClicked = true & takes us to constructionview
///// </summary>
//public void BuildButtonClicked()
//{
//    _buildButtonClicked = true;
//    transform.localEulerAngles = Vector3.zero;
//    transform.localPosition = Vector3.zero;
//    _CameraFreeRoam = false;
//}

///// <summary>
///// Makes _DrawBUttonClicked = true & takes us to gameplay view
///// </summary>
//public void DrawButtonClicked()
//{
//    _DrawButtonClicked = true;
//    transform.localEulerAngles = Vector3.zero;
//    transform.localPosition = Vector3.zero;
//    _CameraFreeRoam = false;
//}

//public void SetCameraFreeRoam()
//{
//    _CameraFreeRoam = true;
//}

//private void Update()
//{
//    //if(_currentView == _views[0])
//    //{
//    //    Debug.Log("CurrentView = " + "<color=red>  Normal-View  </color>");
//    //}
//    //else if(_currentView == _views[1])
//    //{
//    //    Debug.Log("CurrentView = " + "<color=blue> Gameplay-View </color>");
//    //}
//    //else if (_currentView == _views[2])
//    //{
//    //    Debug.Log("CurrentView = " + "<color=yellow> Build-View </color>");
//    //}

//    //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

//    if (!_inBetweenConstructionProcess)
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            mMouseDownPosition = Input.mousePosition;
//            Vector2 drawButtonlocalMousePosition = mDrawButtonRectTransform.InverseTransformPoint(mMouseDownPosition);
//            Vector2 openHandLocalMousePosition = mOpenHandRectTransform.InverseTransformPoint(mMouseDownPosition); //New Addition
//            Vector2 BuildingScrollViewLocalPosition = mScrollViewRectTransform.InverseTransformPoint(mMouseDownPosition);
//            if (_isCameraInGamePlayView) //New Addition
//            {
//                if (!mDrawButtonRectTransform.rect.Contains(drawButtonlocalMousePosition) && !mOpenHandRectTransform.rect.Contains(openHandLocalMousePosition))
//                {
//                    _DrawButtonClicked = false;
//                    _isCameraInGamePlayView = false;
//                    Invoke("SetCameraFreeRoam", 0.11f);
//                    mCardDeck.BackToNormalState();
//                }
//            }
//            else
//            {
//                if (!mDrawButtonRectTransform.rect.Contains(drawButtonlocalMousePosition))
//                {
//                    _DrawButtonClicked = false;
//                    _isCameraInGamePlayView = false;
//                    Invoke("SetCameraFreeRoam", 0.11f);
//                    mCardDeck.BackToNormalState();
//                }
//            }

//            if (_isCameraInConstructionView && !_isCameraInGamePlayView)
//            {
//                if (!mScrollViewRectTransform.rect.Contains(BuildingScrollViewLocalPosition))
//                {
//                    _buildButtonClicked = false;
//                    _isCameraInConstructionView = false;
//                    Invoke("SetCameraFreeRoam", 0.11f);
//                    mMenuUI.CloseBuildButton();
//                }
//            }
//        }

//        if (_CameraFreeRoam && !Input.GetMouseButton(0))
//        {
//            _CameraFreeRoam = false;
//        }


//        if (!_buildButtonClicked)
//        {
//            if (_DrawButtonClicked)
//            {
//                mCardDeck.mCardHolderParent.GetComponent<Animator>().SetBool("Shrink", false);

//                _isCameraInGamePlayView = true;
//                mOpenCardRegion.SetActive(true);

//                ViewShifter(1, 0.1f);   // 1 takes to gameplay view //_currentView = _views[1];
//            }
//            else
//            {
//                mCardDeck.mCardHolderParent.SetActive(true);
//                mCardDeck.mCardHolderParent.GetComponent<Animator>().SetBool("Shrink", true);
//                if (!_CameraFreeRoam)
//                {
//                    mOpenCardRegion.SetActive(false);
//                    if (Mathf.Floor(_CameraParent.rotation.eulerAngles.x) != _views[0].rotation.eulerAngles.x)
//                    {

//                        ViewShifter(0, 0.1f); // 0 takes to normal view //_currentView = _views[0];
//                    }
//                }
//                HandleMouse();
//            }
//        }

//        if (!_DrawButtonClicked)
//        {
//            if (_buildButtonClicked)
//            {
//                if (_DrawButtonClicked == true)
//                {
//                    _DrawButtonClicked = false;
//                }
//                mCardDeck.mCardHolderParent.SetActive(false);
//                _isCameraInConstructionView = true;
//                ViewShifter(2, 0.1f); // 2 takes to construction view
//            }
//            else
//            {
//                mCardDeck.mCardHolderParent.SetActive(true);
//                mCardDeck.mCardHolderParent.GetComponent<Animator>().SetBool("Shrink", true);
//                if (!_CameraFreeRoam)
//                {
//                    mOpenCardRegion.SetActive(false);
//                    if (Mathf.Floor(_CameraParent.rotation.eulerAngles.x) != _views[0].rotation.eulerAngles.x)
//                    {
//                        ViewShifter(0, Time.fixedDeltaTime * _transitionSpeed); // 0 takes to normal view
//                    }
//                }
//            }
//        }
//    }
//}

//void InvokeViewShifting()
//{

//}

//private void ViewShifter(int inViewNumber, float inTransitionSpeed)
//{
//    _currentView = _views[inViewNumber];
//    //Debug.Log("Current View Changed To: " + _currentView);
//    _CameraParent.position = Vector3.Lerp(_CameraParent.position, _currentView.position, 0.1f);// Time.deltaTime * _transitionSpeed);

//    Vector3 currentAngle = new Vector3(
//        Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, 0.1f),// Time.deltaTime * _transitionSpeed),
//        Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, 0.1f),//Time.deltaTime * _transitionSpeed),
//        Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, 0.1f));//Time.deltaTime * _transitionSpeed));

//    _CameraParent.eulerAngles = currentAngle;

//}

//void HandleMouse()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        lastPanPosition = Input.mousePosition;
//    }
//    else if (Input.GetMouseButton(0))
//    {
//        timeToAcceleration += 1 * Time.deltaTime;
//        if (timeToAcceleration > mTimeTakenToAutoPan)
//        {
//            PanCamera(Input.mousePosition, mAutoDragPanSpeed, 0);
//        }
//        else
//        {
//            PanCamera(Input.mousePosition, mPanSpeed, 1);
//        }
//    }
//    if (Input.GetMouseButtonUp(0))
//    {
//        timeToAcceleration = 0;
//    }

//    if (!Input.GetMouseButton(0))
//    {
//        if (_CameraParent.transform.position.x < mCameraLeftBound)
//        {
//            _CameraParent.transform.position = Vector3.Lerp(_CameraParent.transform.position, new Vector3(mCameraLeftBound, _CameraParent.transform.position.y, _CameraParent.transform.position.z), 4 * Time.deltaTime);
//        }
//        else if (_CameraParent.transform.position.x > mCameraRightBound)
//        {
//            _CameraParent.transform.position = Vector3.Lerp(_CameraParent.transform.position, new Vector3(mCameraRightBound, _CameraParent.transform.position.y, _CameraParent.transform.position.z), 4 * Time.deltaTime);
//        }
//        if (_CameraParent.transform.position.z > mCameraNearBound)
//        {
//            _CameraParent.transform.position = Vector3.Lerp(_CameraParent.transform.position, new Vector3(_CameraParent.transform.position.x, _CameraParent.transform.position.y, mCameraNearBound), 4 * Time.deltaTime);
//        }
//        else if (_CameraParent.transform.position.z < mCameraFarBound)
//        {
//            _CameraParent.transform.position = Vector3.Lerp(_CameraParent.transform.position, new Vector3(_CameraParent.transform.position.x, _CameraParent.transform.position.y, mCameraFarBound), 4 * Time.deltaTime);
//        }
//    }
//} //New Addition

//void PanCamera(Vector3 inNewPanPosition, float inPanSpeed, int inCameraMode)
//{
//    Vector3 offset = this.gameObject.GetComponent<Camera>().ScreenToViewportPoint(lastPanPosition - inNewPanPosition);
//    Vector3 move = new Vector3(offset.x * inPanSpeed, offset.y, offset.y * inPanSpeed);
//    Vector3 pos = _CameraParent.transform.position;
//    if (pos.x > mCameraLeftBound + (-60) && pos.x < mCameraRightBound + 60 && pos.z < mCameraNearBound + 60 && pos.z > mCameraFarBound - 60)
//    {
//        _CameraParent.transform.Translate(move, Space.World);
//    }

//    //pos.x = Mathf.Clamp(_CameraParent.transform.position.x, mCameraLeftBound, mCameraRightBound);
//    //pos.z = Mathf.Clamp(_CameraParent.transform.position.z, mCameraFarBound, mCameraNearBound);
//    //_CameraParent.transform.position = pos;

//    if (inCameraMode == 1)
//    {
//        lastPanPosition = inNewPanPosition;
//    }
//}
#endregion
#region "OLD CAMERA"
//[Header("Camera")]
//public Transform _CameraParent;
//private float mInitialPositionX;
//private float mChangedPositionX;
//private float mInitialPositionY;
//private float mChangedPositionY;

//[Header("Horizontal Panning")]
//[SerializeField] private Transform mTargetToRotateAround;
//[SerializeField] private float mHorizontalPanSpeed;
//public float _CameraLeftBound = 0;
//public float _CameraRightBound = 0;

//public float _CameraUpBound = 0;
//public float _CameraDownBound = 0;

//[Header("Vertical Zooming")]
//[SerializeField] private float mZoomSpeed;
//[SerializeField] private float mCameraNearBound;
//[SerializeField] private float mCameraFarBound;


//[Header("Camera Views")]
//private Transform _currentView;

//private Vector3 initialVector = Vector3.forward;
//private Vector2 _MouseDownPosition = Vector2.zero;

//public Transform[] _views;
//public float _transitionSpeed;
//public RectTransform _DrawButtonRectTransform;
//public RectTransform _OpenHandRectTransform;
//public RectTransform _ScrollViewRectTransform;

//public bool _DrawButtonClicked = false;
//public bool _CameraFreeRoam = true;
//public bool _isCameraInGamePlayView = false;

//public GameObject OpenCardRegion;

//public int _RotationLimit = 30;

//private CardDeck mCardDeck;

//public bool _buildButtonClicked = false;
//public bool _inBetweenConstructionProcess = false;
//public bool _isCameraInConstructionView;
//public MenuUI mMenuUI;

//public float PanSpeed = 20f;    //New Addition

//private Vector3 lastPanPosition;    //New Addition

//public float timeToAcceleration = 0;

//private void Start()
//{
//    mCardDeck = GameObject.Find("CardDeck").GetComponent<CardDeck>();
//    mMenuUI = GameObject.Find("GameCanvas").GetComponent<MenuUI>();

//    _CameraParent = transform.parent;

//    if (mTargetToRotateAround != null)
//    {
//        initialVector = transform.position - mTargetToRotateAround.position;
//        initialVector.y = 0;
//    }
//}

//public void BuildButtonClicked()
//{
//    _buildButtonClicked = true;
//    transform.localEulerAngles = Vector3.zero;
//    transform.localPosition = Vector3.zero;
//    _CameraFreeRoam = false;
//}

//public void DrawButtonClicked()
//{
//    _DrawButtonClicked = true;
//    transform.localEulerAngles = Vector3.zero;
//    transform.localPosition = Vector3.zero;
//    _CameraFreeRoam = false;
//}

//public void SetCameraFreeRoam()
//{
//    _CameraFreeRoam = true;
//}

//private void Update()
//{

//    if (!_inBetweenConstructionProcess)
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            _MouseDownPosition = Input.mousePosition;
//            Vector2 drawButtonlocalMousePosition = _DrawButtonRectTransform.InverseTransformPoint(_MouseDownPosition);
//            Vector2 openHandLocalMousePosition = _OpenHandRectTransform.InverseTransformPoint(_MouseDownPosition); //New Addition
//            Vector2 BuildingScrollViewLocalPosition = _ScrollViewRectTransform.InverseTransformPoint(_MouseDownPosition);
//            if (_isCameraInGamePlayView) //New Addition
//            {
//                if (!_DrawButtonRectTransform.rect.Contains(drawButtonlocalMousePosition) && !_OpenHandRectTransform.rect.Contains(openHandLocalMousePosition))
//                {
//                    _DrawButtonClicked = false;
//                    _isCameraInGamePlayView = false;
//                    Invoke("SetCameraFreeRoam", 0.11f);
//                    mCardDeck.BackToNormalState();
//                }
//            }
//            else
//            {
//                if (!_DrawButtonRectTransform.rect.Contains(drawButtonlocalMousePosition))
//                {
//                    _DrawButtonClicked = false;
//                    _isCameraInGamePlayView = false;
//                    Invoke("SetCameraFreeRoam", 0.11f);
//                    mCardDeck.BackToNormalState();
//                }
//            }

//            if (_isCameraInConstructionView && !_isCameraInGamePlayView)
//            {
//                if (!_ScrollViewRectTransform.rect.Contains(BuildingScrollViewLocalPosition))
//                {
//                    _buildButtonClicked = false;
//                    _isCameraInConstructionView = false;
//                    Invoke("SetCameraFreeRoam", 0.11f);
//                    mMenuUI.CloseBuildButton();
//                }
//            }
//        }


//        if (_CameraFreeRoam && !Input.GetMouseButton(0))
//        {
//            _CameraFreeRoam = false;
//        }

//        if (_buildButtonClicked)
//        {
//            _isCameraInConstructionView = true;
//            ViewShifter(2, Time.deltaTime * _transitionSpeed);
//        }


//        if (_DrawButtonClicked)
//        {
//            _isCameraInGamePlayView = true;
//            OpenCardRegion.SetActive(true);

//            ViewShifter(1, 0.1f);
//        }
//        else
//        {
//            if (!_CameraFreeRoam)
//            {
//                OpenCardRegion.SetActive(false);
//                if (Mathf.Floor(_CameraParent.rotation.eulerAngles.x) != _views[0].rotation.eulerAngles.x)
//                {
//                    ViewShifter(0, Time.deltaTime * _transitionSpeed);
//                }
//            }

//            HandleMouse();    //New Addition
//            HorizontalPanning();
//            VerticalZooming();
//        }
//    }
//}

//private void ViewShifter(int inViewNumber, float inTransitionSpeed)
//{
//    _currentView = _views[inViewNumber];

//    _CameraParent.position = Vector3.Lerp(_CameraParent.position, _currentView.position, inTransitionSpeed);// Time.deltaTime * _transitionSpeed);

//    Vector3 currentAngle = new Vector3(
//        Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, inTransitionSpeed),// Time.deltaTime * _transitionSpeed),
//        Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, inTransitionSpeed),//Time.deltaTime * _transitionSpeed),
//        Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, inTransitionSpeed));//Time.deltaTime * _transitionSpeed));

//    _CameraParent.eulerAngles = currentAngle;
//}

//void HandleMouse()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        lastPanPosition = Input.mousePosition;
//    }
//    else if (Input.GetMouseButton(0))
//    {
//        timeToAcceleration += 1 * Time.deltaTime;
//        if (timeToAcceleration > 1.5f)
//        {
//            //Debug.Log(this.gameObject.GetComponent<Camera>().ScreenToViewportPoint(lastPanPosition - Input.mousePosition));
//            Pan(1 * Time.deltaTime * Input.mousePosition.x);
//        }
//        else
//        {
//            PanCamera(Input.mousePosition);
//        }
//    }
//    if (Input.GetMouseButtonUp(0))
//    {
//        timeToAcceleration = 0;
//    }
//} //New Addition

//void PanCamera(Vector3 newPanPosition)
//{
//    Vector3 offset = this.gameObject.GetComponent<Camera>().ScreenToViewportPoint(lastPanPosition - newPanPosition);
//    Vector3 move = new Vector3(offset.x * PanSpeed, offset.y, offset.y * PanSpeed);


//    _CameraParent.transform.Translate(move, Space.World);

//    Vector3 pos = _CameraParent.transform.position;
//    pos.x = Mathf.Clamp(_CameraParent.transform.position.x, _CameraLeftBound, _CameraRightBound);
//    pos.z = Mathf.Clamp(_CameraParent.transform.position.z, mCameraFarBound, mCameraNearBound);
//    _CameraParent.transform.position = pos;

//    lastPanPosition = newPanPosition;
//} //New Addition

//public void HorizontalPanning()
//{
//    float panSpeed = 0;

//    if (Input.GetMouseButtonDown(0))
//    {
//        mInitialPositionX = Input.mousePosition.x;
//    }

//    if (_CameraFreeRoam)
//    {
//        mChangedPositionX = Input.mousePosition.x;

//        if (mChangedPositionX == mInitialPositionX)
//        {
//            return;
//        }
//        if (mChangedPositionX < mInitialPositionX - 50f)
//        {
//            panSpeed = mZoomSpeed * -1f * Time.deltaTime;
//            Pan(mHorizontalPanSpeed * Time.deltaTime);
//        }
//        if (mChangedPositionX > mInitialPositionX + 50f)
//        {
//            panSpeed = mZoomSpeed * Time.deltaTime;
//            Pan(mHorizontalPanSpeed * -1f * Time.deltaTime);
//        }
//    }
//    if (!_isCameraInConstructionView)
//    {

//        if (!Input.GetMouseButton(0))
//        {
//            PlayCameraBoundEffectX();
//            PlayCameraBoundEffect(_CameraParent.position.x, _CameraRightBound, _CameraLeftBound, new Vector3(_CameraLeftBound, _CameraParent.position.y, _CameraParent.position.z), new Vector3(_CameraRightBound, _CameraParent.position.y, _CameraParent.position.z));
//        }
//    }

//    if ((transform.position.x <= _CameraRightBound + 30 && panSpeed > 0) || (transform.position.x >= _CameraLeftBound - 2 && panSpeed < 0))
//    {
//        transform.Translate(panSpeed * transform.right);
//    }

//}

//private void Pan(float inPanSpeed) //New Addition
//{
//    if ((_CameraParent.position.x <= _CameraRightBound + 30 && inPanSpeed > 0) || (_CameraParent.position.x >= _CameraLeftBound - 30 && inPanSpeed < 0))
//    {
//        _CameraParent.Translate(inPanSpeed * _CameraParent.right);
//    }
//}

/// <summary>
/// We get the input position in y on click.
/// And keep updating the input.y position as save it to mInitialPosition and keep taking count of whats the changed Position and see if its greater or lesser
/// and do action accordingly
/// </summary>
//private void VerticalZooming()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        mInitialPositionY = Input.mousePosition.y;
//    }

//    if (_CameraFreeRoam)
//    {
//        mChangedPositionY = Input.mousePosition.y;

//        if (mChangedPositionY == mInitialPositionY)
//        {
//            return;
//        }
//        if (mChangedPositionY < mInitialPositionY - 100f) //Plus and -100 is to restrict the movement diagonally
//        {
//            Zoom(mZoomSpeed * Time.deltaTime);
//        }
//        if (mChangedPositionY > mInitialPositionY + 100f)
//        {
//            Zoom(mZoomSpeed * -1f * Time.deltaTime);
//        }
//    }

//    if (!_isCameraInConstructionView)
//    {
//        if (!Input.GetMouseButton(0))
//        {
//            PlayCameraBoundEffectZ();
//            PlayCameraBoundEffect(_CameraParent.position.z, mCameraNearBound, mCameraFarBound, new Vector3(_CameraParent.position.x, _CameraParent.position.y, mCameraFarBound), new Vector3(_CameraParent.position.x, _CameraParent.position.y, mCameraNearBound));
//            PlayCameraBoundEffect(_CameraParent.position.y, _CameraUpBound, _CameraDownBound, new Vector3(_CameraParent.position.x, _CameraDownBound, _CameraParent.position.z), new Vector3(_CameraParent.position.x, _CameraUpBound, _CameraParent.position.z));
//        }
//    }
//}

/// <summary>
/// Zoom Condition
/// check for camera near and far bounds, check conditions independently
/// Give a small buffer value to bring in rubber band effect for the camera
/// Buffervalue being 2
/// </summary>
/// <param name = "inZoomSpeed" ></ param >
//private void Zoom(float inZoomSpeed)
//{                                                                                //New Change
//    if ((_CameraParent.position.z <= mCameraNearBound + 30 && inZoomSpeed > 0 && _CameraParent.position.y <= _CameraUpBound + 30) || (_CameraParent.position.z >= mCameraFarBound - 30 && inZoomSpeed < 0 && _CameraParent.position.y >= _CameraDownBound - 30))
//    {
//        _CameraParent.Translate(inZoomSpeed * _CameraParent.forward, Space.World);
//    }
//}

//public void PlayCameraBoundEffect(float inCameraDirection, float inBound1, float inBound2, Vector3 inCameraBound1, Vector3 inCameraBound2) //Function Modification
//{
//    Vector3 newCameraParentPos = Vector3.zero;
//    if (inCameraDirection > inBound1 || inCameraDirection < inBound2)
//    {

//        if (Mathf.Abs(inBound2 - inCameraDirection) < Mathf.Abs(inBound1 - inCameraDirection))
//        {
//            newCameraParentPos = inCameraBound1;
//        }
//        else
//        {
//            newCameraParentPos = inCameraBound2;
//        }

//        _CameraParent.position = Vector3.Lerp(_CameraParent.position, newCameraParentPos, 0.1f);
//    }
//}
#endregion
//if (!_DrawButtonClicked || !_buildButtonClicked)
//{
//    if (!_CameraFreeRoam)
//    {
//        OpenCardRegion.SetActive(false);
//        if (Mathf.Floor(_CameraParent.rotation.eulerAngles.x) != _views[0].rotation.eulerAngles.x)
//        {
//            _currentView = _views[0];
//            _CameraParent.position = Vector3.Lerp(_CameraParent.position, _currentView.position, Time.deltaTime * _transitionSpeed);

//            Vector3 currentAngle = new Vector3(
//                Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, Time.deltaTime * _transitionSpeed),
//                Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, Time.deltaTime * _transitionSpeed),
//                Mathf.LerpAngle(_CameraParent.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, Time.deltaTime * _transitionSpeed));

//            _CameraParent.eulerAngles = currentAngle;
//        }
//    }

//    HorizontalPanning();
//    VerticalZooming();
//}
/// <summary>
/// Responsible for Making the camera move right & left along with rotation.
/// 1. We store first touch position as previous position or initial position when we click mouseButtonDown
/// 2. With mouseButtonDown being true we keep tracking the mouseposition and store it to newPosition and then we take the initial/previous position
/// and check the differnce and store it in as direction as it says which direction are we moving
/// </summary>
//private void HorizontalPanningWithRotation()
//{
//    if (Input.GetMouseButtonDown(0))
//    {
//        mInitialPositionX = Input.mousePosition.x;
//    }

//    if (_CameraFreeRoam)
//    {
//        mChangedPositionX = Input.mousePosition.x;
//        float rotateDegrees = 0f;
//        if (mChangedPositionX < mInitialPositionX + 5f)
//        {
//            rotateDegrees -= mHorizontalPanSpeed * Time.deltaTime;
//        }
//        if (mChangedPositionX > mInitialPositionX - 5f)
//        {
//            rotateDegrees += mHorizontalPanSpeed * Time.deltaTime;
//        }
//        Vector3 currentVector = transform.position - mTargetToRotateAround.position;
//        currentVector.y = 0;
//        float angleBetween = Vector3.Angle(initialVector, currentVector) * (Vector3.Cross(initialVector, currentVector).y > 0 ? 1 : -1);
//        float newAngle = Mathf.Clamp(angleBetween + rotateDegrees, -_RotationLimit, _RotationLimit);
//        rotateDegrees = newAngle - angleBetween;

//        transform.RotateAround(mTargetToRotateAround.position, Vector3.up, rotateDegrees);

//        mInitialPositionX = mChangedPositionX;


//    }

//}
/// <summary>
/// Reset the camera position when touch is released, to set it back to its closest bound, either far or near. 
/// </summary>
//public void PlayCameraBoundEffectZ()
//{
//    Vector3 newCameraParentPos = Vector3.zero;


//    if (_CameraParent.position.z > mCameraNearBound || _CameraParent.position.z < mCameraFarBound)
//    {

//        if (Mathf.Abs(mCameraFarBound - _CameraParent.position.z) < Mathf.Abs(mCameraNearBound - _CameraParent.position.z))
//        {
//            newCameraParentPos = new Vector3(_CameraParent.position.x, _CameraParent.position.y, mCameraFarBound);
//        }
//        else
//        {
//            newCameraParentPos = new Vector3(_CameraParent.position.x, _CameraParent.position.y, mCameraNearBound);
//        }

//        _CameraParent.position = Vector3.Lerp(_CameraParent.position, newCameraParentPos, 0.1f);
//    }
//}

//public void PlayCameraBoundEffectX()
//{
//    Vector3 newCameraParentPos = Vector3.zero;


//    if (_CameraParent.position.x > _CameraRightBound || _CameraParent.position.x < _CameraLeftBound)
//    {

//        if (Mathf.Abs(_CameraLeftBound - _CameraParent.position.x) < Mathf.Abs(_CameraRightBound - _CameraParent.position.x))
//        {
//            newCameraParentPos = new Vector3(_CameraLeftBound, _CameraParent.position.y, _CameraParent.position.z);
//        }
//        else
//        {
//            newCameraParentPos = new Vector3(_CameraRightBound, _CameraParent.position.y, _CameraParent.position.z);
//        }

//        _CameraParent.position = Vector3.Lerp(_CameraParent.position, newCameraParentPos, 0.1f);
//    }
//}

//+              //-

//void Panning()
//    {

//[SerializeField] private float mCameraRightBound;
//[SerializeField] private float mCameraLeftBound;

//        //    if (mChangedPositionX == mInitialPositionX)
//        //    {
//        //        return;
//        //    }
//        //    if (mChangedPositionX < mInitialPositionX - 100f) //Plus and -100 is to restrict the movement diagonally
//        //    {
//        //        Pan(mHorizontalPanSpeed  * Time.deltaTime);
//        //    }
//        //    if (mChangedPositionX > mInitialPositionX + 100f)
//        //    {
//        //        Pan(mHorizontalPanSpeed  * -1f * Time.deltaTime);
//        //    }

//        //}
//        //if (!Input.GetMouseButton(0))
//        //{
//        //    PlayCameraBoundEffect();
//        //}

//        private void Pan(float inPanSpeed)
//        {
//            if ((_CameraParent.position.x <= mCameraRightBound + 30 && inPanSpeed > 0) || (_CameraParent.position.x >= mCameraLeftBound - 30 && inPanSpeed < 0))
//            {
//                _CameraParent.Translate(inPanSpeed * _CameraParent.right);
//            }
//        }
//    }