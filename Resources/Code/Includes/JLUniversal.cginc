#ifndef LUKA_LITE_UNIVERSAL_INCLUDED
#define LUKA_LITE_UNIVERSAL_INCLUDED

//|===============================================|
//|		       global definitions              	  |
//|===============================================|
#define JUNE_LITE_SAMPLER_DEDICATED(tex) Texture2D tex; SamplerState sampler##tex
#define JUNE_LITE_SAMPLER_TEXTURE_LOD(tex, coords, lod) tex.SampleLevel(sampler##tex, coords, lod)
float _LiteRenderingFalloffStart, _LiteRenderingFalloffEnd, _LiteRenderingOOB,
_LiteRenderingPower;
sampler2D _JLPass;
float4 _JLPass_ST;
static const float3 lumaWeighting = float3(0.29, 0.59, 0.11);
static const float2 screenRatio = float2(1.0, (_ScreenParams.y / _ScreenParams.x));

//|===============================================|
//|		        global functions              	  |
//|===============================================|
float3 getCamera() {
	#if UNITY_SINGLE_PASS_STEREO
		return (unity_StereoWorldSpaceCameraPos[0] + unity_StereoWorldSpaceCameraPos[1]) * 0.5;
	#else
		return _WorldSpaceCameraPos;
	#endif
}

float getDistance() {
	// getting a basic distance away from the center of the object 
	return float(distance(getCamera(), mul(unity_ObjectToWorld, float4(0, 0, 0, 1))));
}

float getFalloff(float inputCameraDistance, 
	float inputFalloffStart, float inputFalloffEnd) {
	return (float((clamp(inputCameraDistance, inputFalloffStart, inputFalloffEnd) - inputFalloffStart) / (inputFalloffEnd - inputFalloffStart)));
}

float getFalloffLinear(float inputCameraDistance, 
	float inputFalloffStart, float inputFalloffEnd) {
	// distance falloff with ~~interpolation~~ now without lerping!
	return float(1.0 - (clamp(inputCameraDistance, inputFalloffStart, inputFalloffEnd) - inputFalloffStart) / (inputFalloffEnd - inputFalloffStart));
}

float2 makeOverlayUVs(float3 inputCoordinates,
	float4 inputControls) {
	// generates a vr-friendly overlay uv
	inputCoordinates = mul(UNITY_MATRIX_V, float4((inputCoordinates - _WorldSpaceCameraPos), 0)).xyz;
	float2 coordinatesNormalized = float2(inputCoordinates.xy / inputCoordinates.z);
	float aspectRatio = (_ScreenParams.z / _ScreenParams.w);
	float2 overlayUVs = float2(0, 0);
	overlayUVs.x = (coordinatesNormalized.x * inputControls.x) + inputControls.z;
	overlayUVs.y = (coordinatesNormalized.y * inputControls.y) + inputControls.w;
	overlayUVs = ((aspectRatio * overlayUVs) * - 1) + 0.5;
	return overlayUVs;
}

void doDistortSincos(inout float2 inputUVs, 
	float2 inputPower, float2 inputSpeed) {
	// fancy trig distortion (~x2 trig + extra calculations)
	float distortX = inputUVs.x * 10 * inputPower.x + (_Time.y * inputSpeed.x);
	float distortY = inputUVs.y * 10 * inputPower.y - (_Time.y * inputSpeed.y);
	inputUVs.x += sin(distortX) * cos(-distortY) * (0.1 * inputPower.x);
	inputUVs.y += sin(-distortY) * cos(distortX) * (0.1 * inputPower.y);
}

void doDistortWavey(inout float2 inputUVs, 
	float2 inputPower, float2 inputSpeed) {
	// zoom-ly!! trig distortion (1/2 trig + less calculations)
    inputSpeed *= 10.0;
	float2 distortTime = float2(_Time.y * inputSpeed.x, _Time.y * inputSpeed.y);
	float2 distortScale = float2(100.0 * 0.1, 100.0 * 0.1) + float2(5.0, 5.0);
	inputUVs.x += lerp(0, cos(inputUVs.y * distortScale.x + distortTime.x) / 10.0, inputPower.x);
	inputUVs.y += lerp(0, sin(inputUVs.x * distortScale.y + distortTime.y) / 10.0, inputPower.y);
}

