//|===============================================|
//|			    <3  readme.txt  <3	        	  |
//|===============================================|
// (✿◠‿◠) henlo! why are you in the code? 
// well, it doesn't really matter. i hope u r happy!
// licensing in all files, and in ui

Shader "luka/june/lite"
{
    Properties
    {
        // rendering 
        //[Header(luka june lite version 0)]
        //[Header(do not animate module toggles)]
        //[Header(luka#8375 www.luka.moe)]
        //[Space(30)]
        //[Header(RENDERING)]
        //[Header(0 is clamp 1 is mirror 2 is repeat for out of bounds)]
        //[Space(20)]
        _LiteRenderingFalloffStart ("Falloff Start", Float) = 15
        _LiteRenderingFalloffEnd ("Falloff End", Float) = 20
        [IntRange] _LiteRenderingOOB ("Out Of Bounds", Range(0, 2)) = 1
        _LiteRenderingPower ("Shader Power", Range(0, 1)) = 1
        [Toggle(_KEYWORD_ENABLE_LITE_QUALITY)] _LiteRenderingQuality ("High Quality", Float) = 0
        // audiolink
        //[Space(30)]
        //[Header(AUDIOLINK)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_AUDIO_LINK)] _LiteAudioLinkModule ("AudioLink Module", Float) = 0
        _LiteAudioLinkBand ("AudioLink Band", Range(0, 4)) = 0
        _LiteAudioLinkPower ("AudioLink Power", Range(0, 1)) = 0
        _LiteAudioLinkMin ("AudioLink Min", Range(0, 1)) = 0
        _LiteAudioLinkMax ("AudioLink Max", Range(0, 2)) = 1
        // coloring
        //[Space(30)]
        //[Header(COLORING)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_COLORING)] _LiteColoringModule ("Coloring Module", Float) = 0
        [HDR] _LiteColoringRGBMultiply ("RGB Multiply", Color) = (1, 1, 1, 1)
        [HDR] _LiteColoringRGBOverlay ("RGB Overlay", Color) = (0, 0, 0, 0)
        _LiteColoringRGBOverlayTransparency ("RGB Overlay Transparency", Range(0, 1)) = 0
        [IntRange] _LiteColoringHSVStyle ("HSV Style", Range(0, 2)) = 0
        _LiteColoringHSVh ("HSV Hue", Range(0, 5)) = 0
        _LiteColoringHSVs ("HSV Saturation", Range(0, 5)) = 0
        _LiteColoringHSVv ("HSV Value", Range(0, 5)) = 0
        _LiteColoringInvert ("Invert", Range(0, 1)) = 0
        _LiteColoringDrain ("Drain", Range(0, 1)) = 0
        _LiteColoringDarkness ("Darkness", Range(0, 1)) = 0
        _LiteColoringBrightness ("Brightness", Range(0, 6)) = 0
        _LiteColoringEmission ("World Emission", Range(1, 10)) = 1
        _LiteColoringPosterization ("Posterization", Range(0, 100)) = 0
        _LiteColoringColorGrading ("Color Grading", Range(0, 1)) = 0
        [PowerSlider(2.0)] _LiteColoringColorGradingTone ("Color Grading Tone", Range(0, 2)) = 0
        // blur 
        //[Space(30)]
        //[Header(BLUR)]
        //[Header(0 is off 1 is box 2 is radial 3 is chromatic)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_BLUR)] _LiteBlurModule ("Blur Module", Float) = 0
        [IntRange] _LiteBlurStyle ("Blur Style", Range(0, 3)) = 0 // 0 = disabled, 1 = box, 2 = radial, 3 = chromatic
        _LiteBlurPower ("Blur Power", Range(0, 1)) = 0
        _LiteBlurRadius ("Blur Radius", Range(1, -2)) = 1
        _LiteBlurTransparency ("Blur Transparency", Range(0, 1)) = 1
        _LiteBlurColor ("Blur Color", Color) = (1, 1, 1, 1)
        // distortion
        //[Space(30)]
        //[Header(DISTORTION)]
        //[Header(0 is off 1 is sincos 2 is wave 3 is texture)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_DISTORTION)] _LiteDistortionModule ("Distortion Module", Float) = 0
        [IntRange] _LiteDistortionStyle ("Distortion Style", Range(0, 3)) = 0 // 0 = dis, 1 = sincos, 2 = wave, 3 = tex
        _LiteDistortionPowerX ("Distortion Power X", Range(0, 1)) = 0.5
        _LiteDistortionPowerY ("Distortion Power Y", Range(0, 1)) = 0.5
        [PowerSlider(2.0)] _LiteDistortionSpeedX ("Distortion Speed X", Range(-2, 2)) = 0.5
        [PowerSlider(2.0)] _LiteDistortionSpeedY ("Distortion Speed Y", Range(-2, 2)) = 0.5
        _LiteDistortionTexture ("Distortion Texture", 2D) = "" {}
        _LiteDistortionTextureScale ("Distortion Texture Scale", Float) = 1
        [IntRange] _LiteDistortionWobble ("Wobble?", Range(0, 1)) = 0
        _LiteDistortionWobblePower ("Wobble Power", Range(0, 1)) = 0
        _LiteDistortionWobbleSpeed ("Wobble Speed", Range(0, 1)) = 0
        _LiteDistortionWobbleCoverage ("Wobble Coverage", Range(20, 80)) = 50
        // fog
        //[Space(30)]
        //[Header(FOG)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_FOG)] _LiteFogModule ("Fog Module", Float) = 0
        _LiteFogDensity ("Fog Density", Range(0, 100)) = 1
        _LiteFogDistribution ("Fog Distribution", Range(0, 100)) = 1
        _LiteFogColor ("Fog Color", Color) = (1, 1, 1, 1)
        _LiteFogSafespace ("Fog Safespace", Range(0, 1)) = 0
        _LiteFogSafespaceSize ("Fog Safespace Size", Range(1, 10)) = 10
        // glitch
        // [Space(30)]
        // [Header(GLITCH)]
        // [Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_GLITCH)] _LiteGlitchModule ("Glitch Module", Float) = 0
        _LiteGlitchScale ("Glitch Scale", Range(0, 15)) = 0
        _LiteGlitchAmount ("Glitch Amount", Range(0, 1)) = 0
        _LiteGlitchUVs ("Glitch UVs", Range(0, 0.25)) = 0
        _LiteGlitchChromatic ("Glitch Chromatic", Range(0, 0.25)) = 0
        // border
        //[Space(30)]
        //[Header(BORDER)]
        //[Header(0 is off 1 is horizontal 2 is vertical)]
        //[Space(20)]   
        [Toggle(_KEYWORD_ENABLE_LITE_BORDER)] _LiteBorderModule ("Border Module", Float) = 0
        [IntRange] _LiteBorderStyle ("Border Style", Range(0, 2)) = 0
        _LiteBorderColor ("Border Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _LiteBorderPower ("Border Power", Range(0, 0.5)) = 0
        _LiteBorderSoften ("Border Soften", Range(0, 30)) = 0
        // overlay
        //[Space(30)]
        //[Header(OVERLAY)]
        //[Space(20)]      
        [Toggle(_KEYWORD_ENABLE_LITE_OVERLAY)] _LiteOverlayModule ("Overlay Module", Float) = 0
        _LiteOverlayTexture ("Overlay Texture", 2D) = "" {}
        _LiteOverlaySizeX ("Overlay Size X", Range(-5, 5)) = 1
        _LiteOverlaySizeY ("Overlay Size Y", Range(-5, 5)) = 1
        _LiteOverlayOffsetX ("Overlay Offset X", Range(-5, 5)) = 0
        _LiteOverlayOffsetY ("Overlay Offset Y", Range(-5, 5)) = 0
        _LiteOverlayTransparency ("Overlay Transparency", Range(0, 1)) = 0
        _LiteOverlayAnimated ("Overlay Animated", Range(0, 1)) = 0
        _LiteOverlayFramesX ("Overlay Frames X", Float) = 0
        _LiteOverlayFramesY ("Overlay Frames Y", Float) = 0
        _LiteOverlayFrames ("Overlay Frames", Float) = 0
        _LiteOverlaySpeed ("Overlay Speed", Float) = 0
        _LiteOverlayScrub ("Overlay Scrub", Float) = 0
        // uv manipulation
        //[Space(30)]
        //[Header(UV MANIPULATION)]
        //[Header(0 is off 1 is on for all fx here)]
        //[Header(shake 0 is off 1 is rough 2 is smooth 3 is circular)]
        //[Space(20)]      
        [Toggle(_KEYWORD_ENABLE_LITE_UV_MANIPULATION)] _LiteUVManipulationModule ("UV Manipulation Module", Float) = 0
	    _LiteUVManipulationTransformationSlantTL("Slant Top Left", Range(0, 2)) = 0
		_LiteUVManipulationTransformationSlantTR("Slant Top Right", Range(0, 2)) = 0
		_LiteUVManipulationTransformationSlantBL("Slant Bottom Left", Range(0, 2)) = 0
		_LiteUVManipulationTransformationSlantBR("Slant Bottom Right", Range(0, 2)) = 0
		_LiteUVManipulationTransformationFlipX("Flip X", Range(0, 1)) = 0
		_LiteUVManipulationTransformationFlipY("Flip Y", Range(0, 1)) = 0
		_LiteUVManipulationTransformationStretchX("Stretch X", Range(0, 0.5)) = 0
		_LiteUVManipulationTransformationStretchY("Stretch Y", Range(0, 0.5)) = 0
        [PowerSlider(2.0)] _LiteUVManipulationMoveX ("Move X", Range(-1.5, 1.5)) = 0
        [PowerSlider(2.0)] _LiteUVManipulationMoveY ("Move Y", Range(-1.5, 1.5)) = 0
        [IntRange] _LiteUVManipulationShakeStyle ("Shake Style", Range(0, 3)) = 0
        _LiteUVManipulationShakePowerX ("Shake Power", Range(0, 1)) = 0
        _LiteUVManipulationShakePowerY ("Shake Power", Range(0, 1)) = 0
        _LiteUVManipulationShakeSpeedX ("Shake Speed", Range(0, 1)) = 0
        _LiteUVManipulationShakeSpeedY ("Shake Speed", Range(0, 1)) = 0
        [IntRange] _LiteUVManipulationPixelation ("Pixelation", Range(0, 1)) = 0
        _LiteUVManipulationPixelationPower ("Pixelation Power", Range(2000, 1)) = 2000
        [IntRange] _LiteUVManipulationRotation ("Rotation", Range(0, 1)) = 0
        [PowerSlider(3.0)] _LiteUVManipulationRotationAngle ("Rotation Angle", Range(-360, 360)) = 0
        [IntRange] _LiteUVManipulationSpherize ("Spherize", Range(0, 1)) = 0
        [PowerSlider(3.0)] _LiteUVManipulationSpherizePower ("Spherize Power", Range(-1, 1)) = 0
        // filters
        //[Space(30)]
        //[Header(FILTERS)]
        //[Header(0 is off 1 is on for all fx here)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_FILTER)] _LiteFilterModule ("Filter Module", Float) = 0
        [IntRange] _LiteFilterVignette ("Vignette", Range(0, 1)) = 0
        _LiteFilterVignettePower ("Vignette Power", Range(0, 1.0)) = 0.05
        [HDR] _LiteFilterVignetteColor ("Vignette Color", Color) = (0.0, 0.0, 0.0, 1.0)
        [IntRange] _LiteFilterColorCrush ("Color Crush", Range(0, 1)) = 0
        _LiteFilterColorCrushPower ("Color Crush Power", Range(0, 10)) = 0
        [IntRange] _LiteFilterDuotone ("Duotone", Range(0, 1)) = 0
        _LiteFilterDuotoneTransparency ("Duotone Transparency", Range(0, 1)) = 0
        [HDR] _LiteFilterDuotoneColorOne ("Duotone Color One", Color) = (0.0, 0.0, 0.0, 1.0)
        [HDR] _LiteFilterDuotoneColorTwo ("Duotone Color Two", Color) = (0.0, 0.0, 0.0, 1.0)
        _LiteFilterDuotoneThreshold ("Duotone Threshold", Range(0, 1)) = 0.5
        [IntRange] _LiteFilterRainbow ("Rainbow", Range(0, 1)) = 0
        _LiteFilterRainbowSaturation ("Rainbow Saturation", Range(0, 2)) = 1.0
        _LiteFilterRainbowSpeed ("Rainbow Speed", Range(0, 1)) = 0.5
        [IntRange] _LiteFilterFilm ("Film", Range(0, 1)) = 0
        _LiteFilterFilmAmount ("Film Amount", Range(0, 1)) = 0.5
        [IntRange] _LiteFilterGrain ("Grain", Range(0, 1)) = 0
        _LiteFilterGrainAmount ("Grain Amount", Range(0, 1)) = 0.5
        [HDR] _LiteFilterGrainColor ("Grain Color", Color) = (0, 0, 0, 1)
        [IntRange] _LiteFilterVHS ("VHS", Range(0, 1)) = 0
        _LiteFilterVHSAmount ("VHS Amount", Range(0, 20)) = 0.5
        [IntRange] _LiteFilterGradient ("Gradient", Range(0, 1)) = 0
        [HDR] _LiteFilterGradientLHS ("Gradient LHS", Color) = (0.7, 0.2, 0.3, 1.0)
        [HDR] _LiteFilterGradientRHS ("Gradient RHS", Color) = (0.2, 0.3, 0.7, 1.0)
        _LiteFilterGradientTransparency ("Gradient Transparency", Range(0, 1)) = 1.0
        [IntRange] _LiteFilterOutline ("Outline", Range(0, 1)) = 0
        _LiteFilterOutlineWidth ("Outline Power", Range(0, 5)) = 3
        _LiteFilterOutlineTolerance ("Outline Tolerance", Range(0, 5)) = 3
        [HDR] _LiteFilterOutlineColor ("Outline Color", Color) = (0.0, 0.0, 1.0, 1.0)
        [IntRange] _LiteFilterAstral ("Astral", Range(0, 1)) = 0
        _LiteFilterAstralZoom ("Astral Zoom", Range(0, 1)) = 0
        _LiteFilterAstralTransparency ("Astral Transparency", Range(0, 1)) = 0.6
        [HDR] _LiteFilterAstralColor ("Astral Color", Color) = (1.0, 1.0, 1.0, 1.0)
        [IntRange] _LiteFilterNeon ("Neon", Range(0, 1)) = 0
        _LiteFilterNeonWidth ("Neon Width", Range(0, 2)) = 0.5
        _LiteFilterNeonTransparency ("Neon Transparency", Range(0, 1)) = 1.0
        _LiteFilterNeonHue ("Neon Hue", Range(0, 5)) = 1.0
        // zoom
        //[Space(30)]
        //[Header(ZOOM)]
        //[Header(0 for range is use global range 1 is use custom falloff)]
        //[Space(20)]
        [Toggle(_KEYWORD_ENABLE_LITE_ZOOM)] _LiteZoomModule ("Zoom Module", Float) = 0
        _LiteZoomPower ("Zoom Power", Range(-1, 1)) = 0
        [IntRange] _LiteZoomRangeStyle ("Zoom Range Style", Range(0, 1)) = 0
        _LiteZoomRangeStart ("Zoom Range Start", Float) = 5
        _LiteZoomRangeEnd ("Zoom Range End", Float) = 10
        // fix for blocked avatars
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        [HideInInspector] _Alpha ("Alpha", Range(0, 1)) = 0
        [HideInInspector] _Color ("Color", Color) = (1, 1, 1, 0)
        [HideInInspector] _Transparency ("Transparency", Range(0, 1)) = 0
        [HideInInspector] _Clip ("Clip", Range(0, 1)) = 0
    }
    SubShader
    {

		//|===============================================|
		//|			render settings  					  |
		//|===============================================|
	    Tags 
		{
			"RenderType" = "Transparent"
			"Queue" = "Overlay+2000"
			"LightMode" = "Always"
			"IgnoreProjector" = "True"
			"DisableBatching"="True" 
			"ForceNoShadowCasting" = "True" 
			"PreviewType"="Plane"
            "VRCFallback" = "Hidden"
		}

        Blend One Zero
        ZTest Always
        Cull Off 

		//|===============================================|
		//|				 lite pass					      |
		//|===============================================|
        GrabPass { Tags {"LightMode" = "ForwardBase" } "_JLPass" }
        Pass
        {
		    Name "JLPass"
			Tags {"LightMode" = "ForwardBase" }
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment pixel
			#pragma exclude_renderers ps3 ps4 psp2 xbox360 xboxone wiiu n3ds flash switch nomrt 
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_QUALITY
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_COLORING
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_BLUR
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_DISTORTION
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_FOG
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_GLITCH
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_BORDER
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_OVERLAY
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_UV_MANIPULATION
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_FILTER
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_ZOOM
            #pragma shader_feature_local _ _KEYWORD_ENABLE_LITE_AUDIO_LINK
            #pragma target 5.0
            #include "UnityCG.cginc"
            #include "Resources/Code/Includes/JLUniversal.cginc"
            #include "Resources/Code/Includes/JLVertex.cginc"
            #include "Resources/Code/Includes/JLPixel.cginc"
            ENDCG
        }
    }

    //|===============================================|
	//|			 <3 made with love <3   			  |
	//|===============================================|
	// UNITY_SHADER_NO_UPGRADE
	FallBack "Diffuse"
	CustomEditor "JuneLite.JuneLiteUI"
}