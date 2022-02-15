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

            //Debug.Log(mGameManager._buildingGameManagerDataRef[i]._isBuildingSpawned);
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
        FirebaseManager.Instance.AddBuildsInLevelListner();
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
            // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards

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
                    //
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
        //mCameraControllerRef._inBetweenConstructionProcess = true;

        GameObject goRef = GameObject.Find(inBuildingName);
        _buildingData[inBuildingsElementNumber]._isUnderConstruction = true;
        _buildings.Remove(goRef);
        //Upgrading Scenario Starts Here
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
        //Check for Maxing
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


















//if (_buildingData[i]._buildingLevel <= 0 && !GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed && !GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingSpawned)
//{
//    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[0], _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
//    GORef.name = _buildingData[i]._buildingName;
//    GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].NextUpgradeImages[4];
//    _buildingData[i].isBuildingSpawnedAndActive = true;

//    _buildings.Add(GORef);
//}
//else if(_buildingData[i]._buildingLevel <= 0 && GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed && GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingSpawned)
//{
//    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[0], _buildingData[i]._buildingSpawnPoint.position,Quaternion.identity);
//    GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].NextUpgradeImages[4];
//    GORef.transform.position = new Vector3(GORef.transform.position.x, _buildingSinkPositionAmount, GORef.transform.position.z);
//    GORef.transform.rotation = Quaternion.Euler(GORef.transform.eulerAngles.x, GORef.transform.eulerAngles.y, _buildingTiltRotationAmount);
//    Instantiate(_destroyedSmokeEffectVFX, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity, GORef.transform);
//    GORef.name = _buildingData[i]._buildingName;

//    _buildings.Add(GORef);
//}
//else if (!GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed && _buildingData[i]._buildingLevel > 0)  // But if there are buildings already spawned and active the grab the information from Game Manager
//{
//    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//    GORef.name = _buildingData[i]._buildingName;
//    if (GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingShielded)
//    {
//        _buildingData[i].isBuildingShielded = true;
//    }
//    if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//    {
//        _buildingData[i].didBuildingReachMaxLevel = true;
//    }
//    //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//    //GORef.name = _buildingData[i]._buildingName;
//}
//else
//{
//    GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//    GORef.transform.position = new Vector3(GORef.transform.position.x, _buildingSinkPositionAmount, GORef.transform.position.z);
//    GORef.transform.rotation = Quaternion.Euler(GORef.transform.eulerAngles.x, GORef.transform.eulerAngles.y, _buildingTiltRotationAmount);
//    Instantiate(_destroyedSmokeEffectVFX, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity, GORef.transform);
//    GORef.name = _buildingData[i]._buildingName;
//    //if (_buildingData[i].isBuildingShielded)
//    //{
//    //    Debug.Log("Im Shielded");
//    //}
//    if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//    {
//        _buildingData[i].didBuildingReachMaxLevel = true;
//    }
//}






//public void RepairBuilding(int inBuildingsElementNumber , int inBuildingLevel)
//{
//    //mCameraControllerRef._inBetweenConstructionProcess = true;
//    //var DestroyedBuildingRef = GameObject.Find(_buildingData[inBuildingsElementNumber]._buildingName);
//    //Destroy(DestroyedBuildingRef,1.25f);
//    //GameObject smokeVFX = Instantiate(mBuildingConstructionVFX, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position + mParticleOffSetFromBuilding, Quaternion.identity);

//    //mCameraParentRef.transform.DOMove(_buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position + mCameraOffSetFromBuilding, mCameraFocusSpeed, false);//.OnComplete(()=> { mCameraParentRef.transform.parent = inPanPoint.transform; });
//    //DestroyedBuildingRef.transform.DOScale(mSizeToDecrease, mBuildingShrinkAndEnlargeTime);

//    //GameObject newGoRef = Instantiate(_buildingData[inBuildingsElementNumber].UpgradeLevels[inBuildingLevel - 1], _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.rotation);
//    //newGoRef.name = _buildingData[inBuildingsElementNumber]._buildingName;
//}

