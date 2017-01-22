using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class PulsingEffect  : MonoBehaviour
    {
        public float pulseRate = 1;
        public float minSize = 0.5f;
        //pulsing heart
        //m_currentState holds value between 0.5 - 1
        //private List<RectTransform> m_rectTransforms;

        bool isGoingUp;
        float m_currentState = 1;

        // Use this for initialization
        void Start()
        {
            //rectTransform = GetComponent<Tran;  

        }

        // Update is called once per frame
        void Update()
        {
            if (isGoingUp)
            {
                m_currentState += Time.deltaTime * pulseRate; //without modification we got exactly 60 hz
                if (m_currentState >= 1)
                {
                    isGoingUp = false;
                }
            }
            else
            {
                m_currentState -= Time.deltaTime * pulseRate;
                if (m_currentState <= minSize)
                {
                    isGoingUp = true;
                }
            }

            Vector3 newLocalScale = new Vector3(m_currentState, m_currentState, m_currentState);
            transform.localScale = newLocalScale;
        }
    }
}

