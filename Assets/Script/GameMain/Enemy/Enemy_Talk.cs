using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Talk : MonoBehaviour
{
    private float timer;

    public void Update_Enemy_Talk(Enemy_Components enemy_Components)
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            timer = 0;
            IconType[] iconArray = new IconType[] { IconType.Happy, IconType.Neutral, IconType.Angry };
            IconType icon = iconArray[Random.Range(0, iconArray.Length)];
            ChatBubble.Create(enemy_Components.Enemy_ChatBubble, new Vector3(5f, 0f), icon, Enemy_message());
        }
    }

    /// <summary>
    /// 敌人发送的消息
    /// </summary>
    /// <returns></returns>
    private string Enemy_message()
    {
        string[] messageArray = new string[]
        {
            "Hello World!",
            "Good morning!",
            "Subscribe to Code Monkey!",
            "Check out Code Monkey on Steam!",
            "This is a really excellent place!",
            "I'm having so much fun walking around!",
            "I'm really sad about something",
            "I heard someone said something!",
            "I was wondering why the ball was getting bigger, then it hit me",
            "Did you hear about the guy whose whole left side was cut off? He’s all right now",
            "I'm reading a book about anti-gravity. It's impossible to put down!",
            "Don't trust atoms. They make up everything!",
            "What did the pirate say on his 80th birthday? AYE MATEY",
            "What’s Forrest Gump’s password? 1forrest1",
            "Two guys walk into a bar, the third one ducks.",
            "How many tickles does it take to make an octopus laugh? Ten-tickles",
            "Our wedding was so beautiful, even the cake was in tiers.",
            "What do you call a dinosaur with a extensive vocabulary? A thesaurus."
        };
        return messageArray[Random.Range(0, messageArray.Length)];
    }
}
