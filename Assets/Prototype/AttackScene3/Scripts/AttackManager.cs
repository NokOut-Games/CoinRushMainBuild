using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttackManager : MonoBehaviour
{
    public bool isDataChanging=true;

    [SerializeField] private GameManager mGameManager;
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

    [SerializeField] EnemyInfoPopulator profileInUI;
    [SerializeField]List<PlayerProfile> PreSetPayers = new List<PlayerProfile>();
    bool isSpawned;

    private void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _enemyName.text = MultiplayerManager.Instance.mPlayerName;
        _enemyPlayerLevel = MultiplayerManager.Instance._enemyPlayerLevel;
        if ( MultiplayerManager.Instance.MultiplayerBuildingDetails.Count <= 0)
        {
            PlayerProfile profile = PreSetPayers[Random.Range(0, PreSetPayers.Count)];
            _enemyPlayerLevel = profile.Level;

            InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints();

            foreach (var building in profile.Buildings)
            {
                building._buildingCurrentLevel = Random.Range(0, 5);
                building._isBuildingDestroyed = Random.Range(0, 100) > 50;
                building._isBuildingShielded = Random.Range(0, 100) > 50;
            }

            InstantiatePresetBuildings(profile.Buildings);
            profileInUI.ChangeProfile(profile.Picture, profile.Name);
        }
        else
        {
            InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints();
            InstantiateBuildingBasedOnLevel();
        }
        mMultiplayerPlayerData = FindObjectOfType<MultiplayerPlayerData>();
        cam = Camera.main;
        mCameraController = cam.GetComponent<AttackCameraController>();

        Invoke(nameof(UpdateCamerHorizontalBounds), 0.5f);

        Invoke(nameof(TargetInstantiation), 0.5f);
        InvokeRepeating("DoMultiplierSwitching", 0.5f, _MultiplierSwitchTime);
        ShuffleBuildingCostList();
    }
    private void Update()
    {
        TargetButtonPositionUpdate();

      
       /* if (!isSpawned)
        {
            isSpawned = true;
            
        }*/


       /* if (_enemyBuildingsTransformList.Count == MultiplayerManager.Instance.MultiplayerBuildingDetails.Count) return;
        else
        {
            InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints();
        }

        if (_enemyBuildings.Count == MultiplayerManager.Instance.MultiplayerBuildingDetails.Count) return;
        else
        {
            InstantiateBuildingBasedOnLevel();
        }*/
    }


    void InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints()
    {
        Instantiate(_LevelHolder[_enemyPlayerLevel - 1], Vector3.zero, Quaternion.identity);
        mTransformPoint = GameObject.Find("TransformPoints");

        for (int i = 0; i < MultiplayerManager.Instance.MultiplayerBuildingDetails.Count; i++)
        {
            bool shieldedEnemy = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._isBuildingShielded;
            _shieldedEnemyBuildings.Add(shieldedEnemy);
        }

        for (int i = 0; i < mTransformPoint.transform.childCount; i++)
        {
            _enemyBuildingsTransformList.Add(mTransformPoint.transform.GetChild(i));
        }
    }
 
    void InstantiateBuildingBasedOnLevel()
    {
        for (int i = 0; i < MultiplayerManager.Instance.MultiplayerBuildingDetails.Count; i++)
        {
            GameObject enemyBuilding;

            if (!MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._isBuildingDestroyed)
            {
                if (MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel) as GameObject;

                    enemyBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, _enemyBuildingsTransformList[i].rotation);
                    enemyBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;

                    enemyBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _enemyPlayerLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "Image");
                    enemyBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    enemyBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "0";
                }
                _enemyBuildings.Add(enemyBuilding);
            }
            else
            {
                if (MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel) as GameObject;
                    enemyBuilding = Instantiate(/*mGameManager._BuildingDetails*/ building, _enemyBuildingsTransformList[i].position, _enemyBuildingsTransformList[i].rotation);
                    enemyBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;
                    enemyBuilding = Instantiate(/*mGameManager._BuildingDetails*/building, _enemyBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _enemyPlayerLevel + "/" + MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "Image");
                    enemyBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    enemyBuilding.name = MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName + "0";

                }
                enemyBuilding.transform.position = new Vector3(enemyBuilding.transform.position.x, _buildingSinkPositionAmount, enemyBuilding.transform.position.z);
                enemyBuilding.transform.rotation = Quaternion.Euler(enemyBuilding.transform.eulerAngles.x, enemyBuilding.transform.eulerAngles.y, _buildingTiltRotationAmount);
                Instantiate(_destroyedSmokeEffectVFX, enemyBuilding.transform.position, Quaternion.identity, enemyBuilding.transform);
                _enemyBuildings.Add(enemyBuilding);
            }
        }
    }

    void InstantiatePresetBuildings(List<Building> BuildingsDetails)
    {
        for (int i = 0; i < BuildingsDetails.Count; i++)
        {
            GameObject otherPlayerBuilding;

            if (!BuildingsDetails[i]._isBuildingDestroyed)
            {
                if (BuildingsDetails[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + BuildingsDetails[i]._buildingName + BuildingsDetails[i]._buildingCurrentLevel) as GameObject;

                    otherPlayerBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, _enemyBuildingsTransformList[i].rotation);
                    otherPlayerBuilding.name = BuildingsDetails[i]._buildingName + BuildingsDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;

                    otherPlayerBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _enemyPlayerLevel + "/" + BuildingsDetails[i]._buildingName + "Image");
                    otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    otherPlayerBuilding.name = BuildingsDetails[i]._buildingName + "0";
                }
                _enemyBuildings.Add(otherPlayerBuilding);
            }
            else
            {
                if (BuildingsDetails[i]._buildingCurrentLevel != 0)
                {
                    GameObject building = Resources.Load("Level" + _enemyPlayerLevel + "/" + BuildingsDetails[i]._buildingName + BuildingsDetails[i]._buildingCurrentLevel) as GameObject;
                    otherPlayerBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, _enemyBuildingsTransformList[i].rotation);
                    otherPlayerBuilding.name = BuildingsDetails[i]._buildingName + BuildingsDetails[i]._buildingCurrentLevel;
                }
                else
                {
                    GameObject building = Resources.Load("Plunk_Attack") as GameObject;
                    otherPlayerBuilding = Instantiate(building, _enemyBuildingsTransformList[i].position, Quaternion.identity);
                    Sprite BuildingImage = Resources.Load<Sprite>("Level" + _enemyPlayerLevel + "/" + BuildingsDetails[i]._buildingName + "Image");
                    otherPlayerBuilding.GetComponentInChildren<SpriteRenderer>().sprite = BuildingImage;
                    otherPlayerBuilding.name = BuildingsDetails[i]._buildingName + "0";

                }
                otherPlayerBuilding.transform.position = new Vector3(otherPlayerBuilding.transform.position.x, _buildingSinkPositionAmount, otherPlayerBuilding.transform.position.z);
                otherPlayerBuilding.transform.rotation = Quaternion.Euler(otherPlayerBuilding.transform.eulerAngles.x, otherPlayerBuilding.transform.eulerAngles.y, _buildingTiltRotationAmount);
                Instantiate(_destroyedSmokeEffectVFX, otherPlayerBuilding.transform.position, Quaternion.identity, otherPlayerBuilding.transform);
                _enemyBuildings.Add(otherPlayerBuilding);
            }
        }

    }


    private void UpdateCamerHorizontalBounds()
    {
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
    public void ResetAnim()
    {
        _spawnedTargetPoints[cachedTargetPoint].GetComponentInChildren<Animator>().SetBool("CanPlay", false);
    }

    void DoMultiplierSwitching()
    {
        if (_multiplierGameObject == null)
        {
            _multiplierGameObject = _spawnedTargetPoints[0];
        }


        int rand = Random.Range(0,_enemyBuildings.Count);
        cachedTargetPoint = rand;
        _multiplierGameObject = _spawnedTargetPoints[cachedTargetPoint];

        _spawnedTargetPoints[cachedTargetPoint].GetComponentInChildren<Animator>().SetBool("CanPlay", true);
        Invoke("ResetAnim", 0.25f);
    }  
    void TargetInstantiation()
    {

        for (int i = 0; i < _enemyBuildings.Count; i++)
        {
            GameObject go = Instantiate(_TargetButton) as GameObject;
            go.transform.SetParent(_CanvasGO.transform);

            Vector3 screenPos = Camera.main.WorldToScreenPoint(_enemyBuildings[i].transform.position);
            screenPos.y = screenPos.y + HeightAdjustment;
            screenPos.z = 0;
            go.transform.position = screenPos;


            go.name = i.ToString();

            _spawnedTargetPoints.Add(go);

            go.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                
                AssignTarget(_enemyBuildings[int.Parse(go.name)].transform);
                if (_shieldedEnemyBuildings[int.Parse(go.name)])
                    _Shield = _shieldedEnemyBuildings[int.Parse(go.name)];

                TargetObjectIndex = int.Parse(go.name);

                if (_multiplierGameObject.name == go.name)
                {

                    _multiplierSelected = true;

                }

                for (int i = 0; i < _spawnedTargetPoints.Count; i++)
                {
                    _spawnedTargetPoints[i].GetComponentInChildren<Image>().enabled = false;
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
            Vector3 screenPos = Camera.main.WorldToScreenPoint(_enemyBuildings[i].transform.position);
            screenPos.y = screenPos.y + HeightAdjustment;
            screenPos.z = 0;
            go.transform.position = screenPos;
        }
    }


    public void AssignTarget(Transform trans)
    {

        if (_AllowInteraction == true)
        {
            _AllowInteraction = false;
            _TargetTransform = trans;

            GameObject CamParent = Camera.main.gameObject;
            CamParent.GetComponent<AttackCameraController>()._CameraFreeRoam = false;


            for (int i = 0; i < mMultiplayerPlayerData._buildingMultiplayerDataRef.Count; i++)
            {
                if (_shieldedEnemyBuildings[i])
                {
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
        StartCoroutine(ScoreCalculation(_TargetTransform));

        if (_Shield == true)
        {
            Debug.Log("shield Activated");
        }
        else
        {
            Invoke(nameof(MakeBuildingDestroyed), 4.5f);

        }
        ChangeEnemyBuildingData();


    }

    public IEnumerator ScoreCalculation(Transform trans)
    {

        int RewardValue = _buildingCost[TargetObjectIndex];

        if (_multiplierSelected == true)
        {
            _ScoreTextTwo.text = "Multiplier (2x) - " + RewardValue + "*2";
            RewardValue = RewardValue * 2;
        }
        mGameManager._coins = mGameManager._coins + _buildingCost[TargetObjectIndex];

        _ScoreTextOne.text = "Building Cost - " + RewardValue;

        yield return new WaitForSeconds(7);
        _ScorePanel.SetActive(true);

    }

    public void CannonActivation()
    {
        _Cannon.GetComponent<CannonShotController>().AssignPos(_TargetTransform);
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = Camera.main.transform.position;
        Vector3 endPos = new Vector3(_TargetTransform.position.x-20+63.6f , CameraAttackPosition.y+220+26, _TargetTransform.position.z - 570-427); //new Vector3(-476.4f, 316, -570);


        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);

            Camera.main.transform.position = Vector3.Lerp(startingPos, endPos, t * 3);
            yield return 0;
        }
    }
    public void ChangeEnemyBuildingData()
    {
        if (isDataChanging)
        {
            var alteredTargetName = _TargetTransform.name.Substring(0, _TargetTransform.name.Length - 1);
            string enemyBuildingName = alteredTargetName;
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
            MultiplayerManager.Instance.WriteDetailsOnAttackComplete();
        }
       
    }
    public void BackButton()
    {
        _ScorePanel.GetComponentInChildren<Button>().interactable = false;
        //MultiplayerManager.Instance.CheckAttackDataFromFirebase();
         LevelLoadManager.instance.BacktoHome();
        //ChangeEnemyBuildingData();

    }


    void MakeBuildingDestroyed()
    {
        _TargetTransform.position = new Vector3(_TargetTransform.position.x, _buildingSinkPositionAmount, _TargetTransform.position.z);
        _TargetTransform.rotation = Quaternion.Euler(_TargetTransform.transform.eulerAngles.x, _TargetTransform.eulerAngles.y, _buildingTiltRotationAmount);
        Instantiate(_destroyedSmokeEffectVFX, _TargetTransform.position, Quaternion.identity, _TargetTransform);
    }
}

