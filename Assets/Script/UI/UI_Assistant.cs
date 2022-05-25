using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tool;
using CodeMonkey.Utils;

public enum UI_AssistantComponent
{
    messageText,
    talkingSound,
    message,
}

public class UI_Assistant : MonoBehaviour
{
    private Text messageText;
    private AudioSource talkingAudioSource;
    private TextWriter.TextWriterSingle textWriterSingle;

    private void Awake()
    {
        messageText = transform.Find_Child<Text>(UI_AssistantComponent.messageText.ToString());
        talkingAudioSource = transform.Find_Child<AudioSource>(UI_AssistantComponent.talkingSound.ToString());


        transform.Find_Child<Button_UI>(UI_AssistantComponent.message.ToString()).ClickFunc = () =>
        {
            if (textWriterSingle != null && textWriterSingle.IsActive)
            {
                // 当前活动TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {
                string[] messageArray = new string[] {
                    "WSAD移动",
                    "鼠标左击开火",
                    "快点来打僵尸吧！",
                    "游戏还没有完成！",
                    //"This is the assistant speaking, hello and goodbye, see you next time!",
                    //"Hey there!",
                    //"This is a really cool and useful effect",
                    //"Let's learn some code and make awesome games!",
                    //"Check out Battle Royale Tycoon on Steam!",
                };

                string message = messageArray[Random.Range(0, messageArray.Length)];
                StartTalkingSound();
                textWriterSingle = TextWriter.Instance.AddWriter_Static(messageText, message, .1f, true, true, StopTalkingSound);
            }
        };
    }

    private void Start()
    {
        StartTalkingSound();
        TextWriter.Instance.AddWriter_Static(messageText, "现在进入游戏吧！！", .2f, true, true, StopTalkingSound);
    }

    private void StartTalkingSound()
    {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
    }
}
