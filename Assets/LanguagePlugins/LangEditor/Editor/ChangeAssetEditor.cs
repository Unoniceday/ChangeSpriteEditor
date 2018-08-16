using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ChangeAssetEditor : MonoBehaviour {



    public static string AssetFolder = "Assets/LanguagePlugins/LangData/XXX";
    public static string SplitExtension = "_";
    public static string ChangeExtension = "TW";



    static bool ChangeSprite(string name,SpriteRenderer self)
    {
      
        string[] spritePath = AssetDatabase.FindAssets("t:Sprite", new[] { AssetFolder });
        
        foreach (string path in spritePath)
        {
            string t = AssetDatabase.GUIDToAssetPath(path);
            var sp = t.Split('/');

            string spc = sp[sp.Length - 1];
            var spct = spc.Split('.');

            var selfName = name;
            //Debug.Log("找的名字: " + name+"_s" + "替換的名字 :" + spct[0]);

            
            if (spct[0] == name + SplitExtension + ChangeExtension)
            {
                self.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                //Sprite g = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                return true;
            }

            



        }

        return false;

        //var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
    }

    static bool ChangeSprite(string name, Image self)
    {
        //找所有sprite
        string[] spritePath = AssetDatabase.FindAssets("t:Sprite", new[] { AssetFolder });


        foreach (string path in spritePath)
        {
            string t = AssetDatabase.GUIDToAssetPath(path);
            var sp = t.Split('/');

            string spc = sp[sp.Length - 1];
            var spct = spc.Split('.');
            //spct[0]是資料夾內名稱 name是外部名稱
            if (spct[0] == name + SplitExtension + ChangeExtension)
            {
                self.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                //Sprite g = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                return true;
            }


        }

        return false;

        //var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
    }

    static bool RevertSprite(string name, SpriteRenderer self)
    {
        string[] spritePath = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets" });

        foreach (string path in spritePath)
        {
            string t = AssetDatabase.GUIDToAssetPath(path);
            var sp = t.Split('/');

            string spc = sp[sp.Length - 1];
            var spct = spc.Split('.');

            var selfName = name;
           
            var tmpS = selfName.Replace(SplitExtension + ChangeExtension, "");

            if (spct[0] == tmpS)
            {
                    self.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                    //Sprite g = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                    return true;
            }
            



        }

        return false;
    }
    static bool RevertSprite(string name, Image self)
    {
        string[] spritePath = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets" });

        foreach (string path in spritePath)
        {
            string t = AssetDatabase.GUIDToAssetPath(path);
            var sp = t.Split('/');

            string spc = sp[sp.Length - 1];
            var spct = spc.Split('.');

            var selfName = name;

            var tmpS = selfName.Replace(SplitExtension + ChangeExtension, "");

            if (spct[0] == tmpS)
            {
                self.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                //Sprite g = (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
                return true;
            }




        }

        return false;
    }
    //[UnityEditor.MenuItem("ChangeAsset/Change Sprite")]
    public static void FindAllObjInHierarchy()
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {

            if (obj.transform.parent == null)
            {

                Traverse(obj,false);
            }
        }
    }

    public static void RevertAllObjInHierarchy()
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        {

            if (obj.transform.parent == null)
            {

                Traverse(obj,true);
            }
        }
    }

    public static void SetChangeAssetParameter(string assetpath,string splitExt,string changeExt)
    {
        AssetFolder = assetpath;
        SplitExtension = splitExt;
        ChangeExtension = changeExt;
    }


    static void Traverse(GameObject obj,bool isRevert)
    {

        if (obj.GetComponent<SpriteRenderer>() != null)
        {
            var tmp = obj.GetComponent<SpriteRenderer>();

            if (tmp.sprite != null)
            {
                if (isRevert)
                {
                    if (RevertSprite(tmp.sprite.name, tmp))
                    {
                        Debug.Log("<color=#48FF00FF>" + "Reverse ->  [" + tmp.sprite.name + "] -> [" + tmp + "]" + "</color>");
                    }
                    else
                    {
                        Debug.Log("not find [" + tmp.sprite.name +"]");
                    }
                }
                else
                {
                    if (ChangeSprite(tmp.sprite.name, tmp))
                    {
                        Debug.Log("<color=#26CCA3FF>" + "Change Sprite ->  [" + tmp.sprite.name + "] -> [" + tmp + "]" + "</color>");
                    }
                    else
                    {
                        Debug.Log("not find [" + tmp.sprite.name + "]");
                    }
                }
            }

        }
        else if (obj.GetComponent<Image>() != null)
        {
            var tmp = obj.GetComponent<Image>();

            if (tmp.sprite != null)
            {

                string originNmae = tmp.sprite.name;
                if (isRevert)
                {
                    if (RevertSprite(tmp.sprite.name, tmp))
                    {
                        Debug.Log("<color=#48FF00FF>" + "Reverse ->  [" + originNmae + "] -> [" + tmp.sprite.name + "]" +"</color>");
                    }
                    else
                    {
                        Debug.Log("not find [" + originNmae + "]");
                    }
                }
                else
                {
                    if (ChangeSprite(tmp.sprite.name, tmp))
                    {
                        Debug.Log("<color=#26CCA3FF>"+" Change Sprite ->  [" + originNmae + "] -> [" + tmp.sprite.name + "]" +"</color>");
                    }
                    else
                    {
                        Debug.Log("not find [" + originNmae + "]");
                    }
                }
            }
        }
        else
        {
            //Debug.Log(obj.name);
        }


        foreach (Transform child in obj.transform)
        {
            Traverse(child.gameObject,isRevert);
        }


    }
   


    public static void ReplaceFileName(string extension)
    {
       
        string[] spritePath = AssetDatabase.FindAssets("t:Sprite", new[] { AssetFolder });
        
      
        foreach (string path in spritePath)
        {
            string t = AssetDatabase.GUIDToAssetPath(path);
            // var gg = AssetDatabase.LoadAssetAtPath(t, typeof(Sprite));
            var sp = t.Split('/');

            string spc = sp[sp.Length - 1];
            var spct = spc.Split('.');
            AssetDatabase.RenameAsset(t, spct[0]+extension);
            AssetDatabase.SaveAssets();

        }

    }

}