//void UpgradingBuilding()
//{
//    /*.OnUpdate(() => { inPanPoint.DORotate(new Vector3(inPanPoint.transform.rotation.eulerAngles.x, inPanPoint.transform.rotation.eulerAngles.y + (-15f), inPanPoint.transform.rotation.eulerAngles.z), 1, RotateMode.Fast); }).OnComplete(() => { inPanPoint.transform.eulerAngles = new Vector3(inPanPoint.transform.localEulerAngles.x, 0f, inPanPoint.transform.localEulerAngles.z); mCameraParentRef.transform.parent = null; });*/
//    //inPanPoint.DORotate(new Vector3(inPanPoint.transform.rotation.eulerAngles.x, inPanPoint.transform.rotation.eulerAngles.y + (-30f), inPanPoint.transform.rotation.eulerAngles.z), 1, RotateMode.Fast).OnComplete(() => { inPanPoint.transform.eulerAngles = new Vector3(inPanPoint.transform.localEulerAngles.x, 0f,inPanPoint.transform.localEulerAngles.z); mCameraParentRef.transform.parent = null; });

//    //_buildings[inBuildingsElementNumber] = newGoRef;

//    //newGoRef.AddComponent<BuildingDetails>();
//    //BuildingDetails buildingDetailRef = newGoRef.GetComponent<BuildingDetails>();
//    //buildingDetailRef._buildingLevel = inBuildingLevel;
//    //buildingDetailRef.isUnderConstruction = true;
//    //yield return new WaitForSeconds(mTimeDelayFromNewBuildingToCameraDefaultState);

//    //_isAnotherBuildingInConstruction = false;



//    //_buildingData[inBuildingNumber].currentLevelGameObject = inCurrentLevelsMesh;
//    //Just in case if these data's are required
//    //buildingDetailRef._buildMeshBasedOnCurrentLevel = inCurrentLevelsMesh;

//    //Saves the data from building manager to game manager


//    //if (mCoroutineIsInProcess)
//    //{
//    //    DOTween.CompleteAll();
//    //    StopCoroutine(BuildingCoroutine);
//    //    //StopAllCoroutines();
//    //    mCoroutineIsInProcess = false;
//    //    //t.Stop();
//    //}

//    //StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

//    //t = new Task(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

//    //BuildingCoroutine = StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));
//}







//Future Scenario
//for (int i = 0; i < _buildingData.Count; i++)
//{
//    // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards
//    if (_buildingData[i]._buildingLevel <= 0)
//    {
//        //Instantitate the basic building (Plunk Card)
//        GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
//        GORef.name = _buildingData[i]._buildingName;
//        _buildingData[i].isBuildingSpawnedAndActive = true;

//        _buildingsList.Add(GORef);
//    }
//    else if (_buildingData[i].isBuildingDamaged)  // But if there are buildings already spawned and active the grab the information from Game Manager
//    {
//        //If the building is damaged spawn the damaged building
//        GameObject GORef = Instantiate(_buildingData[i].destroyedVersions[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        GORef.name = _buildingData[i]._buildingName;
//        //if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//        //{
//        //    _buildingData[i].didBuildingReachMaxLevel = true;
//        //}
//        //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        //GORef.name = _buildingData[i]._buildingName;
//    }
//    else if (_buildingData[i].isBuildingShielded)
//    {
//        //spawn the shielded building
//        GameObject GORef = Instantiate(_buildingData[i].destroyedVersions[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        GORef.name = _buildingData[i]._buildingName;
//    }
//    else
//    {
//        GameObject GORef = Instantiate(_buildingData[i].destroyedVersions[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        GORef.name = _buildingData[i]._buildingName;
//    }
//}

#region Manual Button Functions
///// <summary>
///// Function for Button-1
///// </summary>
//public void Building1Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(0);
//}

///// <summary>
///// Function for Button-2
///// </summary>
//public void Building2Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(1);
//}

