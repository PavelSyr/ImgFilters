using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class GammaCorrectionProcessor : BaseProcessor
    {
        [SerializeField]
        [Range(0.01f, 5)]
        public float Gamma = 1f;

        public GammaCorrectionProcessor(Material material):base(material)
        {
        }

        public GammaCorrectionProcessor()
        {
        }

        public override void SetupMaterial()
        {
            m_Material.SetFloat("_Gamma", Gamma);
        }
    }
}

