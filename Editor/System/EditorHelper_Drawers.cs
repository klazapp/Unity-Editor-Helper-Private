using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace com.Klazapp.Editor
{
    //TODO: Complete draw property
    public static partial class EditorHelper
    {
        #region Public Access
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawHorizontalLine(int space = 0)
        {
            var defaultBgColor = GUI.backgroundColor;
            DrawSpace(space);
            GUI.backgroundColor = Color.white;
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            DrawSpace(space);
            GUI.backgroundColor = defaultBgColor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawSpace(int space)
        {
            EditorGUILayout.Space(space);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawVerticalSpace(int height)
        {
            EditorGUILayout.BeginHorizontal(); 
            DrawBox(0, height);
            EditorGUILayout.EndHorizontal();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DrawButton(int buttonWidth = 0, int buttonHeight = 0, Color buttonColor = new(), string titleText = "", GUIStyle titleStyle = null, Texture2D iconTexture = null, Alignment alignment = Alignment.Middle)
        {
            //Button pressed state
            var buttonPressed = false;
            
            //Store original color
            var originalBgColor = GUI.backgroundColor;

            //Retrieve current skin
            var currentSkin = GUI.skin;
       
            var currentSkinBoxNormalBg = currentSkin.box.normal.background;
            currentSkin.box.normal.background = Texture2D.whiteTexture;
           
            //Set new color
            GUI.backgroundColor = buttonColor;

            var (buttonWidthGUILayoutOptions, buttonHeightGUILayoutOptions) = GetGUILayoutOptions(buttonWidth, buttonHeight);
            
            EditorGUILayout.BeginHorizontal();
            
            if (alignment == Alignment.Middle)
            {
                GUILayout.FlexibleSpace();
            }

            EditorGUILayout.BeginVertical(currentSkin.button, buttonWidthGUILayoutOptions, buttonHeightGUILayoutOptions);
      
            titleStyle ??= new GUIStyle();
            
            if (iconTexture != null)
            {
                var buttonGUIContent = new GUIContent
                {       
                    image = iconTexture,
                
                    //Add spacing betwixt texture and text
                    text = string.IsNullOrEmpty(titleText) ? "" : "     " + titleText,
                };

                buttonPressed = GUILayout.Button(buttonGUIContent, titleStyle, buttonWidthGUILayoutOptions,
                    buttonHeightGUILayoutOptions);
            }
            else
            {
                buttonPressed = GUILayout.Button(titleText, titleStyle, buttonWidthGUILayoutOptions,
                    buttonHeightGUILayoutOptions);
            }

            EditorGUILayout.EndVertical();

            if (alignment == Alignment.Middle)
            {
                GUILayout.FlexibleSpace();
            }
            
            EditorGUILayout.EndHorizontal();
            
            //Restore original color and skin
            GUI.backgroundColor = originalBgColor;
            currentSkin.box.normal.background = currentSkinBoxNormalBg;
            
            return buttonPressed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawBox(int boxWidth = 0, int boxHeight = 0, Color boxColor = new(), string titleText = "", GUIStyle titleStyle = null, Texture2D iconTexture = null, Alignment alignment = Alignment.Middle)
        {
            //Store original color
            var originalBgColor = GUI.backgroundColor;

            //Retrieve current skin
            var currentSkin = GUI.skin;
       
            var currentSkinBoxNormalBg = currentSkin.box.normal.background;
            //currentSkin.box.normal.background = Texture2D.whiteTexture;
           
            //Set new color
            GUI.backgroundColor = boxColor;

            var (boxWidthGUILayoutOptions, boxHeightGUILayoutOptions) = GetGUILayoutOptions(boxWidth, boxHeight);

            EditorGUILayout.BeginHorizontal();

            if (alignment == Alignment.Middle)
            {
                GUILayout.FlexibleSpace();
            }
            
            EditorGUILayout.BeginVertical(currentSkin.box, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);
            EditorGUILayout.BeginHorizontal();

            titleStyle ??= new GUIStyle();
            
            if (iconTexture != null)
            {
                var titleWithIconGUI = new GUIContent
                {       
                    image = iconTexture,
                
                    //Add spacing betwixt texture and text
                    text = string.IsNullOrEmpty(titleText) ? "" : "     " + titleText,
                };
                
                GUILayout.Label(titleWithIconGUI, titleStyle, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);
            }
            else
            {
                GUILayout.Label(titleText, titleStyle, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);
            }
       
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            
            if (alignment == Alignment.Middle)
            {
                GUILayout.FlexibleSpace();
            }

            EditorGUILayout.EndHorizontal();

            //Restore original color and skin
            GUI.backgroundColor = originalBgColor;
            currentSkin.box.normal.background = currentSkinBoxNormalBg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawBoxWithBackground(int boxWidth = 0, int boxHeight = 0, int padding = 2, Color outerBoxColor = new(), Color innerBoxColor = new(), string titleText = "", GUIStyle titleStyle = null, Texture2D iconTexture = null, Alignment alignment = Alignment.Middle)
        {
            //Store original color and background
            var originalBgColor = GUI.backgroundColor;
            var originalColor = GUI.color;
            var originalBackground = GUI.skin.box.normal.background;

            //Create a white texture for coloring
            var whiteTexture = new Texture2D(1, 1);
            whiteTexture.SetPixel(0, 0, Color.white);
            whiteTexture.Apply();

            //Set the background texture to the white texture
            GUI.skin.box.normal.background = whiteTexture;

            var (outerBoxWidthOptions, outerBoxHeightOptions) = GetGUILayoutOptions(boxWidth + padding * 2, boxHeight + padding * 2);

            EditorGUILayout.BeginHorizontal();
            if (alignment == Alignment.Middle)
            {
                GUILayout.FlexibleSpace();
            }

            //Draw outer (background) box
            GUI.backgroundColor = outerBoxColor;
            EditorGUILayout.BeginVertical(GUI.skin.box, outerBoxWidthOptions, outerBoxHeightOptions);
            GUILayout.Space(boxHeight + padding * 2);
            EditorGUILayout.EndVertical();

            //Draw inner (colored) box
            var lastRect = GUILayoutUtility.GetLastRect();
            lastRect = new Rect(lastRect.x + padding, lastRect.y + padding, lastRect.width - padding * 2, lastRect.height - padding * 2);
            GUI.backgroundColor = innerBoxColor;
            GUI.Box(lastRect, GUIContent.none);

            //Draw content
            if (!string.IsNullOrEmpty(titleText) || iconTexture != null)
            {
                //Add a bit of offset
                titleText = "   " + titleText;
                var contentStyle = titleStyle ?? new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
                GUI.Label(lastRect, new GUIContent(titleText, iconTexture), contentStyle);
            }

            //Restore original color, background and skin
            GUI.backgroundColor = originalBgColor;
            GUI.color = originalColor;
            GUI.skin.box.normal.background = originalBackground;

            if (alignment == Alignment.Middle)
            {
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();

            //Clean up white texture
            UnityEngine.Object.DestroyImmediate(whiteTexture);
        }
         
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DrawColoredFoldout(int width = 0, int height = 0, string contentText = "", GUIStyle contentStyle = null, Color32 backgroundColor = new Color32(), bool foldout = true, Texture2D foldoutDownTex = null, Texture2D foldoutUpTex = null, bool readOnly = false)
        {
            if (readOnly)
            {
                GUI.enabled = false;
            }
            
            // Store the original colors
            var originalBackgroundColor = GUI.backgroundColor;
            var originalContentColor = GUI.contentColor;

            //Set the new colors
            GUI.backgroundColor = backgroundColor;

            var (widthGUILayoutOption, heightGUILayoutOption) = GetGUILayoutOptions(width, height);

            //Draw the box as background
            EditorGUILayout.BeginVertical(GUI.skin.box, widthGUILayoutOption, heightGUILayoutOption);

            var foldoutGUIContent = new GUIContent()
            {
                image = foldout ? foldoutDownTex : foldoutUpTex,
                //Add space before text and after image
                text = "" + contentText,
            };
            
            //Draw the foldout
            foldout = EditorGUILayout.Foldout(foldout, foldoutGUIContent, true, contentStyle);

            EditorGUILayout.EndVertical();

            // Restore the original colors
            GUI.backgroundColor = originalBackgroundColor;
            GUI.contentColor = originalContentColor;

            if (readOnly)
            {
                GUI.enabled = true;
            }
            
            return foldout;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DrawProperty(SerializedProperty prop, int boxWidth = 0, int boxHeight = 0, bool readOnly = false)
        {
            if (readOnly)
            {
                GUI.enabled = false;
            }
            
            var (boxWidthGUILayoutOptions, boxHeightGUILayoutOptions) = GetGUILayoutOptions(boxWidth, boxHeight);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical(GUI.skin.box, boxWidthGUILayoutOptions, boxHeightGUILayoutOptions);

            DrawProperty(prop, readOnly);
          
            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            if (readOnly)
            {
                GUI.enabled = true;
            }
        }
        #endregion
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void DrawProperty(SerializedProperty prop, bool readOnly = false)
        {
            if (readOnly)
            {
                GUI.enabled = false;
            }
            
            var propType = prop.propertyType;
            switch (propType)
            {
                case SerializedPropertyType.Generic:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Boolean:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Float:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.String:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Color:
                    EditorGUILayout.ColorField(prop.colorValue);
                    break;
                case SerializedPropertyType.ObjectReference:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.LayerMask:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Enum:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Vector2:
                    EditorGUILayout.Vector2Field("", prop.vector2Value);
                    break;
                case SerializedPropertyType.Vector3:
                    EditorGUILayout.Vector3Field("", prop.vector3Value);
                    break;
                case SerializedPropertyType.Vector4:
                    EditorGUILayout.Vector4Field("", prop.vector4Value);
                    break;
                case SerializedPropertyType.Rect:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.ArraySize:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Character:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.AnimationCurve:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Bounds:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Gradient:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Quaternion:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.ExposedReference:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Vector2Int:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Vector3Int:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.RectInt:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.BoundsInt:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.ManagedReference:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                case SerializedPropertyType.Hash128:
                    EditorGUILayout.PropertyField(prop, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (readOnly)
            {
                GUI.enabled = true;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (GUILayoutOption widthOption, GUILayoutOption heightOption) GetGUILayoutOptions(int width, int height)
        {
            var widthGUILayoutOptions = width == 0 ? GUILayout.ExpandWidth(true) : GUILayout.Width(width);
            var heightGUILayoutOptions = height == 0 ? GUILayout.ExpandHeight(true) : GUILayout.Height(height);

            return (widthGUILayoutOptions, heightGUILayoutOptions);
        }
    }
}
