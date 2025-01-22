using GameNetcodeStuff;
using HarmonyLib;

namespace CentipedeCatLaugh.Patches;

[HarmonyPatch(typeof(CentipedeAI))]
internal static class CentipedeAIPatch
{
    [HarmonyPatch("ClingToPlayer"), HarmonyPostfix]
    internal static void ClingToPlayerPostfix(ref CentipedeAI __instance, PlayerControllerB playerScript)
    {
        CentipedeCatLaughBase.LogMessage(
            $"Patching \"CentipedeAI ClingToPlayer\"... for gameObject {__instance.gameObject.name}"
        );

        var isTargetLocalPlayer = playerScript == GameNetworkManager.Instance.localPlayerController;
        var volume = isTargetLocalPlayer ? 1f : 1.5f;
        __instance.creatureSFX.PlayOneShot(CentipedeCatLaughBase.SnareClip, volume);

        CentipedeCatLaughBase.LogMessage("Done!");
    }
}