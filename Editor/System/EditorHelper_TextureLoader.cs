using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace com.Klazapp.Editor
{
    public static partial class EditorHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Texture2D LoadTextureFromPackages(string packageRelativePath)
        {
            var fullPath = "Packages/" + packageRelativePath;
            return AssetDatabase.LoadAssetAtPath<Texture2D>(fullPath);
        }
    }
}