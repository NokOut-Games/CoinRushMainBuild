using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

//Trying this
[System.Serializable]
public class BuildingData
{
    public string _buildingName;
    public string _buildingDisplayName;
    public int _buildingLevel = 0;
    public int _buildingMaxLevel;
    public Transform _buildingSpawnPoint;

    public GameObject currentLevelGameObject;
    public Sprite[] NextUpgradeImages; //Future


    public GameObject[] UpgradeLevels;
    public int[] UpgradeCosts;
    public GameObject _respectiveBuildingButtons;

    public bool isBuildingSpawnedAndActive; //Just in case for Attack
    public bool isBuildingDamaged; //Just in case to check if building is damaged or not.
    public bool isBuildingShielded;
    public bool didBuildingReachMaxLevel;
    public bool _isUnderConstruction;
}

public class BuildingManager : MonoBehaviour
{
    public List<BuildingData> _buildingData;
    [SerializeField]List<GameObject> _buildings = new List<GameObject>();

    private GameManager mGameManager;
    private CameraController mCameraControllerRef;

    [SerializeField] private GameObject mCameraParentRef;

    [SerializeField] private GameObject mBuildingConstructionVFX;
    [SerializeField] private GameObject mBuildingConstructionMaxVFX;
    [Tooltip("Speed the building should be focused when the upgrade of that button is being clicked")]
    [SerializeField] private float mCameraFocusSpeed;
    [Tooltip("Delay time to spawn the next upgrade")]
    [SerializeField] private float mBuildingSpawnTimeDelay;
    [Tooltip("Lesser the value the faster it happens / Higher the value the slower it happens")]
    [SerializeField] private float mBuildingShrinkAndEnlargeTime;

    [SerializeField] private Vector3 mCameraOffSetFromBuilding;
    [SerializeField] private Vector3 mParticleOffSetFromBuilding;

    [SerializeField] private Vector3 newBuildingSpawnScale;
    [SerializeField] private float mSizeToDecrease;

    public float mTimeDelayFromNewBuildingToCameraDefaultState;

    [HideInInspector]public List<int> _shieldedBuildings;

    public float _buildingSinkPositionAmount;
    public float _buildingTiltRotationAmount;
    public GameObject _destroyedSmokeEffectVFX;
    public GameObject _destroyedPlayerPlunkCard;
    public Vector3 _DestroyedBuildingPlunkOffsetFromBuilding;

    [SerializeField] MenuUI menuUI;
   public List<List<int>> listCost = new List<List<int>>();


    private void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        mGameManager._maxShield = _buildingData.Count;
        mCameraParentRef = GameObject.Find("CameraParent");
        mCameraControllerRef = mCameraParentRef.GetComponentInChildren<CameraController>();

