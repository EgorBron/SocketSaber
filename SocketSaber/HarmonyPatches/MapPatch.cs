﻿using System;
using HarmonyLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using BeatSaverSharp;
using System.Threading;
using System.Reflection.Emit;
using SocketSaber.Utils;

namespace SocketSaber.HarmonyPatches {
    [HarmonyPatch]
    internal static class SongStartInfoGetterSoloPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), "Init")]
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            new Thread(() => {
                EventProcessors.MapProcessor.MapStart(11, difficultyBeatmap, gameplayModifiers);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongStartInfoGetterMultiplayerPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), "Init")]
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            new Thread(() => {
                EventProcessors.MapProcessor.MapStart(12, difficultyBeatmap, gameplayModifiers);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongStartInfoGetterCampaignPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MissionLevelScenesTransitionSetupDataSO), "Init")]
        private static void Postfix(IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            new Thread(() => {
                EventProcessors.MapProcessor.MapStart(13, difficultyBeatmap, gameplayModifiers);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongEndInfoGetterSoloPatch {
        static TimeSpan lastcall;
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(StandardLevelScenesTransitionSetupDataSO), "Finish")]
        private static void Postfix(LevelCompletionResults levelCompletionResults) {
            var now = new TimeSpan();
            if (lastcall == null) lastcall = now;
            if (now == lastcall || (now - lastcall).TotalMilliseconds > 1000) {
                    new Thread(() => {
                        EventProcessors.MapProcessor.MapEnd(14, levelCompletionResults);
                    }).Start();
                    lastcall = now;
                }
        }
    }
    [HarmonyPatch]
    internal static class SongEndInfoGetterMultiplayerPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), "Finish")]
        private static void Postfix(MultiplayerResultsData resultsData) {
            new Thread(() => {
                EventProcessors.MapProcessor.MapEnd(15, resultsData);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongEndDisconnectInfoGetterMultiplayerPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MultiplayerLevelScenesTransitionSetupDataSO), "FinishWithDisconnect")]
        private static void Postfix(object disconnectedReason) { //IL_000c: Unknown result type (might be due to invalid IL or missing references)
            //new Thread(() => {
                //EventProcessors.MapProcessor.Process(15, );
            //}).Start();
        }
    }
    [HarmonyPatch]
    internal static class SongEndInfoGetterCampaignPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MissionLevelScenesTransitionSetupDataSO), "Finish")]
        private static void Postfix(MissionCompletionResults levelCompletionResults) {
            new Thread(() => {
                EventProcessors.MapProcessor.MapEnd(16, levelCompletionResults);
            }).Start();
        }
    }
    [HarmonyPatch]
    internal class MenueLoadPatch {
        [HarmonyPriority(int.MinValue)]
        [HarmonyPatch(typeof(MenuScenesTransitionSetupDataSO), "Init")]
        private void Postfix() {
            new Thread(() => {
                var mainDict = new Dict {
                    ["op"] = 10,
                    ["d"] = null
                };
                Plugin.ConnProc.SendRawDataToAll(JsonConvert.SerializeObject(mainDict));
            }).Start();
        }
    }
}