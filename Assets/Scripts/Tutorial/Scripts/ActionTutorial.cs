using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorials/Action Tutorial")]
public class ActionTutorial : Tutorial
{
    [SerializeField] TutorialActionType actionType;
    [SerializeField] CardType cardType;
    public int MaxPlayerClick;
    int numberOfPlayerClicks;
    public override bool GetUserInput()
    {
        switch (actionType)
        {
            case TutorialActionType.Card:
                if (numberOfPlayerClicks >= MaxPlayerClick)
                    return true;

                break;
            case TutorialActionType.Building:
                if (numberOfPlayerClicks >= MaxPlayerClick)
                {
                    //GameObject.Find("GameCanvas").GetComponent<MenuUI>().CloseBuildButton();
                    Camera.main.GetComponent<CameraController>().DrawButtonClicked();
                    return true;
                }


                break;
            case TutorialActionType.Multiplier:

                if (numberOfPlayerClicks >= MaxPlayerClick)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;

    }
    public override void SetUpTutorialTask()
    {
        switch (actionType)
        {
            case TutorialActionType.Card:
                GameObject.Find("CardDeck").GetComponent<CardDeck>().AssignTutorial(this, cardType);
                break;
            case TutorialActionType.Building:
                GameManager.Instance.AssignTutorial(this);
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().BuildButton();
                break;
            case TutorialActionType.Multiplier:
                GameObject.Find("CardDeck").GetComponent<CardDeck>().AssignTutorial(this,CardType.JOKER);
                


                break;
            default:
                break;
        }
    }


    public override void RegisterUserAction()
    {
        numberOfPlayerClicks++;
    }
    public override void ResetTheTutorial()
    {
        base.ResetTheTutorial();
        numberOfPlayerClicks = 0;
    }
}

public enum TutorialActionType
{
    Card,
    Building,
    Multiplier
}