void doDistortTexture(inout float2 inputUVs,
    float inputDeformSpeedX, float inputDeformSpeedY,
    float inputDeformPowerX, float inputDeformPowerY,
    sampler2D inputDeformMap, float inputDeformMapScale,
    float4 inputDeformMap_ST, float3 inputOverlayCoordinates) {
	// distort the uv with a texture
	float2 distortionOverlayUVs = inputUVs;
	#if UNITY_SINGLE_PASS_STEREO
		distortionOverlayUVs = makeOverlayUVs(inputOverlayCoordinates, float4(0.5, 0.5, 0, 0));
	#endif
	float2 distortMapUV = TRANSFORM_TEX(distortionOverlayUVs * screenRatio, inputDeformMap);
	float2 distortMapFlow = float2(frac(_Time.y * inputDeformSpeedX), frac(_Time.y * inputDeformSpeedY));
	float2 distortionOffset = UnpackNormal(tex2D(inputDeformMap, (distortMapUV * inputDeformMapScale) + distortMapFlow)) * float2(inputDeformPowerX, inputDeformPowerY) * screenRatio.yx;
	inputUVs += distortionOffset;
}

void doWobble(inout float2 inputUVs,
	float inputPower, float inputSpeed,
	float inputWobbleCoverage) {
	// wobble da uvs
	inputUVs.x += cos(inputUVs.x * inputWobbleCoverage * sin(_Time.y * inputSpeed)) * 0.1 * inputPower;
	inputUVs.y += sin(inputUVs.y * inputWobbleCoverage * cos(_Time.y * inputSpeed)) * 0.1 * inputPower;
}

float doRemap(float inputValue,
	float inputOldLow, float inputOldHigh,
	float inputNewLow, float inputNewHigh) {
	// remaps a value within range X-Y to Z-W, taking ranges as four floats
	return float(inputNewLow + (inputValue - inputOldLow) * (inputNewHigh - inputNewLow) / (inputOldHigh - inputOldLow));
}

float3 doCsRGBtoHSV(float3 c) {
	// converting rgb color space to hsv color space
	float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
	float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
	float d = q.x - min(q.w, q.y);
	float E = 1e-10;
	return float3(abs(q.z + (q.w - q.y) / (6.0 * d + E)), d / (q.x + E), q.x);
}

float3 doCsRGBtoHSVapproximation(float3 c) {
	// converting rgb color space to hsv color space
	float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
	float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));
	float d = q.x - min(q.w, q.y);
	// note to self: epsilon precision lowered a LOT to maybe (make up for precision errors??) fix it idkkkk aaa!!! GPUs SUCK
	float E = 0.005;
	return float3(abs(q.z + (q.w - q.y) / (6.0 * d + E)), d / (q.x + E), q.x);
}

float3 doCsHSVtoRGB(float3 inputRgb) {
	// converting hsv color space to rgb color space
	float4 hsvWeight = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	float3 hsvColor = abs(frac(inputRgb.xxx + hsvWeight.xyz) * 6.0 - hsvWeight.www);
	return float3(inputRgb.z * lerp(hsvWeight.xxx, saturate(hsvColor - hsvWeight.xxx), inputRgb.y));
}

float3 doCsSmoothHSVtoRGB(float3 c) {
	// smoothly converting from hsv color space to rgb color space
	// attribution: inigo quilez
	float3 rgb = clamp(abs(fmod(c.x * 6.0 + float3(0.0, 4.0, 2.0), 6.0) - 3.0) - 1.0, 0.0, 1.0);
	rgb = rgb * rgb * (3.0 - 2.0 * rgb);
	return c.z * lerp(float3(1.0, 1.0, 1.0), rgb, c.y);
}

