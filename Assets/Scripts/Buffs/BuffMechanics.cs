using Players;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Buffs
{
    public class BuffMechanics : MonoBehaviour
    {
        public GameObject SpeedUpHighlight;
       

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
            buff.InitBuff(player);
            
        }

        public void AddSpeedupBuff()
        {
            SpeedUpBuff buff = new SpeedUpBuff();
            mBuffs.Add(buff);
            OnBuffAdded(buff);

            if (SpeedUpHighlight)
            {
                GameObject highlight = Instantiate(SpeedUpHighlight);
                highlight.transform.position = player.transform.position;
            }
        }
    }
}
