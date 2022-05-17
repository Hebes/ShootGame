using UnityEngine;

/// <summary>
/// 玩家健康值-气血
/// </summary>
public class Player_HP : MonoBehaviour, ICommonCollide
{
    private Bar_HealthSystem bar_HealthSystem;
    [SerializeField] private int Hp = 100;

    public void Damage(int damageAmount)
    {
        bar_HealthSystem.Damage(damageAmount);
    }

    private void Awake()
    {
        Player_Components player_Components = GetComponent<Player_Components>();
        Transform tfPlayerHPBar = player_Components.Player_Hp_PlayerHPBar;

        //实例化血条
        GameObject pfHpBar = ResMgr.Instance.Load_pf_Instantiate(Config_ResLoadPaths.BarHP_pf, new Vector3(tfPlayerHPBar.position.x, tfPlayerHPBar.position.y + 8.5f), Quaternion.identity, tfPlayerHPBar);

        //血条组件
        bar_HealthSystem = new Bar_HealthSystem(Hp);
        pfHpBar.GetComponent<Bar_Health>().Setup(bar_HealthSystem);
    }

    #region 可以删除的
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        bar_HealthSystem.Damage(10);
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        bar_HealthSystem.Heal(10);
    //    }
    //}
    #endregion
}
