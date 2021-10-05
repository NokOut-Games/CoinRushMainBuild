//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Skybox/Procedural" {
Properties {
[KeywordEnum(None, Simple, High Quality)] _SunDisk ("Sun", Float) = 2
_SunSize ("Sun Size", Range(0, 1)) = 0.04
_SunSizeConvergence ("Sun Size Convergence", Range(1, 10)) = 5
_AtmosphereThickness ("Atmosphere Thickness", Range(0, 5)) = 1
_SkyTint ("Sky Tint", Color) = (0.5,0.5,0.5,1)
_GroundColor ("Ground", Color) = (0.369,0.349,0.341,1)
_Exposure ("Exposure", Range(0, 8)) = 1.3
}
SubShader {
 Tags { "PreviewType" = "Skybox" "QUEUE" = "Background" "RenderType" = "Background" }
 Pass {
  Tags { "PreviewType" = "Skybox" "QUEUE" = "Background" "RenderType" = "Background" }
  ZWrite Off
  Cull Off
  GpuProgramID 42598
Program "vp" {
SubProgram "gles3 hw_tier00 " {
Local Keywords { "_SUNDISK_NONE" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out mediump float vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
float u_xlat8;
vec3 u_xlat13;
float u_xlat16;
float u_xlat21;
float u_xlat24;
float u_xlat25;
float u_xlat26;
mediump float u_xlat16_27;
float u_xlat28;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat24 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat24 = inversesqrt(u_xlat24);
    u_xlat2.xyz = vec3(u_xlat24) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat24 + u_xlat0.x;
        u_xlat8 = (-u_xlat0.y) * u_xlat24 + 1.0;
        u_xlat16 = u_xlat8 * 5.25 + -6.80000019;
        u_xlat16 = u_xlat8 * u_xlat16 + 3.82999992;
        u_xlat16 = u_xlat8 * u_xlat16 + 0.458999991;
        u_xlat8 = u_xlat8 * u_xlat16 + -0.00286999997;
        u_xlat8 = u_xlat8 * 1.44269502;
        u_xlat0.y = exp2(u_xlat8);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat24 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat24 = sqrt(u_xlat24);
        u_xlat25 = (-u_xlat24) + 1.0;
        u_xlat25 = u_xlat25 * 230.831207;
        u_xlat25 = exp2(u_xlat25);
        u_xlat26 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat26 = u_xlat26 / u_xlat24;
        u_xlat28 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat24 = u_xlat28 / u_xlat24;
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat28 = u_xlat26 * 5.25 + -6.80000019;
        u_xlat28 = u_xlat26 * u_xlat28 + 3.82999992;
        u_xlat28 = u_xlat26 * u_xlat28 + 0.458999991;
        u_xlat26 = u_xlat26 * u_xlat28 + -0.00286999997;
        u_xlat26 = u_xlat26 * 1.44269502;
        u_xlat26 = exp2(u_xlat26);
        u_xlat24 = (-u_xlat24) + 1.0;
        u_xlat28 = u_xlat24 * 5.25 + -6.80000019;
        u_xlat28 = u_xlat24 * u_xlat28 + 3.82999992;
        u_xlat28 = u_xlat24 * u_xlat28 + 0.458999991;
        u_xlat24 = u_xlat24 * u_xlat28 + -0.00286999997;
        u_xlat24 = u_xlat24 * 1.44269502;
        u_xlat24 = exp2(u_xlat24);
        u_xlat24 = u_xlat24 * 0.25;
        u_xlat24 = u_xlat26 * 0.25 + (-u_xlat24);
        u_xlat24 = u_xlat25 * u_xlat24 + u_xlat0.y;
        u_xlat24 = max(u_xlat24, 0.0);
        u_xlat24 = min(u_xlat24, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat24)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat24 = u_xlat0.z * u_xlat25;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat25 = (-u_xlat0.x) + 1.0;
        u_xlat25 = u_xlat25 * 230.831207;
        u_xlat25 = exp2(u_xlat25);
        u_xlat26 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat26 = u_xlat26 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat4.x = u_xlat26 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat26 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat26 * u_xlat4.x + 0.458999991;
        u_xlat26 = u_xlat26 * u_xlat4.x + -0.00286999997;
        u_xlat26 = u_xlat26 * 1.44269502;
        u_xlat26 = exp2(u_xlat26);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat26 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat25 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat25;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat24) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat26 = min(u_xlat2.y, -0.00100000005);
        u_xlat26 = -9.99999975e-05 / u_xlat26;
        u_xlat5.xyz = vec3(u_xlat26) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat21 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat21 = u_xlat5.y * u_xlat21 + 3.82999992;
        u_xlat21 = u_xlat5.y * u_xlat21 + 0.458999991;
        u_xlat13.x = u_xlat5.y * u_xlat21 + -0.00286999997;
        u_xlat13.x = u_xlat13.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat13.x);
        u_xlat21 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat21 = u_xlat5.x * u_xlat21 + 3.82999992;
        u_xlat21 = u_xlat5.x * u_xlat21 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat21 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat13.xz = vec2(u_xlat26) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat13.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat26 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat26 = sqrt(u_xlat26);
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat26 = u_xlat26 * 230.831207;
        u_xlat26 = exp2(u_xlat26);
        u_xlat5.x = u_xlat26 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat26 = u_xlat13.z * u_xlat26;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat26);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat26 = u_xlat2.y * -50.0;
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_27 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_27 = u_xlat16_27 * u_xlat16_27;
    u_xlat16_27 = u_xlat16_27 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_27) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD0 = u_xlat26;
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
in mediump float vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
mediump float u_xlat16_0;
mediump vec3 u_xlat16_1;
void main()
{
    u_xlat16_0 = vs_TEXCOORD0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0 = min(max(u_xlat16_0, 0.0), 1.0);
#else
    u_xlat16_0 = clamp(u_xlat16_0, 0.0, 1.0);
#endif
    u_xlat16_1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    SV_Target0.xyz = vec3(u_xlat16_0) * u_xlat16_1.xyz + vs_TEXCOORD2.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier01 " {
Local Keywords { "_SUNDISK_NONE" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out mediump float vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
float u_xlat8;
vec3 u_xlat13;
float u_xlat16;
float u_xlat21;
float u_xlat24;
float u_xlat25;
float u_xlat26;
mediump float u_xlat16_27;
float u_xlat28;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat24 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat24 = inversesqrt(u_xlat24);
    u_xlat2.xyz = vec3(u_xlat24) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat24 + u_xlat0.x;
        u_xlat8 = (-u_xlat0.y) * u_xlat24 + 1.0;
        u_xlat16 = u_xlat8 * 5.25 + -6.80000019;
        u_xlat16 = u_xlat8 * u_xlat16 + 3.82999992;
        u_xlat16 = u_xlat8 * u_xlat16 + 0.458999991;
        u_xlat8 = u_xlat8 * u_xlat16 + -0.00286999997;
        u_xlat8 = u_xlat8 * 1.44269502;
        u_xlat0.y = exp2(u_xlat8);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat24 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat24 = sqrt(u_xlat24);
        u_xlat25 = (-u_xlat24) + 1.0;
        u_xlat25 = u_xlat25 * 230.831207;
        u_xlat25 = exp2(u_xlat25);
        u_xlat26 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat26 = u_xlat26 / u_xlat24;
        u_xlat28 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat24 = u_xlat28 / u_xlat24;
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat28 = u_xlat26 * 5.25 + -6.80000019;
        u_xlat28 = u_xlat26 * u_xlat28 + 3.82999992;
        u_xlat28 = u_xlat26 * u_xlat28 + 0.458999991;
        u_xlat26 = u_xlat26 * u_xlat28 + -0.00286999997;
        u_xlat26 = u_xlat26 * 1.44269502;
        u_xlat26 = exp2(u_xlat26);
        u_xlat24 = (-u_xlat24) + 1.0;
        u_xlat28 = u_xlat24 * 5.25 + -6.80000019;
        u_xlat28 = u_xlat24 * u_xlat28 + 3.82999992;
        u_xlat28 = u_xlat24 * u_xlat28 + 0.458999991;
        u_xlat24 = u_xlat24 * u_xlat28 + -0.00286999997;
        u_xlat24 = u_xlat24 * 1.44269502;
        u_xlat24 = exp2(u_xlat24);
        u_xlat24 = u_xlat24 * 0.25;
        u_xlat24 = u_xlat26 * 0.25 + (-u_xlat24);
        u_xlat24 = u_xlat25 * u_xlat24 + u_xlat0.y;
        u_xlat24 = max(u_xlat24, 0.0);
        u_xlat24 = min(u_xlat24, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat24)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat24 = u_xlat0.z * u_xlat25;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat25 = (-u_xlat0.x) + 1.0;
        u_xlat25 = u_xlat25 * 230.831207;
        u_xlat25 = exp2(u_xlat25);
        u_xlat26 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat26 = u_xlat26 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat4.x = u_xlat26 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat26 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat26 * u_xlat4.x + 0.458999991;
        u_xlat26 = u_xlat26 * u_xlat4.x + -0.00286999997;
        u_xlat26 = u_xlat26 * 1.44269502;
        u_xlat26 = exp2(u_xlat26);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat26 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat25 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat25;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat24) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat26 = min(u_xlat2.y, -0.00100000005);
        u_xlat26 = -9.99999975e-05 / u_xlat26;
        u_xlat5.xyz = vec3(u_xlat26) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat21 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat21 = u_xlat5.y * u_xlat21 + 3.82999992;
        u_xlat21 = u_xlat5.y * u_xlat21 + 0.458999991;
        u_xlat13.x = u_xlat5.y * u_xlat21 + -0.00286999997;
        u_xlat13.x = u_xlat13.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat13.x);
        u_xlat21 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat21 = u_xlat5.x * u_xlat21 + 3.82999992;
        u_xlat21 = u_xlat5.x * u_xlat21 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat21 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat13.xz = vec2(u_xlat26) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat13.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat26 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat26 = sqrt(u_xlat26);
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat26 = u_xlat26 * 230.831207;
        u_xlat26 = exp2(u_xlat26);
        u_xlat5.x = u_xlat26 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat26 = u_xlat13.z * u_xlat26;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat26);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat26 = u_xlat2.y * -50.0;
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_27 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_27 = u_xlat16_27 * u_xlat16_27;
    u_xlat16_27 = u_xlat16_27 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_27) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD0 = u_xlat26;
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
in mediump float vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
mediump float u_xlat16_0;
mediump vec3 u_xlat16_1;
void main()
{
    u_xlat16_0 = vs_TEXCOORD0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0 = min(max(u_xlat16_0, 0.0), 1.0);
#else
    u_xlat16_0 = clamp(u_xlat16_0, 0.0, 1.0);
#endif
    u_xlat16_1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    SV_Target0.xyz = vec3(u_xlat16_0) * u_xlat16_1.xyz + vs_TEXCOORD2.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier02 " {
Local Keywords { "_SUNDISK_NONE" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out mediump float vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
float u_xlat8;
vec3 u_xlat13;
float u_xlat16;
float u_xlat21;
float u_xlat24;
float u_xlat25;
float u_xlat26;
mediump float u_xlat16_27;
float u_xlat28;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat24 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat24 = inversesqrt(u_xlat24);
    u_xlat2.xyz = vec3(u_xlat24) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat24 + u_xlat0.x;
        u_xlat8 = (-u_xlat0.y) * u_xlat24 + 1.0;
        u_xlat16 = u_xlat8 * 5.25 + -6.80000019;
        u_xlat16 = u_xlat8 * u_xlat16 + 3.82999992;
        u_xlat16 = u_xlat8 * u_xlat16 + 0.458999991;
        u_xlat8 = u_xlat8 * u_xlat16 + -0.00286999997;
        u_xlat8 = u_xlat8 * 1.44269502;
        u_xlat0.y = exp2(u_xlat8);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat24 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat24 = sqrt(u_xlat24);
        u_xlat25 = (-u_xlat24) + 1.0;
        u_xlat25 = u_xlat25 * 230.831207;
        u_xlat25 = exp2(u_xlat25);
        u_xlat26 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat26 = u_xlat26 / u_xlat24;
        u_xlat28 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat24 = u_xlat28 / u_xlat24;
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat28 = u_xlat26 * 5.25 + -6.80000019;
        u_xlat28 = u_xlat26 * u_xlat28 + 3.82999992;
        u_xlat28 = u_xlat26 * u_xlat28 + 0.458999991;
        u_xlat26 = u_xlat26 * u_xlat28 + -0.00286999997;
        u_xlat26 = u_xlat26 * 1.44269502;
        u_xlat26 = exp2(u_xlat26);
        u_xlat24 = (-u_xlat24) + 1.0;
        u_xlat28 = u_xlat24 * 5.25 + -6.80000019;
        u_xlat28 = u_xlat24 * u_xlat28 + 3.82999992;
        u_xlat28 = u_xlat24 * u_xlat28 + 0.458999991;
        u_xlat24 = u_xlat24 * u_xlat28 + -0.00286999997;
        u_xlat24 = u_xlat24 * 1.44269502;
        u_xlat24 = exp2(u_xlat24);
        u_xlat24 = u_xlat24 * 0.25;
        u_xlat24 = u_xlat26 * 0.25 + (-u_xlat24);
        u_xlat24 = u_xlat25 * u_xlat24 + u_xlat0.y;
        u_xlat24 = max(u_xlat24, 0.0);
        u_xlat24 = min(u_xlat24, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat24)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat24 = u_xlat0.z * u_xlat25;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat25 = (-u_xlat0.x) + 1.0;
        u_xlat25 = u_xlat25 * 230.831207;
        u_xlat25 = exp2(u_xlat25);
        u_xlat26 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat26 = u_xlat26 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat4.x = u_xlat26 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat26 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat26 * u_xlat4.x + 0.458999991;
        u_xlat26 = u_xlat26 * u_xlat4.x + -0.00286999997;
        u_xlat26 = u_xlat26 * 1.44269502;
        u_xlat26 = exp2(u_xlat26);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat26 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat25 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat25;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat24) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat26 = min(u_xlat2.y, -0.00100000005);
        u_xlat26 = -9.99999975e-05 / u_xlat26;
        u_xlat5.xyz = vec3(u_xlat26) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat21 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat21 = u_xlat5.y * u_xlat21 + 3.82999992;
        u_xlat21 = u_xlat5.y * u_xlat21 + 0.458999991;
        u_xlat13.x = u_xlat5.y * u_xlat21 + -0.00286999997;
        u_xlat13.x = u_xlat13.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat13.x);
        u_xlat21 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat21 = u_xlat5.x * u_xlat21 + 3.82999992;
        u_xlat21 = u_xlat5.x * u_xlat21 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat21 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat13.xz = vec2(u_xlat26) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat13.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat26 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat26 = sqrt(u_xlat26);
        u_xlat26 = (-u_xlat26) + 1.0;
        u_xlat26 = u_xlat26 * 230.831207;
        u_xlat26 = exp2(u_xlat26);
        u_xlat5.x = u_xlat26 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat26 = u_xlat13.z * u_xlat26;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat26);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat26 = u_xlat2.y * -50.0;
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_27 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_27 = u_xlat16_27 * u_xlat16_27;
    u_xlat16_27 = u_xlat16_27 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_27) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD0 = u_xlat26;
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
in mediump float vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
layout(location = 0) out mediump vec4 SV_Target0;
mediump float u_xlat16_0;
mediump vec3 u_xlat16_1;
void main()
{
    u_xlat16_0 = vs_TEXCOORD0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0 = min(max(u_xlat16_0, 0.0), 1.0);
#else
    u_xlat16_0 = clamp(u_xlat16_0, 0.0, 1.0);
#endif
    u_xlat16_1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    SV_Target0.xyz = vec3(u_xlat16_0) * u_xlat16_1.xyz + vs_TEXCOORD2.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "vulkan hw_tier00 " {
Local Keywords { "_SUNDISK_NONE" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 830
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %816 %819 %823 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %768 RelaxedPrecision 
                                                      OpDecorate %770 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %782 RelaxedPrecision 
                                                      OpDecorate %783 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %788 RelaxedPrecision 
                                                      OpDecorate %789 RelaxedPrecision 
                                                      OpDecorate %790 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %799 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %808 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %812 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 816 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 819 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 823 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 5 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 6 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         f32 %762 = OpConstant 3.674022E-40 
                                         i32 %764 = OpConstant 4 
                                Private f32* %790 = OpVariable Private 
                                         f32 %802 = OpConstant 3.674022E-40 
                              Private f32_3* %805 = OpVariable Private 
                                             %815 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                                             %822 = OpTypePointer Output %6 
                         Output f32* vs_TEXCOORD0 = OpVariable Output 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Private f32* %760 = OpAccessChain %50 %70 
                                         f32 %761 = OpLoad %760 
                                         f32 %763 = OpFMul %761 %762 
                                                      OpStore %268 %763 
                                Uniform f32* %765 = OpAccessChain %21 %764 %133 
                                         f32 %766 = OpLoad %765 
                                Uniform f32* %767 = OpAccessChain %21 %764 %70 
                                         f32 %768 = OpLoad %767 
                                Uniform f32* %769 = OpAccessChain %21 %764 %371 
                                         f32 %770 = OpLoad %769 
                                       f32_3 %771 = OpCompositeConstruct %766 %768 %770 
                                Uniform f32* %772 = OpAccessChain %21 %764 %133 
                                         f32 %773 = OpLoad %772 
                                Uniform f32* %774 = OpAccessChain %21 %764 %70 
                                         f32 %775 = OpLoad %774 
                                Uniform f32* %776 = OpAccessChain %21 %764 %371 
                                         f32 %777 = OpLoad %776 
                                       f32_3 %778 = OpCompositeConstruct %773 %775 %777 
                                       f32_3 %779 = OpFMul %771 %778 
                                                      OpStore %127 %779 
                                       f32_3 %780 = OpLoad %127 
                                       f32_3 %781 = OpLoad %561 
                                       f32_3 %782 = OpFMul %780 %781 
                                       f32_3 %783 = OpLoad %559 
                                       f32_3 %784 = OpFAdd %782 %783 
                                                      OpStore %127 %784 
                                       f32_3 %785 = OpLoad %127 
                                Uniform f32* %786 = OpAccessChain %21 %46 
                                         f32 %787 = OpLoad %786 
                                       f32_3 %788 = OpCompositeConstruct %787 %787 %787 
                                       f32_3 %789 = OpFMul %785 %788 
                                                      OpStore %127 %789 
                              Uniform f32_4* %791 = OpAccessChain %21 %28 
                                       f32_4 %792 = OpLoad %791 
                                       f32_3 %793 = OpVectorShuffle %792 %792 0 1 2 
                                       f32_4 %794 = OpLoad %50 
                                       f32_3 %795 = OpVectorShuffle %794 %794 0 1 2 
                                       f32_3 %796 = OpFNegate %795 
                                         f32 %797 = OpDot %793 %796 
                                                      OpStore %790 %797 
                                         f32 %798 = OpLoad %790 
                                         f32 %799 = OpLoad %790 
                                         f32 %800 = OpFMul %798 %799 
                                                      OpStore %790 %800 
                                         f32 %801 = OpLoad %790 
                                         f32 %803 = OpFMul %801 %802 
                                         f32 %804 = OpFAdd %803 %802 
                                                      OpStore %790 %804 
                                         f32 %806 = OpLoad %790 
                                       f32_3 %807 = OpCompositeConstruct %806 %806 %806 
                                       f32_3 %808 = OpLoad %559 
                                       f32_3 %809 = OpFMul %807 %808 
                                                      OpStore %805 %809 
                                       f32_3 %810 = OpLoad %805 
                                Uniform f32* %811 = OpAccessChain %21 %46 
                                         f32 %812 = OpLoad %811 
                                       f32_3 %813 = OpCompositeConstruct %812 %812 %812 
                                       f32_3 %814 = OpFMul %810 %813 
                                                      OpStore %805 %814 
                                       f32_3 %817 = OpLoad %127 
                                       f32_3 %818 = OpExtInst %1 31 %817 
                                                      OpStore vs_TEXCOORD1 %818 
                                       f32_3 %820 = OpLoad %805 
                                       f32_3 %821 = OpExtInst %1 31 %820 
                                                      OpStore vs_TEXCOORD2 %821 
                                         f32 %824 = OpLoad %268 
                                                      OpStore vs_TEXCOORD0 %824 
                                 Output f32* %825 = OpAccessChain %74 %28 %70 
                                         f32 %826 = OpLoad %825 
                                         f32 %827 = OpFNegate %826 
                                 Output f32* %828 = OpAccessChain %74 %28 %70 
                                                      OpStore %828 %827 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 42
; Schema: 0
                              OpCapability Shader 
                       %1 = OpExtInstImport "GLSL.std.450" 
                              OpMemoryModel Logical GLSL450 
                              OpEntryPoint Fragment %4 "main" %10 %20 %22 %28 
                              OpExecutionMode %4 OriginUpperLeft 
                              OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                              OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                              OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                              OpDecorate %8 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD0 Location 10 
                              OpDecorate %11 RelaxedPrecision 
                              OpDecorate %12 RelaxedPrecision 
                              OpDecorate %15 RelaxedPrecision 
                              OpDecorate %18 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD1 Location 20 
                              OpDecorate %21 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD2 Location 22 
                              OpDecorate %23 RelaxedPrecision 
                              OpDecorate %24 RelaxedPrecision 
                              OpDecorate %25 RelaxedPrecision 
                              OpDecorate %28 RelaxedPrecision 
                              OpDecorate %28 Location 28 
                              OpDecorate %29 RelaxedPrecision 
                              OpDecorate %30 RelaxedPrecision 
                              OpDecorate %31 RelaxedPrecision 
                              OpDecorate %32 RelaxedPrecision 
                              OpDecorate %33 RelaxedPrecision 
                              OpDecorate %34 RelaxedPrecision 
                       %2 = OpTypeVoid 
                       %3 = OpTypeFunction %2 
                       %6 = OpTypeFloat 32 
                       %7 = OpTypePointer Private %6 
          Private f32* %8 = OpVariable Private 
                       %9 = OpTypePointer Input %6 
  Input f32* vs_TEXCOORD0 = OpVariable Input 
                  f32 %13 = OpConstant 3.674022E-40 
                  f32 %14 = OpConstant 3.674022E-40 
                      %16 = OpTypeVector %6 3 
                      %17 = OpTypePointer Private %16 
       Private f32_3* %18 = OpVariable Private 
                      %19 = OpTypePointer Input %16 
Input f32_3* vs_TEXCOORD1 = OpVariable Input 
Input f32_3* vs_TEXCOORD2 = OpVariable Input 
                      %26 = OpTypeVector %6 4 
                      %27 = OpTypePointer Output %26 
        Output f32_4* %28 = OpVariable Output 
                      %37 = OpTypeInt 32 0 
                  u32 %38 = OpConstant 3 
                      %39 = OpTypePointer Output %6 
                  void %4 = OpFunction None %3 
                       %5 = OpLabel 
                  f32 %11 = OpLoad vs_TEXCOORD0 
                              OpStore %8 %11 
                  f32 %12 = OpLoad %8 
                  f32 %15 = OpExtInst %1 43 %12 %13 %14 
                              OpStore %8 %15 
                f32_3 %21 = OpLoad vs_TEXCOORD1 
                f32_3 %23 = OpLoad vs_TEXCOORD2 
                f32_3 %24 = OpFNegate %23 
                f32_3 %25 = OpFAdd %21 %24 
                              OpStore %18 %25 
                  f32 %29 = OpLoad %8 
                f32_3 %30 = OpCompositeConstruct %29 %29 %29 
                f32_3 %31 = OpLoad %18 
                f32_3 %32 = OpFMul %30 %31 
                f32_3 %33 = OpLoad vs_TEXCOORD2 
                f32_3 %34 = OpFAdd %32 %33 
                f32_4 %35 = OpLoad %28 
                f32_4 %36 = OpVectorShuffle %35 %34 4 5 6 3 
                              OpStore %28 %36 
          Output f32* %40 = OpAccessChain %28 %38 
                              OpStore %40 %14 
                              OpReturn
                              OpFunctionEnd
"
}
SubProgram "vulkan hw_tier01 " {
Local Keywords { "_SUNDISK_NONE" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 830
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %816 %819 %823 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %768 RelaxedPrecision 
                                                      OpDecorate %770 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %782 RelaxedPrecision 
                                                      OpDecorate %783 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %788 RelaxedPrecision 
                                                      OpDecorate %789 RelaxedPrecision 
                                                      OpDecorate %790 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %799 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %808 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %812 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 816 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 819 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 823 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 5 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 6 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         f32 %762 = OpConstant 3.674022E-40 
                                         i32 %764 = OpConstant 4 
                                Private f32* %790 = OpVariable Private 
                                         f32 %802 = OpConstant 3.674022E-40 
                              Private f32_3* %805 = OpVariable Private 
                                             %815 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                                             %822 = OpTypePointer Output %6 
                         Output f32* vs_TEXCOORD0 = OpVariable Output 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Private f32* %760 = OpAccessChain %50 %70 
                                         f32 %761 = OpLoad %760 
                                         f32 %763 = OpFMul %761 %762 
                                                      OpStore %268 %763 
                                Uniform f32* %765 = OpAccessChain %21 %764 %133 
                                         f32 %766 = OpLoad %765 
                                Uniform f32* %767 = OpAccessChain %21 %764 %70 
                                         f32 %768 = OpLoad %767 
                                Uniform f32* %769 = OpAccessChain %21 %764 %371 
                                         f32 %770 = OpLoad %769 
                                       f32_3 %771 = OpCompositeConstruct %766 %768 %770 
                                Uniform f32* %772 = OpAccessChain %21 %764 %133 
                                         f32 %773 = OpLoad %772 
                                Uniform f32* %774 = OpAccessChain %21 %764 %70 
                                         f32 %775 = OpLoad %774 
                                Uniform f32* %776 = OpAccessChain %21 %764 %371 
                                         f32 %777 = OpLoad %776 
                                       f32_3 %778 = OpCompositeConstruct %773 %775 %777 
                                       f32_3 %779 = OpFMul %771 %778 
                                                      OpStore %127 %779 
                                       f32_3 %780 = OpLoad %127 
                                       f32_3 %781 = OpLoad %561 
                                       f32_3 %782 = OpFMul %780 %781 
                                       f32_3 %783 = OpLoad %559 
                                       f32_3 %784 = OpFAdd %782 %783 
                                                      OpStore %127 %784 
                                       f32_3 %785 = OpLoad %127 
                                Uniform f32* %786 = OpAccessChain %21 %46 
                                         f32 %787 = OpLoad %786 
                                       f32_3 %788 = OpCompositeConstruct %787 %787 %787 
                                       f32_3 %789 = OpFMul %785 %788 
                                                      OpStore %127 %789 
                              Uniform f32_4* %791 = OpAccessChain %21 %28 
                                       f32_4 %792 = OpLoad %791 
                                       f32_3 %793 = OpVectorShuffle %792 %792 0 1 2 
                                       f32_4 %794 = OpLoad %50 
                                       f32_3 %795 = OpVectorShuffle %794 %794 0 1 2 
                                       f32_3 %796 = OpFNegate %795 
                                         f32 %797 = OpDot %793 %796 
                                                      OpStore %790 %797 
                                         f32 %798 = OpLoad %790 
                                         f32 %799 = OpLoad %790 
                                         f32 %800 = OpFMul %798 %799 
                                                      OpStore %790 %800 
                                         f32 %801 = OpLoad %790 
                                         f32 %803 = OpFMul %801 %802 
                                         f32 %804 = OpFAdd %803 %802 
                                                      OpStore %790 %804 
                                         f32 %806 = OpLoad %790 
                                       f32_3 %807 = OpCompositeConstruct %806 %806 %806 
                                       f32_3 %808 = OpLoad %559 
                                       f32_3 %809 = OpFMul %807 %808 
                                                      OpStore %805 %809 
                                       f32_3 %810 = OpLoad %805 
                                Uniform f32* %811 = OpAccessChain %21 %46 
                                         f32 %812 = OpLoad %811 
                                       f32_3 %813 = OpCompositeConstruct %812 %812 %812 
                                       f32_3 %814 = OpFMul %810 %813 
                                                      OpStore %805 %814 
                                       f32_3 %817 = OpLoad %127 
                                       f32_3 %818 = OpExtInst %1 31 %817 
                                                      OpStore vs_TEXCOORD1 %818 
                                       f32_3 %820 = OpLoad %805 
                                       f32_3 %821 = OpExtInst %1 31 %820 
                                                      OpStore vs_TEXCOORD2 %821 
                                         f32 %824 = OpLoad %268 
                                                      OpStore vs_TEXCOORD0 %824 
                                 Output f32* %825 = OpAccessChain %74 %28 %70 
                                         f32 %826 = OpLoad %825 
                                         f32 %827 = OpFNegate %826 
                                 Output f32* %828 = OpAccessChain %74 %28 %70 
                                                      OpStore %828 %827 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 42
; Schema: 0
                              OpCapability Shader 
                       %1 = OpExtInstImport "GLSL.std.450" 
                              OpMemoryModel Logical GLSL450 
                              OpEntryPoint Fragment %4 "main" %10 %20 %22 %28 
                              OpExecutionMode %4 OriginUpperLeft 
                              OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                              OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                              OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                              OpDecorate %8 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD0 Location 10 
                              OpDecorate %11 RelaxedPrecision 
                              OpDecorate %12 RelaxedPrecision 
                              OpDecorate %15 RelaxedPrecision 
                              OpDecorate %18 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD1 Location 20 
                              OpDecorate %21 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD2 Location 22 
                              OpDecorate %23 RelaxedPrecision 
                              OpDecorate %24 RelaxedPrecision 
                              OpDecorate %25 RelaxedPrecision 
                              OpDecorate %28 RelaxedPrecision 
                              OpDecorate %28 Location 28 
                              OpDecorate %29 RelaxedPrecision 
                              OpDecorate %30 RelaxedPrecision 
                              OpDecorate %31 RelaxedPrecision 
                              OpDecorate %32 RelaxedPrecision 
                              OpDecorate %33 RelaxedPrecision 
                              OpDecorate %34 RelaxedPrecision 
                       %2 = OpTypeVoid 
                       %3 = OpTypeFunction %2 
                       %6 = OpTypeFloat 32 
                       %7 = OpTypePointer Private %6 
          Private f32* %8 = OpVariable Private 
                       %9 = OpTypePointer Input %6 
  Input f32* vs_TEXCOORD0 = OpVariable Input 
                  f32 %13 = OpConstant 3.674022E-40 
                  f32 %14 = OpConstant 3.674022E-40 
                      %16 = OpTypeVector %6 3 
                      %17 = OpTypePointer Private %16 
       Private f32_3* %18 = OpVariable Private 
                      %19 = OpTypePointer Input %16 
Input f32_3* vs_TEXCOORD1 = OpVariable Input 
Input f32_3* vs_TEXCOORD2 = OpVariable Input 
                      %26 = OpTypeVector %6 4 
                      %27 = OpTypePointer Output %26 
        Output f32_4* %28 = OpVariable Output 
                      %37 = OpTypeInt 32 0 
                  u32 %38 = OpConstant 3 
                      %39 = OpTypePointer Output %6 
                  void %4 = OpFunction None %3 
                       %5 = OpLabel 
                  f32 %11 = OpLoad vs_TEXCOORD0 
                              OpStore %8 %11 
                  f32 %12 = OpLoad %8 
                  f32 %15 = OpExtInst %1 43 %12 %13 %14 
                              OpStore %8 %15 
                f32_3 %21 = OpLoad vs_TEXCOORD1 
                f32_3 %23 = OpLoad vs_TEXCOORD2 
                f32_3 %24 = OpFNegate %23 
                f32_3 %25 = OpFAdd %21 %24 
                              OpStore %18 %25 
                  f32 %29 = OpLoad %8 
                f32_3 %30 = OpCompositeConstruct %29 %29 %29 
                f32_3 %31 = OpLoad %18 
                f32_3 %32 = OpFMul %30 %31 
                f32_3 %33 = OpLoad vs_TEXCOORD2 
                f32_3 %34 = OpFAdd %32 %33 
                f32_4 %35 = OpLoad %28 
                f32_4 %36 = OpVectorShuffle %35 %34 4 5 6 3 
                              OpStore %28 %36 
          Output f32* %40 = OpAccessChain %28 %38 
                              OpStore %40 %14 
                              OpReturn
                              OpFunctionEnd
"
}
SubProgram "vulkan hw_tier02 " {
Local Keywords { "_SUNDISK_NONE" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 830
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %816 %819 %823 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %768 RelaxedPrecision 
                                                      OpDecorate %770 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %782 RelaxedPrecision 
                                                      OpDecorate %783 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %788 RelaxedPrecision 
                                                      OpDecorate %789 RelaxedPrecision 
                                                      OpDecorate %790 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %799 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %808 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %812 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 816 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 819 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 823 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 5 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 6 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         f32 %762 = OpConstant 3.674022E-40 
                                         i32 %764 = OpConstant 4 
                                Private f32* %790 = OpVariable Private 
                                         f32 %802 = OpConstant 3.674022E-40 
                              Private f32_3* %805 = OpVariable Private 
                                             %815 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                                             %822 = OpTypePointer Output %6 
                         Output f32* vs_TEXCOORD0 = OpVariable Output 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Private f32* %760 = OpAccessChain %50 %70 
                                         f32 %761 = OpLoad %760 
                                         f32 %763 = OpFMul %761 %762 
                                                      OpStore %268 %763 
                                Uniform f32* %765 = OpAccessChain %21 %764 %133 
                                         f32 %766 = OpLoad %765 
                                Uniform f32* %767 = OpAccessChain %21 %764 %70 
                                         f32 %768 = OpLoad %767 
                                Uniform f32* %769 = OpAccessChain %21 %764 %371 
                                         f32 %770 = OpLoad %769 
                                       f32_3 %771 = OpCompositeConstruct %766 %768 %770 
                                Uniform f32* %772 = OpAccessChain %21 %764 %133 
                                         f32 %773 = OpLoad %772 
                                Uniform f32* %774 = OpAccessChain %21 %764 %70 
                                         f32 %775 = OpLoad %774 
                                Uniform f32* %776 = OpAccessChain %21 %764 %371 
                                         f32 %777 = OpLoad %776 
                                       f32_3 %778 = OpCompositeConstruct %773 %775 %777 
                                       f32_3 %779 = OpFMul %771 %778 
                                                      OpStore %127 %779 
                                       f32_3 %780 = OpLoad %127 
                                       f32_3 %781 = OpLoad %561 
                                       f32_3 %782 = OpFMul %780 %781 
                                       f32_3 %783 = OpLoad %559 
                                       f32_3 %784 = OpFAdd %782 %783 
                                                      OpStore %127 %784 
                                       f32_3 %785 = OpLoad %127 
                                Uniform f32* %786 = OpAccessChain %21 %46 
                                         f32 %787 = OpLoad %786 
                                       f32_3 %788 = OpCompositeConstruct %787 %787 %787 
                                       f32_3 %789 = OpFMul %785 %788 
                                                      OpStore %127 %789 
                              Uniform f32_4* %791 = OpAccessChain %21 %28 
                                       f32_4 %792 = OpLoad %791 
                                       f32_3 %793 = OpVectorShuffle %792 %792 0 1 2 
                                       f32_4 %794 = OpLoad %50 
                                       f32_3 %795 = OpVectorShuffle %794 %794 0 1 2 
                                       f32_3 %796 = OpFNegate %795 
                                         f32 %797 = OpDot %793 %796 
                                                      OpStore %790 %797 
                                         f32 %798 = OpLoad %790 
                                         f32 %799 = OpLoad %790 
                                         f32 %800 = OpFMul %798 %799 
                                                      OpStore %790 %800 
                                         f32 %801 = OpLoad %790 
                                         f32 %803 = OpFMul %801 %802 
                                         f32 %804 = OpFAdd %803 %802 
                                                      OpStore %790 %804 
                                         f32 %806 = OpLoad %790 
                                       f32_3 %807 = OpCompositeConstruct %806 %806 %806 
                                       f32_3 %808 = OpLoad %559 
                                       f32_3 %809 = OpFMul %807 %808 
                                                      OpStore %805 %809 
                                       f32_3 %810 = OpLoad %805 
                                Uniform f32* %811 = OpAccessChain %21 %46 
                                         f32 %812 = OpLoad %811 
                                       f32_3 %813 = OpCompositeConstruct %812 %812 %812 
                                       f32_3 %814 = OpFMul %810 %813 
                                                      OpStore %805 %814 
                                       f32_3 %817 = OpLoad %127 
                                       f32_3 %818 = OpExtInst %1 31 %817 
                                                      OpStore vs_TEXCOORD1 %818 
                                       f32_3 %820 = OpLoad %805 
                                       f32_3 %821 = OpExtInst %1 31 %820 
                                                      OpStore vs_TEXCOORD2 %821 
                                         f32 %824 = OpLoad %268 
                                                      OpStore vs_TEXCOORD0 %824 
                                 Output f32* %825 = OpAccessChain %74 %28 %70 
                                         f32 %826 = OpLoad %825 
                                         f32 %827 = OpFNegate %826 
                                 Output f32* %828 = OpAccessChain %74 %28 %70 
                                                      OpStore %828 %827 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 42
; Schema: 0
                              OpCapability Shader 
                       %1 = OpExtInstImport "GLSL.std.450" 
                              OpMemoryModel Logical GLSL450 
                              OpEntryPoint Fragment %4 "main" %10 %20 %22 %28 
                              OpExecutionMode %4 OriginUpperLeft 
                              OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                              OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                              OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                              OpDecorate %8 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD0 Location 10 
                              OpDecorate %11 RelaxedPrecision 
                              OpDecorate %12 RelaxedPrecision 
                              OpDecorate %15 RelaxedPrecision 
                              OpDecorate %18 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD1 Location 20 
                              OpDecorate %21 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                              OpDecorate vs_TEXCOORD2 Location 22 
                              OpDecorate %23 RelaxedPrecision 
                              OpDecorate %24 RelaxedPrecision 
                              OpDecorate %25 RelaxedPrecision 
                              OpDecorate %28 RelaxedPrecision 
                              OpDecorate %28 Location 28 
                              OpDecorate %29 RelaxedPrecision 
                              OpDecorate %30 RelaxedPrecision 
                              OpDecorate %31 RelaxedPrecision 
                              OpDecorate %32 RelaxedPrecision 
                              OpDecorate %33 RelaxedPrecision 
                              OpDecorate %34 RelaxedPrecision 
                       %2 = OpTypeVoid 
                       %3 = OpTypeFunction %2 
                       %6 = OpTypeFloat 32 
                       %7 = OpTypePointer Private %6 
          Private f32* %8 = OpVariable Private 
                       %9 = OpTypePointer Input %6 
  Input f32* vs_TEXCOORD0 = OpVariable Input 
                  f32 %13 = OpConstant 3.674022E-40 
                  f32 %14 = OpConstant 3.674022E-40 
                      %16 = OpTypeVector %6 3 
                      %17 = OpTypePointer Private %16 
       Private f32_3* %18 = OpVariable Private 
                      %19 = OpTypePointer Input %16 
Input f32_3* vs_TEXCOORD1 = OpVariable Input 
Input f32_3* vs_TEXCOORD2 = OpVariable Input 
                      %26 = OpTypeVector %6 4 
                      %27 = OpTypePointer Output %26 
        Output f32_4* %28 = OpVariable Output 
                      %37 = OpTypeInt 32 0 
                  u32 %38 = OpConstant 3 
                      %39 = OpTypePointer Output %6 
                  void %4 = OpFunction None %3 
                       %5 = OpLabel 
                  f32 %11 = OpLoad vs_TEXCOORD0 
                              OpStore %8 %11 
                  f32 %12 = OpLoad %8 
                  f32 %15 = OpExtInst %1 43 %12 %13 %14 
                              OpStore %8 %15 
                f32_3 %21 = OpLoad vs_TEXCOORD1 
                f32_3 %23 = OpLoad vs_TEXCOORD2 
                f32_3 %24 = OpFNegate %23 
                f32_3 %25 = OpFAdd %21 %24 
                              OpStore %18 %25 
                  f32 %29 = OpLoad %8 
                f32_3 %30 = OpCompositeConstruct %29 %29 %29 
                f32_3 %31 = OpLoad %18 
                f32_3 %32 = OpFMul %30 %31 
                f32_3 %33 = OpLoad vs_TEXCOORD2 
                f32_3 %34 = OpFAdd %32 %33 
                f32_4 %35 = OpLoad %28 
                f32_4 %36 = OpVectorShuffle %35 %34 4 5 6 3 
                              OpStore %28 %36 
          Output f32* %40 = OpAccessChain %28 %38 
                              OpStore %40 %14 
                              OpReturn
                              OpFunctionEnd
"
}
SubProgram "gles3 hw_tier00 " {
Local Keywords { "_SUNDISK_SIMPLE" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump vec4 _LightColor0;
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out mediump vec3 vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
out mediump vec3 vs_TEXCOORD3;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
mediump vec3 u_xlat16_8;
float u_xlat9;
vec3 u_xlat14;
float u_xlat18;
float u_xlat23;
float u_xlat27;
float u_xlat28;
float u_xlat29;
mediump float u_xlat16_30;
float u_xlat31;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat27 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat27 = inversesqrt(u_xlat27);
    u_xlat2.xyz = vec3(u_xlat27) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat27 + u_xlat0.x;
        u_xlat9 = (-u_xlat0.y) * u_xlat27 + 1.0;
        u_xlat18 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat18 = u_xlat9 * u_xlat18 + 3.82999992;
        u_xlat18 = u_xlat9 * u_xlat18 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat18 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat0.y = exp2(u_xlat9);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat27 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat27 = sqrt(u_xlat27);
        u_xlat28 = (-u_xlat27) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat27;
        u_xlat31 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat27 = u_xlat31 / u_xlat27;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat31 = u_xlat29 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat29 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat29 * u_xlat31 + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat31 + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat27 = (-u_xlat27) + 1.0;
        u_xlat31 = u_xlat27 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat27 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat27 * u_xlat31 + 0.458999991;
        u_xlat27 = u_xlat27 * u_xlat31 + -0.00286999997;
        u_xlat27 = u_xlat27 * 1.44269502;
        u_xlat27 = exp2(u_xlat27);
        u_xlat27 = u_xlat27 * 0.25;
        u_xlat27 = u_xlat29 * 0.25 + (-u_xlat27);
        u_xlat27 = u_xlat28 * u_xlat27 + u_xlat0.y;
        u_xlat27 = max(u_xlat27, 0.0);
        u_xlat27 = min(u_xlat27, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat27)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat27 = u_xlat0.z * u_xlat28;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat28 = (-u_xlat0.x) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat4.x = u_xlat29 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat4.x + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat29 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat28 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat28;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat27) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat29 = min(u_xlat2.y, -0.00100000005);
        u_xlat29 = -9.99999975e-05 / u_xlat29;
        u_xlat5.xyz = vec3(u_xlat29) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat23 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.y * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.y * u_xlat23 + 0.458999991;
        u_xlat14.x = u_xlat5.y * u_xlat23 + -0.00286999997;
        u_xlat14.x = u_xlat14.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat14.x);
        u_xlat23 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.x * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.x * u_xlat23 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat23 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat14.xz = vec2(u_xlat29) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat14.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat29 = sqrt(u_xlat29);
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat29 = u_xlat29 * 230.831207;
        u_xlat29 = exp2(u_xlat29);
        u_xlat5.x = u_xlat29 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat29 = u_xlat14.z * u_xlat29;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat29);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_30 = u_xlat16_30 * u_xlat16_30;
    u_xlat16_30 = u_xlat16_30 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_30) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_LightColor0.xyz, _LightColor0.xyz);
    u_xlat16_30 = sqrt(u_xlat16_30);
    u_xlat16_30 = max(u_xlat16_30, 0.25);
    u_xlat16_30 = min(u_xlat16_30, 1.0);
    u_xlat16_8.xyz = u_xlat16_0.xyz * vec3(8000.0, 8000.0, 8000.0);
#ifdef UNITY_ADRENO_ES3
    u_xlat16_8.xyz = min(max(u_xlat16_8.xyz, 0.0), 1.0);
#else
    u_xlat16_8.xyz = clamp(u_xlat16_8.xyz, 0.0, 1.0);
#endif
    u_xlat16_8.xyz = u_xlat16_8.xyz * _LightColor0.xyz;
    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(27.0, 27.0, 27.0);
    u_xlat16_8.xyz = u_xlat16_8.xyz / vec3(u_xlat16_30);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD3.xyz = sqrt(u_xlat16_8.xyz);
    vs_TEXCOORD0.xyz = (-u_xlat2.xyz);
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	mediump float _SunSize;
in mediump vec3 vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
in mediump vec3 vs_TEXCOORD3;
layout(location = 0) out mediump vec4 SV_Target0;
mediump vec3 u_xlat16_0;
mediump vec3 u_xlat16_1;
bool u_xlatb2;
mediump vec3 u_xlat16_3;
void main()
{
    u_xlat16_0.xyz = vs_TEXCOORD0.xyz + _WorldSpaceLightPos0.xyz;
    u_xlat16_0.x = dot(u_xlat16_0.xyz, u_xlat16_0.xyz);
    u_xlat16_0.x = sqrt(u_xlat16_0.x);
    u_xlat16_3.x = float(1.0) / _SunSize;
    u_xlat16_0.x = u_xlat16_3.x * u_xlat16_0.x;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0.x = min(max(u_xlat16_0.x, 0.0), 1.0);
#else
    u_xlat16_0.x = clamp(u_xlat16_0.x, 0.0, 1.0);
#endif
    u_xlat16_3.x = u_xlat16_0.x * -2.0 + 3.0;
    u_xlat16_0.x = u_xlat16_0.x * u_xlat16_0.x;
    u_xlat16_0.x = (-u_xlat16_3.x) * u_xlat16_0.x + 1.0;
    u_xlat16_0.x = u_xlat16_0.x * u_xlat16_0.x;
    u_xlat16_3.x = vs_TEXCOORD0.y * 50.0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
    u_xlat16_1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat16_3.xyz = u_xlat16_3.xxx * u_xlat16_1.xyz + vs_TEXCOORD2.xyz;
    u_xlat16_1.xyz = vs_TEXCOORD3.xyz * u_xlat16_0.xxx + u_xlat16_3.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb2 = !!(vs_TEXCOORD0.y<0.0);
#else
    u_xlatb2 = vs_TEXCOORD0.y<0.0;
#endif
    SV_Target0.xyz = (bool(u_xlatb2)) ? u_xlat16_1.xyz : u_xlat16_3.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier01 " {
Local Keywords { "_SUNDISK_SIMPLE" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump vec4 _LightColor0;
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out mediump vec3 vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
out mediump vec3 vs_TEXCOORD3;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
mediump vec3 u_xlat16_8;
float u_xlat9;
vec3 u_xlat14;
float u_xlat18;
float u_xlat23;
float u_xlat27;
float u_xlat28;
float u_xlat29;
mediump float u_xlat16_30;
float u_xlat31;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat27 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat27 = inversesqrt(u_xlat27);
    u_xlat2.xyz = vec3(u_xlat27) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat27 + u_xlat0.x;
        u_xlat9 = (-u_xlat0.y) * u_xlat27 + 1.0;
        u_xlat18 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat18 = u_xlat9 * u_xlat18 + 3.82999992;
        u_xlat18 = u_xlat9 * u_xlat18 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat18 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat0.y = exp2(u_xlat9);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat27 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat27 = sqrt(u_xlat27);
        u_xlat28 = (-u_xlat27) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat27;
        u_xlat31 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat27 = u_xlat31 / u_xlat27;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat31 = u_xlat29 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat29 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat29 * u_xlat31 + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat31 + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat27 = (-u_xlat27) + 1.0;
        u_xlat31 = u_xlat27 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat27 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat27 * u_xlat31 + 0.458999991;
        u_xlat27 = u_xlat27 * u_xlat31 + -0.00286999997;
        u_xlat27 = u_xlat27 * 1.44269502;
        u_xlat27 = exp2(u_xlat27);
        u_xlat27 = u_xlat27 * 0.25;
        u_xlat27 = u_xlat29 * 0.25 + (-u_xlat27);
        u_xlat27 = u_xlat28 * u_xlat27 + u_xlat0.y;
        u_xlat27 = max(u_xlat27, 0.0);
        u_xlat27 = min(u_xlat27, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat27)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat27 = u_xlat0.z * u_xlat28;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat28 = (-u_xlat0.x) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat4.x = u_xlat29 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat4.x + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat29 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat28 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat28;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat27) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat29 = min(u_xlat2.y, -0.00100000005);
        u_xlat29 = -9.99999975e-05 / u_xlat29;
        u_xlat5.xyz = vec3(u_xlat29) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat23 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.y * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.y * u_xlat23 + 0.458999991;
        u_xlat14.x = u_xlat5.y * u_xlat23 + -0.00286999997;
        u_xlat14.x = u_xlat14.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat14.x);
        u_xlat23 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.x * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.x * u_xlat23 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat23 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat14.xz = vec2(u_xlat29) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat14.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat29 = sqrt(u_xlat29);
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat29 = u_xlat29 * 230.831207;
        u_xlat29 = exp2(u_xlat29);
        u_xlat5.x = u_xlat29 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat29 = u_xlat14.z * u_xlat29;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat29);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_30 = u_xlat16_30 * u_xlat16_30;
    u_xlat16_30 = u_xlat16_30 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_30) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_LightColor0.xyz, _LightColor0.xyz);
    u_xlat16_30 = sqrt(u_xlat16_30);
    u_xlat16_30 = max(u_xlat16_30, 0.25);
    u_xlat16_30 = min(u_xlat16_30, 1.0);
    u_xlat16_8.xyz = u_xlat16_0.xyz * vec3(8000.0, 8000.0, 8000.0);
