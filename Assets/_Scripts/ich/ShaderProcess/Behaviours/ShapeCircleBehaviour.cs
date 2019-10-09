using ich.ShaderProcess.Processors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{

    public class ShapeCircleBehaviour : ProcessBehaviour
    {
        private const float EPS = 0.0001f;
        private ShapeCircleProcessor m_Processor;

        [SerializeField]
        private Slider m_Slider_Radius;

        public float Radius
        {
            get { return m_Processor.Radius; }
            set
            {
                if (m_Processor.Radius != value)
                {
                    m_Processor.Radius = value;
                    UpdateMaterial();
                }
            }
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new ShapeCircleProcessor(m_Material);
            m_BaseProcessor = m_Processor;
            m_Slider_Radius.value = Radius;
        }

        public void OnRadiusChanged(float value)
        {
            Radius = value;
        }
    }
}