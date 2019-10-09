using ich.ShaderProcess.Processors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{
    public class CDFProcessBehaviour : ProcessBehaviour
    {
        private AligementHistogrammProcessor m_Processor;

        private void OnDestroy()
        {
            OnMainTexChanged -= OnMainTexChanged_Callback;
        }

        protected override void Initicialize()
        {
            base.Initicialize();
            m_Processor = new AligementHistogrammProcessor(m_Material);
            m_BaseProcessor = m_Processor;
            OnMainTexChanged += OnMainTexChanged_Callback;
        }

        private void OnMainTexChanged_Callback()
        {
            m_Processor.SrcTexture = m_MainTexture;
            m_Processor.SetupMaterial();
        }
    }
}