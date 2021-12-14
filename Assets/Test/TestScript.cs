using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    [SerializeField] Transform GotoPoint;
    public Quaternion rotateTo;

    private void OnMouseDown()
    {
        Func();
    }

    void Func()
    {
        this.gameObject.transform.DOMove(GotoPoint.position, 1, false)
            .OnUpdate(() =>
            {
                //Time.timeScale = 0.1f;
                this.gameObject.transform.DOScale(.5f, 1);
                this.gameObject.GetComponent<Animator>().SetTrigger("showcase");

                this.gameObject.transform.DORotateQuaternion(rotateTo, 1);
                
            })
            .OnComplete(() =>
            {
                Debug.Log("I Have Completed");
            });
    }
}






//private void OnCollisionEnter2D(Collision2D collision)
//{
//    this.transform.GetComponent<Animator>().SetTrigger("isColliding?");
//    Debug.Log("Ouch!!!");
//}