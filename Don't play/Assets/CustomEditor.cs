using UnityEngine;
using UnityEditor;

public class CustomEditor : EditorWindow
{
    [MenuItem("Custom Editor/Editor")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditor>("Custom");
    }

    private void OnGUI()
    {
        GUILayout.Label("Povezivanje na photon server");
        if (GUILayout.Button("CONNECT TO SERVER"))
        {

        }

        GUILayout.Label("Kreirati će sobu po imenom 'Example'");
        if (GUILayout.Button("CREATE ROOM"))
        {

        }

        GUILayout.Label("Ukoliko soba 'Example' postoji, spojit ce se.");
        if (GUILayout.Button("ROOM JOIN"))
        {

        }

        GUILayout.Label("Kontrole za igraca.");
        EditorGUILayout.Space();
        if (GUILayout.Button("SPAWN PLAYER"))
        {

        }
        if (GUILayout.Button("SET FULL HEALTH"))
        {

        }
        if (GUILayout.Button("KILL PLAYER"))
        {

        }
    }

}
