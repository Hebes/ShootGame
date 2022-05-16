using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tool;

/// <summary>
/// 距离处理管理者
/// </summary>
public class Prefab_DistanceHandler : SingletonMono<Prefab_DistanceHandler>
{

    private TextMeshPro distanceText;

    private new void Awake()
    {
        distanceText = transform.Find_Child<TextMeshPro>(Config_Common.sign_pf_TargeText);
    }

    private void Update()
    {
        Vector3 thisPrefabPos = transform.position;
        Vector3 playerPos = GameObject.FindGameObjectWithTag(Config_Tags.Player).GetComponent<Player_Components>().Player_Transform.position;
        //考虑精灵大小，因此设置为3F，具体自行参考
        int distance = Mathf.RoundToInt(Vector3.Distance(thisPrefabPos, playerPos) / 3f);
        distanceText.text = distance + "M";
    }
}