float getLuma(float3 inputColors) {
	// get brightness of pixel
	float3 toLuma = float3(0.29, 0.59, 0.11);
	return float(dot(inputColors, toLuma));
}

float approxPow(float a,
	float b) {
	return a / ((1. - b) * a + b);
}

float2 approxPow(float2 a2,
	float b) {
	float2 approxed = float2(0, 0);
	approxed.x = approxPow(a2.x, b);
	approxed.y = approxPow(a2.y, b);
	return approxed;
}

float3 approxPow(float3 a3,
	float b) {
	float3 approxed = float3(0, 0, 0);
	approxed.r = approxPow(a3.r, b);
	approxed.g = approxPow(a3.g, b);
	approxed.b = approxPow(a3.b, b);
	return approxed;
}

void doBleachBypass(inout float4 inputColors,
	float inputBleachOpacity, float inputBleachTone) {
	float bbLuma = getLuma(inputColors.rgb);
	float bbLumaNormalized = min(1.0, max(0.0, 10.0 * (bbLuma - inputBleachTone)));
	float3 bbHighlights = 2.0 * inputColors.rgb * bbLuma;
	float3 bbShadows = 1.0 - 2.0 * (1.0 - bbLuma) * (1.0 - inputColors.rgb);
	inputColors.rgb = lerp(inputColors.rgb, lerp(bbHighlights, bbShadows, bbLumaNormalized), inputBleachOpacity);
}

void doPosterize(inout float3 inputColors,
	float inputSteps) {
	// performs simple posterization equation
 	inputColors.rgb = floor(inputColors.rgb / (1.0 / inputSteps)) * (1.0 / inputSteps);
}

float3 blurKernelBoxFast(float3 inputColors,
    float inputPower, float inputIteration[2],
    float2 inputUVs, sampler2D inputPass,
	float4 inputColor) {
    // blurs in every direction but also fills in the missing
    float2 boxUVs = inputUVs + float2(inputPower * inputIteration[0], inputPower * inputIteration[1]);
    inputColors += (tex2D(inputPass, boxUVs).rgb * lerp(float3(1, 1, 1), inputColor.rgb, saturate(inputIteration[0] + inputIteration[1])));		
	return inputColors;
}

float2 makeCenter(float2 inputUVs) {
	// get a center coordinate that is vr friendly
	float2 thisCenter;
	#if UNITY_SINGLE_PASS_STEREO
		if (inputUVs.x < 0.5) {
			thisCenter = float2(0.25, 0.5);
		}
		else {
			thisCenter = float2(0.75, 0.5);
		}
	#else
		thisCenter = float2(0.5, 0.5);
	#endif
	return thisCenter;
}

float2 makeDirection(float2 inputUVs) {
	return float2(makeCenter(inputUVs) - inputUVs.xy);
}

float2 makeCenterOffsetted(float2 inputUVs, 
	float inputX, float inputY) {
	// note to self: assuming two floats instead of a float2 is input
	float2 thisCenter;
	#if UNITY_SINGLE_PASS_STEREO
		if (inputUVs.x < 0.5) {
			thisCenter = float2(inputX / 2, inputY);
		}
	else {
		thisCenter = float2(inputX + (inputX / 2), inputY);
	}
	#else
		thisCenter = float2(inputX, inputY);
	#endif
	return thisCenter;
}

float2 makeCenterOffsetted(float2 inputUVs,
	float2 inputOffset) {
	// note to self: assuming a float2 instead of two floats is input
	float2 thisCenter;
	#if UNITY_SINGLE_PASS_STEREO
		if (inputUVs.x < 0.5) {
			thisCenter = float2(inputOffset.x / 2, inputOffset.y);
		}
	else {
		thisCenter = float2(inputOffset.x + (inputOffset.x / 2), inputOffset.y);
	}
	#else
		thisCenter = float2(inputOffset.x, inputOffset.y);
	#endif
	return thisCenter;
}

