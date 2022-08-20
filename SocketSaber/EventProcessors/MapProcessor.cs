using BeatSaverSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketSaber.Utils;

namespace SocketSaber.EventProcessors {
    class MapProcessor {
        static BeatSaver beatsaver = new BeatSaver("SocketSaberClient", new Version(0, 0, 1));

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
        public static void MapEnd(int opcode, LevelCompletionResults results) {
            var levelEndDataDict = new Dict {
                ["causeOfEnd"] = results.levelEndAction.ToString(),
                ["energyLeft"] = results.energy,
                ["songEndedAt"] = results.endSongTime,
                ["gameplayModifiers"] = results.gameplayModifiers,
                ["rank"] = results.rank,
                ["score"] = results.totalCutScore,
                ["combo"] = new Dict {
                    ["isFullCombo"] = results.fullCombo,
                    ["maxCombo"] = results.maxCombo
                },
                ["cuts"] = new Dict {
                    // okCuts - all GOOD and REGISTRED cuts (maybe equals goodCuts? need to check...)
                    ["okCuts"] = results.okCount,
                    ["goodCuts"] = results.goodCutsCount,
                    // badCuts - all MISSED and WRONGLY CUTTED notes
                    ["badCuts"] = results.badCutsCount,
                    // just notes what you missed
                    ["missedNotes"] = results.missedCount,
                    // wrongly cutted notes
                    ["notGoodCuts"] = results.notGoodCount
                },
                ["movement"] = new Dict {
                    ["right"] = new Dict {
                        ["hand"] = results.rightHandMovementDistance,
                        ["saber"] = results.rightSaberMovementDistance
                    },
                    ["left"] = new Dict {
                        ["hand"] = results.leftHandMovementDistance,
                        ["saber"] = results.leftSaberMovementDistance
                    }
                }
            };
            Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
        public static void MapEnd(int opcode, MultiplayerResultsData results) {
            var levelEndDataDict = new Dictionary<string, object> {

            };
            Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict, ["lobbyData"] = null});
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
        public static void MapEnd(int opcode, MissionCompletionResults results) {
            var levelEndDataDict = new Dictionary<string, object> {

            };
            Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict, ["campaignData"] = null });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
    }
}
