#ifndef LUKA_LITE_PIXEL_INCLUDED
#define LUKA_LITE_PIXEL_INCLUDED

//|===============================================|
//|			   pixel func. + f.x.				  |
//|===============================================|
void effectDistortion(inout float2 inputUVs,
    float inputStyle, float inputPowerX,
    float inputPowerY, float inputSpeedX,
    float inputSpeedY, sampler2D inputTexture,
    float inputTextureScale, float4 inputTexture_ST,
    float inputWobble, float inputWobbleAmount,
    float inputWobbleSpeed, float inputWobbleCoverage) {
    // distortion effect
    [branch] if (inputStyle != 0) {
        [forcecase] switch (inputStyle) {
            case 2: // wavey
                doDistortWavey(inputUVs, float2(inputPowerX, inputPowerY), float2(inputSpeedX, inputSpeedY));
                break;
            case 3: // texture
                doDistortTexture(inputUVs, inputSpeedX, inputSpeedY, inputPowerX, inputPowerY, inputTexture, inputTextureScale, inputTexture_ST);
                break;
            default: // sincos
                doDistortSincos(inputUVs, float2(inputPowerX, inputPowerY), float2(inputSpeedX, inputSpeedY));
                break;
        }
        [branch] if (inputWobble == 1) {
            doWobble(inputUVs, inputWobbleAmount, inputWobbleSpeed, inputWobbleCoverage);
        }
    }
}

void effectBorder(inout float4 inputColors,
    inout float2 inputStatus, float inputStyle,
    float inputPower, float inputSoften,
    float4 inputColor, float2 inputUVs) {
    // border effect
    [branch] if (inputStyle != 0) {
        // porting from june pro
        float2 inputBorderCalculations = float2(inputPower, inputPower);
        float2 inputSoftenCalculations = float2(0, 0);
        switch (inputStyle) {
            case 2: // horizontal
                // hard edge
                float borderLeft = step(inputBorderCalculations.x, inputUVs.x);
                float borderRight = step(inputBorderCalculations.y, 1.0 - inputUVs.x);
                inputStatus.x = borderLeft + borderRight;
                // soft edge
                [branch] if (inputSoften != 0) {
                    // ummm... let's make it squishy! :adorbs:
                    float distanceToMidX = abs(inputUVs.x - 0.5);
                    // remap it from 0 to 1 to 0 to inputSoften
                    inputStatus.y = saturate(doRemap(distanceToMidX, 0, 1.0, 0, inputSoften));
                }
                break;
            case 3: // box
                // hard edge
                float borderBLeft = step(inputBorderCalculations.x, inputUVs.x);
                float borderBRight = step(inputBorderCalculations.y, 1.0 - inputUVs.x);
                float borderBTop = step(inputBorderCalculations.x, inputUVs.y);
                float borderBBottom = step(inputBorderCalculations.y, 1.0 - inputUVs.y);
                inputStatus.x = borderBTop + borderBBottom;
                inputStatus.x += borderBLeft + borderBRight;
                // soft edge
                [branch] if (inputSoften != 0) {
                    // ummm... let's make it squishy! :adorbs:
                    float distanceToMidBX = abs(inputUVs.x - 0.5);
                    // remap it from 0 to 1 to 0 to inputSoften
                    inputStatus.y += saturate(doRemap(distanceToMidBX, 0, 1.0, 0, inputSoften));
                    // ummm... let's make it squishy! :adorbs:
                    float distanceToMidBY = abs(inputUVs.y - 0.5);
                    // remap it from 0 to 1 to 0 to inputSoften
                    inputStatus.y += saturate(doRemap(distanceToMidBY, 0, 1.0, 0, inputSoften));	
                }
                break;
            default: // vertical
                // hard edge
                float borderTop = step(inputBorderCalculations.x, inputUVs.y);
                float borderBottom = step(inputBorderCalculations.y, 1.0 - inputUVs.y);
                inputStatus.x = borderTop + borderBottom;
                // soft edge 
                [branch] if (inputSoften != 0) {
                    // ummm... let's make it squishy! :adorbs:
                    float distanceToMidY = abs(inputUVs.y - 0.5);
                    // remap it from 0 to 1 to 0 to inputSoften
                    inputStatus.y = saturate(doRemap(distanceToMidY, 0, 1.0, 0, inputSoften));	
                }
                break;
        }
    }
}

