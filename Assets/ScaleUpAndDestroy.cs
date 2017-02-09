using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpAndDestroy : MonoBehaviour {

    public float seconds = 3;
    public float maxScale = 5;
   
    private float livingTime;

    public AnimationCurve animationCurve;

    // Use this for initialization
    void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {

        livingTime += Time.deltaTime;
        float scale = GetProgress() * maxScale;


        transform.localScale = new Vector3(scale, scale, scale);
        if (livingTime >= seconds)
        {
            Destroy(gameObject);
            Debug.Log("destroyed");
        }
	}

    /// <summary>
    /// 0.0 - 0.9999, depends on living time.
    /// </summary>
    /// <returns></returns>
    public float GetProgress()
    {
        return animationCurve.Evaluate(livingTime / seconds);
    }
}
