using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Raycast : BaseManager<Bullet_Raycast>
{
    public void Shoot(Vector3 shootPosition, Vector3 shootDirection)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(shootPosition, shootDirection);//发射点   发射距离
        if (raycastHit2D.collider != null)
        {
            // Hit!
            GunTarget target = raycastHit2D.collider.GetComponent<GunTarget>();
            if (target != null)
                target.Damage();
        }
    }
}
