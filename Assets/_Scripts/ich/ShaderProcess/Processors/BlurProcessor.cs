using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class BlurProcessor : BaseProcessor
    {
        //protected Material BlurX { get { return m_Material; } }
        [SerializeField]
        protected Material BlurY;

        public BlurProcessor()
        {
        }

        public BlurProcessor(Material blurX, Material blurY) : base(blurX)
        {
            BlurY = blurY;
        }

        public override void Execute()
        {
            //blur x
            base.Execute();
            //blur y
            DstTexture = Blit(DstTexture, BlurY);
        }
    }
}