using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tool;

/// <summary>
/// 
/// </summary>
public class Prefab_DistanceHandler : MonoBehaviour
{

    private TextMeshPro distanceText;

    private void Awake()
    {
        distanceText = transform.Find_Child<TextMeshPro>(Config_Common.TargetSystem_Prefab_TargeText);
    }

    private void Update()
    {
        Vector3 thisPrefabPos = transform.position;
        Vector3 playerPos = Player_Manager.Instance.Player_Transform.position;
        //考虑精灵大小，因此设置为3F，具体自行参考
        int distance = Mathf.RoundToInt(Vector3.Distance(thisPrefabPos, playerPos) / 3f);
        distanceText.text = distance + "M";
    }
}