#ifdef UNITY_ADRENO_ES3
    u_xlat16_8.xyz = min(max(u_xlat16_8.xyz, 0.0), 1.0);
#else
    u_xlat16_8.xyz = clamp(u_xlat16_8.xyz, 0.0, 1.0);
#endif
    u_xlat16_8.xyz = u_xlat16_8.xyz * _LightColor0.xyz;
    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(27.0, 27.0, 27.0);
    u_xlat16_8.xyz = u_xlat16_8.xyz / vec3(u_xlat16_30);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD3.xyz = sqrt(u_xlat16_8.xyz);
    vs_TEXCOORD0.xyz = (-u_xlat2.xyz);
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	mediump float _SunSize;
in mediump vec3 vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
in mediump vec3 vs_TEXCOORD3;
layout(location = 0) out mediump vec4 SV_Target0;
mediump vec3 u_xlat16_0;
mediump vec3 u_xlat16_1;
bool u_xlatb2;
mediump vec3 u_xlat16_3;
void main()
{
    u_xlat16_0.xyz = vs_TEXCOORD0.xyz + _WorldSpaceLightPos0.xyz;
    u_xlat16_0.x = dot(u_xlat16_0.xyz, u_xlat16_0.xyz);
    u_xlat16_0.x = sqrt(u_xlat16_0.x);
    u_xlat16_3.x = float(1.0) / _SunSize;
    u_xlat16_0.x = u_xlat16_3.x * u_xlat16_0.x;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0.x = min(max(u_xlat16_0.x, 0.0), 1.0);
#else
    u_xlat16_0.x = clamp(u_xlat16_0.x, 0.0, 1.0);
#endif
    u_xlat16_3.x = u_xlat16_0.x * -2.0 + 3.0;
    u_xlat16_0.x = u_xlat16_0.x * u_xlat16_0.x;
    u_xlat16_0.x = (-u_xlat16_3.x) * u_xlat16_0.x + 1.0;
    u_xlat16_0.x = u_xlat16_0.x * u_xlat16_0.x;
    u_xlat16_3.x = vs_TEXCOORD0.y * 50.0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
    u_xlat16_1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat16_3.xyz = u_xlat16_3.xxx * u_xlat16_1.xyz + vs_TEXCOORD2.xyz;
    u_xlat16_1.xyz = vs_TEXCOORD3.xyz * u_xlat16_0.xxx + u_xlat16_3.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb2 = !!(vs_TEXCOORD0.y<0.0);
#else
    u_xlatb2 = vs_TEXCOORD0.y<0.0;
#endif
    SV_Target0.xyz = (bool(u_xlatb2)) ? u_xlat16_1.xyz : u_xlat16_3.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier02 " {
Local Keywords { "_SUNDISK_SIMPLE" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump vec4 _LightColor0;
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out mediump vec3 vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
out mediump vec3 vs_TEXCOORD3;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
mediump vec3 u_xlat16_8;
float u_xlat9;
vec3 u_xlat14;
float u_xlat18;
float u_xlat23;
float u_xlat27;
float u_xlat28;
float u_xlat29;
mediump float u_xlat16_30;
float u_xlat31;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat27 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat27 = inversesqrt(u_xlat27);
    u_xlat2.xyz = vec3(u_xlat27) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat27 + u_xlat0.x;
        u_xlat9 = (-u_xlat0.y) * u_xlat27 + 1.0;
        u_xlat18 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat18 = u_xlat9 * u_xlat18 + 3.82999992;
        u_xlat18 = u_xlat9 * u_xlat18 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat18 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat0.y = exp2(u_xlat9);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat27 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat27 = sqrt(u_xlat27);
        u_xlat28 = (-u_xlat27) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat27;
        u_xlat31 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat27 = u_xlat31 / u_xlat27;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat31 = u_xlat29 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat29 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat29 * u_xlat31 + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat31 + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat27 = (-u_xlat27) + 1.0;
        u_xlat31 = u_xlat27 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat27 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat27 * u_xlat31 + 0.458999991;
        u_xlat27 = u_xlat27 * u_xlat31 + -0.00286999997;
        u_xlat27 = u_xlat27 * 1.44269502;
        u_xlat27 = exp2(u_xlat27);
        u_xlat27 = u_xlat27 * 0.25;
        u_xlat27 = u_xlat29 * 0.25 + (-u_xlat27);
        u_xlat27 = u_xlat28 * u_xlat27 + u_xlat0.y;
        u_xlat27 = max(u_xlat27, 0.0);
        u_xlat27 = min(u_xlat27, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat27)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat27 = u_xlat0.z * u_xlat28;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat28 = (-u_xlat0.x) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat4.x = u_xlat29 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat4.x + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat29 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat28 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat28;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat27) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat29 = min(u_xlat2.y, -0.00100000005);
        u_xlat29 = -9.99999975e-05 / u_xlat29;
        u_xlat5.xyz = vec3(u_xlat29) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat23 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.y * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.y * u_xlat23 + 0.458999991;
        u_xlat14.x = u_xlat5.y * u_xlat23 + -0.00286999997;
        u_xlat14.x = u_xlat14.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat14.x);
        u_xlat23 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.x * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.x * u_xlat23 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat23 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat14.xz = vec2(u_xlat29) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat14.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat29 = sqrt(u_xlat29);
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat29 = u_xlat29 * 230.831207;
        u_xlat29 = exp2(u_xlat29);
        u_xlat5.x = u_xlat29 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat29 = u_xlat14.z * u_xlat29;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat29);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_30 = u_xlat16_30 * u_xlat16_30;
    u_xlat16_30 = u_xlat16_30 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_30) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_LightColor0.xyz, _LightColor0.xyz);
    u_xlat16_30 = sqrt(u_xlat16_30);
    u_xlat16_30 = max(u_xlat16_30, 0.25);
    u_xlat16_30 = min(u_xlat16_30, 1.0);
    u_xlat16_8.xyz = u_xlat16_0.xyz * vec3(8000.0, 8000.0, 8000.0);
#ifdef UNITY_ADRENO_ES3
    u_xlat16_8.xyz = min(max(u_xlat16_8.xyz, 0.0), 1.0);
#else
    u_xlat16_8.xyz = clamp(u_xlat16_8.xyz, 0.0, 1.0);
