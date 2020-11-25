using System.Linq;
using StickyTeam.FashionClash.Customization.Infrastructure;
using UnityEditor;
using UnityEngine;

namespace StickyTeam.FashionClash.Editor.Customization
{
    [CustomEditor(typeof(UnityCategory))]
    public class UnityCategoryEditor : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var db = (UnityCategory)target;
            EditorUtility.SetDirty(db);
            if (GUILayout.Button("Collect Items"))
            {
                db.items = EditorUtils.GetAllInstances<UnityItem>().Where(x => x.category.Id == db.Id).ToList();
            }
        }
    }
}