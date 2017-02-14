using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        public GameObject progressElement;

        private float m_progress;

        public void SetProgress(float progress)
        {
            if (progress > 1)
            {
                progress = 1;
            }
            m_progress = progress;

            if (progressElement != null)
            {
                progressElement.transform.localScale = new Vector3(m_progress, progressElement.transform.localScale.y,progressElement.transform.localScale.z);
            }
        }

        // Use this for initialization
        void Start()
        {
            SetProgress(0);
        }
    }
}