float2 makeDirectionOffsetted(float2 inputUVs, 
	float inputX, float inputY) {
	// direction with offset
	return float2(makeCenterOffsetted(inputUVs, inputX, inputY) - inputUVs.xy);
}

float makeDistance(float2 inputDir) {
	// distance function
	return sqrt(inputDir.x * inputDir.x + inputDir.y * inputDir.y);
}

float makeRadialSmooth(float inputDistance, 
	float inputMax, float2 inputUVs) {
	// returns a (used for radial) distance using smoothstep 
	return float(smoothstep(1, inputMax, inputDistance - makeDistance(makeDirection(inputUVs))));
}

float makeNoiseRandomI2O1(float inputSeedX, 
	float inputSeedY) {
	return frac(sin(dot(float2(inputSeedX, inputSeedY), float2(12.9898, 78.233))) * 43758.5453);
}

float makeNoiseRandomI2O1(float2 inputSeed) {
	return frac(sin(dot(inputSeed, float2(12.9898, 78.233))) * 43758.5453);
}

float makeNoiseRandomI1O1(float inputSeed) {
	return frac(sin(inputSeed) * 43758.5453123);
}

float makeNoiseRandomTwoI2O1(float2 inputSeed) { 
	inputSeed = frac(inputSeed * float2(233.34, 851.73));
	inputSeed += dot(inputSeed, inputSeed + 23.56);
	return float(frac(inputSeed.x * inputSeed.y));
}

float2 makeNoiseRandomTwoI2O2(float2 inputSeed) { 
	float n = makeNoiseRandomTwoI2O1(inputSeed);
	return float2(n, makeNoiseRandomTwoI2O1(inputSeed + n));
}

float2 makeGradientDirection(float2 p) {
	// attribution: unity technologies
    p = p % 289;
    float x = (34 * p.x + 1) * p.x % 289 + p.y;
    x = (34 * x + 1) * x % 289;
    x = frac(x / 41) * 2 - 1;
    return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
}

float makeNoisePerlin(float2 inputUVs,
	float inputScale) {
	// attribution: unity technologies
	inputUVs *= inputScale;
    float2 ip = floor(inputUVs);
    float2 fp = frac(inputUVs);
    float d00 = dot(makeGradientDirection(ip), fp);
    float d01 = dot(makeGradientDirection(ip + float2(0, 1)), fp - float2(0, 1));
    float d10 = dot(makeGradientDirection(ip + float2(1, 0)), fp - float2(1, 0));
    float d11 = dot(makeGradientDirection(ip + float2(1, 1)), fp - float2(1, 1));
    fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
    return float(float(lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x)) + 0.5);
}

float makeGlitchChance(float inputValue, 
	float inputBooster, float inputChance, 
	float inputPower) {
	// a little helper thingy i made to use for glitch.. nothing fancy
	return float(saturate(((inputValue * inputBooster) - inputChance) * inputPower));
}

void doNoiseNegateSeeded(inout float inputNumber,
	float inputSeed, float inputChance) {
	// retuns the number possibly negative.. incorporates a seed factor
	// note to self: inputChance should be 0 to 1
	inputChance += 1.0;
	float noiseNegate = makeNoiseRandomI2O1(inputSeed, 13.0) * inputChance;
	if (noiseNegate > 0.5) inputNumber *= -1;
}

