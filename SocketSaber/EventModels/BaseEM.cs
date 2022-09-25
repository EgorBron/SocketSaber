using Newtonsoft.Json;

namespace SocketSaber.EventModels {
    /// <summary>
    /// Event model (EM) interface
    /// </summary>
    public interface IEventModel { }
    /// <summary>
    /// Delegate for any event (ingame)
    /// </summary>
    /// <param name="e"></param>
    public delegate void BaseEventDelegate(IEventModel e);
    /// <summary>
    /// List of opcodes (there it means type of event)
    /// </summary>
    public enum EventList {
        MenuLoad = 10,
        SongStart = 11,
        SongMultiplayerStart = 12,
        SongCampaignStart = 13,
        SongEnd = 14,
        SongMultiplayerEnd = 15,
        SongCampaignEnd = 16,
        SongPaused = 17,
        SongResumed = 18,
    }
    /// <summary>
    /// Event model base (it used only to send data to socket clients)
    /// </summary>
    public class BaseEM {
        /// <summary>
        /// Operation code (opcode, there it means type of event) || "op" key
        /// </summary>
        [JsonProperty("op")]
        public EventList Opcode;
        /// <summary>
        /// Data of event || "d" key
        /// </summary>
        [JsonProperty("d")]
        public IEventModel Data;
    }
}
