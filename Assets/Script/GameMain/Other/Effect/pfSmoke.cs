using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pfSmoke : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Push", 1);
    }

    private void Push()
    {
        PoolMgr.Instance.PushObj(name, gameObject);
    }
}
