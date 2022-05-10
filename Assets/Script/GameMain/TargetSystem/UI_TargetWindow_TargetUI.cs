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
        textMeshPro = transform.Find_Child<TextMeshProUGUI>(Config_Common.UI_TargetWindow_Prefab_TargeText);
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public TargetSystem.Ping Ping
    {
        set { ping = value; }
    }
    private void Update()
    {
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
            Vector3 playerPos = Player_Manager.Instance.Player_Transform.position;
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
