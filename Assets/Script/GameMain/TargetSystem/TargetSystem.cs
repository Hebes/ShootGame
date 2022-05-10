using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CodeMonkey.Utils;
using TMPro;
using Tool;

public class TargetSystem : BaseManager<TargetSystem>
{

    /// <summary>
    /// 加载TargetSystemPrefab物体后 需要执行的unityAction
    /// </summary>
    /// <param name="unityAction"></param>
    public void AddPing(Ping ping, Transform parent)
    {
        //ResMgr.Instance.LoadAsync<GameObject>(Config_ResourcesLoadPaths.targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab, unityAction);

        //TUDO 需要重构这段代码
        ResourceRequest goRR = Resources.LoadAsync<Transform>(Config_ResourcesLoadPaths.targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab);

        Transform pingTransform = (Transform)Object.Instantiate(goRR.asset, ping.GetPosition, Quaternion.identity, parent);

        switch (ping.GetPingType)
        {
            case Ping.Type.Move:
                break;
            case Ping.Type.Enemy:
                pingTransform.Find_Child<SpriteRenderer>("TargetBody").sprite = GameManager.Instance.pingEnemySprite;

                TextMeshPro textMeshPro = pingTransform.Find_Child<TextMeshPro>("Targe_Text");
                Debug.Log(textMeshPro);
                
                // pingTransform.Find_Child_Transform("Targe_Text").GetComponent<TextMeshPro>().color = GameManager.Instance.pingEnemyColor;
                break;
            default:
                break;
        }

        UI_TargetWindow.Instance.AddPing(ping);
    }


    /*
    * Handles a single Ping
    * */
    public class Ping
    {
        public enum Type
        {
            Move,
            Enemy,
        }

        private Type type;
        private Vector3 position;
        public Ping(Type type, Vector3 position)
        {
            this.type = type;
            this.position = position;
        }
        public Type GetPingType
        {
            get { return type; }
        }
        public Vector3 GetPosition
        {
            get { return position; }
        }

    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(Config_Key.Key_Mouse_Right))
    //    {
    //        ResMgr.Instance.LoadAsync<GameObject>(Config_ResourcesLoadPaths.targetSystem_Prefab_TargetSystemIcon_Prefab_TargetSystemPrefab,
    //            (obj) => {
    //                obj.transform.position = UtilsClass.GetMouseWorldPosition();
    //                obj.transform.SetParent(transform);
    //                UI_TargetWindow.Instance.AddPing(obj.transform.position);
    //            });

    //    }
    //}
}



