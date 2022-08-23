namespace SocketSaber.EventModels {
    public class SongEndModel : IEventModel {
        /// <summary>
        /// What caused level ending
        /// </summary>
        public string causeOfEnd;
        /// <summary>
        /// How many energy left
        /// </summary>
        public float energyLeft;
        /// <summary>
        /// When song ended
        /// </summary>
        public float songEndedAt;
        /// <summary>
        /// With what gameplay modifiers player played
        /// </summary>
        public GameplayModifiers gameplayModifiers;
        /// <summary>
        /// Ranking grade, if level finished succefully
        /// </summary>
        public string rank;
        /// <summary>
        /// Total score with all modifications applied
        /// </summary>
        public int score;
        /// <summary>
        /// All info about combo
        /// </summary>
        public ComboInfo combo;
        /// <summary>
        /// All info about cuts
        /// </summary>
        public CutsInfo cuts;
        /// <summary>
        /// All info about your hands movement
        /// </summary>
        public MovementInfo movement;
    }
    /// <summary>
    /// Struct representing info about combo
    /// </summary>
    public struct ComboInfo {
        /// <summary>
        /// Is player got Full Combo (all notes has registred cuts)
        /// </summary>
        public bool isFullCombo;
        /// <summary>
        /// Maximal got combo
        /// </summary>
        public int maxCombo;
    }
    /// <summary>
    /// Struct representing info about cuts
    /// </summary>
    public struct CutsInfo {
        /// <summary>
        /// All good and registred cuts (maybe equals goodCuts? need to check...)
        /// </summary>
        public int okCuts;
        public int goodCuts;
        /// <summary>
        /// All missed and wrongly cutted notes
        /// </summary>
        public int badCuts;
        /// <summary>
        /// Only notes what you missed
        /// </summary>
        public int missedNotes;
        /// <summary>
        /// Only wrongly cutted notes
        /// </summary>
        public int notGoodCuts;
    }
    /// <summary>
    /// Struct representing info about hands or sabers movement
    /// </summary>
    public struct MovementInfo {
        /// <summary>
        /// Struct representing info about hand or saber movement
        /// </summary>
        public struct SpecMovementInfo {
            /// <summary>
            /// Movement of hand
            /// </summary>
            public float hand;
            /// <summary>
            /// Movement of saber
            /// </summary>
            public float saber;
        }
        /// <summary>
        /// Movements of right hand/saber
        /// </summary>
        public SpecMovementInfo right;
        /// <summary>
        /// Movements of left hand/saber
        /// </summary>
        public SpecMovementInfo left;
    }
}