void effectColoring(inout float4 inputColors,
    float4 inputRGBMultiply, float4 inputRGBOverlay,
    float inputOverlayTransparency, float inputHSVStyle,
    float inputHSVh, float inputHSVs, 
    float inputHSVv, float inputInvert,
    float inputDrain, float inputDarkness,
    float inputBrightness, float inputEmission,
    float inputPosterization, float inputGrading,
    float inputGradingTone) {
    // coloring effect 
    // note to self: no "universal toggle" for this module
    // rgb 
    inputColors.rgb *= inputRGBMultiply.rgb;
    inputColors.rgb = lerp(inputColors.rgb, inputRGBOverlay.rgb, inputOverlayTransparency);
    // hsv
    [branch] if (inputHSVStyle != 0) {
        inputColors.rgb = doCsRGBtoHSV(inputColors.rgb);
        switch (inputHSVStyle) {
            case 1: // multiply
                inputColors.r *= inputHSVh;
                inputColors.g *= inputHSVs;
                inputColors.b *= inputHSVv;
                break;
            default: // add
                inputColors.r += inputHSVh;
                inputColors.g += inputHSVs;
                inputColors.b += inputHSVv;
                break;
        }
        inputColors.rgb = doCsHSVtoRGB(inputColors.rgb);
    }
    // invert
    [branch] if (inputInvert != 0) {
        inputColors.rgb = lerp(inputColors.rgb, 1.0 - inputColors.rgb, inputInvert);
    }
    // drain
    [branch] if (inputDrain != 0) {
        inputColors.rgb = lerp(inputColors.rgb, getLuma(inputColors.rgb), inputDrain);
    }
    // darkness
    inputColors.rgb *= (1.0 - inputDarkness);
    // brightness
    inputColors.rgb *= (1.0 + inputBrightness);
    // emission
	[branch] if (inputEmission != 1.0) {
		inputColors.rgb *= approxPow(inputEmission, getLuma(inputColors.rgb));
	} 
    // posterization
    [branch] if (inputPosterization != 0) {
        doPosterize(inputColors.rgb, -inputPosterization);
    }
    // grading
    [branch] if (inputGrading != 0) {
        doBleachBypass(inputColors, inputGrading, inputGradingTone);
    }
}

void effectBlur(inout float4 inputColors,
    float2 inputUVs, float inputStyle,
    float inputRadius, float inputPower, 
    float inputTransparency, float4 inputColor,
    sampler2D inputPass, float2 inputZoomUVs) {
    // blur effect
    [branch] if (inputStyle != 0) {
        float4 cleanColors = inputColors;
        [branch] if (inputRadius != 1) inputPower *= makeRadialSmooth(1, inputRadius, inputUVs);
        switch (inputStyle) {
            case 2: // radial
                #ifdef _KEYWORD_ENABLE_LITE_QUALITY
                // high quality 
                [fastopt] for (int radialHQ = 0; radialHQ < 24; radialHQ++) {
                    float thisStepRadial = ((float)radialHQ) / 24.0;
                    float2 thisUVs = lerp(inputUVs, inputZoomUVs, thisStepRadial * inputPower);
                    inputColors.rgb += (tex2D(inputPass, thisUVs).rgb * lerp(float3(1, 1, 1), inputColor.rgb, thisStepRadial));
                }
                inputColors.rgb /= 24.0;
                #else
                // low quality
                [fastopt] for (int radialLQ = 0; radialLQ < 10; radialLQ++) {
                    float thisStepRadial = ((float)radialLQ) / 10.0;
                    float2 thisUVs = lerp(inputUVs, inputZoomUVs, thisStepRadial * inputPower);
                    inputColors.rgb += (tex2D(inputPass, thisUVs).rgb * lerp(float3(1, 1, 1), inputColor.rgb, thisStepRadial));
                }
                inputColors.rgb /= 10.0;
                #endif 
                break;
            case 3: // chromatic
                inputPower /= 10.0;
                #ifdef _KEYWORD_ENABLE_LITE_QUALITY
                // high quality 
                [fastopt] for (int chromaticHQ = 0; chromaticHQ < 16; chromaticHQ++) {
                    float thisStep = ((float)chromaticHQ / 16.0);
                    float3 thisColor = float3(0, 0, 0);
                    thisColor.r = tex2D(inputPass, inputUVs + float2(inputPower * thisStep, 0)).r;
                    thisColor.g = tex2D(inputPass, inputUVs).g;
                    thisColor.b = tex2D(inputPass, inputUVs - float2(inputPower * thisStep, 0)).b;
                    inputColors.rgb += thisColor.rgb;
                }
                inputColors.rgb /= 16.0;
                #else 
                // low quality
                inputColors.r = tex2D(inputPass, inputUVs + float2(inputPower, 0)).r;
                inputColors.b = tex2D(inputPass, inputUVs + float2(-inputPower, 0)).b;
                #endif 
                break;
            default: // box
                inputPower *= 0.01;
                float blurIterations[2] = {0, 0};
                #ifdef _KEYWORD_ENABLE_LITE_QUALITY
                // high quality 
                [fastopt] for (int iterBoxBasicX = -4; iterBoxBasicX < 4; iterBoxBasicX++) {
                    blurIterations[0] = iterBoxBasicX;
                    [fastopt] for (int iterBoxBasicY = -4; iterBoxBasicY < 4; iterBoxBasicY++) {
                        blurIterations[1] = iterBoxBasicY;
                        inputColors.rgb = blurKernelBoxFast(inputColors.rgb, inputPower, blurIterations, inputUVs, inputPass, inputColor);
                    }
                }
                inputColors /= 64.0;
                #else
                // low quality
                [fastopt] for (int iterBoxZoomiesX = -1; iterBoxZoomiesX < 2; iterBoxZoomiesX++) {
                    blurIterations[0] = iterBoxZoomiesX;
                    [fastopt] for (int iterBoxZoomiesY = -1; iterBoxZoomiesY < 2; iterBoxZoomiesY++) {
                        blurIterations[1] = iterBoxZoomiesY;
                        inputColors.rgb = blurKernelBoxFast(inputColors.rgb, inputPower, blurIterations, inputUVs, inputPass, inputColor);		
                    }
                }
                inputColors /= 10.0;
                #endif 
                break;
        }
        inputColors = lerp(cleanColors, inputColors, inputTransparency);
    }
}