#endif
    u_xlat16_8.xyz = u_xlat16_8.xyz * _LightColor0.xyz;
    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(27.0, 27.0, 27.0);
    u_xlat16_8.xyz = u_xlat16_8.xyz / vec3(u_xlat16_30);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD3.xyz = sqrt(u_xlat16_8.xyz);
    vs_TEXCOORD0.xyz = (-u_xlat2.xyz);
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	mediump float _SunSize;
in mediump vec3 vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
in mediump vec3 vs_TEXCOORD3;
layout(location = 0) out mediump vec4 SV_Target0;
mediump vec3 u_xlat16_0;
mediump vec3 u_xlat16_1;
bool u_xlatb2;
mediump vec3 u_xlat16_3;
void main()
{
    u_xlat16_0.xyz = vs_TEXCOORD0.xyz + _WorldSpaceLightPos0.xyz;
    u_xlat16_0.x = dot(u_xlat16_0.xyz, u_xlat16_0.xyz);
    u_xlat16_0.x = sqrt(u_xlat16_0.x);
    u_xlat16_3.x = float(1.0) / _SunSize;
    u_xlat16_0.x = u_xlat16_3.x * u_xlat16_0.x;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0.x = min(max(u_xlat16_0.x, 0.0), 1.0);
#else
    u_xlat16_0.x = clamp(u_xlat16_0.x, 0.0, 1.0);
#endif
    u_xlat16_3.x = u_xlat16_0.x * -2.0 + 3.0;
    u_xlat16_0.x = u_xlat16_0.x * u_xlat16_0.x;
    u_xlat16_0.x = (-u_xlat16_3.x) * u_xlat16_0.x + 1.0;
    u_xlat16_0.x = u_xlat16_0.x * u_xlat16_0.x;
    u_xlat16_3.x = vs_TEXCOORD0.y * 50.0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
    u_xlat16_1.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat16_3.xyz = u_xlat16_3.xxx * u_xlat16_1.xyz + vs_TEXCOORD2.xyz;
    u_xlat16_1.xyz = vs_TEXCOORD3.xyz * u_xlat16_0.xxx + u_xlat16_3.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb2 = !!(vs_TEXCOORD0.y<0.0);
#else
    u_xlatb2 = vs_TEXCOORD0.y<0.0;
#endif
    SV_Target0.xyz = (bool(u_xlatb2)) ? u_xlat16_1.xyz : u_xlat16_3.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "vulkan hw_tier00 " {
Local Keywords { "_SUNDISK_SIMPLE" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 867
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %848 %851 %854 %857 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpMemberDecorate %19 7 RelaxedPrecision 
                                                      OpMemberDecorate %19 7 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %762 RelaxedPrecision 
                                                      OpDecorate %764 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %767 RelaxedPrecision 
                                                      OpDecorate %769 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %774 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %776 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %786 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %795 RelaxedPrecision 
                                                      OpDecorate %796 RelaxedPrecision 
                                                      OpDecorate %797 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %802 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %811 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate %816 RelaxedPrecision 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate %819 RelaxedPrecision 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate %822 RelaxedPrecision 
                                                      OpDecorate %823 RelaxedPrecision 
                                                      OpDecorate %824 RelaxedPrecision 
                                                      OpDecorate %825 RelaxedPrecision 
                                                      OpDecorate %826 RelaxedPrecision 
                                                      OpDecorate %829 RelaxedPrecision 
                                                      OpDecorate %830 RelaxedPrecision 
                                                      OpDecorate %831 RelaxedPrecision 
                                                      OpDecorate %832 RelaxedPrecision 
                                                      OpDecorate %833 RelaxedPrecision 
                                                      OpDecorate %834 RelaxedPrecision 
                                                      OpDecorate %836 RelaxedPrecision 
                                                      OpDecorate %837 RelaxedPrecision 
                                                      OpDecorate %838 RelaxedPrecision 
                                                      OpDecorate %839 RelaxedPrecision 
                                                      OpDecorate %842 RelaxedPrecision 
                                                      OpDecorate %843 RelaxedPrecision 
                                                      OpDecorate %844 RelaxedPrecision 
                                                      OpDecorate %845 RelaxedPrecision 
                                                      OpDecorate %846 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 848 
                                                      OpDecorate %849 RelaxedPrecision 
                                                      OpDecorate %850 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 851 
                                                      OpDecorate %852 RelaxedPrecision 
                                                      OpDecorate %853 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 Location 854 
                                                      OpDecorate %855 RelaxedPrecision 
                                                      OpDecorate %856 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 857 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %7 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32_4; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 6 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 7 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         i32 %760 = OpConstant 5 
                                         i32 %782 = OpConstant 4 
                                Private f32* %787 = OpVariable Private 
                                         f32 %799 = OpConstant 3.674022E-40 
                              Private f32_3* %802 = OpVariable Private 
                              Private f32_3* %825 = OpVariable Private 
                                         f32 %827 = OpConstant 3.674022E-40 
                                       f32_3 %828 = OpConstantComposite %827 %827 %827 
                                         f32 %840 = OpConstant 3.674022E-40 
                                       f32_3 %841 = OpConstantComposite %840 %840 %840 
                                             %847 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD3 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD0 = OpVariable Output 
                                             %861 = OpTypePointer Output %6 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Uniform f32* %761 = OpAccessChain %21 %760 %133 
                                         f32 %762 = OpLoad %761 
                                Uniform f32* %763 = OpAccessChain %21 %760 %70 
                                         f32 %764 = OpLoad %763 
                                Uniform f32* %765 = OpAccessChain %21 %760 %371 
                                         f32 %766 = OpLoad %765 
                                       f32_3 %767 = OpCompositeConstruct %762 %764 %766 
                                Uniform f32* %768 = OpAccessChain %21 %760 %133 
                                         f32 %769 = OpLoad %768 
                                Uniform f32* %770 = OpAccessChain %21 %760 %70 
                                         f32 %771 = OpLoad %770 
                                Uniform f32* %772 = OpAccessChain %21 %760 %371 
                                         f32 %773 = OpLoad %772 
                                       f32_3 %774 = OpCompositeConstruct %769 %771 %773 
                                       f32_3 %775 = OpFMul %767 %774 
                                                      OpStore %127 %775 
                                       f32_3 %776 = OpLoad %127 
                                       f32_3 %777 = OpLoad %561 
                                       f32_3 %778 = OpFMul %776 %777 
                                       f32_3 %779 = OpLoad %559 
                                       f32_3 %780 = OpFAdd %778 %779 
                                                      OpStore %127 %780 
                                       f32_3 %781 = OpLoad %127 
                                Uniform f32* %783 = OpAccessChain %21 %782 
                                         f32 %784 = OpLoad %783 
                                       f32_3 %785 = OpCompositeConstruct %784 %784 %784 
                                       f32_3 %786 = OpFMul %781 %785 
                                                      OpStore %127 %786 
                              Uniform f32_4* %788 = OpAccessChain %21 %28 
                                       f32_4 %789 = OpLoad %788 
                                       f32_3 %790 = OpVectorShuffle %789 %789 0 1 2 
                                       f32_4 %791 = OpLoad %50 
                                       f32_3 %792 = OpVectorShuffle %791 %791 0 1 2 
                                       f32_3 %793 = OpFNegate %792 
                                         f32 %794 = OpDot %790 %793 
                                                      OpStore %787 %794 
                                         f32 %795 = OpLoad %787 
                                         f32 %796 = OpLoad %787 
                                         f32 %797 = OpFMul %795 %796 
                                                      OpStore %787 %797 
                                         f32 %798 = OpLoad %787 
                                         f32 %800 = OpFMul %798 %799 
                                         f32 %801 = OpFAdd %800 %799 
                                                      OpStore %787 %801 
                                         f32 %803 = OpLoad %787 
                                       f32_3 %804 = OpCompositeConstruct %803 %803 %803 
                                       f32_3 %805 = OpLoad %559 
                                       f32_3 %806 = OpFMul %804 %805 
                                                      OpStore %802 %806 
                                       f32_3 %807 = OpLoad %802 
                                Uniform f32* %808 = OpAccessChain %21 %782 
                                         f32 %809 = OpLoad %808 
                                       f32_3 %810 = OpCompositeConstruct %809 %809 %809 
                                       f32_3 %811 = OpFMul %807 %810 
                                                      OpStore %802 %811 
                              Uniform f32_4* %812 = OpAccessChain %21 %46 
                                       f32_4 %813 = OpLoad %812 
                                       f32_3 %814 = OpVectorShuffle %813 %813 0 1 2 
                              Uniform f32_4* %815 = OpAccessChain %21 %46 
                                       f32_4 %816 = OpLoad %815 
                                       f32_3 %817 = OpVectorShuffle %816 %816 0 1 2 
                                         f32 %818 = OpDot %814 %817 
                                                      OpStore %787 %818 
                                         f32 %819 = OpLoad %787 
                                         f32 %820 = OpExtInst %1 31 %819 
                                                      OpStore %787 %820 
                                         f32 %821 = OpLoad %787 
                                         f32 %822 = OpExtInst %1 40 %821 %330 
                                                      OpStore %787 %822 
                                         f32 %823 = OpLoad %787 
                                         f32 %824 = OpExtInst %1 37 %823 %89 
                                                      OpStore %787 %824 
                                       f32_3 %826 = OpLoad %561 
                                       f32_3 %829 = OpFMul %826 %828 
                                                      OpStore %825 %829 
                                       f32_3 %830 = OpLoad %825 
                                       f32_3 %831 = OpCompositeConstruct %174 %174 %174 
                                       f32_3 %832 = OpCompositeConstruct %89 %89 %89 
                                       f32_3 %833 = OpExtInst %1 43 %830 %831 %832 
                                                      OpStore %825 %833 
                                       f32_3 %834 = OpLoad %825 
                              Uniform f32_4* %835 = OpAccessChain %21 %46 
                                       f32_4 %836 = OpLoad %835 
                                       f32_3 %837 = OpVectorShuffle %836 %836 0 1 2 
                                       f32_3 %838 = OpFMul %834 %837 
                                                      OpStore %825 %838 
                                       f32_3 %839 = OpLoad %825 
                                       f32_3 %842 = OpFMul %839 %841 
                                                      OpStore %825 %842 
                                       f32_3 %843 = OpLoad %825 
                                         f32 %844 = OpLoad %787 
                                       f32_3 %845 = OpCompositeConstruct %844 %844 %844 
                                       f32_3 %846 = OpFDiv %843 %845 
                                                      OpStore %825 %846 
                                       f32_3 %849 = OpLoad %127 
                                       f32_3 %850 = OpExtInst %1 31 %849 
                                                      OpStore vs_TEXCOORD1 %850 
                                       f32_3 %852 = OpLoad %802 
                                       f32_3 %853 = OpExtInst %1 31 %852 
                                                      OpStore vs_TEXCOORD2 %853 
                                       f32_3 %855 = OpLoad %825 
                                       f32_3 %856 = OpExtInst %1 31 %855 
                                                      OpStore vs_TEXCOORD3 %856 
                                       f32_4 %858 = OpLoad %50 
                                       f32_3 %859 = OpVectorShuffle %858 %858 0 1 2 
                                       f32_3 %860 = OpFNegate %859 
                                                      OpStore vs_TEXCOORD0 %860 
                                 Output f32* %862 = OpAccessChain %74 %28 %70 
                                         f32 %863 = OpLoad %862 
                                         f32 %864 = OpFNegate %863 
                                 Output f32* %865 = OpAccessChain %74 %28 %70 
                                                      OpStore %865 %864 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 135
; Schema: 0
                                      OpCapability Shader 
                               %1 = OpExtInstImport "GLSL.std.450" 
                                      OpMemoryModel Logical GLSL450 
                                      OpEntryPoint Fragment %4 "main" %11 %93 %95 %105 %119 
                                      OpExecutionMode %4 OriginUpperLeft 
                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                      OpDecorate %9 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD0 Location 11 
                                      OpDecorate %12 RelaxedPrecision 
                                      OpMemberDecorate %14 0 Offset 14 
                                      OpMemberDecorate %14 1 RelaxedPrecision 
                                      OpMemberDecorate %14 1 Offset 14 
                                      OpDecorate %14 Block 
                                      OpDecorate %16 DescriptorSet 16 
                                      OpDecorate %16 Binding 16 
                                      OpDecorate %24 RelaxedPrecision 
                                      OpDecorate %25 RelaxedPrecision 
                                      OpDecorate %26 RelaxedPrecision 
                                      OpDecorate %32 RelaxedPrecision 
                                      OpDecorate %33 RelaxedPrecision 
                                      OpDecorate %35 RelaxedPrecision 
                                      OpDecorate %40 RelaxedPrecision 
                                      OpDecorate %41 RelaxedPrecision 
                                      OpDecorate %44 RelaxedPrecision 
                                      OpDecorate %46 RelaxedPrecision 
                                      OpDecorate %47 RelaxedPrecision 
                                      OpDecorate %50 RelaxedPrecision 
                                      OpDecorate %52 RelaxedPrecision 
                                      OpDecorate %55 RelaxedPrecision 
                                      OpDecorate %57 RelaxedPrecision 
                                      OpDecorate %59 RelaxedPrecision 
                                      OpDecorate %62 RelaxedPrecision 
                                      OpDecorate %64 RelaxedPrecision 
                                      OpDecorate %65 RelaxedPrecision 
                                      OpDecorate %68 RelaxedPrecision 
                                      OpDecorate %69 RelaxedPrecision 
                                      OpDecorate %71 RelaxedPrecision 
                                      OpDecorate %72 RelaxedPrecision 
                                      OpDecorate %73 RelaxedPrecision 
                                      OpDecorate %76 RelaxedPrecision 
                                      OpDecorate %78 RelaxedPrecision 
                                      OpDecorate %79 RelaxedPrecision 
                                      OpDecorate %84 RelaxedPrecision 
                                      OpDecorate %86 RelaxedPrecision 
                                      OpDecorate %89 RelaxedPrecision 
                                      OpDecorate %90 RelaxedPrecision 
                                      OpDecorate %92 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD1 Location 93 
                                      OpDecorate %94 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD2 Location 95 
                                      OpDecorate %96 RelaxedPrecision 
                                      OpDecorate %97 RelaxedPrecision 
                                      OpDecorate %98 RelaxedPrecision 
                                      OpDecorate %99 RelaxedPrecision 
                                      OpDecorate %100 RelaxedPrecision 
                                      OpDecorate %101 RelaxedPrecision 
                                      OpDecorate %102 RelaxedPrecision 
                                      OpDecorate %103 RelaxedPrecision 
                                      OpDecorate %104 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD3 Location 105 
                                      OpDecorate %106 RelaxedPrecision 
                                      OpDecorate %107 RelaxedPrecision 
                                      OpDecorate %108 RelaxedPrecision 
                                      OpDecorate %109 RelaxedPrecision 
                                      OpDecorate %110 RelaxedPrecision 
                                      OpDecorate %111 RelaxedPrecision 
                                      OpDecorate %116 RelaxedPrecision 
                                      OpDecorate %119 RelaxedPrecision 
                                      OpDecorate %119 Location 119 
                                      OpDecorate %125 RelaxedPrecision 
                                      OpDecorate %127 RelaxedPrecision 
                                      OpDecorate %128 RelaxedPrecision 
                               %2 = OpTypeVoid 
                               %3 = OpTypeFunction %2 
                               %6 = OpTypeFloat 32 
                               %7 = OpTypeVector %6 3 
                               %8 = OpTypePointer Private %7 
                Private f32_3* %9 = OpVariable Private 
                              %10 = OpTypePointer Input %7 
        Input f32_3* vs_TEXCOORD0 = OpVariable Input 
                              %13 = OpTypeVector %6 4 
                              %14 = OpTypeStruct %13 %6 
                              %15 = OpTypePointer Uniform %14 
Uniform struct {f32_4; f32;}* %16 = OpVariable Uniform 
                              %17 = OpTypeInt 32 1 
                          i32 %18 = OpConstant 0 
                              %19 = OpTypePointer Uniform %13 
                              %27 = OpTypeInt 32 0 
                          u32 %28 = OpConstant 0 
                              %29 = OpTypePointer Private %6 
               Private f32_3* %35 = OpVariable Private 
                          f32 %36 = OpConstant 3.674022E-40 
                          i32 %37 = OpConstant 1 
                              %38 = OpTypePointer Uniform %6 
                          f32 %51 = OpConstant 3.674022E-40 
                          f32 %56 = OpConstant 3.674022E-40 
                          f32 %58 = OpConstant 3.674022E-40 
                          u32 %81 = OpConstant 1 
                              %82 = OpTypePointer Input %6 
                          f32 %85 = OpConstant 3.674022E-40 
               Private f32_3* %92 = OpVariable Private 
        Input f32_3* vs_TEXCOORD1 = OpVariable Input 
        Input f32_3* vs_TEXCOORD2 = OpVariable Input 
        Input f32_3* vs_TEXCOORD3 = OpVariable Input 
                             %112 = OpTypeBool 
                             %113 = OpTypePointer Private %112 
               Private bool* %114 = OpVariable Private 
                             %118 = OpTypePointer Output %13 
               Output f32_4* %119 = OpVariable Output 
                             %121 = OpTypePointer Function %7 
                         u32 %131 = OpConstant 3 
                             %132 = OpTypePointer Output %6 
                          void %4 = OpFunction None %3 
                               %5 = OpLabel 
             Function f32_3* %122 = OpVariable Function 
                        f32_3 %12 = OpLoad vs_TEXCOORD0 
               Uniform f32_4* %20 = OpAccessChain %16 %18 
                        f32_4 %21 = OpLoad %20 
                        f32_3 %22 = OpVectorShuffle %21 %21 0 1 2 
                        f32_3 %23 = OpFAdd %12 %22 
                                      OpStore %9 %23 
                        f32_3 %24 = OpLoad %9 
                        f32_3 %25 = OpLoad %9 
                          f32 %26 = OpDot %24 %25 
                 Private f32* %30 = OpAccessChain %9 %28 
                                      OpStore %30 %26 
                 Private f32* %31 = OpAccessChain %9 %28 
                          f32 %32 = OpLoad %31 
                          f32 %33 = OpExtInst %1 31 %32 
                 Private f32* %34 = OpAccessChain %9 %28 
                                      OpStore %34 %33 
                 Uniform f32* %39 = OpAccessChain %16 %37 
                          f32 %40 = OpLoad %39 
                          f32 %41 = OpFDiv %36 %40 
                 Private f32* %42 = OpAccessChain %35 %28 
                                      OpStore %42 %41 
                 Private f32* %43 = OpAccessChain %35 %28 
                          f32 %44 = OpLoad %43 
                 Private f32* %45 = OpAccessChain %9 %28 
                          f32 %46 = OpLoad %45 
                          f32 %47 = OpFMul %44 %46 
                 Private f32* %48 = OpAccessChain %9 %28 
                                      OpStore %48 %47 
                 Private f32* %49 = OpAccessChain %9 %28 
                          f32 %50 = OpLoad %49 
                          f32 %52 = OpExtInst %1 43 %50 %51 %36 
                 Private f32* %53 = OpAccessChain %9 %28 
                                      OpStore %53 %52 
                 Private f32* %54 = OpAccessChain %9 %28 
                          f32 %55 = OpLoad %54 
                          f32 %57 = OpFMul %55 %56 
                          f32 %59 = OpFAdd %57 %58 
                 Private f32* %60 = OpAccessChain %35 %28 
                                      OpStore %60 %59 
                 Private f32* %61 = OpAccessChain %9 %28 
                          f32 %62 = OpLoad %61 
                 Private f32* %63 = OpAccessChain %9 %28 
                          f32 %64 = OpLoad %63 
                          f32 %65 = OpFMul %62 %64 
                 Private f32* %66 = OpAccessChain %9 %28 
                                      OpStore %66 %65 
                 Private f32* %67 = OpAccessChain %35 %28 
                          f32 %68 = OpLoad %67 
                          f32 %69 = OpFNegate %68 
                 Private f32* %70 = OpAccessChain %9 %28 
                          f32 %71 = OpLoad %70 
                          f32 %72 = OpFMul %69 %71 
                          f32 %73 = OpFAdd %72 %36 
                 Private f32* %74 = OpAccessChain %9 %28 
                                      OpStore %74 %73 
                 Private f32* %75 = OpAccessChain %9 %28 
                          f32 %76 = OpLoad %75 
                 Private f32* %77 = OpAccessChain %9 %28 
                          f32 %78 = OpLoad %77 
                          f32 %79 = OpFMul %76 %78 
                 Private f32* %80 = OpAccessChain %9 %28 
                                      OpStore %80 %79 
                   Input f32* %83 = OpAccessChain vs_TEXCOORD0 %81 
                          f32 %84 = OpLoad %83 
                          f32 %86 = OpFMul %84 %85 
                 Private f32* %87 = OpAccessChain %35 %28 
                                      OpStore %87 %86 
                 Private f32* %88 = OpAccessChain %35 %28 
                          f32 %89 = OpLoad %88 
                          f32 %90 = OpExtInst %1 43 %89 %51 %36 
                 Private f32* %91 = OpAccessChain %35 %28 
                                      OpStore %91 %90 
                        f32_3 %94 = OpLoad vs_TEXCOORD1 
                        f32_3 %96 = OpLoad vs_TEXCOORD2 
                        f32_3 %97 = OpFNegate %96 
                        f32_3 %98 = OpFAdd %94 %97 
                                      OpStore %92 %98 
                        f32_3 %99 = OpLoad %35 
                       f32_3 %100 = OpVectorShuffle %99 %99 0 0 0 
                       f32_3 %101 = OpLoad %92 
                       f32_3 %102 = OpFMul %100 %101 
                       f32_3 %103 = OpLoad vs_TEXCOORD2 
                       f32_3 %104 = OpFAdd %102 %103 
                                      OpStore %35 %104 
                       f32_3 %106 = OpLoad vs_TEXCOORD3 
                       f32_3 %107 = OpLoad %9 
                       f32_3 %108 = OpVectorShuffle %107 %107 0 0 0 
                       f32_3 %109 = OpFMul %106 %108 
                       f32_3 %110 = OpLoad %35 
                       f32_3 %111 = OpFAdd %109 %110 
                                      OpStore %92 %111 
                  Input f32* %115 = OpAccessChain vs_TEXCOORD0 %81 
                         f32 %116 = OpLoad %115 
                        bool %117 = OpFOrdLessThan %116 %51 
                                      OpStore %114 %117 
                        bool %120 = OpLoad %114 
                                      OpSelectionMerge %124 None 
                                      OpBranchConditional %120 %123 %126 
                             %123 = OpLabel 
                       f32_3 %125 = OpLoad %92 
                                      OpStore %122 %125 
                                      OpBranch %124 
                             %126 = OpLabel 
                       f32_3 %127 = OpLoad %35 
                                      OpStore %122 %127 
                                      OpBranch %124 
                             %124 = OpLabel 
                       f32_3 %128 = OpLoad %122 
                       f32_4 %129 = OpLoad %119 
                       f32_4 %130 = OpVectorShuffle %129 %128 4 5 6 3 
                                      OpStore %119 %130 
                 Output f32* %133 = OpAccessChain %119 %131 
                                      OpStore %133 %36 
                                      OpReturn
                                      OpFunctionEnd
"
}
SubProgram "vulkan hw_tier01 " {
Local Keywords { "_SUNDISK_SIMPLE" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 867
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %848 %851 %854 %857 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpMemberDecorate %19 7 RelaxedPrecision 
                                                      OpMemberDecorate %19 7 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %762 RelaxedPrecision 
                                                      OpDecorate %764 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %767 RelaxedPrecision 
                                                      OpDecorate %769 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %774 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %776 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %786 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %795 RelaxedPrecision 
                                                      OpDecorate %796 RelaxedPrecision 
                                                      OpDecorate %797 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %802 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %811 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate %816 RelaxedPrecision 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate %819 RelaxedPrecision 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate %822 RelaxedPrecision 
                                                      OpDecorate %823 RelaxedPrecision 
                                                      OpDecorate %824 RelaxedPrecision 
                                                      OpDecorate %825 RelaxedPrecision 
                                                      OpDecorate %826 RelaxedPrecision 
                                                      OpDecorate %829 RelaxedPrecision 
                                                      OpDecorate %830 RelaxedPrecision 
                                                      OpDecorate %831 RelaxedPrecision 
                                                      OpDecorate %832 RelaxedPrecision 
                                                      OpDecorate %833 RelaxedPrecision 
                                                      OpDecorate %834 RelaxedPrecision 
                                                      OpDecorate %836 RelaxedPrecision 
                                                      OpDecorate %837 RelaxedPrecision 
                                                      OpDecorate %838 RelaxedPrecision 
                                                      OpDecorate %839 RelaxedPrecision 
                                                      OpDecorate %842 RelaxedPrecision 
                                                      OpDecorate %843 RelaxedPrecision 
                                                      OpDecorate %844 RelaxedPrecision 
                                                      OpDecorate %845 RelaxedPrecision 
                                                      OpDecorate %846 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 848 
                                                      OpDecorate %849 RelaxedPrecision 
                                                      OpDecorate %850 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 851 
                                                      OpDecorate %852 RelaxedPrecision 
                                                      OpDecorate %853 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 Location 854 
                                                      OpDecorate %855 RelaxedPrecision 
                                                      OpDecorate %856 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 857 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %7 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32_4; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 6 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 7 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         i32 %760 = OpConstant 5 
                                         i32 %782 = OpConstant 4 
                                Private f32* %787 = OpVariable Private 
                                         f32 %799 = OpConstant 3.674022E-40 
                              Private f32_3* %802 = OpVariable Private 
                              Private f32_3* %825 = OpVariable Private 
                                         f32 %827 = OpConstant 3.674022E-40 
                                       f32_3 %828 = OpConstantComposite %827 %827 %827 
                                         f32 %840 = OpConstant 3.674022E-40 
                                       f32_3 %841 = OpConstantComposite %840 %840 %840 
                                             %847 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD3 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD0 = OpVariable Output 
                                             %861 = OpTypePointer Output %6 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Uniform f32* %761 = OpAccessChain %21 %760 %133 
                                         f32 %762 = OpLoad %761 
                                Uniform f32* %763 = OpAccessChain %21 %760 %70 
                                         f32 %764 = OpLoad %763 
                                Uniform f32* %765 = OpAccessChain %21 %760 %371 
                                         f32 %766 = OpLoad %765 
                                       f32_3 %767 = OpCompositeConstruct %762 %764 %766 
                                Uniform f32* %768 = OpAccessChain %21 %760 %133 
                                         f32 %769 = OpLoad %768 
                                Uniform f32* %770 = OpAccessChain %21 %760 %70 
                                         f32 %771 = OpLoad %770 
                                Uniform f32* %772 = OpAccessChain %21 %760 %371 
                                         f32 %773 = OpLoad %772 
                                       f32_3 %774 = OpCompositeConstruct %769 %771 %773 
                                       f32_3 %775 = OpFMul %767 %774 
                                                      OpStore %127 %775 
                                       f32_3 %776 = OpLoad %127 
                                       f32_3 %777 = OpLoad %561 
                                       f32_3 %778 = OpFMul %776 %777 
                                       f32_3 %779 = OpLoad %559 
                                       f32_3 %780 = OpFAdd %778 %779 
                                                      OpStore %127 %780 
                                       f32_3 %781 = OpLoad %127 
                                Uniform f32* %783 = OpAccessChain %21 %782 
                                         f32 %784 = OpLoad %783 
                                       f32_3 %785 = OpCompositeConstruct %784 %784 %784 
                                       f32_3 %786 = OpFMul %781 %785 
                                                      OpStore %127 %786 
                              Uniform f32_4* %788 = OpAccessChain %21 %28 
                                       f32_4 %789 = OpLoad %788 
                                       f32_3 %790 = OpVectorShuffle %789 %789 0 1 2 
                                       f32_4 %791 = OpLoad %50 
                                       f32_3 %792 = OpVectorShuffle %791 %791 0 1 2 
                                       f32_3 %793 = OpFNegate %792 
                                         f32 %794 = OpDot %790 %793 
                                                      OpStore %787 %794 
                                         f32 %795 = OpLoad %787 
                                         f32 %796 = OpLoad %787 
                                         f32 %797 = OpFMul %795 %796 
                                                      OpStore %787 %797 
                                         f32 %798 = OpLoad %787 
                                         f32 %800 = OpFMul %798 %799 
                                         f32 %801 = OpFAdd %800 %799 
                                                      OpStore %787 %801 
                                         f32 %803 = OpLoad %787 
                                       f32_3 %804 = OpCompositeConstruct %803 %803 %803 
                                       f32_3 %805 = OpLoad %559 
                                       f32_3 %806 = OpFMul %804 %805 
                                                      OpStore %802 %806 
                                       f32_3 %807 = OpLoad %802 
                                Uniform f32* %808 = OpAccessChain %21 %782 
                                         f32 %809 = OpLoad %808 
                                       f32_3 %810 = OpCompositeConstruct %809 %809 %809 
                                       f32_3 %811 = OpFMul %807 %810 
                                                      OpStore %802 %811 
                              Uniform f32_4* %812 = OpAccessChain %21 %46 
                                       f32_4 %813 = OpLoad %812 
                                       f32_3 %814 = OpVectorShuffle %813 %813 0 1 2 
                              Uniform f32_4* %815 = OpAccessChain %21 %46 
                                       f32_4 %816 = OpLoad %815 
                                       f32_3 %817 = OpVectorShuffle %816 %816 0 1 2 
                                         f32 %818 = OpDot %814 %817 
                                                      OpStore %787 %818 
                                         f32 %819 = OpLoad %787 
                                         f32 %820 = OpExtInst %1 31 %819 
                                                      OpStore %787 %820 
                                         f32 %821 = OpLoad %787 
                                         f32 %822 = OpExtInst %1 40 %821 %330 
                                                      OpStore %787 %822 
                                         f32 %823 = OpLoad %787 
                                         f32 %824 = OpExtInst %1 37 %823 %89 
                                                      OpStore %787 %824 
                                       f32_3 %826 = OpLoad %561 
                                       f32_3 %829 = OpFMul %826 %828 
                                                      OpStore %825 %829 
                                       f32_3 %830 = OpLoad %825 
                                       f32_3 %831 = OpCompositeConstruct %174 %174 %174 
                                       f32_3 %832 = OpCompositeConstruct %89 %89 %89 
                                       f32_3 %833 = OpExtInst %1 43 %830 %831 %832 
                                                      OpStore %825 %833 
                                       f32_3 %834 = OpLoad %825 
                              Uniform f32_4* %835 = OpAccessChain %21 %46 
                                       f32_4 %836 = OpLoad %835 
                                       f32_3 %837 = OpVectorShuffle %836 %836 0 1 2 
                                       f32_3 %838 = OpFMul %834 %837 
                                                      OpStore %825 %838 
                                       f32_3 %839 = OpLoad %825 
                                       f32_3 %842 = OpFMul %839 %841 
                                                      OpStore %825 %842 
                                       f32_3 %843 = OpLoad %825 
                                         f32 %844 = OpLoad %787 
                                       f32_3 %845 = OpCompositeConstruct %844 %844 %844 
                                       f32_3 %846 = OpFDiv %843 %845 
                                                      OpStore %825 %846 
                                       f32_3 %849 = OpLoad %127 
                                       f32_3 %850 = OpExtInst %1 31 %849 
                                                      OpStore vs_TEXCOORD1 %850 
                                       f32_3 %852 = OpLoad %802 
                                       f32_3 %853 = OpExtInst %1 31 %852 
                                                      OpStore vs_TEXCOORD2 %853 
                                       f32_3 %855 = OpLoad %825 
                                       f32_3 %856 = OpExtInst %1 31 %855 
                                                      OpStore vs_TEXCOORD3 %856 
                                       f32_4 %858 = OpLoad %50 
                                       f32_3 %859 = OpVectorShuffle %858 %858 0 1 2 
                                       f32_3 %860 = OpFNegate %859 
                                                      OpStore vs_TEXCOORD0 %860 
                                 Output f32* %862 = OpAccessChain %74 %28 %70 
                                         f32 %863 = OpLoad %862 
                                         f32 %864 = OpFNegate %863 
                                 Output f32* %865 = OpAccessChain %74 %28 %70 
                                                      OpStore %865 %864 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 135
; Schema: 0
                                      OpCapability Shader 
                               %1 = OpExtInstImport "GLSL.std.450" 
                                      OpMemoryModel Logical GLSL450 
                                      OpEntryPoint Fragment %4 "main" %11 %93 %95 %105 %119 
                                      OpExecutionMode %4 OriginUpperLeft 
                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                      OpDecorate %9 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD0 Location 11 
                                      OpDecorate %12 RelaxedPrecision 
                                      OpMemberDecorate %14 0 Offset 14 
                                      OpMemberDecorate %14 1 RelaxedPrecision 
                                      OpMemberDecorate %14 1 Offset 14 
                                      OpDecorate %14 Block 
                                      OpDecorate %16 DescriptorSet 16 
                                      OpDecorate %16 Binding 16 
                                      OpDecorate %24 RelaxedPrecision 
                                      OpDecorate %25 RelaxedPrecision 
                                      OpDecorate %26 RelaxedPrecision 
                                      OpDecorate %32 RelaxedPrecision 
                                      OpDecorate %33 RelaxedPrecision 
                                      OpDecorate %35 RelaxedPrecision 
                                      OpDecorate %40 RelaxedPrecision 
                                      OpDecorate %41 RelaxedPrecision 
                                      OpDecorate %44 RelaxedPrecision 
                                      OpDecorate %46 RelaxedPrecision 
                                      OpDecorate %47 RelaxedPrecision 
                                      OpDecorate %50 RelaxedPrecision 
                                      OpDecorate %52 RelaxedPrecision 
                                      OpDecorate %55 RelaxedPrecision 
                                      OpDecorate %57 RelaxedPrecision 
                                      OpDecorate %59 RelaxedPrecision 
                                      OpDecorate %62 RelaxedPrecision 
                                      OpDecorate %64 RelaxedPrecision 
                                      OpDecorate %65 RelaxedPrecision 
                                      OpDecorate %68 RelaxedPrecision 
                                      OpDecorate %69 RelaxedPrecision 
                                      OpDecorate %71 RelaxedPrecision 
                                      OpDecorate %72 RelaxedPrecision 
                                      OpDecorate %73 RelaxedPrecision 
                                      OpDecorate %76 RelaxedPrecision 
                                      OpDecorate %78 RelaxedPrecision 
                                      OpDecorate %79 RelaxedPrecision 
                                      OpDecorate %84 RelaxedPrecision 
                                      OpDecorate %86 RelaxedPrecision 
                                      OpDecorate %89 RelaxedPrecision 
                                      OpDecorate %90 RelaxedPrecision 
                                      OpDecorate %92 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD1 Location 93 
                                      OpDecorate %94 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD2 Location 95 
                                      OpDecorate %96 RelaxedPrecision 
                                      OpDecorate %97 RelaxedPrecision 
                                      OpDecorate %98 RelaxedPrecision 
                                      OpDecorate %99 RelaxedPrecision 
                                      OpDecorate %100 RelaxedPrecision 
                                      OpDecorate %101 RelaxedPrecision 
                                      OpDecorate %102 RelaxedPrecision 
                                      OpDecorate %103 RelaxedPrecision 
                                      OpDecorate %104 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD3 Location 105 
                                      OpDecorate %106 RelaxedPrecision 
                                      OpDecorate %107 RelaxedPrecision 
                                      OpDecorate %108 RelaxedPrecision 
                                      OpDecorate %109 RelaxedPrecision 
                                      OpDecorate %110 RelaxedPrecision 
                                      OpDecorate %111 RelaxedPrecision 
                                      OpDecorate %116 RelaxedPrecision 
                                      OpDecorate %119 RelaxedPrecision 
                                      OpDecorate %119 Location 119 
                                      OpDecorate %125 RelaxedPrecision 
                                      OpDecorate %127 RelaxedPrecision 
                                      OpDecorate %128 RelaxedPrecision 
                               %2 = OpTypeVoid 
                               %3 = OpTypeFunction %2 
                               %6 = OpTypeFloat 32 
                               %7 = OpTypeVector %6 3 
                               %8 = OpTypePointer Private %7 
                Private f32_3* %9 = OpVariable Private 
                              %10 = OpTypePointer Input %7 
        Input f32_3* vs_TEXCOORD0 = OpVariable Input 
                              %13 = OpTypeVector %6 4 
                              %14 = OpTypeStruct %13 %6 
                              %15 = OpTypePointer Uniform %14 
Uniform struct {f32_4; f32;}* %16 = OpVariable Uniform 
                              %17 = OpTypeInt 32 1 
                          i32 %18 = OpConstant 0 
                              %19 = OpTypePointer Uniform %13 
                              %27 = OpTypeInt 32 0 
                          u32 %28 = OpConstant 0 
                              %29 = OpTypePointer Private %6 
               Private f32_3* %35 = OpVariable Private 
                          f32 %36 = OpConstant 3.674022E-40 
                          i32 %37 = OpConstant 1 
                              %38 = OpTypePointer Uniform %6 
                          f32 %51 = OpConstant 3.674022E-40 
                          f32 %56 = OpConstant 3.674022E-40 
                          f32 %58 = OpConstant 3.674022E-40 
                          u32 %81 = OpConstant 1 
                              %82 = OpTypePointer Input %6 
                          f32 %85 = OpConstant 3.674022E-40 
               Private f32_3* %92 = OpVariable Private 
        Input f32_3* vs_TEXCOORD1 = OpVariable Input 
        Input f32_3* vs_TEXCOORD2 = OpVariable Input 
        Input f32_3* vs_TEXCOORD3 = OpVariable Input 
                             %112 = OpTypeBool 
                             %113 = OpTypePointer Private %112 
               Private bool* %114 = OpVariable Private 
                             %118 = OpTypePointer Output %13 
               Output f32_4* %119 = OpVariable Output 
                             %121 = OpTypePointer Function %7 
                         u32 %131 = OpConstant 3 
                             %132 = OpTypePointer Output %6 
                          void %4 = OpFunction None %3 
                               %5 = OpLabel 
             Function f32_3* %122 = OpVariable Function 
                        f32_3 %12 = OpLoad vs_TEXCOORD0 
               Uniform f32_4* %20 = OpAccessChain %16 %18 
                        f32_4 %21 = OpLoad %20 
                        f32_3 %22 = OpVectorShuffle %21 %21 0 1 2 
                        f32_3 %23 = OpFAdd %12 %22 
                                      OpStore %9 %23 
                        f32_3 %24 = OpLoad %9 
                        f32_3 %25 = OpLoad %9 
                          f32 %26 = OpDot %24 %25 
                 Private f32* %30 = OpAccessChain %9 %28 
                                      OpStore %30 %26 
                 Private f32* %31 = OpAccessChain %9 %28 
                          f32 %32 = OpLoad %31 
                          f32 %33 = OpExtInst %1 31 %32 
                 Private f32* %34 = OpAccessChain %9 %28 
                                      OpStore %34 %33 
                 Uniform f32* %39 = OpAccessChain %16 %37 
                          f32 %40 = OpLoad %39 
                          f32 %41 = OpFDiv %36 %40 
                 Private f32* %42 = OpAccessChain %35 %28 
                                      OpStore %42 %41 
                 Private f32* %43 = OpAccessChain %35 %28 
                          f32 %44 = OpLoad %43 
                 Private f32* %45 = OpAccessChain %9 %28 
                          f32 %46 = OpLoad %45 
                          f32 %47 = OpFMul %44 %46 
                 Private f32* %48 = OpAccessChain %9 %28 
                                      OpStore %48 %47 
                 Private f32* %49 = OpAccessChain %9 %28 
                          f32 %50 = OpLoad %49 
                          f32 %52 = OpExtInst %1 43 %50 %51 %36 
                 Private f32* %53 = OpAccessChain %9 %28 
                                      OpStore %53 %52 
                 Private f32* %54 = OpAccessChain %9 %28 
                          f32 %55 = OpLoad %54 
                          f32 %57 = OpFMul %55 %56 
                          f32 %59 = OpFAdd %57 %58 
                 Private f32* %60 = OpAccessChain %35 %28 
                                      OpStore %60 %59 
                 Private f32* %61 = OpAccessChain %9 %28 
                          f32 %62 = OpLoad %61 
                 Private f32* %63 = OpAccessChain %9 %28 
                          f32 %64 = OpLoad %63 
                          f32 %65 = OpFMul %62 %64 
                 Private f32* %66 = OpAccessChain %9 %28 
                                      OpStore %66 %65 
                 Private f32* %67 = OpAccessChain %35 %28 
                          f32 %68 = OpLoad %67 
                          f32 %69 = OpFNegate %68 
                 Private f32* %70 = OpAccessChain %9 %28 
                          f32 %71 = OpLoad %70 
                          f32 %72 = OpFMul %69 %71 
                          f32 %73 = OpFAdd %72 %36 
                 Private f32* %74 = OpAccessChain %9 %28 
                                      OpStore %74 %73 
                 Private f32* %75 = OpAccessChain %9 %28 
                          f32 %76 = OpLoad %75 
                 Private f32* %77 = OpAccessChain %9 %28 
                          f32 %78 = OpLoad %77 
                          f32 %79 = OpFMul %76 %78 
                 Private f32* %80 = OpAccessChain %9 %28 
                                      OpStore %80 %79 
                   Input f32* %83 = OpAccessChain vs_TEXCOORD0 %81 
                          f32 %84 = OpLoad %83 
                          f32 %86 = OpFMul %84 %85 
                 Private f32* %87 = OpAccessChain %35 %28 
                                      OpStore %87 %86 
                 Private f32* %88 = OpAccessChain %35 %28 
                          f32 %89 = OpLoad %88 
                          f32 %90 = OpExtInst %1 43 %89 %51 %36 
                 Private f32* %91 = OpAccessChain %35 %28 
                                      OpStore %91 %90 
                        f32_3 %94 = OpLoad vs_TEXCOORD1 
                        f32_3 %96 = OpLoad vs_TEXCOORD2 
                        f32_3 %97 = OpFNegate %96 
                        f32_3 %98 = OpFAdd %94 %97 
                                      OpStore %92 %98 
                        f32_3 %99 = OpLoad %35 
                       f32_3 %100 = OpVectorShuffle %99 %99 0 0 0 
                       f32_3 %101 = OpLoad %92 
                       f32_3 %102 = OpFMul %100 %101 
                       f32_3 %103 = OpLoad vs_TEXCOORD2 
                       f32_3 %104 = OpFAdd %102 %103 
                                      OpStore %35 %104 
                       f32_3 %106 = OpLoad vs_TEXCOORD3 
                       f32_3 %107 = OpLoad %9 
                       f32_3 %108 = OpVectorShuffle %107 %107 0 0 0 
                       f32_3 %109 = OpFMul %106 %108 
                       f32_3 %110 = OpLoad %35 
                       f32_3 %111 = OpFAdd %109 %110 
                                      OpStore %92 %111 
                  Input f32* %115 = OpAccessChain vs_TEXCOORD0 %81 
                         f32 %116 = OpLoad %115 
                        bool %117 = OpFOrdLessThan %116 %51 
                                      OpStore %114 %117 
                        bool %120 = OpLoad %114 
                                      OpSelectionMerge %124 None 
                                      OpBranchConditional %120 %123 %126 
                             %123 = OpLabel 
                       f32_3 %125 = OpLoad %92 
                                      OpStore %122 %125 
                                      OpBranch %124 
                             %126 = OpLabel 
                       f32_3 %127 = OpLoad %35 
                                      OpStore %122 %127 
                                      OpBranch %124 
                             %124 = OpLabel 
                       f32_3 %128 = OpLoad %122 
                       f32_4 %129 = OpLoad %119 
                       f32_4 %130 = OpVectorShuffle %129 %128 4 5 6 3 
                                      OpStore %119 %130 
                 Output f32* %133 = OpAccessChain %119 %131 
                                      OpStore %133 %36 
                                      OpReturn
                                      OpFunctionEnd
"
}
SubProgram "vulkan hw_tier02 " {
Local Keywords { "_SUNDISK_SIMPLE" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 867
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %848 %851 %854 %857 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpMemberDecorate %19 7 RelaxedPrecision 
                                                      OpMemberDecorate %19 7 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %762 RelaxedPrecision 
                                                      OpDecorate %764 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %767 RelaxedPrecision 
                                                      OpDecorate %769 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %774 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %776 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %786 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %795 RelaxedPrecision 
                                                      OpDecorate %796 RelaxedPrecision 
                                                      OpDecorate %797 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %802 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %811 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate %816 RelaxedPrecision 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate %819 RelaxedPrecision 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate %822 RelaxedPrecision 
                                                      OpDecorate %823 RelaxedPrecision 
                                                      OpDecorate %824 RelaxedPrecision 
                                                      OpDecorate %825 RelaxedPrecision 
                                                      OpDecorate %826 RelaxedPrecision 
                                                      OpDecorate %829 RelaxedPrecision 
                                                      OpDecorate %830 RelaxedPrecision 
                                                      OpDecorate %831 RelaxedPrecision 
                                                      OpDecorate %832 RelaxedPrecision 
                                                      OpDecorate %833 RelaxedPrecision 
                                                      OpDecorate %834 RelaxedPrecision 
                                                      OpDecorate %836 RelaxedPrecision 
                                                      OpDecorate %837 RelaxedPrecision 
                                                      OpDecorate %838 RelaxedPrecision 
                                                      OpDecorate %839 RelaxedPrecision 
                                                      OpDecorate %842 RelaxedPrecision 
                                                      OpDecorate %843 RelaxedPrecision 
                                                      OpDecorate %844 RelaxedPrecision 
                                                      OpDecorate %845 RelaxedPrecision 
                                                      OpDecorate %846 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 848 
                                                      OpDecorate %849 RelaxedPrecision 
                                                      OpDecorate %850 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 851 
                                                      OpDecorate %852 RelaxedPrecision 
                                                      OpDecorate %853 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 Location 854 
                                                      OpDecorate %855 RelaxedPrecision 
                                                      OpDecorate %856 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 857 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %7 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32_4; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 6 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 7 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         i32 %760 = OpConstant 5 
                                         i32 %782 = OpConstant 4 
                                Private f32* %787 = OpVariable Private 
                                         f32 %799 = OpConstant 3.674022E-40 
                              Private f32_3* %802 = OpVariable Private 
                              Private f32_3* %825 = OpVariable Private 
                                         f32 %827 = OpConstant 3.674022E-40 
                                       f32_3 %828 = OpConstantComposite %827 %827 %827 
                                         f32 %840 = OpConstant 3.674022E-40 
                                       f32_3 %841 = OpConstantComposite %840 %840 %840 
                                             %847 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD3 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD0 = OpVariable Output 
                                             %861 = OpTypePointer Output %6 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Uniform f32* %761 = OpAccessChain %21 %760 %133 
                                         f32 %762 = OpLoad %761 
                                Uniform f32* %763 = OpAccessChain %21 %760 %70 
                                         f32 %764 = OpLoad %763 
                                Uniform f32* %765 = OpAccessChain %21 %760 %371 
                                         f32 %766 = OpLoad %765 
                                       f32_3 %767 = OpCompositeConstruct %762 %764 %766 
                                Uniform f32* %768 = OpAccessChain %21 %760 %133 
                                         f32 %769 = OpLoad %768 
                                Uniform f32* %770 = OpAccessChain %21 %760 %70 
                                         f32 %771 = OpLoad %770 
                                Uniform f32* %772 = OpAccessChain %21 %760 %371 
                                         f32 %773 = OpLoad %772 
                                       f32_3 %774 = OpCompositeConstruct %769 %771 %773 
                                       f32_3 %775 = OpFMul %767 %774 
                                                      OpStore %127 %775 
                                       f32_3 %776 = OpLoad %127 
                                       f32_3 %777 = OpLoad %561 
                                       f32_3 %778 = OpFMul %776 %777 
                                       f32_3 %779 = OpLoad %559 
                                       f32_3 %780 = OpFAdd %778 %779 
                                                      OpStore %127 %780 
                                       f32_3 %781 = OpLoad %127 
                                Uniform f32* %783 = OpAccessChain %21 %782 
                                         f32 %784 = OpLoad %783 
                                       f32_3 %785 = OpCompositeConstruct %784 %784 %784 
                                       f32_3 %786 = OpFMul %781 %785 
                                                      OpStore %127 %786 
                              Uniform f32_4* %788 = OpAccessChain %21 %28 
                                       f32_4 %789 = OpLoad %788 
                                       f32_3 %790 = OpVectorShuffle %789 %789 0 1 2 
                                       f32_4 %791 = OpLoad %50 
                                       f32_3 %792 = OpVectorShuffle %791 %791 0 1 2 
                                       f32_3 %793 = OpFNegate %792 
                                         f32 %794 = OpDot %790 %793 
                                                      OpStore %787 %794 
                                         f32 %795 = OpLoad %787 
                                         f32 %796 = OpLoad %787 
                                         f32 %797 = OpFMul %795 %796 
                                                      OpStore %787 %797 
                                         f32 %798 = OpLoad %787 
                                         f32 %800 = OpFMul %798 %799 
                                         f32 %801 = OpFAdd %800 %799 
                                                      OpStore %787 %801 
                                         f32 %803 = OpLoad %787 
                                       f32_3 %804 = OpCompositeConstruct %803 %803 %803 
                                       f32_3 %805 = OpLoad %559 
                                       f32_3 %806 = OpFMul %804 %805 
                                                      OpStore %802 %806 
                                       f32_3 %807 = OpLoad %802 
                                Uniform f32* %808 = OpAccessChain %21 %782 
                                         f32 %809 = OpLoad %808 
                                       f32_3 %810 = OpCompositeConstruct %809 %809 %809 
                                       f32_3 %811 = OpFMul %807 %810 
                                                      OpStore %802 %811 
                              Uniform f32_4* %812 = OpAccessChain %21 %46 
                                       f32_4 %813 = OpLoad %812 
                                       f32_3 %814 = OpVectorShuffle %813 %813 0 1 2 
                              Uniform f32_4* %815 = OpAccessChain %21 %46 
                                       f32_4 %816 = OpLoad %815 
                                       f32_3 %817 = OpVectorShuffle %816 %816 0 1 2 
                                         f32 %818 = OpDot %814 %817 
                                                      OpStore %787 %818 
                                         f32 %819 = OpLoad %787 
                                         f32 %820 = OpExtInst %1 31 %819 
                                                      OpStore %787 %820 
                                         f32 %821 = OpLoad %787 
                                         f32 %822 = OpExtInst %1 40 %821 %330 
                                                      OpStore %787 %822 
                                         f32 %823 = OpLoad %787 
                                         f32 %824 = OpExtInst %1 37 %823 %89 
                                                      OpStore %787 %824 
                                       f32_3 %826 = OpLoad %561 
                                       f32_3 %829 = OpFMul %826 %828 
                                                      OpStore %825 %829 
                                       f32_3 %830 = OpLoad %825 
                                       f32_3 %831 = OpCompositeConstruct %174 %174 %174 
                                       f32_3 %832 = OpCompositeConstruct %89 %89 %89 
                                       f32_3 %833 = OpExtInst %1 43 %830 %831 %832 
                                                      OpStore %825 %833 
                                       f32_3 %834 = OpLoad %825 
                              Uniform f32_4* %835 = OpAccessChain %21 %46 
                                       f32_4 %836 = OpLoad %835 
                                       f32_3 %837 = OpVectorShuffle %836 %836 0 1 2 
                                       f32_3 %838 = OpFMul %834 %837 
                                                      OpStore %825 %838 
                                       f32_3 %839 = OpLoad %825 
                                       f32_3 %842 = OpFMul %839 %841 
                                                      OpStore %825 %842 
                                       f32_3 %843 = OpLoad %825 
                                         f32 %844 = OpLoad %787 
                                       f32_3 %845 = OpCompositeConstruct %844 %844 %844 
                                       f32_3 %846 = OpFDiv %843 %845 
                                                      OpStore %825 %846 
                                       f32_3 %849 = OpLoad %127 
                                       f32_3 %850 = OpExtInst %1 31 %849 
                                                      OpStore vs_TEXCOORD1 %850 
                                       f32_3 %852 = OpLoad %802 
                                       f32_3 %853 = OpExtInst %1 31 %852 
                                                      OpStore vs_TEXCOORD2 %853 
                                       f32_3 %855 = OpLoad %825 
                                       f32_3 %856 = OpExtInst %1 31 %855 
                                                      OpStore vs_TEXCOORD3 %856 
                                       f32_4 %858 = OpLoad %50 
                                       f32_3 %859 = OpVectorShuffle %858 %858 0 1 2 
                                       f32_3 %860 = OpFNegate %859 
                                                      OpStore vs_TEXCOORD0 %860 
                                 Output f32* %862 = OpAccessChain %74 %28 %70 
                                         f32 %863 = OpLoad %862 
                                         f32 %864 = OpFNegate %863 
                                 Output f32* %865 = OpAccessChain %74 %28 %70 
                                                      OpStore %865 %864 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 135
; Schema: 0
                                      OpCapability Shader 
                               %1 = OpExtInstImport "GLSL.std.450" 
                                      OpMemoryModel Logical GLSL450 
                                      OpEntryPoint Fragment %4 "main" %11 %93 %95 %105 %119 
                                      OpExecutionMode %4 OriginUpperLeft 
                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                      OpDecorate %9 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD0 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD0 Location 11 
                                      OpDecorate %12 RelaxedPrecision 
                                      OpMemberDecorate %14 0 Offset 14 
                                      OpMemberDecorate %14 1 RelaxedPrecision 
                                      OpMemberDecorate %14 1 Offset 14 
                                      OpDecorate %14 Block 
                                      OpDecorate %16 DescriptorSet 16 
                                      OpDecorate %16 Binding 16 
                                      OpDecorate %24 RelaxedPrecision 
                                      OpDecorate %25 RelaxedPrecision 
                                      OpDecorate %26 RelaxedPrecision 
                                      OpDecorate %32 RelaxedPrecision 
                                      OpDecorate %33 RelaxedPrecision 
                                      OpDecorate %35 RelaxedPrecision 
                                      OpDecorate %40 RelaxedPrecision 
                                      OpDecorate %41 RelaxedPrecision 
                                      OpDecorate %44 RelaxedPrecision 
                                      OpDecorate %46 RelaxedPrecision 
                                      OpDecorate %47 RelaxedPrecision 
                                      OpDecorate %50 RelaxedPrecision 
                                      OpDecorate %52 RelaxedPrecision 
                                      OpDecorate %55 RelaxedPrecision 
                                      OpDecorate %57 RelaxedPrecision 
                                      OpDecorate %59 RelaxedPrecision 
                                      OpDecorate %62 RelaxedPrecision 
                                      OpDecorate %64 RelaxedPrecision 
                                      OpDecorate %65 RelaxedPrecision 
                                      OpDecorate %68 RelaxedPrecision 
                                      OpDecorate %69 RelaxedPrecision 
                                      OpDecorate %71 RelaxedPrecision 
                                      OpDecorate %72 RelaxedPrecision 
                                      OpDecorate %73 RelaxedPrecision 
                                      OpDecorate %76 RelaxedPrecision 
                                      OpDecorate %78 RelaxedPrecision 
                                      OpDecorate %79 RelaxedPrecision 
                                      OpDecorate %84 RelaxedPrecision 
                                      OpDecorate %86 RelaxedPrecision 
                                      OpDecorate %89 RelaxedPrecision 
                                      OpDecorate %90 RelaxedPrecision 
                                      OpDecorate %92 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD1 Location 93 
                                      OpDecorate %94 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD2 Location 95 
                                      OpDecorate %96 RelaxedPrecision 
                                      OpDecorate %97 RelaxedPrecision 
                                      OpDecorate %98 RelaxedPrecision 
                                      OpDecorate %99 RelaxedPrecision 
                                      OpDecorate %100 RelaxedPrecision 
                                      OpDecorate %101 RelaxedPrecision 
                                      OpDecorate %102 RelaxedPrecision 
                                      OpDecorate %103 RelaxedPrecision 
                                      OpDecorate %104 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                      OpDecorate vs_TEXCOORD3 Location 105 
                                      OpDecorate %106 RelaxedPrecision 
                                      OpDecorate %107 RelaxedPrecision 
                                      OpDecorate %108 RelaxedPrecision 
                                      OpDecorate %109 RelaxedPrecision 
                                      OpDecorate %110 RelaxedPrecision 
                                      OpDecorate %111 RelaxedPrecision 
                                      OpDecorate %116 RelaxedPrecision 
                                      OpDecorate %119 RelaxedPrecision 
                                      OpDecorate %119 Location 119 
                                      OpDecorate %125 RelaxedPrecision 
                                      OpDecorate %127 RelaxedPrecision 
                                      OpDecorate %128 RelaxedPrecision 
                               %2 = OpTypeVoid 
                               %3 = OpTypeFunction %2 
                               %6 = OpTypeFloat 32 
                               %7 = OpTypeVector %6 3 
                               %8 = OpTypePointer Private %7 
                Private f32_3* %9 = OpVariable Private 
                              %10 = OpTypePointer Input %7 
        Input f32_3* vs_TEXCOORD0 = OpVariable Input 
                              %13 = OpTypeVector %6 4 
                              %14 = OpTypeStruct %13 %6 
                              %15 = OpTypePointer Uniform %14 
Uniform struct {f32_4; f32;}* %16 = OpVariable Uniform 
                              %17 = OpTypeInt 32 1 
                          i32 %18 = OpConstant 0 
                              %19 = OpTypePointer Uniform %13 
                              %27 = OpTypeInt 32 0 
                          u32 %28 = OpConstant 0 
                              %29 = OpTypePointer Private %6 
               Private f32_3* %35 = OpVariable Private 
                          f32 %36 = OpConstant 3.674022E-40 
                          i32 %37 = OpConstant 1 
                              %38 = OpTypePointer Uniform %6 
                          f32 %51 = OpConstant 3.674022E-40 
                          f32 %56 = OpConstant 3.674022E-40 
                          f32 %58 = OpConstant 3.674022E-40 
                          u32 %81 = OpConstant 1 
                              %82 = OpTypePointer Input %6 
                          f32 %85 = OpConstant 3.674022E-40 
               Private f32_3* %92 = OpVariable Private 
        Input f32_3* vs_TEXCOORD1 = OpVariable Input 
        Input f32_3* vs_TEXCOORD2 = OpVariable Input 
        Input f32_3* vs_TEXCOORD3 = OpVariable Input 
                             %112 = OpTypeBool 
                             %113 = OpTypePointer Private %112 
               Private bool* %114 = OpVariable Private 
                             %118 = OpTypePointer Output %13 
               Output f32_4* %119 = OpVariable Output 
                             %121 = OpTypePointer Function %7 
                         u32 %131 = OpConstant 3 
                             %132 = OpTypePointer Output %6 
                          void %4 = OpFunction None %3 
                               %5 = OpLabel 
             Function f32_3* %122 = OpVariable Function 
                        f32_3 %12 = OpLoad vs_TEXCOORD0 
               Uniform f32_4* %20 = OpAccessChain %16 %18 
                        f32_4 %21 = OpLoad %20 
                        f32_3 %22 = OpVectorShuffle %21 %21 0 1 2 
                        f32_3 %23 = OpFAdd %12 %22 
                                      OpStore %9 %23 
                        f32_3 %24 = OpLoad %9 
                        f32_3 %25 = OpLoad %9 
                          f32 %26 = OpDot %24 %25 
                 Private f32* %30 = OpAccessChain %9 %28 
                                      OpStore %30 %26 
                 Private f32* %31 = OpAccessChain %9 %28 
                          f32 %32 = OpLoad %31 
                          f32 %33 = OpExtInst %1 31 %32 
                 Private f32* %34 = OpAccessChain %9 %28 
                                      OpStore %34 %33 
                 Uniform f32* %39 = OpAccessChain %16 %37 
                          f32 %40 = OpLoad %39 
                          f32 %41 = OpFDiv %36 %40 
                 Private f32* %42 = OpAccessChain %35 %28 
                                      OpStore %42 %41 
                 Private f32* %43 = OpAccessChain %35 %28 
                          f32 %44 = OpLoad %43 
                 Private f32* %45 = OpAccessChain %9 %28 
                          f32 %46 = OpLoad %45 
                          f32 %47 = OpFMul %44 %46 
                 Private f32* %48 = OpAccessChain %9 %28 
                                      OpStore %48 %47 
                 Private f32* %49 = OpAccessChain %9 %28 
                          f32 %50 = OpLoad %49 
                          f32 %52 = OpExtInst %1 43 %50 %51 %36 
                 Private f32* %53 = OpAccessChain %9 %28 
                                      OpStore %53 %52 
                 Private f32* %54 = OpAccessChain %9 %28 
                          f32 %55 = OpLoad %54 
                          f32 %57 = OpFMul %55 %56 
                          f32 %59 = OpFAdd %57 %58 
                 Private f32* %60 = OpAccessChain %35 %28 
                                      OpStore %60 %59 
                 Private f32* %61 = OpAccessChain %9 %28 
                          f32 %62 = OpLoad %61 
                 Private f32* %63 = OpAccessChain %9 %28 
                          f32 %64 = OpLoad %63 
                          f32 %65 = OpFMul %62 %64 
                 Private f32* %66 = OpAccessChain %9 %28 
                                      OpStore %66 %65 
                 Private f32* %67 = OpAccessChain %35 %28 
                          f32 %68 = OpLoad %67 
                          f32 %69 = OpFNegate %68 
                 Private f32* %70 = OpAccessChain %9 %28 
                          f32 %71 = OpLoad %70 
                          f32 %72 = OpFMul %69 %71 
                          f32 %73 = OpFAdd %72 %36 
                 Private f32* %74 = OpAccessChain %9 %28 
                                      OpStore %74 %73 
                 Private f32* %75 = OpAccessChain %9 %28 
                          f32 %76 = OpLoad %75 
                 Private f32* %77 = OpAccessChain %9 %28 
                          f32 %78 = OpLoad %77 
                          f32 %79 = OpFMul %76 %78 
                 Private f32* %80 = OpAccessChain %9 %28 
                                      OpStore %80 %79 
                   Input f32* %83 = OpAccessChain vs_TEXCOORD0 %81 
                          f32 %84 = OpLoad %83 
                          f32 %86 = OpFMul %84 %85 
                 Private f32* %87 = OpAccessChain %35 %28 
                                      OpStore %87 %86 
                 Private f32* %88 = OpAccessChain %35 %28 
                          f32 %89 = OpLoad %88 
                          f32 %90 = OpExtInst %1 43 %89 %51 %36 
                 Private f32* %91 = OpAccessChain %35 %28 
                                      OpStore %91 %90 
                        f32_3 %94 = OpLoad vs_TEXCOORD1 
                        f32_3 %96 = OpLoad vs_TEXCOORD2 
                        f32_3 %97 = OpFNegate %96 
                        f32_3 %98 = OpFAdd %94 %97 
                                      OpStore %92 %98 
                        f32_3 %99 = OpLoad %35 
                       f32_3 %100 = OpVectorShuffle %99 %99 0 0 0 
                       f32_3 %101 = OpLoad %92 
                       f32_3 %102 = OpFMul %100 %101 
                       f32_3 %103 = OpLoad vs_TEXCOORD2 
                       f32_3 %104 = OpFAdd %102 %103 
                                      OpStore %35 %104 
                       f32_3 %106 = OpLoad vs_TEXCOORD3 
                       f32_3 %107 = OpLoad %9 
                       f32_3 %108 = OpVectorShuffle %107 %107 0 0 0 
                       f32_3 %109 = OpFMul %106 %108 
                       f32_3 %110 = OpLoad %35 
                       f32_3 %111 = OpFAdd %109 %110 
                                      OpStore %92 %111 
                  Input f32* %115 = OpAccessChain vs_TEXCOORD0 %81 
                         f32 %116 = OpLoad %115 
                        bool %117 = OpFOrdLessThan %116 %51 
                                      OpStore %114 %117 
                        bool %120 = OpLoad %114 
                                      OpSelectionMerge %124 None 
                                      OpBranchConditional %120 %123 %126 
                             %123 = OpLabel 
                       f32_3 %125 = OpLoad %92 
                                      OpStore %122 %125 
                                      OpBranch %124 
                             %126 = OpLabel 
                       f32_3 %127 = OpLoad %35 
                                      OpStore %122 %127 
                                      OpBranch %124 
                             %124 = OpLabel 
                       f32_3 %128 = OpLoad %122 
                       f32_4 %129 = OpLoad %119 
                       f32_4 %130 = OpVectorShuffle %129 %128 4 5 6 3 
                                      OpStore %119 %130 
                 Output f32* %133 = OpAccessChain %119 %131 
                                      OpStore %133 %36 
                                      OpReturn
                                      OpFunctionEnd
"
}
SubProgram "gles3 hw_tier00 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump vec4 _LightColor0;
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out highp vec3 vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
out mediump vec3 vs_TEXCOORD3;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
mediump vec3 u_xlat16_8;
float u_xlat9;
vec3 u_xlat14;
float u_xlat18;
float u_xlat23;
float u_xlat27;
float u_xlat28;
float u_xlat29;
mediump float u_xlat16_30;
float u_xlat31;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat27 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat27 = inversesqrt(u_xlat27);
    u_xlat2.xyz = vec3(u_xlat27) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat27 + u_xlat0.x;
        u_xlat9 = (-u_xlat0.y) * u_xlat27 + 1.0;
        u_xlat18 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat18 = u_xlat9 * u_xlat18 + 3.82999992;
        u_xlat18 = u_xlat9 * u_xlat18 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat18 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat0.y = exp2(u_xlat9);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat27 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat27 = sqrt(u_xlat27);
        u_xlat28 = (-u_xlat27) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat27;
        u_xlat31 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat27 = u_xlat31 / u_xlat27;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat31 = u_xlat29 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat29 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat29 * u_xlat31 + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat31 + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat27 = (-u_xlat27) + 1.0;
        u_xlat31 = u_xlat27 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat27 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat27 * u_xlat31 + 0.458999991;
        u_xlat27 = u_xlat27 * u_xlat31 + -0.00286999997;
        u_xlat27 = u_xlat27 * 1.44269502;
        u_xlat27 = exp2(u_xlat27);
        u_xlat27 = u_xlat27 * 0.25;
        u_xlat27 = u_xlat29 * 0.25 + (-u_xlat27);
        u_xlat27 = u_xlat28 * u_xlat27 + u_xlat0.y;
        u_xlat27 = max(u_xlat27, 0.0);
        u_xlat27 = min(u_xlat27, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat27)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat27 = u_xlat0.z * u_xlat28;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat28 = (-u_xlat0.x) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat4.x = u_xlat29 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat4.x + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat29 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat28 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat28;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat27) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat29 = min(u_xlat2.y, -0.00100000005);
        u_xlat29 = -9.99999975e-05 / u_xlat29;
        u_xlat5.xyz = vec3(u_xlat29) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat23 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.y * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.y * u_xlat23 + 0.458999991;
        u_xlat14.x = u_xlat5.y * u_xlat23 + -0.00286999997;
        u_xlat14.x = u_xlat14.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat14.x);
        u_xlat23 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.x * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.x * u_xlat23 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat23 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat14.xz = vec2(u_xlat29) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat14.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat29 = sqrt(u_xlat29);
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat29 = u_xlat29 * 230.831207;
        u_xlat29 = exp2(u_xlat29);
        u_xlat5.x = u_xlat29 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat29 = u_xlat14.z * u_xlat29;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat29);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_30 = u_xlat16_30 * u_xlat16_30;
    u_xlat16_30 = u_xlat16_30 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_30) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_LightColor0.xyz, _LightColor0.xyz);
    u_xlat16_30 = sqrt(u_xlat16_30);
    u_xlat16_30 = max(u_xlat16_30, 0.25);
    u_xlat16_30 = min(u_xlat16_30, 1.0);
    u_xlat16_0.xyz = u_xlat16_0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0.xyz = min(max(u_xlat16_0.xyz, 0.0), 1.0);
