using UnityEditor;
using UnityEngine;

namespace StickyTeam.FashionClash.Editor
{
    public static class EditorUtils
    {
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            T[] a = null;
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            a = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return a;
        }
    }
}