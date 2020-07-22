using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Unit)), CanEditMultipleObjects]
public class TypeEditor : Editor
{

    public SerializedProperty
        Class_Prop,
        Speed_Prop,

        ShootDis_Prop,
        ShootDelay_Prop,
        Projectile_Prop,
        ShootPos_Prop,

        MeleeDam_Prop,
        FightDis_Prop,
        FightDelay_Prop;
        //controllable_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        Class_Prop = serializedObject.FindProperty("Class");
        Speed_Prop = serializedObject.FindProperty("Speed");

        ShootDis_Prop = serializedObject.FindProperty("ShootDis");
        ShootDelay_Prop = serializedObject.FindProperty("ShootDelay");
        Projectile_Prop = serializedObject.FindProperty("Projectile");
        ShootPos_Prop = serializedObject.FindProperty("ShootPos");


        MeleeDam_Prop = serializedObject.FindProperty("MeleeDam");
        FightDis_Prop = serializedObject.FindProperty("FightDis");
        FightDelay_Prop = serializedObject.FindProperty("FightDelay");
        //controllable_Prop = serializedObject.FindProperty("controllable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(Class_Prop);

        Unit.Type st = (Unit.Type)Class_Prop.enumValueIndex;

        switch (st)
        {
            case Unit.Type.Shooter:
                //EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.Slider(ShootDis_Prop, 0, 100, new GUIContent("ShootDis"));
                EditorGUILayout.Slider(ShootDelay_Prop, 0, 100, new GUIContent("ShootDelay"));
                EditorGUILayout.ObjectField(Projectile_Prop);
                EditorGUILayout.ObjectField(ShootPos_Prop);
                EditorGUILayout.Slider(Speed_Prop, 0, 100, new GUIContent("Speed"));
                break;

            case Unit.Type.Fighter:
                //EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(MeleeDam_Prop, 0, 100, new GUIContent("MeleeDam"));
                EditorGUILayout.Slider(FightDis_Prop, 0, 100, new GUIContent("FightDis"));
                EditorGUILayout.Slider(FightDelay_Prop, 0, 100, new GUIContent("FightDelay"));
                EditorGUILayout.Slider(Speed_Prop, 0, 100, new GUIContent("Speed"));
                break;

                /*
            case Unit.Type.C:
                EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(valForC_Prop, 0, 100, new GUIContent("valForC"));
                break;
                */
        }


        serializedObject.ApplyModifiedProperties();
    }
}