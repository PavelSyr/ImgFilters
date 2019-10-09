using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ich.Views
{
    public class BaseView : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Initialize();
            AddListeners();
        }

        protected virtual void OnDestroy()
        {
            RemoveListeners();
            Dispose();
        }

        protected virtual void Initialize()
        {

        }

        protected virtual void AddListeners()
        {

        }

        protected virtual void Dispose()
        {

        }

        protected virtual void RemoveListeners()
        {

        }
    }
}
