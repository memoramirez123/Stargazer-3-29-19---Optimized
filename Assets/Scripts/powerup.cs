using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class powerup
{
    [SerializeField]
    public string name;

    [SerializeField]
    public float duration;

    [SerializeField]
    public UnityEvent startAction;

    [SerializeField]
    public UnityEvent endAction;

    public void end()
    {
        if (endAction != null)
            endAction.Invoke();
    }

    public void start()
    {
        if (startAction != null)
            startAction.Invoke();
    }
}