///// <summary>
///// Function for Button-3
///// </summary>
//public void Building3Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(2);
//}

///// <summary>
///// Function for Button-4
///// </summary>
//public void Building4Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(3);
//}

///// <summary>
///// Function for Button-5
///// </summary>
//public void Building5Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(4);
//}

///// <summary>
///// Function for Button-6
///// </summary>
//public void Building6Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(5);
//}

///// <summary>
///// Function for Button-7
///// </summary>
//public void Building7Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(6);
//}

///// <summary>
///// Function for Button-8
///// </summary>
//public void Building8Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(7);
//}

#endregion




//if (inLevel != 0)
//{
//    GameObject oldMesh = inCurrentLevelsMesh;
//    Destroy(oldMesh);   
//}

/*_buildingData[inElementNumber]._buildingLevel < _buildingData[inElementNumber].UpgradeLevels.Length &&*/

//void residue()
//{
//    public BuildingDetails[] _building;
//public List<GameObject> _buildings;

//int i = 0;
//int j = 0;
//int k = 0;
//// Start is called before the first frame update
//void Start()
//{

//    for (int i = 0; i < _building.Length; i++)
//    {
//        GameObject GO = new GameObject();
//        GO.AddComponent<MeshFilter>().mesh = _building[i]._initialBuildingMesh;
//        GO.AddComponent<MeshRenderer>().material = _building[i]._material;
//        GO.AddComponent<TestScript>();
//        GO.transform.position = _building[i].transformPoint.position;
//        GO.name = _building[i]._name;
//        _buildings.Add(GO);
//    }
//}

//// Update is called once per frame
//void Update()
//{

//}

//public void UpgradeBuilding(string name, int inBuildingNumber, int inLevel)
//{
//    GameObject goRef = GameObject.Find(name);
//    goRef.GetComponent<MeshFilter>().mesh = _building[inBuildingNumber].UpgradeLevels[inLevel];
//    //Access the building script & keepChaning inLevel Number to Level
//    goRef.GetComponent<TestScript>().BuildingLevel = inLevel;
//}

//public void CubeUpgrade()
//{
//    UpgradeBuilding("Cube", 0, i);
//    i++;
//}

//public void CylinderUpgrade()
//{
//    UpgradeBuilding("Cylinder", 1, j);
//    j++;
//}

//public void CircleUpgrade()
//{
//    UpgradeBuilding("Circle", 2, k);
//    k++;
//}









#region Old Building Manager
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening;
//using TMPro;

////Trying this
//[System.Serializable]
//public class BuildingData
//{
//    [Header("Building Name And Level: ")]
//    public string _buildingName;
//    public string _buildingDisplayName;
//    public int _buildingLevel = 0;
//    public int _buildingMaxLevel;
//    public Transform _buildingSpawnPoint;
//    public Transform _cameraFocusPoint;

//    [Header("Building's GameObject: ")]
//    public GameObject _initialBuildingGameObject;
//    public GameObject currentLevelGameObject;
//    public Sprite[] NextUpgradeImages; //Future
//    [Space]
//    public GameObject[] UpgradeLevels;
//    public int[] UpgradeCosts;
//    [Space]
//    public GameObject[] destroyedVersions; //Just in Case for Future
//    public int[] _repairCosts;

//    public GameObject _respectiveBuildingButtons;

//    [Header("State Checkers: ")]
//    public bool isBuildingSpawnedAndActive; //Just in case for Attack
//    public bool isBuildingDamaged; //Just in case to check if building is damaged or not.
//    public bool isBuildingShielded;
//    public bool didBuildingReachMaxLevel;
//    public bool _isUnderConstruction;
//}

//public class BuildingManager : MonoBehaviour
//{
//    public List<BuildingData> _buildingData;
//    public List<GameObject> _buildings;

//    private GameManager mGameManager;

//    [SerializeField] private GameObject mCameraParentRef;
//    CameraController mCameraControllerRef;

