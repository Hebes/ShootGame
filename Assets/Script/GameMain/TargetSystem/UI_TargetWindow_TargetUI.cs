using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Tool;

public class UI_TargetWindow_TargetUI : MonoBehaviour
{
    private TargetSystem.Ping ping;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private Image image;

    private void Awake()
    {
        textMeshPro = transform.Find_Child<TextMeshProUGUI>(Config_Common.UI_SignWindow_pf_TargeText);
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    /// <summary>
    /// 设置ping的图标和颜色
    /// </summary>
    /// <param name="ping"></param>
    public void Ping(TargetSystem.Ping ping)
    {
        this.ping = ping;

        switch (ping.GetPingType)
        {
            default:
            case TargetSystem.Ping.Type.Move:
                break;
            case TargetSystem.Ping.Type.Enemy:
                image.sprite = GameManager.Instance.pingEnemySprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
            case TargetSystem.Ping.Type.Item:
                image.sprite = ItemIdentifier.Instance.GetItemSprite(ping.GetItemType());
                textMeshPro.color = ItemIdentifier.Instance.GetItemColor(ping.GetItemType());
                break;
            case TargetSystem.Ping.Type.Looting:
                image.sprite = GameManager.Instance.pingLootingSprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
            case TargetSystem.Ping.Type.Attacking:
                image.sprite = GameManager.Instance.pingAttackingSprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
            case TargetSystem.Ping.Type.GoingHere:
                image.sprite = GameManager.Instance.pingGoingHereSprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
            case TargetSystem.Ping.Type.Defend:
                image.sprite = GameManager.Instance.pingDefendSprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
            case TargetSystem.Ping.Type.Watching:
                image.sprite = GameManager.Instance.pingWatchingSprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
            case TargetSystem.Ping.Type.Enemyseen:
                image.sprite = GameManager.Instance.pingEnemyseenSprite;
                textMeshPro.color = GameManager.Instance.pingEnemyColor;
                break;
           
        }

        switch (ping.GetPingType)
        {
            default:
            case TargetSystem.Ping.Type.Move:
                break;
            case TargetSystem.Ping.Type.Enemy:
                
                break;
            case TargetSystem.Ping.Type.Item:
                
                break;
        }

        ping.OnDestroyed += delegate (object sender, System.EventArgs e)
        {
            Destroy(gameObject);
        };
    }
    private void Update()
    {
        //离开摄像机显示的时候关闭图标和距离
        Vector2 pingScreenCoordinates = Camera.main.WorldToScreenPoint(ping.GetPosition);
        bool isOffScreen =
        pingScreenCoordinates.x > Screen.width ||
            pingScreenCoordinates.x < 0 ||
            pingScreenCoordinates.y > Screen.height ||
            pingScreenCoordinates.y < 0;
        image.enabled = isOffScreen;
        textMeshPro.enabled = isOffScreen;

        if (isOffScreen)
        {
            //Update UI position
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (ping.GetPosition - fromPosition).normalized;//targetPos为鼠标右击的点的坐标
            float uiRadius = 500f;//调节TargetUI的远近 还需调节 canvas的match
            rectTransform.anchoredPosition = dir * uiRadius;

            //Update Distance text
            Vector3 playerPos = GameObject.FindGameObjectWithTag(Config_Tags.Player).GetComponent<Player_Components>(). Player_Transform.position;
            //考虑精灵大小，因此设置为3F，具体自行参考
            int distance = Mathf.RoundToInt(Vector3.Distance(ping.GetPosition, playerPos) / 3f);
            textMeshPro.text = distance + "M";
        }
        else
        {
            // Ping is on screen Ping在屏幕上  
            // UI element hidden UI元素被隐藏  
        }

    }
}
