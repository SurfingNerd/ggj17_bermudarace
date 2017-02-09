using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncidentTextController : MonoBehaviour {

    public void SetText(string text)
    {
        foreach(TextMesh mesh in this.GetComponentsInChildren<TextMesh>())
        {
            mesh.text = text;
        }
    }


    TextMesh mTextMesh;
    ScaleUpAndDestroy mScaleUpScript;
    AnimationCurve mAcinmationCurve;
    // Use this for initialization
    void Start ()
    {
        mTextMesh = this.GetComponentInChildren<TextMesh>();
        mScaleUpScript = GetComponent<ScaleUpAndDestroy>();
        mAcinmationCurve = AnimationCurve.EaseInOut(0, 0, mScaleUpScript.seconds, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //float alpha = mAcinmationCurve.Evaluate(mScaleUpScript.GetProgress());
        float alpha = 1 - mScaleUpScript.GetProgress();
        mTextMesh.color = new Color(mTextMesh.color.r, mTextMesh.color.g, mTextMesh.color.b,alpha);
	}
}