#else
    u_xlat16_0.xyz = clamp(u_xlat16_0.xyz, 0.0, 1.0);
#endif
    u_xlat16_8.xyz = u_xlat16_0.xyz * _LightColor0.xyz;
    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(15.0, 15.0, 15.0);
    u_xlat16_8.xyz = u_xlat16_8.xyz / vec3(u_xlat16_30);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD3.xyz = sqrt(u_xlat16_8.xyz);
    vs_TEXCOORD0.xyz = (-u_xlat2.xyz);
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	mediump float _SunSize;
uniform 	mediump float _SunSizeConvergence;
in highp vec3 vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
in mediump vec3 vs_TEXCOORD3;
layout(location = 0) out mediump vec4 SV_Target0;
mediump vec2 u_xlat16_0;
vec3 u_xlat1;
bool u_xlatb1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump float u_xlat16_6;
void main()
{
    u_xlat16_0.x = log2(_SunSize);
    u_xlat16_0.x = u_xlat16_0.x * 0.649999976;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat1.x = dot(vs_TEXCOORD0.xyz, vs_TEXCOORD0.xyz);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat1.xyz = u_xlat1.xxx * vs_TEXCOORD0.xyz;
    u_xlat16_3.x = dot(_WorldSpaceLightPos0.xyz, (-u_xlat1.xyz));
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
    u_xlat16_3.x = log2(u_xlat16_3.x);
    u_xlat16_3.x = u_xlat16_3.x * _SunSizeConvergence;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_6 = (-u_xlat16_3.x) * 1.98000002 + 1.98010004;
    u_xlat16_0.y = u_xlat16_3.x * u_xlat16_3.x + 1.0;
    u_xlat16_0.xy = u_xlat16_0.xy * vec2(10.0, 0.0100164423);
    u_xlat16_6 = log2(u_xlat16_6);
    u_xlat16_0.x = u_xlat16_6 * u_xlat16_0.x;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_0.x = max(u_xlat16_0.x, 9.99999975e-05);
    u_xlat16_0.x = u_xlat16_0.y / u_xlat16_0.x;
    u_xlat16_0.x = log2(u_xlat16_0.x);
    u_xlat16_0.x = u_xlat16_0.x * 0.454544991;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_3.x = u_xlat1.y * 50.0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
#ifdef UNITY_ADRENO_ES3
    u_xlatb1 = !!(u_xlat1.y<0.0);
#else
    u_xlatb1 = u_xlat1.y<0.0;
#endif
    u_xlat16_2.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat16_3.xyz = u_xlat16_3.xxx * u_xlat16_2.xyz + vs_TEXCOORD2.xyz;
    u_xlat16_2.xyz = vs_TEXCOORD3.xyz * u_xlat16_0.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = (bool(u_xlatb1)) ? u_xlat16_2.xyz : u_xlat16_3.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier01 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump vec4 _LightColor0;
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out highp vec3 vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
out mediump vec3 vs_TEXCOORD3;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
mediump vec3 u_xlat16_8;
float u_xlat9;
vec3 u_xlat14;
float u_xlat18;
float u_xlat23;
float u_xlat27;
float u_xlat28;
float u_xlat29;
mediump float u_xlat16_30;
float u_xlat31;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat27 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat27 = inversesqrt(u_xlat27);
    u_xlat2.xyz = vec3(u_xlat27) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat27 + u_xlat0.x;
        u_xlat9 = (-u_xlat0.y) * u_xlat27 + 1.0;
        u_xlat18 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat18 = u_xlat9 * u_xlat18 + 3.82999992;
        u_xlat18 = u_xlat9 * u_xlat18 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat18 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat0.y = exp2(u_xlat9);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat27 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat27 = sqrt(u_xlat27);
        u_xlat28 = (-u_xlat27) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat27;
        u_xlat31 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat27 = u_xlat31 / u_xlat27;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat31 = u_xlat29 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat29 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat29 * u_xlat31 + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat31 + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat27 = (-u_xlat27) + 1.0;
        u_xlat31 = u_xlat27 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat27 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat27 * u_xlat31 + 0.458999991;
        u_xlat27 = u_xlat27 * u_xlat31 + -0.00286999997;
        u_xlat27 = u_xlat27 * 1.44269502;
        u_xlat27 = exp2(u_xlat27);
        u_xlat27 = u_xlat27 * 0.25;
        u_xlat27 = u_xlat29 * 0.25 + (-u_xlat27);
        u_xlat27 = u_xlat28 * u_xlat27 + u_xlat0.y;
        u_xlat27 = max(u_xlat27, 0.0);
        u_xlat27 = min(u_xlat27, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat27)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat27 = u_xlat0.z * u_xlat28;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat28 = (-u_xlat0.x) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat4.x = u_xlat29 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat4.x + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat29 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat28 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat28;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat27) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat29 = min(u_xlat2.y, -0.00100000005);
        u_xlat29 = -9.99999975e-05 / u_xlat29;
        u_xlat5.xyz = vec3(u_xlat29) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat23 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.y * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.y * u_xlat23 + 0.458999991;
        u_xlat14.x = u_xlat5.y * u_xlat23 + -0.00286999997;
        u_xlat14.x = u_xlat14.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat14.x);
        u_xlat23 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.x * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.x * u_xlat23 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat23 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat14.xz = vec2(u_xlat29) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat14.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat29 = sqrt(u_xlat29);
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat29 = u_xlat29 * 230.831207;
        u_xlat29 = exp2(u_xlat29);
        u_xlat5.x = u_xlat29 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat29 = u_xlat14.z * u_xlat29;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat29);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_30 = u_xlat16_30 * u_xlat16_30;
    u_xlat16_30 = u_xlat16_30 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_30) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_LightColor0.xyz, _LightColor0.xyz);
    u_xlat16_30 = sqrt(u_xlat16_30);
    u_xlat16_30 = max(u_xlat16_30, 0.25);
    u_xlat16_30 = min(u_xlat16_30, 1.0);
    u_xlat16_0.xyz = u_xlat16_0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0.xyz = min(max(u_xlat16_0.xyz, 0.0), 1.0);
#else
    u_xlat16_0.xyz = clamp(u_xlat16_0.xyz, 0.0, 1.0);
#endif
    u_xlat16_8.xyz = u_xlat16_0.xyz * _LightColor0.xyz;
    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(15.0, 15.0, 15.0);
    u_xlat16_8.xyz = u_xlat16_8.xyz / vec3(u_xlat16_30);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD3.xyz = sqrt(u_xlat16_8.xyz);
    vs_TEXCOORD0.xyz = (-u_xlat2.xyz);
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	mediump float _SunSize;
uniform 	mediump float _SunSizeConvergence;
in highp vec3 vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
in mediump vec3 vs_TEXCOORD3;
layout(location = 0) out mediump vec4 SV_Target0;
mediump vec2 u_xlat16_0;
vec3 u_xlat1;
bool u_xlatb1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump float u_xlat16_6;
void main()
{
    u_xlat16_0.x = log2(_SunSize);
    u_xlat16_0.x = u_xlat16_0.x * 0.649999976;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat1.x = dot(vs_TEXCOORD0.xyz, vs_TEXCOORD0.xyz);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat1.xyz = u_xlat1.xxx * vs_TEXCOORD0.xyz;
    u_xlat16_3.x = dot(_WorldSpaceLightPos0.xyz, (-u_xlat1.xyz));
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
    u_xlat16_3.x = log2(u_xlat16_3.x);
    u_xlat16_3.x = u_xlat16_3.x * _SunSizeConvergence;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_6 = (-u_xlat16_3.x) * 1.98000002 + 1.98010004;
    u_xlat16_0.y = u_xlat16_3.x * u_xlat16_3.x + 1.0;
    u_xlat16_0.xy = u_xlat16_0.xy * vec2(10.0, 0.0100164423);
    u_xlat16_6 = log2(u_xlat16_6);
    u_xlat16_0.x = u_xlat16_6 * u_xlat16_0.x;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_0.x = max(u_xlat16_0.x, 9.99999975e-05);
    u_xlat16_0.x = u_xlat16_0.y / u_xlat16_0.x;
    u_xlat16_0.x = log2(u_xlat16_0.x);
    u_xlat16_0.x = u_xlat16_0.x * 0.454544991;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_3.x = u_xlat1.y * 50.0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
#ifdef UNITY_ADRENO_ES3
    u_xlatb1 = !!(u_xlat1.y<0.0);
#else
    u_xlatb1 = u_xlat1.y<0.0;
#endif
    u_xlat16_2.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat16_3.xyz = u_xlat16_3.xxx * u_xlat16_2.xyz + vs_TEXCOORD2.xyz;
    u_xlat16_2.xyz = vs_TEXCOORD3.xyz * u_xlat16_0.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = (bool(u_xlatb1)) ? u_xlat16_2.xyz : u_xlat16_3.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "gles3 hw_tier02 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
"#ifdef VERTEX
#version 300 es

#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
uniform 	mediump vec4 _LightColor0;
uniform 	mediump float _Exposure;
uniform 	mediump vec3 _GroundColor;
uniform 	mediump vec3 _SkyTint;
uniform 	mediump float _AtmosphereThickness;
in highp vec4 in_POSITION0;
out highp vec3 vs_TEXCOORD0;
out mediump vec3 vs_TEXCOORD1;
out mediump vec3 vs_TEXCOORD2;
out mediump vec3 vs_TEXCOORD3;
vec4 u_xlat0;
mediump vec3 u_xlat16_0;
bool u_xlatb0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec3 u_xlat16_3;
vec3 u_xlat4;
mediump vec3 u_xlat16_4;
vec4 u_xlat5;
vec3 u_xlat6;
mediump vec3 u_xlat16_7;
mediump vec3 u_xlat16_8;
float u_xlat9;
vec3 u_xlat14;
float u_xlat18;
float u_xlat23;
float u_xlat27;
float u_xlat28;
float u_xlat29;
mediump float u_xlat16_30;
float u_xlat31;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat1 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
    u_xlat2 = u_xlat1.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat1.xxxx + u_xlat2;
    u_xlat2 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat1.zzzz + u_xlat2;
    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat1.wwww + u_xlat2;
    u_xlat1.xyz = (-_SkyTint.xyz) + vec3(1.0, 1.0, 1.0);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.300000012, 0.300000042, 0.300000012) + vec3(0.5, 0.419999987, 0.324999988);
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = u_xlat1.xyz * u_xlat1.xyz;
    u_xlat1.xyz = vec3(1.0, 1.0, 1.0) / u_xlat1.xyz;
    u_xlat16_3.x = log2(_AtmosphereThickness);
    u_xlat16_3.x = u_xlat16_3.x * 2.5;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_3.xy = u_xlat16_3.xx * vec2(0.049999997, 0.0314159282);
    u_xlat27 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat27 = inversesqrt(u_xlat27);
    u_xlat2.xyz = vec3(u_xlat27) * u_xlat0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlatb0 = !!(u_xlat2.y>=0.0);
#else
    u_xlatb0 = u_xlat2.y>=0.0;
#endif
    if(u_xlatb0){
        u_xlat0.x = u_xlat2.y * u_xlat2.y + 0.0506249666;
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat0.x = (-u_xlat0.y) * u_xlat27 + u_xlat0.x;
        u_xlat9 = (-u_xlat0.y) * u_xlat27 + 1.0;
        u_xlat18 = u_xlat9 * 5.25 + -6.80000019;
        u_xlat18 = u_xlat9 * u_xlat18 + 3.82999992;
        u_xlat18 = u_xlat9 * u_xlat18 + 0.458999991;
        u_xlat9 = u_xlat9 * u_xlat18 + -0.00286999997;
        u_xlat9 = u_xlat9 * 1.44269502;
        u_xlat0.y = exp2(u_xlat9);
        u_xlat0.xyz = u_xlat0.xyx * vec3(0.5, 0.246031836, 20.0);
        u_xlat4.xyz = u_xlat0.xxx * u_xlat2.xyz;
        u_xlat4.xyz = u_xlat4.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat27 = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat27 = sqrt(u_xlat27);
        u_xlat28 = (-u_xlat27) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat27;
        u_xlat31 = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat27 = u_xlat31 / u_xlat27;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat31 = u_xlat29 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat29 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat29 * u_xlat31 + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat31 + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat27 = (-u_xlat27) + 1.0;
        u_xlat31 = u_xlat27 * 5.25 + -6.80000019;
        u_xlat31 = u_xlat27 * u_xlat31 + 3.82999992;
        u_xlat31 = u_xlat27 * u_xlat31 + 0.458999991;
        u_xlat27 = u_xlat27 * u_xlat31 + -0.00286999997;
        u_xlat27 = u_xlat27 * 1.44269502;
        u_xlat27 = exp2(u_xlat27);
        u_xlat27 = u_xlat27 * 0.25;
        u_xlat27 = u_xlat29 * 0.25 + (-u_xlat27);
        u_xlat27 = u_xlat28 * u_xlat27 + u_xlat0.y;
        u_xlat27 = max(u_xlat27, 0.0);
        u_xlat27 = min(u_xlat27, 50.0);
        u_xlat5.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat6.xyz = (-vec3(u_xlat27)) * u_xlat5.xyz;
        u_xlat6.xyz = u_xlat6.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat6.xyz = exp2(u_xlat6.xyz);
        u_xlat27 = u_xlat0.z * u_xlat28;
        u_xlat4.xyz = u_xlat2.xyz * u_xlat0.xxx + u_xlat4.xyz;
        u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
        u_xlat0.x = sqrt(u_xlat0.x);
        u_xlat28 = (-u_xlat0.x) + 1.0;
        u_xlat28 = u_xlat28 * 230.831207;
        u_xlat28 = exp2(u_xlat28);
        u_xlat29 = dot(_WorldSpaceLightPos0.xyz, u_xlat4.xyz);
        u_xlat29 = u_xlat29 / u_xlat0.x;
        u_xlat4.x = dot(u_xlat2.xyz, u_xlat4.xyz);
        u_xlat0.x = u_xlat4.x / u_xlat0.x;
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat4.x = u_xlat29 * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat29 * u_xlat4.x + 0.458999991;
        u_xlat29 = u_xlat29 * u_xlat4.x + -0.00286999997;
        u_xlat29 = u_xlat29 * 1.44269502;
        u_xlat29 = exp2(u_xlat29);
        u_xlat0.x = (-u_xlat0.x) + 1.0;
        u_xlat4.x = u_xlat0.x * 5.25 + -6.80000019;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 3.82999992;
        u_xlat4.x = u_xlat0.x * u_xlat4.x + 0.458999991;
        u_xlat0.x = u_xlat0.x * u_xlat4.x + -0.00286999997;
        u_xlat0.x = u_xlat0.x * 1.44269502;
        u_xlat0.x = exp2(u_xlat0.x);
        u_xlat0.x = u_xlat0.x * 0.25;
        u_xlat0.x = u_xlat29 * 0.25 + (-u_xlat0.x);
        u_xlat0.x = u_xlat28 * u_xlat0.x + u_xlat0.y;
        u_xlat0.x = max(u_xlat0.x, 0.0);
        u_xlat0.x = min(u_xlat0.x, 50.0);
        u_xlat4.xyz = u_xlat5.xyz * (-u_xlat0.xxx);
        u_xlat4.xyz = u_xlat4.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat4.xyz = exp2(u_xlat4.xyz);
        u_xlat0.x = u_xlat0.z * u_xlat28;
        u_xlat0.xyz = u_xlat0.xxx * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat6.xyz * vec3(u_xlat27) + u_xlat0.xyz;
        u_xlat4.xyz = u_xlat1.xyz * u_xlat16_3.xxx;
        u_xlat4.xyz = u_xlat0.xyz * u_xlat4.xyz;
        u_xlat0.xyz = u_xlat0.xyz * vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    } else {
        u_xlat29 = min(u_xlat2.y, -0.00100000005);
        u_xlat29 = -9.99999975e-05 / u_xlat29;
        u_xlat5.xyz = vec3(u_xlat29) * u_xlat2.xyz + vec3(0.0, 1.00010002, 0.0);
        u_xlat5.w = dot((-u_xlat2.xyz), u_xlat5.xyz);
        u_xlat5.x = dot(_WorldSpaceLightPos0.xyz, u_xlat5.xyz);
        u_xlat5.xy = (-u_xlat5.xw) + vec2(1.0, 1.0);
        u_xlat23 = u_xlat5.y * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.y * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.y * u_xlat23 + 0.458999991;
        u_xlat14.x = u_xlat5.y * u_xlat23 + -0.00286999997;
        u_xlat14.x = u_xlat14.x * 1.44269502;
        u_xlat5.y = exp2(u_xlat14.x);
        u_xlat23 = u_xlat5.x * 5.25 + -6.80000019;
        u_xlat23 = u_xlat5.x * u_xlat23 + 3.82999992;
        u_xlat23 = u_xlat5.x * u_xlat23 + 0.458999991;
        u_xlat5.x = u_xlat5.x * u_xlat23 + -0.00286999997;
        u_xlat5.xyz = u_xlat5.xyy * vec3(1.44269502, 0.25, 0.249900013);
        u_xlat5.x = exp2(u_xlat5.x);
        u_xlat5.x = u_xlat5.x * 0.25 + u_xlat5.y;
        u_xlat14.xz = vec2(u_xlat29) * vec2(0.5, 20.0);
        u_xlat6.xyz = u_xlat2.xyz * u_xlat14.xxx;
        u_xlat6.xyz = u_xlat6.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.0, 1.00010002, 0.0);
        u_xlat29 = dot(u_xlat6.xyz, u_xlat6.xyz);
        u_xlat29 = sqrt(u_xlat29);
        u_xlat29 = (-u_xlat29) + 1.0;
        u_xlat29 = u_xlat29 * 230.831207;
        u_xlat29 = exp2(u_xlat29);
        u_xlat5.x = u_xlat29 * u_xlat5.x + (-u_xlat5.z);
        u_xlat5.x = max(u_xlat5.x, 0.0);
        u_xlat5.x = min(u_xlat5.x, 50.0);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.yyy + vec3(0.0125663709, 0.0125663709, 0.0125663709);
        u_xlat5.xyz = (-u_xlat5.xxx) * u_xlat6.xyz;
        u_xlat5.xyz = u_xlat5.xyz * vec3(1.44269502, 1.44269502, 1.44269502);
        u_xlat0.xyz = exp2(u_xlat5.xyz);
        u_xlat29 = u_xlat14.z * u_xlat29;
        u_xlat5.xyz = u_xlat0.xyz * vec3(u_xlat29);
        u_xlat6.xyz = u_xlat1.xyz * u_xlat16_3.xxx + vec3(0.0199999996, 0.0199999996, 0.0199999996);
        u_xlat4.xyz = u_xlat5.xyz * u_xlat6.xyz;
        u_xlat16_4.xyz = u_xlat4.xyz;
        u_xlat16_0.xyz = u_xlat0.xyz;
    }
    u_xlat16_3.xyz = vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z) * vec3(_GroundColor.x, _GroundColor.y, _GroundColor.z);
    u_xlat16_3.xyz = u_xlat16_3.xyz * u_xlat16_0.xyz + u_xlat16_4.xyz;
    u_xlat16_3.xyz = u_xlat16_3.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_WorldSpaceLightPos0.xyz, (-u_xlat2.xyz));
    u_xlat16_30 = u_xlat16_30 * u_xlat16_30;
    u_xlat16_30 = u_xlat16_30 * 0.75 + 0.75;
    u_xlat16_7.xyz = vec3(u_xlat16_30) * u_xlat16_4.xyz;
    u_xlat16_7.xyz = u_xlat16_7.xyz * vec3(_Exposure);
    u_xlat16_30 = dot(_LightColor0.xyz, _LightColor0.xyz);
    u_xlat16_30 = sqrt(u_xlat16_30);
    u_xlat16_30 = max(u_xlat16_30, 0.25);
    u_xlat16_30 = min(u_xlat16_30, 1.0);
    u_xlat16_0.xyz = u_xlat16_0.xyz;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_0.xyz = min(max(u_xlat16_0.xyz, 0.0), 1.0);
