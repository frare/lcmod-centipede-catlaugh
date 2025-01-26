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
        var volume = isTargetLocalPlayer ? CentipedeCatLaughBase.LocalPlayerClipVolume : CentipedeCatLaughBase.OtherPlayerClipVolume;
        __instance.creatureSFX.PlayOneShot(CentipedeCatLaughBase.SnareClip, volume);

        CentipedeCatLaughBase.LogMessage($"Playing audio clip for {(isTargetLocalPlayer ? "local player" : "other player")} at volume {volume}");

        CentipedeCatLaughBase.LogMessage("Done!");
    }
}