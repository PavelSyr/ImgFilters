using ich.ShaderProcess.Processors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{

    public class GammaCorrectionBehaviour : ProcessBehaviour
    {
        private GammaCorrectionProcessor m_Processor;

        [SerializeField]
        private Slider m_Slider_Gamma;

        public float Gamma
        {
            get { return m_Processor.Gamma; }
            set
            {
                if (m_Processor.Gamma != value)
                {
                    m_Processor.Gamma = value;
                    UpdateMaterial();
                }
            }
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new GammaCorrectionProcessor(m_Material);
            m_BaseProcessor = m_Processor;
            m_Slider_Gamma.value = Gamma;
        }

        public void OnGammaChanged(float value)
        {
            Gamma = value;
        }
    }
}