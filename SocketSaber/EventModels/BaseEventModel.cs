namespace SocketSaber.EventModels {
    public class BaseEventModel {
        /// <summary>
        /// Opcode
        /// </summary>
        public int op;
        /// <summary>
        /// Data
        /// </summary>
        public IEventModel d;
    }
}
