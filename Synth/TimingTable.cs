namespace Synth
{
    // todo: add <summary> and rescale these out to resemble SID tables
    // e.g. choose what times you want first then find the co-efficient
    // consider popular BPM, 240, 96 hiphop whatevs  240 = 0.25s 
    public static class TimingTable
    {
        /// <summary>
        /// 24 seconds
        /// </summary>
        public const byte S24 = 1;

        /// <summary>
        /// 12 seconds
        /// </summary>
        public const byte S12 = 2;

        /// <summary>
        /// 8 seconds
        /// </summary>
        public const byte S8 = 3;

        /// <summary>
        /// 6 seconds
        /// </summary>
        public const byte S6 = 4;


        public const byte S5 = 5;
        public const byte S4 = 6;
        public const byte S3_5 = 7;
        public const byte S3_1 = 8;
        public const byte S2_85 = 9;

        /// <summary>
        /// 1 second
        /// </summary>
        public const byte S1 = 32;
    }
}