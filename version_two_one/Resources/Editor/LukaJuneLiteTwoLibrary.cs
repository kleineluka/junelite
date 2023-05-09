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

namespace JuneLite {

    public class LukaJuneLiteTwoLibrary
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
        double dblPortVersion = 2.5;

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

        public static int getThirdWidth()
        {
            return ((int)EditorGUIUtility.currentViewWidth / 3);
        }

        public static void makeBanner(string inputImage)
        {
            Texture2D texBanner = Resources.Load(inputImage, typeof(Texture2D)) as Texture2D;
            EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(0, int.MaxValue, getThirdWidth(), 10), texBanner, null, ScaleMode.ScaleToFit);
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
            style.fontSize = 14;
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

        public static void startSection() {
            makeBoxStart();
        }

        public static void endSection() {
            makeBoxEnd();
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

        private static string truncateLanguage(string line) {
            // ex; lang_item=My Item
            // returns My Item
            string[] split_line = line.Split('=');
            return split_line[1];
        }

        public static void loadLanguage(int language) {
            try {
                string chosen_lang = "English";
                switch (language) {
                    case 1: // German
                        chosen_lang = "German";
                        break;
                    case 2: // Japanese
                        chosen_lang = "Japanese";
                        break;
                    default: // English
                        chosen_lang = "English";
                        break;
                }
                TextAsset lang_data = (TextAsset) Resources.Load("Languages/" + chosen_lang);
                string lang_text = lang_data.text;
                string[] lang_lines = lang_text.Split(
                    new string[] { "\n" },
                    StringSplitOptions.None
                );
                LukaJuneLiteUITwo.lang_rendering_shaderpower = truncateLanguage(lang_lines[0]);
                LukaJuneLiteUITwo.lang_rendering_falloffstart = truncateLanguage(lang_lines[1]);
                LukaJuneLiteUITwo.lang_rendering_falloffend = truncateLanguage(lang_lines[2]);
                LukaJuneLiteUITwo.lang_rendering_outofboundsstyle = truncateLanguage(lang_lines[3]);
                LukaJuneLiteUITwo.lang_blur_enable = truncateLanguage(lang_lines[4]);
                LukaJuneLiteUITwo.lang_blur_highquality = truncateLanguage(lang_lines[5]);
                LukaJuneLiteUITwo.lang_blur_style = truncateLanguage(lang_lines[6]);
                LukaJuneLiteUITwo.lang_blur_power = truncateLanguage(lang_lines[7]);
                LukaJuneLiteUITwo.lang_blur_radius = truncateLanguage(lang_lines[8]);
                LukaJuneLiteUITwo.lang_blur_transparency = truncateLanguage(lang_lines[9]);
                LukaJuneLiteUITwo.lang_blur_color = truncateLanguage(lang_lines[10]);
                LukaJuneLiteUITwo.lang_border_enable = truncateLanguage(lang_lines[11]);
                LukaJuneLiteUITwo.lang_border_style = truncateLanguage(lang_lines[12]);
                LukaJuneLiteUITwo.lang_border_color = truncateLanguage(lang_lines[13]);
                LukaJuneLiteUITwo.lang_border_power = truncateLanguage(lang_lines[14]);
                LukaJuneLiteUITwo.lang_border_soften = truncateLanguage(lang_lines[15]);
                LukaJuneLiteUITwo.lang_coloring_enable = truncateLanguage(lang_lines[16]);
                LukaJuneLiteUITwo.lang_coloring_rgbmultiply = truncateLanguage(lang_lines[17]);
                LukaJuneLiteUITwo.lang_coloring_rgboverlaytransparency = truncateLanguage(lang_lines[18]);
                LukaJuneLiteUITwo.lang_coloring_rgboverlay = truncateLanguage(lang_lines[19]);
                LukaJuneLiteUITwo.lang_coloring_hsvstyle = truncateLanguage(lang_lines[20]);
                LukaJuneLiteUITwo.lang_coloring_hsvhue = truncateLanguage(lang_lines[21]);
                LukaJuneLiteUITwo.lang_coloring_hsvsaturation = truncateLanguage(lang_lines[22]);
                LukaJuneLiteUITwo.lang_coloring_hsvvalue = truncateLanguage(lang_lines[23]);
                LukaJuneLiteUITwo.lang_coloring_invert = truncateLanguage(lang_lines[24]);
                LukaJuneLiteUITwo.lang_coloring_colordrain = truncateLanguage(lang_lines[25]);
                LukaJuneLiteUITwo.lang_coloring_darkness = truncateLanguage(lang_lines[26]);
                LukaJuneLiteUITwo.lang_coloring_brightness = truncateLanguage(lang_lines[27]);
                LukaJuneLiteUITwo.lang_coloring_emission = truncateLanguage(lang_lines[28]);
                LukaJuneLiteUITwo.lang_coloring_posterization = truncateLanguage(lang_lines[29]);
                LukaJuneLiteUITwo.lang_coloring_colorgrading = truncateLanguage(lang_lines[30]);
                LukaJuneLiteUITwo.lang_coloring_colorgradingtone = truncateLanguage(lang_lines[31]);
                LukaJuneLiteUITwo.lang_distortion_enable = truncateLanguage(lang_lines[32]);
                LukaJuneLiteUITwo.lang_distortion_style = truncateLanguage(lang_lines[33]);
                LukaJuneLiteUITwo.lang_distortion_powerx = truncateLanguage(lang_lines[34]);
                LukaJuneLiteUITwo.lang_distortion_powery = truncateLanguage(lang_lines[35]);
                LukaJuneLiteUITwo.lang_distortion_speedx = truncateLanguage(lang_lines[36]);
                LukaJuneLiteUITwo.lang_distortion_speedy = truncateLanguage(lang_lines[37]);
                LukaJuneLiteUITwo.lang_distortion_texture = truncateLanguage(lang_lines[38]);
                LukaJuneLiteUITwo.lang_distortion_texturescale = truncateLanguage(lang_lines[39]);
                LukaJuneLiteUITwo.lang_distortion_addinwobble = truncateLanguage(lang_lines[40]);
                LukaJuneLiteUITwo.lang_distortion_wobblepower = truncateLanguage(lang_lines[41]);
                LukaJuneLiteUITwo.lang_distortion_wobblespeed = truncateLanguage(lang_lines[42]);
                LukaJuneLiteUITwo.lang_distortion_wobblecoverage = truncateLanguage(lang_lines[43]);
                LukaJuneLiteUITwo.lang_filter_enable = truncateLanguage(lang_lines[44]);
                LukaJuneLiteUITwo.lang_vignette_enable = truncateLanguage(lang_lines[45]);
                LukaJuneLiteUITwo.lang_vignette_power = truncateLanguage(lang_lines[46]);
                LukaJuneLiteUITwo.lang_vignette_color = truncateLanguage(lang_lines[47]);
                LukaJuneLiteUITwo.lang_colorcrush_enable = truncateLanguage(lang_lines[48]);
                LukaJuneLiteUITwo.lang_colorcrush_power = truncateLanguage(lang_lines[49]);
                LukaJuneLiteUITwo.lang_duotone_enable = truncateLanguage(lang_lines[50]);
                LukaJuneLiteUITwo.lang_duotone_transparency = truncateLanguage(lang_lines[51]);
                LukaJuneLiteUITwo.lang_duotone_colorone = truncateLanguage(lang_lines[52]);
                LukaJuneLiteUITwo.lang_duotone_colortwo = truncateLanguage(lang_lines[53]);
                LukaJuneLiteUITwo.lang_duotone_threshold = truncateLanguage(lang_lines[54]);
                LukaJuneLiteUITwo.lang_rainbow_enable = truncateLanguage(lang_lines[55]);
                LukaJuneLiteUITwo.lang_rainbow_saturation = truncateLanguage(lang_lines[56]);
                LukaJuneLiteUITwo.lang_rainbow_speed = truncateLanguage(lang_lines[57]);
                LukaJuneLiteUITwo.lang_film_enable = truncateLanguage(lang_lines[58]);
                LukaJuneLiteUITwo.lang_film_amount = truncateLanguage(lang_lines[59]);
                LukaJuneLiteUITwo.lang_grain_enable = truncateLanguage(lang_lines[60]);
                LukaJuneLiteUITwo.lang_grain_amount = truncateLanguage(lang_lines[61]);
                LukaJuneLiteUITwo.lang_vhs_enable = truncateLanguage(lang_lines[62]);
                LukaJuneLiteUITwo.lang_vhs_amount = truncateLanguage(lang_lines[63]);
                LukaJuneLiteUITwo.lang_gradient_enable = truncateLanguage(lang_lines[64]);
                LukaJuneLiteUITwo.lang_gradient_lhs = truncateLanguage(lang_lines[65]);
                LukaJuneLiteUITwo.lang_gradient_rhs = truncateLanguage(lang_lines[66]);
                LukaJuneLiteUITwo.lang_gradient_transparency = truncateLanguage(lang_lines[67]);
                LukaJuneLiteUITwo.lang_outline_enable = truncateLanguage(lang_lines[68]);
                LukaJuneLiteUITwo.lang_outline_width = truncateLanguage(lang_lines[69]);
                LukaJuneLiteUITwo.lang_outline_tolerance = truncateLanguage(lang_lines[70]);
                LukaJuneLiteUITwo.lang_outline_color = truncateLanguage(lang_lines[71]);
                LukaJuneLiteUITwo.lang_astral_enable = truncateLanguage(lang_lines[72]);
                LukaJuneLiteUITwo.lang_astral_zoom = truncateLanguage(lang_lines[73]);
                LukaJuneLiteUITwo.lang_astral_transparency = truncateLanguage(lang_lines[74]);
                LukaJuneLiteUITwo.lang_astral_color = truncateLanguage(lang_lines[75]);
                LukaJuneLiteUITwo.lang_neon_enable = truncateLanguage(lang_lines[76]);
                LukaJuneLiteUITwo.lang_neon_width = truncateLanguage(lang_lines[77]);
                LukaJuneLiteUITwo.lang_neon_transparency = truncateLanguage(lang_lines[78]);
                LukaJuneLiteUITwo.lang_neon_hue = truncateLanguage(lang_lines[79]);
                LukaJuneLiteUITwo.lang_overlay_enable = truncateLanguage(lang_lines[80]);
                LukaJuneLiteUITwo.lang_overlay_texture = truncateLanguage(lang_lines[81]);
                LukaJuneLiteUITwo.lang_overlay_transparency = truncateLanguage(lang_lines[82]);
                LukaJuneLiteUITwo.lang_overlay_sizex = truncateLanguage(lang_lines[83]);
                LukaJuneLiteUITwo.lang_overlay_sizey = truncateLanguage(lang_lines[84]);
                LukaJuneLiteUITwo.lang_overlay_speedx = truncateLanguage(lang_lines[85]);
                LukaJuneLiteUITwo.lang_overlay_speedy = truncateLanguage(lang_lines[86]);
                LukaJuneLiteUITwo.lang_overlay_offsetx = truncateLanguage(lang_lines[87]);
                LukaJuneLiteUITwo.lang_overlay_offsety = truncateLanguage(lang_lines[88]);
                LukaJuneLiteUITwo.lang_uvmanipulation_enable = truncateLanguage(lang_lines[89]);
                LukaJuneLiteUITwo.lang_transformation_slanttopleft = truncateLanguage(lang_lines[90]);
                LukaJuneLiteUITwo.lang_transformation_slanttopright = truncateLanguage(lang_lines[91]);
                LukaJuneLiteUITwo.lang_transformation_slantbottomleft = truncateLanguage(lang_lines[92]);
                LukaJuneLiteUITwo.lang_transformation_slantbottomright = truncateLanguage(lang_lines[93]);
                LukaJuneLiteUITwo.lang_transformation_flipx = truncateLanguage(lang_lines[94]);
                LukaJuneLiteUITwo.lang_transformation_flipy = truncateLanguage(lang_lines[95]);
                LukaJuneLiteUITwo.lang_transformation_stretchx = truncateLanguage(lang_lines[96]);
                LukaJuneLiteUITwo.lang_transformation_stretchy = truncateLanguage(lang_lines[97]);
                LukaJuneLiteUITwo.lang_movement_movex = truncateLanguage(lang_lines[98]);
                LukaJuneLiteUITwo.lang_movement_movey = truncateLanguage(lang_lines[99]);
                LukaJuneLiteUITwo.lang_shake_style = truncateLanguage(lang_lines[100]);
                LukaJuneLiteUITwo.lang_shake_powerx = truncateLanguage(lang_lines[101]);
                LukaJuneLiteUITwo.lang_shake_powery = truncateLanguage(lang_lines[102]);
                LukaJuneLiteUITwo.lang_shake_speedx = truncateLanguage(lang_lines[103]);
                LukaJuneLiteUITwo.lang_shake_speedy = truncateLanguage(lang_lines[104]);
                LukaJuneLiteUITwo.lang_pixelation_enable = truncateLanguage(lang_lines[105]);
                LukaJuneLiteUITwo.lang_pixelation_power = truncateLanguage(lang_lines[106]);
                LukaJuneLiteUITwo.lang_rotation_enable = truncateLanguage(lang_lines[107]);
                LukaJuneLiteUITwo.lang_rotation_angle = truncateLanguage(lang_lines[108]);
                LukaJuneLiteUITwo.lang_spherize_enable = truncateLanguage(lang_lines[109]);
                LukaJuneLiteUITwo.lang_spherize_power = truncateLanguage(lang_lines[110]);
                LukaJuneLiteUITwo.lang_zoom_enable = truncateLanguage(lang_lines[111]);
                LukaJuneLiteUITwo.lang_zoom_power = truncateLanguage(lang_lines[112]);
                LukaJuneLiteUITwo.lang_zoom_customrange = truncateLanguage(lang_lines[113]);
                LukaJuneLiteUITwo.lang_zoom_rangestart = truncateLanguage(lang_lines[114]);
                LukaJuneLiteUITwo.lang_zoom_rangeend = truncateLanguage(lang_lines[115]);
                LukaJuneLiteUITwo.lang_fog_enable = truncateLanguage(lang_lines[116]);
                LukaJuneLiteUITwo.lang_fog_density = truncateLanguage(lang_lines[117]);
                LukaJuneLiteUITwo.lang_fog_distribution = truncateLanguage(lang_lines[118]);
                LukaJuneLiteUITwo.lang_fog_color = truncateLanguage(lang_lines[119]);
                LukaJuneLiteUITwo.lang_fog_safespace = truncateLanguage(lang_lines[120]);
                LukaJuneLiteUITwo.lang_fog_safespacesize = truncateLanguage(lang_lines[121]);
                LukaJuneLiteUITwo.lang_glitch_enable = truncateLanguage(lang_lines[122]);
                LukaJuneLiteUITwo.lang_glitch_scale = truncateLanguage(lang_lines[123]);
                LukaJuneLiteUITwo.lang_glitch_amount = truncateLanguage(lang_lines[124]);
                LukaJuneLiteUITwo.lang_glitch_uvs = truncateLanguage(lang_lines[125]);
                LukaJuneLiteUITwo.lang_glitch_chromatic = truncateLanguage(lang_lines[126]);
                LukaJuneLiteUITwo.lang_overlay_animated = truncateLanguage(lang_lines[127]);
                LukaJuneLiteUITwo.lang_overlay_framesx = truncateLanguage(lang_lines[128]);
                LukaJuneLiteUITwo.lang_overlay_framesy = truncateLanguage(lang_lines[129]);
                LukaJuneLiteUITwo.lang_overlay_frames = truncateLanguage(lang_lines[130]);
                LukaJuneLiteUITwo.lang_overlay_speed = truncateLanguage(lang_lines[131]);
                LukaJuneLiteUITwo.lang_overlay_scrub = truncateLanguage(lang_lines[132]);
                LukaJuneLiteUITwo.lang_ui_notice = truncateLanguage(lang_lines[133]);
                LukaJuneLiteUITwo.lang_ui_depth = truncateLanguage(lang_lines[134]);
            } catch (Exception e) {
                // ignore for now
            }
        }


    }

}

#endif
