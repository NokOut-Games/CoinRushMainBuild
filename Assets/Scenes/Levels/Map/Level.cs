using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelIndex;
    public string levelName;
    public int levelNO;
    public int setIndex;
    public bool isUnlocked;

    public void UnlockLevel()
    {
        isUnlocked = true;
        //PlayerPrefs.SetInt(gameObject.name, 1);
    }
}
