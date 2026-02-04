using System.Collections.Generic;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    public static KillTracker instance;

    private Queue<float> killTimes = new Queue<float>();
    public float trackingWindow = 10f; // seconds

    void Awake()
    {
        instance = this;
    }

    public void RegisterKill()
    {
        killTimes.Enqueue(Time.time);
    }

    public float GetKillsPerSecond()
    {
        Cleanup();
        return killTimes.Count / trackingWindow;
    }

    void Cleanup()
    {
        while (killTimes.Count > 0 && Time.time - killTimes.Peek() > trackingWindow)
        {
            killTimes.Dequeue();
        }
    }
}
