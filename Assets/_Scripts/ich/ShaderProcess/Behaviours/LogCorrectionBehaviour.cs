using ich.ShaderProcess.Processors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{

    public class LogCorrectionBehaviour : ProcessBehaviour
    {
        private LogCorrectionProcessor m_Processor;

        [SerializeField]
        private Slider m_Slider_Offset;
        [SerializeField]
        private Slider m_Slider_Multiplicator;

        public float Offset
        {
            get { return m_Processor.Offset; }
            set
            {
                if (m_Processor.Offset != value)
                {
                    m_Processor.Offset = value;
                    UpdateMaterial();
                }
            }
        }

        public float Multiplicator
        {
            get { return m_Processor.Multiplicator; }
            set
            {
                if (m_Processor.Multiplicator != value)
                {
                    m_Processor.Multiplicator = value;
                    UpdateMaterial();
                }
            }
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new LogCorrectionProcessor(m_Material);
            m_BaseProcessor = m_Processor;
            m_Slider_Offset.value = Offset;
            m_Slider_Multiplicator.value = Multiplicator;
        }

        public void OnOffsetChanged(float value)
        {
            Offset = value;
        }

        public void OnMultiplicatorChanged(float value)
        {
            Multiplicator = value;
        }
    }
}