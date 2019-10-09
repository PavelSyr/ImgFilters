using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class LogCorrectionProcessor : BaseProcessor
    {
        [SerializeField]
        [Range(0.0f, 1f)]
        public float Offset = 0f;

        [SerializeField]
        [Range(0.0f, 2f)]
        public float Multiplicator;

        public LogCorrectionProcessor(Material material):base(material)
        {
        }

        public LogCorrectionProcessor()
        {
        }

        public override void SetupMaterial()
        {
            m_Material.SetFloat("_Offset", Offset);
            m_Material.SetFloat("_Multiplicator", Multiplicator);
        }
    }
}

