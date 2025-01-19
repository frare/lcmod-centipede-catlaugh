using HarmonyLib;

namespace CentipedeCatLaugh.Patches;

[HarmonyPatch(typeof(CentipedeAI))]
internal static class CentipedeAIPatch
{
    [HarmonyPatch("ClingToPlayerClientRpc"), HarmonyPostfix]
    internal static void ClingToPlayerClientRpcPostfix(ref CentipedeAI __instance)
    {
        CentipedeCatLaughBase.LogMessage(
            $"Patching \"CentipedeAI ClingToPlayerClientRpc\"... for gameObject {__instance.gameObject.name}",
            BepInEx.Logging.LogLevel.Debug
        );

        __instance.creatureSFX.PlayOneShot(CentipedeCatLaughBase.SnareClip);

        CentipedeCatLaughBase.LogMessage("Done!", BepInEx.Logging.LogLevel.Debug);
    }
}