        if (!GameManager.Instance._IsBuildingFromFBase)
        {
            PutCurrentLevelBuildingdetails();
            DestroyAllBuildings();
            SpawningBuilding();
            AllocateBuildingCostList();

        }
    }


    void AllocateBuildingCostList()
    {
        int x = 0;
        int buildingCount = GameManager.Instance.BuildingCost.Count / 5;
        for (int i = 0; i < buildingCount; i++, x += 4)
        {
            listCost.Add(new List<int>());
            for (int j = 0; j < GameManager.Instance.BuildingCost.Count / buildingCount; j++)
            {
                listCost[i].Add(int.Parse(GameManager.Instance.BuildingCost[j + x + i]));
            }
           
        }
        AssignBuildingCost();
    }
    void AssignBuildingCost()
    {
        for (int i = 0; i <_buildingData.Count; i++)
        {
            _buildingData[i].UpgradeCosts = listCost[i].ToArray();
        }
    }

    void Update()
    {
        ManageCameraStatesBasedOnBuildingConstruction();
        if (GameManager.Instance._IsRefreshNeeded)
        {
            GameManager.Instance._IsRefreshNeeded = false;
            GetCurrentBuildingDetails();
            DestroyAllBuildings();
            SpawningBuilding();
            AllocateBuildingCostList();
            CheckForAllBuildingMax();
        }
    }
    private void ManageCameraStatesBasedOnBuildingConstruction()
    {
        foreach (BuildingData building in _buildingData)
        {
            if (building._isUnderConstruction != false)
            {
                mCameraControllerRef._inBetweenConstructionProcess = true;
                return;
            }
            else
            {
                mCameraControllerRef._inBetweenConstructionProcess = false;
            }
        }
    }

    void GetCurrentBuildingDetails()
    {
        GameManager.Instance._shield = 0;
        for (int i = 0; i < GameManager.Instance._buildingGameManagerDataRef.Count; i++)
        {
            _buildingData[i]._buildingName = GameManager.Instance._buildingGameManagerDataRef[i]._buildingName;
            _buildingData[i]._buildingLevel = GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel;
            _buildingData[i].isBuildingDamaged = GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed;
            _buildingData[i].isBuildingShielded = GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingShielded;
            if (_buildingData[i].isBuildingShielded)
            {
                _shieldedBuildings.Add(i);
                GameManager.Instance._shield++;
            }
        }
        StartCoroutine(menuUI.UpDateShieldInUICoroutine(0,true));

    }

    public void PutCurrentLevelBuildingdetails()
    {
        GameManager.Instance._buildingGameManagerDataRef.Clear();
        for (int i = 0; i < _buildingData.Count; i++)
        {
            Building data = new Building();
            data._buildingName = _buildingData[i]._buildingName;          
            data._buildingCurrentLevel = _buildingData[i]._buildingLevel;
            data._isBuildingDestroyed = _buildingData[i].isBuildingDamaged;
            GameManager.Instance._buildingGameManagerDataRef.Add(data);
        }
        FirebaseManager.Instance.WriteAllDataToFireBase();
        //FirebaseManager.Instance.AddBuildsInLevelListner();
    }

    void DestroyAllBuildings()
    {
        foreach (GameObject building in _buildings)
        {
            Destroy(building);
        }
        _buildings.Clear();
    }

    void SpawningBuilding()
    {
        for (int i = 0; i < _buildingData.Count; i++)
        {
            if (_buildingData[i]._buildingLevel <= 0) /*&& !GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed && !GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingSpawned*/
            {
                if (!_buildingData[i].isBuildingDamaged)
                {
                    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[0], _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
                    GORef.name = _buildingData[i]._buildingName;
                    GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].NextUpgradeImages[4];
                    _buildingData[i].isBuildingSpawnedAndActive = true;
                    _buildings.Add(GORef);
           
                }
                else
                {
                    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[0], _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
                    GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].NextUpgradeImages[4];
                    GORef.transform.position = new Vector3(GORef.transform.position.x, _buildingSinkPositionAmount, GORef.transform.position.z);
                    GORef.transform.rotation = Quaternion.Euler(GORef.transform.eulerAngles.x, GORef.transform.eulerAngles.y, -(_buildingTiltRotationAmount)); //Adjusting the model problem #### IMPORTANT ####
                    Instantiate(_destroyedSmokeEffectVFX, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity, GORef.transform);
                    GameObject pluckCard = Instantiate(_destroyedPlayerPlunkCard, _buildingData[i]._buildingSpawnPoint.position + _DestroyedBuildingPlunkOffsetFromBuilding, Quaternion.identity, GORef.transform);
                    System.Action<Sprite> OnGettingAttackedPlayerImage = (image) =>
                    {
                        pluckCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = image;
                    };
                    CheckIsBuildingAttack(_buildingData[i]._buildingName + _buildingData[i]._buildingLevel, OnGettingAttackedPlayerImage);


                    GORef.name = _buildingData[i]._buildingName;
                    _buildings.Add(GORef);
                }
            }
            else /*if (!GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed && _buildingData[i]._buildingLevel > 0)*/  // But if there are buildings already spawned and active the grab the information from Game Manager
            {
                if (!_buildingData[i].isBuildingDamaged)
                {
                    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[_buildingData[i]._buildingLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
                    GORef.name = _buildingData[i]._buildingName;
                    if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
                    {
                        _buildingData[i].didBuildingReachMaxLevel = true;
                    }
                    _buildings.Add(GORef);

                }
                else
                {
                    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[_buildingData[i]._buildingLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
                    GORef.transform.position = new Vector3(GORef.transform.position.x, _buildingSinkPositionAmount, GORef.transform.position.z);
                    GORef.transform.rotation = Quaternion.Euler(GORef.transform.eulerAngles.x, GORef.transform.eulerAngles.y, _buildingTiltRotationAmount);
                    Instantiate(_destroyedSmokeEffectVFX, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity, GORef.transform);
                    GameObject pluckCard = Instantiate(_destroyedPlayerPlunkCard, _buildingData[i]._buildingSpawnPoint.position + _DestroyedBuildingPlunkOffsetFromBuilding, Quaternion.identity, GORef.transform);
                    System.Action<Sprite> OnGettingAttackedPlayerImage = (image) =>
                    {
                        pluckCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = image;
                    };
                    CheckIsBuildingAttack(_buildingData[i]._buildingName + _buildingData[i]._buildingLevel, OnGettingAttackedPlayerImage);
                    GORef.name = _buildingData[i]._buildingName;
                    if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
                    {
                        _buildingData[i].didBuildingReachMaxLevel = true;
                    }
                    _buildings.Add(GORef);

                }
            }
        }
    }


    public IEnumerator UpgradeOrRepairBuilding(string inBuildingName, int inBuildingsElementNumber, int inBuildingLevel, Transform inBuildingSpawnPoint,bool isRepair =false)
    {

        GameObject goRef = GameObject.Find(inBuildingName);
        _buildingData[inBuildingsElementNumber]._isUnderConstruction = true;
        _buildings.Remove(goRef);
        Destroy(goRef, 1.25f);
        GameObject smokeVFX;
        if (_buildingData[inBuildingsElementNumber]._buildingMaxLevel == inBuildingLevel)
           smokeVFX = Instantiate(mBuildingConstructionMaxVFX, inBuildingSpawnPoint.position + mParticleOffSetFromBuilding, Quaternion.identity);
        else
           smokeVFX = Instantiate(mBuildingConstructionVFX, inBuildingSpawnPoint.position + mParticleOffSetFromBuilding, Quaternion.identity);
        mCameraParentRef.transform.DOMove(inBuildingSpawnPoint.position + mCameraOffSetFromBuilding, mCameraFocusSpeed, false);//.OnComplete(()=> { mCameraParentRef.transform.parent = inPanPoint.transform; });
        goRef.transform.DOScale(mSizeToDecrease, mBuildingShrinkAndEnlargeTime);
       
        yield return new WaitForSeconds(mBuildingSpawnTimeDelay);

        if (_buildingData[inBuildingsElementNumber]._buildingLevel != 0)
        {
            GameObject newGoRef = Instantiate(_buildingData[inBuildingsElementNumber].UpgradeLevels[inBuildingLevel], _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.rotation);
            newGoRef.transform.localScale = newBuildingSpawnScale;
            newGoRef.name = _buildingData[inBuildingsElementNumber]._buildingName;
            GameManager.Instance.UpdateBuildingData(inBuildingName, inBuildingsElementNumber, inBuildingLevel, true, false);
            _buildings.Add(newGoRef);

            newGoRef.transform.DOScale(1, mBuildingShrinkAndEnlargeTime);
        }
        else
        {
            GameObject newGoRef = Instantiate(_buildingData[inBuildingsElementNumber].UpgradeLevels[inBuildingLevel], _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position, Quaternion.identity);
            newGoRef.transform.localScale = newBuildingSpawnScale;
            newGoRef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[inBuildingsElementNumber].NextUpgradeImages[4];
            newGoRef.name = _buildingData[inBuildingsElementNumber]._buildingName;
            GameManager.Instance.UpdateBuildingData(inBuildingName, inBuildingsElementNumber, inBuildingLevel, true, false);

            _buildings.Add(newGoRef);

            newGoRef.transform.DOScale(1, mBuildingShrinkAndEnlargeTime);
        }
        if (isRepair)
            FirebaseManager.Instance.WriteBuildingDataToFirebase();
        if (_buildingData[inBuildingsElementNumber]._buildingMaxLevel == inBuildingLevel)
            Destroy(smokeVFX,5f);
        else
            Destroy(smokeVFX,1.5f);
        yield return new WaitForSeconds(2f);
        _buildingData[inBuildingsElementNumber]._isUnderConstruction = false;
        
    }

    

    public void GrabElementNumberBasedOnButtonClick(int inBuildingsElementNumber)
    {
        if (_buildingData[inBuildingsElementNumber]._buildingLevel < _buildingData[inBuildingsElementNumber]._buildingMaxLevel)
        {
            if (!_buildingData[inBuildingsElementNumber].isBuildingDamaged)
            {
                //Upgrade the Building
                StartCoroutine(UpgradeOrRepairBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel + 1, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint));
                _buildingData[inBuildingsElementNumber]._buildingLevel += 1;
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Building...";
            }
            else
            {
                //Repair the Building
                StartCoroutine(UpgradeOrRepairBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint,true));
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Building...";
            }

           
        }
        else
        {
            //Repair when the Building is Max
            if (_buildingData[inBuildingsElementNumber].isBuildingDamaged)
            {
                StartCoroutine(UpgradeOrRepairBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint,true));
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Building...";
            }
            else
            {
                _buildingData[inBuildingsElementNumber].didBuildingReachMaxLevel = true;
                CheckForAllBuildingMax();
            }
            
        }
        if (_buildingData[inBuildingsElementNumber]._buildingLevel == _buildingData[inBuildingsElementNumber]._buildingMaxLevel)
        {
            _buildingData[inBuildingsElementNumber].didBuildingReachMaxLevel = true;
            CheckForAllBuildingMax();
        }
    }



    void CheckIsBuildingAttack(string buildName,System.Action<Sprite> image)
    {
        foreach (AttackedPlayerInformation data in FirebaseManager.Instance.AttackedData)
        {
            if (data._attackedBuildingName == buildName)
            {
                FacebookManager.Instance.GetProfilePictureWithId(data._attackedPlayerID,image);
            }
        }
    }


    void CheckForAllBuildingMax()
    {
        for (int i = 0; i < _buildingData.Count; i++)
        {
            if (!_buildingData[i].didBuildingReachMaxLevel)
                return;
        }
        StartCoroutine(LevelCompletedCoroutine());
    }

    IEnumerator LevelCompletedCoroutine()
    {
        GameManager.Instance._PauseGame = true;
        yield return new WaitForSeconds(3.6f);
        StartCoroutine(menuUI.ShowLevelCompletedParticlesCoroutine());
    }

}