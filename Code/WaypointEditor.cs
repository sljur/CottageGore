using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]

public class WaypointEditor : Editor
{
    Waypoint Waypoint => target as Waypoint;

    private void OnSceneGUI()
    {
        //This right here determines way point color when setting 
        Handles.color = Color.cyan;
        for (int i = 0; i < Waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //creates handles of the waypoints
            Vector3 currentWaypointPoint = Waypoint.CurrentPosition + Waypoint.Points[i];
            var fmh_22_86_638344600082203399 = Quaternion.identity; Vector3 newWaypointPoint = Handles.FreeMoveHandle( currentWaypointPoint, 0.7f, new Vector3(0.3f,0.3f,0.3f), Handles.SphereHandleCap);
           
            //create text for the waypoint; this also helps with how the waypoint looks
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.fontSize = 16;
            textStyle.normal.textColor = Color.white;
            Vector3 textAlligment = Vector3.down * 0.35f + Vector3.right * 0.35f;
            Handles.Label(Waypoint.CurrentPosition + Waypoint.Points[i] + textAlligment, $"{i + 1}", textStyle);
            EditorGUI.EndChangeCheck();
            
            //Helps remove or add new waypoint in the Unity interface
            if (EditorGUI.EndChangeCheck())
            {
              Undo.RecordObject(target, "FreeMoveHandle");
              Waypoint.Points[i] = newWaypointPoint - Waypoint.CurrentPosition;
            } 
        }
    
    }
}
