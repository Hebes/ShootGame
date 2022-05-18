using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tool;

/// <summary>
/// 距离处理管理者
/// </summary>
public class Prefab_DistanceHandler : SingletonMono_Temp<Prefab_DistanceHandler>
{

    private TextMeshPro distanceText;
    
    protected override void Awake()
    {
        distanceText = transform.Find_Child<TextMeshPro>(Config_Common.sign_pf_TargeText);
    }

    private void Update()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag(ETags.Player.ToString()).GetComponent<Player_Components>().Player_Transform.position;
        //考虑精灵大小，因此设置为3F，具体自行参考
        int distance = Mathf.RoundToInt(Vector3.Distance(transform.position, playerPos) / 3f);
        distanceText.text = distance + "M";
    }
}
