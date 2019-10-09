using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class ContrastProcessor : BaseProcessor
    {
        [SerializeField]
        [Range(0,30)]
        private float m_Contrast;
        [SerializeField]
        [Range(0, 1)]
        private float m_Level;

        public ContrastProcessor(Material material) : base(material)
        {
        }

        public ContrastProcessor()
        {
        }

        public override void SetupMaterial()
        {
            m_Material.SetFloat("_Contrast", m_Contrast);
            m_Material.SetFloat("_Level", m_Level);
        }
    }
}
