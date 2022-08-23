using System.Collections.Generic;
using SocketSaber.EventModels;
using SocketSaber.Utils;

namespace SocketSaber.EventProcessors {
    partial class MapProcessor {
        public static void MapStart(int opcode, IDifficultyBeatmap difficultyBeatmap, GameplayModifiers gameplayModifiers) {
            var songData = new SongStartModel {
                songName = difficultyBeatmap.level.songName,
                songAuthorName = difficultyBeatmap.level.songAuthorName,
                duration = difficultyBeatmap.level.songDuration,
                songSubName = difficultyBeatmap.level.songSubName,
                mapAuthor = difficultyBeatmap.level.levelAuthorName,
                mapGameLevelID = difficultyBeatmap.level.levelID,
                songBPM = difficultyBeatmap.level.beatsPerMinute,
                mapDifficulty = difficultyBeatmap.difficulty.ToString(),
                mapNJS = difficultyBeatmap.noteJumpMovementSpeed,
                mapBeatSaverID = null,
                mapBeatSaverDescription = null,
                mapBeatSaverCoverLink = null,
                mapScoreSaberRanked = false
            };
            if (difficultyBeatmap.level.levelID.StartsWith("custom_level_")) {
                var getMapM = beatsaver.BeatmapByHash(difficultyBeatmap.level.levelID.Replace("custom_level_", ""));
                var map = getMapM.Result;
                songData.mapBeatSaverID = map.ID;
                songData.mapBeatSaverDescription = map.Description;
                songData.mapScoreSaberRanked = map.Ranked;
                songData.mapBeatSaverCoverLink = map.LatestVersion.CoverURL;
            }
            Plugin.ConnProc.SendDataToAll(new BaseEventModel { op = opcode, d = songData });
            Plugin.Log.Notice($"Level started. Opcode: {opcode}");
        }
    }
}
