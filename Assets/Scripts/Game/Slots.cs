using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slots : MonoBehaviour
{
    public Reels[] reels;
    public Button _uiSpinButton;
    public Text _uiSpinButtonText;

    private GameManager mGameManager;

    public List<string> elementNames;
    
    private void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _uiSpinButton.onClick.AddListener(()=>
        {
            _uiSpinButton.interactable = false; _uiSpinButtonText.text = "Rolled";
            StartCoroutine(DelayedSpin());
            elementNames.Clear();
        });
    }

    /// <summary>
    /// Function for spin button to work that contains an IEnumerator / Invoke Method for a Delayed start of Reels Spinning creating a moment of suspension.
    /// </summary>
    /// 
    IEnumerator DelayedSpin()
    {
        foreach (Reels reel in reels)
        {
            reel.roll = true;
            
            //Things to happen when roll ends and stops.                                                                                              
            reel.OnReelRollEnd(reel => { elementNames.Add(reel.SlotElementsName); _uiSpinButton.interactable = true; _uiSpinButtonText.text = "Roll"; Invoke(nameof(MainSceneInvoke), 1f);/*ResultChecker();*/ });
            reel.mdisableRoll = false;
        }
        for (int i = 0; i < reels.Length; i++)
        {
            //Allow The Reels To Spin For A Random Amout Of Time Then Stop Them
            yield return new WaitForSeconds(Random.Range(2, 5));
            reels[i].Spin();
        }
    }

    //private void ResultChecker()
    //{
    //    if (elementNames[0] == "5KCoins" && elementNames[1] == "5KCoins" && elementNames[2] == "5KCoins")
    //    {
    //        Debug.Log("3 are Diamonds");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "25KCoins" && elementNames[1] == "25KCoins" && elementNames[2] == "25KCoins")
    //    {
    //        Debug.Log("3 are Crowns");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "100KCoins" && elementNames[1] == "100KCoins" && elementNames[2] == "100KCoins")
    //    {
    //        Debug.Log("3 are Melons");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "500KCoins" && elementNames[1] == "500KCoins" && elementNames[2] == "500KCoins")
    //    {
    //        Debug.Log("3 are Bars");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "1M" && elementNames[1] == "1M" && elementNames[2] == "1M")
    //    {   
    //        Debug.Log("3 are Sevens");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "10Energy" && elementNames[1] == "10Energy" && elementNames[2] == "10Energy")
    //    {
    //        Debug.Log("3 are Cherries");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "25Energy" && elementNames[1] == "25Energy" && elementNames[2] == "25Energy")
    //    {
    //        Debug.Log("3 are Lemons");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if (elementNames[0] == "100Energy" && elementNames[1] == "100Energy" && elementNames[2] == "100Energy")
    //    {
    //        Debug.Log("3 are Lemons");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "5KCoins" && elementNames[1] == "5KCoins") || (elementNames[1] == "5KCoins" && elementNames[2] == "5KCoins") || (elementNames[0] == "5KCoins" && elementNames[2] == "5KCoins"))
    //    {
    //        Debug.Log(" Diamond " + " Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "25KCoins" && elementNames[1] == "25KCoins") || (elementNames[1] == "25KCoins" && elementNames[2] == "25KCoins") || (elementNames[0] == "25KCoins" && elementNames[2] == "25KCoins"))
    //    {
    //        Debug.Log(" Crown " + " Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "100KCoins" && elementNames[1] == "100KCoins") || (elementNames[1] == "100KCoins" && elementNames[2] == "100KCoins") || (elementNames[0] == "100KCoins" && elementNames[2] == "100KCoins"))
    //    {
    //        Debug.Log(" Melon " + " Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "500KCoins" && elementNames[1] == "500KCoins") || (elementNames[1] == "500KCoins" && elementNames[2] == "500KCoins") || (elementNames[0] == "500KCoins" && elementNames[2] == "500KCoins"))
    //    {
    //        Debug.Log(" Bar " + " Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "1M" && elementNames[1] == "1M") || (elementNames[1] == "1M" && elementNames[2] == "1M") || (elementNames[0] == "1M" && elementNames[2] == "1M"))
    //    {
    //        Debug.Log(" Seven " + "Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "10Energy" && elementNames[1] == "10Energy") || (elementNames[1] == "10Energy" && elementNames[2] == "10Energy") || (elementNames[0] == "10Energy" && elementNames[2] == "10Energy"))
    //    {
    //        Debug.Log(" Cherry " + " Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //    else if ((elementNames[0] == "25Energy" && elementNames[1] == "25Energy") || (elementNames[1] == "25Energy" && elementNames[2] == "25Energy") || (elementNames[0] == "25Energy" && elementNames[2] == "25Energy"))
    //    {
    //        Debug.Log(" Lemon " + "Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);
    //    }
    //    else if ((elementNames[0] == "100Energy" && elementNames[1] == "100Energy") || (elementNames[1] == "100Energy" && elementNames[2] == "100Energy") || (elementNames[0] == "100Energy" && elementNames[2] == "100Energy"))
    //    {
    //        Debug.Log(" Lemon " + "Only Two are identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);
    //    }
    //    else
    //    {
    //        Debug.Log("Dude Nothing is Identical");
    //        Invoke(nameof(MainSceneInvoke), 2f);

    //    }
    //}

    void MainSceneInvoke()
    {
        SceneManager.LoadScene(0);
    }
}


































