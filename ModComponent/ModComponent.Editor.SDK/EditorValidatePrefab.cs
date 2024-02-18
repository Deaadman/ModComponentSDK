#if UNITY_EDITOR
using ModComponent.SDK;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ModComponent.Editor.SDK
{
    internal class EditorValidatePrefab : EditorValidateBase
    {
        private GameObject selectedPrefab;
        private readonly Dictionary<string, CheckStatus> checkStatuses = new();
        private bool hasValidated = false;

        private int totalChecks;
        private int checksPassed;

        [MenuItem("ModComponent SDK/Validate GEAR Prefab", false, 20)]
        internal static void ShowWindow()
        {
            var window = GetWindow<EditorValidatePrefab>("GEAR Prefab Validator");
            window.minSize = new Vector2(500, 500);
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Select a prefab below to validate", ModComponentEditorStyles.CenteredLabelBold);
            selectedPrefab = (GameObject)EditorGUILayout.ObjectField(selectedPrefab, typeof(GameObject), false);

            GUILayout.Space(10);

            if (!hasValidated)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Waiting for prefab validation", ModComponentEditorStyles.CenteredLabelBold);
                GUILayout.FlexibleSpace();
            }
            else
            {
                foreach (var key in checkStatuses.Keys.ToList())
                {
                    CheckStatus tempStatus = checkStatuses[key];
                    DrawStatus(key, ref tempStatus);

                    if (tempStatus != checkStatuses[key])
                    {
                        checkStatuses[key] = tempStatus;
                    }
                }

                totalChecks = checkStatuses.Count;
                checksPassed = checkStatuses.Values.Count(status => status == CheckStatus.Success);
            }

            GUILayout.FlexibleSpace();
            if (!hasValidated) {}
            else
            {
                EditorGUILayout.LabelField($"{checksPassed}/{totalChecks} Checks Passed", ModComponentEditorStyles.CenteredLabelBold);
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Validate Prefab", GUILayout.Height(40)))
            {
                PerformValidation();
                hasValidated = true;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void PerformValidation()
        {
            var validationMessages = PrefabValidator.Validate(selectedPrefab, out bool validationResult);
            UpdateCheckStatuses(validationMessages);
        }

        private void UpdateCheckStatuses(List<string> messages)
        {
            checkStatuses.Clear();

            foreach (var message in messages)
            {
                var status = message.Contains("passed") ? CheckStatus.Success : CheckStatus.Failed;
                checkStatuses[message] = status;
            }
        }

        protected override string GetStatusMessage(string baseLabel, CheckStatus status)
        {
            return status switch
            {
                CheckStatus.Pending => $"{baseLabel} check is pending.",
                _ => baseLabel,
            };
        }
    }
}
#endif