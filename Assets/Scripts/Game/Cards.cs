using UnityEngine;
using UnityEngine.UI;
public enum CardType
{
    ATTACK,
    STEAL,
    SHIELD,
    JOKER,
    ENERGY,
    COINS,
    FORTUNEWHEEL,
    SLOTMACHINE
}

public class Cards : MonoBehaviour
{
    public int _cardID;
    public CardType _cardType;
    public Vector3 _Position;
    private float rotationZ;
    Sprite changeSprite;
    Vector2 prePos;
    float preRotationZ;
    float preScale;
    [SerializeField] GameObject cardParticle;

    public void PlayTwoCardMatchAnim()
    {
        Animation anim = GetComponent<Animation>();

        AnimationCurve CurvePosY;
        AnimationCurve CurvePosX;

        AnimationClip clip = new AnimationClip();
        clip.legacy = true;


        Keyframe[] PosY;
        PosY = new Keyframe[3];
        PosY[0] = new Keyframe(0f, transform.localPosition.y);
        PosY[1] = new Keyframe(.4f, transform.localPosition.y + 100);
        PosY[2] = new Keyframe(.8f, transform.localPosition.y);
        CurvePosY = new AnimationCurve(PosY);


        Keyframe[] PosX;
        PosX = new Keyframe[3];
        PosX[0] = new Keyframe(0f, transform.localPosition.x);
        PosX[1] = new Keyframe(.4f, transform.localPosition.x);
        PosX[2] = new Keyframe(.8f, transform.localPosition.x);
        CurvePosX = new AnimationCurve(PosX);

        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        clip.SetCurve("", typeof(Transform), "localPosition.x", CurvePosX);
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }
    void ChangeSprite()
    {
        gameObject.GetComponent<Image>().sprite = changeSprite;
    }
    public void PlayJokerSelectionPairAnim(Transform inAnimateToTransform)
    { 
        prePos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        preRotationZ = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        preScale = transform.localScale.x;
       float rotation = (inAnimateToTransform.localEulerAngles.z > 180) ? inAnimateToTransform.localEulerAngles.z - 360 : inAnimateToTransform.localEulerAngles.z;
        Animation anim = GetComponent<Animation>();

        AnimationCurve CurvePosY;
        AnimationCurve CurvePosX;
        AnimationCurve CurveRotationZ;
        AnimationCurve CurveScale;
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;


        Keyframe[] PosY;
        PosY = new Keyframe[2];
        PosY[0] = new Keyframe(0f,transform.localPosition.y);
        PosY[1] = new Keyframe(1f, inAnimateToTransform.localPosition.y);



        Keyframe[] PosX;
        PosX = new Keyframe[2];
        PosX[0] = new Keyframe(0f, transform.localPosition.x);
        PosX[1] = new Keyframe(1f, inAnimateToTransform.localPosition.x);


        CurvePosY = new AnimationCurve(PosY);
        CurvePosX = new AnimationCurve(PosX);

        Keyframe[] RotationZ;
        RotationZ = new Keyframe[2];
        RotationZ[0] = new Keyframe(0f, (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z);
        RotationZ[1] = new Keyframe(1f, rotation);

        CurveRotationZ = new AnimationCurve(RotationZ);

        Keyframe[] Scale;
        Scale = new Keyframe[2];
        Scale[0] = new Keyframe(0f, transform.localScale.x);
        Scale[1] = new Keyframe(1f, inAnimateToTransform.localScale.x);

        CurveScale = new AnimationCurve(Scale);

        clip.SetCurve("", typeof(Transform), "localScale.y", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.x", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.z", CurveScale);
        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        clip.SetCurve("", typeof(Transform), "localPosition.x", CurvePosX);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", CurveRotationZ);

        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);

    }
    public void PlayJokerSelectionPairAnim(bool isLeft, int index)
    {
        prePos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        float inPosX = index == 1 ? 312 : 372;
        inPosX = isLeft ? inPosX * -1 : inPosX;
        Animation anim = GetComponent<Animation>();

        AnimationCurve CurvePosY;
        AnimationCurve CurvePosX;
        AnimationClip clip = new AnimationClip();

        clip.legacy = true;
        Keyframe[] PosY;
        PosY = new Keyframe[2];
        PosY[0] = new Keyframe(0f, transform.localPosition.y);
        PosY[1] = new Keyframe(1f, transform.localPosition.y + 300);

        CurvePosY = new AnimationCurve(PosY);


        Keyframe[] PosX;
        PosX = new Keyframe[2];
        PosX[0] = new Keyframe(0f, transform.localPosition.x);
        PosX[1] = new Keyframe(1f, inPosX);

        CurvePosX = new AnimationCurve(PosX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        clip.SetCurve("", typeof(Transform), "localPosition.x", CurvePosX);
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);

    }
    public void PlayJokerSelectionPairGetBackAnim()
    {
        Animation anim = GetComponent<Animation>();

        AnimationCurve CurvePosY;
        AnimationCurve CurvePosX;
        AnimationCurve CurveScale;
        AnimationCurve CurveRotationZ;
        AnimationClip clip = new AnimationClip();

        clip.legacy = true;
        Keyframe[] PosY;
        PosY = new Keyframe[2];
        PosY[0] = new Keyframe(0f, transform.localPosition.y);
        PosY[1] = new Keyframe(.4f, prePos.y);

        CurvePosY = new AnimationCurve(PosY);


        Keyframe[] PosX;
        PosX = new Keyframe[2];
        PosX[0] = new Keyframe(0f, transform.localPosition.x);
        PosX[1] = new Keyframe(.4f, prePos.x);

        CurvePosX = new AnimationCurve(PosX);

        Keyframe[] Scale;
        Scale = new Keyframe[2];
        Scale[0] = new Keyframe(0f, transform.localScale.x);
        Scale[1] = new Keyframe(.4f, preScale);

        CurveScale = new AnimationCurve(Scale);

        Keyframe[] RotationZ;
        RotationZ = new Keyframe[2];
        RotationZ[0] = new Keyframe(0f, (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z);
        RotationZ[1] = new Keyframe(.4f, preRotationZ);

        CurveRotationZ = new AnimationCurve(RotationZ);

        clip.SetCurve("", typeof(Transform), "localScale.y", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.x", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.z", CurveScale);
        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        clip.SetCurve("", typeof(Transform), "localPosition.x", CurvePosX);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", CurveRotationZ);
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }

    public void PlayThreeCardMatchAnim(float inPosX, Sprite s = null)
    {
        if (inPosX == 0&&_cardType!=CardType.SHIELD) Invoke(nameof(SpawnParticle), 3f);
        Animation anim = GetComponent<Animation>();

        AnimationCurve CurvePosY;
        AnimationCurve CurvePosX;
        AnimationCurve rotationCurvZ;
        AnimationCurve rotationCurvy;
        AnimationCurve scaleCurv;


        AnimationClip clip = new AnimationClip();

        clip.legacy = true;

        if (s != null)
        {
            changeSprite = s;
            Invoke("ChangeSprite", 1.2f);
            AnimationCurve rotationCurvX;
            Keyframe[] RotationX;
            RotationX = new Keyframe[2];
            RotationX[0] = new Keyframe(1f, 0);
            RotationX[1] = new Keyframe(1.5f, -360);
            rotationCurvX = new AnimationCurve(RotationX);
            clip.SetCurve("", typeof(Transform), "localEulerAngles.x", rotationCurvX);

        }

        Keyframe[] PosY;
        PosY = new Keyframe[5];
        PosY[0] = new Keyframe(0f, transform.localPosition.y);
        PosY[1] = new Keyframe(1f, 716);
        PosY[2] = new Keyframe(3.2f, 716);//716
        PosY[3] = new Keyframe(6.5f, 716);//716
        PosY[4] = new Keyframe(7.5f, -1100);

        CurvePosY = new AnimationCurve(PosY);

        Keyframe[] PosX;
        PosX = new Keyframe[4];
        PosX[0] = new Keyframe(0f, transform.localPosition.x);
        PosX[1] = new Keyframe(1f, inPosX);
        PosX[2] = new Keyframe(1.75f, inPosX);
        PosX[3] = new Keyframe(2f, 0);

        CurvePosX = new AnimationCurve(PosX);

        rotationZ = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;

        Keyframe[] RotationZ;
        RotationZ = new Keyframe[2];
        RotationZ[0] = new Keyframe(0f, rotationZ);
        RotationZ[1] = new Keyframe(1f, 0);

        Keyframe[] scale;
        scale = new Keyframe[5];
        scale[0] = new Keyframe(0f, .4f);
        scale[1] = new Keyframe(1f, .8f);
        scale[2] = new Keyframe(1.75f, .8f);
        scale[3] = new Keyframe(2.5f, .4f);
        scale[4] = new Keyframe(3f, 1.5f);

        rotationCurvZ = new AnimationCurve(RotationZ);
        scaleCurv = new AnimationCurve(scale);



        Keyframe[] Rotationy;
        Rotationy = new Keyframe[2];
        Rotationy[0] = new Keyframe(2f, 0);
        Rotationy[1] = new Keyframe(3f, -1080);

        rotationCurvy = new AnimationCurve(Rotationy);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", rotationCurvZ);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.y", rotationCurvy);

        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        clip.SetCurve("", typeof(Transform), "localPosition.x", CurvePosX);
        clip.SetCurve("", typeof(Transform), "localScale.x", scaleCurv);
        clip.SetCurve("", typeof(Transform), "localScale.y", scaleCurv);
        clip.SetCurve("", typeof(Transform), "localScale.z", scaleCurv);
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }


    void SpawnParticle()
    {
        GameObject go = Instantiate(cardParticle, this.transform);
        //go.GetComponent<CardParticle>()._Init(this.transform);
        go.transform.SetAsFirstSibling();
        go.SetActive(true);
    }



}

