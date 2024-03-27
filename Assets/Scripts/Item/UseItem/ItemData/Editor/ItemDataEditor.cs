using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ItemData), true)]
public class ItemDataEditor : Editor
{
    ItemData itemData;

    private void OnEnable()
    {
        itemData = target as ItemData;
    }

    public override void OnInspectorGUI()
    {
        if (itemData != null && itemData.itemImage != null) 
        {
            Texture2D texture;
            EditorGUILayout.LabelField("Item Icon Privew");
            texture = AssetPreview.GetAssetPreview(itemData.itemImage);
            if (texture != null) 
            {
                GUILayout.Label("", GUILayout.Height(64), GUILayout.Width(64));
                GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
            }
        }
        base.OnInspectorGUI();
    }
}
#endif