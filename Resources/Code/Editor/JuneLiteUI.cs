#if UNITY_EDITOR

using UnityEngine;
using System;
using System.Net;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JuneLite {

    public class JuneLiteUI : ShaderGUI
    {

        // universal properties, fix up later
        public const string strDeveloper = "kleineluka";
        public const string strVersion = "2.5";

        // setting up properties
        MaterialProperty prpLiteRenderingFalloffStart = null;
        MaterialProperty prpLiteRenderingFalloffEnd = null;
        MaterialProperty prpLiteRenderingOOB = null;
        MaterialProperty prpLiteRenderingPower = null;
        MaterialProperty prpLiteRenderingQuality = null;
        MaterialProperty prpLiteColoringModule = null;
        MaterialProperty prpLiteColoringRGBMultiply = null;
        MaterialProperty prpLiteColoringRGBOverlay = null;
        MaterialProperty prpLiteColoringRGBOverlayTransparency = null;
        MaterialProperty prpLiteColoringHSVStyle = null;
        MaterialProperty prpLiteColoringHSVh = null;
        MaterialProperty prpLiteColoringHSVs = null;
        MaterialProperty prpLiteColoringHSVv = null;
        MaterialProperty prpLiteColoringInvert = null;
        MaterialProperty prpLiteColoringDrain = null;
        MaterialProperty prpLiteColoringDarkness = null;
        MaterialProperty prpLiteColoringBrightness = null;
        MaterialProperty prpLiteColoringEmission = null;
        MaterialProperty prpLiteColoringPosterization = null;
        MaterialProperty prpLiteColoringColorGrading = null;
        MaterialProperty prpLiteColoringColorGradingTone = null;
        MaterialProperty prpLiteBlurModule = null;
        MaterialProperty prpLiteBlurStyle = null;
        MaterialProperty prpLiteBlurPower = null;
        MaterialProperty prpLiteBlurRadius = null;
        MaterialProperty prpLiteBlurTransparency = null;
        MaterialProperty prpLiteBlurColor = null;
        MaterialProperty prpLiteDistortionModule = null;
        MaterialProperty prpLiteDistortionStyle = null;
        MaterialProperty prpLiteDistortionPowerX = null;
        MaterialProperty prpLiteDistortionPowerY = null;
        MaterialProperty prpLiteDistortionSpeedX = null;
        MaterialProperty prpLiteDistortionSpeedY = null;
        MaterialProperty prpLiteDistortionTexture = null;
        MaterialProperty prpLiteDistortionTextureScale = null;
        MaterialProperty prpLiteDistortionWobble = null;
        MaterialProperty prpLiteDistortionWobblePower = null;
        MaterialProperty prpLiteDistortionWobbleSpeed = null;
        MaterialProperty prpLiteDistortionWobbleCoverage = null;
        MaterialProperty prpLiteBorderModule = null;
        MaterialProperty prpLiteBorderStyle = null;
        MaterialProperty prpLiteBorderColor = null;
        MaterialProperty prpLiteBorderPower = null;
        MaterialProperty prpLiteBorderSoften = null;
        MaterialProperty prpLiteOverlayModule = null;
        MaterialProperty prpLiteOverlayTexture = null;
        MaterialProperty prpLiteOverlaySizeX = null;
        MaterialProperty prpLiteOverlaySizeY = null;
        MaterialProperty prpLiteOverlayOffsetX = null;
        MaterialProperty prpLiteOverlayOffsetY = null;
        MaterialProperty prpLiteOverlayTransparency = null;
        MaterialProperty prpLiteUVManipulationModule = null;
        MaterialProperty prpLiteUVManipulationTransformationSlantTL = null;
        MaterialProperty prpLiteUVManipulationTransformationSlantTR = null;
        MaterialProperty prpLiteUVManipulationTransformationSlantBL = null;
        MaterialProperty prpLiteUVManipulationTransformationSlantBR = null;
        MaterialProperty prpLiteUVManipulationTransformationFlipX = null;
        MaterialProperty prpLiteUVManipulationTransformationFlipY = null;
        MaterialProperty prpLiteUVManipulationTransformationStretchX = null;
        MaterialProperty prpLiteUVManipulationTransformationStretchY = null;
        MaterialProperty prpLiteUVManipulationMoveX = null;
        MaterialProperty prpLiteUVManipulationMoveY = null;
        MaterialProperty prpLiteUVManipulationShakeStyle = null;
        MaterialProperty prpLiteUVManipulationShakePowerX = null;
        MaterialProperty prpLiteUVManipulationShakePowerY = null;
        MaterialProperty prpLiteUVManipulationShakeSpeedX = null;
        MaterialProperty prpLiteUVManipulationShakeSpeedY = null;
        MaterialProperty prpLiteUVManipulationPixelation = null;
        MaterialProperty prpLiteUVManipulationPixelationPower = null;
        MaterialProperty prpLiteUVManipulationRotation = null;
        MaterialProperty prpLiteUVManipulationRotationAngle = null;
        MaterialProperty prpLiteUVManipulationSpherize = null;
        MaterialProperty prpLiteUVManipulationSpherizePower = null;
        MaterialProperty prpLiteFilterModule = null;
        MaterialProperty prpLiteFilterVignette = null;
        MaterialProperty prpLiteFilterVignettePower = null;
        MaterialProperty prpLiteFilterVignetteColor = null;
        MaterialProperty prpLiteFilterColorCrush = null;
        MaterialProperty prpLiteFilterColorCrushPower = null;
        MaterialProperty prpLiteFilterDuotone = null;
        MaterialProperty prpLiteFilterDuotoneTransparency = null;
        MaterialProperty prpLiteFilterDuotoneColorOne = null;
        MaterialProperty prpLiteFilterDuotoneColorTwo = null;
        MaterialProperty prpLiteFilterDuotoneThreshold = null;
        MaterialProperty prpLiteFilterRainbow = null;
        MaterialProperty prpLiteFilterRainbowSaturation = null;
        MaterialProperty prpLiteFilterRainbowSpeed = null;
        MaterialProperty prpLiteFilterFilm = null;
        MaterialProperty prpLiteFilterFilmAmount = null;
        MaterialProperty prpLiteFilterGrain = null;
        MaterialProperty prpLiteFilterGrainAmount = null;
        MaterialProperty prpLiteFilterGrainColor = null;
        MaterialProperty prpLiteFilterVHS = null;
        MaterialProperty prpLiteFilterVHSAmount = null;
        MaterialProperty prpLiteFilterGradient = null;
        MaterialProperty prpLiteFilterGradientLHS = null;
        MaterialProperty prpLiteFilterGradientRHS = null;
        MaterialProperty prpLiteFilterGradientTransparency = null;
        MaterialProperty prpLiteFilterOutline = null;
        MaterialProperty prpLiteFilterOutlineWidth = null;
        MaterialProperty prpLiteFilterOutlineTolerance = null;
        MaterialProperty prpLiteFilterOutlineColor = null;
        MaterialProperty prpLiteFilterAstral = null;
        MaterialProperty prpLiteFilterAstralZoom = null;
        MaterialProperty prpLiteFilterAstralZoomTransparency = null;
        MaterialProperty prpLiteFilterAstralZoomColor = null;
        MaterialProperty prpLiteFilterNeon = null;
        MaterialProperty prpLiteFilterNeonWidth = null;
        MaterialProperty prpLiteFilterNeonTransparency = null;
        MaterialProperty prpLiteFilterNeonHue = null;
        MaterialProperty prpLiteZoomModule = null;
        MaterialProperty prpLiteZoomPower = null;
        MaterialProperty prpLiteZoomRangeStyle = null;
        MaterialProperty prpLiteZoomRangeStart = null;
        MaterialProperty prpLiteZoomRangeEnd = null;
        MaterialProperty prpLiteFogModule = null;
        MaterialProperty prpLiteGlitchModule = null;
        MaterialProperty prpLiteAudioLinkModule = null;
        MaterialProperty prpLiteAudioLinkBand = null;
        MaterialProperty prpLiteAudioLinkPower = null;
        MaterialProperty prpLiteAudioLinkMin = null;
        MaterialProperty prpLiteAudioLinkMax = null;

        // strings that will be translated
        public static string lang_rendering_shaderpower = "Shader Power";
        public static string lang_rendering_falloffstart = "Falloff Start";
        public static string lang_rendering_falloffend = "Falloff End";
        public static string lang_rendering_outofboundsstyle = "Out of Bounds Style";
        public static string lang_blur_enable = "Enable Blur Module";
        public static string lang_blur_highquality = "Higher Quality Blurs";
        public static string lang_blur_style = "Blur Style";
        public static string lang_blur_power = "Blur Power";
        public static string lang_blur_radius = "Blur Radius";
        public static string lang_blur_transparency = "Blur Transparency";
        public static string lang_blur_color = "Blur Color";
        public static string lang_border_enable = "Enable Border Module";
        public static string lang_border_style = "Border Style";
        public static string lang_border_color = "Border Color";
        public static string lang_border_power = "Border Power";
        public static string lang_border_soften = "Border Soften";
        public static string lang_coloring_enable = "Enable Coloring Module";
        public static string lang_coloring_rgbmultiply = "RGB Multiply";
        public static string lang_coloring_rgboverlaytransparency = "RGB Overlay Transparency";
        public static string lang_coloring_rgboverlay = "RGB Overlay";
        public static string lang_coloring_hsvstyle = "HSV Style";
        public static string lang_coloring_hsvhue = "HSV Hue";
        public static string lang_coloring_hsvsaturation = "HSV Saturation";
        public static string lang_coloring_hsvvalue = "HSV Lightness";
        public static string lang_coloring_invert = "Invert";
        public static string lang_coloring_colordrain = "Color Drain";
        public static string lang_coloring_darkness = "Darkness";
        public static string lang_coloring_brightness = "Brightness";
        public static string lang_coloring_emission = "Emission";
        public static string lang_coloring_posterization = "Posterization";
        public static string lang_coloring_colorgrading = "Color Grading";
        public static string lang_coloring_colorgradingtone = "Color Grading Tone";
        public static string lang_distortion_enable = "Enable Distortion Module";
        public static string lang_distortion_style = "Distortion Style";
        public static string lang_distortion_powerx = "Distortion Power X";
        public static string lang_distortion_powery = "Distortion Power Y";
        public static string lang_distortion_speedx = "Distortion Speed X";
        public static string lang_distortion_speedy = "Distortion Speed Y";
        public static string lang_distortion_texture = "Distortion Texture";
        public static string lang_distortion_texturescale = "Distortion Texture Scale";
        public static string lang_distortion_addinwobble = "Add in WoBbLe?";
        public static string lang_distortion_wobblepower = "Wobble Power";
        public static string lang_distortion_wobblespeed = "Wobble Speed";
        public static string lang_distortion_wobblecoverage = "Wobble Coverage";
        public static string lang_filter_enable = "Enable Filter Module";
        public static string lang_vignette_enable = "Enable Vignette";
        public static string lang_vignette_power = "Vignette Power";
        public static string lang_vignette_color = "Vignette Color";
        public static string lang_colorcrush_enable = "Enable Color Crush";
        public static string lang_colorcrush_power = "Color Crush Power";
        public static string lang_duotone_enable = "Enable Duotone";
        public static string lang_duotone_transparency = "Duotone Transparency";
        public static string lang_duotone_colorone = "Duotone Color One";
        public static string lang_duotone_colortwo = "Duotone Color Two";
        public static string lang_duotone_threshold = "Duotone Threshold";
        public static string lang_rainbow_enable = "Enable Rainbow";
        public static string lang_rainbow_saturation = "Rainbow Saturation";
        public static string lang_rainbow_speed = "Rainbow Speed";
        public static string lang_film_enable = "Enable Film";
        public static string lang_film_amount = "Film Amount";
        public static string lang_grain_enable = "Enable Grain";
        public static string lang_grain_amount = "Grain Amount";
        public static string lang_grain_color = "Grain Color";
        public static string lang_vhs_enable = "Enable VHS";
        public static string lang_vhs_amount = "VHS Amount";
        public static string lang_gradient_enable = "Enable Gradient";
        public static string lang_gradient_lhs = "Gradient LHS";
        public static string lang_gradient_rhs = "Gradient RHS";
        public static string lang_gradient_transparency = "Gradient Transparency";
        public static string lang_outline_enable = "Enable Outline";
        public static string lang_outline_width = "Outline Width";
        public static string lang_outline_tolerance = "Outline Tolerance";
        public static string lang_outline_color = "Outline Color";
        public static string lang_astral_enable = "Enable Astral";
        public static string lang_astral_zoom = "Astral Zoom";
        public static string lang_astral_transparency = "Astral Transparency";
        public static string lang_astral_color = "Astral Color";
        public static string lang_neon_enable = "Enable Neon";
        public static string lang_neon_width = "Neon Width";
        public static string lang_neon_transparency = "Neon Transparency";
        public static string lang_neon_hue = "Neon Hue";
        public static string lang_overlay_enable = "Enable Overlay Module";
        public static string lang_overlay_texture = "Overlay Texture";
        public static string lang_overlay_transparency = "Overlay Transparency";
        public static string lang_overlay_sizex = "Overlay Size X";
        public static string lang_overlay_sizey = "Overlay Size Y";
        public static string lang_overlay_speedx = "Overlay Speed X";
        public static string lang_overlay_speedy = "Overlay Speed Y";
        public static string lang_overlay_offsetx = "Overlay Offset X";
        public static string lang_overlay_offsety = "Overlay Offset Y";
        public static string lang_uvmanipulation_enable = "Enable UV Module";
        public static string lang_transformation_slanttopleft = "Slant Top Left";
        public static string lang_transformation_slanttopright = "Slant Top Right";
        public static string lang_transformation_slantbottomleft = "Slant Bottom Left";
        public static string lang_transformation_slantbottomright = "Slant Bottom Right";
        public static string lang_transformation_flipx = "Flip X";
        public static string lang_transformation_flipy = "Flip Y";
        public static string lang_transformation_stretchx = "Stretch X";
        public static string lang_transformation_stretchy = "Stretch Y";
        public static string lang_movement_movex = "Move X";
        public static string lang_movement_movey = "Move Y";
        public static string lang_shake_style = "Shake Style";
        public static string lang_shake_powerx = "Shake X Power";
        public static string lang_shake_powery = "Shake Y Power";
        public static string lang_shake_speedx = "Shake X Speed";
        public static string lang_shake_speedy = "Shake Y Speed";
        public static string lang_pixelation_enable = "Enable Pixelation";
        public static string lang_pixelation_power = "Pixelation Power";
        public static string lang_rotation_enable = "Enable Rotation";
        public static string lang_rotation_angle = "Rotation Angle";
        public static string lang_spherize_enable = "Enable Spherize";
        public static string lang_spherize_power = "Spherize Power";
        public static string lang_zoom_enable = "Enable Zoom Module";
        public static string lang_zoom_power = "Zoom Power";
        public static string lang_zoom_customrange = "Zoom Custom Range";
        public static string lang_zoom_rangestart = "Zoom Range Start";
        public static string lang_zoom_rangeend = "Zoom Range End";
        public static string lang_fog_enable = "Enable Fog Module";
        public static string lang_fog_density = "Fog Density";
        public static string lang_fog_distribution = "Fog Distribution";
        public static string lang_fog_color = "Fog Color";
        public static string lang_fog_safespace = "Fog Safe Space";
        public static string lang_fog_safespacesize = "Fog Safe Space Size";
        public static string lang_glitch_enable = "Enable Glitch Module";
        public static string lang_glitch_scale = "Glitch Scale";
        public static string lang_glitch_amount = "Glitch Amount";
        public static string lang_glitch_uvs = "Glitch UVs";
        public static string lang_glitch_chromatic = "Glitch Chromatic";
        public static string lang_overlay_animated = "Animated Overlay";
        public static string lang_overlay_framesx = "Overlay Columns";
        public static string lang_overlay_framesy = "Overlay Rows";
        public static string lang_overlay_frames = "Overlay Frames";
        public static string lang_overlay_speed = "Overlay Speed";
        public static string lang_overlay_scrub = "Overlay Scrub";
        public static string lang_ui_notice = "Super-Duper Helpful Advice!";
        public static string lang_ui_depth = "This effect requires a depth light in the scene alongside it. A prefab is provided in the package in Resources/Prefabs.";
        public static string lang_audiolink_enable = "Enable Audio Link";
        public static string lang_audiolink_band = "Band";
        public static string lang_audiolink_power = "Power";
        public static string lang_audiolink_min = "Min Value";
        public static string lang_audiolink_max = "Max Value";

        // setting up extra ui strings
        private static string strUiLanguage = "Language";
        private static string strUiSaveSettings = "Save Settings";
        enum enumUiLanguages { English, Deutsch, 日本語 };
        private static int intUiLanguages = 0;
        private static int intUiLanguage = 0;
        private static bool boolUiMetaLiteUpdate = false;
        private static string strUiMetaLiteUpdateTitle = "June Lite has an update avaliable!";
        private static string strUiMetaLiteUpdateDescription = "Download it from www.luka.moe/june !";
        string strUiButtonWebsite = "Open my website!";
        string strUiButtonPayhip = "Open my Payhip!";
        string strUiButtonGithub = "Open my GitHub!";
        string strUiButtonJinxxy = "Open my Jinxxy!";
        string strUiButtonGumroad = "Open my Gumroad!";
        string strUiButtonBooth = "Open my Booth store!";

        // setting up enums
        enum enumLiteToggle { Off, On };
        enum enumLiteAudioLink { Disabled, Bass, LowerMid, UpperMid, Treble };
        enum enumLiteOOB { Clamp, Mirror, Repeat };
        enum enumLiteHSV { Disabled, Multiply, Add };
        enum enumLiteBlur { Disabled, Gaussian, Radial, Chromatic };
        enum enumLiteDistortion { Disabled, SinCos, Wavey, Texture };
        enum enumLiteBorder { Disabled, Horizontal, Vertical };
        enum enumLiteShake { Disabled, Bumpy, Smooth, Circular };

        // booleans
        private static bool tabRendering = false;
        private static bool tabAudioLink = false;
        private static bool tabColoring = false;
        private static bool tabBlur = false;
        private static bool tabDistortion = false;
        private static bool tabBorder = false;
        private static bool tabFog = false;
        private static bool tabGlitch = false;
        private static bool tabOverlay = false;
        private static bool tabUVManipulation = false;
        private static bool tabFilter = false;
        private static bool tabZoom = false;
        private static bool tabSubFilterVignette = false;
        private static bool tabSubFilterColorCrush = false;
        private static bool tabSubFilterDuotone = false;
        private static bool tabSubFilterRainbow = false;
        private static bool tabSubFilterFilm = false;
        private static bool tabSubFilterGrain = false;
        private static bool tabSubFilterVHS = false;
        private static bool tabSubFilterGradient = false;
        private static bool tabSubFilterOutline = false;
        private static bool tabSubFilterAstral = false;
        private static bool tabSubFilterNeon = false;
        private static bool tabSubUVTransformation = false;
        private static bool tabSubUVMove = false;
        private static bool tabSubUVShake = false;
        private static bool tabSubUVPixelation = false;
        private static bool tabSubUVRotation = false;
        private static bool tabSubUVSpherize = false;
        private static bool tabExtraSettings = false;

        // setting up other variables
        static GUIStyle styHeader = null;
        static GUIStyle styFooter = null;

        // set up notifications of modules on (shouldn't be static, sorry)
        static string toggleBlur = "";
        static string toggleDistortion = "";
        static string toggleBorder = "";
        static string toggleColoring = "";
        static string toggleOverlay = "";
        static string toggleFog = "";
        static string toggleGlitch = "";
        static string toggleUVManipulation = "";
        static string toggleZoom = "";
        static string toggleFilters = "";    
        static string toggleAudioLink = "";

        // functions 
        private static void loadStyles() {
            styHeader = JuneLiteLib.makeStyle(18, TextAnchor.MiddleCenter);
            styFooter = JuneLiteLib.makeStyle(10, TextAnchor.MiddleCenter);
        }

        private static void loadToggles(float inputBlur,
            float inputBorder, float inputColoring,
            float inputDistortion, float inputFilters,
            float inputOverlay, float inputUVManipulation,
            float inputZoom, float inputFog,
            float inputGlitch, float inputAudioLink) {
            string _enabled = " (Enabled)";
            toggleBlur = (inputBlur == 1.0f) ? _enabled : "";
            toggleBorder = (inputBorder == 1.0f) ? _enabled : "";
            toggleColoring = (inputColoring == 1.0f) ? _enabled : "";
            toggleDistortion = (inputDistortion == 1.0f) ? _enabled : "";
            toggleFilters = (inputFilters == 1.0f) ? _enabled : "";
            toggleOverlay = (inputOverlay == 1.0f) ? _enabled : "";
            toggleUVManipulation = (inputUVManipulation == 1.0f) ? _enabled : "";
            toggleZoom = (inputZoom == 1.0f) ? _enabled : "";
            toggleFog = (inputFog == 1.0f) ? _enabled : "";
            toggleGlitch = (inputGlitch == 1.0f) ? _enabled : "";
            toggleAudioLink = (inputAudioLink == 1.0f) ? _enabled : "";
        }

        private static void loadMeta() {
            string strMetaRaw = JuneLiteLib.getServerContents("https://www.luka.moe/call/public/JuneLiteUniversal/JuneMeta.moe");
            int intIndex = 0;
            string line = string.Empty;
            using (StringReader reader = new StringReader(strMetaRaw))
            {
                do
                {
                    // assigning
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        switch (intIndex) {
                            case 0: // lv
                                if (strVersion != line) {
                                    boolUiMetaLiteUpdate = true;
                                } else {
                                    boolUiMetaLiteUpdate = false;
                                }
                                break;
                            case 1: // lvm
                                strUiMetaLiteUpdateDescription = line;
                                break;
                            case 2: // pv
                                break;
                            case 3: // pvm
                                break;
                            case 4: // pvd
                                break;
                            case 5: // pvdd
                                break;
                            case 6: // oa
                                break;
                            case 7: // oad
                                break;
                            case 8: // motd
                                break;
                            case 9: // motdd
                                break;
                            case 10: // oand
                                break;
                            case 11: // oandd
                                break;
                            case 12: // pd
                                break;
                            default:  // end
                                break;
                        }
                    }
                    intIndex++;
                } while (line != null);
            }
        }

        private static void loadFailedMeta() {
            boolUiMetaLiteUpdate = false;
            strUiMetaLiteUpdateDescription = "There is no update at this time! Check the Discord for more info.";
        }

        private static void saveSettings(int language) {
            string strPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/" + strDeveloper;
            // make sure the folder exists
            if (!Directory.Exists(strPath)) {
                Directory.CreateDirectory(strPath);
            }
            strPath += "/junelite";
            if (!Directory.Exists(strPath)) {
                Directory.CreateDirectory(strPath);
            }
            // and make, or overwrite, the settings file (settings.moe) with the language int
            File.WriteAllText(strPath + "/settings.moe", language.ToString());
        }

        private static void loadSettings() {
            // see if the settings file exists
            string strPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/lukasong/JuneLite/Two/settings.moe";
            if (File.Exists(strPath)) {
                // if it does, read it
                string strRaw = File.ReadAllText(strPath);
                // and try to parse it
                int intLanguage = 0;
                if (int.TryParse(strRaw, out intLanguage)) {
                    // if it works, set the language
                    intUiLanguage = intLanguage;
                } else {
                    // if it doesn't, set the language to english
                    intUiLanguage = 0;
                }
            } else {
                // if it doesn't, set the language to english
                intUiLanguage = 0;
                // and save the settings
                saveSettings(intUiLanguage);
            }
            JuneLiteLib.loadLanguage(intUiLanguage);
        }

        [InitializeOnLoad]
        public class Startup
        {
            static Startup()
            {
                try
                {
                    loadMeta();
                }
                catch (Exception e)
                {
                    loadFailedMeta();
                }
                try {
                    loadSettings();
                } catch (Exception e) {
                    // nothing
                }
            }
        }

        //|===============================================|
        //|			              ui        	          |
        //|===============================================|
        public override void OnGUI(MaterialEditor meThis, MaterialProperty[] propertiesThis)
        {

            // set up
            Material materialThis = meThis.target as Material;
            if (meThis.isVisible == false) return;
            if (styHeader == null) loadStyles();

            // header
            EditorGUILayout.Space(15);
            JuneLiteLib.makeBanner("Images/JuneLiteBanner_ForLukaSong_ByAtArotxt_OnTwitter");
            EditorGUILayout.Space(10);

            // lite update
            JuneLiteLib.doNotifcation(boolUiMetaLiteUpdate, strUiMetaLiteUpdateTitle, strUiMetaLiteUpdateDescription, 12); 

            // displaying the gui..
            JuneLiteLib.makeDivider();
            EditorGUI.BeginChangeCheck();

            // load toggles
            prpLiteBlurModule = ShaderGUI.FindProperty("_LiteBlurModule", propertiesThis);
            prpLiteDistortionModule = ShaderGUI.FindProperty("_LiteDistortionModule", propertiesThis);
            prpLiteBorderModule = ShaderGUI.FindProperty("_LiteBorderModule", propertiesThis);
            prpLiteColoringModule = ShaderGUI.FindProperty("_LiteColoringModule", propertiesThis);
            prpLiteOverlayModule = ShaderGUI.FindProperty("_LiteOverlayModule", propertiesThis);
            prpLiteUVManipulationModule = ShaderGUI.FindProperty("_LiteUVManipulationModule", propertiesThis);
            prpLiteFilterModule = ShaderGUI.FindProperty("_LiteFilterModule", propertiesThis);
            prpLiteZoomModule = ShaderGUI.FindProperty("_LiteZoomModule", propertiesThis);
            prpLiteFogModule = ShaderGUI.FindProperty("_LiteFogModule", propertiesThis);
            prpLiteGlitchModule = ShaderGUI.FindProperty("_LiteGlitchModule", propertiesThis);
            prpLiteAudioLinkModule = ShaderGUI.FindProperty("_LiteAudioLinkModule", propertiesThis);
            loadToggles(prpLiteBlurModule.floatValue, prpLiteBorderModule.floatValue, prpLiteColoringModule.floatValue,
                prpLiteDistortionModule.floatValue, prpLiteFilterModule.floatValue,
                prpLiteOverlayModule.floatValue, prpLiteUVManipulationModule.floatValue,
                prpLiteZoomModule.floatValue, prpLiteFogModule.floatValue, prpLiteGlitchModule.floatValue,
                prpLiteAudioLinkModule.floatValue);

            // rendering tab
            tabRendering = JuneLiteLib.makeFoldout(tabRendering, "Rendering", 18);
            if (tabRendering)
            {
                JuneLiteLib.startSection();
                prpLiteRenderingFalloffStart = ShaderGUI.FindProperty("_LiteRenderingFalloffStart", propertiesThis);
                prpLiteRenderingFalloffEnd = ShaderGUI.FindProperty("_LiteRenderingFalloffEnd", propertiesThis);
                prpLiteRenderingPower = ShaderGUI.FindProperty("_LiteRenderingPower", propertiesThis);
                prpLiteRenderingOOB = ShaderGUI.FindProperty("_LiteRenderingOOB", propertiesThis);
                meThis.ShaderProperty(prpLiteRenderingPower, lang_rendering_shaderpower);
                meThis.ShaderProperty(prpLiteRenderingFalloffStart, lang_rendering_falloffstart);
                meThis.ShaderProperty(prpLiteRenderingFalloffEnd, lang_rendering_falloffend);
                prpLiteRenderingOOB.floatValue = (float)(enumLiteOOB)EditorGUILayout.EnumPopup(lang_rendering_outofboundsstyle, (enumLiteOOB)prpLiteRenderingOOB.floatValue);
                JuneLiteLib.endSection();
            }

            // blur tab
            tabBlur = JuneLiteLib.makeFoldout(tabBlur, "Blur" + toggleBlur, 18);
            if (tabBlur)
            {
                JuneLiteLib.startSection();
                prpLiteBlurModule = ShaderGUI.FindProperty("_LiteBlurModule", propertiesThis);
                prpLiteBlurPower = ShaderGUI.FindProperty("_LiteBlurPower", propertiesThis);
                prpLiteBlurRadius = ShaderGUI.FindProperty("_LiteBlurRadius", propertiesThis);
                prpLiteBlurTransparency = ShaderGUI.FindProperty("_LiteBlurTransparency", propertiesThis);
                prpLiteBlurColor = ShaderGUI.FindProperty("_LiteBlurColor", propertiesThis);
                prpLiteBlurStyle = ShaderGUI.FindProperty("_LiteBlurStyle", propertiesThis);
                prpLiteRenderingQuality = ShaderGUI.FindProperty("_LiteRenderingQuality", propertiesThis);
                meThis.ShaderProperty(prpLiteBlurModule, lang_blur_enable);
                meThis.ShaderProperty(prpLiteRenderingQuality, lang_blur_highquality);
                prpLiteBlurStyle.floatValue = (float)(enumLiteBlur)EditorGUILayout.EnumPopup(lang_blur_style, (enumLiteBlur)prpLiteBlurStyle.floatValue);
                meThis.ShaderProperty(prpLiteBlurPower, lang_blur_power);
                meThis.ShaderProperty(prpLiteBlurRadius, lang_blur_radius);
                meThis.ShaderProperty(prpLiteBlurTransparency, lang_blur_transparency);
                meThis.ShaderProperty(prpLiteBlurColor, lang_blur_color);
                JuneLiteLib.endSection();
            }

            // border tab
            tabBorder = JuneLiteLib.makeFoldout(tabBorder, "Border" + toggleBorder, 18);
            if (tabBorder)
            {
                JuneLiteLib.startSection();
                prpLiteBorderModule = ShaderGUI.FindProperty("_LiteBorderModule", propertiesThis);
                prpLiteBorderStyle = ShaderGUI.FindProperty("_LiteBorderStyle", propertiesThis);
                prpLiteBorderColor = ShaderGUI.FindProperty("_LiteBorderColor", propertiesThis);
                prpLiteBorderPower = ShaderGUI.FindProperty("_LiteBorderPower", propertiesThis);
                prpLiteBorderSoften = ShaderGUI.FindProperty("_LiteBorderSoften", propertiesThis);
                meThis.ShaderProperty(prpLiteBorderModule, lang_border_enable);
                prpLiteBorderStyle.floatValue = (float)(enumLiteBorder)EditorGUILayout.EnumPopup(lang_border_style, (enumLiteBorder)prpLiteBorderStyle.floatValue);
                meThis.ShaderProperty(prpLiteBorderColor, lang_border_color);
                meThis.ShaderProperty(prpLiteBorderPower, lang_border_power);
                meThis.ShaderProperty(prpLiteBorderSoften, lang_border_soften);
                JuneLiteLib.endSection();
            }

            // coloring tab
            tabColoring = JuneLiteLib.makeFoldout(tabColoring, "Coloring" + toggleColoring, 18);
            if (tabColoring)
            {
                JuneLiteLib.startSection();
                prpLiteColoringModule = ShaderGUI.FindProperty("_LiteColoringModule", propertiesThis);
                prpLiteColoringRGBMultiply = ShaderGUI.FindProperty("_LiteColoringRGBMultiply", propertiesThis);
                prpLiteColoringRGBOverlay = ShaderGUI.FindProperty("_LiteColoringRGBOverlay", propertiesThis);
                prpLiteColoringRGBOverlayTransparency = ShaderGUI.FindProperty("_LiteColoringRGBOverlayTransparency", propertiesThis);
                prpLiteColoringHSVStyle = ShaderGUI.FindProperty("_LiteColoringHSVStyle", propertiesThis);
                prpLiteColoringHSVh = ShaderGUI.FindProperty("_LiteColoringHSVh", propertiesThis);
                prpLiteColoringHSVs = ShaderGUI.FindProperty("_LiteColoringHSVs", propertiesThis);
                prpLiteColoringHSVv = ShaderGUI.FindProperty("_LiteColoringHSVv", propertiesThis);
                prpLiteColoringInvert = ShaderGUI.FindProperty("_LiteColoringInvert", propertiesThis);
                prpLiteColoringDrain = ShaderGUI.FindProperty("_LiteColoringDrain", propertiesThis);
                prpLiteColoringDarkness = ShaderGUI.FindProperty("_LiteColoringDarkness", propertiesThis);
                prpLiteColoringEmission = ShaderGUI.FindProperty("_LiteColoringEmission", propertiesThis);
                prpLiteColoringBrightness = ShaderGUI.FindProperty("_LiteColoringBrightness", propertiesThis);
                prpLiteColoringPosterization = ShaderGUI.FindProperty("_LiteColoringPosterization", propertiesThis);
                prpLiteColoringColorGrading = ShaderGUI.FindProperty("_LiteColoringColorGrading", propertiesThis);
                prpLiteColoringColorGradingTone = ShaderGUI.FindProperty("_LiteColoringColorGradingTone", propertiesThis);
                meThis.ShaderProperty(prpLiteColoringModule, lang_coloring_enable);
                meThis.ShaderProperty(prpLiteColoringRGBMultiply, lang_coloring_rgbmultiply);
                meThis.ShaderProperty(prpLiteColoringRGBOverlayTransparency, lang_coloring_rgboverlaytransparency);
                if (prpLiteColoringRGBOverlayTransparency.floatValue != 0) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty(prpLiteColoringRGBOverlay, lang_coloring_rgboverlay);
                    EditorGUI.indentLevel--;
                }
                prpLiteColoringHSVStyle.floatValue = (float)(enumLiteHSV)EditorGUILayout.EnumPopup(lang_coloring_hsvstyle, (enumLiteHSV)prpLiteColoringHSVStyle.floatValue);
                if (prpLiteColoringHSVStyle.floatValue != 0) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty(prpLiteColoringHSVh, lang_coloring_hsvhue);
                    meThis.ShaderProperty(prpLiteColoringHSVs, lang_coloring_hsvsaturation);
                    meThis.ShaderProperty(prpLiteColoringHSVv, lang_coloring_hsvvalue);
                    EditorGUI.indentLevel--;
                }
                meThis.ShaderProperty(prpLiteColoringInvert, lang_coloring_invert);
                meThis.ShaderProperty(prpLiteColoringDrain, lang_coloring_colordrain);
                meThis.ShaderProperty(prpLiteColoringDarkness, lang_coloring_darkness);
                meThis.ShaderProperty(prpLiteColoringBrightness, lang_coloring_brightness);
                meThis.ShaderProperty(prpLiteColoringEmission, lang_coloring_emission);
                meThis.ShaderProperty(prpLiteColoringPosterization, lang_coloring_posterization);
                meThis.ShaderProperty(prpLiteColoringColorGrading, lang_coloring_colorgrading);
                if (prpLiteColoringColorGrading.floatValue != 0) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty(prpLiteColoringColorGradingTone, lang_coloring_colorgradingtone);
                    EditorGUI.indentLevel--;
                }
                JuneLiteLib.endSection();
            }

            // distortion tab
            tabDistortion = JuneLiteLib.makeFoldout(tabDistortion, "Distortion" + toggleDistortion, 18);
            if (tabDistortion)
            {
                JuneLiteLib.startSection();
                prpLiteDistortionModule = ShaderGUI.FindProperty("_LiteDistortionModule", propertiesThis);
                prpLiteDistortionStyle = ShaderGUI.FindProperty("_LiteDistortionStyle", propertiesThis);
                prpLiteDistortionPowerX = ShaderGUI.FindProperty("_LiteDistortionPowerX", propertiesThis);
                prpLiteDistortionPowerY = ShaderGUI.FindProperty("_LiteDistortionPowerY", propertiesThis);
                prpLiteDistortionSpeedX = ShaderGUI.FindProperty("_LiteDistortionSpeedX", propertiesThis);
                prpLiteDistortionSpeedY = ShaderGUI.FindProperty("_LiteDistortionSpeedY", propertiesThis);
                prpLiteDistortionTexture = ShaderGUI.FindProperty("_LiteDistortionTexture", propertiesThis);
                prpLiteDistortionTextureScale = ShaderGUI.FindProperty("_LiteDistortionTextureScale", propertiesThis);
                prpLiteDistortionWobble = ShaderGUI.FindProperty("_LiteDistortionWobble", propertiesThis);
                prpLiteDistortionWobblePower = ShaderGUI.FindProperty("_LiteDistortionWobblePower", propertiesThis);
                prpLiteDistortionWobbleSpeed = ShaderGUI.FindProperty("_LiteDistortionWobbleSpeed", propertiesThis);
                prpLiteDistortionWobbleCoverage = ShaderGUI.FindProperty("_LiteDistortionWobbleCoverage", propertiesThis);
                meThis.ShaderProperty(prpLiteDistortionModule, lang_distortion_enable);
                prpLiteDistortionStyle.floatValue = (float)(enumLiteDistortion)EditorGUILayout.EnumPopup(lang_distortion_style, (enumLiteDistortion)prpLiteDistortionStyle.floatValue);
                meThis.ShaderProperty(prpLiteDistortionPowerX, lang_distortion_powerx);
                meThis.ShaderProperty(prpLiteDistortionPowerY, lang_distortion_powery);
                meThis.ShaderProperty(prpLiteDistortionSpeedX, lang_distortion_speedx);
                meThis.ShaderProperty(prpLiteDistortionSpeedY, lang_distortion_speedy);
                if (prpLiteDistortionStyle.floatValue == (float)enumLiteDistortion.Texture) {
                    meThis.ShaderProperty(prpLiteDistortionTexture, lang_distortion_texture);
                    meThis.ShaderProperty(prpLiteDistortionTextureScale, lang_distortion_texturescale);
                }
                prpLiteDistortionWobble.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_distortion_addinwobble, (enumLiteToggle)prpLiteDistortionWobble.floatValue);
                if (prpLiteDistortionWobble.floatValue != 0) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty(prpLiteDistortionWobblePower, lang_distortion_wobblepower);
                    meThis.ShaderProperty(prpLiteDistortionWobbleSpeed, lang_distortion_wobblespeed);
                    meThis.ShaderProperty(prpLiteDistortionWobbleCoverage, lang_distortion_wobblecoverage);
                    EditorGUI.indentLevel--;
                }
                JuneLiteLib.endSection();
            }

            // filter tab
            tabFilter = JuneLiteLib.makeFoldout(tabFilter, "Filters" + toggleFilters, 18);
            if (tabFilter)
            {
                JuneLiteLib.startSection();
                prpLiteFilterModule = ShaderGUI.FindProperty("_LiteFilterModule", propertiesThis);
                meThis.ShaderProperty(prpLiteFilterModule, lang_filter_enable);
                tabSubFilterVignette = JuneLiteLib.makeFoldoutSub(tabSubFilterVignette, "Vignette");
                if (tabSubFilterVignette) {
                    prpLiteFilterVignette = ShaderGUI.FindProperty("_LiteFilterVignette", propertiesThis);
                    prpLiteFilterVignettePower = ShaderGUI.FindProperty("_LiteFilterVignettePower", propertiesThis);
                    prpLiteFilterVignetteColor = ShaderGUI.FindProperty("_LiteFilterVignetteColor", propertiesThis);
                    prpLiteFilterVignette.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Vignette", (enumLiteToggle)prpLiteFilterVignette.floatValue);
                    meThis.ShaderProperty(prpLiteFilterVignettePower, lang_vignette_power);
                    meThis.ShaderProperty(prpLiteFilterVignetteColor, lang_vignette_color);
                    EditorGUILayout.Space();
                }
                tabSubFilterColorCrush = JuneLiteLib.makeFoldoutSub(tabSubFilterColorCrush, "Color Crush");
                if (tabSubFilterColorCrush) {
                    prpLiteFilterColorCrush = ShaderGUI.FindProperty("_LiteFilterColorCrush", propertiesThis);
                    prpLiteFilterColorCrushPower = ShaderGUI.FindProperty("_LiteFilterColorCrushPower", propertiesThis);
                    prpLiteFilterColorCrush.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_colorcrush_enable, (enumLiteToggle)prpLiteFilterColorCrush.floatValue);
                    meThis.ShaderProperty(prpLiteFilterColorCrushPower, lang_colorcrush_power);
                    EditorGUILayout.Space();
                }
                tabSubFilterDuotone = JuneLiteLib.makeFoldoutSub(tabSubFilterDuotone, "Duotone");
                if (tabSubFilterDuotone) {
                    prpLiteFilterDuotone = ShaderGUI.FindProperty("_LiteFilterDuotone", propertiesThis);
                    prpLiteFilterDuotoneTransparency = ShaderGUI.FindProperty("_LiteFilterDuotoneTransparency", propertiesThis);
                    prpLiteFilterDuotoneColorOne = ShaderGUI.FindProperty("_LiteFilterDuotoneColorOne", propertiesThis);
                    prpLiteFilterDuotoneColorTwo = ShaderGUI.FindProperty("_LiteFilterDuotoneColorTwo", propertiesThis);
                    prpLiteFilterDuotoneThreshold = ShaderGUI.FindProperty("_LiteFilterDuotoneThreshold", propertiesThis);
                    prpLiteFilterDuotone.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_duotone_enable, (enumLiteToggle)prpLiteFilterDuotone.floatValue);
                    meThis.ShaderProperty(prpLiteFilterDuotoneTransparency, lang_duotone_transparency);
                    meThis.ShaderProperty(prpLiteFilterDuotoneColorOne, lang_duotone_colorone);
                    meThis.ShaderProperty(prpLiteFilterDuotoneColorTwo, lang_duotone_colortwo);
                    meThis.ShaderProperty(prpLiteFilterDuotoneThreshold, lang_duotone_threshold);
                    EditorGUILayout.Space();
                }
                tabSubFilterRainbow = JuneLiteLib.makeFoldoutSub(tabSubFilterRainbow, "Rainbow");
                if (tabSubFilterRainbow) {
                    prpLiteFilterRainbow = ShaderGUI.FindProperty("_LiteFilterRainbow", propertiesThis);
                    prpLiteFilterRainbowSaturation = ShaderGUI.FindProperty("_LiteFilterRainbowSaturation", propertiesThis);
                    prpLiteFilterRainbowSpeed = ShaderGUI.FindProperty("_LiteFilterRainbowSpeed", propertiesThis);
                    prpLiteFilterRainbow.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_rainbow_enable, (enumLiteToggle)prpLiteFilterRainbow.floatValue);
                    meThis.ShaderProperty(prpLiteFilterRainbowSaturation, lang_rainbow_saturation);
                    meThis.ShaderProperty(prpLiteFilterRainbowSpeed, lang_rainbow_speed);
                    EditorGUILayout.Space();
                }
                tabSubFilterFilm = JuneLiteLib.makeFoldoutSub(tabSubFilterFilm, "Film");
                if (tabSubFilterFilm) {
                    prpLiteFilterFilm = ShaderGUI.FindProperty("_LiteFilterFilm", propertiesThis);
                    prpLiteFilterFilmAmount = ShaderGUI.FindProperty("_LiteFilterFilmAmount", propertiesThis);
                    prpLiteFilterFilm.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_film_enable, (enumLiteToggle)prpLiteFilterFilm.floatValue);
                    meThis.ShaderProperty(prpLiteFilterFilmAmount, lang_film_amount);
                    EditorGUILayout.Space();
                }
                tabSubFilterGrain = JuneLiteLib.makeFoldoutSub(tabSubFilterGrain, "Grain");
                if (tabSubFilterGrain) {
                    prpLiteFilterGrain = ShaderGUI.FindProperty("_LiteFilterGrain", propertiesThis);
                    prpLiteFilterGrainAmount = ShaderGUI.FindProperty("_LiteFilterGrainAmount", propertiesThis);
                    prpLiteFilterGrainColor = ShaderGUI.FindProperty("_LiteFilterGrainColor", propertiesThis);
                    prpLiteFilterGrain.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_grain_enable, (enumLiteToggle)prpLiteFilterGrain.floatValue);
                    meThis.ShaderProperty(prpLiteFilterGrainAmount, lang_grain_amount);
                    meThis.ShaderProperty(prpLiteFilterGrainColor, lang_grain_color);
                    EditorGUILayout.Space();
                }
                tabSubFilterVHS = JuneLiteLib.makeFoldoutSub(tabSubFilterVHS, "VHS");
                if (tabSubFilterVHS) {
                    prpLiteFilterVHS = ShaderGUI.FindProperty("_LiteFilterVHS", propertiesThis);
                    prpLiteFilterVHSAmount = ShaderGUI.FindProperty("_LiteFilterVHSAmount", propertiesThis);
                    prpLiteFilterVHS.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_vhs_amount, (enumLiteToggle)prpLiteFilterVHS.floatValue);
                    meThis.ShaderProperty(prpLiteFilterVHSAmount, lang_vhs_amount);
                    EditorGUILayout.Space();
                }
                tabSubFilterGradient = JuneLiteLib.makeFoldoutSub(tabSubFilterGradient, "Gradient");
                if (tabSubFilterGradient) {
                    prpLiteFilterGradient = ShaderGUI.FindProperty("_LiteFilterGradient", propertiesThis);
                    prpLiteFilterGradientLHS = ShaderGUI.FindProperty("_LiteFilterGradientLHS", propertiesThis);
                    prpLiteFilterGradientRHS = ShaderGUI.FindProperty("_LiteFilterGradientRHS", propertiesThis);
                    prpLiteFilterGradientTransparency = ShaderGUI.FindProperty("_LiteFilterGradientTransparency", propertiesThis);
                    prpLiteFilterGradient.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_gradient_enable, (enumLiteToggle)prpLiteFilterGradient.floatValue);
                    meThis.ShaderProperty(prpLiteFilterGradientLHS, lang_gradient_lhs);
                    meThis.ShaderProperty(prpLiteFilterGradientRHS, lang_gradient_rhs);
                    meThis.ShaderProperty(prpLiteFilterGradientTransparency, lang_gradient_transparency);
                    EditorGUILayout.Space();
                }
                tabSubFilterOutline = JuneLiteLib.makeFoldoutSub(tabSubFilterOutline, "Outline");
                if (tabSubFilterOutline) {
                    prpLiteFilterOutline = ShaderGUI.FindProperty("_LiteFilterOutline", propertiesThis);
                    prpLiteFilterOutlineWidth = ShaderGUI.FindProperty("_LiteFilterOutlineWidth", propertiesThis);
                    prpLiteFilterOutlineTolerance = ShaderGUI.FindProperty("_LiteFilterOutlineTolerance", propertiesThis);
                    prpLiteFilterOutlineColor = ShaderGUI.FindProperty("_LiteFilterOutlineColor", propertiesThis);
                    prpLiteFilterOutline.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_outline_enable, (enumLiteToggle)prpLiteFilterOutline.floatValue);
                    meThis.ShaderProperty(prpLiteFilterOutlineWidth, lang_outline_width);
                    meThis.ShaderProperty(prpLiteFilterOutlineTolerance, lang_outline_tolerance);
                    meThis.ShaderProperty(prpLiteFilterOutlineColor, lang_outline_color);
                    EditorGUILayout.Space();
                }
                tabSubFilterAstral = JuneLiteLib.makeFoldoutSub(tabSubFilterAstral, "Astral");
                if (tabSubFilterAstral) {
                    prpLiteFilterAstral = ShaderGUI.FindProperty("_LiteFilterAstral", propertiesThis);
                    prpLiteFilterAstralZoom = ShaderGUI.FindProperty("_LiteFilterAstralZoom", propertiesThis);
                    prpLiteFilterAstralZoomTransparency = ShaderGUI.FindProperty("_LiteFilterAstralTransparency", propertiesThis);
                    prpLiteFilterAstralZoomColor = ShaderGUI.FindProperty("_LiteFilterAstralColor", propertiesThis);
                    prpLiteFilterAstral.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_astral_enable, (enumLiteToggle)prpLiteFilterAstral.floatValue);
                    meThis.ShaderProperty(prpLiteFilterAstralZoom, lang_astral_zoom);
                    meThis.ShaderProperty(prpLiteFilterAstralZoomTransparency, lang_astral_transparency);
                    meThis.ShaderProperty(prpLiteFilterAstralZoomColor, lang_astral_color);
                    EditorGUILayout.Space();
                }
                tabSubFilterNeon = JuneLiteLib.makeFoldoutSub(tabSubFilterNeon, "Neon");
                if (tabSubFilterNeon) {
                    prpLiteFilterNeon = ShaderGUI.FindProperty("_LiteFilterNeon", propertiesThis);
                    prpLiteFilterNeonWidth = ShaderGUI.FindProperty("_LiteFilterNeonWidth", propertiesThis);
                    prpLiteFilterNeonTransparency = ShaderGUI.FindProperty("_LiteFilterNeonTransparency", propertiesThis);
                    prpLiteFilterNeonHue = ShaderGUI.FindProperty("_LiteFilterNeonHue", propertiesThis);
                    prpLiteFilterNeon.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_neon_enable, (enumLiteToggle)prpLiteFilterNeon.floatValue);
                    meThis.ShaderProperty(prpLiteFilterNeonWidth, lang_neon_width);
                    meThis.ShaderProperty(prpLiteFilterNeonTransparency, lang_neon_transparency);
                    meThis.ShaderProperty(prpLiteFilterNeonHue, lang_neon_hue);
                    EditorGUILayout.Space();
                }
                JuneLiteLib.endSection();
            }

            // fog tab
            tabFog = JuneLiteLib.makeFoldout(tabFog, "Fog" + toggleFog, 18);
            if (tabFog) {
                JuneLiteLib.startSection();
                MaterialProperty prpFogModule = ShaderGUI.FindProperty("_LiteFogModule", propertiesThis);
                MaterialProperty prpFogDensity = ShaderGUI.FindProperty("_LiteFogDensity", propertiesThis);
                MaterialProperty prpFogDistribution = ShaderGUI.FindProperty("_LiteFogDistribution", propertiesThis);
                MaterialProperty prpFogColor = ShaderGUI.FindProperty("_LiteFogColor", propertiesThis);
                MaterialProperty prpFogSafespace = ShaderGUI.FindProperty("_LiteFogSafespace", propertiesThis);
                MaterialProperty prpFogSafespaceSize = ShaderGUI.FindProperty("_LiteFogSafespaceSize", propertiesThis);
                meThis.ShaderProperty(prpFogModule, lang_fog_enable );
                meThis.ShaderProperty(prpFogDensity, lang_fog_density);
                meThis.ShaderProperty(prpFogDistribution, lang_fog_distribution);
                meThis.ShaderProperty(prpFogColor, lang_fog_color);
                prpFogSafespace.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_fog_safespace, (enumLiteToggle)prpFogSafespace.floatValue);
                if (prpFogSafespace.floatValue == 1) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty(prpFogSafespaceSize, lang_fog_safespacesize);
                    EditorGUI.indentLevel--;
                }
                JuneLiteLib.doNotifcation(true, lang_ui_notice, lang_ui_depth, 12); 
                JuneLiteLib.endSection();
            }

            // glitch tab
            tabGlitch = JuneLiteLib.makeFoldout(tabGlitch, "Glitch" + toggleGlitch, 18);
            if (tabGlitch) {
                JuneLiteLib.startSection();
                MaterialProperty prpGlitchModule = ShaderGUI.FindProperty("_LiteGlitchModule", propertiesThis);
                MaterialProperty prpGlitchScale = ShaderGUI.FindProperty("_LiteGlitchScale", propertiesThis);
                MaterialProperty prpGlitchAmount = ShaderGUI.FindProperty("_LiteGlitchAmount", propertiesThis);
                MaterialProperty prpGlitchUVs = ShaderGUI.FindProperty("_LiteGlitchUVs", propertiesThis);
                MaterialProperty prpGlichChromatic = ShaderGUI.FindProperty("_LiteGlitchChromatic", propertiesThis);
                meThis.ShaderProperty(prpGlitchModule, lang_glitch_enable);
                meThis.ShaderProperty(prpGlitchScale, lang_glitch_scale);
                meThis.ShaderProperty(prpGlitchAmount, lang_glitch_amount);
                meThis.ShaderProperty(prpGlitchUVs, lang_glitch_uvs);
                meThis.ShaderProperty(prpGlichChromatic, lang_glitch_chromatic);
                JuneLiteLib.endSection();
            }
            
            // overlay tab
            tabOverlay = JuneLiteLib.makeFoldout(tabOverlay, "Overlay" + toggleOverlay, 18);
            if (tabOverlay)
            {
                JuneLiteLib.startSection();
                prpLiteOverlayModule = ShaderGUI.FindProperty("_LiteOverlayModule", propertiesThis);
                prpLiteOverlayTexture = ShaderGUI.FindProperty("_LiteOverlayTexture", propertiesThis);
                prpLiteOverlayTransparency = ShaderGUI.FindProperty("_LiteOverlayTransparency", propertiesThis);
                prpLiteOverlaySizeX = ShaderGUI.FindProperty("_LiteOverlaySizeX", propertiesThis);
                prpLiteOverlaySizeY = ShaderGUI.FindProperty("_LiteOverlaySizeY", propertiesThis);
                prpLiteOverlayOffsetX = ShaderGUI.FindProperty("_LiteOverlayOffsetX", propertiesThis);
                prpLiteOverlayOffsetY = ShaderGUI.FindProperty("_LiteOverlayOffsetY", propertiesThis);
                MaterialProperty prpLiteOverlayAnimated = ShaderGUI.FindProperty("_LiteOverlayAnimated", propertiesThis);
                MaterialProperty prpLiteOverlayFramesX = ShaderGUI.FindProperty("_LiteOverlayFramesX", propertiesThis);
                MaterialProperty prpLiteOverlayFramesY = ShaderGUI.FindProperty("_LiteOverlayFramesY", propertiesThis);
                MaterialProperty prpLiteOverlayFrames = ShaderGUI.FindProperty("_LiteOverlayFrames", propertiesThis);
                MaterialProperty prpLiteOverlaySpeed = ShaderGUI.FindProperty("_LiteOverlaySpeed", propertiesThis);
                MaterialProperty prpLiteOverlayScrub = ShaderGUI.FindProperty("_LiteOverlayScrub", propertiesThis);
                meThis.ShaderProperty (prpLiteOverlayModule, lang_overlay_enable);
                meThis.ShaderProperty (prpLiteOverlayTexture, lang_overlay_texture);
                meThis.ShaderProperty (prpLiteOverlayTransparency, lang_overlay_transparency);
                meThis.ShaderProperty (prpLiteOverlaySizeX, lang_overlay_sizex);
                meThis.ShaderProperty (prpLiteOverlaySizeY, lang_overlay_sizey);
                meThis.ShaderProperty (prpLiteOverlayOffsetX, lang_overlay_offsetx);
                meThis.ShaderProperty (prpLiteOverlayOffsetY, lang_overlay_offsety);
                prpLiteOverlayAnimated.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_overlay_animated, (enumLiteToggle)prpLiteOverlayAnimated.floatValue);
                if (prpLiteOverlayAnimated.floatValue == 1) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty (prpLiteOverlayFramesX, lang_overlay_framesx);
                    meThis.ShaderProperty (prpLiteOverlayFramesY, lang_overlay_framesy);
                    meThis.ShaderProperty (prpLiteOverlayFrames, lang_overlay_frames);
                    meThis.ShaderProperty (prpLiteOverlaySpeed, lang_overlay_speed);
                    meThis.ShaderProperty (prpLiteOverlayScrub, lang_overlay_scrub);
                    EditorGUI.indentLevel--;
                }
                JuneLiteLib.endSection();
            }

            // uv manipulation tab
            tabUVManipulation = JuneLiteLib.makeFoldout(tabUVManipulation, "UV Manipulation" + toggleUVManipulation, 18);
            if (tabUVManipulation)
            {
                JuneLiteLib.startSection();
                prpLiteUVManipulationModule = ShaderGUI.FindProperty("_LiteUVManipulationModule", propertiesThis);
                meThis.ShaderProperty (prpLiteUVManipulationModule, lang_uvmanipulation_enable);
                tabSubUVTransformation = JuneLiteLib.makeFoldoutSub(tabSubUVTransformation, "Transformation");
                if (tabSubUVTransformation) {
                    prpLiteUVManipulationTransformationSlantTL = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantTL", propertiesThis);
                    prpLiteUVManipulationTransformationSlantTR = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantTR", propertiesThis);
                    prpLiteUVManipulationTransformationSlantBL = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantBL", propertiesThis);
                    prpLiteUVManipulationTransformationSlantBR = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantBR", propertiesThis);
                    prpLiteUVManipulationTransformationFlipX = ShaderGUI.FindProperty("_LiteUVManipulationTransformationFlipX", propertiesThis);
                    prpLiteUVManipulationTransformationFlipY = ShaderGUI.FindProperty("_LiteUVManipulationTransformationFlipY", propertiesThis);
                    prpLiteUVManipulationTransformationStretchX = ShaderGUI.FindProperty("_LiteUVManipulationTransformationStretchX", propertiesThis);
                    prpLiteUVManipulationTransformationStretchY = ShaderGUI.FindProperty("_LiteUVManipulationTransformationStretchY", propertiesThis);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantTL, lang_transformation_slanttopleft);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantTR, lang_transformation_slanttopright);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantBL, lang_transformation_slantbottomleft);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantBR, lang_transformation_slantbottomright);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationFlipX, lang_transformation_flipx);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationFlipY, lang_transformation_flipy);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationStretchX, lang_transformation_stretchx);
                    meThis.ShaderProperty(prpLiteUVManipulationTransformationStretchY, lang_transformation_stretchy);
                    EditorGUILayout.Space();
                }
                tabSubUVMove = JuneLiteLib.makeFoldoutSub(tabSubUVMove, "Movement");
                if (tabSubUVMove)
                {
                    prpLiteUVManipulationMoveX = ShaderGUI.FindProperty("_LiteUVManipulationMoveX", propertiesThis);
                    prpLiteUVManipulationMoveY = ShaderGUI.FindProperty("_LiteUVManipulationMoveY", propertiesThis);
                    meThis.ShaderProperty(prpLiteUVManipulationMoveX, lang_movement_movex);
                    meThis.ShaderProperty(prpLiteUVManipulationMoveY, lang_movement_movey);
                    EditorGUILayout.Space();
                }
                tabSubUVShake = JuneLiteLib.makeFoldoutSub(tabSubUVShake, "Shake");
                if (tabSubUVShake)
                {
                    prpLiteUVManipulationShakeStyle = ShaderGUI.FindProperty("_LiteUVManipulationShakeStyle", propertiesThis);
                    prpLiteUVManipulationShakePowerX = ShaderGUI.FindProperty("_LiteUVManipulationShakePowerX", propertiesThis);
                    prpLiteUVManipulationShakePowerY = ShaderGUI.FindProperty("_LiteUVManipulationShakePowerY", propertiesThis);
                    prpLiteUVManipulationShakeSpeedX = ShaderGUI.FindProperty("_LiteUVManipulationShakeSpeedX", propertiesThis);
                    prpLiteUVManipulationShakeSpeedY = ShaderGUI.FindProperty("_LiteUVManipulationShakeSpeedY", propertiesThis);
                    prpLiteUVManipulationShakeStyle.floatValue = (float)(enumLiteShake)EditorGUILayout.EnumPopup(lang_shake_style, (enumLiteShake)prpLiteUVManipulationShakeStyle.floatValue);
                    meThis.ShaderProperty(prpLiteUVManipulationShakePowerX, lang_shake_powerx);
                    meThis.ShaderProperty(prpLiteUVManipulationShakePowerY, lang_shake_powery);
                    meThis.ShaderProperty(prpLiteUVManipulationShakeSpeedX, lang_shake_speedx);
                    meThis.ShaderProperty(prpLiteUVManipulationShakeSpeedY, lang_shake_speedy);
                    EditorGUILayout.Space();
                }
                tabSubUVPixelation = JuneLiteLib.makeFoldoutSub(tabSubUVPixelation, "Pixelation");
                if (tabSubUVPixelation)
                {
                    prpLiteUVManipulationPixelation = ShaderGUI.FindProperty("_LiteUVManipulationPixelation", propertiesThis);
                    prpLiteUVManipulationPixelationPower = ShaderGUI.FindProperty("_LiteUVManipulationPixelationPower", propertiesThis);
                    prpLiteUVManipulationPixelation.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_pixelation_enable, (enumLiteToggle)prpLiteUVManipulationPixelation.floatValue);
                    meThis.ShaderProperty(prpLiteUVManipulationPixelationPower, lang_pixelation_power);
                    EditorGUILayout.Space();
                }
                tabSubUVRotation = JuneLiteLib.makeFoldoutSub(tabSubUVRotation, "Rotation");
                if (tabSubUVRotation)
                {
                    prpLiteUVManipulationRotation = ShaderGUI.FindProperty("_LiteUVManipulationRotation", propertiesThis);
                    prpLiteUVManipulationRotationAngle = ShaderGUI.FindProperty("_LiteUVManipulationRotationAngle", propertiesThis);
                    prpLiteUVManipulationRotation.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_rotation_enable, (enumLiteToggle)prpLiteUVManipulationRotation.floatValue);
                    meThis.ShaderProperty(prpLiteUVManipulationRotationAngle, lang_rotation_angle);
                    EditorGUILayout.Space();
                }
                tabSubUVSpherize = JuneLiteLib.makeFoldoutSub(tabSubUVSpherize, "Spherize");
                if (tabSubUVSpherize)
                {
                    prpLiteUVManipulationSpherize = ShaderGUI.FindProperty("_LiteUVManipulationSpherize", propertiesThis);
                    prpLiteUVManipulationSpherizePower = ShaderGUI.FindProperty("_LiteUVManipulationSpherizePower", propertiesThis);
                    prpLiteUVManipulationSpherize.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_spherize_enable, (enumLiteToggle)prpLiteUVManipulationSpherize.floatValue);
                    meThis.ShaderProperty(prpLiteUVManipulationSpherizePower, lang_spherize_power);
                    EditorGUILayout.Space();
                }
                JuneLiteLib.endSection();
            }

            // zoom tab
            tabZoom = JuneLiteLib.makeFoldout(tabZoom, "Zoom" + toggleZoom, 18);
            if (tabZoom)
            {
                JuneLiteLib.startSection();
                prpLiteZoomModule = ShaderGUI.FindProperty("_LiteZoomModule", propertiesThis);
                prpLiteZoomPower = ShaderGUI.FindProperty("_LiteZoomPower", propertiesThis);
                prpLiteZoomRangeStyle = ShaderGUI.FindProperty("_LiteZoomRangeStyle", propertiesThis);
                prpLiteZoomRangeStart = ShaderGUI.FindProperty("_LiteZoomRangeStart", propertiesThis);
                prpLiteZoomRangeEnd = ShaderGUI.FindProperty("_LiteZoomRangeEnd", propertiesThis);
                meThis.ShaderProperty(prpLiteZoomModule, lang_zoom_enable);
                meThis.ShaderProperty(prpLiteZoomPower, lang_zoom_power);
                prpLiteZoomRangeStyle.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup(lang_zoom_customrange, (enumLiteToggle)prpLiteZoomRangeStyle.floatValue);
                if (prpLiteZoomRangeStyle.floatValue != 0) {
                    EditorGUI.indentLevel++;
                    meThis.ShaderProperty(prpLiteZoomRangeStart, lang_zoom_rangestart);
                    meThis.ShaderProperty(prpLiteZoomRangeEnd, lang_zoom_rangeend);
                    EditorGUI.indentLevel--;
                }
                JuneLiteLib.endSection();
            }

            // divider for bottom half
            JuneLiteLib.makeDivider();

            // audiolink tab
            tabAudioLink = JuneLiteLib.makeFoldout(tabAudioLink, "AudioLink" + toggleAudioLink, 18);
            if (tabAudioLink)
            {
                JuneLiteLib.startSection();
                prpLiteAudioLinkModule = ShaderGUI.FindProperty("_LiteAudioLinkModule", propertiesThis);
                prpLiteAudioLinkBand = ShaderGUI.FindProperty("_LiteAudioLinkBand", propertiesThis);
                prpLiteAudioLinkPower = ShaderGUI.FindProperty("_LiteAudioLinkPower", propertiesThis);
                prpLiteAudioLinkMin = ShaderGUI.FindProperty("_LiteAudioLinkMin", propertiesThis);
                prpLiteAudioLinkMax = ShaderGUI.FindProperty("_LiteAudioLinkMax", propertiesThis);
                meThis.ShaderProperty(prpLiteAudioLinkModule, lang_audiolink_enable);
                prpLiteAudioLinkBand.floatValue = (float)(enumLiteAudioLink)EditorGUILayout.EnumPopup(lang_audiolink_band, (enumLiteAudioLink)prpLiteAudioLinkBand.floatValue);
                meThis.ShaderProperty(prpLiteAudioLinkPower, lang_audiolink_power);
                meThis.ShaderProperty(prpLiteAudioLinkMin, lang_audiolink_min);
                meThis.ShaderProperty(prpLiteAudioLinkMax, lang_audiolink_max);
                JuneLiteLib.endSection();
            }

            // settings tab
            tabExtraSettings = JuneLiteLib.makeFoldout(tabExtraSettings, "Settings", 18);
            if (tabExtraSettings) {
                EditorGUILayout.BeginVertical("GroupBox");
                intUiLanguages = (int)(enumUiLanguages)EditorGUILayout.EnumPopup(strUiLanguage, (enumUiLanguages)intUiLanguages);
                if (GUILayout.Button(strUiSaveSettings))
                {
                    saveSettings(intUiLanguages);
                    loadSettings();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }

            // <3
            EditorGUILayout.Space(5);
            JuneLiteLib.makeBanner("Images/JuneLiteBanner_JunePro");
            EditorGUILayout.Space(15);

            // buttons
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(strUiButtonWebsite))
            {
                string strWebsite = "https://luka.moe/go/";
                if (!JuneLiteLib.openWebpage(strWebsite))
                {
                    JuneLiteLib.makePopup("Website", strWebsite);
                }
            }
            if (GUILayout.Button(strUiButtonGithub))
            {
                string strGithub = "https://luka.moe/go/github";
                if (!JuneLiteLib.openWebpage(strGithub))
                {
                    JuneLiteLib.makePopup("Github", strGithub);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(strUiButtonGumroad))
            {
                string strGumroad = "https://luka.moe/go/gumroad";
                if (!JuneLiteLib.openWebpage(strGumroad))
                {
                    JuneLiteLib.makePopup("Gumroad", strGumroad);
                }
            }
            if (GUILayout.Button(strUiButtonBooth))
            {
                string strBooth = "https://luka.moe/go/booth";
                if (!JuneLiteLib.openWebpage(strBooth))
                {
                    JuneLiteLib.makePopup("Booth:", strBooth);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(strUiButtonJinxxy))
            {
                string strJinxxy = "https://luka.moe/go/jinxxy";
                if (!JuneLiteLib.openWebpage(strJinxxy))
                {
                    JuneLiteLib.makePopup("Jinxxy", strJinxxy);
                }
            }
            if (GUILayout.Button(strUiButtonPayhip))
            {
                string strPayhip = "https://luka.moe/go/payhip";
                if (!JuneLiteLib.openWebpage(strPayhip))
                {
                    JuneLiteLib.makePopup("Payhip", strPayhip);
                }
            }
            EditorGUILayout.EndHorizontal();


            // footer
            EditorGUILayout.Space();
            JuneLiteLib.makeText("june lite " + JuneLiteLib.getHeart() + " version " + strVersion, styFooter);
            JuneLiteLib.makeText("artwork provided by @lumiechuu " + JuneLiteLib.getHeart() + " see <b>license</b> for more", styFooter);
            JuneLiteLib.makeText("you are using the <b>free and public version</b>", styFooter);
            EditorGUI.EndChangeCheck();

        }

    }

}

#endif