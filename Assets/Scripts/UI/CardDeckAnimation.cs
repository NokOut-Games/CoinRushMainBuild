using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeckAnimation : MonoBehaviour
{

    public Sprite cardSprite;

    public GameObject effects;
    // Start is called before the first frame update


    public void SpriteChange()
    {
        this.GetComponent<Image>().sprite = cardSprite;
    }

    public void OnJokerChooseAnimation()
    {
        transform.localEulerAngles = Vector3.zero;

        Animation anim = GetComponent<Animation>();
        AnimationCurve CurvePosY;
        AnimationCurve CurveScale;
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        Keyframe[] PosY;
        PosY = new Keyframe[2];
        PosY[0] = new Keyframe(.3f, 716f);
        PosY[1] = new Keyframe(1f, 950f);

        CurvePosY = new AnimationCurve(PosY);

        Keyframe[] Scale;
        Scale = new Keyframe[2];
        Scale[0] = new Keyframe(.5f, 1.5f);
        Scale[1] = new Keyframe(1.0f, .4f);

        CurveScale = new AnimationCurve(Scale);


        clip.SetCurve("", typeof(Transform), "localScale.y", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.x", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.z", CurveScale);
        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);

    }
    public void PlayOnDropAnimation(Vector3 lastPos, float rotationZ)
    {
        effects.SetActive(true);
        Invoke(nameof(OffEffect), .4f);
        rotationZ = (rotationZ > 180) ? rotationZ - 360 : rotationZ;
        //a.SampleAnimation;
        Animation anim = GetComponent<Animation>();
        AnimationCurve CurvePosY;
        AnimationCurve CurvePosX;
        AnimationCurve rotationCurv;
        AnimationCurve CurveScale;
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;


        Keyframe[] PosY;
        PosY = new Keyframe[4];
        PosY[0] = new Keyframe(0f, 716f);
        PosY[1] = new Keyframe(.4f, 716f);
        /*PosY[1] = new Keyframe(1.0f,lastPos.y);*/

        PosY[2] = new Keyframe(.8f, lastPos.y - 50f);
        PosY[3] = new Keyframe(1f, lastPos.y);



        Keyframe[] PosX;
        PosX = new Keyframe[3];
        PosX[0] = new Keyframe(0f, 0f);
        PosX[1] = new Keyframe(.4f, 0f);
        PosX[2] = new Keyframe(.8f, lastPos.x);


        CurvePosY = new AnimationCurve(PosY);
        CurvePosX = new AnimationCurve(PosX);


        /* anim.AddClip(clip, clip.name);
         anim.Play(clip.name);*/


        //keys[2] = new Keyframe(20.0f, 0.0f);
        Keyframe[] RotationZ;
        RotationZ = new Keyframe[3];
        RotationZ[0] = new Keyframe(0f, 0f);
        RotationZ[1] = new Keyframe(.4f, 0f);
        RotationZ[2] = new Keyframe(.8f, rotationZ);



        rotationCurv = new AnimationCurve(RotationZ);




        Keyframe[] Scale;
        Scale = new Keyframe[3];
        Scale[0] = new Keyframe(0f, 1.5f);
        Scale[1] = new Keyframe(.4f, 1.5f);
        Scale[2] = new Keyframe(.8f, 0.4f);





        CurveScale = new AnimationCurve(Scale);

        clip.SetCurve("", typeof(Transform), "localScale.y", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.x", CurveScale);
        clip.SetCurve("", typeof(Transform), "localScale.z", CurveScale);
        clip.SetCurve("", typeof(Transform), "localPosition.y", CurvePosY);
        clip.SetCurve("", typeof(Transform), "localPosition.x", CurvePosX);
        clip.SetCurve("", typeof(Transform), "localEulerAngles.z", rotationCurv);


        // s.position.

        // update the clip to a change the red color             
        //animation = clip;
        // now animate the GameObject
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);
    }

    private void OffEffect()
    {
        effects.SetActive(false);

    }
}