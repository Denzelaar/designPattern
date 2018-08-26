using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : Singleton<AdsManager>{

    const float _TIMEOUTDURATION = 300;
    bool _inAdCoolDown;

    public override void Init()
    {
        base.Init();

    }

    public void StartAd()
    {
         _StartAdTimer();
        if (!Advertisement.IsReady())
        {
            Debug.Log("Ads not ready for default placement");
            return;
        }

        Advertisement.Show();
    }

    void _StartAdTimer()
    {
        Invoke("_ResetTimeout", _TIMEOUTDURATION);
        _inAdCoolDown = true;
    }

    void _ResetTimeout()
    {
        _inAdCoolDown = false;
    }

    public bool InAdCoolDown
    {
        get
        {
            return _inAdCoolDown;
        }
    }
}
