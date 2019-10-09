using ich.ShaderProcess.Processors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{

    public class LinearContrastBehaviour : ProcessBehaviour
    {
        private const float EPS = 0.0001f;
        private LinearContrastProcessor m_Processor;

        [SerializeField]
        private Slider m_Slider_Min;

        [SerializeField]
        private Slider m_Slider_Max;

        [SerializeField]
        private Slider m_Slider_Contrast;

        public float Contrast
        {
            get { return m_Processor.Contrast; }
            set
            {
                if (m_Processor.Contrast != value)
                {
                    m_Processor.Contrast = value;
                    UpdateMaterial();
                }
            }
        }

        public float Min
        {
            get { return m_Processor.Min; }
            set
            {
                if (m_Processor.Min != value)
                {
                    m_Processor.Min = value;
                    UpdateMaterial();
                }
            }
        }

        public float Max
        {
            get { return m_Processor.Max; }
            set
            {
                if (m_Processor.Max != value)
                {
                    m_Processor.Max = value;
                    UpdateMaterial();
                }
            }
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new LinearContrastProcessor(m_Material);
            m_BaseProcessor = m_Processor;
            m_Slider_Max.value = InverValue(m_Processor.Max);
            m_Slider_Min.value = m_Processor.Min;
            m_Slider_Contrast.value = m_Processor.Contrast;
        }

        public void OnContrastValueChange(float value)
        {
            Contrast = value;
        }

        public void OnMaxValueChange(float value)
        {
            //convert slider value to our scale;
            Max = InverValue(value);

            if (Max <= Min)
            {
                float n_min = Max - EPS;
                if (n_min < 0) n_min = 0;
                Min = n_min;
                m_Slider_Min.value = n_min;
            }
        }

        public void OnMinValueChange(float value)
        {
            Min = value;
            if (Min >= Max)
            {
                float n_max = Min + EPS;
                if (n_max > 1) n_max = 1;
                Max = n_max;
                //convert our scale to slider;
                m_Slider_Max.value = InverValue(n_max);
            }
        }

        private float InverValue(float value)
        {
            return 1 - value;
        }
    }
}