void doGlitch(inout float2 inputUVs, 
	float inputPower, float inputAmount, 
	float inputSpeed, float inputSize, 
	float inputSeed) {
	float2 glitchUVs = inputUVs + float2(0.5, 0);
	float blockSpeed = sin(_Time.y / inputSpeed);
	float2 blockUVs = glitchUVs;
	float2 blockPos = makeNoiseRandomI2O1(floor(blockUVs * inputSize) * blockSpeed);
	blockPos = makeNoiseRandomI2O1(floor(blockUVs * inputSize * blockPos) * blockSpeed);
	blockPos = makeNoiseRandomI2O1(floor(blockUVs * inputSize * blockPos) * blockSpeed);
	blockPos = makeNoiseRandomI2O1(floor(blockUVs * inputSize * blockPos) * blockSpeed);
	blockPos = makeNoiseRandomI2O1(floor(blockUVs * inputSize * blockPos) * blockSpeed);
	float blockDisplace = pow(blockPos.x, inputAmount) * pow(blockPos.y, inputAmount);
	float blockPower = blockDisplace.x * makeNoiseRandomI2O1(6 * inputSeed) * 10;
	float toReturn = makeGlitchChance(blockPower * makeNoiseRandomI2O1(blockPower * inputSeed), 1, inputAmount, 1);
	doNoiseNegateSeeded(blockPower, blockPower, inputPower);
	inputUVs += (blockPower * toReturn);
}

float getEdgeGradient(float2 inputUVs,
	float inputTolerance, float inputWidth,
	float2 inputCoordinates, sampler2D inputPass) {
	// grabs a gradient of the desired edge's luma for sobel algorithm..
	float2 egUVs = inputUVs;
	egUVs.x += (inputCoordinates.x * inputWidth);
	egUVs.y += (inputCoordinates.y * inputWidth);
	egUVs /= _ScreenParams.xy;
	float3 egColor = tex2D(inputPass, egUVs).rgb;
	float3 egTolerance = lumaWeighting * inputTolerance;
	float egValue = egTolerance.r * egColor.r + egTolerance.g * egColor.g + egTolerance.b * egColor.b;
	return egValue;
}

float makeSobelEdge(float2 inputUVs,
	float inputTolerance, float inputWidth,
	float2 inputOffset, sampler2D inputPass) {
	// creates an outline using the sobel weights..
	float2 scUVs = float2(inputUVs + inputOffset) * _ScreenParams.xy;
	float scWeights[2];
	// apply kernel in x direction
	scWeights[0] = 0.0;
	scWeights[0] -= getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(-1.0, -1.0), inputPass);
	scWeights[0] -= 2.0 * getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(-1.0, 0.0), inputPass);
	scWeights[0] -= getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(-1.0, 1.0), inputPass);
	scWeights[0] += getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(1.0, -1.0), inputPass);
	scWeights[0] += 2.0 * getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(1.0, 0.0), inputPass);
	scWeights[0] += getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(1.0, 1.0), inputPass);
	// applying kernel in y direction
	scWeights[1] = 0.0;
	scWeights[1] -= getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(-1.0, -1.0), inputPass);
	scWeights[1] -= 2.0 * getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(0.0, -1.0), inputPass);
	scWeights[1] -= getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(1.0, -1.0), inputPass);
	scWeights[1] += getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(-1.0, 1.0), inputPass);
	scWeights[1] += 2.0 * getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(0.0, 1.0), inputPass);
	scWeights[1] += getEdgeGradient(scUVs, inputTolerance, inputWidth, float2(1.0, 1.0), inputPass);
	//applying and returning color
	float sobelWeight = scWeights[0] * scWeights[0] + scWeights[1] * scWeights[1];
	//float3 sobelColor = inputColor.rgb * sobelWeight;
	return sobelWeight;
}

float makeNoiseRandomRangeFloat(float2 inputSeed,
	float inputMinimum, float inputMaximum) {
	float randomNumber = makeNoiseRandomI2O1(inputSeed);
	return float(lerp(inputMinimum, inputMaximum, randomNumber));
}

float2 doScreentearMirrorFancy(float2 inputUVs) {
	// prettiest but most expensive (ugh)
	if (inputUVs.y > 1.0) {
		inputUVs.y = (1.0 - ((inputUVs.y) - 1.0));
	}
	else if (inputUVs.y < 0.0) {
		inputUVs.y *= -1.0;
	}
	if (inputUVs.x > 1.0) {
		inputUVs.x = (1.0 - ((inputUVs.x) - 1.0));
	}
	else if (inputUVs.x < 0.0) {
		inputUVs.x *= -1.0;
	}
	return inputUVs;
}

