using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LerpValues : MonoBehaviour // have UI scale depending on particle graph for extra effect, lerp it here instead of animation
{
    [SerializeField]
    AnimationCurve animationCurve; // im the curve             // make ui and select you´re lerp for the object

    [SerializeField]
    enum MyLerp { Curve, Float, Vector }

    [SerializeField]
    MyLerp lerp;

    public void LerpCurve(AnimationCurve curve) // lerp anything
    {

    }

    public void LerpVector(Vector3 f1, Vector3 f2, float t = 2)
    {
        StartCoroutine(LerpVectory(f1, f2, t)); 
    }

    IEnumerator LerpVectory(Vector3 f1, Vector3 f2, float cd) // need the object
    {
        float t = 0;
        while (t <= cd)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(f1, f2, t);
            yield return new WaitForEndOfFrame();
        }
    }

    public void LerpFloat(float f1, float f2, float t)// call this and send the object to lerp as well or set this on the item that needs to be lerped?
    {
        StartCoroutine(LerpFloaty(f1, f2, t));
    }
    IEnumerator LerpFloaty(float f1, float f2, float cd) // unclamped gives linear interpolation
    {
        float t = 0;
        while (t <= cd)
        {
            t += Time.deltaTime;
            Mathf.Lerp(f1, f2, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
