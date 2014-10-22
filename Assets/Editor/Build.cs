using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;

/// <summary>
/// Used to build Unity project from command line.
/// Must be in Assets/Editor/ folder in order to be accessible via -executeMethod param of Unity CLI.
///
/// Also contains some Unity menus extensions, not only for building.
///
/// TODO: rename to WBTools
/// </summary>
public class WBBuild : MonoBehaviour {
	
	/// <summary>
	/// Builds Unity project.
	///
	/// <param name="buildPath">
	/// where to build, must be folder for iOS, and *.apk file for Android. Containing folder must already exist.
	/// </param>
	///
	/// <param name="buildTarget">
	/// Target platform (see Unity Editor's BuildTarget enum).
	/// </param>
	/// --buildTarget 
	/// </summary>
	static void BuildImpl(string buildPath, BuildTarget buildTarget, BuildOptions options)
	{
		List<string> scenes = new List<string>();            
		for( int sceneIndex = 0; sceneIndex < EditorBuildSettings.scenes.Length; ++sceneIndex )
		{
			if( EditorBuildSettings.scenes[sceneIndex].enabled )
			{
				scenes.Add( EditorBuildSettings.scenes[sceneIndex].path );
			}
		}
		
		// Always select platform build when using this system
		//EditorMods.PlatformBuildPro();
		BuildPipeline.BuildPlayer(scenes.ToArray(), buildPath, buildTarget, options);
	}
	
	/// <summary>
	/// Parses CLI arguments & invokes BuildImpl() to Build Unity project.
	///
	/// Parameters:
	/// --buildPath FULL_PATH - where to build, must be folder for iOS, and *.apk file for Android. Containing folder must already exist.
	///
	/// --buildTarget name of the target platform, case insensitive, can be iPhone, Android, etc (see Unity Editor's BuildTarget enum).
	///
	/// --strippingLevel level of stripping to aplly to build (Disabled, StripAssemblies, StripByteCode, UseMicroMSCorlib)
	///
	/// --androidSubTarget to build for a specific android sub-target (Generic, ATC, DXT, PVRTC, ETC, ETC2)
	///
	/// --scenes strings separated with space, with relative to project path path to scenes files, including extension, i.e.:
	/// Assets/MainScene.unity
	///
	/// Usage example: 
	/// $ /Applications/Unity/Unity.app/Contents/MacOS/Unity -projectPath `pwd`/myProject -executeMethod WBBuild.IOS -quit -batchmode --buildTarget Android --buildPath `pwd`/myAndroidBuild.apk --scenes Assets/Scenes/1.unity Assets/Scenes/2.unity Assets/Scenes/3.unity
	/// </summary>
	static void Build()
	{
		BuildTarget buildTarget = BuildTarget.Android;	 
		String buildPath = "/Users/nikhilnavakiran/UnityProjects/myAndroidTestV1.apk";
		BuildImpl(buildPath, buildTarget, BuildOptions.None);
	}
	

}