void effectFilterVignette(inout float4 inputColors,
    float2 inputUVs, float inputPower, 
    float4 inputColor, float inputStyle) {
    // vignette effect
    [branch] if (inputStyle != 0) {
        float2 vignetteUVs = (inputUVs * (1.0 - inputUVs)) * screenRatio;
        float vignettePower = (approxPow(vignetteUVs.x * vignetteUVs.y * 10.0, inputPower));
        inputColors.rgb = lerp(inputColor.rgb, inputColors.rgb, vignettePower);
    }
}

void effectFilterColorCrush(inout float4 inputColors,
    float inputPower, float inputStyle) {
    // color crush effect
    [branch] if (inputStyle != 0) {
        inputColors.rgb = lerp(inputColors.rgb, (1.0 - inputColors.rgb) * -1000 + inputColors.rgb * 1000 * inputPower, saturate(inputPower));
    }
}

void effectFilterDuotone(inout float4 inputColors,
    float inputThreshold, float inputTransparency,
    float4 inputColorOne, float4 inputColorTwo,
    float inputStyle) {
    // duotone effect
    [branch] if (inputStyle != 0) {
        float duotoneLuma = getLuma(inputColors.rgb);
		float duoToneValue = (duotoneLuma > inputThreshold) ?
		lerp(0.5, 1.0, smoothstep(inputThreshold, inputThreshold + 0.5, duotoneLuma)) : 
		lerp(0.0, 0.5, smoothstep(inputThreshold - 0.5, inputThreshold, duotoneLuma));
		inputColors.rgb = lerp(inputColors, lerp(inputColorOne.rgb, inputColorTwo.rgb, duoToneValue), inputTransparency);
    }
}

void effectFilterRainbow(inout float4 inputColors,
    float inputStyle, float inputSaturation,
    float inputSpeed) {
    // rainbow effect
    [branch] if (inputStyle != 0) {
        inputSpeed *= _Time.y;
        float3 rainbowColors = float3(doCsSmoothHSVtoRGB(float3(inputSpeed, inputSaturation, 1.0)));
        inputColors.rgb *= rainbowColors.rgb;
    }
}