float2 doScreentearRepeat(float2 inputUVs) {
	// repeat screen, not that pretty but quick
	return frac(inputUVs);
}

float makeSimpleGlitchUVs(float2 inputUVs,
float inputSize, float inputAmount,
float inputSeed, float inputSpeed)
{
	// set up standard time
	float glitchTime = sin(frac(_Time.y) / inputSpeed);
	// make random blocks first, in two layers
	inputUVs.xy += 0.5; // prevent 0 seeding
	inputUVs += inputSeed;
	inputUVs.xy *= float2(1.0, 2.5);
	float2 layers[2] = {
		float2(floor(inputUVs * inputSize)),
		float2(floor((inputUVs + 0.1) * (inputSize * 2.1)))
	};
	// layer one
	float blockLayer = makeNoiseRandomI2O1(layers[0] + glitchTime);
	blockLayer = step(inputAmount, blockLayer);
	// layer two
	float blockLayer2 = makeNoiseRandomI2O1(layers[1] + glitchTime);
	blockLayer += step(inputAmount, blockLayer2);
	// and now a few lines
	float lineLayer = floor((sin(inputUVs.y * 2.5) * 0.25) * inputSize);
	lineLayer = makeNoiseRandomI1O1(lineLayer + glitchTime) * 1.1;
	lineLayer = step(inputAmount, lineLayer);
	// merge
	blockLayer = saturate(blockLayer + lineLayer);
	return blockLayer;
}

float2 makeSpritesheet(float inputColumns,
float inputRows, float inputTotal,
float inputSpeed, int inputCurrent,
float2 inputUVs)
{
	// transforms the current uv into a frame of a spritesheet
	inputCurrent += frac(inputSpeed * _Time.y) * inputTotal;
	inputCurrent = clamp(inputCurrent, 0, inputTotal);
	inputCurrent = fmod(inputCurrent, inputColumns * inputRows);
	float2 inputCurrentCount = float2(1.0, 1.0) / float2(inputColumns, inputRows);
	float inputCurrentY = abs(inputRows - (floor(inputCurrent * inputCurrentCount.x) + 1));
	float inputCurrentX = abs(0 * inputColumns - ((inputCurrent - inputColumns * floor(inputCurrent * inputCurrentCount.x))));
	return float2((inputUVs + float2(inputCurrentX, inputCurrentY)) * inputCurrentCount);
}

