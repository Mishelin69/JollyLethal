using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

[HarmonyPatch(typeof(EnemyAI))]
internal class FixHatPositionOnEnemyStateChange
{
    [HarmonyPatch("SwitchToBehaviourStateOnLocalClient")]
    [HarmonyPostfix]
    private static void FixEnemyHatPosition(EnemyAI __instance, int stateIndex)
    {
        Transform? hatTransform = RecursiveFindChild(__instance.transform, JollyLethal.JollyHatSpawnedObjName);
        if (hatTransform is null)
        {
            return;
        }

        switch (__instance.enemyType.enemyName)
        {
            case "Jester":
                HandleJesterStateChange(__instance.transform, hatTransform, stateIndex);
                break;
            case "Maneater":
                __instance.StartCoroutine(HandleManeaterStateChange(__instance.transform, hatTransform, stateIndex));
                break;
        }
    }

    private static void HandleJesterStateChange(Transform enemy, Transform hat, int state)
    {
        JollyLethal.PluginLogInfoWithPrefix("Handling Jesters change of state");
        switch (state)
        {
            case 0:
            case 1: // Boxed / Cranking State
                JollyLethal.PluginLogInfoWithPrefix("Changing hat position due to change of jester state to winding/chilling");
                var (_, posOffset, rotOffset, scale) = EnemyHatConfigs.GetJesterSantaHatConfig();

                PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffset, rotOffset, scale);
                break;

            case 2: // Popped / Chasing State
                JollyLethal.PluginLogInfoWithPrefix("Changing hat position due to change of jester state to popped");
                var (bonePath, posOffsetPopped, rotOffsetPopped, scalePopped) = EnemyHatConfigs.GetJesterPoppedSantaHatConfig();
                Transform newParent = enemy.Find(bonePath);

                hat.SetParent(newParent, false);
                PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffsetPopped, rotOffsetPopped, scalePopped);
                break;
        }
    }

    private static System.Collections.IEnumerator HandleManeaterStateChange(Transform enemy, Transform hat, int state)
    {
        yield return new WaitForSeconds(2.5f);

        JollyLethal.PluginLogInfoWithPrefix("Handling Maneaters change of state");

        bool isAdult = (state == 2) || !IsManeaterInBabyState(enemy);
        if (!isAdult)
        {
            JollyLethal.PluginLogInfoWithPrefix("Changing hat position due to change of maneater appearance to smol");
            var (bonePath, posOffset, rotOffset, scale) = EnemyHatConfigs.GetManeaterSantaHatConfig();
            Transform newParent = enemy.Find(bonePath);

            hat.SetParent(newParent, false);
            PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffset, rotOffset, scale);
        }
        else
        {
            JollyLethal.PluginLogInfoWithPrefix("Changing hat position due to change of maneater appearance to big");
            var (bonePathBig, posOffsetBig, rotOffsetBig, scaleBig) = EnemyHatConfigs.GetManeaterBigSantaHatConfig();
            Transform newParentBig = enemy.Find(bonePathBig);

            hat.SetParent(newParentBig, false);
            PlaceHatsOnEnemiesPatch.ApplyOffsetsToObject(hat, posOffsetBig, rotOffsetBig, scaleBig);
        }
    }

    internal static bool IsManeaterInBabyState(Transform maneater)
    {
        return maneater.Find("BabyMeshContainer").gameObject.activeSelf && !maneater.Find("MeshContainer").gameObject.activeSelf;
    }

    internal static Transform? RecursiveFindChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }

            Transform? found = RecursiveFindChild(child, childName);
            if (found is not null)
            {
                return found;
            }
        }
        return null;
    }
}
