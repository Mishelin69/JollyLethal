using HarmonyLib;
using UnityEngine;

namespace JollyLethal.HatActions;

[HarmonyPatch(typeof(GrabbableObject))]
internal class PlaceHatsOnItemsPatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void GiveEnemySantaHat(GrabbableObject __instance)
    {
        if (JollyLethal.myCustomAssets is null) 
        {
            JollyLethal.PluginLogErrorWithPrefix("AssetBundle not loaded!!!");
            return;
        }

        PlaceHatOnObjectTransform(__instance.transform, __instance.itemProperties);
    }

    private static void FixAdditionalChildrenPositions(string itemName, Transform objParent, Transform hatParent)
    {
        switch(itemName)
        {
            case "Fancy lamp":
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 25, 0));
                break;
            case "Plastic cup":
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 25, 0));
                break;
            case "Jar of pickles":
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 225, 0));
                break;
            case "Plastic fish":
                JollyHatActions.FixHatsScaleBasedOnSquishedParent(objParent, hatParent, 0.2f);
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 290, 0));
                break;
            case "Toy robot":
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 25, 0));
                break;
            case "Cat":
                JollyHatActions.HatChangeParent(objParent, hatParent, HatConfigs.ObjectHatConfigs.CatBonePath);
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(0, 250, 0));
                break;
            case "Teeth":
                JollyHatActions.HatChangeParent(objParent, hatParent, HatConfigs.ObjectHatConfigs.TeethBonePath);
                break;
            case "Rubber Ducky":
                JollyHatActions.FixHatsScaleBasedOnSquishedParent(objParent, hatParent, 0.15f);
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(90, 0, 0));
                break;
            case "Gift":
                JollyHatActions.FixHatsScaleBasedOnSquishedParent(objParent, hatParent, 0.5f);
                JollyHatActions.ChangeRotOfHatParentChild(hatParent, new Vector3(90, 0, 0));
                break;
        }
    } 

    internal static void PlaceHatOnObjectTransform(Transform itemTransform, Item itemProp)
    {
        string itemName = itemProp.itemName;
        var (shouldPlaceHat, posOffset, rotOffset, scale) = GetItemHatConfig(itemName);
        if (!shouldPlaceHat || JollyHatActions.DoesTransformRootAlreadyContainJollyHat(itemTransform))
        {
            return;
        }
        Transform hatPrefab = JollyHatActions.PlaceHatOnTransform(itemTransform, posOffset, rotOffset, scale); 
        FixAdditionalChildrenPositions(itemName, itemTransform, hatPrefab);
    }

    internal static (bool, Vector3, Vector3, float) GetItemHatConfig(string itemName)
    {
        return itemName switch
        {
            "Fancy lamp" => HatConfigs.ObjectHatConfigs.GetFancyLampSantaHatConfig(),
            "Plastic cup" => HatConfigs.ObjectHatConfigs.GetPlasticCupSantaHatConfig(),
            "Jar of pickles" => HatConfigs.ObjectHatConfigs.GetPicklesSantaHatConfig(),
            "Toy robot" => HatConfigs.ObjectHatConfigs.GetToyRobotSantaHatConfig(),
            "Old phone" => HatConfigs.ObjectHatConfigs.GetOldPhoneSantaHatConfig(),
            "Plastic fish" => HatConfigs.ObjectHatConfigs.GetPlasticFishSantaHatConfig(),
            "Hive" => HatConfigs.ObjectHatConfigs.GetBeeHiveSantaHatConfig(),
            "Rubber Ducky" => HatConfigs.ObjectHatConfigs.GetRubberDuckySantaHatConfig(),
            "Cat" => HatConfigs.ObjectHatConfigs.GetCatSantaHatConfig(),
            "Teeth" => HatConfigs.ObjectHatConfigs.GetTeethSantaHatConfig(),
            "Gift" => HatConfigs.ObjectHatConfigs.GetGiftSantaHatConfig(),
                // Uncomment when debugging
            _ => HatConfigs.ObjectHatConfigs.GetIncompatibleSantaHatConfig() 
        };
    }
}
