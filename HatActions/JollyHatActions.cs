using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

internal class JollyHatActions
{
    internal static bool DoesTransformAlreadyContainJollyHat(Transform parent, string pathToObject)
    {
        return parent.Find($"{pathToObject}/{JollyLethal.JollyHatSpawnedObjName}") is not null;
    }

    internal static bool DoesTransformRootAlreadyContainJollyHat(Transform parent)
    {
        return parent.Find($"{JollyLethal.JollyHatSpawnedObjName}") is not null;
    }

    internal static void FixHatParentChildPosScale(Transform parent)
    {
        Transform child = parent.GetChild(0);
        ApplyOffsetsToHat(child, Vector3.zero, Vector3.zero, 1);
    }

    internal static void FixHatsScaleBasedOnSquishedParent(Transform objParent, Transform hatParent, float targetScale = 1f)
    {
        Vector3 pScale = objParent.localScale;
        hatParent.localScale = new Vector3(targetScale / pScale.x, targetScale / pScale.y, targetScale / pScale.z);
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

    internal static Transform PlaceHatOnTransform(Transform targetBone, Vector3 posOffset, Vector3 rotOffset, float scale)
    {
        GameObject hatPrefabSpawn = Object.Instantiate(JollyLethal.mySantaHat, targetBone);
        hatPrefabSpawn.name = JollyLethal.JollyHatSpawnedObjName;
        Transform objToTranslate = hatPrefabSpawn.transform;

        FixHatParentChildPosScale(objToTranslate);
        ApplyOffsetsToHat(objToTranslate, posOffset, rotOffset, scale); 

        return objToTranslate;
    }
}
