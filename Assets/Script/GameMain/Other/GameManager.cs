using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Tool;



public class GameManager : SingletonMono_Temp<GameManager>
{
    private Player_Components player;
    public Transform[] Npc;
    private int npcIndex;
    public Transform tfgo;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player").GetComponent<Player_Components>();
    }
    private void Start()
    {
        ChatBubble.Create(player.Player_ChatBubble_transform, new Vector3(2f, 6f), IconType.Neutral, "Here is some text!");
        FunctionPeriodic.Create(() => {
            Transform npcTransform = Npc[npcIndex].Find_Child<Transform>("Enemy_ChatBubble");
            npcIndex = (npcIndex + 1) % Npc.Length;
            string message = GetRandomMessage();

            IconType[] iconArray =
                new IconType[] { IconType.Happy, IconType.Neutral, IconType.Angry };
            IconType icon = iconArray[UnityEngine.Random.Range(0, iconArray.Length)];

            ChatBubble.Create(npcTransform, new Vector3(3, 8), icon, message);
        }, 1.5f);
    }

    private string GetRandomMessage()
    {
        string[] messageArray = new string[] {
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

        return messageArray[UnityEngine.Random.Range(0, messageArray.Length)];
    }
}
