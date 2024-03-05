//using DG.DOTweenEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonScaleAnimationProvider))]
public class ButtonScaleAnimationProviderEditor : Editor {

    private SerializedProperty _maxScale;
    private SerializedProperty _scaleUpDuration;
    private SerializedProperty _scaleDownDuration;

    private ButtonScaleAnimationProvider ButtonScaleProvider => target.GetComponent<ButtonScaleAnimationProvider>();

    private bool _separateUpDown;
    private float _totalDuration;

    private Rect _previewButtonRect;
    private Rect _durationRect;

    private void OnEnable() {
        _maxScale = serializedObject.FindProperty("maxScale");
        _scaleUpDuration = serializedObject.FindProperty("scaleUpDuration"); 
        _scaleDownDuration = serializedObject.FindProperty("scaleDownDuration");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        ButtonScaleProvider.maxScale = EditorGUILayout.Slider("Max scale", _maxScale.floatValue, 0.5f, 2f);
        EditorGUILayout.Separator();
        
        _separateUpDown = EditorGUILayout.Toggle("Separate up and down duration", _separateUpDown);
        if (!_separateUpDown) {
            ButtonScaleProvider.scaleUpDuration = ButtonScaleProvider.scaleDownDuration =
                EditorGUILayout.Slider("Scale duration", _scaleUpDuration.floatValue, .1f, 1f);
        }
        else { 
            ButtonScaleProvider.scaleUpDuration = EditorGUILayout.Slider("Scale up duration", _scaleUpDuration.floatValue, .1f, 1f);
            ButtonScaleProvider.scaleDownDuration = EditorGUILayout.Slider("Scale down duration", _scaleDownDuration.floatValue, .1f, 1f);
        }
        
        EditorGUILayout.Separator();
        _totalDuration = _separateUpDown ? _scaleUpDuration.floatValue + _scaleDownDuration.floatValue : _scaleUpDuration.floatValue * 2f;
        EditorGUILayout.LabelField("Total duration : ", _totalDuration.ToString());
        
        /*_previewButtonRect = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(_previewButtonRect, GUIContent.none)) {
            ButtonScaleProvider.ScaleAnimation();
            DOTweenEditorPreview.PrepareTweenForPreview(ButtonScaleProvider.sequence ,false, false, true);
        }
        GUILayout.Label("Preview");
        EditorGUILayout.EndHorizontal();*/

        serializedObject.ApplyModifiedProperties();
    }
}
#endif