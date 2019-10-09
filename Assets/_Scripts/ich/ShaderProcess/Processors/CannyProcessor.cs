using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class CannyProcessor : BaseProcessor
    {
        [SerializeField]
        [Range(0, 1)]
        public float Alpha = 0.1f;

        public CannyProcessor()
        {
        }

        public CannyProcessor(Material material) : base(material)
        {
        }

        public override void SetupMaterial()
        {
            m_Material.SetFloat("_Alpha", Alpha);
        }
    }
}