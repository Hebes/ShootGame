using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;
/// <summary>
/// 射线检测
/// 推荐使用
/// 在 Unity 中查找目标的 3 种方法！（对撞机、物理、距离）
/// 3 Ways to Find Targets in Unity! (Collider, Physics, Distance)
/// 参考：https://www.youtube.com/watch?v=h9oEhVqGptU
/// </summary>
public class TargetingSystem_PhysicsOverlap : MonoBehaviour
{
    private float range = 60;

    public void Update_TargetingSystem_PhysicsOverlap(Enemy_Components enemy_Components)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(enemy_Components.Eneny_Transform.position, range);
        foreach (var item in collider2Ds)
        {
            if (item.TryGetComponent(out Player_Components player_Components))
                enemy_Components.Enemy_Target = player_Components.transform;
        }
    }
}
