using UnityEngine;
using UnityEditor;

public class BoneRenamer : MonoBehaviour
{
    [MenuItem("Tools/Rename Bones Recursively")]
    static void RenameBones()
    {
        if (Selection.activeTransform == null)
        {
            Debug.LogWarning("No GameObject selected. Please select the root bone or model.");
            return;
        }

        int renameCount = 0;
        RenameRecursively(Selection.activeTransform, ref renameCount);

        Debug.Log($"Renaming complete. {renameCount} objects renamed.");
    }

    static void RenameRecursively(Transform current, ref int count)
    {
        string oldName = current.name;
        string newName = oldName.Replace("_", ".");

        if (newName != oldName)
        {
            Undo.RecordObject(current.gameObject, "Rename Bone");
            current.name = newName;
            count++;
        }

        foreach (Transform child in current)
        {
            RenameRecursively(child, ref count);
        }
    }
}
