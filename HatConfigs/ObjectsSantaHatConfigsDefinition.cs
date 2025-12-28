using UnityEngine;

namespace JollyLethal.HatConfigs;

internal class ObjectHatConfigs
{
    internal const string CatBonePath = "Cat_Simple_anim_IP/Arm_Cat/root_bone/Spine_base/spine_02/spine_03/spine_04/spine_05/neck/head";
    internal const string TeethBonePath = "AnimContainer/MeshContainer/JawUpper";
    internal static (bool, Vector3, Vector3, float) GetRubberDuckySantaHatConfig()
    {
        return (true, new Vector3(0, -0.2f, 2), new Vector3(0, 0, 180), 1);
    }
    internal static (bool, Vector3, Vector3, float) GetGiftSantaHatConfig()
    {
        return (true, new Vector3(0.7f, 1.5f, 2.2f), new Vector3(0, 0, 200), 1);
    }
    internal static (bool, Vector3, Vector3, float) GetTeethSantaHatConfig()
    {
        return (true, new Vector3(0.7f, 1.5f, 2.2f), new Vector3(10, 120, 0), 6);
    }
    internal static (bool, Vector3, Vector3, float) GetCatSantaHatConfig()
    {
        return (true, new Vector3(0, 0.035f, 0.06f), new Vector3(80, 0, 0), 0.11f);
    }
    internal static (bool, Vector3, Vector3, float) GetBeeHiveSantaHatConfig()
    {
        return (true, new Vector3(0, 1, 0.2f), new Vector3(0, 225, 0), 1.75f);
    }
    internal static (bool, Vector3, Vector3, float) GetFancyLampSantaHatConfig()
    {
        return (true, new Vector3(0, 0, 2.25f), new Vector3(270, 180, 0), 1);
    }
    internal static (bool, Vector3, Vector3, float) GetPlasticCupSantaHatConfig()
    {
        return (true, new Vector3(0, 0, 0.4f), new Vector3(270, 180, 0), 0.9f);
    }
    internal static (bool, Vector3, Vector3, float) GetPicklesSantaHatConfig()
    {
        return (true, new Vector3(0, 0, 2), new Vector3(90, 0, 0), 3);
    }
    internal static (bool, Vector3, Vector3, float) GetToyRobotSantaHatConfig()
    {
        return (true, new Vector3(0, 0, 2.35f), new Vector3(90, 0, 0), 1.25f);
    }
    internal static (bool, Vector3, Vector3, float) GetOldPhoneSantaHatConfig()
    {
        return (true, new Vector3(0, 0.85f, 0), new Vector3(0, 115, 0), 1);
    }
    internal static (bool, Vector3, Vector3, float) GetPlasticFishSantaHatConfig()
    {
        return (true, new Vector3(0, -0.7f, 1.3f), new Vector3(270, 180, 0), 1);
    }
    internal static (bool, Vector3, Vector3, float) GetDefaultSantaHatConfig()
    {
        return (true, Vector3.zero, Vector3.zero, 1);
    }
    internal static (bool, Vector3, Vector3, float) GetIncompatibleSantaHatConfig()
    {
        return (false, Vector3.zero, Vector3.zero, 0);
    }
}
