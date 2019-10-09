using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.ShaderProcess.Processors
{
    public class SequenceProcessor : IProcessor
    {
        private IProcessor[] m_Processors;
        public Texture SrcTexture { get; set; }
        public Texture DstTexture { get; set; }
        public bool IsEnabled { get; set; }

        public SequenceProcessor(params IProcessor[] processors)
        {
            m_Processors = processors;
        }

        public void Dispose()
        {
            m_Processors = null;
        }

        public void Execute()
        {
            Texture tmp = SrcTexture;
            for (int i = 0; i < m_Processors.Length; i++)
            {
                var proc = m_Processors[i];
                if (proc.IsEnabled)
                {
                    proc.SrcTexture = tmp;
                    proc.Execute();
                    tmp = proc.DstTexture;
                }
            }
            DstTexture = tmp;
        }

        public void SetupMaterial()
        {
            for (int i = 0; i < m_Processors.Length; i++)
            {
                m_Processors[i].SetupMaterial();
            }
        }
    }
}
