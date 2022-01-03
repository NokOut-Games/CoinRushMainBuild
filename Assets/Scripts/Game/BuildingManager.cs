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
    [Header("Building Name And Level: ")]
    public string _buildingName;
    public int _buildingLevel = 0;
    public int _buildingMaxLevel;
    public Transform _buildingSpawnPoint;
    public Transform _cameraFocusPoint;

    [Header("Building's GameObject: ")]
    public GameObject _initialBuildingGameObject;
    public Sprite PlunkCardImage;
    public GameObject currentLevelGameObject;
    public Sprite[] NextUpgradeImages; //Future
    [Space]
    public GameObject[] UpgradeLevels;
    public int[] UpgradeCosts;
    [Space]
    public GameObject[] destroyedVersions; //Just in Case for Future

    public GameObject _respectiveBuildingButtons;

    [Header("State Checkers: ")]
    public bool isBuildingSpawnedAndActive; //Just in case for Attack
    public bool isBuildingDamaged; //Just in case to check if building is damaged or not.
    public bool isBuildingShielded;
    public bool didBuildingReachMaxLevel;
}

public class BuildingManager : MonoBehaviour
{
    public List<BuildingData> _buildingData;

    private GameManager mGameManager;

    [SerializeField] private GameObject mCameraParentRef;
    CameraController mCameraControllerRef;

    [SerializeField] private GameObject mBuildingConstructionVFX;
    [Tooltip("Speed the building should be focused when the upgrade of that button is being clicked")]
    [SerializeField] private float mCameraFocusSpeed;
    [Tooltip("Delay time to spawn the next upgrade")]
    [SerializeField] private float mBuildingSpawnTimeDelay;
    [Tooltip("Lesser the value the faster it happens / Higher the value the slower it happens")]
    [SerializeField] private float mBuildingShrinkAndEnlargeTime;

    [SerializeField] private Vector3 mCameraOffSetFromBuilding;
    [SerializeField] private Vector3 mParticleOffSetFromBuilding;

    public float mTimeDelayFromNewBuildingToCameraDefaultState;

    public bool _isAnotherBuildingInConstruction = false;

    public Coroutine BuildingCoroutine;
    [SerializeField] private bool mCoroutineIsInProcess;


    Task t;
    private void Awake()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        mCameraParentRef = GameObject.Find("CameraParent");
        mCameraControllerRef = mCameraParentRef.GetComponentInChildren<CameraController>();

        //if (!GameManager.Instance._IsBuildingFromFBase)
        //{
        //    PutCurrentLevelBuildingdetails();
        //    SpawningBuilding();
        //}

