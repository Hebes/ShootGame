using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Talk : MonoBehaviour
{
    private Player_Components player_Components;
    private void Awake() => player_Components = GetComponent<Player_Components>();
    private void Start() 
        => ChatBubble.Create(player_Components.Player_ChatBubble_transform, new Vector3(2f, 6f), IconType.Neutral, "Here is some text!");

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UI_InputWindow.Instance.Show_Static(
                "Say what?",
                "",
                "abcdefghijklmnopqrstuvxywz! ABCDEFGHIJKLMNOPQRSTUVXYWZ,.?",
                50,
                () => { },
                (string inputText) =>
                {
                    ChatBubble.Create(player_Components.Player_ChatBubble_transform, new Vector3(3, 8), IconType.Happy, inputText);
                }
            );
        }

        //TDUO 此处优化显示弹出的数据 以下为暂时写法
    }
}
