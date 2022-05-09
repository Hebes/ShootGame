using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Move : MonoBehaviour,IMove
{
    private float player_MoveSpeed = 50;
    private Vector3 velocityVector;

    public void SetMove(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void Update()
    {
        //float moveX = 0f;
        //float moveY = 0f;

        //if (input.getkey(keycode.w)) moveY = +1f;
        //if (input.getkey(keycode.s)) moveY = -1f;
        //if (input.getkey(keycode.a)) moveX = -1f;
        //if (input.getkey(keycode.d)) moveX = +1f;

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;

        if (isIdle)
        {
            Player_Manager.Instance.Player_Animator.SetBool(Config_Player.player_Animator_Bool, false);
            Player_Manager.Instance.Player_MiniMapIcon_Animator.SetBool(Config_Player.player_Animator_Bool, false);
        }
        else
        {
            Player_Manager.Instance.Player_Animator.SetBool(Config_Player.player_Animator_Bool, true);
            Player_Manager.Instance.Player_MiniMapIcon_Animator.SetBool(Config_Player.player_Animator_Bool, true);
        }

        GetComponent<IMove>().SetMove(moveVector);
    }

    private void FixedUpdate()
    {
        Player_Manager.Instance.Player_Rigidbody2D.velocity = velocityVector * player_MoveSpeed;//刚体运动
    }
}
