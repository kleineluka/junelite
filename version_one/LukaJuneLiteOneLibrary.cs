#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;

public class LukaJuneLiteOneLibrary
{

    // me
    const string strMyUsername = "luka";
    const string strMyRealName = "Zoey";
    const string strMyDiscord = "luka#8375";
    const string strMyGithub = "github.com/lukasong";
    const string strMyWebsite = "www.luka.moe";
    const string strMyEmail = "lukazoeysong@gmail.com";

    // configuration menu
    string strPortFor = "JuneLite";
    double dblLibraryVersion = 1.0;
    double dblPortVersion = 1.0;

    // my library c:

    public static void lukaLog(string message)
    {
        Debug.Log("Luka Log : " + message);
    }

    public static bool openWebpage(string url)
    {
        try
        {
            Application.OpenURL(url);
            return true;
        }
        catch (System.Exception e)
        {
            lukaLog("Failed to open website.. the url[ " + url + "] and error[" + e.Message + "].");
            return false;
        }
    }

    public static void makeDivider()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }

    public static GUIStyle makeStyle(int inputSize, UnityEngine.TextAnchor inputPosition)
    {
        GUIStyle thisStyle = new GUIStyle(GUI.skin.label);
        thisStyle.wordWrap = true;
        thisStyle.alignment = inputPosition;
        thisStyle.fontSize = inputSize;
        thisStyle.richText = true;
        return thisStyle;
    }

    public static void makeBanner(string inputImage)
    {
        Texture2D texBanner = Resources.Load(inputImage, typeof(Texture2D)) as Texture2D;
        EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(0, int.MaxValue, 280, 30), texBanner, null, ScaleMode.ScaleToFit);
    }

    public static void makeText(string inputText, GUIStyle inputStyle)
    {
        EditorGUILayout.LabelField(inputText, inputStyle);
    }

    public static bool makeFoldout(bool boolState, string inputLabel)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.label).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fontSize = 12;
        style.fixedHeight = 22;
        style.contentOffset = new Vector2(20f, -2f);
        var rect = GUILayoutUtility.GetRect(16f, 22f, style);
        GUI.Box(rect, inputLabel, style);
        var e = Event.current;
        var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
        if (e.type == EventType.Repaint)
        {
            EditorStyles.foldout.Draw(toggleRect, false, false, boolState, false);
        }
        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            boolState = !boolState;
            e.Use();
        }
        return boolState;
    }

    public static bool makeFoldout(bool boolState, string inputLabel, int fontSize)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.label).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fontSize = fontSize;
        style.fixedHeight = fontSize + 10;
        style.contentOffset = new Vector2(20f, -2f);
        var rect = GUILayoutUtility.GetRect(16f, (float)fontSize + 10f, style);
        GUI.Box(rect, inputLabel, style);
        var e = Event.current;
        var toggleRect = new Rect(rect.x + 4f, rect.y + 6f, 13f, 13f);
        if (e.type == EventType.Repaint)
        {
            EditorStyles.foldout.Draw(toggleRect, false, false, boolState, false);
        }
        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            boolState = !boolState;
            e.Use();
        }
        return boolState;
    }

    public static bool makeFoldoutSub(bool boolState, string inputLabel)
    {
        // makes a sub-level foldout
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.label).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fontSize = 12;
        style.fixedHeight = 18;
        style.contentOffset = new Vector2(20f, -2f);
        var rect = GUILayoutUtility.GetRect(16f, 18f, style);
        GUI.Box(rect, inputLabel, style);
        var e = Event.current;
        var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
        if (e.type == EventType.Repaint)
        {
            EditorStyles.foldout.Draw(toggleRect, false, false, boolState, false);
        }
        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            boolState = !boolState;
            e.Use();
        }
        return boolState;
    }

    public static void makeBoxStart()
    {
        EditorGUILayout.BeginVertical("GroupBox");
    }

    public static void makeBoxEnd()
    {
        EditorGUILayout.EndVertical();
    }

    public static void makeRowStart()
    {
        EditorGUILayout.BeginHorizontal();
    }

    public static void makeRowEnd()
    {
        EditorGUILayout.EndHorizontal();
    }

    public static string getHeart()
    {
        return "\u2665";
    }

    public static int makeRandomInt(int lowerBound, int upperBound)
    {
        // note to self: might be outdated? Random.Range(x, y)
        System.Random thisSeed = new System.Random();
        return thisSeed.Next(lowerBound, upperBound);
    }

    public static void makePopup(string inputLabel, string inputContent)
    {
        EditorUtility.DisplayDialog(inputLabel, inputContent, "OK");
    }

    public static void makeCopyright()
    {
        makeBoxStart();
        GUIStyle copyrightStyle = makeStyle(10, TextAnchor.MiddleCenter);
        makeText("<b>Copyright Notice</b>!\nYou are restricted from sharing, redistributing, retelling, explaining, changing, modifying, or otherwise editing or transferring the code in this project. The only exception is uploading the linked particles to games such as VRChat to you or a friends account (public or private is ok). You may not send prefabs of the particles. If you want a commercial license, please see my Booth or Gumroad to buy one.", copyrightStyle);
        makeBoxEnd();
    }

    public static void drawToast(string inputIconSource, string inputTextContents, int inputFontSize, int inputIconMinWidth, int inputIconMaxWidth, int inputWidth)
    {
        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.BeginHorizontal();
        var styleCenter = new GUIStyle(GUI.skin.label);
        styleCenter.wordWrap = true;
        styleCenter.alignment = TextAnchor.MiddleLeft;
        styleCenter.fontSize = inputFontSize;
        styleCenter.richText = true;
        Texture2D texToastIcon = Resources.Load<Texture2D>(inputIconSource);
        EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(inputIconMinWidth, inputIconMaxWidth, inputIconMinWidth, inputIconMaxWidth), texToastIcon, null, ScaleMode.ScaleAndCrop);
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUIUtility.labelWidth = inputWidth - inputIconMaxWidth * 3;
        EditorGUILayout.LabelField(inputTextContents, styleCenter);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    public static void drawToastNoImage(string inputTextContents, int inputFontSize, int inputWidth)
    {
        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.BeginHorizontal();
        var styleCenter = new GUIStyle(GUI.skin.label);
        styleCenter.wordWrap = true;
        styleCenter.richText = true;
        styleCenter.alignment = TextAnchor.MiddleCenter;
        styleCenter.fontSize = inputFontSize;
        EditorGUILayout.LabelField(inputTextContents, styleCenter);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    public static bool drawToastFoldoutHurtbox(string inputTextContents, int inputFontSize, int inputWidth)
    {
        EditorGUILayout.BeginVertical("GroupBox");
        EditorGUILayout.BeginHorizontal();
        var styleCenter = new GUIStyle(GUI.skin.label);
        var styleBorder = styleCenter.border;
        styleBorder.top = 0;
        styleBorder.bottom = 0;
        styleBorder.left = 0;
        styleBorder.right = 0;
        styleCenter.wordWrap = true;
        styleCenter.richText = true;
        styleCenter.alignment = TextAnchor.MiddleCenter;
        styleCenter.fontSize = inputFontSize;
        bool clicked = GUILayout.Button(inputTextContents, styleCenter);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        return clicked;
    }

    public static string getStringEncryption(string inputKey, string inputString)
    {
        byte[] iv = new byte[16];
        byte[] array;
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(inputKey);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream =
                    new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(inputString);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(array);
    }

    public static string getStringDecryption(string inputKey, string inputString)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(inputString);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(inputKey);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream =
                    new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    public static void doFoldoutIndentStart(int intIndentation)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(intIndentation);
        EditorGUILayout.BeginVertical();
    }

    public static void doFoldoutIndentEnd()
    {
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    public static void doGreyStart() {
        GUI.enabled = false;
        //EditorGUI.BeginDisabledGroup(bool) 
    }

    public static void doGreyEnd() {
        GUI.enabled = true;
        //EditorGUI.BeginDisabledGroup(bool) 
    }

    public static void doNotifcation(bool inputCondition, string inputTitle, string inputContents, int fontSize) {
        if (inputCondition) {
            string totalText = "<b>" + inputTitle + "</b>\n" + inputContents;
            drawToastNoImage(totalText, fontSize, 300);
        }
    }


    public static string getServerContents(string inputUrl)
    {
        System.Net.WebClient wc = new System.Net.WebClient();
        return wc.DownloadString(inputUrl);
    }


}

#endif
