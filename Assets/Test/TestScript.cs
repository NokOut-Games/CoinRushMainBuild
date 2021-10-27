using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public int _maxHoldTime = 5;
    public RectTransform _drawButtonRectTransform;
    [SerializeField] private GameManager mGameManager;

    [SerializeField] private GameObject mCardHolderParent;
    private int clicks = 0;

    [SerializeField] public List<ScriptedCards> mScriptedCards;
    public List<Cards> _CardList = new List<Cards>();
    public List<Transform> _playerHandPoints;
    [HideInInspector] public List<Vector3> _PositionList = new List<Vector3>();
    [HideInInspector] public List<Quaternion> _RotationList = new List<Quaternion>();

    public float time = 0;

    public bool autoCardDraw = false;
    public bool automaticDrawModeOn = false;
    public bool onceDone = false;

    public Image DrawButton;
    public Sprite drawNormal, drawAutomatic;
    
    private void Start()
    {
        DrawButton.sprite = drawNormal;
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (clicks == 8)
        {
            clicks = 0;
        }

        //time = Mathf.Clamp(time,0f,5f);
        Vector2 localMousePosition = _drawButtonRectTransform.InverseTransformPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (_drawButtonRectTransform.rect.Contains(localMousePosition))
            {
                if (automaticDrawModeOn)
                {
                    BackToNormalState();
                }
                else
                {
                    time = 0;
                    DrawCard();
                }
            }
        }

        if (!onceDone)
        {
            if (Input.GetMouseButton(0))
            {
                if (_drawButtonRectTransform.rect.Contains(localMousePosition))
                {
                    time += Time.fixedDeltaTime;
                    if (time >= _maxHoldTime)
                    {
                        onceDone = true;
                        automaticDrawModeOn = true;
                        autoCardDraw = true;
                        ChangeSprites();
                        StartCoroutine(AutomaticCardDrawing());
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_drawButtonRectTransform.rect.Contains(localMousePosition))
            {
                time = 0;
            }
        }
    }

    public void BackToNormalState()
    {
        automaticDrawModeOn = false;
        ChangeSprites();
        onceDone = false;
        autoCardDraw = false;
        StopCoroutine(AutomaticCardDrawing());
    }

    void ChangeSprites()
    {
        if (DrawButton.sprite == drawNormal)
        {
            DrawButton.sprite = drawAutomatic;
        }
        else if (DrawButton.sprite == drawAutomatic)
        {
            DrawButton.sprite = drawNormal;
        }
    }

    public void DrawCard()
    {
        if (_CardList.Count >= 8)
        {
            return;
        }
        mGameManager._energy -= 1;

        Camera.main.GetComponent<CameraController>().DrawButtonClicked();

        ScriptedCards cards = mScriptedCards[Random.Range(0, mScriptedCards.Count)];

        GameObject card = Instantiate(cards._cardModel, _playerHandPoints[clicks].localPosition, _playerHandPoints[clicks].localRotation, mCardHolderParent.transform);
        Cards cardDetails = card.GetComponent<Cards>();

        cardDetails._cardType = cards._cardType;
        cardDetails._cardID = cards._cardID;
        cardDetails._Position = card.transform.position;

        clicks += 1;
        AddNewCard(card.GetComponent<Cards>());
        ReplacementOfCards();
        CardCheckingFunction();
    }

    IEnumerator AutomaticCardDrawing()
    {
        while(autoCardDraw)
        {
            DrawCard();
            yield return new WaitForSeconds(5);
        }
    }
    
    public void AddNewCard(Cards inNewCard)
    {
        for (int i = 0; i < _CardList.Count; i++)
        {
            if (_CardList[i]._cardType == inNewCard._cardType)
            {
                _CardList.Insert(i, inNewCard);
                return;
            }
        }
        _CardList.Add(inNewCard);
    }

    public void ReplacementOfCards()
    {
        int medianIndex = _playerHandPoints.Count / 2;

        int incrementValue = 0;
        _PositionList.Clear();
        _RotationList.Clear();

        List<int> drawOrderArrange = new List<int>();

        for (int i = 0; i < _CardList.Count; i++)
        {
            if (i % 2 == 0 || i == 0)
            {
                drawOrderArrange.Add(medianIndex + incrementValue);
                incrementValue++;
            }
            else
            {
                drawOrderArrange.Add(medianIndex - incrementValue);
            }
        }

        drawOrderArrange.Sort();

        for (int i = 0; i < _CardList.Count; i++)
        {
            _PositionList.Add(_playerHandPoints[drawOrderArrange[i]].transform.position);
            _RotationList.Add(_playerHandPoints[drawOrderArrange[i]].transform.rotation);
        }

        for (int i = 0; i < _CardList.Count; i++)
        {
            _CardList[i]._Position = _PositionList[i];
            _CardList[i].transform.position = _PositionList[i];
            _CardList[i].transform.rotation = _RotationList[i];
            _CardList[i].transform.SetSiblingIndex(i + 1);
        }
    }

    void CardCheckingFunction()
    {
        for (int i = 0; i < _CardList.Count - 2; i++)
        {
            if (_CardList[i]._cardType == _CardList[i + 1]._cardType && _CardList[i + 1]._cardType == _CardList[i + 2]._cardType)
            {
                StartCoroutine(DelayedSceneLoader(_CardList[i]._cardType));
            }
        }
    }

    IEnumerator DelayedSceneLoader(CardType inType)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(inType.ToString());
    }
}
