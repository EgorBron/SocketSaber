using BeatSaverSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketSaber.Utils;

namespace SocketSaber.EventProcessors {
    partial class MapProcessor {
        public static void MapStart(int opcode, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            var songDataDict = new Dict {
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
            };
            if (difficultyBeatmap.level.levelID.StartsWith("custom_level_")) {
                var getMapM = beatsaver.BeatmapByHash(difficultyBeatmap.level.levelID.Replace("custom_level_", ""));
                var map = getMapM.Result;
                songDataDict["mapBeatSaverID"] = map.ID;
                songDataDict["mapBeatSaverDescription"] = map.Description;
                songDataDict["mapScoreSaberRanked"] = map.Ranked;
                songDataDict["mapBeatSaverCoverLink"] = map.LatestVersion.CoverURL;
            }

            Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> {["songData"] = songDataDict, ["lobbyData"] = null, ["campaignData"] = null });
            Plugin.Log.Notice($"Level started. Opcode: {opcode}");
        }
    }
}
