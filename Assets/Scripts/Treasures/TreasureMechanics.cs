﻿using Players;
using System.Collections.Generic;
using UnityEngine;

namespace Treasures
{
    public class TreasureMechanics : MonoBehaviour
    {
        private int numTreasuresPickedUpTotal = 0;

        public int numTreasuresUntilApocalypse = 6;


        [HideInInspector]
        public HashSet<Treasure> Treasures;

        [HideInInspector]
        public Dictionary<Treasure, TreasureInteraction> TreasureInteractions = new Dictionary<Treasure, TreasureInteraction>(); 

        public void InitAllTreasures()
        {
            //streategy: Loop throught all the TreasureInteraction and invent 1 Treasure for each.
            GameObject root = GetParentRecursive(gameObject);

            
            TreasureInteraction[] allKnownINteractions = root.GetComponentsInChildren<TreasureInteraction>();

            foreach (TreasureInteraction interaction in allKnownINteractions)
            {
                Treasure newTreasure = new Treasure();

                Treasures.Add(newTreasure);
                TreasureInteractions.Add(newTreasure, interaction);


            }

        }

        private GameObject GetParentRecursive(GameObject obj)
        {
            if (obj.transform.parent == null)
                return obj.transform.parent.gameObject;

            return GetParentRecursive(obj.transform.parent.gameObject);
        }


        // TODO implement
        public void PlayerPickedUpTreasure(Player player, Treasure treasure)
        {
            numTreasuresPickedUpTotal++;
            Debug.Log("Player " + player.Name + " picked up treasure " + treasure.Name);
            Debug.Log("Total treasures picked up: " + numTreasuresPickedUpTotal + " (" + numTreasuresUntilApocalypse + " until Cthulhu apocalypse)");

            if (numTreasuresPickedUpTotal >= numTreasuresUntilApocalypse)
            {
                TriggerApocalypse();
            }
            else
            {
                TreasurePickupConsequence();
            }
        }

        private void TriggerApocalypse()
        {
            Debug.Log("APOCALPYPSE INCOMING! (todo)");

            // Trigger big Cthulhu - instakill on contact
            // Trigger a few Deep Ones on islands - slow down players if close
            // Trigger apocalypse camera filter
            // Trigger apocalypse music

            // Activate Death trigger at left screen edge
            // Trigger Camera autoscroll
        }

        private void TreasurePickupConsequence()
        {
            Debug.Log("Treasure pickup consequence... (todo)");

            // Trigger some minor obstacle (at treasure position or random?)
            // Trigger music change? stinger or ramp up tension level
        }

        //public Player Player;
        //public HashSet<Treasure> CurrentlyHeldTreasures;
    }
}
