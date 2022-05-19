using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tool;
public enum IconType
{
    Happy,//开心
    Neutral,//中立
    Angry,//愤怒
}
/// <summary>
/// Unity 中的简单聊天气泡！（聊天，NPC，多人）
/// Simple Chat Bubble in Unity! (Chat, NPC, Multiplayer)
/// 参考：https://www.youtube.com/watch?v=K13WnNL1OYM
/// </summary>
public class ChatBubble : MonoBehaviour
{
    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameManager.Instance.tfgo, parent);
        chatBubbleTransform.localPosition = localPosition;

        chatBubbleTransform.GetComponent<ChatBubble>().Setup(iconType, text);

        Destroy(chatBubbleTransform.gameObject, 5f);
    }


    [SerializeField] private Sprite happyIconSprite;
    [SerializeField] private Sprite neutralIconSprite;
    [SerializeField] private Sprite angryIconSprite;

    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();   
    }

    private void Setup(IconType iconType, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();//网格更新
        Vector2 textSize = textMeshPro.GetRenderedValues(false);//获得值呈现

        Vector2 padding = new Vector2(13f, 4f);
        backgroundSpriteRenderer.size = textSize + padding;

        Vector2 offset = new Vector3(-14f, 0f);    
        backgroundSpriteRenderer.transform.localPosition = new Vector2(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        iconSpriteRenderer.sprite = GetIconSprite(iconType);

        //TUDO 这里需要填写执行下面脚本
        //TextWriter.AddWriter_Static(textMeshPro, text, .5f, true, true, () => { });
    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Happy: return happyIconSprite;
            case IconType.Neutral: return neutralIconSprite;
            case IconType.Angry: return angryIconSprite;
        }
    }

    private void Push()
    {
        PoolMgr.Instance.PushObj(name, gameObject);
    }
}
