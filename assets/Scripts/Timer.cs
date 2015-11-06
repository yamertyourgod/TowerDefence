using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public delegate void TimerDelegate();

    public event TimerDelegate Done;
	// Use this for initialization

	
	// Update is called once per frame
    public static Timer AddTimer(float sec)
    {
        var go = new GameObject("timer");
        var timer = go.AddComponent<Timer>();
        timer.StartTimer(sec);
        timer.Done += ()=>Destroy(timer.gameObject);
        return timer;
    }
    void StartTimer(float sec)
    {
        StartCoroutine(TimerCoroutine(sec));
    }
    private IEnumerator TimerCoroutine(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (Done != null) Done();
    }


}
