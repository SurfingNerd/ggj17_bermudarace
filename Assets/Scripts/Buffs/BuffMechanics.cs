using Players;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    public class BuffMechanics : MonoBehaviour
    {
        public GameObject SpeedUpHighlight;
       
        public double currentTreasurePickupSpeedModifier = 1;

        Player player;
        HashSet<BuffBase> mBuffs = new HashSet<BuffBase>();

        void Awake()
        {
            player = GetComponent<Player>();
        }

        

        void Update()
        {
            List<BuffBase> buffsToRemove = new List<BuffBase>();
            foreach (var buff in mBuffs)
            {
                buff.CalcTick(player);
                buff.durationLeft -= Time.deltaTime;

                if (buff.durationLeft <= 0)
                {
                    buffsToRemove.Add(buff);
                    buff.BuffEnded(player);
                }
            }

            foreach (var buffToRemove in buffsToRemove)
            {
                mBuffs.Remove(buffToRemove);
            }        
        }

        private void OnBuffAdded(BuffBase buff)
        {
            mBuffs.Add(buff);
            buff.InitBuff(player);
            if (SpeedUpHighlight)
            {
                GameObject highlight = Instantiate(SpeedUpHighlight);
                highlight.transform.position = player.transform.position;
            }
        }

        public void AddSpeedupPickupBuff()
        {
            TreasurePickupBuff buff = new TreasurePickupBuff();
            OnBuffAdded(buff);
             
        }

        public void AddSpeedupBuff()
        {
            SpeedUpBuff buff = new SpeedUpBuff();
            OnBuffAdded(buff);
        }
    }
}
