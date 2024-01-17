using System.Runtime.CompilerServices;
using UnityEngine;

namespace com.Klazapp.Editor
{
    using Event = UnityEngine.Event;
    
    public static partial class EditorHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnPointerHover()
        {
            return GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnPointerDownInLastRect()
        {
            return Event.current.type == EventType.MouseDown &&
                   GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnPointerUpInLastRect()
        {
            return Event.current.type == EventType.MouseUp &&
                   GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnPointerDown()
        {
            return Event.current.type == EventType.MouseDown;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnPointerUp()
        {
            return Event.current.type == EventType.MouseUp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OnPointerLeftRect()
        {
            return !GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition);
        }
    }
}