using Players;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game;

namespace Treasures
{
    public class TreasureMechanics : MonoBehaviour
    {
        private int numTreasuresPickedUpTotal = 0;

        public int numTreasuresUntilApocalypse = 6;
        public AudioClip audioGotTreasure = null;


        public double secondsToElevateATreasure = 1.5;

        void Start()
        {
            //UnityEngine.Debug.Log("Starting up Mechanics");
            //InitAllTreasures();            
        }

        [HideInInspector]
        public HashSet<Treasure> Treasures = new HashSet<Treasure>();

        [HideInInspector]
        public Dictionary<Treasure, TreasureInteraction> TreasureInteractions = new Dictionary<Treasure, TreasureInteraction>();
        
        public void InitTreasureInteraction(TreasureInteraction interaction)
        {
            int count = Treasures.Count + 1;
            Treasure treasure = new Treasure();
            treasure.Name = "Treasure " + count.ToString();
            interaction.Treasure = treasure;
            Treasures.Add(treasure);
            TreasureInteractions.Add(treasure, interaction);
        }

        private GameObject GetParentRecursive(GameObject obj)
        {
            if (obj.transform.parent == null)
                return obj;

            return GetParentRecursive(obj.transform.parent.gameObject);
        }

        public void PlayerReturnedTreasures(Player player, IEnumerable<Treasure> treasures)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                Debug.Log("Player returned treasures. playing " + audioGotTreasure.name + " on " + audioSource.name);
                audioSource.PlayOneShot(audioGotTreasure);
            }
        }

        // TODO implement
        public void PlayerPickedUpTreasure(Player player, Treasure treasure)
        {

            numTreasuresPickedUpTotal++;
            Debug.Log("Player " + player.Name + " picked up treasure " + treasure.Name);
            Debug.Log("Total treasures picked up: " + numTreasuresPickedUpTotal + " (" + numTreasuresUntilApocalypse + " until Cthulhu apocalypse)");
            player.boardedTreasures.Add(treasure);

            if (numTreasuresPickedUpTotal >= numTreasuresUntilApocalypse)
            {
                TriggerApocalypse(treasure);
            }
            else
            {
                TreasurePickupConsequence(treasure);
            }

            treasure.LastOwner = player;

            player.Input.AddVelocityMod(treasure, 0.9);
            //player.
        }

        private void TriggerApocalypse(Treasure treasure)
        {
            GameController.Instance.TriggerApocalypse();
            

            // Trigger big Cthulhu - instakill on contact
            // Trigger a few Deep Ones on islands - slow down players if close
            // Trigger apocalypse camera filter
            // Trigger apocalypse music

            // Activate Death trigger at left screen edge
            // Trigger Camera autoscroll
        }

        private void TreasurePickupConsequence(Treasure treasure)
        {
            

            
           
            // Trigger some minor obstacle (at treasure position or random?)
            // Trigger music change? stinger or ramp up tension level
        }

        //public Player Player;
        //public HashSet<Treasure> CurrentlyHeldTreasures;
    }
}