void effectFilterFilm(inout float4 inputColors,
    float inputStyle, float inputAmount,
    float2 inputUVs) {
    // film effect
    [branch] if (inputStyle != 0) {
        float filmTime = sin(floor(_Time.y * 10.0));
		float lineUVs = inputUVs.x;
        lineUVs += 0.5;
        lineUVs = floor(lineUVs * 1000) / 1000;
        float lineSeedDensity = makeNoiseRandomI2O1(lineUVs * filmTime, lineUVs * filmTime);
        float lineSeedChance = makeNoiseRandomI2O1(-lineUVs * filmTime, -lineUVs * filmTime);
        float filmValue = 0;
        if (lineSeedChance < inputAmount) {
            filmValue = 0.5;
        } 
        inputColors.rgb = lerp(inputColors.rgb, float3(0, 0, 0), filmValue);
    }
}

void effectFilterGrain(inout float4 inputColors,
    float inputStyle, float inputTransparency,
    float4 inputColor, float2 inputUVs) {
    // grain effect
    [branch] if (inputStyle != 0) {
        float grainValue = makeNoisePerlin(inputUVs + frac(_Time.y), 500);
        inputColors.rgb = lerp(inputColors.rgb, inputColor.rgb, grainValue * inputTransparency);
    }
}

void effectFilterVHS(inout float4 inputColors,
    float inputStyle, float inputAmount,
    float2 inputUVs) {
    // vhs effect
    [branch] if (inputStyle != 0) {
        float vhsUVs = inputUVs.y;
	    vhsUVs += frac(0.2 * _Time.y);
		float vhsLines = fmod((vhsUVs * inputAmount), 2) / 3;
        inputColors.rgb = lerp(inputColors.rgb, float3(0, 0, 0), vhsLines);
    }
}

void effectFilterGradient(inout float4 inputColors,
    float inputStyle, float inputTransparency,
    float4 inputColorOne, float4 inputColorTwo,
    float2 inputUVs) {
    // gradient effect
    [branch] if (inputStyle != 0) {
        inputColors.rgb *= lerp(float3(1, 1, 1), lerp(inputColorOne.rgb, inputColorTwo.rgb, inputUVs.x), inputTransparency);
    }
}

void effectFilterOutline(inout float4 inputColors,
    float inputStyle, float inputWidth,
    float inputTolerance, float4 inputColor,
    float2 inputUVs, sampler2D inputPass) {
    // outline effect
    [branch] if (inputStyle != 0) {
        float outlineValue = makeSobelEdge(inputUVs, inputTolerance, inputWidth, float2(0, 0), inputPass);
        inputColors.rgb = lerp(inputColors.rgb, inputColor.rgb, outlineValue); 
    }
}

void effectFilterGlitch(inout float4 inputColors,
    float inputStyle, float inputAmount,
    float2 inputUVs) {
    // glitch effect
    [branch] if (inputStyle != 0) {
        float2 coloredUVs = inputUVs + 2.0;
        doGlitch(coloredUVs, 2 * inputAmount, 0.5 * inputAmount, 100000000.0, 10.0, 2.5);
        float3 colorGenerated = float3(0, 0, 0);
        float2 colorBlock = floor(inputUVs * 20.0);
        colorGenerated.r = makeNoiseRandomI2O1(colorBlock + 20.0);
        colorGenerated.g = makeNoiseRandomI2O1(colorBlock - 12.0);
        colorGenerated.b = makeNoiseRandomI2O1((colorBlock + 3.0) * 2.0);
        colorGenerated.rgb *= 2.0;
        inputColors.rgb = lerp(inputColors.rgb, inputColors.rgb * colorGenerated, step(coloredUVs.x - 0.5, 0.5));
    }
}

void effectFilterAstral(inout float4 inputColors,
    float inputStyle, float inputZoom,
    float inputTransparency, float4 inputColor,
    float2 inputZoomUVs, float2 inputUVs,
    sampler2D inputPass) {
    // astral effect
    [branch] if (inputStyle != 0) {
        float2 astralUVs = lerp(inputUVs, inputZoomUVs, inputZoom);
        float4 astralColor = tex2D(inputPass, astralUVs);
        astralColor.rgb *= inputColor.rgb;
        astralColor.rgb = lerp(inputColors.rgb, astralColor.rgb, inputTransparency);
        inputColors.rgb += astralColor.rgb;
        inputColors.rgb /= 2.0;
    }
}