//    [SerializeField] private GameObject mBuildingConstructionVFX;
//    [Tooltip("Speed the building should be focused when the upgrade of that button is being clicked")]
//    [SerializeField] private float mCameraFocusSpeed;
//    [Tooltip("Delay time to spawn the next upgrade")]
//    [SerializeField] private float mBuildingSpawnTimeDelay;
//    [Tooltip("Lesser the value the faster it happens / Higher the value the slower it happens")]
//    [SerializeField] private float mBuildingShrinkAndEnlargeTime;

//    [SerializeField] private Vector3 mCameraOffSetFromBuilding;
//    [SerializeField] private Vector3 mParticleOffSetFromBuilding;

//    [SerializeField] private Vector3 newBuildingSpawnScale;
//    [SerializeField] private float mSizeToDecrease;

//    public float mTimeDelayFromNewBuildingToCameraDefaultState;

//    public bool _isAnotherBuildingInConstruction = false;

//    public Coroutine BuildingCoroutine;
//    [SerializeField] private bool mCoroutineIsInProcess;

//    public bool tempCheck;

//    public List<int> _shieldedBuildings;

//    public float _buildingSinkPositionAmount;
//    public float _buildingTiltRotationAmount;
//    public GameObject _destroyedSmokeEffectVFX;

//    private void Awake()
//    {
//        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
//    }

//    private void Start()
//    {
//        //tempCheck = mCameraControllerRef._inBetweenConstructionProcess;
//        mGameManager._maxShield = _buildingData.Count;
//        mCameraParentRef = GameObject.Find("CameraParent");
//        mCameraControllerRef = mCameraParentRef.GetComponentInChildren<CameraController>();

//        if (!GameManager.Instance._IsBuildingFromFBase)
//        {
//            PutCurrentLevelBuildingdetails();
//            SpawningBuilding();
//        }

//        #region "Test Script When Not Grabbing details from FireBase"
//        //    for (int i = 0; i < _buildingData.Count; i++)
//        //    {
//        //        // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards

//        ////        if (_buildingData[i]._buildingLevel <= 0)
//        ////        {
//        ////            GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
//        ////            GORef.name = _buildingData[i]._buildingName;
//        ////            GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].NextUpgradeImages[4];
//        ////            _buildingData[i].isBuildingSpawnedAndActive = true;

//        ////            _buildings.Add(GORef);
//        ////        }
//        ////        else if (!_buildingData[i].isBuildingDamaged)  // But if there are buildings already spawned and active the grab the information from Game Manager
//        ////        {
//        ////            GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[_buildingData[i]._buildingLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        ////            GORef.name = _buildingData[i]._buildingName;
//        ////            if (_buildingData[i].isBuildingShielded) // Get from GameManager
//        ////            {
//        ////                _buildingData[i].isBuildingShielded = true;
//        ////            }
//        ////            if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//        ////            {
//        ////                _buildingData[i].didBuildingReachMaxLevel = true;
//        ////            }
//        ////            //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        ////            //GORef.name = _buildingData[i]._buildingName;
//        ////        }
//        ////        else
//        ////        {
//        ////            GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[_buildingData[i]._buildingLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        ////            GORef.transform.position = new Vector3(GORef.transform.position.x, _buildingSinkPositionAmount, GORef.transform.position.z);
//        ////            GORef.transform.rotation = Quaternion.Euler(GORef.transform.eulerAngles.x, GORef.transform.eulerAngles.y, _buildingTiltRotationAmount);
//        ////            GORef.name = _buildingData[i]._buildingName;
//        ////            //if (_buildingData[i].isBuildingShielded)
//        ////            //{
//        ////            //    Debug.Log("Im Shielded");
//        ////            //}
//        ////            if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//        ////            {
//        ////                _buildingData[i].didBuildingReachMaxLevel = true;
//        ////            }
//        ////        }
//        ////    }
//        ////
//        #endregion 
//    }

