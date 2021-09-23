using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerUI : MonoBehaviour
{
    public List<GameObject> FiveK;
    public List<GameObject> TWENTYFIVEK;
    public List<GameObject> HUNDREDK;
    public List<GameObject> FIVEHUNDREDK;
    public List<GameObject> ONEM;

    public GameManager gameManager;

    private void Start()
    {
 
    }

    private void Update()
    {
        if (FiveK.Count == 3)
        {
            gameManager._coins += 5000;
        }
    }
}
