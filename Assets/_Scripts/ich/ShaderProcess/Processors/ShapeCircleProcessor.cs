using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class ShapeCircleProcessor : BaseProcessor
    {
        [SerializeField]
        [Range(0, 1)]
        public float Radius = 0.1f;

        public ShapeCircleProcessor(Material material):base(material)
        {
        }

        public ShapeCircleProcessor()
        {
        }

        public override void SetupMaterial()
        {
            m_Material.SetFloat("_Radius", Radius);
        }
    }
}