#else
    u_xlat16_0.xyz = clamp(u_xlat16_0.xyz, 0.0, 1.0);
#endif
    u_xlat16_8.xyz = u_xlat16_0.xyz * _LightColor0.xyz;
    u_xlat16_8.xyz = u_xlat16_8.xyz * vec3(15.0, 15.0, 15.0);
    u_xlat16_8.xyz = u_xlat16_8.xyz / vec3(u_xlat16_30);
    vs_TEXCOORD1.xyz = sqrt(u_xlat16_3.xyz);
    vs_TEXCOORD2.xyz = sqrt(u_xlat16_7.xyz);
    vs_TEXCOORD3.xyz = sqrt(u_xlat16_8.xyz);
    vs_TEXCOORD0.xyz = (-u_xlat2.xyz);
    return;
}

#endif
#ifdef FRAGMENT
#version 300 es

precision highp float;
precision highp int;
#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
#if HLSLCC_ENABLE_UNIFORM_BUFFERS
#define UNITY_UNIFORM
#else
#define UNITY_UNIFORM uniform
#endif
#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
#if UNITY_SUPPORTS_UNIFORM_LOCATION
#define UNITY_LOCATION(x) layout(location = x)
#define UNITY_BINDING(x) layout(binding = x, std140)
#else
#define UNITY_LOCATION(x)
#define UNITY_BINDING(x) layout(std140)
#endif
uniform 	vec4 _WorldSpaceLightPos0;
uniform 	mediump float _SunSize;
uniform 	mediump float _SunSizeConvergence;
in highp vec3 vs_TEXCOORD0;
in mediump vec3 vs_TEXCOORD1;
in mediump vec3 vs_TEXCOORD2;
in mediump vec3 vs_TEXCOORD3;
layout(location = 0) out mediump vec4 SV_Target0;
mediump vec2 u_xlat16_0;
vec3 u_xlat1;
bool u_xlatb1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump float u_xlat16_6;
void main()
{
    u_xlat16_0.x = log2(_SunSize);
    u_xlat16_0.x = u_xlat16_0.x * 0.649999976;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat1.x = dot(vs_TEXCOORD0.xyz, vs_TEXCOORD0.xyz);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat1.xyz = u_xlat1.xxx * vs_TEXCOORD0.xyz;
    u_xlat16_3.x = dot(_WorldSpaceLightPos0.xyz, (-u_xlat1.xyz));
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
    u_xlat16_3.x = log2(u_xlat16_3.x);
    u_xlat16_3.x = u_xlat16_3.x * _SunSizeConvergence;
    u_xlat16_3.x = exp2(u_xlat16_3.x);
    u_xlat16_6 = (-u_xlat16_3.x) * 1.98000002 + 1.98010004;
    u_xlat16_0.y = u_xlat16_3.x * u_xlat16_3.x + 1.0;
    u_xlat16_0.xy = u_xlat16_0.xy * vec2(10.0, 0.0100164423);
    u_xlat16_6 = log2(u_xlat16_6);
    u_xlat16_0.x = u_xlat16_6 * u_xlat16_0.x;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_0.x = max(u_xlat16_0.x, 9.99999975e-05);
    u_xlat16_0.x = u_xlat16_0.y / u_xlat16_0.x;
    u_xlat16_0.x = log2(u_xlat16_0.x);
    u_xlat16_0.x = u_xlat16_0.x * 0.454544991;
    u_xlat16_0.x = exp2(u_xlat16_0.x);
    u_xlat16_3.x = u_xlat1.y * 50.0;
#ifdef UNITY_ADRENO_ES3
    u_xlat16_3.x = min(max(u_xlat16_3.x, 0.0), 1.0);
#else
    u_xlat16_3.x = clamp(u_xlat16_3.x, 0.0, 1.0);
#endif
#ifdef UNITY_ADRENO_ES3
    u_xlatb1 = !!(u_xlat1.y<0.0);
#else
    u_xlatb1 = u_xlat1.y<0.0;
#endif
    u_xlat16_2.xyz = vs_TEXCOORD1.xyz + (-vs_TEXCOORD2.xyz);
    u_xlat16_3.xyz = u_xlat16_3.xxx * u_xlat16_2.xyz + vs_TEXCOORD2.xyz;
    u_xlat16_2.xyz = vs_TEXCOORD3.xyz * u_xlat16_0.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = (bool(u_xlatb1)) ? u_xlat16_2.xyz : u_xlat16_3.xyz;
    SV_Target0.w = 1.0;
    return;
}

#endif
"
}
SubProgram "vulkan hw_tier00 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 864
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %845 %848 %851 %854 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpMemberDecorate %19 7 RelaxedPrecision 
                                                      OpMemberDecorate %19 7 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %762 RelaxedPrecision 
                                                      OpDecorate %764 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %767 RelaxedPrecision 
                                                      OpDecorate %769 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %774 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %776 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %786 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %795 RelaxedPrecision 
                                                      OpDecorate %796 RelaxedPrecision 
                                                      OpDecorate %797 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %802 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %811 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate %816 RelaxedPrecision 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate %819 RelaxedPrecision 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate %822 RelaxedPrecision 
                                                      OpDecorate %823 RelaxedPrecision 
                                                      OpDecorate %824 RelaxedPrecision 
                                                      OpDecorate %825 RelaxedPrecision 
                                                      OpDecorate %826 RelaxedPrecision 
                                                      OpDecorate %827 RelaxedPrecision 
                                                      OpDecorate %828 RelaxedPrecision 
                                                      OpDecorate %829 RelaxedPrecision 
                                                      OpDecorate %830 RelaxedPrecision 
                                                      OpDecorate %831 RelaxedPrecision 
                                                      OpDecorate %833 RelaxedPrecision 
                                                      OpDecorate %834 RelaxedPrecision 
                                                      OpDecorate %835 RelaxedPrecision 
                                                      OpDecorate %836 RelaxedPrecision 
                                                      OpDecorate %839 RelaxedPrecision 
                                                      OpDecorate %840 RelaxedPrecision 
                                                      OpDecorate %841 RelaxedPrecision 
                                                      OpDecorate %842 RelaxedPrecision 
                                                      OpDecorate %843 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 845 
                                                      OpDecorate %846 RelaxedPrecision 
                                                      OpDecorate %847 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 848 
                                                      OpDecorate %849 RelaxedPrecision 
                                                      OpDecorate %850 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 Location 851 
                                                      OpDecorate %852 RelaxedPrecision 
                                                      OpDecorate %853 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 854 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %7 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32_4; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 6 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 7 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         i32 %760 = OpConstant 5 
                                         i32 %782 = OpConstant 4 
                                Private f32* %787 = OpVariable Private 
                                         f32 %799 = OpConstant 3.674022E-40 
                              Private f32_3* %802 = OpVariable Private 
                              Private f32_3* %830 = OpVariable Private 
                                         f32 %837 = OpConstant 3.674022E-40 
                                       f32_3 %838 = OpConstantComposite %837 %837 %837 
                                             %844 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD3 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD0 = OpVariable Output 
                                             %858 = OpTypePointer Output %6 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Uniform f32* %761 = OpAccessChain %21 %760 %133 
                                         f32 %762 = OpLoad %761 
                                Uniform f32* %763 = OpAccessChain %21 %760 %70 
                                         f32 %764 = OpLoad %763 
                                Uniform f32* %765 = OpAccessChain %21 %760 %371 
                                         f32 %766 = OpLoad %765 
                                       f32_3 %767 = OpCompositeConstruct %762 %764 %766 
                                Uniform f32* %768 = OpAccessChain %21 %760 %133 
                                         f32 %769 = OpLoad %768 
                                Uniform f32* %770 = OpAccessChain %21 %760 %70 
                                         f32 %771 = OpLoad %770 
                                Uniform f32* %772 = OpAccessChain %21 %760 %371 
                                         f32 %773 = OpLoad %772 
                                       f32_3 %774 = OpCompositeConstruct %769 %771 %773 
                                       f32_3 %775 = OpFMul %767 %774 
                                                      OpStore %127 %775 
                                       f32_3 %776 = OpLoad %127 
                                       f32_3 %777 = OpLoad %561 
                                       f32_3 %778 = OpFMul %776 %777 
                                       f32_3 %779 = OpLoad %559 
                                       f32_3 %780 = OpFAdd %778 %779 
                                                      OpStore %127 %780 
                                       f32_3 %781 = OpLoad %127 
                                Uniform f32* %783 = OpAccessChain %21 %782 
                                         f32 %784 = OpLoad %783 
                                       f32_3 %785 = OpCompositeConstruct %784 %784 %784 
                                       f32_3 %786 = OpFMul %781 %785 
                                                      OpStore %127 %786 
                              Uniform f32_4* %788 = OpAccessChain %21 %28 
                                       f32_4 %789 = OpLoad %788 
                                       f32_3 %790 = OpVectorShuffle %789 %789 0 1 2 
                                       f32_4 %791 = OpLoad %50 
                                       f32_3 %792 = OpVectorShuffle %791 %791 0 1 2 
                                       f32_3 %793 = OpFNegate %792 
                                         f32 %794 = OpDot %790 %793 
                                                      OpStore %787 %794 
                                         f32 %795 = OpLoad %787 
                                         f32 %796 = OpLoad %787 
                                         f32 %797 = OpFMul %795 %796 
                                                      OpStore %787 %797 
                                         f32 %798 = OpLoad %787 
                                         f32 %800 = OpFMul %798 %799 
                                         f32 %801 = OpFAdd %800 %799 
                                                      OpStore %787 %801 
                                         f32 %803 = OpLoad %787 
                                       f32_3 %804 = OpCompositeConstruct %803 %803 %803 
                                       f32_3 %805 = OpLoad %559 
                                       f32_3 %806 = OpFMul %804 %805 
                                                      OpStore %802 %806 
                                       f32_3 %807 = OpLoad %802 
                                Uniform f32* %808 = OpAccessChain %21 %782 
                                         f32 %809 = OpLoad %808 
                                       f32_3 %810 = OpCompositeConstruct %809 %809 %809 
                                       f32_3 %811 = OpFMul %807 %810 
                                                      OpStore %802 %811 
                              Uniform f32_4* %812 = OpAccessChain %21 %46 
                                       f32_4 %813 = OpLoad %812 
                                       f32_3 %814 = OpVectorShuffle %813 %813 0 1 2 
                              Uniform f32_4* %815 = OpAccessChain %21 %46 
                                       f32_4 %816 = OpLoad %815 
                                       f32_3 %817 = OpVectorShuffle %816 %816 0 1 2 
                                         f32 %818 = OpDot %814 %817 
                                                      OpStore %787 %818 
                                         f32 %819 = OpLoad %787 
                                         f32 %820 = OpExtInst %1 31 %819 
                                                      OpStore %787 %820 
                                         f32 %821 = OpLoad %787 
                                         f32 %822 = OpExtInst %1 40 %821 %330 
                                                      OpStore %787 %822 
                                         f32 %823 = OpLoad %787 
                                         f32 %824 = OpExtInst %1 37 %823 %89 
                                                      OpStore %787 %824 
                                       f32_3 %825 = OpLoad %561 
                                                      OpStore %561 %825 
                                       f32_3 %826 = OpLoad %561 
                                       f32_3 %827 = OpCompositeConstruct %174 %174 %174 
                                       f32_3 %828 = OpCompositeConstruct %89 %89 %89 
                                       f32_3 %829 = OpExtInst %1 43 %826 %827 %828 
                                                      OpStore %561 %829 
                                       f32_3 %831 = OpLoad %561 
                              Uniform f32_4* %832 = OpAccessChain %21 %46 
                                       f32_4 %833 = OpLoad %832 
                                       f32_3 %834 = OpVectorShuffle %833 %833 0 1 2 
                                       f32_3 %835 = OpFMul %831 %834 
                                                      OpStore %830 %835 
                                       f32_3 %836 = OpLoad %830 
                                       f32_3 %839 = OpFMul %836 %838 
                                                      OpStore %830 %839 
                                       f32_3 %840 = OpLoad %830 
                                         f32 %841 = OpLoad %787 
                                       f32_3 %842 = OpCompositeConstruct %841 %841 %841 
                                       f32_3 %843 = OpFDiv %840 %842 
                                                      OpStore %830 %843 
                                       f32_3 %846 = OpLoad %127 
                                       f32_3 %847 = OpExtInst %1 31 %846 
                                                      OpStore vs_TEXCOORD1 %847 
                                       f32_3 %849 = OpLoad %802 
                                       f32_3 %850 = OpExtInst %1 31 %849 
                                                      OpStore vs_TEXCOORD2 %850 
                                       f32_3 %852 = OpLoad %830 
                                       f32_3 %853 = OpExtInst %1 31 %852 
                                                      OpStore vs_TEXCOORD3 %853 
                                       f32_4 %855 = OpLoad %50 
                                       f32_3 %856 = OpVectorShuffle %855 %855 0 1 2 
                                       f32_3 %857 = OpFNegate %856 
                                                      OpStore vs_TEXCOORD0 %857 
                                 Output f32* %859 = OpAccessChain %74 %28 %70 
                                         f32 %860 = OpLoad %859 
                                         f32 %861 = OpFNegate %860 
                                 Output f32* %862 = OpAccessChain %74 %28 %70 
                                                      OpStore %862 %861 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 189
; Schema: 0
                                           OpCapability Shader 
                                    %1 = OpExtInstImport "GLSL.std.450" 
                                           OpMemoryModel Logical GLSL450 
                                           OpEntryPoint Fragment %4 "main" %37 %153 %155 %165 %173 
                                           OpExecutionMode %4 OriginUpperLeft 
                                           OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                           OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                           OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                           OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                           OpDecorate %9 RelaxedPrecision 
                                           OpMemberDecorate %11 0 Offset 11 
                                           OpMemberDecorate %11 1 RelaxedPrecision 
                                           OpMemberDecorate %11 1 Offset 11 
                                           OpMemberDecorate %11 2 RelaxedPrecision 
                                           OpMemberDecorate %11 2 Offset 11 
                                           OpDecorate %11 Block 
                                           OpDecorate %13 DescriptorSet 13 
                                           OpDecorate %13 Binding 13 
                                           OpDecorate %18 RelaxedPrecision 
                                           OpDecorate %19 RelaxedPrecision 
                                           OpDecorate %25 RelaxedPrecision 
                                           OpDecorate %27 RelaxedPrecision 
                                           OpDecorate %30 RelaxedPrecision 
                                           OpDecorate %31 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD0 Location 37 
                                           OpDecorate %50 RelaxedPrecision 
                                           OpDecorate %61 RelaxedPrecision 
                                           OpDecorate %64 RelaxedPrecision 
                                           OpDecorate %67 RelaxedPrecision 
                                           OpDecorate %68 RelaxedPrecision 
                                           OpDecorate %71 RelaxedPrecision 
                                           OpDecorate %74 RelaxedPrecision 
                                           OpDecorate %75 RelaxedPrecision 
                                           OpDecorate %78 RelaxedPrecision 
                                           OpDecorate %79 RelaxedPrecision 
                                           OpDecorate %81 RelaxedPrecision 
                                           OpDecorate %83 RelaxedPrecision 
                                           OpDecorate %84 RelaxedPrecision 
                                           OpDecorate %86 RelaxedPrecision 
                                           OpDecorate %88 RelaxedPrecision 
                                           OpDecorate %90 RelaxedPrecision 
                                           OpDecorate %92 RelaxedPrecision 
                                           OpDecorate %93 RelaxedPrecision 
                                           OpDecorate %94 RelaxedPrecision 
                                           OpDecorate %97 RelaxedPrecision 
                                           OpDecorate %101 RelaxedPrecision 
                                           OpDecorate %102 RelaxedPrecision 
                                           OpDecorate %103 RelaxedPrecision 
                                           OpDecorate %104 RelaxedPrecision 
                                           OpDecorate %106 RelaxedPrecision 
                                           OpDecorate %107 RelaxedPrecision 
                                           OpDecorate %110 RelaxedPrecision 
                                           OpDecorate %111 RelaxedPrecision 
                                           OpDecorate %114 RelaxedPrecision 
                                           OpDecorate %116 RelaxedPrecision 
                                           OpDecorate %119 RelaxedPrecision 
                                           OpDecorate %121 RelaxedPrecision 
                                           OpDecorate %122 RelaxedPrecision 
                                           OpDecorate %125 RelaxedPrecision 
                                           OpDecorate %126 RelaxedPrecision 
                                           OpDecorate %129 RelaxedPrecision 
                                           OpDecorate %131 RelaxedPrecision 
                                           OpDecorate %134 RelaxedPrecision 
                                           OpDecorate %135 RelaxedPrecision 
                                           OpDecorate %143 RelaxedPrecision 
                                           OpDecorate %144 RelaxedPrecision 
                                           OpDecorate %152 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD1 Location 153 
                                           OpDecorate %154 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD2 Location 155 
                                           OpDecorate %156 RelaxedPrecision 
                                           OpDecorate %157 RelaxedPrecision 
                                           OpDecorate %158 RelaxedPrecision 
                                           OpDecorate %159 RelaxedPrecision 
                                           OpDecorate %160 RelaxedPrecision 
                                           OpDecorate %161 RelaxedPrecision 
                                           OpDecorate %162 RelaxedPrecision 
                                           OpDecorate %163 RelaxedPrecision 
                                           OpDecorate %164 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD3 Location 165 
                                           OpDecorate %166 RelaxedPrecision 
                                           OpDecorate %167 RelaxedPrecision 
                                           OpDecorate %168 RelaxedPrecision 
                                           OpDecorate %169 RelaxedPrecision 
                                           OpDecorate %170 RelaxedPrecision 
                                           OpDecorate %171 RelaxedPrecision 
                                           OpDecorate %173 RelaxedPrecision 
                                           OpDecorate %173 Location 173 
                                           OpDecorate %179 RelaxedPrecision 
                                           OpDecorate %181 RelaxedPrecision 
                                           OpDecorate %182 RelaxedPrecision 
                                    %2 = OpTypeVoid 
                                    %3 = OpTypeFunction %2 
                                    %6 = OpTypeFloat 32 
                                    %7 = OpTypeVector %6 2 
                                    %8 = OpTypePointer Private %7 
                     Private f32_2* %9 = OpVariable Private 
                                   %10 = OpTypeVector %6 4 
                                   %11 = OpTypeStruct %10 %6 %6 
                                   %12 = OpTypePointer Uniform %11 
Uniform struct {f32_4; f32; f32;}* %13 = OpVariable Uniform 
                                   %14 = OpTypeInt 32 1 
                               i32 %15 = OpConstant 1 
                                   %16 = OpTypePointer Uniform %6 
                                   %20 = OpTypeInt 32 0 
                               u32 %21 = OpConstant 0 
                                   %22 = OpTypePointer Private %6 
                               f32 %26 = OpConstant 3.674022E-40 
                                   %33 = OpTypeVector %6 3 
                                   %34 = OpTypePointer Private %33 
                    Private f32_3* %35 = OpVariable Private 
                                   %36 = OpTypePointer Input %33 
             Input f32_3* vs_TEXCOORD0 = OpVariable Input 
                    Private f32_3* %50 = OpVariable Private 
                               i32 %51 = OpConstant 0 
                                   %52 = OpTypePointer Uniform %10 
                               f32 %62 = OpConstant 3.674022E-40 
                               f32 %63 = OpConstant 3.674022E-40 
                               i32 %72 = OpConstant 2 
                      Private f32* %81 = OpVariable Private 
                               f32 %85 = OpConstant 3.674022E-40 
                               f32 %87 = OpConstant 3.674022E-40 
                               u32 %95 = OpConstant 1 
                               f32 %98 = OpConstant 3.674022E-40 
                               f32 %99 = OpConstant 3.674022E-40 
                            f32_2 %100 = OpConstantComposite %98 %99 
                              f32 %115 = OpConstant 3.674022E-40 
                              f32 %130 = OpConstant 3.674022E-40 
                              f32 %139 = OpConstant 3.674022E-40 
                                  %146 = OpTypeBool 
                                  %147 = OpTypePointer Private %146 
                    Private bool* %148 = OpVariable Private 
                   Private f32_3* %152 = OpVariable Private 
             Input f32_3* vs_TEXCOORD1 = OpVariable Input 
             Input f32_3* vs_TEXCOORD2 = OpVariable Input 
             Input f32_3* vs_TEXCOORD3 = OpVariable Input 
                                  %172 = OpTypePointer Output %10 
                    Output f32_4* %173 = OpVariable Output 
                                  %175 = OpTypePointer Function %33 
                              u32 %185 = OpConstant 3 
                                  %186 = OpTypePointer Output %6 
                               void %4 = OpFunction None %3 
                                    %5 = OpLabel 
                  Function f32_3* %176 = OpVariable Function 
                      Uniform f32* %17 = OpAccessChain %13 %15 
                               f32 %18 = OpLoad %17 
                               f32 %19 = OpExtInst %1 30 %18 
                      Private f32* %23 = OpAccessChain %9 %21 
                                           OpStore %23 %19 
                      Private f32* %24 = OpAccessChain %9 %21 
                               f32 %25 = OpLoad %24 
                               f32 %27 = OpFMul %25 %26 
                      Private f32* %28 = OpAccessChain %9 %21 
                                           OpStore %28 %27 
                      Private f32* %29 = OpAccessChain %9 %21 
                               f32 %30 = OpLoad %29 
                               f32 %31 = OpExtInst %1 29 %30 
                      Private f32* %32 = OpAccessChain %9 %21 
                                           OpStore %32 %31 
                             f32_3 %38 = OpLoad vs_TEXCOORD0 
                             f32_3 %39 = OpLoad vs_TEXCOORD0 
                               f32 %40 = OpDot %38 %39 
                      Private f32* %41 = OpAccessChain %35 %21 
                                           OpStore %41 %40 
                      Private f32* %42 = OpAccessChain %35 %21 
                               f32 %43 = OpLoad %42 
                               f32 %44 = OpExtInst %1 32 %43 
                      Private f32* %45 = OpAccessChain %35 %21 
                                           OpStore %45 %44 
                             f32_3 %46 = OpLoad %35 
                             f32_3 %47 = OpVectorShuffle %46 %46 0 0 0 
                             f32_3 %48 = OpLoad vs_TEXCOORD0 
                             f32_3 %49 = OpFMul %47 %48 
                                           OpStore %35 %49 
                    Uniform f32_4* %53 = OpAccessChain %13 %51 
                             f32_4 %54 = OpLoad %53 
                             f32_3 %55 = OpVectorShuffle %54 %54 0 1 2 
                             f32_3 %56 = OpLoad %35 
                             f32_3 %57 = OpFNegate %56 
                               f32 %58 = OpDot %55 %57 
                      Private f32* %59 = OpAccessChain %50 %21 
                                           OpStore %59 %58 
                      Private f32* %60 = OpAccessChain %50 %21 
                               f32 %61 = OpLoad %60 
                               f32 %64 = OpExtInst %1 43 %61 %62 %63 
                      Private f32* %65 = OpAccessChain %50 %21 
                                           OpStore %65 %64 
                      Private f32* %66 = OpAccessChain %50 %21 
                               f32 %67 = OpLoad %66 
                               f32 %68 = OpExtInst %1 30 %67 
                      Private f32* %69 = OpAccessChain %50 %21 
                                           OpStore %69 %68 
                      Private f32* %70 = OpAccessChain %50 %21 
                               f32 %71 = OpLoad %70 
                      Uniform f32* %73 = OpAccessChain %13 %72 
                               f32 %74 = OpLoad %73 
                               f32 %75 = OpFMul %71 %74 
                      Private f32* %76 = OpAccessChain %50 %21 
                                           OpStore %76 %75 
                      Private f32* %77 = OpAccessChain %50 %21 
                               f32 %78 = OpLoad %77 
                               f32 %79 = OpExtInst %1 29 %78 
                      Private f32* %80 = OpAccessChain %50 %21 
                                           OpStore %80 %79 
                      Private f32* %82 = OpAccessChain %50 %21 
                               f32 %83 = OpLoad %82 
                               f32 %84 = OpFNegate %83 
                               f32 %86 = OpFMul %84 %85 
                               f32 %88 = OpFAdd %86 %87 
                                           OpStore %81 %88 
                      Private f32* %89 = OpAccessChain %50 %21 
                               f32 %90 = OpLoad %89 
                      Private f32* %91 = OpAccessChain %50 %21 
                               f32 %92 = OpLoad %91 
                               f32 %93 = OpFMul %90 %92 
                               f32 %94 = OpFAdd %93 %63 
                      Private f32* %96 = OpAccessChain %9 %95 
                                           OpStore %96 %94 
                             f32_2 %97 = OpLoad %9 
                            f32_2 %101 = OpFMul %97 %100 
                                           OpStore %9 %101 
                              f32 %102 = OpLoad %81 
                              f32 %103 = OpExtInst %1 30 %102 
                                           OpStore %81 %103 
                              f32 %104 = OpLoad %81 
                     Private f32* %105 = OpAccessChain %9 %21 
                              f32 %106 = OpLoad %105 
                              f32 %107 = OpFMul %104 %106 
                     Private f32* %108 = OpAccessChain %9 %21 
                                           OpStore %108 %107 
                     Private f32* %109 = OpAccessChain %9 %21 
                              f32 %110 = OpLoad %109 
                              f32 %111 = OpExtInst %1 29 %110 
                     Private f32* %112 = OpAccessChain %9 %21 
                                           OpStore %112 %111 
                     Private f32* %113 = OpAccessChain %9 %21 
                              f32 %114 = OpLoad %113 
                              f32 %116 = OpExtInst %1 40 %114 %115 
                     Private f32* %117 = OpAccessChain %9 %21 
                                           OpStore %117 %116 
                     Private f32* %118 = OpAccessChain %9 %95 
                              f32 %119 = OpLoad %118 
                     Private f32* %120 = OpAccessChain %9 %21 
                              f32 %121 = OpLoad %120 
                              f32 %122 = OpFDiv %119 %121 
                     Private f32* %123 = OpAccessChain %9 %21 
                                           OpStore %123 %122 
                     Private f32* %124 = OpAccessChain %9 %21 
                              f32 %125 = OpLoad %124 
                              f32 %126 = OpExtInst %1 30 %125 
                     Private f32* %127 = OpAccessChain %9 %21 
                                           OpStore %127 %126 
                     Private f32* %128 = OpAccessChain %9 %21 
                              f32 %129 = OpLoad %128 
                              f32 %131 = OpFMul %129 %130 
                     Private f32* %132 = OpAccessChain %9 %21 
                                           OpStore %132 %131 
                     Private f32* %133 = OpAccessChain %9 %21 
                              f32 %134 = OpLoad %133 
                              f32 %135 = OpExtInst %1 29 %134 
                     Private f32* %136 = OpAccessChain %9 %21 
                                           OpStore %136 %135 
                     Private f32* %137 = OpAccessChain %35 %95 
                              f32 %138 = OpLoad %137 
                              f32 %140 = OpFMul %138 %139 
                     Private f32* %141 = OpAccessChain %50 %21 
                                           OpStore %141 %140 
                     Private f32* %142 = OpAccessChain %50 %21 
                              f32 %143 = OpLoad %142 
                              f32 %144 = OpExtInst %1 43 %143 %62 %63 
                     Private f32* %145 = OpAccessChain %50 %21 
                                           OpStore %145 %144 
                     Private f32* %149 = OpAccessChain %35 %95 
                              f32 %150 = OpLoad %149 
                             bool %151 = OpFOrdLessThan %150 %62 
                                           OpStore %148 %151 
                            f32_3 %154 = OpLoad vs_TEXCOORD1 
                            f32_3 %156 = OpLoad vs_TEXCOORD2 
                            f32_3 %157 = OpFNegate %156 
                            f32_3 %158 = OpFAdd %154 %157 
                                           OpStore %152 %158 
                            f32_3 %159 = OpLoad %50 
                            f32_3 %160 = OpVectorShuffle %159 %159 0 0 0 
                            f32_3 %161 = OpLoad %152 
                            f32_3 %162 = OpFMul %160 %161 
                            f32_3 %163 = OpLoad vs_TEXCOORD2 
                            f32_3 %164 = OpFAdd %162 %163 
                                           OpStore %50 %164 
                            f32_3 %166 = OpLoad vs_TEXCOORD3 
                            f32_2 %167 = OpLoad %9 
                            f32_3 %168 = OpVectorShuffle %167 %167 0 0 0 
                            f32_3 %169 = OpFMul %166 %168 
                            f32_3 %170 = OpLoad %50 
                            f32_3 %171 = OpFAdd %169 %170 
                                           OpStore %152 %171 
                             bool %174 = OpLoad %148 
                                           OpSelectionMerge %178 None 
                                           OpBranchConditional %174 %177 %180 
                                  %177 = OpLabel 
                            f32_3 %179 = OpLoad %152 
                                           OpStore %176 %179 
                                           OpBranch %178 
                                  %180 = OpLabel 
                            f32_3 %181 = OpLoad %50 
                                           OpStore %176 %181 
                                           OpBranch %178 
                                  %178 = OpLabel 
                            f32_3 %182 = OpLoad %176 
                            f32_4 %183 = OpLoad %173 
                            f32_4 %184 = OpVectorShuffle %183 %182 4 5 6 3 
                                           OpStore %173 %184 
                      Output f32* %187 = OpAccessChain %173 %185 
                                           OpStore %187 %63 
                                           OpReturn
                                           OpFunctionEnd
