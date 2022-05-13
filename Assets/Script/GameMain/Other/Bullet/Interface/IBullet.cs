using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪的公用射击方法，如需切换不同的射击方法  请继承这个接口
/// 1. public  访问不受限制
/// 2. protected 访问仅限于包含类或从包含类派生出来的类型
/// 3. internal 访问仅限于当前程序集
/// 4. protected internal 访问限制到当前程序集或从包含派生的类型
/// 5. private 访问仅限于包含类型
/// </summary>
internal interface IBullet
{
    void Setup(Vector3 shootDir);
}
