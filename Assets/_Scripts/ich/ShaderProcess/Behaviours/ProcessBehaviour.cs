using ich.ShaderProcess.Processors;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{
    public class ProcessBehaviour : MonoBehaviour
    {
        public event Action OnMainTexChanged;

        [SerializeField]
        protected Material m_Material;

        protected BaseProcessor m_BaseProcessor;
        private Texture2D _m_MainTexture;
        protected Texture2D m_MainTexture
        {
            get { return _m_MainTexture; }
            set
            {
                if (_m_MainTexture != value)
                {
                    _m_MainTexture = value;
                    if (OnMainTexChanged != null) OnMainTexChanged();
                }
            }
        }

        protected virtual void Awake()
        {
            Initicialize();
        }

        protected virtual void Initicialize()
        {
        }

        public void AttachMaterial(Graphic ui_graphic)
        {
            ui_graphic.material = m_Material;
            m_MainTexture = (Texture2D)ui_graphic.mainTexture;
        }

        public void AttachMaterial(Renderer renderer)
        {
            renderer.material = m_Material;
            m_MainTexture = (Texture2D)renderer.material.mainTexture;

        }

        public void AttachMaterial(out Material material)
        {
            material = m_Material;
            m_MainTexture = (Texture2D)material.mainTexture;
        }

        protected virtual void UpdateMaterial()
        {
            if (m_BaseProcessor != null)
            {
                m_BaseProcessor.SetupMaterial();
            }
        }
    }
}