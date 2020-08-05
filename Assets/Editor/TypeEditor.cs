using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Unit)), CanEditMultipleObjects]
public class TypeEditor : Editor
{

    public SerializedProperty
        Class_Prop,
        Damage_Prop,
        AttackRange_Prop,
        Speed_Prop,
        AttackSpeed_Prop,
        AttackDelay_Prop,

        Projectile_Prop,
        ShootPos_Prop;

        //controllable_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        Class_Prop = serializedObject.FindProperty("Class");
        Damage_Prop = serializedObject.FindProperty("Damage");
        AttackRange_Prop = serializedObject.FindProperty("AttackRange");
        Speed_Prop = serializedObject.FindProperty("Speed");
        AttackSpeed_Prop = serializedObject.FindProperty("AttackSpeed");
        AttackDelay_Prop = serializedObject.FindProperty("AttackDelay");

        Projectile_Prop = serializedObject.FindProperty("Projectile");
        ShootPos_Prop = serializedObject.FindProperty("ShootPos");
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
                EditorGUILayout.IntSlider(Damage_Prop, 0, 100, new GUIContent("Damage"));
                EditorGUILayout.Slider(AttackRange_Prop, 0, 100, new GUIContent("AttackRange"));
                EditorGUILayout.Slider(AttackSpeed_Prop, 0, 100, new GUIContent("AttackSpeed"));
                EditorGUILayout.Slider(AttackDelay_Prop, 0, 100, new GUIContent("AttackDelay"));
                EditorGUILayout.ObjectField(Projectile_Prop);
                EditorGUILayout.ObjectField(ShootPos_Prop);
                EditorGUILayout.Slider(Speed_Prop, 0, 100, new GUIContent("Speed"));
                break;

            case Unit.Type.Fighter:
                //EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                EditorGUILayout.IntSlider(Damage_Prop, 0, 100, new GUIContent("Damage"));
                EditorGUILayout.Slider(AttackRange_Prop, 0, 100, new GUIContent("AttackRange"));
                EditorGUILayout.Slider(AttackSpeed_Prop, 0, 100, new GUIContent("AttackSpeed"));
                EditorGUILayout.Slider(AttackDelay_Prop, 0, 100, new GUIContent("AttackDelay"));
                EditorGUILayout.Slider(Speed_Prop, 0, 100, new GUIContent("Speed"));
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }
}