#ifndef LUKA_LITE_VERTEX_INCLUDED
#define LUKA_LITE_VERTEX_INCLUDED

//|===============================================|
//|				 structures 					  |
//|===============================================|
struct v2f {
	float4 grabPos : TEXCOORD0;
	float4 pos : SV_POSITION;
	float2 zoom : TEXCOORD7;
	float2 precalculations : TEXCOORD9;
	float4 postcalculations : TEXCOORD4;
	float4 overlayCoordinates : TEXCOORD3;
	#if UNITY_SINGLE_PASS_STEREO
	UNITY_VERTEX_OUTPUT_STEREO 
	#endif
};
struct appdata {
	float4 vertex : POSITION;
	float2 uv : TEXCOORD6;
	#if UNITY_SINGLE_PASS_STEREO
	UNITY_VERTEX_INPUT_INSTANCE_ID
	#endif
};
struct VertexInput {
	float4 vertex : POSITION;
	float4 worldPos : TEXCOORD2;
};
struct VertexOutput {
	float4 pos : SV_POSITION;
	float4 projPos : TEXCOORD0;
};

//|===============================================|
//|			   vertex func. + f.x.				  |
//|===============================================|
void effectZoom(inout float4 inputRawUVs, 
    float inputZoom, float inputFalloff,
	float inputRangeStyle, float inputRangeStart,
	float inputRangeEnd) {
        // zoom effect
        [branch] if (inputZoom != 0) {
			// custom range?
			[branch] if (inputRangeStyle == 1) {
				inputFalloff = getFalloffLinear(getDistance(), inputRangeStart, inputRangeEnd);
			}
            inputRawUVs.xy = lerp(inputRawUVs.xy, TransformStereoScreenSpaceTex(0.5, 1.0) * inputRawUVs.w, inputZoom * inputFalloff);
        }
}

void effectUVManipulationTransformation(inout float2 inputUVs,
	float inputSlantTL, float inputSlantTR, 
	float inputSlantBL, float inputSlantBR,
	float inputFlipX, float inputFlipY,
	float inputStretchX, float inputStretchY) {
	// transformation section of uv manipulation
	// slant (previously named diagonal)
	inputUVs.y += inputSlantTR * inputUVs.x;
	inputUVs.y -= inputSlantBR * inputUVs.x;
	inputUVs.y += inputSlantTL * abs(1.0 - inputUVs.x);
	inputUVs.y -= inputSlantBL * abs(1.0 - inputUVs.x);
	// flip 
	// note to self: toggle it on or off
	inputUVs.x = lerp(inputUVs.x, (1.0 - inputUVs.x), inputFlipX);
	inputUVs.y = lerp(inputUVs.y, (1.0 - inputUVs.y), inputFlipY);
	// stretch
	// note to self: might not work in vr? maybe use zoom
	// note to self: limit it to 0.5
	inputUVs.x = lerp(inputUVs.x, (1.0 - inputUVs.x), inputStretchX);
	inputUVs.y = lerp(inputUVs.y, (1.0 - inputUVs.y), inputStretchY);
}

void effectUVManipulationShake(inout float2 inputUVs,
	float inputPowerX, float inputPowerY,
	float inputSpeedX, float inputSpeedY,
	float inputStyle) {
	// shake effect
	[branch] if (inputStyle != 0) {
		float2 shakeValue = float2(0, 0);
		switch (inputStyle) {
			case 1: // rough
				inputPowerX /= 10.0;
				inputPowerY /= 10.0;
				shakeValue.x = makeNoiseRandomRangeFloat(makeNoisePerlin(1.0 + (_Time.y * inputSpeedX), 1), -inputPowerX, inputPowerX);
				shakeValue.y = makeNoiseRandomRangeFloat(makeNoisePerlin(1.0 - (_Time.y * inputSpeedY), 1), -inputPowerY, inputPowerY);
				break;
			case 2: // smooth
				inputPowerX /= 10.0;
				inputPowerY /= 10.0;
				inputSpeedX *= 25.0;
				inputSpeedY *= 25.0;
				shakeValue.x = (inputPowerX * sin(_Time.y * inputSpeedX));
				shakeValue.y = (inputPowerY * sin(_Time.y * inputSpeedY));
				break;
			default: // circular
				// note to self: using custom rotation matrix
				// note to self: works better than rm function.. :(
				inputSpeedX *= 20.0;
				inputSpeedY *= 20.0;
				float3 shakeRotation = float3(0, 0, 0);
				shakeRotation.x = inputSpeedX * _Time.y;
				shakeRotation.y = sin(shakeRotation.x);
				shakeRotation.z = cos(shakeRotation.x);
				float2x2 shakeMatrix = float2x2(shakeRotation.z, -shakeRotation.y, shakeRotation.y, shakeRotation.z);
				shakeValue.xy = (mul(normalize(float2(1, 1)), shakeMatrix) / (inputPowerX * 500));
				break;
		}
		inputUVs += shakeValue;
	}
}

