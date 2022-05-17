using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PingWheel : SingletonMono_Temp<PingWheel>
{
    private Vector3 pingPosition;


    protected override void Awake()
    {
        transform.Find("MoveBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Move, pingPosition), null);
            Hide();
        };

        transform.Find("EnemyBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Enemy, pingPosition), null);
            Hide();
        };

        transform.Find("LootingBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Looting, pingPosition), null);
            Hide();
        };

        transform.Find("AttackingBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Attacking, pingPosition), null);
            Hide();
        };

        transform.Find("GoingHereBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.GoingHere, pingPosition), null);
            Hide();
        };

        transform.Find("DefendBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Defend, pingPosition), null);
            Hide();
        };

        transform.Find("WatchingBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Watching, pingPosition), null);
            Hide();
        };

        transform.Find("EnemyseenBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            //null可替换成transform.Find("MoveBtn")
            TargetSystem.Instance.AddPing(new TargetSystem.Ping(TargetSystem.Ping.Type.Enemyseen, pingPosition), null);
            Hide();
        };

        Hide();
    }
    public  void Show(Vector3 pingPosition)
    {
        this.pingPosition = pingPosition;
        gameObject.SetActive(true);
        TargetSystem.Instance.DestroyLastPing();
    }
    public  void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsVisibleStatic
    {
        get { return gameObject.activeSelf; }
    }

}