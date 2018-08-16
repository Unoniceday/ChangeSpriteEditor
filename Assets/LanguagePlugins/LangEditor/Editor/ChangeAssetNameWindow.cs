using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChangeAssetNameWindow : EditorWindow
{
    string path = "Assets/LanguagePlugins/LangData/XXX";

    string splitExtension = "_";
    string changeExtension = "TW";


    [MenuItem("ChangeAsset/Change AssetName Window")]
    static void Init()
    {
       
        ChangeAssetNameWindow window = (ChangeAssetNameWindow)EditorWindow.GetWindow(typeof(ChangeAssetNameWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Change Assets Name Setting", EditorStyles.boldLabel);

        path = EditorGUILayout.TextField("Assets Path ", path);
        splitExtension = EditorGUILayout.TextField("Split", splitExtension);
        changeExtension = EditorGUILayout.TextField("Extension", changeExtension);


        ChangeAssetEditor.SetChangeAssetParameter(path, splitExtension, changeExtension);

        if (GUILayout.Button("Change Assets Name"))
        {
            ChangeAssetEditor.ReplaceFileName(splitExtension + changeExtension); ;
        }

      
    }
}
