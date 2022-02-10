using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttackManager : MonoBehaviour
{
    public bool isDataChanging=true;

    [SerializeField] private GameManager mGameManager;
    // public List<GameObject> _TargetPoints = new List<GameObject>();
    public List<GameObject> _spawnedTargetPoints = new List<GameObject>();
    public GameObject _TargetPrefab;
    public GameObject _multiplierPrefab;
    public GameObject _multiplierGameObject;
    public GameObject _Cannon;
    public float _MultiplierSwitchTime = 1.0f;
    public GameObject _ScorePanel;
    public Text _ScoreTextOne;
    public Text _ScoreTextTwo;
    public Text _ScoreTextThree;
    public GameObject _bulletPre;
    public Sprite _Sprite1, _Sprite2, _Sprite3, _Sprite4, _Sprite5, _Sprite6, _Sprite7, _Sprite8, _Sprite9;
    public Transform _TargetTransform;
    public bool _Shield = false;
    public Quaternion CameraAttackRotation;
    public Vector3 CameraAttackPosition;
    public bool _AllowInteraction = true;
    public GameObject MultiplierGO;
    public GameObject _CanvasGO;
    public GameObject _TargetButton;
    public float HeightAdjustment = 100;
    public Sprite _TargetSprite;
    public Sprite _MultiplierSprite;
    public GameObject shieldPref;
    public bool _multiplierSelected = false;

    private Camera cam;
    private int cachedTargetPoint = -1;
    private int TargetObjectIndex;
    private float transitionDuration = 2.5f;
    public GameObject obj;

    public MultiplayerPlayerData mMultiplayerPlayerData;
    public int _enemyPlayerLevel;

    [Space]
    [Header("EnemyDetails")]
    public Text _enemyName;
    public RawImage _enemyDisplayPicture;
    [Space]

    public List<GameObject> mEnemyBuildingPrefabPopulateList;
    public List<GameObject> _enemyBuildings;

    [SerializeField] private GameObject mTransformPoint;
    public List<Transform> _enemyBuildingsTransformList;

    public List<GameObject> _LevelHolder = new List<GameObject>();
    public List<bool> _shieldedEnemyBuildings;

    [SerializeField] private AttackCameraController mCameraController;
    [SerializeField] private float extraCameraPanDistance;

    public float _buildingSinkPositionAmount = -50;
    public float _buildingTiltRotationAmount = -25;
    public GameObject _destroyedSmokeEffectVFX;

    public List<int> _buildingCost = new List<int>(9);

    public Vector3 _ballAndShieldOffsetToTargetTransform = new Vector3(0, 50, 0);

    private void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        mMultiplayerPlayerData = GameObject.Find("MultiplayerManager").GetComponent<MultiplayerPlayerData>();

       /* _enemyDisplayPicture.texture = mMultiplayerPlayerData._enemyImageTexture; 
        _enemyName.text = mMultiplayerPlayerData._enemyName;
        _enemyPlayerLevel = mMultiplayerPlayerData._enemyPlayerLevel;


        InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints();

        Invoke(nameof(InstantiateBuildingBasedOnLevel), 0f);*/
    }
    

    void InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints()
    {
        Instantiate(_LevelHolder[_enemyPlayerLevel - 1], Vector3.zero, Quaternion.identity);
        mTransformPoint = GameObject.Find("TransformPoints");

        for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
        {
            bool shieldedEnemy = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingShielded;
            _shieldedEnemyBuildings.Add(shieldedEnemy);
        }

        for (int i = 0; i < mTransformPoint.transform.childCount; i++)
        {
            _enemyBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
        }
    }


    void InstantiateBuildingBasedOnLevel()
    {
        for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
        {
            GameObject enemyBuilding;
            
            if (!mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingDestroyed)
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
                {
                   GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;

                    enemyBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, _enemyBuildingsTransformList[i].rotation);
                    enemyBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;

                    enemyBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _enemyPlayerLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "Image");
                    enemyBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    enemyBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";
                }
                _enemyBuildings.Add(enemyBuilding);
            }
            else
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel) as GameObject;
                    enemyBuilding = Instantiate(/*mGameManager._BuildingDetails*/ building, _enemyBuildingsTransformList[i].position, _enemyBuildingsTransformList[i].rotation);
                    enemyBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;
                    enemyBuilding = Instantiate(/*mGameManager._BuildingDetails*/building, _enemyBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _enemyPlayerLevel + "/" + mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "Image");
                    enemyBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    enemyBuilding.name = mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName + "0";

                }
                enemyBuilding.transform.position = new Vector3(enemyBuilding.transform.position.x, _buildingSinkPositionAmount, enemyBuilding.transform.position.z);
                enemyBuilding.transform.rotation = Quaternion.Euler(enemyBuilding.transform.eulerAngles.x, enemyBuilding.transform.eulerAngles.y, _buildingTiltRotationAmount);
                Instantiate(_destroyedSmokeEffectVFX, enemyBuilding.transform.position, Quaternion.identity, enemyBuilding.transform);
                _enemyBuildings.Add(enemyBuilding);
            }
            // }
        }
    }

    private void Start()
    {
        mMultiplayerPlayerData = FindObjectOfType<MultiplayerPlayerData>();
        cam = Camera.main;
        mCameraController = cam.GetComponent<AttackCameraController>();


        _enemyDisplayPicture.texture = mMultiplayerPlayerData._enemyImageTexture;
        _enemyName.text = mMultiplayerPlayerData._enemyName;
        _enemyPlayerLevel = mMultiplayerPlayerData._enemyPlayerLevel;


        InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints();

        Invoke(nameof(InstantiateBuildingBasedOnLevel), 0f);

        Invoke(nameof(UpdateCamerHorizontalBounds), 0.5f);
        Debug.Log(Application.targetFrameRate + "Target Fram Rate ");

        Invoke(nameof(TargetInstantiation), 0.5f);
        //MultiplierInstantiation();
        InvokeRepeating("DoMultiplierSwitching", 0.5f, _MultiplierSwitchTime);
        ShuffleBuildingCostList();
    }

    /// <summary>
    /// Updates the camera bounds based on Level Size
    /// </summary>
    private void UpdateCamerHorizontalBounds()
    {
        //float leftMostBuildingX = Mathf.Min(_enemyBuildings[i].transform.position.x);
        List<float> buildingXPos = new List<float>();
        for (int i = 0; i < _enemyBuildings.Count; i++)
        {
            buildingXPos.Add(_enemyBuildings[i].transform.position.x);
        }
        float leftMostBuilding = Mathf.Min(buildingXPos.ToArray());
        float rightMostBuilding = Mathf.Max(buildingXPos.ToArray());

        mCameraController._CameraLeftBound = leftMostBuilding - extraCameraPanDistance;
        mCameraController._CameraRightBound = rightMostBuilding + extraCameraPanDistance;
    }

    /// <summary>
    /// Shuffles the Building_Cost list
    /// </summary>
    private void ShuffleBuildingCostList()
    {
        for (int i = 0; i < _buildingCost.Count; i++)
        {
            int temp = _buildingCost[i];
            int randomIndex = Random.Range(i, _buildingCost.Count);
            _buildingCost[i] = _buildingCost[randomIndex];
            _buildingCost[randomIndex] = temp;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        TargetButtonPositionUpdate();
    }

    public void ResetAnim()
    {
        _spawnedTargetPoints[cachedTargetPoint].GetComponentInChildren<Animator>().SetBool("CanPlay", false);
    }

    /// <summary>
    /// This Function helps in moving the 2X Multiplier randomly Over the buildings
    /// </summary>
    void DoMultiplierSwitching()
    {
        if (_multiplierGameObject == null)
        {
            // newMultiplier = mGameManager._TargetMarkPost[0];
            _multiplierGameObject = _spawnedTargetPoints[0];
        }


        int rand = Random.Range(0, /*mGameManager._BuildingDetails*/_enemyBuildings.Count);
        cachedTargetPoint = rand;
        _multiplierGameObject = _spawnedTargetPoints[cachedTargetPoint];

        _spawnedTargetPoints[cachedTargetPoint].GetComponentInChildren<Animator>().SetBool("CanPlay", true);
        Invoke("ResetAnim", 0.25f);
    }


    /// <summary>
    /// This helps in Instantiating the Target Mark on the Buildings.
    /// </summary>    
    void TargetInstantiation()
    {

        for (int i = 0; i < /*mGameManager._BuildingDetails*/_enemyBuildings.Count; i++)
        {

            GameObject go = Instantiate(_TargetButton) as GameObject; //GameObject.Instantiate(_Button);//Instantiate(_Button, Vector3.zero, Quaternion.identity) as Button;
            go.transform.SetParent(_CanvasGO.transform);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(/*mGameManager._BuildingDetails[i]*/_enemyBuildings[i].transform.position);
            screenPos.y = screenPos.y + HeightAdjustment;
            screenPos.z = 0;
            go.transform.position = screenPos;


            go.name = i.ToString();

            _spawnedTargetPoints.Add(go);

            go.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                //Debug.Log(go.GetComponent<Image>().sprite.name + "Selected Sprite Name" );
                AssignTarget(/*mGameManager._BuildingDetails*/_enemyBuildings[int.Parse(go.name)].transform);
                if (/*mGameManager._BuildingShield*/_shieldedEnemyBuildings[int.Parse(go.name)] == true)
                    _Shield = /*mGameManager._BuildingShield*/_shieldedEnemyBuildings[int.Parse(go.name)];

                TargetObjectIndex = int.Parse(go.name);

                if (_multiplierGameObject.name == go.name)
                {

                    _multiplierSelected = true;

                }

                for (int i = 0; i < _spawnedTargetPoints.Count; i++)
                {
                    _spawnedTargetPoints[i].GetComponentInChildren<Image>().enabled = false;
                    //_spawnedTargetPoints[i].GetComponent<Image>().enabled = false;
                    _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<Animator>().enabled = true;
                }
            });

        }
    }

    public void TargetButtonPositionUpdate()
    {
        for (int i = 0; i < _spawnedTargetPoints.Count; i++)
        {

            GameObject go = _spawnedTargetPoints[i];
            Vector3 screenPos = Camera.main.WorldToScreenPoint(/*mGameManager._BuildingDetails*/_enemyBuildings[i].transform.position);
            screenPos.y = screenPos.y + HeightAdjustment;
            screenPos.z = 0;
            go.transform.position = screenPos;
        }
    }


    /// <summary>
    /// This Helps in Instantiating the 2X Multiplier 
    /// </summary>    
    void MultiplierInstantiation()
    {

        Vector3 newMultiplier = mGameManager._TargetMarkPost[0];
        _multiplierGameObject = Instantiate(_multiplierPrefab, newMultiplier, Quaternion.identity);
        _multiplierGameObject.name = 0.ToString();
    }


    /// <summary>
    /// This gets the Target mark Transform Details during on mouse Down click 
    /// </summary>
    /// <param name="trans"></param>
    public void AssignTarget(Transform trans)
    {
        Debug.LogError("Assign Target Called");

        if (_AllowInteraction == true)
        {
            _AllowInteraction = false;
            _TargetTransform = trans;

            GameObject CamParent = Camera.main.gameObject; //GameObject.Find("CameraParent");
            CamParent.GetComponent<AttackCameraController>()._CameraFreeRoam = false;


            for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
            {
                if (/*mGameManager._BuildingShield*/_shieldedEnemyBuildings[i] == true)
                {
                    // _spawnedTargetPoints[i].transform.GetChild(0).gameObject.SetActive(true);
                    _spawnedTargetPoints[i].transform.GetChild(2).gameObject.SetActive(true);

                    if (_multiplierGameObject == _TargetTransform)
                    {
                        //  Debug.Log("multiplier 2x");
                        // _multiplierGameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

                switch (_buildingCost[i])
                {
                    case 1000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite1;
                        break;
                    case 2000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite2;
                        break;
                    case 3000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite3;
                        break;
                    case 4000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite4;
                        break;
                    case 5000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite5;
                        break;
                    case 6000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite6;
                        break;
                    case 7000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite7;
                        break;
                    case 8000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite8;
                        break;
                    case 9000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = _Sprite9;
                        break;
                }


            }
            Invoke("DisableBuildingCost", 2f);
            CancelInvoke("DoMultiplierSwitching");
            Invoke("PerformTarget", 2.1f);
        }
    }


    public void DisableBuildingCost()
    {
        for (int i = 0; i < _spawnedTargetPoints.Count; i++)
        {
            _spawnedTargetPoints[i].SetActive(false);

        }
        _multiplierGameObject.SetActive(false);
    }


    public void PerformTarget()
    {
        for (int i = 0; i < _spawnedTargetPoints.Count; i++)
        {
            _spawnedTargetPoints[i].SetActive(false);

        }
        // if (_multiplierGameObject.name != _TargetTransform.gameObject.name)
        {
            _multiplierGameObject.SetActive(false);
        }



        StartCoroutine(Transition());
        Camera.main.transform.rotation = CameraAttackRotation;

        Invoke("CannonActivation", 0f);
        //  ScoreCalculation(_TargetTransform);
        StartCoroutine(ScoreCalculation(_TargetTransform));

        if (_Shield == true)
        {
            Debug.Log("shield Activated");
        }
        else
        {
            Debug.Log("shield Not Activated");
        }

    }

    public IEnumerator ScoreCalculation(Transform trans)
    {
        Debug.Log("Scoring Calculation function Entered");

        int RewardValue = _buildingCost[TargetObjectIndex];

        if (_multiplierSelected == true)
        {
            _ScoreTextTwo.text = "Multiplier (2x) - " + RewardValue + "*2";
            RewardValue = RewardValue * 2;
        }
        //Debug.Log(TargetObjectIndex.)
        mGameManager._coins = mGameManager._coins + _buildingCost[TargetObjectIndex];

        _ScoreTextOne.text = "Building Cost - " + RewardValue;

        yield return new WaitForSeconds(7);
        _ScorePanel.SetActive(true);
        //_ScoreTextThree.transform.parent.gameObject.SetActive(true);
        //Debug.Log("I am Here");

    }

    public void CannonActivation()
    {
        // _Cannon.SetActive(true);
        //float zpos = 700;//_Cannon.transform.position.z - _TargetTransform.position.z; // Difference between last building
        //_Cannon.transform.position = new Vector3(_Cannon.transform.position.x, _Cannon.transform.position.y, _TargetTransform.position.z - zpos);
        //Debug.Log("Cannon Activation function" + _TargetTransform);
        _Cannon.GetComponent<CannonShotController>().AssignPos(_TargetTransform);
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = Camera.main.transform.position;
        Vector3 endPos = new Vector3(_TargetTransform.localPosition.x, CameraAttackPosition.y, CameraAttackPosition.z);


        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            Debug.Log("Inside Coroutine");

            Camera.main.transform.position = Vector3.Lerp(startingPos, endPos, t * 3);
            //GameObject temp = new GameObject();
            //temp.transform.LookAt(_TargetTransform);
            //Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, temp.transform.rotation, t*3);
            yield return 0;
        }
    }


    //public void BackButton()
    //{
    //    ChangeEnemyBuildingData();
    //    MultiplayerManager.Instance.WriteDetailsOnAttackComplete();
    //    MultiplayerManager.Instance.ReadMyData();
    //    Invoke("BackToGame", 2.5f);
    //}
    //public void BackToGame()
    //{
    //    LevelLoadManager.instance.BacktoHome();
    //}


    public void ChangeEnemyBuildingData()
    {
        if (isDataChanging)
        {
            Debug.Log("Changing enemy data");
            var alteredTargetName = _TargetTransform.name.Substring(0, _TargetTransform.name.Length - 1);
            Debug.LogError(alteredTargetName);
            string enemyBuildingName = alteredTargetName;
            //mMultiplayerPlayerData.onceDone = false;
            for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
            {
                if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._buildingName != enemyBuildingName)
                {
                    continue;
                }
                else
                {
                    if (mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingShielded)
                    {
                        mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingShielded = false;
                    }
                    else
                    {
                        mMultiplayerPlayerData._buildingMultiplayerDataRef[i]._isBuildingDestroyed = true;
                    }
                }
            }
            //Invoke("WriteData", 1f);
            MultiplayerManager.Instance.WriteDetailsOnAttackComplete();
        }
       
    }
    public void BackButton()
    {
        _ScorePanel.GetComponentInChildren<Button>().interactable = false;
        MultiplayerManager.Instance.CheckAttackDataFromFirebase();
        //MultiplayerManager.Instance.ReadMyData();
        FirebaseManager.Instance.ReadData();
        ChangeEnemyBuildingData();
    }
    //void WriteData()
    //{
    //    MultiplayerManager.Instance.WriteDetailsOnAttackComplete();
    //}
}

