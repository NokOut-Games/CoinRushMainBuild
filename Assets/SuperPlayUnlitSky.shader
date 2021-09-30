Shader "SuperPlay/UnlitSky" {
	Properties {
		_Gradientspread ("Gradient spread", Range(0.1, 10)) = 1
		_Gradientoffset ("Gradient offset", Range(-1, 1)) = 1
		_Topcolor ("Top color", Vector) = (0.02919188,0.08557343,0.1509434,0)
		_horizon_color ("horizon_color", Vector) = (0.7122642,1,0.9645524,0)
		_Sunsize ("Sun size", Range(0, 0.25)) = 0
		_SunSoftness ("Sun Softness", Range(0, 0.5)) = 0.1
		_SunPosition ("Sun Position", Vector) = (0,0,0,0)
		_SunColor ("Sun Color", Vector) = (1,0.8617569,0.3915094,0)
		[Toggle(_SUN_ON)] _Sun ("Sun", Float) = 0
	}
	SubShader {
		Tags { "QUEUE" = "Geometry+449" "RenderType" = "Opaque" }
		Pass {
			Name "Unlit"
			Tags { "LIGHTMODE" = "ALWAYS" "QUEUE" = "Geometry+449" "RenderType" = "Opaque" }
			GpuProgramID 56195
			Program "vp" {
				SubProgram "gles hw_tier00 " {
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					attribute mediump vec4 in_POSITION0;
					attribute mediump vec2 in_TEXCOORD0;
					varying mediump vec3 vs_COLOR0;
					varying mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					varying mediump vec3 vs_COLOR0;
					#define SV_Target0 gl_FragData[0]
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier01 " {
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					attribute mediump vec4 in_POSITION0;
					attribute mediump vec2 in_TEXCOORD0;
					varying mediump vec3 vs_COLOR0;
					varying mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					varying mediump vec3 vs_COLOR0;
					#define SV_Target0 gl_FragData[0]
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier02 " {
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					attribute mediump vec4 in_POSITION0;
					attribute mediump vec2 in_TEXCOORD0;
					varying mediump vec3 vs_COLOR0;
					varying mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					varying mediump vec3 vs_COLOR0;
					#define SV_Target0 gl_FragData[0]
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier00 " {
					"!!GLES3
					#ifdef VERTEX
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
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					in mediump vec4 in_POSITION0;
					in mediump vec2 in_TEXCOORD0;
					out mediump vec3 vs_COLOR0;
					out mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					in mediump vec3 vs_COLOR0;
					layout(location = 0) out mediump vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier01 " {
					"!!GLES3
					#ifdef VERTEX
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
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					in mediump vec4 in_POSITION0;
					in mediump vec2 in_TEXCOORD0;
					out mediump vec3 vs_COLOR0;
					out mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					in mediump vec3 vs_COLOR0;
					layout(location = 0) out mediump vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier02 " {
					"!!GLES3
					#ifdef VERTEX
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
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					in mediump vec4 in_POSITION0;
					in mediump vec2 in_TEXCOORD0;
					out mediump vec3 vs_COLOR0;
					out mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					in mediump vec3 vs_COLOR0;
					layout(location = 0) out mediump vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier00 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					attribute mediump vec4 in_POSITION0;
					attribute mediump vec2 in_TEXCOORD0;
					varying mediump vec3 vs_COLOR0;
					varying mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					varying mediump vec3 vs_COLOR0;
					#define SV_Target0 gl_FragData[0]
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier01 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					attribute mediump vec4 in_POSITION0;
					attribute mediump vec2 in_TEXCOORD0;
					varying mediump vec3 vs_COLOR0;
					varying mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					varying mediump vec3 vs_COLOR0;
					#define SV_Target0 gl_FragData[0]
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier02 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					attribute mediump vec4 in_POSITION0;
					attribute mediump vec2 in_TEXCOORD0;
					varying mediump vec3 vs_COLOR0;
					varying mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					varying mediump vec3 vs_COLOR0;
					#define SV_Target0 gl_FragData[0]
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier00 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES3
					#ifdef VERTEX
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
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					in mediump vec4 in_POSITION0;
					in mediump vec2 in_TEXCOORD0;
					out mediump vec3 vs_COLOR0;
					out mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					in mediump vec3 vs_COLOR0;
					layout(location = 0) out mediump vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier01 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES3
					#ifdef VERTEX
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
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					in mediump vec4 in_POSITION0;
					in mediump vec2 in_TEXCOORD0;
					out mediump vec3 vs_COLOR0;
					out mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					in mediump vec3 vs_COLOR0;
					layout(location = 0) out mediump vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier02 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES3
					#ifdef VERTEX
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
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	mediump vec4 _horizon_color;
					uniform 	mediump vec4 _Topcolor;
					uniform 	mediump float _Gradientoffset;
					uniform 	mediump float _Gradientspread;
					in mediump vec4 in_POSITION0;
					in mediump vec2 in_TEXCOORD0;
					out mediump vec3 vs_COLOR0;
					out mediump vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					mediump float u_xlat16_2;
					mediump vec3 u_xlat16_5;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat16_2 = in_TEXCOORD0.y + _Gradientoffset;
					    u_xlat16_2 = log2(u_xlat16_2);
					    u_xlat16_2 = u_xlat16_2 * _Gradientspread;
					    u_xlat16_2 = exp2(u_xlat16_2);
					    u_xlat16_2 = min(u_xlat16_2, 1.0);
					    u_xlat16_5.xyz = (-_horizon_color.xyz) + _Topcolor.xyz;
					    vs_COLOR0.xyz = vec3(u_xlat16_2) * u_xlat16_5.xyz + _horizon_color.xyz;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					in mediump vec3 vs_COLOR0;
					layout(location = 0) out mediump vec4 SV_Target0;
					void main()
					{
					    SV_Target0.xyz = vs_COLOR0.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}
					
					#endif"
				}
			}
			Program "fp" {
				SubProgram "gles hw_tier00 " {
					"!!GLES"
				}
				SubProgram "gles hw_tier01 " {
					"!!GLES"
				}
				SubProgram "gles hw_tier02 " {
					"!!GLES"
				}
				SubProgram "gles3 hw_tier00 " {
					"!!GLES3"
				}
				SubProgram "gles3 hw_tier01 " {
					"!!GLES3"
				}
				SubProgram "gles3 hw_tier02 " {
					"!!GLES3"
				}
				SubProgram "gles hw_tier00 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES"
				}
				SubProgram "gles hw_tier01 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES"
				}
				SubProgram "gles hw_tier02 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES"
				}
				SubProgram "gles3 hw_tier00 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES3"
				}
				SubProgram "gles3 hw_tier01 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES3"
				}
				SubProgram "gles3 hw_tier02 " {
					Keywords { "_SP_SHADER_QUALITY_LOW" }
					"!!GLES3"
				}
			}
		}
	}
}