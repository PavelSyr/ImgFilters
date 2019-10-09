using ich.ShaderProcess.Processors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{

    public class WhiteBalanceBehaviour : ProcessBehaviour
    {
        private const float EPS = 0.0001f;
        private GrayWorldWhiteBalaceProcesssor m_Processor;

        [SerializeField]
        private Slider m_Slider_R;

        [SerializeField]
        private Slider m_Slider_G;

        [SerializeField]
        private Slider m_Slider_B;

        [SerializeField]
        private bool m_ManualMode;

        public float AvgColor_r
        {
            get { return m_Processor.AvgColor.r; }
            set
            {
                if (m_Processor.AvgColor.r != value)
                {
                    m_Processor.AvgColor.r = value;
                    UpdateMaterial();
                }
            }
        }

        public float AvgColor_g
        {
            get { return m_Processor.AvgColor.g; }
            set
            {
                if (m_Processor.AvgColor.g != value)
                {
                    m_Processor.AvgColor.g = value;
                    UpdateMaterial();
                }
            }
        }

        public float AvgColor_b
        {
            get { return m_Processor.AvgColor.b; }
            set
            {
                if (m_Processor.AvgColor.b != value)
                {
                    m_Processor.AvgColor.b = value;
                    UpdateMaterial();
                }
            }
        }

        private void OnDestroy()
        {
            OnMainTexChanged -= OnMainTexChanged_Callback;
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new GrayWorldWhiteBalaceProcesssor(m_Material);
            m_Processor.ManualMode = m_ManualMode;
            m_BaseProcessor = m_Processor;
            m_Slider_R.value = AvgColor_r;
            m_Slider_G.value = AvgColor_g;
            m_Slider_B.value = AvgColor_b;

            m_Slider_R.gameObject.SetActive(m_ManualMode);
            m_Slider_G.gameObject.SetActive(m_ManualMode);
            m_Slider_B.gameObject.SetActive(m_ManualMode);

            if (!m_ManualMode)
            {
                OnMainTexChanged += OnMainTexChanged_Callback;
            }
        }

        private void OnMainTexChanged_Callback()
        {
            m_Processor.SrcTexture = m_MainTexture;
            m_Processor.SetupMaterial();
        }

        protected override void UpdateMaterial()
        {
            base.UpdateMaterial();
            m_Slider_R.value = AvgColor_r;
            m_Slider_G.value = AvgColor_g;
            m_Slider_B.value = AvgColor_b;
        }

        public void OnRChanged(float value)
        {
            AvgColor_r = value;
        }

        public void OnGChanged(float value)
        {
            AvgColor_g = value;
        }

        public void OnBChanged(float value)
        {
            AvgColor_b = value;
        }
    }
}