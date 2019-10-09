using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class BaseProcessor : IProcessor
    {
        public static RenderTexture Blit(Texture src, Material mat)
        {
            RenderTexture rTex = new RenderTexture(src.width, src.height, 0);
            Graphics.Blit(src, rTex, mat);
            return rTex;
        }

        [SerializeField]
        protected bool m_Enabled = true;
        [SerializeField]
        protected Material m_Material;
        public Texture SrcTexture { get; set; }
        public Texture DstTexture { get; set; }
        public bool IsEnabled { get { return m_Enabled; } set { m_Enabled = value; } }

        public BaseProcessor(Material material)
        {
            m_Material = material;
        }

        public BaseProcessor()
        {
        }

        public virtual void SetupMaterial()
        {

        }

        public virtual void Execute()
        {
            DstTexture = Blit(SrcTexture, m_Material);
        }

        public void Dispose()
        {
            SrcTexture = null;
            DstTexture = null;
            m_Material = null;
        }
    }
}