//foreach (string s in elementNames)
//{
//    if (elementNames.Any(s => s.Contains("Diamond")))
//    {
//        Debug.Log("x");
//    }
//}
//elementNames.Clear();
//string localName = inReel.SlotElementsName;
//Debug.Log("Outcome: " + localName);
//if (elementNames[0] == localName && elementNames[1] == localName && elementNames[2] == localName)
//{
//    Debug.Log("Result: " + inReel.SlotElementsName);
//}
//else if ((elementNames[0] == localName && elementNames[1] == localName) || (elementNames[1] == localName && elementNames[2] == localName) || (elementNames[0] == localName && elementNames[2] == localName))
//{
//    Debug.Log("Result: " + inReel.SlotElementsName + "  But Only Two Came");
//}
//else if (elementNames[0] != localName && elementNames[1] != localName && elementNames[2] != localName)
//{
//    Debug.Log("Result: " + "Nothing is Identical");
//}
//else
//{
//    Debug.Log("x");
//}
//for (int i = 0; i < elementNames.Count; i++)
//{
//    if (elementNames[i] == localName)
//    {
//        Debug.Log("Result: " + inReel.SlotElementsName);
//    }
//    //else if ((elementNames[0] == localName && elementNames[1] == localName) || (elementNames[1] == localName && elementNames[2] == localName) || (elementNames[0] == localName && elementNames[2] == localName))
//    //{
//    //    Debug.Log("Result: " + inReel.SlotElementsName + "  But Only Two Came");
//    //}
//    //else if (elementNames[0] != localName && elementNames[1] != localName && elementNames[2] != localName)
//    //{
//    //    Debug.Log("Result: " + "Nothing is Identical");
//    //}
//}
//Residue()
//{
//public void SpinButtonFunctionality()
//{
//    //reels[0].Spin();
//    if (startSpin == false)
//    {
//        startSpin = true;

//    }
//}

//void Update()
//{



//}
//CheckResults();
//Allows The Machine To Be Started Again 
//resultsChecked = false;
//    //reels[i].RandomPosition();
//    //startSpin = false;
//    //yield return null;

//if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
//{
//    resultsChecked = false;
//}

//if (reels[0].rowStopped && reels[1].rowStopped && reels[2].rowStopped && !resultsChecked)
//{
//    CheckResults();
//}
//private void CheckResults()
//{
//    //Jackpot();
//    //Twosame();
//    ///this else if is checking whether anyone of the element is not equal
//    //if ((reels[0].stoppedSlot != reels[1].stoppedSlot) && (reels[0].stoppedSlot != reels[2].stoppedSlot) && (reels[1].stoppedSlot != reels[2].stoppedSlot))
//    //{
//    // //   Losepanel.SetActive(true);
//    //    Debug.Log("Better Luck Next Time");
//    //}

//    resultsChecked = true;
//}


//private void Jackpot()
//{

//    ///if 3 items  are equal then here we should give reward
//    if (reels[0].stoppedSlot == "Diamond" && reels[1].stoppedSlot == "Diamond" && reels[2].stoppedSlot == "Diamond")
//    {
//        // JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Diamonds");
//    }