void effectFilterNeon(inout float4 inputColors,
    float inputStyle, float inputWidth,
    float inputTransparency, float inputHue,
    float2 inputUVs, sampler2D inputPass) {
    // neon effect
    [branch] if (inputStyle != 0) {
		float neonEdge = makeSobelEdge(inputUVs, 1.0, inputWidth, float2(0, 0), inputPass);
		float3 neonColors = inputColors;
		neonColors.rgb = doCsRGBtoHSV(neonColors.rgb);
		neonColors.r *= inputHue;
		neonColors.g = 1;
		neonColors.b = 1;
		neonColors.rgb = doCsHSVtoRGB(neonColors.rgb);
		neonColors.rgb *= neonEdge;
		inputColors.rgb = lerp(inputColors.rgb, neonColors.rgb, inputTransparency);
    }
}

void effectUVManipulationPixelation(inout float2 inputUVs,
	float inputStyle, float inputPower) {
	// pixelation effect
	[branch] if (inputStyle != 0) {
		float2 pixelateValues = ceil(abs(float2(inputPower, inputPower)));
		inputUVs = (floor((pixelateValues * inputUVs)) / pixelateValues);
	}
}

void effectUVManipulationSpherize(inout float2 inputUVs,
	float inputStyle, float inputPower,
	float2 inputZoomUVs) {
	// spherize effect
	[branch] if (inputStyle != 0) {
		inputPower *= makeRadialSmooth(1.25, 0.2, inputUVs);
		inputUVs = lerp(inputUVs, inputZoomUVs, inputPower);
	}
}

void effectUVManipulationGlitch(inout float2 inputUVs,
	float inputStyle, float inputAmount) {
	// glitch effect
    [branch] if (inputStyle != 0) {
        float2 seedUVs = inputUVs + 2.0;
        doGlitch(seedUVs, 1 * inputAmount, 1.5 * inputAmount, 100000000.0, 5.0, 2.5);
        float2 glitchVal = seedUVs - 2.0;
        glitchVal -= inputUVs;
        glitchVal /= 10.0;
        inputUVs.x += glitchVal.x;
    }
}

void effectOverlay(inout float4 inputColors,
    float inputSizeX, float inputSizeY,
    float inputOffsetX, float inputOffsetY,
    float inputTransparency, sampler2D inputTexture,
    float2 inputUVs, float3 inputDirections) {
    // overlay effect
    if (inputTransparency != 0) {
        float4 overlayControls = float4(inputSizeX, inputSizeY, inputOffsetX, inputOffsetY);
        float2 overlayUVs = makeOverlayUVs(inputDirections, overlayControls);
        if (overlayUVs.x > 1 || overlayUVs.x < 0 || overlayUVs.y > 1 || overlayUVs.y < 0) {
            inputTransparency = 0;
            return;
        }
        float4 overlayColor = tex2D(inputTexture, overlayUVs);
        inputColors.rgb = lerp(inputColors.rgb, overlayColor.rgb, inputTransparency * overlayColor.a);
    }
}

