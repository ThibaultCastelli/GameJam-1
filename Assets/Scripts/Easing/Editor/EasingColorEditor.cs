using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EasingTC
{
    [CustomEditor(typeof(EasingColor))]
    public class EasingColorEditor : Editor
    {
        // Variables
        EasingColor _target;

        Color prevColor;
        bool prevColorFlag;
        bool previousollowEndValue;

        Renderer renderer = null;
        Image image = null;

        private void OnEnable()
        {
            _target = (EasingColor)target;

            // Get the renderer or image component.
            if (!_target.TryGetComponent<Renderer>(out renderer))
            {
                if (!_target.TryGetComponent<Image>(out image))
                {
                    Debug.LogError("ERROR : Can't find the renderer or the image on this game object.\nLocation : " + _target.gameObject.name);
                    return;
                }
                else
                    prevColor = image.color;
            }
            else
                prevColor = renderer.sharedMaterial.color;

            previousollowEndValue = _target.followEndValue;

            // Prevent from keeping the followEndValue when exit edit mode.
            EditorApplication.playModeStateChanged += ResetFollowEndValue;
        }

        private void OnDisable()
        {
            // Reset follow end value when closing inspector.
            if (target != null)
                ResetFollowEndValue(PlayModeStateChange.ExitingEditMode);

            EditorApplication.playModeStateChanged -= ResetFollowEndValue;
        }

        /// <summary>
        /// Reset follow end value when going into play mode.
        /// </summary>
        void ResetFollowEndValue(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode)
                return;

            _target.followEndValue = false;
            SetFollowEndValue();
        }

        /// <summary>
        /// Show the start or end color in edit mode.
        /// </summary>
        void SetFollowEndValue()
        {
            if (_target.followEndValue)
            {
                if (!prevColorFlag)
                {
                    if (renderer != null)
                        prevColor = renderer.sharedMaterial.color;
                    else
                        prevColor = image.color;
                }
                prevColorFlag = true;

                if (renderer != null)
                    renderer.sharedMaterial.color = _target.endColor;
                else
                    image.color = _target.endColor;
            }
            else
            {
                if (prevColorFlag)
                {
                    if (renderer != null)
                        renderer.sharedMaterial.color = prevColor;
                    else
                        image.color = prevColor;
                }
                prevColorFlag = false;

                if (renderer != null)
                    prevColor = renderer.sharedMaterial.color;
                else
                    prevColor = image.color;
            }
        }

        public override void OnInspectorGUI()
        {
            // Animation choice
            EditorGUILayout.LabelField("ANIMATION CHOICE", EditorStyles.boldLabel);

            _target.animationType = (AnimationType)EditorGUILayout.EnumPopup(new GUIContent("Animation Type", "Ease In : Start slow.\nEase Out : End slow.\nEase In Out : Start and end slow.\nMirror : Go back and forth.\nSpecial Ease : Bounce or back effect."), _target.animationType);

            switch (_target.animationType)
            {
                case AnimationType.EaseIn:
                    _target.easeInType = (EaseIn)EditorGUILayout.EnumPopup(new GUIContent("Ease In Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.easeInType);
                    break;

                case AnimationType.EaseOut:
                    _target.easeOutType = (EaseOut)EditorGUILayout.EnumPopup(new GUIContent("Ease Out Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.easeOutType);
                    break;

                case AnimationType.EaseInOut:
                    _target.easeInOutType = (EaseInOut)EditorGUILayout.EnumPopup(new GUIContent("Ease In Out Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.easeInOutType);
                    break;

                case AnimationType.Mirror:
                    _target.mirorType = (MirorType)EditorGUILayout.EnumPopup(new GUIContent("Mirror Type", "Quad : Slow slope.\nCirc : Hard slope."), _target.mirorType);
                    break;

                case AnimationType.SpecialEase:
                    _target.specialEaseType = (SpecialEase)EditorGUILayout.EnumPopup(new GUIContent("Special Ease Type", "Back In : Back effect at start and go to end value.\nBack Out : Go to end value and back effect.\nBounce Out : Go to end value and bounce effect."), _target.specialEaseType);
                    break;
            }

            EditorGUILayout.Space();

            // Animation infos
            EditorGUILayout.LabelField("ANIMATION INFOS", EditorStyles.boldLabel);

            _target.playOnAwake = EditorGUILayout.Toggle(new GUIContent("Play On Awake", "Select if the animation should automatically start when the game start."), _target.playOnAwake);
            
            _target.loop = EditorGUILayout.Toggle(new GUIContent("Loop", "Select if the animation should automatically loop."), _target.loop);
            if (_target.loop)
                _target.loopType = (LoopType)EditorGUILayout.EnumPopup(new GUIContent("Loop Type", "Simple : Loop the animation.\nMirror : Loop the animation back and forth. (can't work with Mirror animation type"), _target.loopType);

            EditorGUILayout.Space();

            _target.useAnotherStartValue = EditorGUILayout.Toggle(new GUIContent("Use Another Start Color", "Select if you want to use a different start value.\nUnselect if you want to use the current value of the object as the start value."), _target.useAnotherStartValue);

            EditorGUILayout.Space();

            // Animation values
            EditorGUILayout.LabelField("ANIMATION VALUES", EditorStyles.boldLabel);

            if (_target.useAnotherStartValue)
                _target.startColor = EditorGUILayout.ColorField(new GUIContent("Start Color", "Set the value for the start of the animation."), _target.startColor);

            _target.endColor = EditorGUILayout.ColorField(new GUIContent("End Color", "Set the value that the object will reach."), _target.endColor);
            _target.duration = EditorGUILayout.Slider(new GUIContent("Duration", "Set the duration of the animation. (in s)"), _target.duration, 0.01f, 20f);

            EditorGUILayout.Space();

            // Options
            EditorGUILayout.LabelField("OPTIONS", EditorStyles.boldLabel);

            _target.followEndValue = EditorGUILayout.Toggle(new GUIContent("Follow End Value", "Select to see the end value you set."), _target.followEndValue);

            // Only set follow end value when clicking on it
            if (_target.followEndValue != previousollowEndValue)
            {
                SetFollowEndValue();
                previousollowEndValue = _target.followEndValue;
            }
        }
    }
}