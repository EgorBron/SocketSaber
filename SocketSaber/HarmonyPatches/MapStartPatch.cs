using System;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using BeatSaverSharp;
using System.Threading;

namespace SocketSaber.HarmonyPatches {
    [HarmonyPatch]
    internal static class SongInfoGetterSolo {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), "Init")]
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            new Thread(() => {
                EventProcessors.MapStarted.Process(10, difficultyBeatmap, gameplayModifiers);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongInfoGetterMultiplayer {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), "Init")]
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            new Thread(() => {
                EventProcessors.MapStarted.Process(11, difficultyBeatmap, gameplayModifiers);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongInfoGetterCampaign {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MissionLevelScenesTransitionSetupDataSO), "Init")]
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            new Thread(() => {
                EventProcessors.MapStarted.Process(12, difficultyBeatmap, gameplayModifiers);
            }).Start();
        }
    }
}