//|===============================================|
//|			    pixel properties				  |
//|===============================================|
#ifdef _KEYWORD_ENABLE_LITE_DISTORTION
float _LiteDistortionStyle, _LiteDistortionPowerX, _LiteDistortionPowerY,
_LiteDistortionSpeedX, _LiteDistortionSpeedY, _LiteDistortionTextureScale,
_LiteDistortionWobble, _LiteDistortionWobblePower, _LiteDistortionWobbleSpeed,
_LiteDistortionWobbleCoverage;
sampler2D _LiteDistortionTexture;
float4 _LiteDistortionTexture_ST;
#endif
#ifdef _KEYWORD_ENABLE_LITE_BORDER
float _LiteBorderStyle, _LiteBorderPower, _LiteBorderSoften;
float4 _LiteBorderColor;
#endif
#ifdef _KEYWORD_ENABLE_LITE_COLORING
float4 _LiteColoringRGBMultiply, _LiteColoringRGBOverlay;
float _LiteColoringRGBOverlayTransparency, _LiteColoringHSVStyle, _LiteColoringHSVh,
_LiteColoringHSVs, _LiteColoringHSVv, _LiteColoringInvert, _LiteColoringDrain, 
_LiteColoringDarkness, _LiteColoringBrightness, _LiteColoringEmission, 
_LiteColoringPosterization, _LiteColoringColorGrading, _LiteColoringColorGradingTone;
#endif
#ifdef _KEYWORD_ENABLE_LITE_BLUR
float _LiteBlurStyle, _LiteBlurRadius, _LiteBlurTransparency,
_LiteBlurPower;
float4 _LiteBlurColor;
#endif
#ifdef _KEYWORD_ENABLE_LITE_FILTER
float _LiteFilterStyle, _LiteFilterVignette, _LiteFilterVignettePower,
_LiteFilterColorCrush, _LiteFilterColorCrushPower,
_LiteFilterDuotone, _LiteFilterDuotoneTransparency,
_LiteFilterDuotoneThreshold, _LiteFilterRainbow,
_LiteFilterRainbowSaturation, _LiteFilterRainbowSpeed, _LiteFilterFilm,
_LiteFilterFilmAmount, _LiteFilterGrain, _LiteFilterGrainAmount, _LiteFilterVHS,
_LiteFilterVHSAmount, _LiteFilterGradient,
_LiteFilterGradientTransparency, _LiteFilterOutline,
_LiteFilterOutlineWidth, _LiteFilterOutlineTolerance,
_LiteFilterGlitch, _LiteFilterGlitchAmount, _LiteFilterAstral,
_LiteFilterAstralZoom, _LiteFilterAstralTransparency, _LiteFilterNeon,
_LiteFilterNeonWidth, _LiteFilterNeonTransparency, _LiteFilterNeonHue;
float4 _LiteFilterVignetteColor, _LiteFilterDuotoneColorOne, _LiteFilterDuotoneColorTwo, 
_LiteFilterGradientLHS, _LiteFilterGradientRHS, _LiteFilterOutlineColor,
_LiteFilterGrainColor, _LiteFilterAstralColor;
#endif
#ifdef _KEYWORD_ENABLE_LITE_OVERLAY
float _LiteOverlaySizeX, _LiteOverlaySizeY, _LiteOverlayOffsetX,
_LiteOverlayOffsetY, _LiteOverlayTransparency;
sampler2D _LiteOverlayTexture;
#endif

