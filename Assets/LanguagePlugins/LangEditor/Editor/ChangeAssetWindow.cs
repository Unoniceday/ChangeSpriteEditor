using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ChangeAssetWindow : EditorWindow
{

    string path = "Assets/LanguagePlugins/LangData/XXX";

    string splitExtension = "_";
    string changeExtension = "TW";

   


    // Add menu named "My Window" to the Window menu
    [MenuItem("ChangeAsset/Change Sprite Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ChangeAssetWindow window = (ChangeAssetWindow)EditorWindow.GetWindow(typeof(ChangeAssetWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Change Sprite Setting", EditorStyles.boldLabel);

        path = EditorGUILayout.TextField("Assets Path", path);
        splitExtension = EditorGUILayout.TextField("Split", splitExtension);
        changeExtension = EditorGUILayout.TextField("Extension", changeExtension);


        ChangeAssetEditor.SetChangeAssetParameter(path, splitExtension, changeExtension);

        if (GUILayout.Button("Change Assets Sprite"))
        {
            ChangeAssetEditor.FindAllObjInHierarchy();
        }

        if (GUILayout.Button("Reverse Change Sprite"))
        {
            ChangeAssetEditor.RevertAllObjInHierarchy();
        }

    }

    //[UnityEditor.MenuItem("ChangeAsset/Change Sprite")]
    //static void ChangeSpriteName()
    //{
    //    ChangeAssetEditor.ReplaceFileName(splitExtension + changeExtension);
    //}
    

    
}
