using Newtonsoft.Json;
using System;
using SocketSaber.Utils;
using SocketSaber.EventModels;

namespace SocketSaber.EventProcessors {
    partial class MapProcessor {
        public static void MapEnd(EventList opcode, LevelCompletionResults results) {
            var levelEndData = new SongEndEM {
                causeOfEnd = results.levelEndAction.ToString(),
                energyLeft = results.energy,
                songEndedAt = results.endSongTime,
                gameplayModifiers = results.gameplayModifiers,
                rank = RankModel.GetRankName(results.rank),
                score = results.totalCutScore,
                combo = new ComboInfo {
                    isFullCombo = results.fullCombo,
                    maxCombo = results.maxCombo
                },
                cuts = new CutsInfo {
                    okCuts = results.okCount,
                    goodCuts = results.goodCutsCount,
                    badCuts = results.badCutsCount,
                    missedNotes = results.missedCount,
                    notGoodCuts = results.notGoodCount
                },
                movement = new MovementInfo {
                    right = new MovementInfo.SpecMovementInfo {
                        hand = results.rightHandMovementDistance,
                        saber = results.rightSaberMovementDistance
                    },
                    left = new MovementInfo.SpecMovementInfo {
                        hand = results.leftHandMovementDistance,
                        saber = results.leftSaberMovementDistance
                    }
                }
            };
            //Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict });
            Plugin.ConnProc.SendDataToAll(new BaseEM { Opcode = opcode, Data = levelEndData });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
        public static void MapEnd(EventList opcode, MultiplayerResultsData results) {
            var levelEndData = new SongEndEM {

            };
            //Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict, ["lobbyData"] = null });
            Plugin.ConnProc.SendDataToAll(new BaseEM { Opcode = opcode, Data = levelEndData });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
        public static void MapEnd(EventList opcode, MissionCompletionResults results) {
            var levelEndData = new SongEndEM {

            };
            //Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict, ["campaignData"] = null });
            Plugin.ConnProc.SendDataToAll(new BaseEM { Opcode = opcode, Data = levelEndData });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
    }
}
