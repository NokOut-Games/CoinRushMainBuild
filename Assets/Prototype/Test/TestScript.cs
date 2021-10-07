using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
     public float startAngle;
     public float endAngle;
     public int segments;
     public float radius;

    public List<Vector3> arcPoints = new List<Vector3>();
    LineRenderer line;
    public List<GameObject> cardModel;

    public GameObject attachToCanvas;
    public Transform fistCard;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.startColor = Color.black;
        line.endColor = Color.black;
        DrawArc();
    }

    private void Update()
    {

    }
    void DrawArc()
    {
        float angle = startAngle;
        float arcLength = endAngle - startAngle;
        int j = -45;
        for (int i = 0; i <= segments; i++ , j+= 15)
	    {
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            arcPoints.Add(new Vector3(x, y, 0));
            line.SetPosition(i,new Vector3(x,y,0));


            angle += (arcLength / segments);
            GameObject card = Instantiate(cardModel[Random.Range(0, cardModel.Count)], fistCard.transform.position + arcPoints[i], Quaternion.Euler(0,0,j));
            card.transform.SetParent(attachToCanvas.transform);
        }

    }

    public void DrawCardss()
    {

    }
}
