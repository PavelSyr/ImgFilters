using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class LinearContrastProcessor : BaseProcessor
    {
        [SerializeField]
        [Range(0, 30)]
        public float Contrast = 2;
        [SerializeField]
        [Range(0, 1)]
        public float Min = 0.1f;
        [SerializeField]
        [Range(0, 1)]
        public float Max = 0.9f;

        public LinearContrastProcessor()
        {
        }

        public LinearContrastProcessor(Material material) : base(material)
        {
        }

        public override void SetupMaterial()
        {
            m_Material.SetFloat("_Contrast", Contrast);
            m_Material.SetFloat("_Min", Min);
            m_Material.SetFloat("_Max", Max);
        }
    }
}