//|===============================================|
//|		      fog  + depth functions           	  |
//|===============================================|
#ifdef _KEYWORD_ENABLE_LITE_FOG

	float _LiteFogDensity, _LiteFogDistribution, _LiteFogSafespace,
	_LiteFogSafespaceSize;
	float4 _LiteFogColor;
	sampler2D _CameraDepthTexture;

	float4 getFrustumCorrection()
	{
		// attribution: lukis101
		float x1 = -UNITY_MATRIX_P._31 / (UNITY_MATRIX_P._11 * UNITY_MATRIX_P._34);
		float x2 = -UNITY_MATRIX_P._32 / (UNITY_MATRIX_P._22 * UNITY_MATRIX_P._34);
		return float4(x1, x2, 0, UNITY_MATRIX_P._33 / UNITY_MATRIX_P._34 + x1 * UNITY_MATRIX_P._13 + x2 * UNITY_MATRIX_P._23);
	}

	float ManualLinear01Depth(float z)
	{
		return 1.0 / (_ZBufferParams.z * z + _ZBufferParams.w);
	}

	float GetLinearZFromZDepthWorksWithMirrors(float zDepthFromMap, float2 screenUV)
	{
		// attribution: shadertrixx method
		#if defined(UNITY_REVERSED_Z)
			zDepthFromMap = 1 - zDepthFromMap;
			if (zDepthFromMap >= 1.0) return _ProjectionParams.z;
		#endif // UNITY_REVERSED_Z
		float4 clipPos = float4(screenUV.xy, zDepthFromMap, 1.0);
		clipPos.xyz = 2.0f * clipPos.xyz - 1.0f;
		float4 camPos = mul(unity_CameraInvProjection, clipPos);
		return -camPos.z / camPos.w;
	}

	float getVRCLinearDepth(float4 inputVertexPosition,
	float3 inputScreenPosition, float4 inputWorldDirection,
	float3 inputWorldPosition)
	{
		// attribution: shadertrix method, lukis method, my own frankeinstein
		float3 fullVectorFromEyeToGeometry = inputWorldPosition - _WorldSpaceCameraPos;
		// fix perspective stuffs
		float perspectiveDivide = 1.0f / inputVertexPosition.w;
		float2 screenPos = inputScreenPosition.xy * perspectiveDivide;
		// get working depth (works in unity and vrchat mirrors!)
		float perspectiveFactor = length(fullVectorFromEyeToGeometry * perspectiveDivide);
		float eyeDepthWorld = GetLinearZFromZDepthWorksWithMirrors(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, screenPos), screenPos);
		eyeDepthWorld *= perspectiveFactor;
		// correct by size of vertex
		float3 vertexSize = inputWorldDirection.xyz * perspectiveDivide;
		float vertexSizeWorld = length(vertexSize);
		float correctedDepth = eyeDepthWorld / vertexSizeWorld;
		return correctedDepth;
	}

	float getVRCLinearDepth01(float4 inputVertexPosition,
	float3 inputScreenPosition, float4 inputWorldDirection,
	float3 inputWorldPosition)
	{
		// attribution: shadertrix method, lukis method, my own frankeinstein
		float3 fullVectorFromEyeToGeometry = inputWorldPosition - _WorldSpaceCameraPos;
		// fix perspective stuffs
		float perspectiveDivide = 1.0f / inputVertexPosition.w;
		float2 screenPos = inputScreenPosition.xy * perspectiveDivide;
		// get working depth (works in unity and vrchat mirrors!)
		float perspectiveFactor = length(fullVectorFromEyeToGeometry * perspectiveDivide);
		float eyeDepthWorld = GetLinearZFromZDepthWorksWithMirrors(tex2D(_CameraDepthTexture, screenPos), screenPos);
		eyeDepthWorld *= perspectiveFactor;
		eyeDepthWorld = ManualLinear01Depth(eyeDepthWorld);
		// correct by size of vertex
		float3 vertexSize = inputWorldDirection.xyz * perspectiveDivide;
		float vertexSizeWorld = length(vertexSize);
		float correctedDepth = eyeDepthWorld / vertexSizeWorld;
		return correctedDepth;
	}

	float3 getVRCWorldPositionNoFrac(float4 inputVertexPosition,
	float3 inputScreenPosition, float4 inputWorldDirection,
	float3 inputWorldPosition, float3 inputRaycast, 
	float3 inputGrabPos)
	{
		float perspectiveDivide = 1.0f / inputVertexPosition.w;
		float4 direction = inputWorldDirection * perspectiveDivide;
		float2 screenpos = inputGrabPos.xy * perspectiveDivide;
		float depth = getVRCLinearDepth(inputVertexPosition, inputScreenPosition, inputWorldDirection, inputWorldPosition);
		if (depth >= 1000.f) return float3(1.0, 1.0, 1.0); else if (depth <= -1.0f) return float3(0, 0, 0);
		float3 worldpos = direction * depth + (_WorldSpaceCameraPos.xyz);
		return(worldpos);
	}

	float makeSafeSpace(float inputRadius, float4 inputVertexPosition,
	float3 inputScreenPosition, float4 inputWorldDirection,
	float3 inputWorldPosition, float3 inputRaycast,
	float3 inputGrabPos)
	{
		float3 objectCenter = mul(unity_ObjectToWorld, float4(0.0, 0.0, 0.0, 1.0)).xyz;
		float3 worldPos = getVRCWorldPositionNoFrac(inputVertexPosition, inputScreenPosition, inputWorldDirection, inputWorldPosition, inputRaycast, inputGrabPos);
		float objectDistance = length(objectCenter - worldPos);
		return(saturate(inputRadius - objectDistance));
	}

	void effectFog(inout float4 inputColors,
		float2 inputUVs, float inputDensity,
		float inputDistribution, float4 inputColor,
		float inputSafespace, float inputSafespaceSize,
		float4 inputRawUVs, float4 inputWorldDir,
		float3 inputWorldPos, float4 inputPos,
		float3 inputRaycast) {
			float fogDepth = getVRCLinearDepth01(inputPos, inputRawUVs.xyz, inputWorldDir, inputWorldPos);
			fogDepth *= inputDensity;
			float fogValue = 1.0 - saturate(fogDepth * inputDistribution);
			[branch] if (inputSafespace == 1) {
				float safeSpace = makeSafeSpace(inputSafespaceSize, inputPos, inputRawUVs, inputWorldDir, inputWorldPos, inputRaycast, inputRawUVs);
				fogValue *= saturate(1.0 - safeSpace);
			}
			inputColors.rgb = lerp(inputColors.rgb, inputColor.rgb, fogValue);
		}

