using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    public interface IProcessor
    {
        Texture SrcTexture { get; set; }
        Texture DstTexture { get; set; }
        bool IsEnabled { get; set; }
        void Execute();
        void SetupMaterial();
        void Dispose();
    }
}