//    void Update()
//    {
//        //foreach (var v in _buildingData)
//        //{
//        //    if (v._isUnderConstruction != false)
//        //    {
//        //        mCameraControllerRef._inBetweenConstructionProcess = true;
//        //        return;
//        //    }
//        //    else
//        //    {
//        //        mCameraControllerRef._inBetweenConstructionProcess = false;
//        //        //Invoke(nameof(InvokeCamera), 1f);
//        //    }
//        //}
//        if (GameManager.Instance._IsRefreshNeeded)
//        {
//            GameManager.Instance._IsRefreshNeeded = false;

//            GetCurrentBuildingDetails();
//            SpawningBuilding();
//        }
//    }

//    void GetCurrentBuildingDetails()
//    {
//        for (int i = 0; i < GameManager.Instance._buildingGameManagerDataRef.Count; i++)
//        {
//            _buildingData[i]._buildingName = GameManager.Instance._buildingGameManagerDataRef[i]._buildingName;
//            _buildingData[i]._buildingLevel = GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel;
//            _buildingData[i].isBuildingSpawnedAndActive = GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingSpawned;
//            _buildingData[i].isBuildingDamaged = GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed;
//            _buildingData[i].isBuildingShielded = GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingShielded;
//            if (_buildingData[i].isBuildingShielded)
//                _shieldedBuildings.Add(i);

//            //Debug.Log(mGameManager._buildingGameManagerDataRef[i]._isBuildingSpawned);
//        }
//    }

//    public void PutCurrentLevelBuildingdetails()
//    {
//        GameManager.Instance._buildingGameManagerDataRef.Clear();
//        //GameManager.Instance._buildingGameManagerDataRef = new List<GameManagerBuildingData>(_buildingData.Count);
//        for (int i = 0; i < _buildingData.Count; i++)
//        {
//            GameManagerBuildingData data = new GameManagerBuildingData();
//            data._buildingName = _buildingData[i]._buildingName;          
//            data._buildingNo = i;
//            data._buildingCurrentLevel = _buildingData[i]._buildingLevel;
//            data._isBuildingSpawned = _buildingData[i].isBuildingSpawnedAndActive;
//            data._isBuildingDestroyed = _buildingData[i].isBuildingDamaged;
//            GameManager.Instance._buildingGameManagerDataRef.Add(data);
//            //GameManager.Instance.UpdateBuildingData(_buildingData[i]._buildingName, i, _buildingData[i]._buildingLevel, _buildingData[i].isBuildingSpawnedAndActive,_buildingData[i].isBuildingDamaged);
//        }
//    }



//    void SpawningBuilding()
//    {
//        for (int i = 0; i < _buildingData.Count; i++)
//        { 
//            // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards

//            if (_buildingData[i]._buildingLevel <= 0)
//            {
//                GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
//                GORef.name = _buildingData[i]._buildingName;
//                GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].NextUpgradeImages[4];
//                _buildingData[i].isBuildingSpawnedAndActive = true;

//                _buildings.Add(GORef);
//            }
//            else if (!GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingDestroyed)  // But if there are buildings already spawned and active the grab the information from Game Manager
//            {
//                GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//                GORef.name = _buildingData[i]._buildingName;
//                if (GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingShielded)
//                {
//                    _buildingData[i].isBuildingShielded = true;
//                }
//                if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//                {
//                    _buildingData[i].didBuildingReachMaxLevel = true;
//                }
//                //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//                //GORef.name = _buildingData[i]._buildingName;
//            }
//            else
//            {
//                GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//                GORef.transform.position = new Vector3(GORef.transform.position.x, _buildingSinkPositionAmount, GORef.transform.position.z);
//                GORef.transform.rotation = Quaternion.Euler(GORef.transform.eulerAngles.x, GORef.transform.eulerAngles.y, _buildingTiltRotationAmount);
//                Instantiate(_destroyedSmokeEffectVFX, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity, GORef.transform);
//                GORef.name = _buildingData[i]._buildingName;
//                //if (_buildingData[i].isBuildingShielded)
//                //{
//                //    Debug.Log("Im Shielded");
//                //}
//                if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//                {
//                    _buildingData[i].didBuildingReachMaxLevel = true;
//                }
//            }
//        }


