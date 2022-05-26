using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;


/// <summary>
/// 距离
/// 列表循环  推荐使用
/// 在 Unity 中查找目标的 3 种方法！（对撞机、物理、距离）
/// 3 Ways to Find Targets in Unity! (Collider, Physics, Distance)
/// 参考：https://www.youtube.com/watch?v=h9oEhVqGptU
/// </summary>
public class TargetingSystem_ListDistance : MonoBehaviour
{
    private void Start() { }

    //public static List<Player_Components> playerList = new List<Player_Components>();

    //private float range = 60f;
    //private Enemy_Components enemy_Components;

    //private void Awake() => enemy_Components = transform.Get_Component_Up<Enemy_Components>();
    //private void Update()
    //{
    //    foreach (Player_Components player in playerList)
    //    {
    //        if (Vector3.Distance(transform.position, player.transform.position) < range)
    //        {
    //            // Enemy within range
    //            enemy_Components.Enemy_Target = player.Player_Transform;
    //        }
    //    }
    //}
}
