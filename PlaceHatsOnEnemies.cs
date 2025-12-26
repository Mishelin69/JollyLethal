using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

[HarmonyPatch(typeof(EnemyAI))]
internal class PlaceHatsOnEnemiesPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void GiveEnemySantaHat(EnemyAI __instance)
    {
        if (JollyLethal.myCustomAssets is null) 
        {
            JollyLethal.PluginLogErrorWithPrefix("AssetBundle not loaded!!!");
            return;
        }

        PlaceHatOnEnemyTransform(__instance.transform, __instance.enemyType);
    }

    internal static void PlaceHatOnEnemyTransform(Transform enemyTransform, EnemyType enemyType)
    {
        string enemyName = enemyType.enemyName;
        var (bonePath, posOffset, rotOffset, scale) = GetEnemyHatConfig(enemyName);
        if (bonePath is null || JollyHatActions.DoesTransformAlreadyContainJollyHat(enemyTransform, bonePath))
        {
            return;
        }
        Transform targetBone = GetHatPlaceTransform(enemyTransform, bonePath);
        Transform hatPrefab = SpawnSantaHatOnEnemy(targetBone, posOffset, rotOffset, scale); 
        FixAdditionalChildrenPositions(enemyName, hatPrefab);

    }

    private static void FixAdditionalChildrenPositions(string enemyName, Transform hatParent)
    {
        switch(enemyName)
        {
            case "Crawler":
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 285, 0));
                break;
            case "Maneater":
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 115, 0));
                break;
        }
    }

    internal static Transform SpawnSantaHatOnEnemy(Transform targetBone, Vector3 posOffset, Vector3 rotOffset, float scale)
    {
        GameObject hatPrefabSpawn = Object.Instantiate(JollyLethal.mySantaHat, targetBone);
        hatPrefabSpawn.name = JollyLethal.JollyHatSpawnedObjName;
        Transform objToTranslate = hatPrefabSpawn.transform;

        JollyHatActions.FixHatParentChildPosScale(objToTranslate);
        JollyHatActions.ApplyOffsetsToHat(objToTranslate, posOffset, rotOffset, scale); 

        return objToTranslate;
    }  

    internal static Transform GetHatPlaceTransform(Transform enemyTransform, string? bonePath)
    {
        Transform ret = enemyTransform.transform.Find(bonePath);
        return (ret is null) ? enemyTransform.transform : ret;
    } 

    internal static (string?, Vector3, Vector3, float) GetEnemyHatConfig(string enemyName)
    {
        JollyLethal.PluginLogInfoWithPrefix($"Placing hat on: {enemyName}");
        switch (enemyName)
        {
            case "Flowerman": // Bracken
                return EnemyHatConfigs.GetBrackenSantaHatConfig();
            case "Hoarding bug":
                return EnemyHatConfigs.GetHoardingBugSantaHatConfig();
            case "Centipede": // Snare Flea
                return EnemyHatConfigs.GetSnareFleaSantaHatConfig();
            case "Crawler": // Thumper
                return EnemyHatConfigs.GetThumperSantaHatConfig();
            case "Bunker Spider":
                return EnemyHatConfigs.GetBunkerSpiderSantaHatConfig();
            case "Jester":
                return EnemyHatConfigs.GetJesterSantaHatConfig();
            case "Nutcracker":
                return EnemyHatConfigs.GetNutcrackerSantaHatConfig();
            case "Spring":
                return EnemyHatConfigs.GetSpringSantaHatConfig();
            case "Masked":
                return EnemyHatConfigs.GetMaskedSantaHatConfig();
            case "Butler":
                return EnemyHatConfigs.GetButlerSantaHatConfig();
            case "Maneater":
                return EnemyHatConfigs.GetManeaterSantaHatConfig();
            case "Puffer": // Spore Lizard
                return EnemyHatConfigs.GetSporeLizardSantaHatConfig();
            case "Blob":
                return EnemyHatConfigs.GetBlobSantaHatConfig();
            case "Girl":
                return EnemyHatConfigs.GetGirlSantaHatConfig();
            case "MouthDog":
                return EnemyHatConfigs.GetMouthDogSantaHatConfig();
            case "Baboon hawk":
                return EnemyHatConfigs.GetBaboonHawkSantaHatConfig();
            case "ForestGiant":
                return EnemyHatConfigs.GetForestGiantSantaHatConfig();
            case "RadMech": // Old Bird
                return EnemyHatConfigs.GetOldBirdSantaHatConfig();
            case "Tulip Snake":
                return EnemyHatConfigs.GetTulipSnakeSantaHatConfig();
            case "Bush Wolf": // Kidnapper Fox
                return EnemyHatConfigs.GetKidnapperFoxSantaHatConfig();
            case "Clay Surgeon": // Barber
                JollyLethal.PluginLogInfoWithPrefix($"Skipping hat for incompatible enemy: {enemyName}");
                return EnemyHatConfigs.GetBarberSantaHatConfig();
                // Incompatible Group
            case "Red Locust Bees":
            case "Docile Locust Bees":
            case "Butler Bees":
            case "Earth Leviathan":
            case "Manticoil":
            case "Lasso":
            case "Red pill":
            case "GiantKiwi":
                JollyLethal.PluginLogInfoWithPrefix($"Skipping hat for incompatible enemy: {enemyName}");
                return EnemyHatConfigs.GetIncompatibleSantaHatConfig();

            default:
                JollyLethal.PluginLogErrorWithPrefix($"Enemy '{enemyName}' not in config. Defaulting to 'Head'.");
                return EnemyHatConfigs.GetDefaultSantaHatConfig();
        }
    }
}
