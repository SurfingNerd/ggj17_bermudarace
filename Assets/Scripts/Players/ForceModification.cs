using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceModification {

	public Vector2 vector;
	public float factor;

	public ForceModification(Vector2 vector, float factor)
	{
		this.vector = vector;
		this.factor = factor;
	}
}
