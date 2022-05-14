using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Tool;

public class Testing : MonoBehaviour
{

    private void Awake()
    {
        EventCenter.Instance.AddEventListener<OnShootEvnentArgs>(Config_Player.player_Event_Shoot, Player_Weapen_OnShoot);
    }

    private void Player_Weapen_OnShoot(OnShootEvnentArgs arg0)
    {
        bool isCriticalHit = Random.Range(0, 100) < 30;

        Component_Helper.Show_pf_Damage(UtilsClass.GetMouseWorldPosition(), 300, isCriticalHit);
    }
}
