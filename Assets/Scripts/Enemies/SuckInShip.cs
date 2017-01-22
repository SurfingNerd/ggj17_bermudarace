using Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckInShip : MonoBehaviour {

	public float edgeForce = 0.2f;
	public float centerForce = 0.6f;

	public float edgeTwist = 2f;
	public float centerTwist = 4f;

	private List<Player> trappedShips;
	private CircleCollider2D collider2d;

	// Use this for initialization
	void Start () {
		trappedShips = new List<Player>();
		collider2d = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update()
	{
		transform.Rotate(Vector3.forward, 180 * Time.deltaTime);

		foreach (Player player in trappedShips)
		{
			if (player == null) continue;

			Vector2 shipPos = player.transform.position;
			Vector2 normal = (((Vector2)transform.position + collider2d.offset) - shipPos);
			float distance = normal.magnitude;
			//Debug.Log("Maelstrom tracking ship, distance: " + distance);

			float effect = (collider2d.radius - distance) / collider2d.radius; // normed from 0 (at edge) to 1 (center)
			if (effect > 0)
			{
				float factor = Mathf.Lerp(edgeForce, centerForce, effect);
				float twistFactor = Mathf.Lerp(edgeTwist, centerTwist, effect);

				Vector3 twist = (Quaternion.AngleAxis(90, Vector3.back) * ((Vector3)normal)).normalized;
				Vector2 drag = normal.normalized * factor + (Vector2)twist * twistFactor; // suck + twist

				player.Input.AddForceModification(drag, factor);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if(player == null) player = other.GetComponentInParent<Player>();
			if (player != null) trappedShips.Add(player);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();
			if (player == null) player = other.GetComponentInParent<Player>();
			if (player != null) trappedShips.Remove(player);
		}
	}
}
