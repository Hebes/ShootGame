using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool;

public class Player_Move : MonoBehaviour, IMove
{
    private float player_MoveSpeed = 50;
    private Vector3 velocityVector;
    private Player_Components player_Components;

    private void Awake() => player_Components = GetComponent<Player_Components>();

    public void SetMove(Vector3 velocityVector) => this.velocityVector = velocityVector;

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(Config_Key.Key_UP)) moveY = +1f;
        if (Input.GetKey(Config_Key.Key_Down)) moveY = -1f;
        if (Input.GetKey(Config_Key.Key_Left)) moveX = -1f;
        if (Input.GetKey(Config_Key.Key_Right)) moveX = +1f;

        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;

        if (isIdle)
        {
            player_Components.Player_Animator.SetBool(Config_Player.player_Animator_Bool, false);
            player_Components.Player_MiniMapIcon_Animator.SetBool(Config_Player.player_Animator_Bool, false);
        }
        else
        {
            player_Components.Player_Animator.SetBool(Config_Player.player_Animator_Bool, true);
            player_Components.Player_MiniMapIcon_Animator.SetBool(Config_Player.player_Animator_Bool, true);
        }

        GetComponent<IMove>().SetMove(moveVector);
    }

    private void FixedUpdate() => player_Components.Player_Rigidbody2D.velocity = velocityVector * player_MoveSpeed;//刚体运动
}
