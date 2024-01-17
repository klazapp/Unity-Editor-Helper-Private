using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace com.Klazapp.Editor
{
    public static partial class EditorHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector3 GetFloat3AsVector(this SerializedProperty property)
        {
            var output = new Vector3(0, 0, 0);
            var p = property.Copy();
            p.Next(true);
            output.x = p.floatValue;
            p.Next(true);
            output.y = p.floatValue;
            p.Next(true);
            output.z = p.floatValue;
            return output;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetFloat3FromVector(this SerializedProperty property, Vector3 value)
        {
            var p = property.Copy();
            p.Next(true);
            p.floatValue = value.x;
            p.Next(true);
            p.floatValue = value.y;
            p.Next(true);
            p.floatValue = value.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector4 GetQuaternionAsVector(this SerializedProperty property)
        {
            var p = property.Copy();
            var boxedVal = p.boxedValue;
            if (boxedVal is quaternion outRot)
            {
                return new Vector4(outRot.value.x, outRot.value.y, outRot.value.z, outRot.value.w);
            }
            else
            {
                var rot = p.quaternionValue;
                return new Vector4(rot.x, rot.y, rot.z, rot.w);
            }
        }
    }
}