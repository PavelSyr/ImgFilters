using ich.ShaderProcess.Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.Views
{
    public class FilterView : BaseView
    {
        [SerializeField]
        private RawImage m_Image;

        [SerializeField]
        private SelectProcessBehaviour m_SelectProcess;

        protected override void AddListeners()
        {
            base.AddListeners();
            m_SelectProcess.OnProcessBehaviourChanged += OnProcessBehaviourChanged;
        }

        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            m_SelectProcess.OnProcessBehaviourChanged -= OnProcessBehaviourChanged;
        }

        private void OnProcessBehaviourChanged(ProcessBehaviour processBehaviour)
        {
            processBehaviour.AttachMaterial(m_Image);
        }
    }
}