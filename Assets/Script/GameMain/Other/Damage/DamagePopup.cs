using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Tool;
using CodeMonkey.Utils;

/// <summary>
/// 损害弹出
/// 如何制作伤害弹出文本（Unity 教程） How to make Damage Popup Text (Unity Tutorial)
/// 参考：https://www.youtube.com/watch?v=iD1_JczQcFY
/// </summary>
public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1;
    private TextMeshPro textMesh;
    /// <summary>
    /// 消失的计时器
    /// </summary>
    private float disappearTimer;
    private Color textColor;

    private Vector3 moveVector;

    private static int sortingOrder;


    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="damageAmount">伤害</param>
    /// <param name="isCriticalHit">重击概率</param>
    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh = transform.Find_Child<TextMeshPro>(Config_Common.DamagePopup_pf);
        textMesh.SetText(damageAmount.ToString());//设置损伤文字

        disappearTimer = DISAPPEAR_TIMER_MAX;
        transform.localScale = Vector3.one;

        textMesh.fontSize = (!isCriticalHit) ? 47 : 50;
        textColor = (!isCriticalHit) ? UtilsClass.GetColorFromString("FFC500") : UtilsClass.GetColorFromString("FF2B00");

        textMesh.color = textColor;//顺便重置了透明通道值

        moveVector = new Vector3(1, 1) * 30f;//左右移动方向

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }


    private void Update()
    {
        DamageShow();
    }

    /// <summary>
    /// 显示伤害的方法
    /// </summary>
    private void DamageShow()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)//超过一半了
        {
            // First half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            // Second half of the popup lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            // Start disappearing 开始消失
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
                PoolMgr.Instance.PushObj(this.gameObject.name, this.gameObject);
        }
    }
}
