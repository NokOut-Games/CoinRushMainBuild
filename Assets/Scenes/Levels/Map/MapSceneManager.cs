using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneManager : MonoBehaviour
{
    [SerializeField] LevelSet[] levelSets;
    [SerializeField] int unlockedLevel;
    [SerializeField] MapCameraController mCameraController;
    [SerializeField]Button backBtn;
    [SerializeField] GameObject AllLevelCompleted;



    private void Start()
    {
        if (!PlayerPrefs.HasKey("MadeHisChoice"))
        {
            TutorialManager.Instance.StartNextTutorial(0);
            GameManager.Instance.isInTutorial = true;
        }
        backBtn.onClick.AddListener(()=>{ LevelLoadManager.instance.BacktoHome(); });

        if (GameManager.Instance.hasChoiceInLevel) backBtn.gameObject.SetActive(false);
/*
        if (GameManager.Instance._SetIndex < levelSets.Length && IsSetCompleted(GameManager.Instance._SetIndex) )
        {
            GameManager.Instance._CompletedLevelsInSet.Clear();

            GameManager.Instance._SetIndex++;
        }
        else*/ if(GameManager.Instance._SetIndex >= levelSets.Length && IsSetCompleted(GameManager.Instance._SetIndex))
        {
            backBtn.gameObject.SetActive(true);
            AllLevelCompleted.SetActive(true);
        }
        // Debug.LogError(GameManager.Instance._SetIndex);
        MakeSetCompletedUntilIndex(GameManager.Instance._SetIndex,GameManager.Instance._CompletedLevelsInSet);
        SetMapScreen(GameManager.Instance.hasChoiceInLevel);
        mCameraController.transform.position = new Vector3(0, 0, GameManager.Instance._playerCurrentLevel);

    }
    private void Update()
    {
        

        mCameraController._EndBoundary = unlockedLevel;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)&& !TutorialManager.Instance.isPopUpRunning)
        {
            if (hit.collider.CompareTag("MapTarget"))
            {
                FirebaseManager.Instance.readUserData = true;

                hit.collider.transform.parent.GetChild(2).gameObject.SetActive(false);
                GameManager.Instance.hasChoiceInLevel = false;
                hit.collider.transform.parent.GetComponent<Level>().UnlockLevel();
                ArrangeSet(hit.collider.transform.parent.GetComponent<Level>().setIndex);
                GameManager.Instance._CompletedLevelsInSet.Add(hit.collider.transform.parent.GetComponent<Level>().levelIndex);
                hit.collider.transform.parent.GetChild(0).GetComponent<Animator>().SetBool("OpenCloud", true);
                GameManager.Instance._playerCurrentLevel = hit.collider.transform.parent.GetComponent<Level>().levelNO;
                PlayerPrefs.SetInt("MadeHisChoice", 1);
                LevelLoadManager.instance.LoadLevelASyncOf(hit.collider.transform.parent.gameObject.name,2000); 
                GameManager.Instance._IsBuildingFromFBase = false;

            }
        }
    }

    void MakeSetCompletedUntilIndex(int inIndex,List<int> inCompletedLevelIndexes)
    {
        unlockedLevel = 0;
        for (int i = 0; i < levelSets.Length; i++)
        {
          //  Debug.LogError(i);
            if (i > inIndex)
                return;
            for (int j = 0; j < levelSets[i].levels.Length; j++)
            {
                unlockedLevel++;
                //mCameraController.transform.position = new Vector3(0, 0, unlockedLevel);
                if ((i < inIndex)|| (i == inIndex && inCompletedLevelIndexes.Contains(j)))
                {
                   // mCameraController.transform.position = new Vector3(0, 0, unlockedLevel-1);
                    levelSets[i].levels[j].isUnlocked = true;
                }
            }
        }

    }
    void SetMapScreen(bool hasChoise=false)
    {
        int i = 0;
        foreach (var levelSet in levelSets)
        {
            foreach (var level in levelSet.levels)
            {
                if (level.isUnlocked)
                {
                    level.transform.GetChild(0).gameObject.SetActive(false);
                    level.transform.GetChild(2).gameObject.SetActive(false);
                   //unlockedLevel = (int)(level.gameObject.name[level.gameObject.name.Length-1]);
                }
                else
                    level.transform.GetChild(2).gameObject.SetActive(hasChoise);
            }
            if (!IsSetCompleted(i)) return;
            i++;
        }
    }


    void ArrangeSet(int inSetIndex)
    {
        if (!IsSetCompleted(inSetIndex))
        {
            foreach (var level in levelSets[inSetIndex].levels)
            {
                if (!level.isUnlocked) level.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else
        {
            if (GameManager.Instance._SetIndex < levelSets.Length)
            {
                GameManager.Instance._CompletedLevelsInSet.Clear();
                GameManager.Instance._SetIndex++;
            }
        }
        
    }
    bool IsSetCompleted(int inSetIndex)
    {
        foreach (var level in levelSets[inSetIndex].levels)
        {
            if (!level.isUnlocked) return false;
        }
        return true;
    }

}



