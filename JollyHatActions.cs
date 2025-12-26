using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

internal class JollyHatActions
{
    internal static bool DoesTransformAlreadyContainJollyHat(Transform parent, string pathToObject)
    {
        return parent.Find($"{pathToObject}/{JollyLethal.JollyHatSpawnedObjName}") is not null;
    }

    internal static void FixHatParentChildPosScale(Transform parent)
    {
        Transform child = parent.GetChild(0);
        ApplyOffsetsToHat(child, Vector3.zero, Vector3.zero, 1);
    }

    internal static void ApplyOffsetsToHat(Transform obj, Vector3 pos, Vector3 rot, float scale)
    {
        obj.localPosition = pos;
        obj.localEulerAngles = rot;
        obj.localScale = Vector3.one * scale;
    }

    internal static void ChangeRotOfHatParentChild(Transform hatParent, Vector3 newRot)
    {
        Transform child = hatParent.GetChild(0);
        child.localEulerAngles = newRot;
    }
}