//|===============================================|
//|				 pixel kernel 					  |
//|===============================================|
float4 pixel(v2f i, VertexOutput vo, float facing : VFACE) : SV_Target : COLOR {
	// preparing the scene
	#if UNITY_SINGLE_PASS_STEREO
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i)
	#endif
	float2 sceneUVs = float2(vo.projPos.xy / vo.projPos.w);
	float4 sceneColors = tex2D(_LukaJuneLitePass, sceneUVs);
	if (i.precalculations.x >= _LiteRenderingFalloffEnd) {
		// out of range, returning and ending
		return sceneColors;
	}
	else {		

        // storing
        float2 cleanUVs = sceneUVs;

        // building the uv
        #ifdef _KEYWORD_ENABLE_LITE_DISTORTION
        effectDistortion(sceneUVs, _LiteDistortionStyle, _LiteDistortionPowerX, _LiteDistortionPowerY, _LiteDistortionSpeedX, _LiteDistortionSpeedY, _LiteDistortionTexture, _LiteDistortionTextureScale, _LiteDistortionTexture_ST, _LiteDistortionWobble, _LiteDistortionWobblePower, _LiteDistortionWobbleSpeed, _LiteDistortionWobbleCoverage);
        #endif

        #ifdef _KEYWORD_ENABLE_LITE_UV_MANIPULATION
        // note to self: why doesn't pixelation work in vertex?
        // pixelation
        effectUVManipulationPixelation(sceneUVs, _LiteUVManipulationPixelation, _LiteUVManipulationPixelationPower);
        // spherize
        effectUVManipulationSpherize(sceneUVs, _LiteUVManipulationSpherize, _LiteUVManipulationSpherizePower, i.zoom.xy);
        // glitch
        effectUVManipulationGlitch(sceneUVs, _LiteUVManipulationGlitch, _LiteUVManipulationGlitchAmount);
        #endif

        sceneUVs = lerp(cleanUVs, sceneUVs, i.precalculations.y);

        // handling out of bounds
        switch (_LiteRenderingOOB) {
            case 0: // clamp
                // nothing
                break;
            case 1: // mirror
                sceneUVs = doScreentearMirrorFancy(sceneUVs);
                break;
            default: // repeat
                sceneUVs = doScreentearRepeat(sceneUVs);
                break;
        }

        // building the pixels
        sceneColors = tex2D(_LukaJuneLitePass, sceneUVs);
        float4 cleanColors = sceneColors;

        // blur 
        #ifdef _KEYWORD_ENABLE_LITE_BLUR
        effectBlur(sceneColors, sceneUVs, _LiteBlurStyle, _LiteBlurRadius, _LiteBlurPower, _LiteBlurTransparency, _LiteBlurColor, _LukaJuneLitePass, i.zoom);
        #endif

        // coloring
        #ifdef _KEYWORD_ENABLE_LITE_COLORING
        effectColoring(sceneColors, _LiteColoringRGBMultiply, _LiteColoringRGBOverlay, _LiteColoringRGBOverlayTransparency, _LiteColoringHSVStyle, _LiteColoringHSVh, _LiteColoringHSVs, _LiteColoringHSVv, _LiteColoringInvert, _LiteColoringDrain, _LiteColoringDarkness, _LiteColoringBrightness, _LiteColoringEmission, _LiteColoringPosterization, _LiteColoringColorGrading, _LiteColoringColorGradingTone);
        #endif

        // filters
        #ifdef _KEYWORD_ENABLE_LITE_FILTER
        // vignette
        effectFilterVignette(sceneColors, sceneUVs, _LiteFilterVignettePower, _LiteFilterVignetteColor, _LiteFilterVignette);
        // color crush
        effectFilterColorCrush(sceneColors, _LiteFilterColorCrushPower, _LiteFilterColorCrush);
        // duotone
        effectFilterDuotone(sceneColors, _LiteFilterDuotoneThreshold, _LiteFilterDuotoneTransparency, _LiteFilterDuotoneColorOne, _LiteFilterDuotoneColorTwo, _LiteFilterDuotone);
        // rainbow
        effectFilterRainbow(sceneColors, _LiteFilterRainbow, _LiteFilterRainbowSaturation, _LiteFilterRainbowSpeed);
        // film
        effectFilterFilm(sceneColors, _LiteFilterFilm, _LiteFilterFilmAmount, sceneUVs);
        // grain
        effectFilterGrain(sceneColors, _LiteFilterGrain, _LiteFilterGrainAmount, _LiteFilterGrainColor, sceneUVs);
        // vhs
        effectFilterVHS(sceneColors, _LiteFilterVHS, _LiteFilterVHSAmount, sceneUVs);
        // gradient
        effectFilterGradient(sceneColors, _LiteFilterGradient, _LiteFilterGradientTransparency, _LiteFilterGradientLHS, _LiteFilterGradientRHS, sceneUVs);
        // outline
        effectFilterOutline(sceneColors, _LiteFilterOutline, _LiteFilterOutlineWidth, _LiteFilterOutlineTolerance, _LiteFilterOutlineColor, sceneUVs, _LukaJuneLitePass);
        // glitch
        effectFilterGlitch(sceneColors, _LiteFilterGlitch, _LiteFilterGlitchAmount, sceneUVs);
        // astral
        effectFilterAstral(sceneColors, _LiteFilterAstral, _LiteFilterAstralZoom, _LiteFilterAstralTransparency, _LiteFilterAstralColor, i.zoom, sceneUVs, _LukaJuneLitePass);
        // neon
        effectFilterNeon(sceneColors, _LiteFilterNeon, _LiteFilterNeonWidth, _LiteFilterNeonTransparency, _LiteFilterNeonHue, sceneUVs, _LukaJuneLitePass);
        #endif

        // overlay
        #ifdef _KEYWORD_ENABLE_LITE_OVERLAY
        effectOverlay(sceneColors, _LiteOverlaySizeX, _LiteOverlaySizeY, _LiteOverlayOffsetX, _LiteOverlayOffsetY, _LiteOverlayTransparency, _LiteOverlayTexture, sceneUVs, i.overlayCoordinates.xyz);
        #endif

        // border
        #ifdef _KEYWORD_ENABLE_LITE_BORDER
        float2 borderStatus = float2(0, 0);
        effectBorder(sceneColors, borderStatus, _LiteBorderStyle, _LiteBorderPower, _LiteBorderSoften, _LiteBorderColor, cleanUVs);
        if (borderStatus.x == 1) return lerp(sceneColors, _LiteBorderColor, _LiteBorderColor.a);
        else if (borderStatus.y != 0) return lerp(sceneColors, _LiteBorderColor, borderStatus.y * _LiteBorderColor.a);
        #endif

        
        // returning
        sceneColors.rgb = lerp(cleanColors, sceneColors.rgb, i.precalculations.y);
        return sceneColors;
    }

}

#endif