//    }

//    /// <summary>
//    /// Order of progression
//    /// 1. Particle & Camera Zoom-In.
//    /// 2. 
//    /// </summary>
//    /// <param name="name"></param>
//    /// <param name="inBuildingsElementNumber"></param>
//    /// <param name="inBuildingLevel"></param>
//    /// <param name="inCurrentLevelsMesh"></param>
//    public IEnumerator UpgradeOrRepairBuilding(string inBuildingName, int inBuildingsElementNumber, int inBuildingLevel, Transform inBuildingSpawnPoint)
//    {
//        mCameraControllerRef._inBetweenConstructionProcess = true;

//        GameObject goRef = GameObject.Find(inBuildingName);
//        _buildingData[inBuildingsElementNumber]._isUnderConstruction = true;
//        //Upgrading Scenario Starts Here
//        Destroy(goRef, 1.25f);
//        GameObject smokeVFX = Instantiate(mBuildingConstructionVFX, inBuildingSpawnPoint.position + mParticleOffSetFromBuilding, Quaternion.identity);
//        mCameraParentRef.transform.DOMove(inBuildingSpawnPoint.position + mCameraOffSetFromBuilding, mCameraFocusSpeed, false);//.OnComplete(()=> { mCameraParentRef.transform.parent = inPanPoint.transform; });
//        goRef.transform.DOScale(mSizeToDecrease, mBuildingShrinkAndEnlargeTime);

//        yield return new WaitForSeconds(mBuildingSpawnTimeDelay);

//        GameObject newGoRef = Instantiate(_buildingData[inBuildingsElementNumber].UpgradeLevels[inBuildingLevel], _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.rotation);
//        newGoRef.transform.localScale = newBuildingSpawnScale;
//        newGoRef.name = _buildingData[inBuildingsElementNumber]._buildingName;


//        GameManager.Instance.UpdateBuildingData(inBuildingName, inBuildingsElementNumber, inBuildingLevel + 1, true, false);
//        //newGoRef.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), .5f, 5, 0);

//        newGoRef.transform.DOScale(1, mBuildingShrinkAndEnlargeTime).OnComplete(() =>
//        {

//        });//.WaitForCompletion();

//        yield return new WaitForSeconds(2f);
//        Destroy(smokeVFX);
//        mCameraControllerRef._inBetweenConstructionProcess = false;
//        mCoroutineIsInProcess = false;
//        _buildingData[inBuildingsElementNumber]._isUnderConstruction = false;
//    }



//    public void GrabElementNumberBasedOnButtonClick(int inBuildingsElementNumber)
//    {
//        if (_buildingData[inBuildingsElementNumber]._buildingLevel < _buildingData[inBuildingsElementNumber]._buildingMaxLevel)
//        {
//            if (!_buildingData[inBuildingsElementNumber].isBuildingDamaged)
//            {
//                BuildingCoroutine = StartCoroutine(UpgradeOrRepairBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint));
//                _buildingData[inBuildingsElementNumber]._buildingLevel += 1;
//                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;
//                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
//                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Building...";
//            }
//            else
//            {
//                BuildingCoroutine = StartCoroutine(UpgradeOrRepairBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel - 1, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint));
//                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;
//                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
//                _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Building...";
//            }


//        }
//        if (_buildingData[inBuildingsElementNumber]._buildingLevel == _buildingData[inBuildingsElementNumber]._buildingMaxLevel)
//        {
//            //_buildingData[inElementNumber]._respectiveBuildingButtons.GetComponent<Button>().interactable = false;
//            _buildingData[inBuildingsElementNumber].didBuildingReachMaxLevel = true;
//            CheckForAllBuildingMax();
//        }
//    }