"
}
SubProgram "vulkan hw_tier01 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 864
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %845 %848 %851 %854 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpMemberDecorate %19 7 RelaxedPrecision 
                                                      OpMemberDecorate %19 7 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %762 RelaxedPrecision 
                                                      OpDecorate %764 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %767 RelaxedPrecision 
                                                      OpDecorate %769 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %774 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %776 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %786 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %795 RelaxedPrecision 
                                                      OpDecorate %796 RelaxedPrecision 
                                                      OpDecorate %797 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %802 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %811 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate %816 RelaxedPrecision 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate %819 RelaxedPrecision 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate %822 RelaxedPrecision 
                                                      OpDecorate %823 RelaxedPrecision 
                                                      OpDecorate %824 RelaxedPrecision 
                                                      OpDecorate %825 RelaxedPrecision 
                                                      OpDecorate %826 RelaxedPrecision 
                                                      OpDecorate %827 RelaxedPrecision 
                                                      OpDecorate %828 RelaxedPrecision 
                                                      OpDecorate %829 RelaxedPrecision 
                                                      OpDecorate %830 RelaxedPrecision 
                                                      OpDecorate %831 RelaxedPrecision 
                                                      OpDecorate %833 RelaxedPrecision 
                                                      OpDecorate %834 RelaxedPrecision 
                                                      OpDecorate %835 RelaxedPrecision 
                                                      OpDecorate %836 RelaxedPrecision 
                                                      OpDecorate %839 RelaxedPrecision 
                                                      OpDecorate %840 RelaxedPrecision 
                                                      OpDecorate %841 RelaxedPrecision 
                                                      OpDecorate %842 RelaxedPrecision 
                                                      OpDecorate %843 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 845 
                                                      OpDecorate %846 RelaxedPrecision 
                                                      OpDecorate %847 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 848 
                                                      OpDecorate %849 RelaxedPrecision 
                                                      OpDecorate %850 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 Location 851 
                                                      OpDecorate %852 RelaxedPrecision 
                                                      OpDecorate %853 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 854 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %7 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32_4; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 6 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 7 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         i32 %760 = OpConstant 5 
                                         i32 %782 = OpConstant 4 
                                Private f32* %787 = OpVariable Private 
                                         f32 %799 = OpConstant 3.674022E-40 
                              Private f32_3* %802 = OpVariable Private 
                              Private f32_3* %830 = OpVariable Private 
                                         f32 %837 = OpConstant 3.674022E-40 
                                       f32_3 %838 = OpConstantComposite %837 %837 %837 
                                             %844 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD3 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD0 = OpVariable Output 
                                             %858 = OpTypePointer Output %6 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Uniform f32* %761 = OpAccessChain %21 %760 %133 
                                         f32 %762 = OpLoad %761 
                                Uniform f32* %763 = OpAccessChain %21 %760 %70 
                                         f32 %764 = OpLoad %763 
                                Uniform f32* %765 = OpAccessChain %21 %760 %371 
                                         f32 %766 = OpLoad %765 
                                       f32_3 %767 = OpCompositeConstruct %762 %764 %766 
                                Uniform f32* %768 = OpAccessChain %21 %760 %133 
                                         f32 %769 = OpLoad %768 
                                Uniform f32* %770 = OpAccessChain %21 %760 %70 
                                         f32 %771 = OpLoad %770 
                                Uniform f32* %772 = OpAccessChain %21 %760 %371 
                                         f32 %773 = OpLoad %772 
                                       f32_3 %774 = OpCompositeConstruct %769 %771 %773 
                                       f32_3 %775 = OpFMul %767 %774 
                                                      OpStore %127 %775 
                                       f32_3 %776 = OpLoad %127 
                                       f32_3 %777 = OpLoad %561 
                                       f32_3 %778 = OpFMul %776 %777 
                                       f32_3 %779 = OpLoad %559 
                                       f32_3 %780 = OpFAdd %778 %779 
                                                      OpStore %127 %780 
                                       f32_3 %781 = OpLoad %127 
                                Uniform f32* %783 = OpAccessChain %21 %782 
                                         f32 %784 = OpLoad %783 
                                       f32_3 %785 = OpCompositeConstruct %784 %784 %784 
                                       f32_3 %786 = OpFMul %781 %785 
                                                      OpStore %127 %786 
                              Uniform f32_4* %788 = OpAccessChain %21 %28 
                                       f32_4 %789 = OpLoad %788 
                                       f32_3 %790 = OpVectorShuffle %789 %789 0 1 2 
                                       f32_4 %791 = OpLoad %50 
                                       f32_3 %792 = OpVectorShuffle %791 %791 0 1 2 
                                       f32_3 %793 = OpFNegate %792 
                                         f32 %794 = OpDot %790 %793 
                                                      OpStore %787 %794 
                                         f32 %795 = OpLoad %787 
                                         f32 %796 = OpLoad %787 
                                         f32 %797 = OpFMul %795 %796 
                                                      OpStore %787 %797 
                                         f32 %798 = OpLoad %787 
                                         f32 %800 = OpFMul %798 %799 
                                         f32 %801 = OpFAdd %800 %799 
                                                      OpStore %787 %801 
                                         f32 %803 = OpLoad %787 
                                       f32_3 %804 = OpCompositeConstruct %803 %803 %803 
                                       f32_3 %805 = OpLoad %559 
                                       f32_3 %806 = OpFMul %804 %805 
                                                      OpStore %802 %806 
                                       f32_3 %807 = OpLoad %802 
                                Uniform f32* %808 = OpAccessChain %21 %782 
                                         f32 %809 = OpLoad %808 
                                       f32_3 %810 = OpCompositeConstruct %809 %809 %809 
                                       f32_3 %811 = OpFMul %807 %810 
                                                      OpStore %802 %811 
                              Uniform f32_4* %812 = OpAccessChain %21 %46 
                                       f32_4 %813 = OpLoad %812 
                                       f32_3 %814 = OpVectorShuffle %813 %813 0 1 2 
                              Uniform f32_4* %815 = OpAccessChain %21 %46 
                                       f32_4 %816 = OpLoad %815 
                                       f32_3 %817 = OpVectorShuffle %816 %816 0 1 2 
                                         f32 %818 = OpDot %814 %817 
                                                      OpStore %787 %818 
                                         f32 %819 = OpLoad %787 
                                         f32 %820 = OpExtInst %1 31 %819 
                                                      OpStore %787 %820 
                                         f32 %821 = OpLoad %787 
                                         f32 %822 = OpExtInst %1 40 %821 %330 
                                                      OpStore %787 %822 
                                         f32 %823 = OpLoad %787 
                                         f32 %824 = OpExtInst %1 37 %823 %89 
                                                      OpStore %787 %824 
                                       f32_3 %825 = OpLoad %561 
                                                      OpStore %561 %825 
                                       f32_3 %826 = OpLoad %561 
                                       f32_3 %827 = OpCompositeConstruct %174 %174 %174 
                                       f32_3 %828 = OpCompositeConstruct %89 %89 %89 
                                       f32_3 %829 = OpExtInst %1 43 %826 %827 %828 
                                                      OpStore %561 %829 
                                       f32_3 %831 = OpLoad %561 
                              Uniform f32_4* %832 = OpAccessChain %21 %46 
                                       f32_4 %833 = OpLoad %832 
                                       f32_3 %834 = OpVectorShuffle %833 %833 0 1 2 
                                       f32_3 %835 = OpFMul %831 %834 
                                                      OpStore %830 %835 
                                       f32_3 %836 = OpLoad %830 
                                       f32_3 %839 = OpFMul %836 %838 
                                                      OpStore %830 %839 
                                       f32_3 %840 = OpLoad %830 
                                         f32 %841 = OpLoad %787 
                                       f32_3 %842 = OpCompositeConstruct %841 %841 %841 
                                       f32_3 %843 = OpFDiv %840 %842 
                                                      OpStore %830 %843 
                                       f32_3 %846 = OpLoad %127 
                                       f32_3 %847 = OpExtInst %1 31 %846 
                                                      OpStore vs_TEXCOORD1 %847 
                                       f32_3 %849 = OpLoad %802 
                                       f32_3 %850 = OpExtInst %1 31 %849 
                                                      OpStore vs_TEXCOORD2 %850 
                                       f32_3 %852 = OpLoad %830 
                                       f32_3 %853 = OpExtInst %1 31 %852 
                                                      OpStore vs_TEXCOORD3 %853 
                                       f32_4 %855 = OpLoad %50 
                                       f32_3 %856 = OpVectorShuffle %855 %855 0 1 2 
                                       f32_3 %857 = OpFNegate %856 
                                                      OpStore vs_TEXCOORD0 %857 
                                 Output f32* %859 = OpAccessChain %74 %28 %70 
                                         f32 %860 = OpLoad %859 
                                         f32 %861 = OpFNegate %860 
                                 Output f32* %862 = OpAccessChain %74 %28 %70 
                                                      OpStore %862 %861 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 189
; Schema: 0
                                           OpCapability Shader 
                                    %1 = OpExtInstImport "GLSL.std.450" 
                                           OpMemoryModel Logical GLSL450 
                                           OpEntryPoint Fragment %4 "main" %37 %153 %155 %165 %173 
                                           OpExecutionMode %4 OriginUpperLeft 
                                           OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                           OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                           OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                           OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                           OpDecorate %9 RelaxedPrecision 
                                           OpMemberDecorate %11 0 Offset 11 
                                           OpMemberDecorate %11 1 RelaxedPrecision 
                                           OpMemberDecorate %11 1 Offset 11 
                                           OpMemberDecorate %11 2 RelaxedPrecision 
                                           OpMemberDecorate %11 2 Offset 11 
                                           OpDecorate %11 Block 
                                           OpDecorate %13 DescriptorSet 13 
                                           OpDecorate %13 Binding 13 
                                           OpDecorate %18 RelaxedPrecision 
                                           OpDecorate %19 RelaxedPrecision 
                                           OpDecorate %25 RelaxedPrecision 
                                           OpDecorate %27 RelaxedPrecision 
                                           OpDecorate %30 RelaxedPrecision 
                                           OpDecorate %31 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD0 Location 37 
                                           OpDecorate %50 RelaxedPrecision 
                                           OpDecorate %61 RelaxedPrecision 
                                           OpDecorate %64 RelaxedPrecision 
                                           OpDecorate %67 RelaxedPrecision 
                                           OpDecorate %68 RelaxedPrecision 
                                           OpDecorate %71 RelaxedPrecision 
                                           OpDecorate %74 RelaxedPrecision 
                                           OpDecorate %75 RelaxedPrecision 
                                           OpDecorate %78 RelaxedPrecision 
                                           OpDecorate %79 RelaxedPrecision 
                                           OpDecorate %81 RelaxedPrecision 
                                           OpDecorate %83 RelaxedPrecision 
                                           OpDecorate %84 RelaxedPrecision 
                                           OpDecorate %86 RelaxedPrecision 
                                           OpDecorate %88 RelaxedPrecision 
                                           OpDecorate %90 RelaxedPrecision 
                                           OpDecorate %92 RelaxedPrecision 
                                           OpDecorate %93 RelaxedPrecision 
                                           OpDecorate %94 RelaxedPrecision 
                                           OpDecorate %97 RelaxedPrecision 
                                           OpDecorate %101 RelaxedPrecision 
                                           OpDecorate %102 RelaxedPrecision 
                                           OpDecorate %103 RelaxedPrecision 
                                           OpDecorate %104 RelaxedPrecision 
                                           OpDecorate %106 RelaxedPrecision 
                                           OpDecorate %107 RelaxedPrecision 
                                           OpDecorate %110 RelaxedPrecision 
                                           OpDecorate %111 RelaxedPrecision 
                                           OpDecorate %114 RelaxedPrecision 
                                           OpDecorate %116 RelaxedPrecision 
                                           OpDecorate %119 RelaxedPrecision 
                                           OpDecorate %121 RelaxedPrecision 
                                           OpDecorate %122 RelaxedPrecision 
                                           OpDecorate %125 RelaxedPrecision 
                                           OpDecorate %126 RelaxedPrecision 
                                           OpDecorate %129 RelaxedPrecision 
                                           OpDecorate %131 RelaxedPrecision 
                                           OpDecorate %134 RelaxedPrecision 
                                           OpDecorate %135 RelaxedPrecision 
                                           OpDecorate %143 RelaxedPrecision 
                                           OpDecorate %144 RelaxedPrecision 
                                           OpDecorate %152 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD1 Location 153 
                                           OpDecorate %154 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD2 Location 155 
                                           OpDecorate %156 RelaxedPrecision 
                                           OpDecorate %157 RelaxedPrecision 
                                           OpDecorate %158 RelaxedPrecision 
                                           OpDecorate %159 RelaxedPrecision 
                                           OpDecorate %160 RelaxedPrecision 
                                           OpDecorate %161 RelaxedPrecision 
                                           OpDecorate %162 RelaxedPrecision 
                                           OpDecorate %163 RelaxedPrecision 
                                           OpDecorate %164 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD3 Location 165 
                                           OpDecorate %166 RelaxedPrecision 
                                           OpDecorate %167 RelaxedPrecision 
                                           OpDecorate %168 RelaxedPrecision 
                                           OpDecorate %169 RelaxedPrecision 
                                           OpDecorate %170 RelaxedPrecision 
                                           OpDecorate %171 RelaxedPrecision 
                                           OpDecorate %173 RelaxedPrecision 
                                           OpDecorate %173 Location 173 
                                           OpDecorate %179 RelaxedPrecision 
                                           OpDecorate %181 RelaxedPrecision 
                                           OpDecorate %182 RelaxedPrecision 
                                    %2 = OpTypeVoid 
                                    %3 = OpTypeFunction %2 
                                    %6 = OpTypeFloat 32 
                                    %7 = OpTypeVector %6 2 
                                    %8 = OpTypePointer Private %7 
                     Private f32_2* %9 = OpVariable Private 
                                   %10 = OpTypeVector %6 4 
                                   %11 = OpTypeStruct %10 %6 %6 
                                   %12 = OpTypePointer Uniform %11 
Uniform struct {f32_4; f32; f32;}* %13 = OpVariable Uniform 
                                   %14 = OpTypeInt 32 1 
                               i32 %15 = OpConstant 1 
                                   %16 = OpTypePointer Uniform %6 
                                   %20 = OpTypeInt 32 0 
                               u32 %21 = OpConstant 0 
                                   %22 = OpTypePointer Private %6 
                               f32 %26 = OpConstant 3.674022E-40 
                                   %33 = OpTypeVector %6 3 
                                   %34 = OpTypePointer Private %33 
                    Private f32_3* %35 = OpVariable Private 
                                   %36 = OpTypePointer Input %33 
             Input f32_3* vs_TEXCOORD0 = OpVariable Input 
                    Private f32_3* %50 = OpVariable Private 
                               i32 %51 = OpConstant 0 
                                   %52 = OpTypePointer Uniform %10 
                               f32 %62 = OpConstant 3.674022E-40 
                               f32 %63 = OpConstant 3.674022E-40 
                               i32 %72 = OpConstant 2 
                      Private f32* %81 = OpVariable Private 
                               f32 %85 = OpConstant 3.674022E-40 
                               f32 %87 = OpConstant 3.674022E-40 
                               u32 %95 = OpConstant 1 
                               f32 %98 = OpConstant 3.674022E-40 
                               f32 %99 = OpConstant 3.674022E-40 
                            f32_2 %100 = OpConstantComposite %98 %99 
                              f32 %115 = OpConstant 3.674022E-40 
                              f32 %130 = OpConstant 3.674022E-40 
                              f32 %139 = OpConstant 3.674022E-40 
                                  %146 = OpTypeBool 
                                  %147 = OpTypePointer Private %146 
                    Private bool* %148 = OpVariable Private 
                   Private f32_3* %152 = OpVariable Private 
             Input f32_3* vs_TEXCOORD1 = OpVariable Input 
             Input f32_3* vs_TEXCOORD2 = OpVariable Input 
             Input f32_3* vs_TEXCOORD3 = OpVariable Input 
                                  %172 = OpTypePointer Output %10 
                    Output f32_4* %173 = OpVariable Output 
                                  %175 = OpTypePointer Function %33 
                              u32 %185 = OpConstant 3 
                                  %186 = OpTypePointer Output %6 
                               void %4 = OpFunction None %3 
                                    %5 = OpLabel 
                  Function f32_3* %176 = OpVariable Function 
                      Uniform f32* %17 = OpAccessChain %13 %15 
                               f32 %18 = OpLoad %17 
                               f32 %19 = OpExtInst %1 30 %18 
                      Private f32* %23 = OpAccessChain %9 %21 
                                           OpStore %23 %19 
                      Private f32* %24 = OpAccessChain %9 %21 
                               f32 %25 = OpLoad %24 
                               f32 %27 = OpFMul %25 %26 
                      Private f32* %28 = OpAccessChain %9 %21 
                                           OpStore %28 %27 
                      Private f32* %29 = OpAccessChain %9 %21 
                               f32 %30 = OpLoad %29 
                               f32 %31 = OpExtInst %1 29 %30 
                      Private f32* %32 = OpAccessChain %9 %21 
                                           OpStore %32 %31 
                             f32_3 %38 = OpLoad vs_TEXCOORD0 
                             f32_3 %39 = OpLoad vs_TEXCOORD0 
                               f32 %40 = OpDot %38 %39 
                      Private f32* %41 = OpAccessChain %35 %21 
                                           OpStore %41 %40 
                      Private f32* %42 = OpAccessChain %35 %21 
                               f32 %43 = OpLoad %42 
                               f32 %44 = OpExtInst %1 32 %43 
                      Private f32* %45 = OpAccessChain %35 %21 
                                           OpStore %45 %44 
                             f32_3 %46 = OpLoad %35 
                             f32_3 %47 = OpVectorShuffle %46 %46 0 0 0 
                             f32_3 %48 = OpLoad vs_TEXCOORD0 
                             f32_3 %49 = OpFMul %47 %48 
                                           OpStore %35 %49 
                    Uniform f32_4* %53 = OpAccessChain %13 %51 
                             f32_4 %54 = OpLoad %53 
                             f32_3 %55 = OpVectorShuffle %54 %54 0 1 2 
                             f32_3 %56 = OpLoad %35 
                             f32_3 %57 = OpFNegate %56 
                               f32 %58 = OpDot %55 %57 
                      Private f32* %59 = OpAccessChain %50 %21 
                                           OpStore %59 %58 
                      Private f32* %60 = OpAccessChain %50 %21 
                               f32 %61 = OpLoad %60 
                               f32 %64 = OpExtInst %1 43 %61 %62 %63 
                      Private f32* %65 = OpAccessChain %50 %21 
                                           OpStore %65 %64 
                      Private f32* %66 = OpAccessChain %50 %21 
                               f32 %67 = OpLoad %66 
                               f32 %68 = OpExtInst %1 30 %67 
                      Private f32* %69 = OpAccessChain %50 %21 
                                           OpStore %69 %68 
                      Private f32* %70 = OpAccessChain %50 %21 
                               f32 %71 = OpLoad %70 
                      Uniform f32* %73 = OpAccessChain %13 %72 
                               f32 %74 = OpLoad %73 
                               f32 %75 = OpFMul %71 %74 
                      Private f32* %76 = OpAccessChain %50 %21 
                                           OpStore %76 %75 
                      Private f32* %77 = OpAccessChain %50 %21 
                               f32 %78 = OpLoad %77 
                               f32 %79 = OpExtInst %1 29 %78 
                      Private f32* %80 = OpAccessChain %50 %21 
                                           OpStore %80 %79 
                      Private f32* %82 = OpAccessChain %50 %21 
                               f32 %83 = OpLoad %82 
                               f32 %84 = OpFNegate %83 
                               f32 %86 = OpFMul %84 %85 
                               f32 %88 = OpFAdd %86 %87 
                                           OpStore %81 %88 
                      Private f32* %89 = OpAccessChain %50 %21 
                               f32 %90 = OpLoad %89 
                      Private f32* %91 = OpAccessChain %50 %21 
                               f32 %92 = OpLoad %91 
                               f32 %93 = OpFMul %90 %92 
                               f32 %94 = OpFAdd %93 %63 
                      Private f32* %96 = OpAccessChain %9 %95 
                                           OpStore %96 %94 
                             f32_2 %97 = OpLoad %9 
                            f32_2 %101 = OpFMul %97 %100 
                                           OpStore %9 %101 
                              f32 %102 = OpLoad %81 
                              f32 %103 = OpExtInst %1 30 %102 
                                           OpStore %81 %103 
                              f32 %104 = OpLoad %81 
                     Private f32* %105 = OpAccessChain %9 %21 
                              f32 %106 = OpLoad %105 
                              f32 %107 = OpFMul %104 %106 
                     Private f32* %108 = OpAccessChain %9 %21 
                                           OpStore %108 %107 
                     Private f32* %109 = OpAccessChain %9 %21 
                              f32 %110 = OpLoad %109 
                              f32 %111 = OpExtInst %1 29 %110 
                     Private f32* %112 = OpAccessChain %9 %21 
                                           OpStore %112 %111 
                     Private f32* %113 = OpAccessChain %9 %21 
                              f32 %114 = OpLoad %113 
                              f32 %116 = OpExtInst %1 40 %114 %115 
                     Private f32* %117 = OpAccessChain %9 %21 
                                           OpStore %117 %116 
                     Private f32* %118 = OpAccessChain %9 %95 
                              f32 %119 = OpLoad %118 
                     Private f32* %120 = OpAccessChain %9 %21 
                              f32 %121 = OpLoad %120 
                              f32 %122 = OpFDiv %119 %121 
                     Private f32* %123 = OpAccessChain %9 %21 
                                           OpStore %123 %122 
                     Private f32* %124 = OpAccessChain %9 %21 
                              f32 %125 = OpLoad %124 
                              f32 %126 = OpExtInst %1 30 %125 
                     Private f32* %127 = OpAccessChain %9 %21 
                                           OpStore %127 %126 
                     Private f32* %128 = OpAccessChain %9 %21 
                              f32 %129 = OpLoad %128 
                              f32 %131 = OpFMul %129 %130 
                     Private f32* %132 = OpAccessChain %9 %21 
                                           OpStore %132 %131 
                     Private f32* %133 = OpAccessChain %9 %21 
                              f32 %134 = OpLoad %133 
                              f32 %135 = OpExtInst %1 29 %134 
                     Private f32* %136 = OpAccessChain %9 %21 
                                           OpStore %136 %135 
                     Private f32* %137 = OpAccessChain %35 %95 
                              f32 %138 = OpLoad %137 
                              f32 %140 = OpFMul %138 %139 
                     Private f32* %141 = OpAccessChain %50 %21 
                                           OpStore %141 %140 
                     Private f32* %142 = OpAccessChain %50 %21 
                              f32 %143 = OpLoad %142 
                              f32 %144 = OpExtInst %1 43 %143 %62 %63 
                     Private f32* %145 = OpAccessChain %50 %21 
                                           OpStore %145 %144 
                     Private f32* %149 = OpAccessChain %35 %95 
                              f32 %150 = OpLoad %149 
                             bool %151 = OpFOrdLessThan %150 %62 
                                           OpStore %148 %151 
                            f32_3 %154 = OpLoad vs_TEXCOORD1 
                            f32_3 %156 = OpLoad vs_TEXCOORD2 
                            f32_3 %157 = OpFNegate %156 
                            f32_3 %158 = OpFAdd %154 %157 
                                           OpStore %152 %158 
                            f32_3 %159 = OpLoad %50 
                            f32_3 %160 = OpVectorShuffle %159 %159 0 0 0 
                            f32_3 %161 = OpLoad %152 
                            f32_3 %162 = OpFMul %160 %161 
                            f32_3 %163 = OpLoad vs_TEXCOORD2 
                            f32_3 %164 = OpFAdd %162 %163 
                                           OpStore %50 %164 
                            f32_3 %166 = OpLoad vs_TEXCOORD3 
                            f32_2 %167 = OpLoad %9 
                            f32_3 %168 = OpVectorShuffle %167 %167 0 0 0 
                            f32_3 %169 = OpFMul %166 %168 
                            f32_3 %170 = OpLoad %50 
                            f32_3 %171 = OpFAdd %169 %170 
                                           OpStore %152 %171 
                             bool %174 = OpLoad %148 
                                           OpSelectionMerge %178 None 
                                           OpBranchConditional %174 %177 %180 
                                  %177 = OpLabel 
                            f32_3 %179 = OpLoad %152 
                                           OpStore %176 %179 
                                           OpBranch %178 
                                  %180 = OpLabel 
                            f32_3 %181 = OpLoad %50 
                                           OpStore %176 %181 
                                           OpBranch %178 
                                  %178 = OpLabel 
                            f32_3 %182 = OpLoad %176 
                            f32_4 %183 = OpLoad %173 
                            f32_4 %184 = OpVectorShuffle %183 %182 4 5 6 3 
                                           OpStore %173 %184 
                      Output f32* %187 = OpAccessChain %173 %185 
                                           OpStore %187 %63 
                                           OpReturn
                                           OpFunctionEnd
"
}
SubProgram "vulkan hw_tier02 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
"; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 864
; Schema: 0
                                                      OpCapability Shader 
                                               %1 = OpExtInstImport "GLSL.std.450" 
                                                      OpMemoryModel Logical GLSL450 
                                                      OpEntryPoint Vertex %4 "main" %11 %74 %845 %848 %851 %854 
                                                      OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                                      OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                                      OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                                      OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                                      OpDecorate %11 Location 11 
                                                      OpDecorate %16 ArrayStride 16 
                                                      OpDecorate %17 ArrayStride 17 
                                                      OpMemberDecorate %19 0 Offset 19 
                                                      OpMemberDecorate %19 1 Offset 19 
                                                      OpMemberDecorate %19 2 Offset 19 
                                                      OpMemberDecorate %19 3 RelaxedPrecision 
                                                      OpMemberDecorate %19 3 Offset 19 
                                                      OpMemberDecorate %19 4 RelaxedPrecision 
                                                      OpMemberDecorate %19 4 Offset 19 
                                                      OpMemberDecorate %19 5 RelaxedPrecision 
                                                      OpMemberDecorate %19 5 Offset 19 
                                                      OpMemberDecorate %19 6 RelaxedPrecision 
                                                      OpMemberDecorate %19 6 Offset 19 
                                                      OpMemberDecorate %19 7 RelaxedPrecision 
                                                      OpMemberDecorate %19 7 Offset 19 
                                                      OpDecorate %19 Block 
                                                      OpDecorate %21 DescriptorSet 21 
                                                      OpDecorate %21 Binding 21 
                                                      OpMemberDecorate %72 0 BuiltIn 72 
                                                      OpMemberDecorate %72 1 BuiltIn 72 
                                                      OpMemberDecorate %72 2 BuiltIn 72 
                                                      OpDecorate %72 Block 
                                                      OpDecorate %87 RelaxedPrecision 
                                                      OpDecorate %88 RelaxedPrecision 
                                                      OpDecorate %91 RelaxedPrecision 
                                                      OpDecorate %127 RelaxedPrecision 
                                                      OpDecorate %131 RelaxedPrecision 
                                                      OpDecorate %132 RelaxedPrecision 
                                                      OpDecorate %137 RelaxedPrecision 
                                                      OpDecorate %139 RelaxedPrecision 
                                                      OpDecorate %142 RelaxedPrecision 
                                                      OpDecorate %143 RelaxedPrecision 
                                                      OpDecorate %146 RelaxedPrecision 
                                                      OpDecorate %147 RelaxedPrecision 
                                                      OpDecorate %151 RelaxedPrecision 
                                                      OpDecorate %351 RelaxedPrecision 
                                                      OpDecorate %352 RelaxedPrecision 
                                                      OpDecorate %545 RelaxedPrecision 
                                                      OpDecorate %546 RelaxedPrecision 
                                                      OpDecorate %559 RelaxedPrecision 
                                                      OpDecorate %561 RelaxedPrecision 
                                                      OpDecorate %715 RelaxedPrecision 
                                                      OpDecorate %716 RelaxedPrecision 
                                                      OpDecorate %749 RelaxedPrecision 
                                                      OpDecorate %750 RelaxedPrecision 
                                                      OpDecorate %762 RelaxedPrecision 
                                                      OpDecorate %764 RelaxedPrecision 
                                                      OpDecorate %766 RelaxedPrecision 
                                                      OpDecorate %767 RelaxedPrecision 
                                                      OpDecorate %769 RelaxedPrecision 
                                                      OpDecorate %771 RelaxedPrecision 
                                                      OpDecorate %773 RelaxedPrecision 
                                                      OpDecorate %774 RelaxedPrecision 
                                                      OpDecorate %775 RelaxedPrecision 
                                                      OpDecorate %776 RelaxedPrecision 
                                                      OpDecorate %777 RelaxedPrecision 
                                                      OpDecorate %778 RelaxedPrecision 
                                                      OpDecorate %779 RelaxedPrecision 
                                                      OpDecorate %780 RelaxedPrecision 
                                                      OpDecorate %781 RelaxedPrecision 
                                                      OpDecorate %784 RelaxedPrecision 
                                                      OpDecorate %785 RelaxedPrecision 
                                                      OpDecorate %786 RelaxedPrecision 
                                                      OpDecorate %787 RelaxedPrecision 
                                                      OpDecorate %795 RelaxedPrecision 
                                                      OpDecorate %796 RelaxedPrecision 
                                                      OpDecorate %797 RelaxedPrecision 
                                                      OpDecorate %798 RelaxedPrecision 
                                                      OpDecorate %800 RelaxedPrecision 
                                                      OpDecorate %801 RelaxedPrecision 
                                                      OpDecorate %802 RelaxedPrecision 
                                                      OpDecorate %803 RelaxedPrecision 
                                                      OpDecorate %804 RelaxedPrecision 
                                                      OpDecorate %805 RelaxedPrecision 
                                                      OpDecorate %806 RelaxedPrecision 
                                                      OpDecorate %807 RelaxedPrecision 
                                                      OpDecorate %809 RelaxedPrecision 
                                                      OpDecorate %810 RelaxedPrecision 
                                                      OpDecorate %811 RelaxedPrecision 
                                                      OpDecorate %813 RelaxedPrecision 
                                                      OpDecorate %814 RelaxedPrecision 
                                                      OpDecorate %816 RelaxedPrecision 
                                                      OpDecorate %817 RelaxedPrecision 
                                                      OpDecorate %818 RelaxedPrecision 
                                                      OpDecorate %819 RelaxedPrecision 
                                                      OpDecorate %820 RelaxedPrecision 
                                                      OpDecorate %821 RelaxedPrecision 
                                                      OpDecorate %822 RelaxedPrecision 
                                                      OpDecorate %823 RelaxedPrecision 
                                                      OpDecorate %824 RelaxedPrecision 
                                                      OpDecorate %825 RelaxedPrecision 
                                                      OpDecorate %826 RelaxedPrecision 
                                                      OpDecorate %827 RelaxedPrecision 
                                                      OpDecorate %828 RelaxedPrecision 
                                                      OpDecorate %829 RelaxedPrecision 
                                                      OpDecorate %830 RelaxedPrecision 
                                                      OpDecorate %831 RelaxedPrecision 
                                                      OpDecorate %833 RelaxedPrecision 
                                                      OpDecorate %834 RelaxedPrecision 
                                                      OpDecorate %835 RelaxedPrecision 
                                                      OpDecorate %836 RelaxedPrecision 
                                                      OpDecorate %839 RelaxedPrecision 
                                                      OpDecorate %840 RelaxedPrecision 
                                                      OpDecorate %841 RelaxedPrecision 
                                                      OpDecorate %842 RelaxedPrecision 
                                                      OpDecorate %843 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD1 Location 845 
                                                      OpDecorate %846 RelaxedPrecision 
                                                      OpDecorate %847 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD2 Location 848 
                                                      OpDecorate %849 RelaxedPrecision 
                                                      OpDecorate %850 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD3 Location 851 
                                                      OpDecorate %852 RelaxedPrecision 
                                                      OpDecorate %853 RelaxedPrecision 
                                                      OpDecorate vs_TEXCOORD0 Location 854 
                                               %2 = OpTypeVoid 
                                               %3 = OpTypeFunction %2 
                                               %6 = OpTypeFloat 32 
                                               %7 = OpTypeVector %6 4 
                                               %8 = OpTypePointer Private %7 
                                Private f32_4* %9 = OpVariable Private 
                                              %10 = OpTypePointer Input %7 
                                 Input f32_4* %11 = OpVariable Input 
                                              %14 = OpTypeInt 32 0 
                                          u32 %15 = OpConstant 4 
                                              %16 = OpTypeArray %7 %15 
                                              %17 = OpTypeArray %7 %15 
                                              %18 = OpTypeVector %6 3 
                                              %19 = OpTypeStruct %7 %16 %17 %7 %6 %18 %18 %6 
                                              %20 = OpTypePointer Uniform %19 
