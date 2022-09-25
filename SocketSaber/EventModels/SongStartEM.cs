namespace SocketSaber.EventModels {
    public delegate void SongStartEventDelegate(SongStartEM e);
    public class SongStartEM : IEventModel {
        /// <summary>
        /// Song display name
        /// </summary>
        public string songName;
        /// <summary>
        /// Song author display name
        /// </summary>
        public string songAuthorName;
        /// <summary>
        /// Duration of map and song
        /// </summary>
        public float duration;
        /// <summary>
        /// Subname for song (for example "Original mix", "ft. Someone", "Cut ver.")
        /// </summary>
        public string songSubName;
        /// <summary>
        /// Map author display name
        /// </summary>
        public string mapAuthor;
        /// <summary>
        /// Map ID given by game
        /// </summary>
        public string mapGameLevelID;
        /// <summary>
        /// Beats Per Minute in song
        /// </summary>
        public float songBPM;
        /// <summary>
        /// Current map difficulty
        /// </summary>
        public string mapDifficulty;
        /// <summary>
        /// Map Note Jump Speed (how fast notes moving)
        /// </summary>
        public float mapNJS;
        /// <summary>
        /// Map ID given by BeatSaver (only for custom maps)
        /// </summary>
        public string mapBeatSaverID;
        /// <summary>
        /// Map description on BeatSaver (only for custom maps) 
        /// </summary>
        public string mapBeatSaverDescription;
        /// <summary>
        /// Map cover image on BeatSaver (only for custom maps)
        /// </summary>
        public string mapBeatSaverCoverLink;
        /// <summary>
        /// Is map ranked on ScoreSaber (only for custom maps)
        /// </summary>
        public bool mapScoreSaberRanked;
    }
}