//    void CheckForAllBuildingMax()
//    {
//        //Check for Maxing
//        for (int i = 0; i < _buildingData.Count; i++)
//        {
//            if (_buildingData[i].didBuildingReachMaxLevel != true)
//                return;
//        }
//        LevelCompleted();
//    }

//    void LevelCompleted()
//    {
//        //GameManager.Instance._playerCurrentLevel++;
//        //GameManager.Instance._IsBuildingFromFBase = false;
//        GameManager.Instance.CurrentLevelCompleted();
//    }

//}
#endregion

//public void RepairBuilding(int inBuildingsElementNumber , int inBuildingLevel)
//{
//    //mCameraControllerRef._inBetweenConstructionProcess = true;
//    //var DestroyedBuildingRef = GameObject.Find(_buildingData[inBuildingsElementNumber]._buildingName);
//    //Destroy(DestroyedBuildingRef,1.25f);
//    //GameObject smokeVFX = Instantiate(mBuildingConstructionVFX, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position + mParticleOffSetFromBuilding, Quaternion.identity);

//    //mCameraParentRef.transform.DOMove(_buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position + mCameraOffSetFromBuilding, mCameraFocusSpeed, false);//.OnComplete(()=> { mCameraParentRef.transform.parent = inPanPoint.transform; });
//    //DestroyedBuildingRef.transform.DOScale(mSizeToDecrease, mBuildingShrinkAndEnlargeTime);

//    //GameObject newGoRef = Instantiate(_buildingData[inBuildingsElementNumber].UpgradeLevels[inBuildingLevel - 1], _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.rotation);
//    //newGoRef.name = _buildingData[inBuildingsElementNumber]._buildingName;
//}

//void UpgradingBuilding()
//{
//    /*.OnUpdate(() => { inPanPoint.DORotate(new Vector3(inPanPoint.transform.rotation.eulerAngles.x, inPanPoint.transform.rotation.eulerAngles.y + (-15f), inPanPoint.transform.rotation.eulerAngles.z), 1, RotateMode.Fast); }).OnComplete(() => { inPanPoint.transform.eulerAngles = new Vector3(inPanPoint.transform.localEulerAngles.x, 0f, inPanPoint.transform.localEulerAngles.z); mCameraParentRef.transform.parent = null; });*/
//    //inPanPoint.DORotate(new Vector3(inPanPoint.transform.rotation.eulerAngles.x, inPanPoint.transform.rotation.eulerAngles.y + (-30f), inPanPoint.transform.rotation.eulerAngles.z), 1, RotateMode.Fast).OnComplete(() => { inPanPoint.transform.eulerAngles = new Vector3(inPanPoint.transform.localEulerAngles.x, 0f,inPanPoint.transform.localEulerAngles.z); mCameraParentRef.transform.parent = null; });

//    //_buildings[inBuildingsElementNumber] = newGoRef;

//    //newGoRef.AddComponent<BuildingDetails>();
//    //BuildingDetails buildingDetailRef = newGoRef.GetComponent<BuildingDetails>();
//    //buildingDetailRef._buildingLevel = inBuildingLevel;
//    //buildingDetailRef.isUnderConstruction = true;
//    //yield return new WaitForSeconds(mTimeDelayFromNewBuildingToCameraDefaultState);

//    //_isAnotherBuildingInConstruction = false;



//    //_buildingData[inBuildingNumber].currentLevelGameObject = inCurrentLevelsMesh;
//    //Just in case if these data's are required
//    //buildingDetailRef._buildMeshBasedOnCurrentLevel = inCurrentLevelsMesh;

//    //Saves the data from building manager to game manager


//    //if (mCoroutineIsInProcess)
//    //{
//    //    DOTween.CompleteAll();
//    //    StopCoroutine(BuildingCoroutine);
//    //    //StopAllCoroutines();
//    //    mCoroutineIsInProcess = false;
//    //    //t.Stop();
//    //}

//    //StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

//    //t = new Task(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

//    //BuildingCoroutine = StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));
//}







