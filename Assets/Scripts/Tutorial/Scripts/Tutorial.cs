using System;
using UnityEngine;
[CreateAssetMenu(menuName ="Tutorials/PopUp Tutorial")]
public class Tutorial : ScriptableObject
{
    public int TutorialOrder;
    public string TutorialContent;
    public Sprite TutorialImage;
    float TutorialTime;
    public float TutorialShowTime;

    public Action OnTutorialComplete;
    public Action OnTimerRanOut;

    protected bool isCompleted;
    protected bool isTimerFinished;
    public virtual void ResetTheTutorial() 
    {
        isCompleted = false;
        isTimerFinished = false;
        TutorialTime = TutorialShowTime;
    }
    public virtual void SetUpTutorialTask() { }
    public virtual void RegisterUserAction() { }

    public virtual void CheckCompletion()
    {
        if (!isCompleted)
        {
            TutorialTime -= Time.deltaTime;
            if (TutorialTime < 0) 
            {
                OnTimerRanOut?.Invoke();
                isTimerFinished = true;
            }
            if (isTimerFinished && GetUserInput())
            {
                OnTutorialComplete?.Invoke();
                isCompleted = true;
            }
        }
       
    }
    public virtual bool GetUserInput()
    {
        if (Input.touchCount > 0)
            return true;
        else 
            return false;
    }
}
