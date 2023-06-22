using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour, ITimeScaler
{
    private List<ITimeScalable> scalables = new List<ITimeScalable>();

    public void Add(ITimeScalable scalable)
    {
        scalables.Add(scalable);
    }

    public void Remove(ITimeScalable scalable)
    {
        scalables.Remove(scalable);
    }

    private void Update()
    {
        if (scalables.Count == 0)
        {
            Time.timeScale = 1;
            return;
        }
        ITimeScalable timeScalable = scalables[0];
        for(int i = 1; i < scalables.Count;i++)
        {
            if(timeScalable.Priority <= scalables[i].Priority)
            {
                timeScalable = scalables[i];
            }
        }
        Time.timeScale = timeScalable.Scale;
    }
}