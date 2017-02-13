
using System;
using Players;
using UnityEngine;

namespace Boosters
{
    public class WaveSpeedBooster : BoosterBase
    {
        Rigidbody2D body;
        private readonly float force = 100;

        protected override void Init(Player player)
        {
            body = player.GetComponent<Rigidbody2D>();
        }

        protected override void Execute(Player player)
        {
            Vector2 addedForce = Vector2.right * force;
            body.AddForce(addedForce);
        }
    }
}
