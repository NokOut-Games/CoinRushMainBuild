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
        if (numberOfPlayerClicks >= MaxPlayerClick)
            return true;
        else
            return false;

    }
    public override void SetUpTutorialTask()
    {
        switch (actionType)
        {
            case TutorialActionType.UIAnim:
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().UIElementActivate(1,false);
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().UIElementActivate(2,false);
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().UIElementActivate(3,false);
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().UIElementActivate(5, false);

                break;

            case TutorialActionType.Card:
                GameObject.Find("CardDeck").GetComponent<CardDeck>().AssignTutorial(this, cardType);
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().CloseBuildButton();
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().UIElementActivate(5,true);

                break;
            case TutorialActionType.Building:
                GameManager.Instance.AssignTutorial(this);
                GameObject.Find("GameCanvas").GetComponent<MenuUI>().BuildButton();
                break;
            case TutorialActionType.Multiplier:
                GameObject.Find("CardDeck").GetComponent<CardDeck>().AssignTutorial(this,CardType.JOKER);              
                break;
            case TutorialActionType.SceneSwitch:
                LevelLoadManager.instance.AssignTutorial(this);
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
    NONE,
    Card,
    Building,
    Multiplier,
    SceneSwitch,
    UIAnim
}
