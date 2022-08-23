using BeatSaverSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketSaber.Utils;
using SocketSaber.EventModels;
using UnityEngine.UI;

namespace SocketSaber.EventProcessors {
    partial class MapProcessor {
        public static void MapEnd(int opcode, LevelCompletionResults results) {
            var levelEndData = new SongEndModel {
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
            Plugin.ConnProc.SendDataToAll(new BaseEventModel { op = opcode, d = levelEndData });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
        public static void MapEnd(int opcode, MultiplayerResultsData results) {
            var levelEndData = new SongEndModel {

            };
            //Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict, ["lobbyData"] = null });
            Plugin.ConnProc.SendDataToAll(new BaseEventModel { op = opcode, d = levelEndData });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
        public static void MapEnd(int opcode, MissionCompletionResults results) {
            var levelEndData = new SongEndModel {

            };
            //Plugin.ConnProc.SendDataToAll(opcode, new Dictionary<string, object> { ["levelEndData"] = levelEndDataDict, ["campaignData"] = null });
            Plugin.ConnProc.SendDataToAll(new BaseEventModel { op = opcode, d = levelEndData });
            Plugin.Log.Notice($"Level ended. Opcode: {opcode}");
        }
    }
}