Uniform struct {f32_4; f32_4[4]; f32_4[4]; f32_4; f32; f32_3; f32_3; f32;}* %21 = OpVariable Uniform 
                                              %22 = OpTypeInt 32 1 
                                          i32 %23 = OpConstant 1 
                                              %24 = OpTypePointer Uniform %7 
                                          i32 %28 = OpConstant 0 
                                          i32 %36 = OpConstant 2 
                               Private f32_4* %44 = OpVariable Private 
                                          i32 %46 = OpConstant 3 
                               Private f32_4* %50 = OpVariable Private 
                                          u32 %70 = OpConstant 1 
                                              %71 = OpTypeArray %6 %70 
                                              %72 = OpTypeStruct %7 %6 %71 
                                              %73 = OpTypePointer Output %72 
         Output struct {f32_4; f32; f32[1];}* %74 = OpVariable Output 
                                              %82 = OpTypePointer Output %7 
                                          i32 %84 = OpConstant 6 
                                              %85 = OpTypePointer Uniform %18 
                                          f32 %89 = OpConstant 3.674022E-40 
                                        f32_3 %90 = OpConstantComposite %89 %89 %89 
                                          f32 %96 = OpConstant 3.674022E-40 
                                          f32 %97 = OpConstant 3.674022E-40 
                                        f32_3 %98 = OpConstantComposite %96 %97 %96 
                                         f32 %100 = OpConstant 3.674022E-40 
                                         f32 %101 = OpConstant 3.674022E-40 
                                         f32 %102 = OpConstant 3.674022E-40 
                                       f32_3 %103 = OpConstantComposite %100 %101 %102 
                                             %126 = OpTypePointer Private %18 
                              Private f32_3* %127 = OpVariable Private 
                                         i32 %128 = OpConstant 7 
                                             %129 = OpTypePointer Uniform %6 
                                         u32 %133 = OpConstant 0 
                                             %134 = OpTypePointer Private %6 
                                         f32 %138 = OpConstant 3.674022E-40 
                                             %145 = OpTypeVector %6 2 
                                         f32 %148 = OpConstant 3.674022E-40 
                                         f32 %149 = OpConstant 3.674022E-40 
                                       f32_2 %150 = OpConstantComposite %148 %149 
                                Private f32* %154 = OpVariable Private 
                                             %169 = OpTypeBool 
                                             %170 = OpTypePointer Private %169 
                               Private bool* %171 = OpVariable Private 
                                         f32 %174 = OpConstant 3.674022E-40 
                                         f32 %184 = OpConstant 3.674022E-40 
                                Private f32* %200 = OpVariable Private 
                                Private f32* %207 = OpVariable Private 
                                         f32 %209 = OpConstant 3.674022E-40 
                                         f32 %211 = OpConstant 3.674022E-40 
                                         f32 %216 = OpConstant 3.674022E-40 
                                         f32 %221 = OpConstant 3.674022E-40 
                                         f32 %226 = OpConstant 3.674022E-40 
                                         f32 %229 = OpConstant 3.674022E-40 
                                         f32 %236 = OpConstant 3.674022E-40 
                                         f32 %237 = OpConstant 3.674022E-40 
                                       f32_3 %238 = OpConstantComposite %100 %236 %237 
                              Private f32_3* %242 = OpVariable Private 
                                       f32_3 %249 = OpConstantComposite %100 %100 %100 
                                         f32 %251 = OpConstant 3.674022E-40 
                                       f32_3 %252 = OpConstantComposite %174 %251 %174 
                                Private f32* %259 = OpVariable Private 
                                         f32 %264 = OpConstant 3.674022E-40 
                                Private f32* %268 = OpVariable Private 
                                Private f32* %277 = OpVariable Private 
                                         f32 %330 = OpConstant 3.674022E-40 
                                         f32 %346 = OpConstant 3.674022E-40 
                              Private f32_4* %348 = OpVariable Private 
                                         f32 %354 = OpConstant 3.674022E-40 
                                       f32_3 %355 = OpConstantComposite %354 %354 %354 
                              Private f32_3* %359 = OpVariable Private 
                                       f32_3 %367 = OpConstantComposite %229 %229 %229 
                                         u32 %371 = OpConstant 2 
                                         f32 %554 = OpConstant 3.674022E-40 
                                       f32_3 %555 = OpConstantComposite %554 %554 %554 
                              Private f32_3* %559 = OpVariable Private 
                              Private f32_3* %561 = OpVariable Private 
                                         f32 %567 = OpConstant 3.674022E-40 
                                         f32 %569 = OpConstant 3.674022E-40 
                                         u32 %586 = OpConstant 3 
                                       f32_2 %598 = OpConstantComposite %89 %89 
                                Private f32* %602 = OpVariable Private 
                              Private f32_3* %617 = OpVariable Private 
                                         f32 %654 = OpConstant 3.674022E-40 
                                       f32_3 %655 = OpConstantComposite %229 %330 %654 
                                       f32_2 %672 = OpConstantComposite %100 %237 
                                         i32 %760 = OpConstant 5 
                                         i32 %782 = OpConstant 4 
                                Private f32* %787 = OpVariable Private 
                                         f32 %799 = OpConstant 3.674022E-40 
                              Private f32_3* %802 = OpVariable Private 
                              Private f32_3* %830 = OpVariable Private 
                                         f32 %837 = OpConstant 3.674022E-40 
                                       f32_3 %838 = OpConstantComposite %837 %837 %837 
                                             %844 = OpTypePointer Output %18 
                       Output f32_3* vs_TEXCOORD1 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD2 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD3 = OpVariable Output 
                       Output f32_3* vs_TEXCOORD0 = OpVariable Output 
                                             %858 = OpTypePointer Output %6 
                                          void %4 = OpFunction None %3 
                                               %5 = OpLabel 
                                        f32_4 %12 = OpLoad %11 
                                        f32_4 %13 = OpVectorShuffle %12 %12 1 1 1 1 
                               Uniform f32_4* %25 = OpAccessChain %21 %23 %23 
                                        f32_4 %26 = OpLoad %25 
                                        f32_4 %27 = OpFMul %13 %26 
                                                      OpStore %9 %27 
                               Uniform f32_4* %29 = OpAccessChain %21 %23 %28 
                                        f32_4 %30 = OpLoad %29 
                                        f32_4 %31 = OpLoad %11 
                                        f32_4 %32 = OpVectorShuffle %31 %31 0 0 0 0 
                                        f32_4 %33 = OpFMul %30 %32 
                                        f32_4 %34 = OpLoad %9 
                                        f32_4 %35 = OpFAdd %33 %34 
                                                      OpStore %9 %35 
                               Uniform f32_4* %37 = OpAccessChain %21 %23 %36 
                                        f32_4 %38 = OpLoad %37 
                                        f32_4 %39 = OpLoad %11 
                                        f32_4 %40 = OpVectorShuffle %39 %39 2 2 2 2 
                                        f32_4 %41 = OpFMul %38 %40 
                                        f32_4 %42 = OpLoad %9 
                                        f32_4 %43 = OpFAdd %41 %42 
                                                      OpStore %9 %43 
                                        f32_4 %45 = OpLoad %9 
                               Uniform f32_4* %47 = OpAccessChain %21 %23 %46 
                                        f32_4 %48 = OpLoad %47 
                                        f32_4 %49 = OpFAdd %45 %48 
                                                      OpStore %44 %49 
                                        f32_4 %51 = OpLoad %44 
                                        f32_4 %52 = OpVectorShuffle %51 %51 1 1 1 1 
                               Uniform f32_4* %53 = OpAccessChain %21 %36 %23 
                                        f32_4 %54 = OpLoad %53 
                                        f32_4 %55 = OpFMul %52 %54 
                                                      OpStore %50 %55 
                               Uniform f32_4* %56 = OpAccessChain %21 %36 %28 
                                        f32_4 %57 = OpLoad %56 
                                        f32_4 %58 = OpLoad %44 
                                        f32_4 %59 = OpVectorShuffle %58 %58 0 0 0 0 
                                        f32_4 %60 = OpFMul %57 %59 
                                        f32_4 %61 = OpLoad %50 
                                        f32_4 %62 = OpFAdd %60 %61 
                                                      OpStore %50 %62 
                               Uniform f32_4* %63 = OpAccessChain %21 %36 %36 
                                        f32_4 %64 = OpLoad %63 
                                        f32_4 %65 = OpLoad %44 
                                        f32_4 %66 = OpVectorShuffle %65 %65 2 2 2 2 
                                        f32_4 %67 = OpFMul %64 %66 
                                        f32_4 %68 = OpLoad %50 
                                        f32_4 %69 = OpFAdd %67 %68 
                                                      OpStore %50 %69 
                               Uniform f32_4* %75 = OpAccessChain %21 %36 %46 
                                        f32_4 %76 = OpLoad %75 
                                        f32_4 %77 = OpLoad %44 
                                        f32_4 %78 = OpVectorShuffle %77 %77 3 3 3 3 
                                        f32_4 %79 = OpFMul %76 %78 
                                        f32_4 %80 = OpLoad %50 
                                        f32_4 %81 = OpFAdd %79 %80 
                                Output f32_4* %83 = OpAccessChain %74 %28 
                                                      OpStore %83 %81 
                               Uniform f32_3* %86 = OpAccessChain %21 %84 
                                        f32_3 %87 = OpLoad %86 
                                        f32_3 %88 = OpFNegate %87 
                                        f32_3 %91 = OpFAdd %88 %90 
                                        f32_4 %92 = OpLoad %44 
                                        f32_4 %93 = OpVectorShuffle %92 %91 4 5 6 3 
                                                      OpStore %44 %93 
                                        f32_4 %94 = OpLoad %44 
                                        f32_3 %95 = OpVectorShuffle %94 %94 0 1 2 
                                        f32_3 %99 = OpFMul %95 %98 
                                       f32_3 %104 = OpFAdd %99 %103 
                                       f32_4 %105 = OpLoad %44 
                                       f32_4 %106 = OpVectorShuffle %105 %104 4 5 6 3 
                                                      OpStore %44 %106 
                                       f32_4 %107 = OpLoad %44 
                                       f32_3 %108 = OpVectorShuffle %107 %107 0 1 2 
                                       f32_4 %109 = OpLoad %44 
                                       f32_3 %110 = OpVectorShuffle %109 %109 0 1 2 
                                       f32_3 %111 = OpFMul %108 %110 
                                       f32_4 %112 = OpLoad %44 
                                       f32_4 %113 = OpVectorShuffle %112 %111 4 5 6 3 
                                                      OpStore %44 %113 
                                       f32_4 %114 = OpLoad %44 
                                       f32_3 %115 = OpVectorShuffle %114 %114 0 1 2 
                                       f32_4 %116 = OpLoad %44 
                                       f32_3 %117 = OpVectorShuffle %116 %116 0 1 2 
                                       f32_3 %118 = OpFMul %115 %117 
                                       f32_4 %119 = OpLoad %44 
                                       f32_4 %120 = OpVectorShuffle %119 %118 4 5 6 3 
                                                      OpStore %44 %120 
                                       f32_4 %121 = OpLoad %44 
                                       f32_3 %122 = OpVectorShuffle %121 %121 0 1 2 
                                       f32_3 %123 = OpFDiv %90 %122 
                                       f32_4 %124 = OpLoad %44 
                                       f32_4 %125 = OpVectorShuffle %124 %123 4 5 6 3 
                                                      OpStore %44 %125 
                                Uniform f32* %130 = OpAccessChain %21 %128 
                                         f32 %131 = OpLoad %130 
                                         f32 %132 = OpExtInst %1 30 %131 
                                Private f32* %135 = OpAccessChain %127 %133 
                                                      OpStore %135 %132 
                                Private f32* %136 = OpAccessChain %127 %133 
                                         f32 %137 = OpLoad %136 
                                         f32 %139 = OpFMul %137 %138 
                                Private f32* %140 = OpAccessChain %127 %133 
                                                      OpStore %140 %139 
                                Private f32* %141 = OpAccessChain %127 %133 
                                         f32 %142 = OpLoad %141 
                                         f32 %143 = OpExtInst %1 29 %142 
                                Private f32* %144 = OpAccessChain %127 %133 
                                                      OpStore %144 %143 
                                       f32_3 %146 = OpLoad %127 
                                       f32_2 %147 = OpVectorShuffle %146 %146 0 0 
                                       f32_2 %151 = OpFMul %147 %150 
                                       f32_3 %152 = OpLoad %127 
                                       f32_3 %153 = OpVectorShuffle %152 %151 3 4 2 
                                                      OpStore %127 %153 
                                       f32_4 %155 = OpLoad %9 
                                       f32_3 %156 = OpVectorShuffle %155 %155 0 1 2 
                                       f32_4 %157 = OpLoad %9 
                                       f32_3 %158 = OpVectorShuffle %157 %157 0 1 2 
                                         f32 %159 = OpDot %156 %158 
                                                      OpStore %154 %159 
                                         f32 %160 = OpLoad %154 
                                         f32 %161 = OpExtInst %1 32 %160 
                                                      OpStore %154 %161 
                                         f32 %162 = OpLoad %154 
                                       f32_3 %163 = OpCompositeConstruct %162 %162 %162 
                                       f32_4 %164 = OpLoad %9 
                                       f32_3 %165 = OpVectorShuffle %164 %164 0 1 2 
                                       f32_3 %166 = OpFMul %163 %165 
                                       f32_4 %167 = OpLoad %50 
                                       f32_4 %168 = OpVectorShuffle %167 %166 4 5 6 3 
                                                      OpStore %50 %168 
                                Private f32* %172 = OpAccessChain %50 %70 
                                         f32 %173 = OpLoad %172 
                                        bool %175 = OpFOrdGreaterThanEqual %173 %174 
                                                      OpStore %171 %175 
                                        bool %176 = OpLoad %171 
                                                      OpSelectionMerge %178 None 
                                                      OpBranchConditional %176 %177 %564 
                                             %177 = OpLabel 
                                Private f32* %179 = OpAccessChain %50 %70 
                                         f32 %180 = OpLoad %179 
                                Private f32* %181 = OpAccessChain %50 %70 
                                         f32 %182 = OpLoad %181 
                                         f32 %183 = OpFMul %180 %182 
                                         f32 %185 = OpFAdd %183 %184 
                                Private f32* %186 = OpAccessChain %9 %133 
                                                      OpStore %186 %185 
                                Private f32* %187 = OpAccessChain %9 %133 
                                         f32 %188 = OpLoad %187 
                                         f32 %189 = OpExtInst %1 31 %188 
                                Private f32* %190 = OpAccessChain %9 %133 
                                                      OpStore %190 %189 
                                Private f32* %191 = OpAccessChain %9 %70 
                                         f32 %192 = OpLoad %191 
                                         f32 %193 = OpFNegate %192 
                                         f32 %194 = OpLoad %154 
                                         f32 %195 = OpFMul %193 %194 
                                Private f32* %196 = OpAccessChain %9 %133 
                                         f32 %197 = OpLoad %196 
                                         f32 %198 = OpFAdd %195 %197 
                                Private f32* %199 = OpAccessChain %9 %133 
                                                      OpStore %199 %198 
                                Private f32* %201 = OpAccessChain %9 %70 
                                         f32 %202 = OpLoad %201 
                                         f32 %203 = OpFNegate %202 
                                         f32 %204 = OpLoad %154 
                                         f32 %205 = OpFMul %203 %204 
                                         f32 %206 = OpFAdd %205 %89 
                                                      OpStore %200 %206 
                                         f32 %208 = OpLoad %200 
                                         f32 %210 = OpFMul %208 %209 
                                         f32 %212 = OpFAdd %210 %211 
                                                      OpStore %207 %212 
                                         f32 %213 = OpLoad %200 
                                         f32 %214 = OpLoad %207 
                                         f32 %215 = OpFMul %213 %214 
                                         f32 %217 = OpFAdd %215 %216 
                                                      OpStore %207 %217 
                                         f32 %218 = OpLoad %200 
                                         f32 %219 = OpLoad %207 
                                         f32 %220 = OpFMul %218 %219 
                                         f32 %222 = OpFAdd %220 %221 
                                                      OpStore %207 %222 
                                         f32 %223 = OpLoad %200 
                                         f32 %224 = OpLoad %207 
                                         f32 %225 = OpFMul %223 %224 
                                         f32 %227 = OpFAdd %225 %226 
                                                      OpStore %200 %227 
                                         f32 %228 = OpLoad %200 
                                         f32 %230 = OpFMul %228 %229 
                                                      OpStore %200 %230 
                                         f32 %231 = OpLoad %200 
                                         f32 %232 = OpExtInst %1 29 %231 
                                Private f32* %233 = OpAccessChain %9 %70 
                                                      OpStore %233 %232 
                                       f32_4 %234 = OpLoad %9 
                                       f32_3 %235 = OpVectorShuffle %234 %234 0 1 0 
                                       f32_3 %239 = OpFMul %235 %238 
                                       f32_4 %240 = OpLoad %9 
                                       f32_4 %241 = OpVectorShuffle %240 %239 4 5 6 3 
                                                      OpStore %9 %241 
                                       f32_4 %243 = OpLoad %9 
                                       f32_3 %244 = OpVectorShuffle %243 %243 0 0 0 
                                       f32_4 %245 = OpLoad %50 
                                       f32_3 %246 = OpVectorShuffle %245 %245 0 1 2 
                                       f32_3 %247 = OpFMul %244 %246 
                                                      OpStore %242 %247 
                                       f32_3 %248 = OpLoad %242 
                                       f32_3 %250 = OpFMul %248 %249 
                                       f32_3 %253 = OpFAdd %250 %252 
                                                      OpStore %242 %253 
                                       f32_3 %254 = OpLoad %242 
                                       f32_3 %255 = OpLoad %242 
                                         f32 %256 = OpDot %254 %255 
                                                      OpStore %154 %256 
                                         f32 %257 = OpLoad %154 
                                         f32 %258 = OpExtInst %1 31 %257 
                                                      OpStore %154 %258 
                                         f32 %260 = OpLoad %154 
                                         f32 %261 = OpFNegate %260 
                                         f32 %262 = OpFAdd %261 %89 
                                                      OpStore %259 %262 
                                         f32 %263 = OpLoad %259 
                                         f32 %265 = OpFMul %263 %264 
                                                      OpStore %259 %265 
                                         f32 %266 = OpLoad %259 
                                         f32 %267 = OpExtInst %1 29 %266 
                                                      OpStore %259 %267 
                              Uniform f32_4* %269 = OpAccessChain %21 %28 
                                       f32_4 %270 = OpLoad %269 
                                       f32_3 %271 = OpVectorShuffle %270 %270 0 1 2 
                                       f32_3 %272 = OpLoad %242 
                                         f32 %273 = OpDot %271 %272 
                                                      OpStore %268 %273 
                                         f32 %274 = OpLoad %268 
                                         f32 %275 = OpLoad %154 
                                         f32 %276 = OpFDiv %274 %275 
                                                      OpStore %268 %276 
                                       f32_4 %278 = OpLoad %50 
                                       f32_3 %279 = OpVectorShuffle %278 %278 0 1 2 
                                       f32_3 %280 = OpLoad %242 
                                         f32 %281 = OpDot %279 %280 
                                                      OpStore %277 %281 
                                         f32 %282 = OpLoad %277 
                                         f32 %283 = OpLoad %154 
                                         f32 %284 = OpFDiv %282 %283 
                                                      OpStore %154 %284 
                                         f32 %285 = OpLoad %268 
                                         f32 %286 = OpFNegate %285 
                                         f32 %287 = OpFAdd %286 %89 
                                                      OpStore %268 %287 
                                         f32 %288 = OpLoad %268 
                                         f32 %289 = OpFMul %288 %209 
                                         f32 %290 = OpFAdd %289 %211 
                                                      OpStore %277 %290 
                                         f32 %291 = OpLoad %268 
                                         f32 %292 = OpLoad %277 
                                         f32 %293 = OpFMul %291 %292 
                                         f32 %294 = OpFAdd %293 %216 
                                                      OpStore %277 %294 
                                         f32 %295 = OpLoad %268 
                                         f32 %296 = OpLoad %277 
                                         f32 %297 = OpFMul %295 %296 
                                         f32 %298 = OpFAdd %297 %221 
                                                      OpStore %277 %298 
                                         f32 %299 = OpLoad %268 
                                         f32 %300 = OpLoad %277 
                                         f32 %301 = OpFMul %299 %300 
                                         f32 %302 = OpFAdd %301 %226 
                                                      OpStore %268 %302 
                                         f32 %303 = OpLoad %268 
                                         f32 %304 = OpFMul %303 %229 
                                                      OpStore %268 %304 
                                         f32 %305 = OpLoad %268 
                                         f32 %306 = OpExtInst %1 29 %305 
                                                      OpStore %268 %306 
                                         f32 %307 = OpLoad %154 
                                         f32 %308 = OpFNegate %307 
                                         f32 %309 = OpFAdd %308 %89 
                                                      OpStore %154 %309 
                                         f32 %310 = OpLoad %154 
                                         f32 %311 = OpFMul %310 %209 
                                         f32 %312 = OpFAdd %311 %211 
                                                      OpStore %277 %312 
                                         f32 %313 = OpLoad %154 
                                         f32 %314 = OpLoad %277 
                                         f32 %315 = OpFMul %313 %314 
                                         f32 %316 = OpFAdd %315 %216 
                                                      OpStore %277 %316 
                                         f32 %317 = OpLoad %154 
                                         f32 %318 = OpLoad %277 
                                         f32 %319 = OpFMul %317 %318 
                                         f32 %320 = OpFAdd %319 %221 
                                                      OpStore %277 %320 
                                         f32 %321 = OpLoad %154 
                                         f32 %322 = OpLoad %277 
                                         f32 %323 = OpFMul %321 %322 
                                         f32 %324 = OpFAdd %323 %226 
                                                      OpStore %154 %324 
                                         f32 %325 = OpLoad %154 
                                         f32 %326 = OpFMul %325 %229 
                                                      OpStore %154 %326 
                                         f32 %327 = OpLoad %154 
                                         f32 %328 = OpExtInst %1 29 %327 
                                                      OpStore %154 %328 
                                         f32 %329 = OpLoad %154 
                                         f32 %331 = OpFMul %329 %330 
                                                      OpStore %154 %331 
                                         f32 %332 = OpLoad %268 
                                         f32 %333 = OpFMul %332 %330 
                                         f32 %334 = OpLoad %154 
                                         f32 %335 = OpFNegate %334 
                                         f32 %336 = OpFAdd %333 %335 
                                                      OpStore %154 %336 
                                         f32 %337 = OpLoad %259 
                                         f32 %338 = OpLoad %154 
                                         f32 %339 = OpFMul %337 %338 
                                Private f32* %340 = OpAccessChain %9 %70 
                                         f32 %341 = OpLoad %340 
                                         f32 %342 = OpFAdd %339 %341 
                                                      OpStore %154 %342 
                                         f32 %343 = OpLoad %154 
                                         f32 %344 = OpExtInst %1 40 %343 %174 
                                                      OpStore %154 %344 
                                         f32 %345 = OpLoad %154 
                                         f32 %347 = OpExtInst %1 37 %345 %346 
                                                      OpStore %154 %347 
                                       f32_4 %349 = OpLoad %44 
                                       f32_3 %350 = OpVectorShuffle %349 %349 0 1 2 
                                       f32_3 %351 = OpLoad %127 
                                       f32_3 %352 = OpVectorShuffle %351 %351 1 1 1 
                                       f32_3 %353 = OpFMul %350 %352 
                                       f32_3 %356 = OpFAdd %353 %355 
                                       f32_4 %357 = OpLoad %348 
                                       f32_4 %358 = OpVectorShuffle %357 %356 4 5 6 3 
                                                      OpStore %348 %358 
                                         f32 %360 = OpLoad %154 
                                       f32_3 %361 = OpCompositeConstruct %360 %360 %360 
                                       f32_3 %362 = OpFNegate %361 
                                       f32_4 %363 = OpLoad %348 
                                       f32_3 %364 = OpVectorShuffle %363 %363 0 1 2 
                                       f32_3 %365 = OpFMul %362 %364 
                                                      OpStore %359 %365 
                                       f32_3 %366 = OpLoad %359 
                                       f32_3 %368 = OpFMul %366 %367 
                                                      OpStore %359 %368 
                                       f32_3 %369 = OpLoad %359 
                                       f32_3 %370 = OpExtInst %1 29 %369 
                                                      OpStore %359 %370 
                                Private f32* %372 = OpAccessChain %9 %371 
                                         f32 %373 = OpLoad %372 
                                         f32 %374 = OpLoad %259 
                                         f32 %375 = OpFMul %373 %374 
                                                      OpStore %154 %375 
                                       f32_4 %376 = OpLoad %50 
                                       f32_3 %377 = OpVectorShuffle %376 %376 0 1 2 
                                       f32_4 %378 = OpLoad %9 
                                       f32_3 %379 = OpVectorShuffle %378 %378 0 0 0 
                                       f32_3 %380 = OpFMul %377 %379 
                                       f32_3 %381 = OpLoad %242 
                                       f32_3 %382 = OpFAdd %380 %381 
                                                      OpStore %242 %382 
                                       f32_3 %383 = OpLoad %242 
                                       f32_3 %384 = OpLoad %242 
                                         f32 %385 = OpDot %383 %384 
                                Private f32* %386 = OpAccessChain %9 %133 
                                                      OpStore %386 %385 
                                Private f32* %387 = OpAccessChain %9 %133 
                                         f32 %388 = OpLoad %387 
                                         f32 %389 = OpExtInst %1 31 %388 
                                Private f32* %390 = OpAccessChain %9 %133 
                                                      OpStore %390 %389 
                                Private f32* %391 = OpAccessChain %9 %133 
                                         f32 %392 = OpLoad %391 
                                         f32 %393 = OpFNegate %392 
                                         f32 %394 = OpFAdd %393 %89 
                                                      OpStore %259 %394 
                                         f32 %395 = OpLoad %259 
                                         f32 %396 = OpFMul %395 %264 
                                                      OpStore %259 %396 
                                         f32 %397 = OpLoad %259 
                                         f32 %398 = OpExtInst %1 29 %397 
                                                      OpStore %259 %398 
                              Uniform f32_4* %399 = OpAccessChain %21 %28 
                                       f32_4 %400 = OpLoad %399 
                                       f32_3 %401 = OpVectorShuffle %400 %400 0 1 2 
                                       f32_3 %402 = OpLoad %242 
                                         f32 %403 = OpDot %401 %402 
                                                      OpStore %268 %403 
                                         f32 %404 = OpLoad %268 
                                Private f32* %405 = OpAccessChain %9 %133 
                                         f32 %406 = OpLoad %405 
                                         f32 %407 = OpFDiv %404 %406 
                                                      OpStore %268 %407 
                                       f32_4 %408 = OpLoad %50 
                                       f32_3 %409 = OpVectorShuffle %408 %408 0 1 2 
                                       f32_3 %410 = OpLoad %242 
                                         f32 %411 = OpDot %409 %410 
                                Private f32* %412 = OpAccessChain %242 %133 
                                                      OpStore %412 %411 
                                Private f32* %413 = OpAccessChain %242 %133 
                                         f32 %414 = OpLoad %413 
                                Private f32* %415 = OpAccessChain %9 %133 
                                         f32 %416 = OpLoad %415 
                                         f32 %417 = OpFDiv %414 %416 
                                Private f32* %418 = OpAccessChain %9 %133 
                                                      OpStore %418 %417 
                                         f32 %419 = OpLoad %268 
                                         f32 %420 = OpFNegate %419 
                                         f32 %421 = OpFAdd %420 %89 
                                                      OpStore %268 %421 
                                         f32 %422 = OpLoad %268 
                                         f32 %423 = OpFMul %422 %209 
                                         f32 %424 = OpFAdd %423 %211 
                                Private f32* %425 = OpAccessChain %242 %133 
                                                      OpStore %425 %424 
                                         f32 %426 = OpLoad %268 
                                Private f32* %427 = OpAccessChain %242 %133 
                                         f32 %428 = OpLoad %427 
                                         f32 %429 = OpFMul %426 %428 
                                         f32 %430 = OpFAdd %429 %216 
                                Private f32* %431 = OpAccessChain %242 %133 
                                                      OpStore %431 %430 
                                         f32 %432 = OpLoad %268 
                                Private f32* %433 = OpAccessChain %242 %133 
                                         f32 %434 = OpLoad %433 
                                         f32 %435 = OpFMul %432 %434 
                                         f32 %436 = OpFAdd %435 %221 
                                Private f32* %437 = OpAccessChain %242 %133 
                                                      OpStore %437 %436 
                                         f32 %438 = OpLoad %268 
                                Private f32* %439 = OpAccessChain %242 %133 
                                         f32 %440 = OpLoad %439 
                                         f32 %441 = OpFMul %438 %440 
                                         f32 %442 = OpFAdd %441 %226 
                                                      OpStore %268 %442 
                                         f32 %443 = OpLoad %268 
                                         f32 %444 = OpFMul %443 %229 
                                                      OpStore %268 %444 
                                         f32 %445 = OpLoad %268 
                                         f32 %446 = OpExtInst %1 29 %445 
                                                      OpStore %268 %446 
                                Private f32* %447 = OpAccessChain %9 %133 
                                         f32 %448 = OpLoad %447 
                                         f32 %449 = OpFNegate %448 
                                         f32 %450 = OpFAdd %449 %89 
                                Private f32* %451 = OpAccessChain %9 %133 
                                                      OpStore %451 %450 
                                Private f32* %452 = OpAccessChain %9 %133 
                                         f32 %453 = OpLoad %452 
                                         f32 %454 = OpFMul %453 %209 
                                         f32 %455 = OpFAdd %454 %211 
                                Private f32* %456 = OpAccessChain %242 %133 
                                                      OpStore %456 %455 
                                Private f32* %457 = OpAccessChain %9 %133 
                                         f32 %458 = OpLoad %457 
                                Private f32* %459 = OpAccessChain %242 %133 
                                         f32 %460 = OpLoad %459 
                                         f32 %461 = OpFMul %458 %460 
                                         f32 %462 = OpFAdd %461 %216 
                                Private f32* %463 = OpAccessChain %242 %133 
                                                      OpStore %463 %462 
                                Private f32* %464 = OpAccessChain %9 %133 
                                         f32 %465 = OpLoad %464 
                                Private f32* %466 = OpAccessChain %242 %133 
                                         f32 %467 = OpLoad %466 
                                         f32 %468 = OpFMul %465 %467 
                                         f32 %469 = OpFAdd %468 %221 
                                Private f32* %470 = OpAccessChain %242 %133 
                                                      OpStore %470 %469 
                                Private f32* %471 = OpAccessChain %9 %133 
                                         f32 %472 = OpLoad %471 
                                Private f32* %473 = OpAccessChain %242 %133 
                                         f32 %474 = OpLoad %473 
                                         f32 %475 = OpFMul %472 %474 
                                         f32 %476 = OpFAdd %475 %226 
                                Private f32* %477 = OpAccessChain %9 %133 
                                                      OpStore %477 %476 
                                Private f32* %478 = OpAccessChain %9 %133 
                                         f32 %479 = OpLoad %478 
                                         f32 %480 = OpFMul %479 %229 
                                Private f32* %481 = OpAccessChain %9 %133 
                                                      OpStore %481 %480 
                                Private f32* %482 = OpAccessChain %9 %133 
                                         f32 %483 = OpLoad %482 
                                         f32 %484 = OpExtInst %1 29 %483 
                                Private f32* %485 = OpAccessChain %9 %133 
                                                      OpStore %485 %484 
                                Private f32* %486 = OpAccessChain %9 %133 
                                         f32 %487 = OpLoad %486 
                                         f32 %488 = OpFMul %487 %330 
                                Private f32* %489 = OpAccessChain %9 %133 
                                                      OpStore %489 %488 
                                         f32 %490 = OpLoad %268 
                                         f32 %491 = OpFMul %490 %330 
                                Private f32* %492 = OpAccessChain %9 %133 
                                         f32 %493 = OpLoad %492 
                                         f32 %494 = OpFNegate %493 
                                         f32 %495 = OpFAdd %491 %494 
                                Private f32* %496 = OpAccessChain %9 %133 
                                                      OpStore %496 %495 
                                         f32 %497 = OpLoad %259 
                                Private f32* %498 = OpAccessChain %9 %133 
                                         f32 %499 = OpLoad %498 
                                         f32 %500 = OpFMul %497 %499 
                                Private f32* %501 = OpAccessChain %9 %70 
                                         f32 %502 = OpLoad %501 
                                         f32 %503 = OpFAdd %500 %502 
                                Private f32* %504 = OpAccessChain %9 %133 
                                                      OpStore %504 %503 
                                Private f32* %505 = OpAccessChain %9 %133 
                                         f32 %506 = OpLoad %505 
                                         f32 %507 = OpExtInst %1 40 %506 %174 
                                Private f32* %508 = OpAccessChain %9 %133 
                                                      OpStore %508 %507 
                                Private f32* %509 = OpAccessChain %9 %133 
                                         f32 %510 = OpLoad %509 
                                         f32 %511 = OpExtInst %1 37 %510 %346 
                                Private f32* %512 = OpAccessChain %9 %133 
                                                      OpStore %512 %511 
                                       f32_4 %513 = OpLoad %348 
                                       f32_3 %514 = OpVectorShuffle %513 %513 0 1 2 
                                       f32_4 %515 = OpLoad %9 
                                       f32_3 %516 = OpVectorShuffle %515 %515 0 0 0 
                                       f32_3 %517 = OpFNegate %516 
                                       f32_3 %518 = OpFMul %514 %517 
                                                      OpStore %242 %518 
                                       f32_3 %519 = OpLoad %242 
                                       f32_3 %520 = OpFMul %519 %367 
                                                      OpStore %242 %520 
                                       f32_3 %521 = OpLoad %242 
                                       f32_3 %522 = OpExtInst %1 29 %521 
                                                      OpStore %242 %522 
                                Private f32* %523 = OpAccessChain %9 %371 
                                         f32 %524 = OpLoad %523 
                                         f32 %525 = OpLoad %259 
                                         f32 %526 = OpFMul %524 %525 
                                Private f32* %527 = OpAccessChain %9 %133 
                                                      OpStore %527 %526 
                                       f32_4 %528 = OpLoad %9 
                                       f32_3 %529 = OpVectorShuffle %528 %528 0 0 0 
                                       f32_3 %530 = OpLoad %242 
                                       f32_3 %531 = OpFMul %529 %530 
                                       f32_4 %532 = OpLoad %9 
                                       f32_4 %533 = OpVectorShuffle %532 %531 4 5 6 3 
                                                      OpStore %9 %533 
                                       f32_3 %534 = OpLoad %359 
                                         f32 %535 = OpLoad %154 
                                       f32_3 %536 = OpCompositeConstruct %535 %535 %535 
                                       f32_3 %537 = OpFMul %534 %536 
                                       f32_4 %538 = OpLoad %9 
                                       f32_3 %539 = OpVectorShuffle %538 %538 0 1 2 
                                       f32_3 %540 = OpFAdd %537 %539 
                                       f32_4 %541 = OpLoad %9 
                                       f32_4 %542 = OpVectorShuffle %541 %540 4 5 6 3 
                                                      OpStore %9 %542 
                                       f32_4 %543 = OpLoad %44 
                                       f32_3 %544 = OpVectorShuffle %543 %543 0 1 2 
                                       f32_3 %545 = OpLoad %127 
                                       f32_3 %546 = OpVectorShuffle %545 %545 0 0 0 
                                       f32_3 %547 = OpFMul %544 %546 
                                                      OpStore %242 %547 
                                       f32_4 %548 = OpLoad %9 
                                       f32_3 %549 = OpVectorShuffle %548 %548 0 1 2 
                                       f32_3 %550 = OpLoad %242 
                                       f32_3 %551 = OpFMul %549 %550 
                                                      OpStore %242 %551 
                                       f32_4 %552 = OpLoad %9 
                                       f32_3 %553 = OpVectorShuffle %552 %552 0 1 2 
                                       f32_3 %556 = OpFMul %553 %555 
                                       f32_4 %557 = OpLoad %9 
                                       f32_4 %558 = OpVectorShuffle %557 %556 4 5 6 3 
                                                      OpStore %9 %558 
                                       f32_3 %560 = OpLoad %242 
                                                      OpStore %559 %560 
                                       f32_4 %562 = OpLoad %9 
                                       f32_3 %563 = OpVectorShuffle %562 %562 0 1 2 
                                                      OpStore %561 %563 
                                                      OpBranch %178 
                                             %564 = OpLabel 
                                Private f32* %565 = OpAccessChain %50 %70 
                                         f32 %566 = OpLoad %565 
                                         f32 %568 = OpExtInst %1 37 %566 %567 
                                                      OpStore %268 %568 
                                         f32 %570 = OpLoad %268 
                                         f32 %571 = OpFDiv %569 %570 
                                                      OpStore %268 %571 
                                         f32 %572 = OpLoad %268 
                                       f32_3 %573 = OpCompositeConstruct %572 %572 %572 
                                       f32_4 %574 = OpLoad %50 
                                       f32_3 %575 = OpVectorShuffle %574 %574 0 1 2 
                                       f32_3 %576 = OpFMul %573 %575 
                                       f32_3 %577 = OpFAdd %576 %252 
                                       f32_4 %578 = OpLoad %348 
                                       f32_4 %579 = OpVectorShuffle %578 %577 4 5 6 3 
                                                      OpStore %348 %579 
                                       f32_4 %580 = OpLoad %50 
                                       f32_3 %581 = OpVectorShuffle %580 %580 0 1 2 
                                       f32_3 %582 = OpFNegate %581 
                                       f32_4 %583 = OpLoad %348 
                                       f32_3 %584 = OpVectorShuffle %583 %583 0 1 2 
                                         f32 %585 = OpDot %582 %584 
                                Private f32* %587 = OpAccessChain %348 %586 
                                                      OpStore %587 %585 
                              Uniform f32_4* %588 = OpAccessChain %21 %28 
                                       f32_4 %589 = OpLoad %588 
                                       f32_3 %590 = OpVectorShuffle %589 %589 0 1 2 
                                       f32_4 %591 = OpLoad %348 
                                       f32_3 %592 = OpVectorShuffle %591 %591 0 1 2 
                                         f32 %593 = OpDot %590 %592 
                                Private f32* %594 = OpAccessChain %348 %133 
                                                      OpStore %594 %593 
                                       f32_4 %595 = OpLoad %348 
                                       f32_2 %596 = OpVectorShuffle %595 %595 0 3 
                                       f32_2 %597 = OpFNegate %596 
                                       f32_2 %599 = OpFAdd %597 %598 
                                       f32_4 %600 = OpLoad %348 
                                       f32_4 %601 = OpVectorShuffle %600 %599 4 5 2 3 
                                                      OpStore %348 %601 
                                Private f32* %603 = OpAccessChain %348 %70 
                                         f32 %604 = OpLoad %603 
                                         f32 %605 = OpFMul %604 %209 
                                         f32 %606 = OpFAdd %605 %211 
                                                      OpStore %602 %606 
                                Private f32* %607 = OpAccessChain %348 %70 
                                         f32 %608 = OpLoad %607 
                                         f32 %609 = OpLoad %602 
                                         f32 %610 = OpFMul %608 %609 
                                         f32 %611 = OpFAdd %610 %216 
                                                      OpStore %602 %611 
                                Private f32* %612 = OpAccessChain %348 %70 
                                         f32 %613 = OpLoad %612 
                                         f32 %614 = OpLoad %602 
                                         f32 %615 = OpFMul %613 %614 
                                         f32 %616 = OpFAdd %615 %221 
                                                      OpStore %602 %616 
                                Private f32* %618 = OpAccessChain %348 %70 
                                         f32 %619 = OpLoad %618 
                                         f32 %620 = OpLoad %602 
                                         f32 %621 = OpFMul %619 %620 
                                         f32 %622 = OpFAdd %621 %226 
                                Private f32* %623 = OpAccessChain %617 %133 
                                                      OpStore %623 %622 
                                Private f32* %624 = OpAccessChain %617 %133 
                                         f32 %625 = OpLoad %624 
                                         f32 %626 = OpFMul %625 %229 
                                Private f32* %627 = OpAccessChain %617 %133 
                                                      OpStore %627 %626 
                                Private f32* %628 = OpAccessChain %617 %133 
                                         f32 %629 = OpLoad %628 
                                         f32 %630 = OpExtInst %1 29 %629 
                                Private f32* %631 = OpAccessChain %348 %70 
                                                      OpStore %631 %630 
                                Private f32* %632 = OpAccessChain %348 %133 
                                         f32 %633 = OpLoad %632 
                                         f32 %634 = OpFMul %633 %209 
                                         f32 %635 = OpFAdd %634 %211 
                                                      OpStore %602 %635 
                                Private f32* %636 = OpAccessChain %348 %133 
                                         f32 %637 = OpLoad %636 
                                         f32 %638 = OpLoad %602 
                                         f32 %639 = OpFMul %637 %638 
                                         f32 %640 = OpFAdd %639 %216 
                                                      OpStore %602 %640 
                                Private f32* %641 = OpAccessChain %348 %133 
                                         f32 %642 = OpLoad %641 
                                         f32 %643 = OpLoad %602 
                                         f32 %644 = OpFMul %642 %643 
                                         f32 %645 = OpFAdd %644 %221 
                                                      OpStore %602 %645 
                                Private f32* %646 = OpAccessChain %348 %133 
                                         f32 %647 = OpLoad %646 
                                         f32 %648 = OpLoad %602 
                                         f32 %649 = OpFMul %647 %648 
                                         f32 %650 = OpFAdd %649 %226 
                                Private f32* %651 = OpAccessChain %348 %133 
                                                      OpStore %651 %650 
                                       f32_4 %652 = OpLoad %348 
                                       f32_3 %653 = OpVectorShuffle %652 %652 0 1 1 
                                       f32_3 %656 = OpFMul %653 %655 
                                       f32_4 %657 = OpLoad %348 
                                       f32_4 %658 = OpVectorShuffle %657 %656 4 5 6 3 
                                                      OpStore %348 %658 
                                Private f32* %659 = OpAccessChain %348 %133 
                                         f32 %660 = OpLoad %659 
                                         f32 %661 = OpExtInst %1 29 %660 
                                Private f32* %662 = OpAccessChain %348 %133 
                                                      OpStore %662 %661 
                                Private f32* %663 = OpAccessChain %348 %133 
                                         f32 %664 = OpLoad %663 
                                         f32 %665 = OpFMul %664 %330 
                                Private f32* %666 = OpAccessChain %348 %70 
                                         f32 %667 = OpLoad %666 
                                         f32 %668 = OpFAdd %665 %667 
                                Private f32* %669 = OpAccessChain %348 %133 
                                                      OpStore %669 %668 
                                         f32 %670 = OpLoad %268 
                                       f32_2 %671 = OpCompositeConstruct %670 %670 
                                       f32_2 %673 = OpFMul %671 %672 
                                       f32_3 %674 = OpLoad %617 
                                       f32_3 %675 = OpVectorShuffle %674 %673 3 1 4 
                                                      OpStore %617 %675 
                                       f32_4 %676 = OpLoad %50 
                                       f32_3 %677 = OpVectorShuffle %676 %676 0 1 2 
                                       f32_3 %678 = OpLoad %617 
                                       f32_3 %679 = OpVectorShuffle %678 %678 0 0 0 
                                       f32_3 %680 = OpFMul %677 %679 
                                                      OpStore %359 %680 
                                       f32_3 %681 = OpLoad %359 
                                       f32_3 %682 = OpFMul %681 %249 
                                       f32_3 %683 = OpFAdd %682 %252 
                                                      OpStore %359 %683 
                                       f32_3 %684 = OpLoad %359 
                                       f32_3 %685 = OpLoad %359 
                                         f32 %686 = OpDot %684 %685 
                                                      OpStore %268 %686 
                                         f32 %687 = OpLoad %268 
                                         f32 %688 = OpExtInst %1 31 %687 
                                                      OpStore %268 %688 
                                         f32 %689 = OpLoad %268 
                                         f32 %690 = OpFNegate %689 
                                         f32 %691 = OpFAdd %690 %89 
                                                      OpStore %268 %691 
                                         f32 %692 = OpLoad %268 
                                         f32 %693 = OpFMul %692 %264 
                                                      OpStore %268 %693 
                                         f32 %694 = OpLoad %268 
                                         f32 %695 = OpExtInst %1 29 %694 
                                                      OpStore %268 %695 
                                         f32 %696 = OpLoad %268 
                                Private f32* %697 = OpAccessChain %348 %133 
                                         f32 %698 = OpLoad %697 
                                         f32 %699 = OpFMul %696 %698 
                                Private f32* %700 = OpAccessChain %348 %371 
                                         f32 %701 = OpLoad %700 
                                         f32 %702 = OpFNegate %701 
                                         f32 %703 = OpFAdd %699 %702 
                                Private f32* %704 = OpAccessChain %348 %133 
                                                      OpStore %704 %703 
                                Private f32* %705 = OpAccessChain %348 %133 
                                         f32 %706 = OpLoad %705 
                                         f32 %707 = OpExtInst %1 40 %706 %174 
                                Private f32* %708 = OpAccessChain %348 %133 
                                                      OpStore %708 %707 
                                Private f32* %709 = OpAccessChain %348 %133 
                                         f32 %710 = OpLoad %709 
                                         f32 %711 = OpExtInst %1 37 %710 %346 
                                Private f32* %712 = OpAccessChain %348 %133 
                                                      OpStore %712 %711 
                                       f32_4 %713 = OpLoad %44 
                                       f32_3 %714 = OpVectorShuffle %713 %713 0 1 2 
                                       f32_3 %715 = OpLoad %127 
                                       f32_3 %716 = OpVectorShuffle %715 %715 1 1 1 
                                       f32_3 %717 = OpFMul %714 %716 
                                       f32_3 %718 = OpFAdd %717 %355 
                                                      OpStore %359 %718 
                                       f32_4 %719 = OpLoad %348 
                                       f32_3 %720 = OpVectorShuffle %719 %719 0 0 0 
                                       f32_3 %721 = OpFNegate %720 
                                       f32_3 %722 = OpLoad %359 
                                       f32_3 %723 = OpFMul %721 %722 
                                       f32_4 %724 = OpLoad %348 
                                       f32_4 %725 = OpVectorShuffle %724 %723 4 5 6 3 
                                                      OpStore %348 %725 
                                       f32_4 %726 = OpLoad %348 
                                       f32_3 %727 = OpVectorShuffle %726 %726 0 1 2 
                                       f32_3 %728 = OpFMul %727 %367 
                                       f32_4 %729 = OpLoad %348 
                                       f32_4 %730 = OpVectorShuffle %729 %728 4 5 6 3 
                                                      OpStore %348 %730 
                                       f32_4 %731 = OpLoad %348 
                                       f32_3 %732 = OpVectorShuffle %731 %731 0 1 2 
                                       f32_3 %733 = OpExtInst %1 29 %732 
                                       f32_4 %734 = OpLoad %9 
                                       f32_4 %735 = OpVectorShuffle %734 %733 4 5 6 3 
                                                      OpStore %9 %735 
                                Private f32* %736 = OpAccessChain %617 %371 
                                         f32 %737 = OpLoad %736 
                                         f32 %738 = OpLoad %268 
                                         f32 %739 = OpFMul %737 %738 
                                                      OpStore %268 %739 
                                       f32_4 %740 = OpLoad %9 
                                       f32_3 %741 = OpVectorShuffle %740 %740 0 1 2 
                                         f32 %742 = OpLoad %268 
                                       f32_3 %743 = OpCompositeConstruct %742 %742 %742 
                                       f32_3 %744 = OpFMul %741 %743 
                                       f32_4 %745 = OpLoad %348 
                                       f32_4 %746 = OpVectorShuffle %745 %744 4 5 6 3 
                                                      OpStore %348 %746 
                                       f32_4 %747 = OpLoad %44 
                                       f32_3 %748 = OpVectorShuffle %747 %747 0 1 2 
                                       f32_3 %749 = OpLoad %127 
                                       f32_3 %750 = OpVectorShuffle %749 %749 0 0 0 
                                       f32_3 %751 = OpFMul %748 %750 
                                       f32_3 %752 = OpFAdd %751 %555 
                                                      OpStore %359 %752 
                                       f32_4 %753 = OpLoad %348 
                                       f32_3 %754 = OpVectorShuffle %753 %753 0 1 2 
                                       f32_3 %755 = OpLoad %359 
                                       f32_3 %756 = OpFMul %754 %755 
                                                      OpStore %242 %756 
                                       f32_3 %757 = OpLoad %242 
                                                      OpStore %559 %757 
                                       f32_4 %758 = OpLoad %9 
                                       f32_3 %759 = OpVectorShuffle %758 %758 0 1 2 
                                                      OpStore %561 %759 
                                                      OpBranch %178 
                                             %178 = OpLabel 
                                Uniform f32* %761 = OpAccessChain %21 %760 %133 
                                         f32 %762 = OpLoad %761 
                                Uniform f32* %763 = OpAccessChain %21 %760 %70 
                                         f32 %764 = OpLoad %763 
                                Uniform f32* %765 = OpAccessChain %21 %760 %371 
                                         f32 %766 = OpLoad %765 
                                       f32_3 %767 = OpCompositeConstruct %762 %764 %766 
                                Uniform f32* %768 = OpAccessChain %21 %760 %133 
                                         f32 %769 = OpLoad %768 
                                Uniform f32* %770 = OpAccessChain %21 %760 %70 
                                         f32 %771 = OpLoad %770 
                                Uniform f32* %772 = OpAccessChain %21 %760 %371 
                                         f32 %773 = OpLoad %772 
                                       f32_3 %774 = OpCompositeConstruct %769 %771 %773 
                                       f32_3 %775 = OpFMul %767 %774 
                                                      OpStore %127 %775 
                                       f32_3 %776 = OpLoad %127 
                                       f32_3 %777 = OpLoad %561 
                                       f32_3 %778 = OpFMul %776 %777 
                                       f32_3 %779 = OpLoad %559 
                                       f32_3 %780 = OpFAdd %778 %779 
                                                      OpStore %127 %780 
                                       f32_3 %781 = OpLoad %127 
                                Uniform f32* %783 = OpAccessChain %21 %782 
                                         f32 %784 = OpLoad %783 
                                       f32_3 %785 = OpCompositeConstruct %784 %784 %784 
                                       f32_3 %786 = OpFMul %781 %785 
                                                      OpStore %127 %786 
                              Uniform f32_4* %788 = OpAccessChain %21 %28 
                                       f32_4 %789 = OpLoad %788 
                                       f32_3 %790 = OpVectorShuffle %789 %789 0 1 2 
                                       f32_4 %791 = OpLoad %50 
                                       f32_3 %792 = OpVectorShuffle %791 %791 0 1 2 
                                       f32_3 %793 = OpFNegate %792 
                                         f32 %794 = OpDot %790 %793 
                                                      OpStore %787 %794 
                                         f32 %795 = OpLoad %787 
                                         f32 %796 = OpLoad %787 
                                         f32 %797 = OpFMul %795 %796 
                                                      OpStore %787 %797 
                                         f32 %798 = OpLoad %787 
                                         f32 %800 = OpFMul %798 %799 
                                         f32 %801 = OpFAdd %800 %799 
                                                      OpStore %787 %801 
                                         f32 %803 = OpLoad %787 
                                       f32_3 %804 = OpCompositeConstruct %803 %803 %803 
                                       f32_3 %805 = OpLoad %559 
                                       f32_3 %806 = OpFMul %804 %805 
                                                      OpStore %802 %806 
                                       f32_3 %807 = OpLoad %802 
                                Uniform f32* %808 = OpAccessChain %21 %782 
                                         f32 %809 = OpLoad %808 
                                       f32_3 %810 = OpCompositeConstruct %809 %809 %809 
                                       f32_3 %811 = OpFMul %807 %810 
                                                      OpStore %802 %811 
                              Uniform f32_4* %812 = OpAccessChain %21 %46 
                                       f32_4 %813 = OpLoad %812 
                                       f32_3 %814 = OpVectorShuffle %813 %813 0 1 2 
                              Uniform f32_4* %815 = OpAccessChain %21 %46 
                                       f32_4 %816 = OpLoad %815 
                                       f32_3 %817 = OpVectorShuffle %816 %816 0 1 2 
                                         f32 %818 = OpDot %814 %817 
                                                      OpStore %787 %818 
                                         f32 %819 = OpLoad %787 
                                         f32 %820 = OpExtInst %1 31 %819 
                                                      OpStore %787 %820 
                                         f32 %821 = OpLoad %787 
                                         f32 %822 = OpExtInst %1 40 %821 %330 
                                                      OpStore %787 %822 
                                         f32 %823 = OpLoad %787 
                                         f32 %824 = OpExtInst %1 37 %823 %89 
                                                      OpStore %787 %824 
                                       f32_3 %825 = OpLoad %561 
                                                      OpStore %561 %825 
                                       f32_3 %826 = OpLoad %561 
                                       f32_3 %827 = OpCompositeConstruct %174 %174 %174 
                                       f32_3 %828 = OpCompositeConstruct %89 %89 %89 
                                       f32_3 %829 = OpExtInst %1 43 %826 %827 %828 
                                                      OpStore %561 %829 
                                       f32_3 %831 = OpLoad %561 
                              Uniform f32_4* %832 = OpAccessChain %21 %46 
                                       f32_4 %833 = OpLoad %832 
                                       f32_3 %834 = OpVectorShuffle %833 %833 0 1 2 
                                       f32_3 %835 = OpFMul %831 %834 
                                                      OpStore %830 %835 
                                       f32_3 %836 = OpLoad %830 
                                       f32_3 %839 = OpFMul %836 %838 
                                                      OpStore %830 %839 
                                       f32_3 %840 = OpLoad %830 
                                         f32 %841 = OpLoad %787 
                                       f32_3 %842 = OpCompositeConstruct %841 %841 %841 
                                       f32_3 %843 = OpFDiv %840 %842 
                                                      OpStore %830 %843 
                                       f32_3 %846 = OpLoad %127 
                                       f32_3 %847 = OpExtInst %1 31 %846 
                                                      OpStore vs_TEXCOORD1 %847 
                                       f32_3 %849 = OpLoad %802 
                                       f32_3 %850 = OpExtInst %1 31 %849 
                                                      OpStore vs_TEXCOORD2 %850 
                                       f32_3 %852 = OpLoad %830 
                                       f32_3 %853 = OpExtInst %1 31 %852 
                                                      OpStore vs_TEXCOORD3 %853 
                                       f32_4 %855 = OpLoad %50 
                                       f32_3 %856 = OpVectorShuffle %855 %855 0 1 2 
                                       f32_3 %857 = OpFNegate %856 
                                                      OpStore vs_TEXCOORD0 %857 
                                 Output f32* %859 = OpAccessChain %74 %28 %70 
                                         f32 %860 = OpLoad %859 
                                         f32 %861 = OpFNegate %860 
                                 Output f32* %862 = OpAccessChain %74 %28 %70 
                                                      OpStore %862 %861 
                                                      OpReturn
                                                      OpFunctionEnd
