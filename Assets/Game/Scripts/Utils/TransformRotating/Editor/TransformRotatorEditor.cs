using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Utils.TransformRotating.Editor
{
    [CustomEditor(typeof(TransformRotator))]
    public class TransformRotatorEditor: UnityEditor.Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawPropertiesExcluding(serializedObject, "m_Script");
            
            var myRotator = (TransformRotator)target;

            if(GUILayout.Button("Start Rotate"))
            {
                myRotator.StartRotate();
            }
            
            if(GUILayout.Button("Stop Rotate"))
            {
                myRotator.StopRotate();
            }
        }
    }
}