using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    public static TutorialManager Instance;
    [SerializeField] Tutorial[] Tutorials;
    public object[] Tutorialsw;
    [SerializeField] GameObject TutorialUICanvas;
    [SerializeField] Image TutorialImage;
    [SerializeField] TMP_Text TutorialContentTxt;
    [SerializeField] TMP_Text TutorialHintTxt;

    Tutorial currentTutorial;
    int currentTutorialIndex = 0;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this); //Singleton
            return;
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        Tutorials = Resources.LoadAll<Tutorial>("Tutorials");
        StartNextTutorial(0);
        GameManager.Instance.isInTutorial = true;
    }
    public void StartNextTutorial(int tutorialIndex)
    {
        if (tutorialIndex >= Tutorials.Length)
        {
            GameManager.Instance.isInTutorial = false;

            return;
        }
        currentTutorialIndex = tutorialIndex;
        currentTutorial = Tutorials[tutorialIndex];

        currentTutorial.ResetTheTutorial();
        currentTutorial.SetUpTutorialTask();
       

        currentTutorial.OnTutorialComplete += OnTutorialComplete;
        currentTutorial.OnTimerRanOut += OnTimerFinish;

        if (currentTutorial.TutorialImage == null) return;
        TutorialImage.sprite = currentTutorial.TutorialImage;
        TutorialContentTxt.text = currentTutorial.TutorialContent;
        TutorialUICanvas.SetActive(true);
    }
    void OnTutorialComplete()
    {
        TutorialHintTxt.gameObject.SetActive(false);
        TutorialUICanvas.SetActive(false);
        StartNextTutorial(currentTutorialIndex + 1);
       

    }
    void OnTimerFinish()
    {
        TutorialHintTxt.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (currentTutorial != null) currentTutorial.CheckCompletion();
    }
}