; SPIR-V
; Version: 1.0
; Generator: Khronos Glslang Reference Front End; 6
; Bound: 189
; Schema: 0
                                           OpCapability Shader 
                                    %1 = OpExtInstImport "GLSL.std.450" 
                                           OpMemoryModel Logical GLSL450 
                                           OpEntryPoint Fragment %4 "main" %37 %153 %155 %165 %173 
                                           OpExecutionMode %4 OriginUpperLeft 
                                           OpName vs_TEXCOORD0 "vs_TEXCOORD0" 
                                           OpName vs_TEXCOORD1 "vs_TEXCOORD1" 
                                           OpName vs_TEXCOORD2 "vs_TEXCOORD2" 
                                           OpName vs_TEXCOORD3 "vs_TEXCOORD3" 
                                           OpDecorate %9 RelaxedPrecision 
                                           OpMemberDecorate %11 0 Offset 11 
                                           OpMemberDecorate %11 1 RelaxedPrecision 
                                           OpMemberDecorate %11 1 Offset 11 
                                           OpMemberDecorate %11 2 RelaxedPrecision 
                                           OpMemberDecorate %11 2 Offset 11 
                                           OpDecorate %11 Block 
                                           OpDecorate %13 DescriptorSet 13 
                                           OpDecorate %13 Binding 13 
                                           OpDecorate %18 RelaxedPrecision 
                                           OpDecorate %19 RelaxedPrecision 
                                           OpDecorate %25 RelaxedPrecision 
                                           OpDecorate %27 RelaxedPrecision 
                                           OpDecorate %30 RelaxedPrecision 
                                           OpDecorate %31 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD0 Location 37 
                                           OpDecorate %50 RelaxedPrecision 
                                           OpDecorate %61 RelaxedPrecision 
                                           OpDecorate %64 RelaxedPrecision 
                                           OpDecorate %67 RelaxedPrecision 
                                           OpDecorate %68 RelaxedPrecision 
                                           OpDecorate %71 RelaxedPrecision 
                                           OpDecorate %74 RelaxedPrecision 
                                           OpDecorate %75 RelaxedPrecision 
                                           OpDecorate %78 RelaxedPrecision 
                                           OpDecorate %79 RelaxedPrecision 
                                           OpDecorate %81 RelaxedPrecision 
                                           OpDecorate %83 RelaxedPrecision 
                                           OpDecorate %84 RelaxedPrecision 
                                           OpDecorate %86 RelaxedPrecision 
                                           OpDecorate %88 RelaxedPrecision 
                                           OpDecorate %90 RelaxedPrecision 
                                           OpDecorate %92 RelaxedPrecision 
                                           OpDecorate %93 RelaxedPrecision 
                                           OpDecorate %94 RelaxedPrecision 
                                           OpDecorate %97 RelaxedPrecision 
                                           OpDecorate %101 RelaxedPrecision 
                                           OpDecorate %102 RelaxedPrecision 
                                           OpDecorate %103 RelaxedPrecision 
                                           OpDecorate %104 RelaxedPrecision 
                                           OpDecorate %106 RelaxedPrecision 
                                           OpDecorate %107 RelaxedPrecision 
                                           OpDecorate %110 RelaxedPrecision 
                                           OpDecorate %111 RelaxedPrecision 
                                           OpDecorate %114 RelaxedPrecision 
                                           OpDecorate %116 RelaxedPrecision 
                                           OpDecorate %119 RelaxedPrecision 
                                           OpDecorate %121 RelaxedPrecision 
                                           OpDecorate %122 RelaxedPrecision 
                                           OpDecorate %125 RelaxedPrecision 
                                           OpDecorate %126 RelaxedPrecision 
                                           OpDecorate %129 RelaxedPrecision 
                                           OpDecorate %131 RelaxedPrecision 
                                           OpDecorate %134 RelaxedPrecision 
                                           OpDecorate %135 RelaxedPrecision 
                                           OpDecorate %143 RelaxedPrecision 
                                           OpDecorate %144 RelaxedPrecision 
                                           OpDecorate %152 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD1 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD1 Location 153 
                                           OpDecorate %154 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD2 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD2 Location 155 
                                           OpDecorate %156 RelaxedPrecision 
                                           OpDecorate %157 RelaxedPrecision 
                                           OpDecorate %158 RelaxedPrecision 
                                           OpDecorate %159 RelaxedPrecision 
                                           OpDecorate %160 RelaxedPrecision 
                                           OpDecorate %161 RelaxedPrecision 
                                           OpDecorate %162 RelaxedPrecision 
                                           OpDecorate %163 RelaxedPrecision 
                                           OpDecorate %164 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD3 RelaxedPrecision 
                                           OpDecorate vs_TEXCOORD3 Location 165 
                                           OpDecorate %166 RelaxedPrecision 
                                           OpDecorate %167 RelaxedPrecision 
                                           OpDecorate %168 RelaxedPrecision 
                                           OpDecorate %169 RelaxedPrecision 
                                           OpDecorate %170 RelaxedPrecision 
                                           OpDecorate %171 RelaxedPrecision 
                                           OpDecorate %173 RelaxedPrecision 
                                           OpDecorate %173 Location 173 
                                           OpDecorate %179 RelaxedPrecision 
                                           OpDecorate %181 RelaxedPrecision 
                                           OpDecorate %182 RelaxedPrecision 
                                    %2 = OpTypeVoid 
                                    %3 = OpTypeFunction %2 
                                    %6 = OpTypeFloat 32 
                                    %7 = OpTypeVector %6 2 
                                    %8 = OpTypePointer Private %7 
                     Private f32_2* %9 = OpVariable Private 
                                   %10 = OpTypeVector %6 4 
                                   %11 = OpTypeStruct %10 %6 %6 
                                   %12 = OpTypePointer Uniform %11 
Uniform struct {f32_4; f32; f32;}* %13 = OpVariable Uniform 
                                   %14 = OpTypeInt 32 1 
                               i32 %15 = OpConstant 1 
                                   %16 = OpTypePointer Uniform %6 
                                   %20 = OpTypeInt 32 0 
                               u32 %21 = OpConstant 0 
                                   %22 = OpTypePointer Private %6 
                               f32 %26 = OpConstant 3.674022E-40 
                                   %33 = OpTypeVector %6 3 
                                   %34 = OpTypePointer Private %33 
                    Private f32_3* %35 = OpVariable Private 
                                   %36 = OpTypePointer Input %33 
             Input f32_3* vs_TEXCOORD0 = OpVariable Input 
                    Private f32_3* %50 = OpVariable Private 
                               i32 %51 = OpConstant 0 
                                   %52 = OpTypePointer Uniform %10 
                               f32 %62 = OpConstant 3.674022E-40 
                               f32 %63 = OpConstant 3.674022E-40 
                               i32 %72 = OpConstant 2 
                      Private f32* %81 = OpVariable Private 
                               f32 %85 = OpConstant 3.674022E-40 
                               f32 %87 = OpConstant 3.674022E-40 
                               u32 %95 = OpConstant 1 
                               f32 %98 = OpConstant 3.674022E-40 
                               f32 %99 = OpConstant 3.674022E-40 
                            f32_2 %100 = OpConstantComposite %98 %99 
                              f32 %115 = OpConstant 3.674022E-40 
                              f32 %130 = OpConstant 3.674022E-40 
                              f32 %139 = OpConstant 3.674022E-40 
                                  %146 = OpTypeBool 
                                  %147 = OpTypePointer Private %146 
                    Private bool* %148 = OpVariable Private 
                   Private f32_3* %152 = OpVariable Private 
             Input f32_3* vs_TEXCOORD1 = OpVariable Input 
             Input f32_3* vs_TEXCOORD2 = OpVariable Input 
             Input f32_3* vs_TEXCOORD3 = OpVariable Input 
                                  %172 = OpTypePointer Output %10 
                    Output f32_4* %173 = OpVariable Output 
                                  %175 = OpTypePointer Function %33 
                              u32 %185 = OpConstant 3 
                                  %186 = OpTypePointer Output %6 
                               void %4 = OpFunction None %3 
                                    %5 = OpLabel 
                  Function f32_3* %176 = OpVariable Function 
                      Uniform f32* %17 = OpAccessChain %13 %15 
                               f32 %18 = OpLoad %17 
                               f32 %19 = OpExtInst %1 30 %18 
                      Private f32* %23 = OpAccessChain %9 %21 
                                           OpStore %23 %19 
                      Private f32* %24 = OpAccessChain %9 %21 
                               f32 %25 = OpLoad %24 
                               f32 %27 = OpFMul %25 %26 
                      Private f32* %28 = OpAccessChain %9 %21 
                                           OpStore %28 %27 
                      Private f32* %29 = OpAccessChain %9 %21 
                               f32 %30 = OpLoad %29 
                               f32 %31 = OpExtInst %1 29 %30 
                      Private f32* %32 = OpAccessChain %9 %21 
                                           OpStore %32 %31 
                             f32_3 %38 = OpLoad vs_TEXCOORD0 
                             f32_3 %39 = OpLoad vs_TEXCOORD0 
                               f32 %40 = OpDot %38 %39 
                      Private f32* %41 = OpAccessChain %35 %21 
                                           OpStore %41 %40 
                      Private f32* %42 = OpAccessChain %35 %21 
                               f32 %43 = OpLoad %42 
                               f32 %44 = OpExtInst %1 32 %43 
                      Private f32* %45 = OpAccessChain %35 %21 
                                           OpStore %45 %44 
                             f32_3 %46 = OpLoad %35 
                             f32_3 %47 = OpVectorShuffle %46 %46 0 0 0 
                             f32_3 %48 = OpLoad vs_TEXCOORD0 
                             f32_3 %49 = OpFMul %47 %48 
                                           OpStore %35 %49 
                    Uniform f32_4* %53 = OpAccessChain %13 %51 
                             f32_4 %54 = OpLoad %53 
                             f32_3 %55 = OpVectorShuffle %54 %54 0 1 2 
                             f32_3 %56 = OpLoad %35 
                             f32_3 %57 = OpFNegate %56 
                               f32 %58 = OpDot %55 %57 
                      Private f32* %59 = OpAccessChain %50 %21 
                                           OpStore %59 %58 
                      Private f32* %60 = OpAccessChain %50 %21 
                               f32 %61 = OpLoad %60 
                               f32 %64 = OpExtInst %1 43 %61 %62 %63 
                      Private f32* %65 = OpAccessChain %50 %21 
                                           OpStore %65 %64 
                      Private f32* %66 = OpAccessChain %50 %21 
                               f32 %67 = OpLoad %66 
                               f32 %68 = OpExtInst %1 30 %67 
                      Private f32* %69 = OpAccessChain %50 %21 
                                           OpStore %69 %68 
                      Private f32* %70 = OpAccessChain %50 %21 
                               f32 %71 = OpLoad %70 
                      Uniform f32* %73 = OpAccessChain %13 %72 
                               f32 %74 = OpLoad %73 
                               f32 %75 = OpFMul %71 %74 
                      Private f32* %76 = OpAccessChain %50 %21 
                                           OpStore %76 %75 
                      Private f32* %77 = OpAccessChain %50 %21 
                               f32 %78 = OpLoad %77 
                               f32 %79 = OpExtInst %1 29 %78 
                      Private f32* %80 = OpAccessChain %50 %21 
                                           OpStore %80 %79 
                      Private f32* %82 = OpAccessChain %50 %21 
                               f32 %83 = OpLoad %82 
                               f32 %84 = OpFNegate %83 
                               f32 %86 = OpFMul %84 %85 
                               f32 %88 = OpFAdd %86 %87 
                                           OpStore %81 %88 
                      Private f32* %89 = OpAccessChain %50 %21 
                               f32 %90 = OpLoad %89 
                      Private f32* %91 = OpAccessChain %50 %21 
                               f32 %92 = OpLoad %91 
                               f32 %93 = OpFMul %90 %92 
                               f32 %94 = OpFAdd %93 %63 
                      Private f32* %96 = OpAccessChain %9 %95 
                                           OpStore %96 %94 
                             f32_2 %97 = OpLoad %9 
                            f32_2 %101 = OpFMul %97 %100 
                                           OpStore %9 %101 
                              f32 %102 = OpLoad %81 
                              f32 %103 = OpExtInst %1 30 %102 
                                           OpStore %81 %103 
                              f32 %104 = OpLoad %81 
                     Private f32* %105 = OpAccessChain %9 %21 
                              f32 %106 = OpLoad %105 
                              f32 %107 = OpFMul %104 %106 
                     Private f32* %108 = OpAccessChain %9 %21 
                                           OpStore %108 %107 
                     Private f32* %109 = OpAccessChain %9 %21 
                              f32 %110 = OpLoad %109 
                              f32 %111 = OpExtInst %1 29 %110 
                     Private f32* %112 = OpAccessChain %9 %21 
                                           OpStore %112 %111 
                     Private f32* %113 = OpAccessChain %9 %21 
                              f32 %114 = OpLoad %113 
                              f32 %116 = OpExtInst %1 40 %114 %115 
                     Private f32* %117 = OpAccessChain %9 %21 
                                           OpStore %117 %116 
                     Private f32* %118 = OpAccessChain %9 %95 
                              f32 %119 = OpLoad %118 
                     Private f32* %120 = OpAccessChain %9 %21 
                              f32 %121 = OpLoad %120 
                              f32 %122 = OpFDiv %119 %121 
                     Private f32* %123 = OpAccessChain %9 %21 
                                           OpStore %123 %122 
                     Private f32* %124 = OpAccessChain %9 %21 
                              f32 %125 = OpLoad %124 
                              f32 %126 = OpExtInst %1 30 %125 
                     Private f32* %127 = OpAccessChain %9 %21 
                                           OpStore %127 %126 
                     Private f32* %128 = OpAccessChain %9 %21 
                              f32 %129 = OpLoad %128 
                              f32 %131 = OpFMul %129 %130 
                     Private f32* %132 = OpAccessChain %9 %21 
                                           OpStore %132 %131 
                     Private f32* %133 = OpAccessChain %9 %21 
                              f32 %134 = OpLoad %133 
                              f32 %135 = OpExtInst %1 29 %134 
                     Private f32* %136 = OpAccessChain %9 %21 
                                           OpStore %136 %135 
                     Private f32* %137 = OpAccessChain %35 %95 
                              f32 %138 = OpLoad %137 
                              f32 %140 = OpFMul %138 %139 
                     Private f32* %141 = OpAccessChain %50 %21 
                                           OpStore %141 %140 
                     Private f32* %142 = OpAccessChain %50 %21 
                              f32 %143 = OpLoad %142 
                              f32 %144 = OpExtInst %1 43 %143 %62 %63 
                     Private f32* %145 = OpAccessChain %50 %21 
                                           OpStore %145 %144 
                     Private f32* %149 = OpAccessChain %35 %95 
                              f32 %150 = OpLoad %149 
                             bool %151 = OpFOrdLessThan %150 %62 
                                           OpStore %148 %151 
                            f32_3 %154 = OpLoad vs_TEXCOORD1 
                            f32_3 %156 = OpLoad vs_TEXCOORD2 
                            f32_3 %157 = OpFNegate %156 
                            f32_3 %158 = OpFAdd %154 %157 
                                           OpStore %152 %158 
                            f32_3 %159 = OpLoad %50 
                            f32_3 %160 = OpVectorShuffle %159 %159 0 0 0 
                            f32_3 %161 = OpLoad %152 
                            f32_3 %162 = OpFMul %160 %161 
                            f32_3 %163 = OpLoad vs_TEXCOORD2 
                            f32_3 %164 = OpFAdd %162 %163 
                                           OpStore %50 %164 
                            f32_3 %166 = OpLoad vs_TEXCOORD3 
                            f32_2 %167 = OpLoad %9 
                            f32_3 %168 = OpVectorShuffle %167 %167 0 0 0 
                            f32_3 %169 = OpFMul %166 %168 
                            f32_3 %170 = OpLoad %50 
                            f32_3 %171 = OpFAdd %169 %170 
                                           OpStore %152 %171 
                             bool %174 = OpLoad %148 
                                           OpSelectionMerge %178 None 
                                           OpBranchConditional %174 %177 %180 
                                  %177 = OpLabel 
                            f32_3 %179 = OpLoad %152 
                                           OpStore %176 %179 
                                           OpBranch %178 
                                  %180 = OpLabel 
                            f32_3 %181 = OpLoad %50 
                                           OpStore %176 %181 
                                           OpBranch %178 
                                  %178 = OpLabel 
                            f32_3 %182 = OpLoad %176 
                            f32_4 %183 = OpLoad %173 
                            f32_4 %184 = OpVectorShuffle %183 %182 4 5 6 3 
                                           OpStore %173 %184 
                      Output f32* %187 = OpAccessChain %173 %185 
                                           OpStore %187 %63 
                                           OpReturn
                                           OpFunctionEnd
"
}
}
Program "fp" {
SubProgram "gles3 hw_tier00 " {
Local Keywords { "_SUNDISK_NONE" }
""
}
SubProgram "gles3 hw_tier01 " {
Local Keywords { "_SUNDISK_NONE" }
""
}
SubProgram "gles3 hw_tier02 " {
Local Keywords { "_SUNDISK_NONE" }
""
}
SubProgram "vulkan hw_tier00 " {
Local Keywords { "_SUNDISK_NONE" }
""
}
SubProgram "vulkan hw_tier01 " {
Local Keywords { "_SUNDISK_NONE" }
""
}
SubProgram "vulkan hw_tier02 " {
Local Keywords { "_SUNDISK_NONE" }
""
}
SubProgram "gles3 hw_tier00 " {
Local Keywords { "_SUNDISK_SIMPLE" }
""
}
SubProgram "gles3 hw_tier01 " {
Local Keywords { "_SUNDISK_SIMPLE" }
""
}
SubProgram "gles3 hw_tier02 " {
Local Keywords { "_SUNDISK_SIMPLE" }
""
}
SubProgram "vulkan hw_tier00 " {
Local Keywords { "_SUNDISK_SIMPLE" }
""
}
SubProgram "vulkan hw_tier01 " {
Local Keywords { "_SUNDISK_SIMPLE" }
""
}
SubProgram "vulkan hw_tier02 " {
Local Keywords { "_SUNDISK_SIMPLE" }
""
}
SubProgram "gles3 hw_tier00 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
""
}
SubProgram "gles3 hw_tier01 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
""
}
SubProgram "gles3 hw_tier02 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
""
}
SubProgram "vulkan hw_tier00 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
""
}
SubProgram "vulkan hw_tier01 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
""
}
SubProgram "vulkan hw_tier02 " {
Local Keywords { "_SUNDISK_HIGH_QUALITY" }
""
}
}
}
}
CustomEditor "SkyboxProceduralShaderGUI"
}