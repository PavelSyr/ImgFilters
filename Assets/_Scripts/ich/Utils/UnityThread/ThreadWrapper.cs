using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ich.Utils.UnityThread
{
    public class ThreadWrapper : MonoBehaviour
    {
        public static void Create(Func<object> worker, Action<object> OnComplete, Action<string> OnError)
        {
            string threadName = "ThreadWrapper_" + worker.Method.Name;
            var go_tw = new GameObject(threadName);
            var tw = go_tw.AddComponent<ThreadWrapper>();
            tw.m_OnComplete = OnComplete;
            tw.m_OnError = OnError;
            tw.m_Worker = worker;
        }

        public enum State { idle, started, completed, error, finished }

        private State m_State;
        private Action<object> m_OnComplete;
        private Action<string> m_OnError;
        private Func<object> m_Worker;
        private string m_Error;
        private object m_result;

        private void Start()
        {
            new Thread(
                ()=>
                {
                    m_State = State.started;
                    try
                    {
                        m_result = m_Worker();
                        m_State = State.completed;
                    }
                    catch(Exception e)
                    {
                        m_Error = e.Message;
                        m_State = State.error;
                    }
                }
            ).Start();
        }

        private void Update()
        {
            if (m_State == State.completed)
            {
                if (m_OnComplete != null) m_OnComplete(m_result);
                Finish();
            }

            if (m_State == State.error)
            {
                if (m_OnError != null) m_OnError(m_Error);
                Finish();
            }
        }

        private void OnDestroy()
        {
            m_OnComplete = null;
            m_Worker = null;
        }

        private void Finish()
        {
            m_State = State.finished;
            Destroy(gameObject);
        }
    }
}