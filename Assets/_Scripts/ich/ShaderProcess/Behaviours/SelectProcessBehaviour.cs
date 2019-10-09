using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ich.ShaderProcess.Behaviours
{
    public class SelectProcessBehaviour : MonoBehaviour
    {
        public event Action<ProcessBehaviour> OnProcessBehaviourChanged;
        private ProcessBehaviour m_ProcessBehaviour;

        [SerializeField]
        private Dropdown m_Dropdown;

        private void Start()
        {
            //fill dropdown
            List<string> options = new List<string>();
            foreach (Transform child in transform)
            {
                options.Add(child.name);
            }
            m_Dropdown.AddOptions(options);
            m_Dropdown.onValueChanged.AddListener(Dropdown_OnValueChanged);
        }

        private void ProcessBehaviourChanged()
        {
            if (OnProcessBehaviourChanged != null)
                OnProcessBehaviourChanged(m_ProcessBehaviour);
        }

        private void Dropdown_OnValueChanged(int index)
        {
            var option = m_Dropdown.options[index];
            var child = transform.Find(option.text);

            if (child != null)
            {
                var tmp = child.GetComponent<ProcessBehaviour>();

                if (tmp != m_ProcessBehaviour)
                {
                    if (m_ProcessBehaviour != null)
                        m_ProcessBehaviour.gameObject.SetActive(false);

                    m_ProcessBehaviour = child.GetComponent<ProcessBehaviour>();

                    child.gameObject.SetActive(true);

                    StartCoroutine(OnNextFrame(ProcessBehaviourChanged));
                }
            }
        }

        private IEnumerator OnNextFrame(Action callback)
        {
            yield return null;
            if (callback != null)
                callback();
        }
    }
}