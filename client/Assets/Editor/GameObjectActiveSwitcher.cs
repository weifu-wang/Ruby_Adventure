using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// 不需要继承 ScriptableObject，纯静态类即可
public static class GameObjectActiveSwitcher
{
    
    private const string MenuPath = "Tools/Toggle Active State _F1";

    [MenuItem(MenuPath, true)] // 校验方法：决定菜单是否可用
    private static bool ValidateToggle()
    {
        // 只有选中了物体，快捷键才生效
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem(MenuPath)] // 实际执行方法
    private static void ToggleActive()
    {
        var gos = Selection.gameObjects;

        foreach (var go in gos)
        {
            // 1. 【安全】注册撤销操作。这行代码让操作支持 Ctrl+Z
            // "Toggle Active" 是出现在 Edit -> Undo 列表里的名字
            Undo.RecordObject(go, "Toggle Active");

            // 2. 【逻辑】使用 activeSelf 而不是 activeInHierarchy
            // 确保只切换物体自身的勾选框，不受父物体影响
            bool newActiveState = !go.activeSelf;

            go.SetActive(newActiveState);
        }
    }
}
