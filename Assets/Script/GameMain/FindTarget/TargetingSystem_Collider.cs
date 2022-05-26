using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

/// <summary>
/// 对撞机
/// 碰撞检测
/// 在 Unity 中查找目标的 3 种方法！（对撞机、物理、距离）
/// 3 Ways to Find Targets in Unity! (Collider, Physics, Distance)
/// 参考：https://www.youtube.com/watch?v=h9oEhVqGptU
/// </summary>
public class TargetingSystem_Collider : MonoBehaviour
{
    private Enemy_Components enemy_Components;

    private void Awake() => enemy_Components = transform.Get_Component_Up<Enemy_Components>();

    private void Start() { }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == ETags.Player.ToString())
            enemy_Components.Enemy_Target = collision.transform;
    }
}
