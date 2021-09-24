using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerUI : MonoBehaviour
{
    public List<GameObject> OverAllCards;

    public List<GameObject> FiveK;
    public List<GameObject> TWENTYFIVEK;
    public List<GameObject> HUNDREDK;
    public List<GameObject> FIVEHUNDREDK;
    public List<GameObject> ONEM;

    public GameManager gameManager;

    public bool isDone = false; 

    private void Start()
    {
 
    }

    private void Update()
    {
        if (!isDone)
        {
            if (FiveK.Count == 3)
            {
                gameManager._coins += 5000;
                isDone = true;
                foreach (GameObject c in FiveK)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                FiveK.Clear();
            }
            if (TWENTYFIVEK.Count == 3)
            {
                gameManager._coins += 25000;
                isDone = true;
                foreach (GameObject c in TWENTYFIVEK)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                TWENTYFIVEK.Clear();
            }
            if (HUNDREDK.Count == 3)
            {
                gameManager._coins += 100000;
                isDone = true;
                foreach (GameObject c in HUNDREDK)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                HUNDREDK.Clear();
            }
            if (FIVEHUNDREDK.Count == 3)
            {
                gameManager._coins += 500000;
                isDone = true;
                foreach (GameObject c in FIVEHUNDREDK)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                FIVEHUNDREDK.Clear();
            }
            if (ONEM.Count == 3)
            {
                gameManager._coins += 1000000;
                isDone = true;
                foreach (GameObject c in ONEM)
                {
                    //PlayAnimation
                    Destroy(c);
                }
                ONEM.Clear();
            }
        }
        //if (FiveK.Count == 3)
        //{
        //    gameManager._coins += 5000;
        //    FiveK.Clear();
        //}
        //if (TWENTYFIVEK.Count == 3)
        //{
        //    gameManager._coins += 25000;
        //    TWENTYFIVEK.Clear();
        //}
        //if (HUNDREDK.Count == 3)
        //{
        //    gameManager._coins += 100000;
        //    HUNDREDK.Clear();
        //}
        //if (FIVEHUNDREDK.Count == 3)
        //{
        //    gameManager._coins += 500000;
        //    FIVEHUNDREDK.Clear();
        //}
        //if (ONEM.Count == 3)
        //{
        //    gameManager._coins += 1000000;
        //    ONEM.Clear();
        //}
    }
}
