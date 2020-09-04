using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleEnableBoss : MonoBehaviour
{

    public Text enable_boss_txt;
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.enable_Boss)
        {
            enable_boss_txt.text = "Boss Enabled";
        }
        else
        {
            enable_boss_txt.text = "Boss Disabled";
        }
    }

    public void Toggle_Boss_Enable()
    {
        GameManager.manager.enable_Boss = !GameManager.manager.enable_Boss;
    }
}
