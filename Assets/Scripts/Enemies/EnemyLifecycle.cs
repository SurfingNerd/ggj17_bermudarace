using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyLifecycle : MonoBehaviour
    {
        //private enum State
        //{
        //    Appearing,
        //    Disappearing,
        //    Hidden,
        //    Active
        //}

        //State mCurrentState = State.Hidden;


        public double changeDuration = 5;
        public double activeDuration = 15;
        public double inactiveDuration = 15;


        private double currenTimepoint;

        void Start()
        {
            currenTimepoint = changeDuration +  activeDuration + changeDuration + (UnityEngine.Random.value * inactiveDuration);
        }

        void Update()
        {
            currenTimepoint += Time.deltaTime;

            if (currenTimepoint >= changeDuration + activeDuration + changeDuration + inactiveDuration)
            {

            }

            if ( currenTimepoint < changeDuration)
            {

            }
            //double totalDuration = activeDuration + inactiveDuration;
            //
        }

        void SetScale(double scale)
        {

        }
    }
}
