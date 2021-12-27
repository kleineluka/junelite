#if UNITY_EDITOR

using UnityEngine;
using System;
using System.Net;
using UnityEditor;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class LukaJuneLiteUIOne : ShaderGUI
{

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
    MaterialProperty prpLiteUVManipulationGlitch = null;
    MaterialProperty prpLiteUVManipulationGlitchAmount = null;
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
    MaterialProperty prpLiteFilterGlitch = null;
    MaterialProperty prpLiteFilterGlitchAmount = null;
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

    // setting up strings
    string strLiteRenderingFalloffStart = "Falloff Start";
    string strLiteRenderingFalloffEnd = "Falloff End";
    string strLiteRenderingOOB = "Out of Bounds";
    string strLiteRenderingPower = "Shader Power";
    string strLiteRenderingQuality = "High Quality Mode";
    string strLiteColoringModule = "Enable Coloring Module";
    string strLiteColoringRGBMultiply = "RGB Multiplication";
    string strLiteColoringRGBOverlay = "RGB Overlay";
    string strLiteColoringRGBOverlayTransparency = "RGB Overlay Transparency";
    string strLiteColoringHSVStyle = "HSV Style";
    string strLiteColoringHSVh = "HSV Hue";
    string strLiteColoringHSVs = "HSV Saturation";
    string strLiteColoringHSVv = "HSV Value";
    string strLiteColoringInvert = "Invert";
    string strLiteColoringDrain = "Color Drain";
    string strLiteColoringDarkness = "Darkness";
    string strLiteColoringBrightness = "Brightness";
    string strLiteColoringEmission = "World Emission";
    string strLiteColoringPosterization = "Posterization";
    string strLiteColoringColorGrading = "Color Grading";
    string strLiteColoringColorGradingTone = "Color Grading Tone";
    string strLiteBlurModule = "Enable Blur Module";
    string strLiteBlurStyle = "Blur Style";
    string strLiteBlurPower = "Blur Power";
    string strLiteBlurRadius = "Blur Radius";
    string strLiteBlurTransparency = "Blur Transparency";
    string strLiteBlurColor = "Blur Color";
    string strLiteDistortionModule = "Enable Distortion Module";
    string strLiteDistortionStyle = "Distortion Style";
    string strLiteDistortionPowerX = "Distoriton X Power";
    string strLiteDistortionPowerY = "Distoriton Y Power";
    string strLiteDistortionSpeedX = "Distoriton X Speed";
    string strLiteDistortionSpeedY = "Distoriton Y Speed";
    string strLiteDistortionTexture = "Distortion Texture";
    string strLiteDistortionTextureScale = "Distortion Texture Scale";
    string strLiteDistortionWobble = "Enable Extra Wobble";
    string strLiteDistortionWobblePower = "Wobble Power";
    string strLiteDistortionWobbleSpeed = "Wobble Speed";
    string strLiteDistortionWobbleCoverage = "Wobble Coverage";
    string strLiteBorderModule = "Enable Border Module";
    string strLiteBorderStyle = "Border Style";
    string strLiteBorderPower = "Border Power";
    string strLiteBorderColor = "Border Color";
    string strLiteBorderSoften = "Border Soften";
    string strLiteOverlayModule = "Enable Overlay Module";
    string strLiteOverlayTexture = "Overlay Texture";
    string strLiteOverlaySizeX = "Overlay Size X";
    string strLiteOverlaySizeY = "Overlay Size Y";
    string strLiteOverlayOffsetX = "Overlay Offset X";
    string strLiteOverlayOffsetY = "Overlay Offset Y";
    string strLiteOverlayTransparency = "Overlay Transparency";
    string strLiteUVManipulationModule = "Enable UV Manipulation Module";
    string strLiteUVManipulationTransformationSlantTL = "Slant Top Left";
    string strLiteUVManipulationTransformationSlantTR = "Slant Top Right";
    string strLiteUVManipulationTransformationSlantBL = "Slant Bottom Left";
    string strLiteUVManipulationTransformationSlantBR = "Slant Bottom Right";
    string strLiteUVManipulationTransformationFlipX = "Flip X";
    string strLiteUVManipulationTransformationFlipY = "Flip Y";
    string strLiteUVManipulationTransformationStretchX = "Stretch X";
    string strLiteUVManipulationTransformationStretchY = "Stretch Y";
    string strLiteUVManipulationMoveX = "Move X";
    string strLiteUVManipulationMoveY = "Move Y";
    string strLiteUVManipulationShakeStyle = "Shake Style";
    string strLiteUVManipulationShakePowerX = "Shake X Power";
    string strLiteUVManipulationShakePowerY = "Shake Y Power";
    string strLiteUVManipulationShakeSpeedX = "Shake X Speed";
    string strLiteUVManipulationShakeSpeedY = "Shake Y Speed";
    string strLiteUVManipulationPixelation = "Pixelation";
    string strLiteUVManipulationPixelationPower = "Pixelation Power";
    string strLiteUVManipulationRotation = "Rotation";
    string strLiteUVManipulationRotationAngle = "Rotation Angle";
    string strLiteUVManipulationSpherize = "Spherize";
    string strLiteUVManipulationSpherizePower = "Spherize Power";
    string strLiteUVManipulationGlitch = "UV Glitch";
    string strLiteUVManipulationGlitchAmount = "UV Glitch Amount";
    string strLiteFilterModule = "Enable Filter Module";
    string strLiteFilterVignette = "Vignette";
    string strLiteFilterVignettePower = "Vignette Power";
    string strLiteFilterColorCrush = "Color Crush";
    string strLiteFilterColorCrushPower = "Color Crush Power";
    string strLiteFilterDuotone = "Duotone";
    string strLiteFilterDuotoneTransparency = "Duotone Transparency";
    string strLiteFilterDuotoneColorOne = "Duotone Color One";
    string strLiteFilterDuotoneColorTwo = "Duotone Color Two";
    string strLiteFilterDuotoneThreshold = "Duotone Threshold";
    string strLiteFilterRainbow = "Rainbow";
    string strLiteFilterRainbowSaturation = "Rainbow Saturation";
    string strLiteFilterRainbowSpeed = "Rainbow Speed";
    string strLiteFilterFilm = "Film";
    string strLiteFilterFilmAmount = "Film Amount";
    string strLiteFilterGrain = "Grain";
    string strLiteFilterGrainAmount = "Grain Amount";
    string strLiteFilterGrainColor = "Grain Color";
    string strLiteFilterVHS = "VHS";
    string strLiteFilterVHSAmount = "VHS Amount";
    string strLiteFilterGradient = "Gradient";
    string strLiteFilterGradientLHS = "Gradient LHS";
    string strLiteFilterGradientRHS = "Gradient RHS";
    string strLiteFilterGradientTransparency = "Gradient Transparency";
    string strLiteFilterOutline = "Outline";
    string strLiteFilterOutlineWidth = "Outline Width";
    string strLiteFilterOutlineTolerance = "Outline Tolerance";
    string strLiteFilterOutlineColor = "Outline Color";
    string strLiteFilterGlitch = "Color Glitch";
    string strLiteFilterGlitchAmount = "Color Glitch Amount";
    string strLiteFilterAstral = "Astral";
    string strLiteFilterAstralZoom = "Astral Zoom";
    string strLiteFilterAstralTransparency = "Astral Transparency";
    string strLiteFilterAstralColor = "Astral Color";
    string strLiteFilterNeon = "Neon";
    string strLiteFilterNeonWidth = "Neon Width";
    string strLiteFilterNeonTransparency = "Neon Transparency";
    string strLiteFilterNeonHue = "Neon Hue";
    string strLiteZoomModule = "Enable Zoom Module";
    string strLiteZoomRangeStyle = "Zoom Range Style";
    string strLiteZoomRangeStart = "Zoom Range Start";
    string strLiteZoomRangeEnd = "Zoom Range End";

    // setting up extra ui strings
    private static string strVersion = "1.0";
    private static string strUiLanguage = "Language";
    private static string strUiTheme = "Theme";
    private static string strUiLockTheme = "Languages and themes are only avaliable in the pro version!";
    private static string strUiSaveSettings = "Save Settings";
    enum enumUiLanguages { English, Deutsch };
    enum enumUiThemes { Default, Emoji, AndTenMore };
    int intUiLanguages = 0;
    int intUiThemes = 0;
    private static bool boolUiMetaLiteUpdate = false;
    private static string strUiMetaLiteUpdateTitle = "June Lite has an update avaliable!";
    private static string strUiMetaLiteUpdateDescription = "Download it from www.luka.moe/june !";
    private static bool boolUiMetaProUpdate = false;
    private static string strUiMetaProUpdateTitle = "June Pro has an update avaliable!";
    private static string strUiMetaProUpdateDescription = "Purchase it from www.luka.moe/june !";
    private static bool boolUiMetaProDiscount = false;
    private static string strUiMetaProDiscountTitle = "June Pro has a discount avaliable!";
    private static string strUiMetaProDiscountDescription = "Get it now from www.luka.moe/june !";
    private static bool boolUiMetaOtherAd = false;
    private static string strUiMetaOtherAdTitle = "Hey you, check this out!";
    private static string strUiMetaOtherAdDescription = "Check out this cool thing..!";
    private static bool boolUiMetaOtherAnnouncement = false;
    private static string strUiMetaOtherAnnouncementTitle = "Hey you, read this!";
    private static string strUiMetaOtherAnnouncementDescription = "I have something cool to tell you..!";
    private static bool boolUiMetaOtherMOTD = true;
    private static string strUiMetaOtherMOTD = "Hey you! Thanks for using June Lite.";
    private static string strPublicDiscord = "none";
    private static string strUiProTitle = "You are using the <b>free and lite</b> version of June.";
    string strUiProDescription = "Did you know.. the paid version of June has 20x the options, 5x the effects, four scripts including auto-animation and avatar testing, themes, more styles for existing effects, more languages and more language support, more updates, more rendering settings, dozens of presets and much more – all for a one-time purchase! No bullshit Patreon subscription needed.";
    string strUiAdviceForNewPpl = 
        "Hey, welcome to June Lite! Thank you for choosing this shader to learn animation and post-processing with <3\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Do not</b> animate module toggles. Modules determine which code are included in the final shader build to provide the highest amount of optimization, and animating the module toggles would result in multiple shader copies being created and used!\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Do not</b> animate speeds (like on distortion or shake) – it will look choppy. Instead, animate the power options.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Use a <b>cube</b> for the shader and material, not a sphere, plane, or particle system. While you be able to achieve the same look with a sphere, think about how many vertices are in one compared to a simple cube?\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Utilize <b>fall-off</b> (under rendering). Please. Nobody likes global effects.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Make sure to <b>remove the box (or mesh) collider</b> from the cube (or mesh) you are using for the shader.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Do not</b> use the shader to annoy, harm, or in anyway disrupt other users' gameplay.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Do not</b> use multiple copies of the shader at once.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Keep in mind: the more modules and effects you use, the <b>more performance will be impacted</b>.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Buy the pro version of June!</b>\n";
    string strUiLicense = 
        "This license is open to being changed and I reserve every right to change it at any time. All code belongs to Luka Song, also known as luka, luka song, lukasong, lukazoeysong, zoey, luka#8375, and others. This license is not all-encompassing, but rather serves as a guide to help you use this shader properly in simple terms! <b>Please reach out if you have any questions.</b> Similarly, I reserve the right to revoke access on a need-be basis at my own discretion. Finally, this license may not be accurate if it is translated to another language beyond English. It is required that you read and agree to the following terms to use the shader.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>may not</b> redistribute this shader in its purest form (ex. the code or packages alone) on any platform such as chatting apps (ex: Discord, Skype), storage hosts (ex: VPS servers, Dropbox, Google Drive), websites dedicated to VRChat or Unity content, or through any other means.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>may not</b> edit the code to the shader or any related files.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>may not</b> use this work to promote hate or harm in any way (ex: seizure-inducing animations, offensive overlays)\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>may</b> include this in transformative works (ex: animations, avatars, worlds, and games). You may bundle the file alongside it if you want, or have the user download it from a respective source I host (ex: Gumroad) if you wish.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>may</b> include this in transformative works that are either public (free) or private (commercial). Either is ok! I would appreciate if you consider donating to me if you use it to make lots of money, but it isn't required <3\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>must</b> attribute me if using this in transformative works being redistributed. The attribution should be as follows: <i>\"June Lite Shader by luka \"zoey\" song (www.luka.moe/june, github.com/lukasong, luka#8375, lukazoeysong@gmail.com).\"</i> You may include additional information, such as a Gumroad or Booth link, or a description of what the shader is used for in your transformative work if you wish!\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " You <b>understand</b> that this work is protected under my (luka) copyright and that I (luka) will take legal action upon breaking any of these actions. <b>Legal action will be taken in North America, European Union, United Kingdom, China, South Korea, Japan, and other territories in which these courts can take action in (essentially the whole world).</b>\n\n";
    string strUiButtonDiscordServer = "Join the Discord server!";
    string strUiButtonDiscordMe = "Add me on Discord!";
    string strUiButtonWebsite = "Open my website!";
    string strUiButtonGithub = "Open my GitHub!";
    string strUiButtonYoutube = "Open my YouTube channel!";
    string strUiButtonGumroad = "Open my Gumroad store!";
    string strUiButtonBooth = "Open my Booth store!";
    string strUiMessageOfTheDayTitle = "Message of the Day!";
    string strUiAboutMe = 
        "Henlo! Thanks for checking this section out, it means a lot to me. I'll keep it short! My name is Zoey, or online I go by 'luka' as I'm sure you know by now (!), and I'm a young student in university studying computer programming! Sadly I use a lot of the money I make from my programming hobby for food, clothes, medicine, etc. – so if you want to suppot me (and get cool stuff in return), please check out the links below! <3\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Discord:</b> luka#8375\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Website:</b> luka.moe\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>GitHub:</b> github.com/lukasong\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>YouTube:</b> https://www.youtube.com/channel/UCwyJeuxwhgnxre3FaSacEuga\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Gumroad:</b> https://lukasong.gumroad.com\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Booth:</b> https://lukasong.booth.pm\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Email:</b> lukazoeysong@gmail.com";
    string strUiWherePro = 
        "June is avaliable to purchase in three different places. If you do not get it from one of these places, <b>it is a scam and may be malware</b>.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Gumroad: </b> 55 USD, 50 Euros, 6000 JPY. Get it at lukasong.gumroad.com!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Booth: </b> 55 USD, 50 Euros, 6000 JPY. Get it at lukasong.booth.pm!\n" + 
        LukaJuneLiteOneLibrary.getHeart() + " <b>Discord (Paypal, CashApp, Bitcoin, Monero): </b> 45 USD for Paypal, 55 USD for other methods. Add me: luka#8375 !\n\n" + 
        "Prices may fluctuate depending on updates or sales. Only one purchase is needed for all updates! <b>If you see the shader being distributed or sold elsewhere, please inform me!</b>\n\n" +
        "Influencers are also avaliable for discounts (such as large servers to promote in, large VRChat worlds, or reasonably sized YouTube channels). Please contact me about this if you think it applies to you!";
    string strUiProVsCompetitor = 
        "June is, to put it simply, <b>better</b> than the competition. Let me lay it out in <b>five points:</b>\n\n" +
        LukaJuneLiteOneLibrary.getHeart() +  " <b>June is unique and customizable.</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
        "No other shader has this many settings, let alone some of the effects that I come up with – hell, a lot of people even steal code and ideas from me. What other shader has vertex reconstruction (like wireframe or shatterwaves around the world), object-tracking zooms, extruding the screen, and more? Pro has over 2000 settings all in an easy-to-use format and UI!\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>June is faster than the rest.</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
        "June uses 2019-specific local shader keywords (which a lot of other shaders haven't even been updated to support), approximated-math (calculations such as exponents are extremely intensive, so I have an equation formula to simulate it at a faster rate), branching-hints to tell Unity how to best optimize the shader, " +
        "trigonometry in place of other equations (GPUs are obtized for it), pre-calculating in the vertex (runs only 1/4 the rate of pixel), pre-defined kernels and loops, and many more techniques! Plus I programmed it to be faster silly, I care about that boring stuff ;p But talk is cheap! I put my shader up to the test against three other screenspace shaders designed for VRChat all using a gaussian blur at runtime. " +
        "The average FPS was measured with Graphy <i>(open-source performance measuring tool avaliable on GitHub)</i> in Unity 2019 LTS on a Macbook Pro with M1 integrated graphics. An optimized avatar, low-poly world, and 4K skybox were included in runtime (and it was measured during runtime). Each test ran for approximately one-two minutes.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>No shaders</b> ran at an average of 65 FPS.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>June</b> ran at an average of 63 FPS. <i>(with a medium-quality blur)</i>\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Akai's Cancerspace</b> ran at an average of 60 FPS. <i>(with an ultra-low-quality blur, the highest offered)</i>\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Mochie's SFX</b> ran at an average of 61 FPS. <i>(with a slightly-under-medium-quality blur)</i>\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Doppelganger's Dope</b> ran at an average of 61 FPS. <i>(with a medium-quality blur)</i>\n\n" +
        "While these tiny FPS differences may seem insignificant, keep in mind that this is one effect and in Uniy (not VRChat). Multiple effects and VRChat avatars and worlds also will add onto the performance load, making the difference increase exponentially. I am able to use ~15 effects on ~3 layers during runtime fullscreen on my MacBook with no performance isuses with June (can do more presumably, just have not had a need to) for reference of how optimized it is.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>June is original.</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
        "Many other screenspace shaders, or shaders in general on VRChat, use code ported from other projects (such as GitHub) or websites (such as ShaderToy) without even crediting the original authors and profiting from their work, passing it off as their own. This leads to many issues (beyond moral/ethical), including not working properly in VR, having issues with performance, and not being able to provide proper support. I offer quick support for everything, because I wrote everything! Anything I do reference, I credit in the acknowledgements tab and follow the original license. I know this isn't a big selling point to many, but I hope at least other creators appreciate my care =)\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>June is updated.</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
        "First note: all updates come free after purchase. No subscription like other Unity projects or shaders! I hate subscription models, and so should you ;p BUT I DO APPRECIATE MONTHLY DONATIONS hehe. Anyways.. a lot of shaders are extremely outdated but are still used in the current Unity/VRChat version. This poses issues not only with how they work, but optimization, because they are missing out on new Unity features! You also miss out on new effects, updates, and bug fixes. And I take requests for updates! Anyways, I think you get the point :>\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>June is also a Unity tool-box.</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
        "June has a lot of built-in features to help you in Unity too, not just VRChat or other games! Beyond the standard things like range preview (gizmos), installation helpers, layer generators, animation particle/mesh converation, June also has some other key features:\n" +
        LukaJuneLiteOneLibrary.getHeart() + " 10+ themes and 10+ languages!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " 30+ presets!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Audio waveform preview generator!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Surfknasen's music auto-animation (his idea and permission to include <3)!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " My Necromancer script! Test your avatar in Unity with animations, movement, and physics, control and automate your camera, and generate poses as .anim files with your keyboard easily!\n\n" +
        "Still on the fence? Feel free to reach out to me (luka#8375 on Discord) and ask any questions!";
    string strUiWhatIsPro = "June originally started out as a private-only shared called MAE. After taking a break for a year, in 2021 I returned to VRChat and Unity development and made June - the sequel to it (free for MAE buyers of course, I keep my word) that was a completely new shader with the same spirit. \n\nAs of now, it's been out for months as a paid shader, but I decided to make a \"lite\" version for a few reasons..\n" +
        LukaJuneLiteOneLibrary.getHeart() + " It is a good introduction for new users. June (pro) itself has so much stuff, a simpler shader like this can help get mew users familiar with post-processing shaders!\n " +
        LukaJuneLiteOneLibrary.getHeart() + " It lets users \'trial\' and see my work before buying June Pro, even if it just a little glimpse.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " It lets creators and animators have a re-distributable package to make their work with!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " It helps out those who cannot afford June Pro, I have been there and understand it frens :3\n" +
        LukaJuneLiteOneLibrary.getHeart() + " It's free advertisement! Duh\n\n" +
        "Thus, June Lite was born. :)";
    string strUiProVsLite = 
        "For a whole list of Pro features, check out the tab <b>below</b>. This tab is more about specific lite-restrictions.\n\n" + 
        LukaJuneLiteOneLibrary.getHeart() + " Pro has 20x the options that Lite has.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has 5x the effects that Lite has.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has multiple scripts for Unity that Lite doesn't.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has support for over 10 languages and has over 10 themes.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has over 30 presets to get you started.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has unique effects not in any other shader but Pro.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has a less cluttured UI with no \"ads\" (sowwy but hey this is free!!).\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has more render settings, including testing values such as depth and stencil buffers!\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has more modules.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro gets more updates.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has more quality variations.\n" +
        LukaJuneLiteOneLibrary.getHeart() + " Pro has easy-to-use buttons in the Unity menu that automatically set up materials and shader instances for you!.\n";
    string strUiProFeatures = 
        "While not complete, I hope this can help you learn more about my \"little\" project.\n\n" +
        LukaJuneLiteOneLibrary.getHeart() + " <b>Modules</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Rendering - 4 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Shader - 3 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Blur - 19 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Chromatic Aberration - 6 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Color Manipulation - 18 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Border - 6 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Creativity - 15 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Distortion - 9 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Enhancements - 6   sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Filters - 37 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Frames - 11 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Generation - 8 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Glitch - 3 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Motion - 10 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Outlines - 10+ styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Overlay - 3 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Stylize - 16 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Special - 12 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Transition - 10 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Triplanar - 4 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " UV Manipulation - 26 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Vertex Reconstruction - 6 sections\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Zoom - 10 styles\n" +
         LukaJuneLiteOneLibrary.getHeart() + " And more on-going!\n\n" +
         LukaJuneLiteOneLibrary.getHeart() + " <b>Unity Features</b> " + LukaJuneLiteOneLibrary.getHeart() + "\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Auto-generate shader instances for you in the scene!\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Auto-generate shader layers with render queues for you in the scene!\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Test your avatars in Unity with avatar controllers, camera controllers, and posing!\n" +
         LukaJuneLiteOneLibrary.getHeart() + " Auto-animate to the beat of the music with scripts!\n" +
         LukaJuneLiteOneLibrary.getHeart() + " 30+ presets in the UI and make your own!\n\n" +
         "And more always on the way!";

    // setting up enums
    enum enumLiteToggle { Off, On };
    enum enumLiteOOB { Clamp, Mirror, Repeat };
    enum enumLiteHSV { Disabled, Multiply, Add };
    enum enumLiteBlur { Disabled, Gaussian, Radial, Chromatic };
    enum enumLiteDistortion { Disabled, SinCos, Wavey, Texture };
    enum enumLiteBorder { Disabled, Horizontal, Vertical };
    enum enumLiteShake { Disabled, Bumpy, Smooth, Circular };

    // booleans
    private static bool tabRendering = false;
    private static bool tabColoring = false;
    private static bool tabBlur = false;
    private static bool tabDistortion = false;
    private static bool tabBorder = false;
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
    private static bool tabSubFilterGlitch = false;
    private static bool tabSubFilterAstral = false;
    private static bool tabSubFilterNeon = false;
    private static bool tabSubUVTransformation = false;
    private static bool tabSubUVMove = false;
    private static bool tabSubUVShake = false;
    private static bool tabSubUVPixelation = false;
    private static bool tabSubUVRotation = false;
    private static bool tabSubUVSpherize = false;
    private static bool tabSubUVGlitch = false;
    private static bool tabExtraSettings = false;
    private static bool tabExtraLicense = false;
    private static bool tabExtraAboutMe = false;
    private static bool tabExtraAdvice = false;
    private static bool tabProWhatIsPro = false;
    private static bool tabProWhyPro = false;
    private static bool tabProWherePro = false;
    private static bool tabProCompetitors = false;
    private static bool tabProFeatures = false;
    private static bool tabProComparison = false;

    // setting up other variables
    static GUIStyle styHeader = null;
    static GUIStyle stySubHeader = null;
    static GUIStyle styFooter = null;
    static GUIStyle styJuneHeader = null;
    static GUIStyle styJuneSubHeader = null;
    static GUIStyle styDescription = null;

    // set up notifications of modules on
    static string toggleBlur = "";
    static string toggleDistortion = "";
    static string toggleBorder = "";
    static string toggleColoring = "";
    static string toggleOverlay = "";
    static string toggleUVManipulation = "";
    static string toggleZoom = "";
    static string toggleFilters = "";    

    // functions 
    private static void loadStyles() {
        styHeader = LukaJuneLiteOneLibrary.makeStyle(18, TextAnchor.MiddleCenter);
        stySubHeader = LukaJuneLiteOneLibrary.makeStyle(12, TextAnchor.MiddleCenter);
        styFooter = LukaJuneLiteOneLibrary.makeStyle(10, TextAnchor.MiddleCenter);
        styJuneHeader = LukaJuneLiteOneLibrary.makeStyle(14, TextAnchor.MiddleCenter);
        styJuneSubHeader = LukaJuneLiteOneLibrary.makeStyle(10, TextAnchor.MiddleCenter);
        styDescription = LukaJuneLiteOneLibrary.makeStyle(12, TextAnchor.MiddleLeft);
    }

    private static void loadToggles(float inputBlur,
        float inputBorder, float inputColoring,
        float inputDistortion, float inputFilters,
        float inputOverlay, float inputUVManipulation,
        float inputZoom) {
        toggleBlur = (inputBlur == 1.0f) ? " (Enabled)" : "";
        toggleBorder = (inputBorder == 1.0f) ? " (Enabled)" : "";
        toggleColoring = (inputColoring == 1.0f) ? " (Enabled)" : "";
        toggleDistortion = (inputDistortion == 1.0f) ? " (Enabled)" : "";
        toggleFilters = (inputFilters == 1.0f) ? " (Enabled)" : "";
        toggleOverlay = (inputOverlay == 1.0f) ? " (Enabled)" : "";
        toggleUVManipulation = (inputUVManipulation == 1.0f) ? " (Enabled)" : "";
        toggleZoom = (inputZoom == 1.0f) ? " (Enabled)" : "";
    }

    private static void loadMeta() {
        string strMetaRaw = LukaJuneLiteOneLibrary.getServerContents("https://www.luka.moe/call/public/JuneLiteOne/JuneMeta.moe");
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
                            if (line == "false") {
                                boolUiMetaProUpdate = false;
                            } else {
                                boolUiMetaProUpdate = true;
                            }
                            break;
                        case 3: // pvm
                            strUiMetaProUpdateDescription = line;
                            break;
                        case 4: // pvd
                            if (line == "false") {
                                boolUiMetaProDiscount = false;
                            } else {
                                boolUiMetaProDiscount = true;
                            }
                            break;
                        case 5: // pvdd
                            strUiMetaProDiscountDescription = line;
                            break;
                        case 6: // oa
                            if (line == "false") {
                                boolUiMetaOtherAd = false;
                            } else {
                                boolUiMetaOtherAd = true;
                            }
                            break;
                        case 7: // oad
                            strUiMetaOtherAdDescription = line;
                            break;
                        case 8: // motd
                            if (line == "false") {
                                boolUiMetaOtherMOTD = false;
                            } else {
                                boolUiMetaOtherMOTD = true;
                            }
                            break;
                        case 9: // motdd
                            strUiMetaOtherMOTD = line;
                            break;
                        case 10: // oand
                            if (line == "false") {
                                boolUiMetaOtherAnnouncement = false;
                            } else {
                                boolUiMetaOtherAnnouncement = true;
                            }
                            break;
                        case 11: // oandd
                            strUiMetaOtherAnnouncementDescription = line;
                            break;
                        case 12: // pd
                            strPublicDiscord = line;
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
        boolUiMetaProUpdate = false;
        strUiMetaProUpdateDescription = "There is no update at this time! Check the Discord for more info.";
        boolUiMetaProDiscount = false;
        strUiMetaProDiscountDescription = "There is no discount at this time! Check the Discord for more info.";
        boolUiMetaOtherAd = false;
        strUiMetaOtherAdDescription = "There is no advertisement at this time! Check the Discord for more info.";
        boolUiMetaOtherMOTD = true;
        strUiMetaOtherMOTD = "Thank you for using June Lite!";
        boolUiMetaOtherAnnouncement = false;
        strUiMetaOtherAnnouncementDescription = "There is no announcement at this time! Check the Discord for more info.";
        strPublicDiscord = "none";
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
        }
    }

    // ui
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
        LukaJuneLiteOneLibrary.makeBanner("Images/JuneLiteBanner_ForLukaSong_ByAtArotxt_OnTwitter");

        // presented by
        EditorGUILayout.Space();
        LukaJuneLiteOneLibrary.makeText("june presented by luka#8375", styHeader);
        // too busy.. LukaJuneLiteOneLibrary.makeText("a post-processing suite and unity toolbox", stySubHeader);
        LukaJuneLiteOneLibrary.makeText("you are using the <b>free, public, and trial version</b>", stySubHeader);
        LukaJuneLiteOneLibrary.makeText("www.luka.moe " + LukaJuneLiteOneLibrary.getHeart() + " github.com/lukasong " + LukaJuneLiteOneLibrary.getHeart() + " luka#8375", stySubHeader);
        LukaJuneLiteOneLibrary.makeText(LukaJuneLiteOneLibrary.getHeart() + " artwork provided by @lumiechuu " + LukaJuneLiteOneLibrary.getHeart(), stySubHeader);
        LukaJuneLiteOneLibrary.makeText("<b>redistribution restricted.</b> please see the license!", stySubHeader);

        // lite update
        LukaJuneLiteOneLibrary.doNotifcation(boolUiMetaLiteUpdate, strUiMetaLiteUpdateTitle, strUiMetaLiteUpdateDescription, 12); 
        LukaJuneLiteOneLibrary.doNotifcation(boolUiMetaProUpdate, strUiMetaProUpdateTitle, strUiMetaProUpdateDescription, 12);
        LukaJuneLiteOneLibrary.doNotifcation(boolUiMetaOtherAnnouncement, strUiMetaOtherAnnouncementTitle, strUiMetaOtherAnnouncementDescription, 12);

        // displaying the gui..
        LukaJuneLiteOneLibrary.makeDivider();
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
        loadToggles(prpLiteBlurModule.floatValue, prpLiteBorderModule.floatValue, prpLiteColoringModule.floatValue,
            prpLiteDistortionModule.floatValue, prpLiteFilterModule.floatValue,
            prpLiteOverlayModule.floatValue, prpLiteUVManipulationModule.floatValue,
            prpLiteZoomModule.floatValue);

        // rendering tab
        tabRendering = LukaJuneLiteOneLibrary.makeFoldout(tabRendering, "Rendering", 16);
        if (tabRendering)
        {
            EditorGUILayout.Space();
            prpLiteRenderingFalloffStart = ShaderGUI.FindProperty("_LiteRenderingFalloffStart", propertiesThis);
            prpLiteRenderingFalloffEnd = ShaderGUI.FindProperty("_LiteRenderingFalloffEnd", propertiesThis);
            prpLiteRenderingPower = ShaderGUI.FindProperty("_LiteRenderingPower", propertiesThis);
            prpLiteRenderingOOB = ShaderGUI.FindProperty("_LiteRenderingOOB", propertiesThis);
            meThis.ShaderProperty(prpLiteRenderingPower, "Shader Power");
            meThis.ShaderProperty(prpLiteRenderingFalloffStart, "Falloff Start");
            meThis.ShaderProperty(prpLiteRenderingFalloffEnd, "Falloff End");
            prpLiteRenderingOOB.floatValue = (float)(enumLiteOOB)EditorGUILayout.EnumPopup("Out of Bounds Style", (enumLiteOOB)prpLiteRenderingOOB.floatValue);
            EditorGUILayout.Space();
        }

        // blur tab
        tabBlur = LukaJuneLiteOneLibrary.makeFoldout(tabBlur, "Blur" + toggleBlur, 16);
        if (tabBlur)
        {
            EditorGUILayout.Space();
            prpLiteBlurModule = ShaderGUI.FindProperty("_LiteBlurModule", propertiesThis);
            prpLiteBlurPower = ShaderGUI.FindProperty("_LiteBlurPower", propertiesThis);
            prpLiteBlurRadius = ShaderGUI.FindProperty("_LiteBlurRadius", propertiesThis);
            prpLiteBlurTransparency = ShaderGUI.FindProperty("_LiteBlurTransparency", propertiesThis);
            prpLiteBlurColor = ShaderGUI.FindProperty("_LiteBlurColor", propertiesThis);
            prpLiteBlurStyle = ShaderGUI.FindProperty("_LiteBlurStyle", propertiesThis);
            prpLiteRenderingQuality = ShaderGUI.FindProperty("_LiteRenderingQuality", propertiesThis);
            meThis.ShaderProperty(prpLiteBlurModule, "Enable Blur Module");
            meThis.ShaderProperty(prpLiteRenderingQuality, "Higher Quality Blurs");
            prpLiteBlurStyle.floatValue = (float)(enumLiteBlur)EditorGUILayout.EnumPopup("Blur Style", (enumLiteBlur)prpLiteBlurStyle.floatValue);
            meThis.ShaderProperty(prpLiteBlurPower, "Blur Power");
            meThis.ShaderProperty(prpLiteBlurRadius, "Blur Radius");
            meThis.ShaderProperty(prpLiteBlurTransparency, "Blur Transparency");
            meThis.ShaderProperty(prpLiteBlurColor, "Blur Color");
            EditorGUILayout.Space();
        }

        // border tab
        tabBorder = LukaJuneLiteOneLibrary.makeFoldout(tabBorder, "Border" + toggleBorder, 16);
        if (tabBorder)
        {
            EditorGUILayout.Space();
            prpLiteBorderModule = ShaderGUI.FindProperty("_LiteBorderModule", propertiesThis);
            prpLiteBorderStyle = ShaderGUI.FindProperty("_LiteBorderStyle", propertiesThis);
            prpLiteBorderColor = ShaderGUI.FindProperty("_LiteBorderColor", propertiesThis);
            prpLiteBorderPower = ShaderGUI.FindProperty("_LiteBorderPower", propertiesThis);
            prpLiteBorderSoften = ShaderGUI.FindProperty("_LiteBorderSoften", propertiesThis);
            meThis.ShaderProperty(prpLiteBorderModule, "Enable Border Module");
            prpLiteBorderStyle.floatValue = (float)(enumLiteBorder)EditorGUILayout.EnumPopup("Border Style", (enumLiteBorder)prpLiteBorderStyle.floatValue);
            meThis.ShaderProperty(prpLiteBorderColor, "Border Color");
            meThis.ShaderProperty(prpLiteBorderPower, "Border Power");
            meThis.ShaderProperty(prpLiteBorderSoften, "Border Soften");
            EditorGUILayout.Space();
        }

        // coloring tab
        tabColoring = LukaJuneLiteOneLibrary.makeFoldout(tabColoring, "Coloring" + toggleColoring, 16);
        if (tabColoring)
        {
            EditorGUILayout.Space();
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
            meThis.ShaderProperty(prpLiteColoringModule, "Enable Coloring Module");
            meThis.ShaderProperty(prpLiteColoringRGBMultiply, "RGB Multiply");
            meThis.ShaderProperty(prpLiteColoringRGBOverlayTransparency, "RGB Overlay Transparency");
            if (prpLiteColoringRGBOverlayTransparency.floatValue != 0) {
                EditorGUI.indentLevel++;
                meThis.ShaderProperty(prpLiteColoringRGBOverlay, "RGB Overlay");
                EditorGUI.indentLevel--;
            }
            prpLiteColoringHSVStyle.floatValue = (float)(enumLiteHSV)EditorGUILayout.EnumPopup("HSV Style", (enumLiteHSV)prpLiteColoringHSVStyle.floatValue);
            if (prpLiteColoringHSVStyle.floatValue != 0) {
                EditorGUI.indentLevel++;
                meThis.ShaderProperty(prpLiteColoringHSVh, "HSV Hue");
                meThis.ShaderProperty(prpLiteColoringHSVs, "HSV Saturation");
                meThis.ShaderProperty(prpLiteColoringHSVv, "HSV Value");
                EditorGUI.indentLevel--;
            }
            meThis.ShaderProperty(prpLiteColoringInvert, "Invert");
            meThis.ShaderProperty(prpLiteColoringDrain, "Color Drain");
            meThis.ShaderProperty(prpLiteColoringDarkness, "Darkness");
            meThis.ShaderProperty(prpLiteColoringBrightness, "Brightness");
            meThis.ShaderProperty(prpLiteColoringEmission, "Emission");
            meThis.ShaderProperty(prpLiteColoringPosterization, "Posterization");
            meThis.ShaderProperty(prpLiteColoringColorGrading, "Color Grading");
            if (prpLiteColoringColorGrading.floatValue != 0) {
                meThis.ShaderProperty(prpLiteColoringColorGradingTone, "Color Grading Tone");
            }
            EditorGUILayout.Space();
        }

        // distortion tab
        tabDistortion = LukaJuneLiteOneLibrary.makeFoldout(tabDistortion, "Distortion" + toggleDistortion, 16);
        if (tabDistortion)
        {
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
            meThis.ShaderProperty(prpLiteDistortionModule, "Enable Distortion Module");
            prpLiteDistortionStyle.floatValue = (float)(enumLiteDistortion)EditorGUILayout.EnumPopup("Distortion Style", (enumLiteDistortion)prpLiteDistortionStyle.floatValue);
            meThis.ShaderProperty(prpLiteDistortionPowerX, "Distortion Power X");
            meThis.ShaderProperty(prpLiteDistortionPowerY, "Distortion Power Y");
            meThis.ShaderProperty(prpLiteDistortionSpeedX, "Distortion Speed X");
            meThis.ShaderProperty(prpLiteDistortionSpeedY, "Distortion Speed Y");
            if (prpLiteDistortionStyle.floatValue == (float)enumLiteDistortion.Texture) {
                meThis.ShaderProperty(prpLiteDistortionTexture, "Distortion Texture");
                meThis.ShaderProperty(prpLiteDistortionTextureScale, "Distortion Texture Scale");
            }
            prpLiteDistortionWobble.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Add in WoBbLe?", (enumLiteToggle)prpLiteDistortionWobble.floatValue);
            if (prpLiteDistortionWobble.floatValue != 0) {
                EditorGUI.indentLevel++;
                meThis.ShaderProperty(prpLiteDistortionWobblePower, "Wobble Power");
                meThis.ShaderProperty(prpLiteDistortionWobbleSpeed, "Wobble Speed");
                meThis.ShaderProperty(prpLiteDistortionWobbleCoverage, "Wobble Coverage");
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space();
        }

        // filter tab
        tabFilter = LukaJuneLiteOneLibrary.makeFoldout(tabFilter, "Filters" + toggleFilters, 16);
        if (tabFilter)
        {
            EditorGUILayout.Space();
            prpLiteFilterModule = ShaderGUI.FindProperty("_LiteFilterModule", propertiesThis);
            meThis.ShaderProperty(prpLiteFilterModule, "Enable Filter Module");
            LukaJuneLiteOneLibrary.doFoldoutIndentStart(20);
            tabSubFilterVignette = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterVignette, "Vignette");
            if (tabSubFilterVignette) {
                prpLiteFilterVignette = ShaderGUI.FindProperty("_LiteFilterVignette", propertiesThis);
                prpLiteFilterVignettePower = ShaderGUI.FindProperty("_LiteFilterVignettePower", propertiesThis);
                prpLiteFilterVignetteColor = ShaderGUI.FindProperty("_LiteFilterVignetteColor", propertiesThis);
                prpLiteFilterVignette.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Vignette", (enumLiteToggle)prpLiteFilterVignette.floatValue);
                meThis.ShaderProperty(prpLiteFilterVignettePower, "Vignette Power");
                meThis.ShaderProperty(prpLiteFilterVignetteColor, "Vignette Color");
                EditorGUILayout.Space();
            }
            tabSubFilterColorCrush = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterColorCrush, "Color Crush");
            if (tabSubFilterColorCrush) {
                prpLiteFilterColorCrush = ShaderGUI.FindProperty("_LiteFilterColorCrush", propertiesThis);
                prpLiteFilterColorCrushPower = ShaderGUI.FindProperty("_LiteFilterColorCrushPower", propertiesThis);
                prpLiteFilterColorCrush.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Color Crush", (enumLiteToggle)prpLiteFilterColorCrush.floatValue);
                meThis.ShaderProperty(prpLiteFilterColorCrushPower, "Color Crush Power");
                EditorGUILayout.Space();
            }
            tabSubFilterDuotone = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterDuotone, "Duotone");
            if (tabSubFilterDuotone) {
                prpLiteFilterDuotone = ShaderGUI.FindProperty("_LiteFilterDuotone", propertiesThis);
                prpLiteFilterDuotoneTransparency = ShaderGUI.FindProperty("_LiteFilterDuotoneTransparency", propertiesThis);
                prpLiteFilterDuotoneColorOne = ShaderGUI.FindProperty("_LiteFilterDuotoneColorOne", propertiesThis);
                prpLiteFilterDuotoneColorTwo = ShaderGUI.FindProperty("_LiteFilterDuotoneColorTwo", propertiesThis);
                prpLiteFilterDuotoneThreshold = ShaderGUI.FindProperty("_LiteFilterDuotoneThreshold", propertiesThis);
                prpLiteFilterDuotone.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Duotone", (enumLiteToggle)prpLiteFilterDuotone.floatValue);
                meThis.ShaderProperty(prpLiteFilterDuotoneTransparency, "Duotone Transparency");
                meThis.ShaderProperty(prpLiteFilterDuotoneColorOne, "Duotone Color One");
                meThis.ShaderProperty(prpLiteFilterDuotoneColorTwo, "Duotone Color Two");
                meThis.ShaderProperty(prpLiteFilterDuotoneThreshold, "Duotone Threshold");
                EditorGUILayout.Space();
            }
            tabSubFilterRainbow = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterRainbow, "Rainbow");
            if (tabSubFilterRainbow) {
                prpLiteFilterRainbow = ShaderGUI.FindProperty("_LiteFilterRainbow", propertiesThis);
                prpLiteFilterRainbowSaturation = ShaderGUI.FindProperty("_LiteFilterRainbowSaturation", propertiesThis);
                prpLiteFilterRainbowSpeed = ShaderGUI.FindProperty("_LiteFilterRainbowSpeed", propertiesThis);
                prpLiteFilterRainbow.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Rainbow", (enumLiteToggle)prpLiteFilterRainbow.floatValue);
                meThis.ShaderProperty(prpLiteFilterRainbowSaturation, "Rainbow Saturation");
                meThis.ShaderProperty(prpLiteFilterRainbowSpeed, "Rainbow Speed");
                EditorGUILayout.Space();
            }
            tabSubFilterFilm = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterFilm, "Film");
            if (tabSubFilterFilm) {
                prpLiteFilterFilm = ShaderGUI.FindProperty("_LiteFilterFilm", propertiesThis);
                prpLiteFilterFilmAmount = ShaderGUI.FindProperty("_LiteFilterFilmAmount", propertiesThis);
                prpLiteFilterFilm.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Film", (enumLiteToggle)prpLiteFilterFilm.floatValue);
                meThis.ShaderProperty(prpLiteFilterFilmAmount, "Film Amount");
                EditorGUILayout.Space();
            }
            tabSubFilterGrain = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterGrain, "Grain");
            if (tabSubFilterGrain) {
                prpLiteFilterGrain = ShaderGUI.FindProperty("_LiteFilterGrain", propertiesThis);
                prpLiteFilterGrainAmount = ShaderGUI.FindProperty("_LiteFilterGrainAmount", propertiesThis);
                prpLiteFilterGrain.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Grain", (enumLiteToggle)prpLiteFilterGrain.floatValue);
                meThis.ShaderProperty(prpLiteFilterGrainAmount, "Grain Amount");
                EditorGUILayout.Space();
            }
            tabSubFilterVHS = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterVHS, "VHS");
            if (tabSubFilterVHS) {
                prpLiteFilterVHS = ShaderGUI.FindProperty("_LiteFilterVHS", propertiesThis);
                prpLiteFilterVHSAmount = ShaderGUI.FindProperty("_LiteFilterVHSAmount", propertiesThis);
                prpLiteFilterVHS.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable VHS", (enumLiteToggle)prpLiteFilterVHS.floatValue);
                meThis.ShaderProperty(prpLiteFilterVHSAmount, "VHS Amount");
                EditorGUILayout.Space();
            }
            tabSubFilterGradient = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterGradient, "Gradient");
            if (tabSubFilterGradient) {
                prpLiteFilterGradient = ShaderGUI.FindProperty("_LiteFilterGradient", propertiesThis);
                prpLiteFilterGradientLHS = ShaderGUI.FindProperty("_LiteFilterGradientLHS", propertiesThis);
                prpLiteFilterGradientRHS = ShaderGUI.FindProperty("_LiteFilterGradientRHS", propertiesThis);
                prpLiteFilterGradientTransparency = ShaderGUI.FindProperty("_LiteFilterGradientTransparency", propertiesThis);
                prpLiteFilterGradient.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Gradient", (enumLiteToggle)prpLiteFilterGradient.floatValue);
                meThis.ShaderProperty(prpLiteFilterGradientLHS, "Gradient LHS");
                meThis.ShaderProperty(prpLiteFilterGradientRHS, "Gradient RHS");
                meThis.ShaderProperty(prpLiteFilterGradientTransparency, "Gradient Transparency");
                EditorGUILayout.Space();
            }
            tabSubFilterOutline = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterOutline, "Outline");
            if (tabSubFilterOutline) {
                prpLiteFilterOutline = ShaderGUI.FindProperty("_LiteFilterOutline", propertiesThis);
                prpLiteFilterOutlineWidth = ShaderGUI.FindProperty("_LiteFilterOutlineWidth", propertiesThis);
                prpLiteFilterOutlineTolerance = ShaderGUI.FindProperty("_LiteFilterOutlineTolerance", propertiesThis);
                prpLiteFilterOutlineColor = ShaderGUI.FindProperty("_LiteFilterOutlineColor", propertiesThis);
                prpLiteFilterOutline.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Outline", (enumLiteToggle)prpLiteFilterOutline.floatValue);
                meThis.ShaderProperty(prpLiteFilterOutlineWidth, "Outline Width");
                meThis.ShaderProperty(prpLiteFilterOutlineTolerance, "Outline Tolerance");
                meThis.ShaderProperty(prpLiteFilterOutlineColor, "Outline Color");
                EditorGUILayout.Space();
            }
            tabSubFilterGlitch = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterGlitch, "Glitch");
            if (tabSubFilterGlitch) {
                prpLiteFilterGlitch = ShaderGUI.FindProperty("_LiteFilterGlitch", propertiesThis);
                prpLiteFilterGlitchAmount = ShaderGUI.FindProperty("_LiteFilterGlitchAmount", propertiesThis);
                prpLiteFilterGlitch.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Glitch", (enumLiteToggle)prpLiteFilterGlitch.floatValue);
                meThis.ShaderProperty(prpLiteFilterGlitchAmount, "Glitch Amount");
                EditorGUILayout.Space();
            }
            tabSubFilterAstral = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterAstral, "Astral");
            if (tabSubFilterAstral) {
                prpLiteFilterAstral = ShaderGUI.FindProperty("_LiteFilterAstral", propertiesThis);
                prpLiteFilterAstralZoom = ShaderGUI.FindProperty("_LiteFilterAstralZoom", propertiesThis);
                prpLiteFilterAstralZoomTransparency = ShaderGUI.FindProperty("_LiteFilterAstralTransparency", propertiesThis);
                prpLiteFilterAstralZoomColor = ShaderGUI.FindProperty("_LiteFilterAstralColor", propertiesThis);
                prpLiteFilterAstral.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Astral", (enumLiteToggle)prpLiteFilterAstral.floatValue);
                meThis.ShaderProperty(prpLiteFilterAstralZoom, "Astral Zoom");
                meThis.ShaderProperty(prpLiteFilterAstralZoomTransparency, "Astral Transparency");
                meThis.ShaderProperty(prpLiteFilterAstralZoomColor, "Astral Color");
                EditorGUILayout.Space();
            }
            tabSubFilterNeon = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubFilterNeon, "Neon");
            if (tabSubFilterNeon) {
                prpLiteFilterNeon = ShaderGUI.FindProperty("_LiteFilterNeon", propertiesThis);
                prpLiteFilterNeonWidth = ShaderGUI.FindProperty("_LiteFilterNeonWidth", propertiesThis);
                prpLiteFilterNeonTransparency = ShaderGUI.FindProperty("_LiteFilterNeonTransparency", propertiesThis);
                prpLiteFilterNeonHue = ShaderGUI.FindProperty("_LiteFilterNeonHue", propertiesThis);
                prpLiteFilterNeon.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Enable Neon", (enumLiteToggle)prpLiteFilterNeon.floatValue);
                meThis.ShaderProperty(prpLiteFilterNeonWidth, "Neon Width");
                meThis.ShaderProperty(prpLiteFilterNeonTransparency, "Neon Transparency");
                meThis.ShaderProperty(prpLiteFilterNeonHue, "Neon Hue");
                EditorGUILayout.Space();
            }
            LukaJuneLiteOneLibrary.doFoldoutIndentEnd();
            EditorGUILayout.Space();
        }
        
        // overlay tab
        tabOverlay = LukaJuneLiteOneLibrary.makeFoldout(tabOverlay, "Overlay" + toggleOverlay, 16);
        if (tabOverlay)
        {
            EditorGUILayout.Space();
            prpLiteOverlayModule = ShaderGUI.FindProperty("_LiteOverlayModule", propertiesThis);
            prpLiteOverlayTexture = ShaderGUI.FindProperty("_LiteOverlayTexture", propertiesThis);
            prpLiteOverlayTransparency = ShaderGUI.FindProperty("_LiteOverlayTransparency", propertiesThis);
            prpLiteOverlaySizeX = ShaderGUI.FindProperty("_LiteOverlaySizeX", propertiesThis);
            prpLiteOverlaySizeY = ShaderGUI.FindProperty("_LiteOverlaySizeY", propertiesThis);
            prpLiteOverlayOffsetX = ShaderGUI.FindProperty("_LiteOverlayOffsetX", propertiesThis);
            prpLiteOverlayOffsetY = ShaderGUI.FindProperty("_LiteOverlayOffsetY", propertiesThis);
            meThis.ShaderProperty (prpLiteOverlayModule, "Enable Overlay");
            meThis.ShaderProperty (prpLiteOverlayTexture, "Overlay Texture");
            meThis.ShaderProperty (prpLiteOverlayTransparency, "Overlay Transparency");
            meThis.ShaderProperty (prpLiteOverlaySizeX, "Overlay Size X");
            meThis.ShaderProperty (prpLiteOverlaySizeY, "Overlay Size Y");
            meThis.ShaderProperty (prpLiteOverlayOffsetX, "Overlay Offset X");
            meThis.ShaderProperty (prpLiteOverlayOffsetY, "Overlay Offset Y");
            EditorGUILayout.Space();
        }

        // uv manipulation tab
        tabUVManipulation = LukaJuneLiteOneLibrary.makeFoldout(tabUVManipulation, "UV Manipulation" + toggleUVManipulation, 16);
        if (tabUVManipulation)
        {
            EditorGUILayout.Space();
            prpLiteUVManipulationModule = ShaderGUI.FindProperty("_LiteUVManipulationModule", propertiesThis);
            meThis.ShaderProperty (prpLiteUVManipulationModule, "Enable UV Module");
            LukaJuneLiteOneLibrary.doFoldoutIndentStart(20);
            tabSubUVTransformation = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVTransformation, "Transformation");
            if (tabSubUVTransformation) {
                prpLiteUVManipulationTransformationSlantTL = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantTL", propertiesThis);
                prpLiteUVManipulationTransformationSlantTR = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantTR", propertiesThis);
                prpLiteUVManipulationTransformationSlantBL = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantBL", propertiesThis);
                prpLiteUVManipulationTransformationSlantBR = ShaderGUI.FindProperty("_LiteUVManipulationTransformationSlantBR", propertiesThis);
                prpLiteUVManipulationTransformationFlipX = ShaderGUI.FindProperty("_LiteUVManipulationTransformationFlipX", propertiesThis);
                prpLiteUVManipulationTransformationFlipY = ShaderGUI.FindProperty("_LiteUVManipulationTransformationFlipY", propertiesThis);
                prpLiteUVManipulationTransformationStretchX = ShaderGUI.FindProperty("_LiteUVManipulationTransformationStretchX", propertiesThis);
                prpLiteUVManipulationTransformationStretchY = ShaderGUI.FindProperty("_LiteUVManipulationTransformationStretchY", propertiesThis);
                meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantTL, "Slant Top Left");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantTR, "Slant Top Right");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantBL, "Slant Bottom Left");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationSlantBR, "Slant Bottom Right");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationFlipX, "Flip X");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationFlipY, "Flip Y");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationStretchX, "Stretch X");
                meThis.ShaderProperty(prpLiteUVManipulationTransformationStretchY, "Stretch Y");
                EditorGUILayout.Space();
            }
            tabSubUVMove = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVMove, "Movement");
            if (tabSubUVMove)
            {
                prpLiteUVManipulationMoveX = ShaderGUI.FindProperty("_LiteUVManipulationMoveX", propertiesThis);
                prpLiteUVManipulationMoveY = ShaderGUI.FindProperty("_LiteUVManipulationMoveY", propertiesThis);
                meThis.ShaderProperty(prpLiteUVManipulationMoveX, "Move X");
                meThis.ShaderProperty(prpLiteUVManipulationMoveY, "Move Y");
                EditorGUILayout.Space();
            }
            tabSubUVShake = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVShake, "Shake");
            if (tabSubUVShake)
            {
                prpLiteUVManipulationShakeStyle = ShaderGUI.FindProperty("_LiteUVManipulationShakeStyle", propertiesThis);
                prpLiteUVManipulationShakePowerX = ShaderGUI.FindProperty("_LiteUVManipulationShakePowerX", propertiesThis);
                prpLiteUVManipulationShakePowerY = ShaderGUI.FindProperty("_LiteUVManipulationShakePowerY", propertiesThis);
                prpLiteUVManipulationShakeSpeedX = ShaderGUI.FindProperty("_LiteUVManipulationShakeSpeedX", propertiesThis);
                prpLiteUVManipulationShakeSpeedY = ShaderGUI.FindProperty("_LiteUVManipulationShakeSpeedY", propertiesThis);
                prpLiteUVManipulationShakeStyle.floatValue = (float)(enumLiteShake)EditorGUILayout.EnumPopup("Shake Style", (enumLiteShake)prpLiteUVManipulationShakeStyle.floatValue);
                meThis.ShaderProperty(prpLiteUVManipulationShakePowerX, "Shake X Power");
                meThis.ShaderProperty(prpLiteUVManipulationShakePowerY, "Shake Y Power");
                meThis.ShaderProperty(prpLiteUVManipulationShakeSpeedX, "Shake X Speed");
                meThis.ShaderProperty(prpLiteUVManipulationShakeSpeedY, "Shake Y peed");
                EditorGUILayout.Space();
            }
            tabSubUVPixelation = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVPixelation, "Pixelation");
            if (tabSubUVPixelation)
            {
                prpLiteUVManipulationPixelation = ShaderGUI.FindProperty("_LiteUVManipulationPixelation", propertiesThis);
                prpLiteUVManipulationPixelationPower = ShaderGUI.FindProperty("_LiteUVManipulationPixelationPower", propertiesThis);
                prpLiteUVManipulationPixelation.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Pixelation", (enumLiteToggle)prpLiteUVManipulationPixelation.floatValue);
                meThis.ShaderProperty(prpLiteUVManipulationPixelationPower, "Pixelation Power");
                EditorGUILayout.Space();
            }
            tabSubUVRotation = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVRotation, "Rotation");
            if (tabSubUVRotation)
            {
                prpLiteUVManipulationRotation = ShaderGUI.FindProperty("_LiteUVManipulationRotation", propertiesThis);
                prpLiteUVManipulationRotationAngle = ShaderGUI.FindProperty("_LiteUVManipulationRotationAngle", propertiesThis);
                prpLiteUVManipulationRotation.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Rotation", (enumLiteToggle)prpLiteUVManipulationRotation.floatValue);
                meThis.ShaderProperty(prpLiteUVManipulationRotationAngle, "Rotation Angle");
                EditorGUILayout.Space();
            }
            tabSubUVSpherize = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVSpherize, "Spherize");
            if (tabSubUVSpherize)
            {
                prpLiteUVManipulationSpherize = ShaderGUI.FindProperty("_LiteUVManipulationSpherize", propertiesThis);
                prpLiteUVManipulationSpherizePower = ShaderGUI.FindProperty("_LiteUVManipulationSpherizePower", propertiesThis);
                prpLiteUVManipulationSpherize.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Spherize", (enumLiteToggle)prpLiteUVManipulationSpherize.floatValue);
                meThis.ShaderProperty(prpLiteUVManipulationSpherizePower, "Spherize Power");
                EditorGUILayout.Space();
            }
            tabSubUVGlitch = LukaJuneLiteOneLibrary.makeFoldoutSub(tabSubUVGlitch, "UV Glitch");
            if (tabSubUVGlitch)
            {
                prpLiteUVManipulationGlitch = ShaderGUI.FindProperty("_LiteUVManipulationGlitch", propertiesThis);
                prpLiteUVManipulationGlitchAmount = ShaderGUI.FindProperty("_LiteUVManipulationGlitchAmount", propertiesThis);
                prpLiteUVManipulationGlitch.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("UV Glitch", (enumLiteToggle)prpLiteUVManipulationGlitch.floatValue);
                meThis.ShaderProperty(prpLiteUVManipulationGlitchAmount, "UV Glitch Amount");
                EditorGUILayout.Space();
            }
            LukaJuneLiteOneLibrary.doFoldoutIndentEnd();
            EditorGUILayout.Space();
        }

        // zoom tab
        tabZoom = LukaJuneLiteOneLibrary.makeFoldout(tabZoom, "Zoom" + toggleZoom, 16);
        if (tabZoom)
        {
            EditorGUILayout.Space();
            prpLiteZoomModule = ShaderGUI.FindProperty("_LiteZoomModule", propertiesThis);
            prpLiteZoomPower = ShaderGUI.FindProperty("_LiteZoomPower", propertiesThis);
            prpLiteZoomRangeStyle = ShaderGUI.FindProperty("_LiteZoomRangeStyle", propertiesThis);
            prpLiteZoomRangeStart = ShaderGUI.FindProperty("_LiteZoomRangeStart", propertiesThis);
            prpLiteZoomRangeEnd = ShaderGUI.FindProperty("_LiteZoomRangeEnd", propertiesThis);
            meThis.ShaderProperty(prpLiteZoomModule, "Enable Zoom Module");
            meThis.ShaderProperty(prpLiteZoomPower, "Zoom Power");
            prpLiteZoomRangeStyle.floatValue = (float)(enumLiteToggle)EditorGUILayout.EnumPopup("Zoom Custom Range", (enumLiteToggle)prpLiteZoomRangeStyle.floatValue);
            if (prpLiteZoomRangeStyle.floatValue != 0) {
                EditorGUI.indentLevel++;
                meThis.ShaderProperty(prpLiteZoomRangeStart, "Zoom Range Start");
                meThis.ShaderProperty(prpLiteZoomRangeEnd, "Zoom Range End");
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space();
        }

        // june discount
        LukaJuneLiteOneLibrary.doNotifcation(boolUiMetaProDiscount, strUiMetaProDiscountTitle, strUiMetaProDiscountDescription, 12);

        LukaJuneLiteOneLibrary.makeDivider();

        // settings tab
        tabExtraSettings = LukaJuneLiteOneLibrary.makeFoldout(tabExtraSettings, "Settings", 16);
        if (tabExtraSettings) {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.doGreyStart();
            intUiLanguages = (int)(enumUiLanguages)EditorGUILayout.EnumPopup(strUiLanguage, (enumUiLanguages)intUiLanguages);
            intUiThemes = (int)(enumUiThemes)EditorGUILayout.EnumPopup(strUiTheme, (enumUiThemes)intUiThemes);
            LukaJuneLiteOneLibrary.doGreyEnd();
            LukaJuneLiteOneLibrary.drawToastNoImage(strUiLockTheme, 12, 200);
            if (GUILayout.Button(strUiSaveSettings))
            {

            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // licesense tab 
        tabExtraLicense = LukaJuneLiteOneLibrary.makeFoldout(tabExtraLicense, "<b>Shader License</b> (must read)", 16);
        if (tabExtraLicense) {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiLicense, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // advice me tab
        tabExtraAdvice = LukaJuneLiteOneLibrary.makeFoldout(tabExtraAdvice, "Advice for New Users", 16);
        if (tabExtraAdvice)
        {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiAdviceForNewPpl, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // about me tab
        tabExtraAboutMe = LukaJuneLiteOneLibrary.makeFoldout(tabExtraAboutMe, "About Me", 16);
        if (tabExtraAboutMe) {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiAboutMe, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // other ad
        LukaJuneLiteOneLibrary.doNotifcation(boolUiMetaOtherAd, strUiMetaOtherAdTitle, strUiMetaOtherAdDescription, 12);

        LukaJuneLiteOneLibrary.makeDivider();

        // <3
        LukaJuneLiteOneLibrary.makeBanner("Images/JuneLiteBanner_JunePro");

        // what is pro
        tabProWhatIsPro = LukaJuneLiteOneLibrary.makeFoldout(tabProWhatIsPro, "What is Pro?", 16);
        if (tabProWhatIsPro)
        {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiWhatIsPro, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // why pro
        /*tabProWhyPro = LukaJuneLiteOneLibrary.makeFoldout(tabProWhyPro, "Why Pro?", 16);
        if (tabProWhyPro)
        {
            // not using as of now
        }*/

        // pro vs lite
        tabProComparison = LukaJuneLiteOneLibrary.makeFoldout(tabProComparison, "Pro vs Lite", 16);
        if (tabProComparison) {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiProVsLite, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // pro features
        tabProFeatures = LukaJuneLiteOneLibrary.makeFoldout(tabProFeatures, "Pro Features", 16);
        if (tabProFeatures) {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiProFeatures, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // pro vs competitors
        tabProCompetitors = LukaJuneLiteOneLibrary.makeFoldout(tabProCompetitors, "Pro vs Competitors", 16);
        if (tabProCompetitors) {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiProVsCompetitor, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // where pro
        tabProWherePro = LukaJuneLiteOneLibrary.makeFoldout(tabProWherePro, "Where can I get Pro?", 16);
        if (tabProWherePro)
        {
            EditorGUILayout.BeginVertical("GroupBox");
            LukaJuneLiteOneLibrary.makeText(strUiWherePro, styDescription);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        // announcements
        LukaJuneLiteOneLibrary.doNotifcation(boolUiMetaOtherMOTD, strUiMessageOfTheDayTitle, strUiMetaOtherMOTD, 12);

        // buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(strUiButtonDiscordMe))
        {
            LukaJuneLiteOneLibrary.makePopup("My Discord", "luka#8375");
        }
        if (GUILayout.Button(strUiButtonDiscordServer))
        {
            if (strPublicDiscord == "none")
            {
                LukaJuneLiteOneLibrary.makePopup("Discord Server", "There is no Discord server at this time! If you have issues or suggestions, or want to commission, etc., please contact me directly at luka#8375 on Discord!");
            } else {
                bool didWork = LukaJuneLiteOneLibrary.openWebpage(strPublicDiscord);
                if (!didWork)
                {
                    LukaJuneLiteOneLibrary.makePopup("Discord Server", "There is no Discord server at this time! If you have issues or suggestions, or want to commission, etc., please contact me directly at luka#8375 on Discord!");
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(strUiButtonGithub))
        {
            bool didWork = LukaJuneLiteOneLibrary.openWebpage("https://github.com/lukasong/");
            if (!didWork)
            {
                LukaJuneLiteOneLibrary.makePopup("Github", "https://github.com/lukasong/");
            }
        }
        if (GUILayout.Button(strUiButtonYoutube))
        {
            bool didWork = LukaJuneLiteOneLibrary.openWebpage("https://www.youtube.com/channel/UCwyJeuxwhgnxre3FaSacEug");
            if (!didWork)
            {
                LukaJuneLiteOneLibrary.makePopup("Youtube", "https://www.youtube.com/channel/UCwyJeuxwhgnxre3FaSacEug");
            }
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(strUiButtonGumroad))
        {
            bool didWork = LukaJuneLiteOneLibrary.openWebpage("https://app.gumroad.com/lukasong");
            if (!didWork)
            {
                LukaJuneLiteOneLibrary.makePopup("Gumroad", "https://app.gumroad.com/lukasong");
            }
        }
        if (GUILayout.Button(strUiButtonBooth))
        {
            bool didWork = LukaJuneLiteOneLibrary.openWebpage("https://lukasong.booth.pm/");
            if (!didWork)
            {
                LukaJuneLiteOneLibrary.makePopup("Booth", "https://lukasong.booth.pm/");
            }
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button(strUiButtonWebsite))
        {
            bool didWork = LukaJuneLiteOneLibrary.openWebpage("https://www.luka.moe/june");
            if (!didWork)
            {
                LukaJuneLiteOneLibrary.makePopup("Website", "https://www.luka.moe/june");
            }
        }


        // footer
        EditorGUILayout.Space();
        LukaJuneLiteOneLibrary.makeText("june lite " + LukaJuneLiteOneLibrary.getHeart() + " version one (rainbow road)", styFooter);
        LukaJuneLiteOneLibrary.makeText(LukaJuneLiteOneLibrary.getHeart() + " <b>assets protected under copyright</b> " + LukaJuneLiteOneLibrary.getHeart(), styFooter);



        EditorGUI.EndChangeCheck();

    }


}

#endif