void effectUVManipulationTwoDRotation(inout float4 inputPosition,
	float4 inputVertex, float inputStyle,
	float inputPower, float inputFalloff) {
	// twod rotation section of uv manipulation
	[branch] if (inputStyle != 0) {
		inputPower *= inputFalloff;
		float rSin, rCos;
		float4 rotationViewPosition = inputVertex;
		sincos(radians(inputPower), rSin, rCos);
		rotationViewPosition.xyz = UnityObjectToViewPos(rotationViewPosition.xyz);
		rotationViewPosition.xy = mul(float2x2(rCos, -rSin, rSin, rCos), rotationViewPosition.xy);
		inputPosition = UnityViewToClipPos(rotationViewPosition);
	}
}

//|===============================================|
//|			   vertex properties				  |
//|===============================================|
#ifdef _KEYWORD_ENABLE_LITE_ZOOM
float _LiteZoomPower, _LiteZoomRangeStyle, _LiteZoomRangeStart,
_LiteZoomRangeEnd;
#endif
#ifdef _KEYWORD_ENABLE_LITE_UV_MANIPULATION
float _LiteUVManipulationTransformationSlantTL, _LiteUVManipulationTransformationSlantTR,
_LiteUVManipulationTransformationSlantBL, _LiteUVManipulationTransformationSlantBR,
_LiteUVManipulationTransformationFlipX, _LiteUVManipulationTransformationFlipY,
_LiteUVManipulationTransformationStretchX, _LiteUVManipulationTransformationStretchY,
_LiteUVManipulationMoveX, _LiteUVManipulationMoveY, _LiteUVManipulationShakeStyle,
_LiteUVManipulationShakePowerX, _LiteUVManipulationShakePowerY, _LiteUVManipulationShakeSpeedX,
_LiteUVManipulationShakeSpeedY, _LiteUVManipulationPixelation, _LiteUVManipulationPixelationPower,
_LiteUVManipulationRotation, _LiteUVManipulationRotationAngle, _LiteUVManipulationSpherize,
_LiteUVManipulationSpherizePower, _LiteUVManipulationGlitch, _LiteUVManipulationGlitchAmount;
#endif

//|===============================================|
//|			   vertex kernel					  |
//|===============================================|
v2f vert(appdata_base v, appdata p, VertexInput vi, float4 pos : POSITION) {
	v2f o = (v2f)0;
	
	#if UNITY_SINGLE_PASS_STEREO
    UNITY_SETUP_INSTANCE_ID(v); 
    UNITY_INITIALIZE_OUTPUT(v2f, o); 
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	#endif
	
	o.overlayCoordinates = mul(unity_ObjectToWorld, v.vertex);
	o.pos = UnityObjectToClipPos(v.vertex);
	o.precalculations.x = getDistance(); 
	o.precalculations.y = getFalloffLinear(o.precalculations.x, _LiteRenderingFalloffStart, _LiteRenderingFalloffEnd);
	o.precalculations.y *= _LiteRenderingPower;
	o.grabPos = ComputeGrabScreenPos(o.pos);
	o.zoom.xy = TransformStereoScreenSpaceTex(0.5, 1.0);
	o.postcalculations.xyw = o.grabPos.xyw;

	// modifying the vertex
	if (o.precalculations.x < _LiteRenderingFalloffEnd) {
        #ifdef _KEYWORD_ENABLE_LITE_ZOOM
        effectZoom(o.grabPos, _LiteZoomPower, o.precalculations.y, _LiteZoomRangeStyle, _LiteZoomRangeStart, _LiteZoomRangeEnd);
        #endif 
		#ifdef _KEYWORD_ENABLE_LITE_UV_MANIPULATION
		// testing div out w then transform then multiply w back in
		o.grabPos.xy /= o.grabPos.w;
		float2 cleanGrabPos = o.grabPos.xy;
		// transformation
		effectUVManipulationTransformation(o.grabPos.xy, _LiteUVManipulationTransformationSlantTL, _LiteUVManipulationTransformationSlantTR,
			_LiteUVManipulationTransformationSlantBL, _LiteUVManipulationTransformationSlantBR,
			_LiteUVManipulationTransformationFlipX, _LiteUVManipulationTransformationFlipY,
			_LiteUVManipulationTransformationStretchX, _LiteUVManipulationTransformationStretchY);
		o.grabPos.xy += float2(_LiteUVManipulationMoveX, _LiteUVManipulationMoveY);
		// shake
		effectUVManipulationShake(o.grabPos.xy, _LiteUVManipulationShakePowerX, _LiteUVManipulationShakePowerY,
			_LiteUVManipulationShakeSpeedX, _LiteUVManipulationShakeSpeedY, _LiteUVManipulationShakeStyle);
		o.grabPos.xy = lerp(cleanGrabPos, o.grabPos.xy, o.precalculations.y);
		o.grabPos.xy *= o.grabPos.w;
		// rotation
		effectUVManipulationTwoDRotation(o.pos, v.vertex, _LiteUVManipulationRotation, _LiteUVManipulationRotationAngle, o.precalculations.y);
		#endif
	} 
	
	return o;
}



#endif