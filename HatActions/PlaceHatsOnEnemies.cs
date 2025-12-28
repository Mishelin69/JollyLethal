using HarmonyLib;
using UnityEngine;

namespace JollyLethal.HatActions;

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
        Transform hatPrefab = JollyHatActions.PlaceHatOnTransform(targetBone, posOffset, rotOffset, scale); 
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
                return HatConfigs.EnemyHatConfigs.GetBrackenSantaHatConfig();
            case "Hoarding bug":
                return HatConfigs.EnemyHatConfigs.GetHoardingBugSantaHatConfig();
            case "Centipede": // Snare Flea
                return HatConfigs.EnemyHatConfigs.GetSnareFleaSantaHatConfig();
            case "Crawler": // Thumper
                return HatConfigs.EnemyHatConfigs.GetThumperSantaHatConfig();
            case "Bunker Spider":
                return HatConfigs.EnemyHatConfigs.GetBunkerSpiderSantaHatConfig();
            case "Jester":
                return HatConfigs.EnemyHatConfigs.GetJesterSantaHatConfig();
            case "Nutcracker":
                return HatConfigs.EnemyHatConfigs.GetNutcrackerSantaHatConfig();
            case "Spring":
                return HatConfigs.EnemyHatConfigs.GetSpringSantaHatConfig();
            case "Masked":
                return HatConfigs.EnemyHatConfigs.GetMaskedSantaHatConfig();
            case "Butler":
                return HatConfigs.EnemyHatConfigs.GetButlerSantaHatConfig();
            case "Maneater":
                return HatConfigs.EnemyHatConfigs.GetManeaterSantaHatConfig();
            case "Puffer": // Spore Lizard
                return HatConfigs.EnemyHatConfigs.GetSporeLizardSantaHatConfig();
            case "Blob":
                return HatConfigs.EnemyHatConfigs.GetBlobSantaHatConfig();
            case "Girl":
                return HatConfigs.EnemyHatConfigs.GetGirlSantaHatConfig();
            case "MouthDog":
                return HatConfigs.EnemyHatConfigs.GetMouthDogSantaHatConfig();
            case "Baboon hawk":
                return HatConfigs.EnemyHatConfigs.GetBaboonHawkSantaHatConfig();
            case "ForestGiant":
                return HatConfigs.EnemyHatConfigs.GetForestGiantSantaHatConfig();
            case "RadMech": // Old Bird
                return HatConfigs.EnemyHatConfigs.GetOldBirdSantaHatConfig();
            case "Tulip Snake":
                return HatConfigs.EnemyHatConfigs.GetTulipSnakeSantaHatConfig();
            case "Bush Wolf": // Kidnapper Fox
                return HatConfigs.EnemyHatConfigs.GetKidnapperFoxSantaHatConfig();
            case "Clay Surgeon": // Barber
                JollyLethal.PluginLogInfoWithPrefix($"Skipping hat for incompatible enemy: {enemyName}");
                return HatConfigs.EnemyHatConfigs.GetBarberSantaHatConfig();
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
                return HatConfigs.EnemyHatConfigs.GetIncompatibleSantaHatConfig();

            default:
                JollyLethal.PluginLogErrorWithPrefix($"Enemy '{enemyName}' not in config. Defaulting to 'Head'.");
                return HatConfigs.EnemyHatConfigs.GetDefaultSantaHatConfig();
        }
    }
}