#endif // _KEYWORD_ENABLE_LITE_FOG

//|===============================================|
//|		           audiolink                  	  |
//|===============================================|
#ifdef _KEYWORD_ENABLE_LITE_AUDIO_LINK
	// audiolink data
	JUNE_LITE_SAMPLER_DEDICATED(_AudioTexture);
    float _LiteAudioLinkBand, _LiteAudioLinkPower, _LiteAudioLinkMin, _LiteAudioLinkMax;

	// audiolink structure
	struct aldata
	{
		bool textureExists;
		float bass;
		float lowMid;
		float upperMid;
		float treble;
	};

	// functions to pull and map audio data
	float audioRemap(float x, float minO, float maxO, float minN, float maxN)
	{
		return minN + (x - minO) * (maxN - minN) / (maxO - minO);
	}

	float getAudioValue(aldata ald, float strength, int band, float remapMin, float remapMax)
	{
		float4 bands = float4(ald.bass, ald.lowMid, ald.upperMid, ald.treble);
		float remapped = audioRemap(bands[band], 0, 1, remapMin, remapMax);
		return remapped * strength;
	}

	void testLinkPresence(inout aldata ald, inout float versionBand, inout float versionTime)
	{
		float width = 0;
		float height = 0;
		_AudioTexture.GetDimensions(width, height);
		if (width > 64)
		{
			versionBand = 0.0625;
			versionTime = 0.25;
		}
		ald.textureExists = width > 16;
	}

	float sampleAudioTex(float time, float band)
	{
		return JUNE_LITE_SAMPLER_TEXTURE_LOD(_AudioTexture, float2(time, band), 0);
	}

	void initLink(inout aldata ald, float time)
	{
		float versionBand = 1;
		float versionTime = 1;
		testLinkPresence(ald, versionBand, versionTime);
		if (ald.textureExists)
		{
			time *= versionTime;
			ald.bass = sampleAudioTex(time, 0.125 * versionBand);
			ald.lowMid = sampleAudioTex(time, 0.375 * versionBand);
			ald.upperMid = sampleAudioTex(time, 0.625 * versionBand);
			ald.treble = sampleAudioTex(time, 0.875 * versionBand);
		}
	}

	float getAudioLink()
	{
        [branch] if (_LiteAudioLinkBand != 0)
		{
            aldata ald = (aldata)0;
            initLink(ald, 0);
            int band_conv = (int) (_LiteAudioLinkBand - 1); // 0 = disabled, 1 = bass, 2 = lowmid, 3 = uppermid, 4 = treble
            return getAudioValue(ald, _LiteAudioLinkPower, band_conv, _LiteAudioLinkMin, _LiteAudioLinkMax);
		} else {
			return 1;
		}
	}

#endif // _KEYWORD_ENABLE_LITE_AUDIO_LINK

#endif