//    else if (reels[0].stoppedSlot == "Crown" && reels[1].stoppedSlot == "Crown" && reels[2].stoppedSlot == "Crown")
//    {
//        //JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Crowns");
//    }

//    else if (reels[0].stoppedSlot == "Melon" && reels[1].stoppedSlot == "Melon" && reels[2].stoppedSlot == "Melon")
//    {
//        //  JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Melons");
//    }

//    else if (reels[0].stoppedSlot == "Bar" && reels[1].stoppedSlot == "Bar" && reels[2].stoppedSlot == "Bar")
//    {
//        //  JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Bars");
//    }

//    else if (reels[0].stoppedSlot == "Seven" && reels[1].stoppedSlot == "Seven" && reels[2].stoppedSlot == "Seven")
//    {
//        // JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Sevens");
//    }

//    else if (reels[0].stoppedSlot == "Cherry" && reels[1].stoppedSlot == "Cherry" && reels[2].stoppedSlot == "Cherry")
//    {
//        // JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Cherries");
//    }

//    else if (reels[0].stoppedSlot == "Lemon" && reels[1].stoppedSlot == "Lemon" && reels[2].stoppedSlot == "Lemon")
//    {
//        //  JackpotPanel.SetActive(true);
//        // jackpotTxt.text = "Hurray the jackpot you won is " + rows[0].stoppedSlot;
//        Debug.Log("3 are Lemons");
//    }
//}


//private void Twosame()
//{
//    ///If 2 items are same in any slot then reward should be given here

//    if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Diamond"))
//       || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Diamond"))
//       || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Diamond")))
//    {
//        //Debug.Log(reels[0].stoppedSlot);
//        // Twosamepanel.SetActive(true);
//        // SameTxt.text = "You Won " + rows[0].stoppedSlot;
//        Debug.Log("Diamond only");
//    }


//    else if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Crown"))
//        || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Crown"))
//        || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Crown")))
//    {
//        //Debug.Log(reels[0].stoppedSlot);
//        //   Twosamepanel.SetActive(true);
//        //  SameTxt.text = "You Won " + rows[0].stoppedSlot;
//        Debug.Log("Crown only");
//    }


//    else if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Melon"))
//          || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Melon"))
//          || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Melon")))
//    {
//        //Debug.Log(reels[0].stoppedSlot);
//        // Twosamepanel.SetActive(true);
//        // SameTxt.text = "You Won " + rows[0].stoppedSlot;
//        Debug.Log("Melon only");
//    }



//    else if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Bar"))
//          || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Bar"))
//          || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Bar")))
//    {
//        //Debug.Log(reels[0].stoppedSlot);
//        // Twosamepanel.SetActive(true);
//        // SameTxt.text = "You Won " + rows[0].stoppedSlot;
//        Debug.Log("Bar only");
//    }


//    else if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Sevon"))
//        || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Sevon"))
//        || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Sevon")))
//    {
//        //Debug.Log(reels[0].stoppedSlot);
//        // Twosamepanel.SetActive(true);
//        // SameTxt.text = "You Won " + rows[0].stoppedSlot;
//        Debug.Log("Sevon only");
//    }


//    else if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Cherry"))
//         || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Cherry"))
//         || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Cherry")))
//    {
//        //Debug.Log("Cherry only");
//        // Twosamepanel.SetActive(true);
//        // SameTxt.text = "You Won " + rows[0].stoppedSlot;
//        Debug.Log(reels[0].stoppedSlot);
//    }



//    else if (((reels[0].stoppedSlot == reels[1].stoppedSlot) && (reels[0].stoppedSlot == "Lemon"))
//        || ((reels[0].stoppedSlot == reels[2].stoppedSlot) && (reels[0].stoppedSlot == "Lemon"))
//        || ((reels[1].stoppedSlot == reels[2].stoppedSlot) && (reels[1].stoppedSlot == "Lemon")))
//    {
//        //Debug.Log(reels[0].stoppedSlot);
//        //  Twosamepanel.SetActive(true);
//        //  SameTxt.text = "You Won" + rows[0].stoppedSlot;
//        Debug.Log("Lemon only");
//    }

//}
//}