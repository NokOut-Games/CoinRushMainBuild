using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttackManager : MonoBehaviour
{
    public bool isDataChanging = true;

    [SerializeField] private GameManager mGameManager;
    public List<GameObject> _spawnedTargetPoints = new List<GameObject>();
    public GameObject _TargetPrefab;
    public GameObject _multiplierPrefab;
    public GameObject _multiplierGameObject;
    public GameObject _Cannon;
    public float _MultiplierSwitchTime = 1.0f;
    public ResultPanelUI resultPanel;
    public Text _ScoreTextOne;
    public Text _ScoreTextTwo;
    public Text _ScoreTextThree;
    public GameObject _bulletPre;
    //public Sprite _Sprite1, _Sprite2, _Sprite3, _Sprite4, _Sprite5, _Sprite6, _Sprite7, _Sprite8, _Sprite9;
    public List<Sprite> valueSprites = new List<Sprite>(8);
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
    public int _enemyPlayerLevel;

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

    [SerializeField] Transform cannonAnimationCamera;
    [SerializeField] float DestroyAnimationDelay;
    [SerializeField] GameObject attackCanvas;
    private void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _enemyPlayerLevel = MultiplayerManager.Instance.Enemydata.UserDetails._playerCurrentLevel;

        InstantiateLevelAndPopulateShieldedBuildingsWithTransformPoints();
        InstantiateBuildingBasedOnLevel();

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
                
                Instantiate(_destroyedSmokeEffectVFX, enemyBuilding.transform.position, Quaternion.identity, enemyBuilding.transform).transform.GetChild(0).localPosition = new Vector3(0, 0, -enemyBuilding.transform.GetComponentInChildren<BoxCollider>().size.z / 2);

                _enemyBuildings.Add(enemyBuilding);
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
            Sprite spriteTemp = valueSprites[i];
            int randomIndex = Random.Range(i, _buildingCost.Count);
            valueSprites[i] = valueSprites[randomIndex];
            _buildingCost[i] = _buildingCost[randomIndex];
            _buildingCost[randomIndex] = temp;
            valueSprites[randomIndex] = spriteTemp;
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


        int rand = Random.Range(0, _enemyBuildings.Count);
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
                attackCanvas.SetActive(false);
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


            for (int i = 0; i < MultiplayerManager.Instance.MultiplayerBuildingDetails.Count; i++)
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
                    case 10:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[0];
                        break;
                    case 20:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[1];
                        break;
                    case 30:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[2];
                        break;
                    case 15000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[3];
                        break;
                    case 21000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[4];
                        break;
                    case 30000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[5];
                        break;
                    case 45000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[6];
                        break;
                    case 75000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[7];
                        break;
                    case 6000:
                        _spawnedTargetPoints[i].transform.GetChild(1).gameObject.GetComponent<TargetBuildingValues>()._Sprite = valueSprites[8];
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
        _multiplierGameObject.SetActive(false);


        CannonActivation();

        StartCoroutine(Transition());
        Camera.main.transform.rotation = CameraAttackRotation;

        StartCoroutine(ScoreCalculation(_TargetTransform));

        if (_Shield == true)
        {
            Debug.Log("shield Activated");
        }
        else
        {
            Invoke(nameof(MakeBuildingDestroyed), DestroyAnimationDelay);

        }


    }

    public IEnumerator ScoreCalculation(Transform trans)
    {
        float RewardValue = _buildingCost[TargetObjectIndex];

        if (_multiplierSelected)
        {
            RewardValue *= (_Shield ? 1.5f : 2);
            resultPanel.ShowMultiplierDetails(2, 3, "Attack Multiplier", (_Shield ? 1.5f : 2).ToString());
        }

        resultPanel.ShowMultiplierDetails(0, 0, "Multiplier", "" + GameManager.Instance._MultiplierValue);
        resultPanel.ShowMultiplierDetails(1, 1, "Cucu Multiplier", GameManager.Instance.cucuMultiplier.ToString());
        if (RewardValue.ToString().Length < 3)
        {
            mGameManager._energy +=(int)(RewardValue * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier);
            resultPanel.ShowResultTotal(1, ((int)(RewardValue * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier)).ToString());

        }
        else
        {
            mGameManager._coins += (int)(RewardValue * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier);
            resultPanel.ShowResultTotal(0, ((int)(RewardValue * GameManager.Instance._MultiplierValue * GameManager.Instance.cucuMultiplier)).ToString());
        }

        yield return new WaitForSeconds(7.5f);
        resultPanel.gameObject.SetActive(true);
    }


    public void CannonActivation()
    {
        _Cannon.GetComponent<CannonShotController>().AssignPos(_TargetTransform);
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = Camera.main.transform.position;
        Vector3 endPos = cannonAnimationCamera.position; //new Vector3(-476.4f, 316, -570);


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
            for (int i = 0; i < MultiplayerManager.Instance.MultiplayerBuildingDetails.Count; i++)
            {
                if (MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._buildingName != enemyBuildingName)
                {
                    continue;
                }
                else
                {
                    if (MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._isBuildingShielded)
                    {
                        MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._isBuildingShielded = false;
                    }
                    else
                    {
                        MultiplayerManager.Instance.MultiplayerBuildingDetails[i]._isBuildingDestroyed = true;
                    }
                }
            }
            MultiplayerManager.Instance.WriteDetailsOnAttackComplete();
        }

    }
    public void BackButton()
    {
        MultiplayerManager.Instance.CheckAttackDataFromFirebase();
        resultPanel.GetComponentInChildren<Button>().interactable = false;
        Invoke(nameof(ChangeEnemyBuildingData), 0.5f);

    }


    void MakeBuildingDestroyed()
    {
        _TargetTransform.position = new Vector3(_TargetTransform.position.x, _buildingSinkPositionAmount, _TargetTransform.position.z);
        _TargetTransform.rotation = Quaternion.Euler(_buildingTiltRotationAmount, _TargetTransform.eulerAngles.y, _TargetTransform.eulerAngles.z);
        Instantiate(_destroyedSmokeEffectVFX, _TargetTransform.position, Quaternion.identity, _TargetTransform);
    }
}

