using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    [System.Serializable]
    public class GrayscaleProcessor : BaseProcessor
    {
        public GrayscaleProcessor()
        {
        }

        public GrayscaleProcessor(Material material) : base(material)
        {
        }
    }
}