//Future Scenario
//for (int i = 0; i < _buildingData.Count; i++)
//{
//    // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards
//    if (_buildingData[i]._buildingLevel <= 0)
//    {
//        //Instantitate the basic building (Plunk Card)
//        GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
//        GORef.name = _buildingData[i]._buildingName;
//        _buildingData[i].isBuildingSpawnedAndActive = true;

//        _buildingsList.Add(GORef);
//    }
//    else if (_buildingData[i].isBuildingDamaged)  // But if there are buildings already spawned and active the grab the information from Game Manager
//    {
//        //If the building is damaged spawn the damaged building
//        GameObject GORef = Instantiate(_buildingData[i].destroyedVersions[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        GORef.name = _buildingData[i]._buildingName;
//        //if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
//        //{
//        //    _buildingData[i].didBuildingReachMaxLevel = true;
//        //}
//        //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        //GORef.name = _buildingData[i]._buildingName;
//    }
//    else if (_buildingData[i].isBuildingShielded)
//    {
//        //spawn the shielded building
//        GameObject GORef = Instantiate(_buildingData[i].destroyedVersions[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        GORef.name = _buildingData[i]._buildingName;
//    }
//    else
//    {
//        GameObject GORef = Instantiate(_buildingData[i].destroyedVersions[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
//        GORef.name = _buildingData[i]._buildingName;
//    }
//}

#region Manual Button Functions
///// <summary>
///// Function for Button-1
///// </summary>
//public void Building1Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(0);
//}

///// <summary>
///// Function for Button-2
///// </summary>
//public void Building2Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(1);
//}

///// <summary>
///// Function for Button-3
///// </summary>
//public void Building3Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(2);
//}

///// <summary>
///// Function for Button-4
///// </summary>
//public void Building4Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(3);
//}

///// <summary>
///// Function for Button-5
///// </summary>
//public void Building5Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(4);
//}

///// <summary>
///// Function for Button-6
///// </summary>
//public void Building6Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(5);
//}

///// <summary>
///// Function for Button-7
///// </summary>
//public void Building7Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(6);
//}

///// <summary>
///// Function for Button-8
///// </summary>
//public void Building8Upgrade()
//{
//    GrabElementNumberBasedOnButtonClick(7);
//}

#endregion




//if (inLevel != 0)
//{
//    GameObject oldMesh = inCurrentLevelsMesh;
//    Destroy(oldMesh);   
//}

/*_buildingData[inElementNumber]._buildingLevel < _buildingData[inElementNumber].UpgradeLevels.Length &&*/

//void residue()
//{
//    public BuildingDetails[] _building;
//public List<GameObject> _buildings;

//int i = 0;
//int j = 0;
//int k = 0;
//// Start is called before the first frame update
//void Start()
//{

//    for (int i = 0; i < _building.Length; i++)
//    {
//        GameObject GO = new GameObject();
//        GO.AddComponent<MeshFilter>().mesh = _building[i]._initialBuildingMesh;
//        GO.AddComponent<MeshRenderer>().material = _building[i]._material;
//        GO.AddComponent<TestScript>();
//        GO.transform.position = _building[i].transformPoint.position;
//        GO.name = _building[i]._name;
//        _buildings.Add(GO);
//    }
//}

//// Update is called once per frame
//void Update()
//{

//}

//public void UpgradeBuilding(string name, int inBuildingNumber, int inLevel)
//{
//    GameObject goRef = GameObject.Find(name);
//    goRef.GetComponent<MeshFilter>().mesh = _building[inBuildingNumber].UpgradeLevels[inLevel];
//    //Access the building script & keepChaning inLevel Number to Level
//    goRef.GetComponent<TestScript>().BuildingLevel = inLevel;
//}

//public void CubeUpgrade()
//{
//    UpgradeBuilding("Cube", 0, i);
//    i++;
//}

//public void CylinderUpgrade()
//{
//    UpgradeBuilding("Cylinder", 1, j);
//    j++;
//}

//public void CircleUpgrade()
//{
//    UpgradeBuilding("Circle", 2, k);
//    k++;
//}