        for (int i = 0; i < _buildingData.Count; i++)
        {
            // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards

            if (_buildingData[i]._buildingLevel == 0)
            {
                GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
                GORef.GetComponentInChildren<SpriteRenderer>().sprite = _buildingData[i].PlunkCardImage;
                GORef.name = _buildingData[i]._buildingName;
                _buildingData[i].isBuildingSpawnedAndActive = true;
            }
            else  // But if there are buildings already spawned and active the grab the information from Game Manager
            {
                GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
                GORef.name = _buildingData[i]._buildingName;
                if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
                {
                    _buildingData[i].didBuildingReachMaxLevel = true;
                }
                //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
                //GORef.name = _buildingData[i]._buildingName;
            }
        }
    }

    void GetCurrentBuildingDetails()
    {
        for (int i = 0; i < GameManager.Instance._buildingGameManagerDataRef.Count; i++)
        {
            _buildingData[i]._buildingName = GameManager.Instance._buildingGameManagerDataRef[i]._buildingName;
            _buildingData[i]._buildingLevel = GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel;
            _buildingData[i].isBuildingSpawnedAndActive = GameManager.Instance._buildingGameManagerDataRef[i]._isBuildingSpawned;
            //Debug.Log(mGameManager._buildingGameManagerDataRef[i]._isBuildingSpawned);
        }
    }

    public void PutCurrentLevelBuildingdetails()
    {
        for (int i = 0; i < _buildingData.Count; i++)
        {
            GameManager.Instance.UpdateBuildingData(_buildingData[i]._buildingName, i, _buildingData[i]._buildingLevel, _buildingData[i].isBuildingSpawnedAndActive,_buildingData[i].isBuildingDamaged,_buildingData[i].isBuildingShielded);
        }
    }

   

    void SpawningBuilding()
    {
        //for (int i = 0; i < _buildingData.Count; i++)
        //{
        //    // Check what building are already spawned and are active currently if no buildings are spawned then spawn the plunk cards

        //    if (_buildingData[i]._buildingLevel == 0)
        //    {
        //        GameObject GORef = Instantiate(_buildingData[i]._initialBuildingGameObject, _buildingData[i]._buildingSpawnPoint.position, Quaternion.identity);
        //        GORef.name = _buildingData[i]._buildingName;
        //        _buildingData[i].isBuildingSpawnedAndActive = true;

        //        _buildingsList.Add(GORef);
        //    }
        //    else  // But if there are buildings already spawned and active the grab the information from Game Manager
        //    {
        //        GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[GameManager.Instance._buildingGameManagerDataRef[i]._buildingCurrentLevel - 1], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
        //        GORef.name = _buildingData[i]._buildingName;
        //        if (_buildingData[i]._buildingLevel >= _buildingData[i]._buildingMaxLevel)
        //        {
        //            _buildingData[i].didBuildingReachMaxLevel = true;
        //        }
        //        //GameObject GORef = Instantiate(_buildingData[i].UpgradeLevels[mGameManager._buildingGameManagerDataRef[i]._buildingCurrentLevel], _buildingData[i]._buildingSpawnPoint.position, _buildingData[i]._buildingSpawnPoint.rotation);
        //        //GORef.name = _buildingData[i]._buildingName;
        //    }
        //}


    }

    void Update()
    {
        //if (mCoroutineIsInProcess)
        //{
        //    t.Finished += delegate (bool manual)
        //    {
        //        if (manual)
        //            Debug.Log("t was stopped manually.");
        //        else
        //            Debug.Log("t completed execution normally.");
        //    };
        //}


        if (GameManager.Instance._IsRefreshNeeded)
        {
            GameManager.Instance._IsRefreshNeeded = false;

            GetCurrentBuildingDetails();
            SpawningBuilding();
        }
    }


    /// <summary>
    /// Order of progression
    /// 1. Particle & Camera Zoom-In.
    /// 2. 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="inBuildingsElementNumber"></param>
    /// <param name="inBuildingLevel"></param>
    /// <param name="inCurrentLevelsMesh"></param>
    public IEnumerator UpgradeBuilding(string inBuildingName, int inBuildingsElementNumber, int inBuildingLevel, Transform inBuildingSpawnPoint, Transform inPanPoint)
    {
        mCoroutineIsInProcess = true;
        _isAnotherBuildingInConstruction = true;
        mCameraControllerRef._inBetweenConstructionProcess = true;
        GameObject goRef = GameObject.Find(inBuildingName);

        //Upgrading Scenario Starts Here
        Destroy(goRef, 1f);
        GameObject smokeVFX = Instantiate(mBuildingConstructionVFX, inBuildingSpawnPoint.position + mParticleOffSetFromBuilding, Quaternion.identity);
        mCameraParentRef.transform.DOMove(inBuildingSpawnPoint.position + mCameraOffSetFromBuilding, mCameraFocusSpeed, false).OnComplete(()=> { mCameraParentRef.transform.parent = inPanPoint.transform; });
        //yield return new WaitForSeconds(0.25f);

        goRef.transform.DOScaleY(0.75f, mBuildingShrinkAndEnlargeTime);/*.OnUpdate(()=> { mCameraParentRef.transform.DORotate(new Vector3(mCameraParentRef.transform.eulerAngles.x, mCameraParentRef.transform.eulerAngles.y -  1, mCameraParentRef.transform.eulerAngles.z), 0.5f, RotateMode.Fast); }).OnComplete(() => { /*Destroy(goRef);});*/
        yield return new WaitForSeconds(mBuildingSpawnTimeDelay);
        inPanPoint.DORotate(new Vector3(inPanPoint.transform.rotation.eulerAngles.x, inBuildingSpawnPoint.transform.rotation.eulerAngles.y + (-30f), inBuildingSpawnPoint.transform.rotation.eulerAngles.z), 1, RotateMode.Fast).OnComplete(() => { inPanPoint.transform.eulerAngles = new Vector3(inPanPoint.transform.localEulerAngles.x, 0f,inPanPoint.transform.localEulerAngles.z); mCameraParentRef.transform.parent = null; });

        GameObject newGoRef = Instantiate(_buildingData[inBuildingsElementNumber].UpgradeLevels[inBuildingLevel], _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.rotation);
        newGoRef.transform.localScale = new Vector3(1, 0.75f, 1);
        newGoRef.name = _buildingData[inBuildingsElementNumber]._buildingName;
        //newGoRef.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), .5f, 5, 0);

        newGoRef.transform.DOScaleY(1, mBuildingShrinkAndEnlargeTime).OnComplete(()=> {
            Destroy(smokeVFX);
            mCameraControllerRef._inBetweenConstructionProcess = false;
        }).WaitForCompletion();
        GameManager.Instance.UpdateBuildingData(inBuildingName, inBuildingsElementNumber, inBuildingLevel + 1, true, false, false);
        mCoroutineIsInProcess = false;
        //yield return new WaitForSeconds(mTimeDelayFromNewBuildingToCameraDefaultState);
        
        //_isAnotherBuildingInConstruction = false;


        
        ////_buildingData[inBuildingNumber].currentLevelGameObject = inCurrentLevelsMesh;
        ////Just in case if these data's are required
        //newGoRef.AddComponent<BuildingDetails>();
        //BuildingDetails buildingDetailRef = newGoRef.GetComponent<BuildingDetails>();
        //buildingDetailRef._buildingLevel = inLevel;
        ////buildingDetailRef._buildMeshBasedOnCurrentLevel = inCurrentLevelsMesh;

        //Saves the data from building manager to game manager
    }

    public void GrabElementNumberBasedOnButtonClick(int inBuildingsElementNumber)
    {
        //if (mCoroutineIsInProcess)
        //{
        //    DOTween.CompleteAll();
        //    //StopCoroutine(BuildingCoroutine);
        //    StopAllCoroutines();
        //    mCoroutineIsInProcess = false;
        //    //t.Stop();
        //}
        if (_buildingData[inBuildingsElementNumber]._buildingLevel < _buildingData[inBuildingsElementNumber]._buildingMaxLevel)
        {
            Debug.Log(mCoroutineIsInProcess);
            
            /*BuildingCoroutine =*/ StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint, _buildingData[inBuildingsElementNumber]._cameraFocusPoint));

            //StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

            //t = new Task(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

            //BuildingCoroutine = StartCoroutine(UpgradeBuilding(_buildingData[inBuildingsElementNumber]._buildingName, inBuildingsElementNumber, _buildingData[inBuildingsElementNumber]._buildingLevel, _buildingData[inBuildingsElementNumber]._buildingSpawnPoint.position));

            _buildingData[inBuildingsElementNumber]._buildingLevel += 1;
            _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Button>().interactable = false;   
            _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponent<Image>().color = new Color32(0, 0, 0, 100);
            _buildingData[inBuildingsElementNumber]._respectiveBuildingButtons.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = "Building....";
        }
        if (_buildingData[inBuildingsElementNumber]._buildingLevel == _buildingData[inBuildingsElementNumber]._buildingMaxLevel)
        {
            //_buildingData[inElementNumber]._respectiveBuildingButtons.GetComponent<Button>().interactable = false;
            _buildingData[inBuildingsElementNumber].didBuildingReachMaxLevel = true;
            CheckForAllBuildingMax();
        }
    }

    void CheckForAllBuildingMax()
    {
        //Check for Maxing
        for (int i = 0; i < _buildingData.Count; i++)
        {
            if (_buildingData[i].didBuildingReachMaxLevel != true)
                return;
        }
        LevelCompleted();
    }

    void LevelCompleted()
    {
        GameManager.Instance._playerCurrentLevel++;
        GameManager.Instance._IsBuildingFromFBase = false;
        GameManager.Instance.gameObject.GetComponent<LevelLoadManager>().LoadLevelASyncOf(GameManager.Instance._playerCurrentLevel);
    }

}




























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
