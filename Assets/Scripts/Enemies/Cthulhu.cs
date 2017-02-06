using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cthulhu : MonoBehaviour {

	public float appearDuration = 3f;
	public float moveAmplitude = 1f;
	public float moveSpeed = 1f;

	public GameObject appearVortex, topVortex, bottomVortex;

	private enum STAGE { START, APPEAR, HUNT, CATCH }
	private STAGE stage = STAGE.START;

	float stageTime;

	private GameObject sprite;
	private BoxCollider2D boxCollider;

	public Vector3 spriteScale = new Vector3(2f, 1.5f, 1f);
	public float screenShakeStrength = 0.5f;

	private Vector3 cachedCamPos;
	private Vector3 targetSpritePos;

	// Use this for initialization
	void Start () {
		sprite = gameObject.transform.Find("Sprite").gameObject;
		boxCollider = gameObject.GetComponent<BoxCollider2D>();

		sprite.transform.localScale = Vector3.zero;
		stageTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch(stage)
		{
			case STAGE.START:
				appearVortex.transform.localScale = Vector3.zero;
				topVortex.transform.localScale = Vector3.zero;
				bottomVortex.transform.localScale = Vector3.zero;

				targetSpritePos = sprite.transform.position;
				sprite.transform.position = targetSpritePos - new Vector3(1f, 0, 0);

				cachedCamPos = Camera.main.transform.position;

				stage = STAGE.APPEAR;
				break;

			case STAGE.APPEAR:
				stageTime += Time.deltaTime;
				if (stageTime < appearDuration)
				{
					float t = stageTime / appearDuration;

					appearVortex.transform.localScale = Vector3.one * (1f - Mathf.Pow(1 - t, 2));
					topVortex.transform.localScale = Vector3.one * (Mathf.Pow(t, 2));
					bottomVortex.transform.localScale = Vector3.one * (Mathf.Pow(t, 2));

					sprite.transform.localScale = spriteScale * (0.5f - 0.5f * Mathf.Cos(Mathf.PI * t));
					sprite.transform.position = targetSpritePos - new Vector3(1f - 1f * t, 0, 0);
					
					// screenshake
					Camera.main.transform.position = cachedCamPos + screenShakeStrength * (0.5f - Mathf.Abs(t - 0.5f)) * new Vector3(Mathf.Sin(50 * t), Mathf.Sin(63 * t));
                    appearDuration = appearDuration / 2; //each appear is faster
                }
				else
				{
					appearVortex.transform.localScale = Vector3.one;
					topVortex.transform.localScale = Vector3.one;
					bottomVortex.transform.localScale = Vector3.one;

					sprite.transform.localScale = spriteScale;
					sprite.transform.position = targetSpritePos;

					stageTime = 0;

					boxCollider.enabled = true;
					stage = STAGE.HUNT;
				}
				break;

			case STAGE.HUNT:
				stageTime += Time.deltaTime;
				sprite.transform.localPosition = new Vector3(moveAmplitude * Mathf.Sin(moveSpeed * stageTime), 0f, -1f);
				break;

			case STAGE.CATCH:
				// ?
				break;
		}
	}

	void switchStage(STAGE newStage)
	{

	}
}
