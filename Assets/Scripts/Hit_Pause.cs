using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Hit_Pause : MonoBehaviour
{
    public float duration;

    private bool isfrozen;
    private float pending_freeze_dur;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pending_freeze_dur > 0 && !isfrozen)
        {
            StartCoroutine("do_freeze");
        }
    }

    public void Freeze()
    {
        pending_freeze_dur = duration;
    }

    IEnumerator do_freeze()
    {
        isfrozen = true;
        var original_timescale = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = original_timescale;
        pending_freeze_dur = 0;
        isfrozen = false;
    }

    public IEnumerator do_freeze_public(float dur)
    {
        isfrozen = true;
        var original_timescale = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = original_timescale;
        pending_freeze_dur = 0;
        isfrozen = false;
    }
}
