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
                var beatsaver = new BeatSaver("SocketSaberClient", new Version(0, 0, 1));
                var dict = new Dictionary<string, object> {
                    ["op"] = 10, // solo song update
                    ["d"] = new Dictionary<string, object> {
                        ["songName"] = difficultyBeatmap.level.songName,
                        ["songAuthorName"] = difficultyBeatmap.level.songAuthorName,
                        ["duration"] = difficultyBeatmap.level.songDuration,
                        ["songSubName"] = difficultyBeatmap.level.songSubName,
                        ["mapAuthor"] = difficultyBeatmap.level.levelAuthorName,
                        ["mapGameLevelID"] = difficultyBeatmap.level.levelID,
                        ["songBPM"] = difficultyBeatmap.level.beatsPerMinute,
                        ["mapDifficulty"] = difficultyBeatmap.difficulty.ToString(),
                        ["mapNJS"] = difficultyBeatmap.noteJumpMovementSpeed,
                        ["mapBeatSaverID"] = null,
                        ["mapBeatSaverDescription"] = null,
                        ["mapBeatSaverCoverLink"] = null,
                        ["mapScoreSaberRanked"] = null
                    }
                };
                if (difficultyBeatmap.level.levelID.StartsWith("custom_level_")) {
                    var getMapM = beatsaver.BeatmapByHash(difficultyBeatmap.level.levelID.Replace("custom_level_", ""));
                    var map = getMapM.Result;
                    dict["mapBeatSaverID"] = map.ID;
                    dict["mapBeatSaverDescription"] = map.Description;
                    dict["mapScoreSaberRanked"] = map.Ranked;
                    dict["mapBeatSaverCoverLink"] = map.LatestVersion.CoverURL;
                }
                Plugin.ConnProc.SendToAll(JsonConvert.SerializeObject(dict));
            }).Start();    
        }
	}
}
