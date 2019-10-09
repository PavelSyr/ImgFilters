using ich.ShaderProcess.Processors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{
    public class CannyProcessBehaviour : ProcessBehaviour
    {
        private CannyProcessor m_Processor;

        [SerializeField]
        private Slider m_Slider_Alpha;

        public float Alpha
        {
            get { return m_Processor.Alpha; }
            set
            {
                if (m_Processor.Alpha != value)
                {
                    m_Processor.Alpha = value;
                    UpdateMaterial();
                }
            }
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new CannyProcessor(m_Material);
            m_BaseProcessor = m_Processor;
            m_Slider_Alpha.value = m_Processor.Alpha;
        }

        public void OnAlphaValueChange(float value)
        {
            Alpha = value;
        }
    }
}