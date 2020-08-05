using System;

namespace Synth
{
    /// <summary>
    /// Pythagorean tuning centered on A4=432Hz
    /// </summary>
    public static class PythagoreanPitchTable
    {
        public static Func<double, double> C0 = (t) => 16;
        public static Func<double, double> Db0 = (t) => 17.09;
        public static Func<double, double> D0 = (t) => 18;
        public static Func<double, double> Eb0 = (t) => 19.221;
        public static Func<double, double> E0 = (t) => 20.25;
        public static Func<double, double> F0 = (t) => 21.333;
        public static Func<double, double> Gb0 = (t) => 22.78;
        public static Func<double, double> G0 = (t) => 24;
        public static Func<double, double> Ab0 = (t) => 25.629;
        public static Func<double, double> A0 = (t) => 27;
        public static Func<double, double> Bb0 = (t) => 28.444;
        public static Func<double, double> B0 = (t) => 30.375;

        public static Func<double, double> C1 = (t) => 32;
        public static Func<double, double> Db1 = (t) => 34.172;
        public static Func<double, double> D1 = (t) => 36;
        public static Func<double, double> Eb1 = (t) => 38.443;
        public static Func<double, double> E1 = (t) => 40.5;
        public static Func<double, double> F1 = (t) => 42.666;
        public static Func<double, double> Gb1 = (t) => 45.56;
        public static Func<double, double> G1 = (t) => 48;
        public static Func<double, double> Ab1 = (t) => 51.258;
        public static Func<double, double> A1 = (t) => 54;
        public static Func<double, double> Bb1 = (t) => 56.888;
        public static Func<double, double> B1 = (t) => 60.75;

        public static Func<double, double> C2 = (t) => 64;
        public static Func<double, double> Db2 = (t) => 68.344;
        public static Func<double, double> D2 = (t) => 72;
        public static Func<double, double> Eb2 = (t) => 76.776;
        public static Func<double, double> E2 = (t) => 81;
        public static Func<double, double> F2 = (t) => 85.33;
        public static Func<double, double> Gb2 = (t) => 91.125;
        public static Func<double, double> G2 = (t) => 96;
        public static Func<double, double> Ab2 = (t) => 102.515;
        public static Func<double, double> A2 = (t) => 108;
        public static Func<double, double> Bb2 = (t) => 113.777;
        public static Func<double, double> B2 = (t) => 121.5;

        public static Func<double, double> C3 = (t) => 128;
        public static Func<double, double> Db3 = (t) => 136.69;
        public static Func<double, double> D3 = (t) => 144;
        public static Func<double, double> Eb3 = (t) => 153.77;
        public static Func<double, double> E3 = (t) => 162;
        public static Func<double, double> F3 = (t) => 170.665;
        public static Func<double, double> Gb3 = (t) => 182.25;
        public static Func<double, double> G3 = (t) => 192;
        public static Func<double, double> Ab3 = (t) => 205.03;
        public static Func<double, double> A3 = (t) => 216;
        public static Func<double, double> Bb3 = (t) => 227.555;
        public static Func<double, double> B3 = (t) => 243;

        public static Func<double, double> C4 = (t) => 256;
        public static Func<double, double> Db4 = (t) => 273.375;
        public static Func<double, double> D4 = (t) => 288;
        public static Func<double, double> Eb4 = (t) => 307.546;
        public static Func<double, double> E4 = (t) => 324;
        public static Func<double, double> F4 = (t) => 341.33;
        public static Func<double, double> Gb4 = (t) => 364.5;
        public static Func<double, double> G4 = (t) => 384;
        public static Func<double, double> Ab4 = (t) => 410.06;
        public static Func<double, double> A4 = (t) => 432;
        public static Func<double, double> Bb4 = (t) => 455.111;
        public static Func<double, double> B4 = (t) => 486;

        public static Func<double, double> C5 = (t) => 512;
        public static Func<double, double> Db5 = (t) => 546.75;
        public static Func<double, double> D5 = (t) => 576;
        public static Func<double, double> Eb5 = (t) => 615.093;
        public static Func<double, double> E5 = (t) => 648;
        public static Func<double, double> F5 = (t) => 682.66;
        public static Func<double, double> Gb5 = (t) => 729;
        public static Func<double, double> G5 = (t) => 768;
        public static Func<double, double> Ab5 = (t) => 820.125;
        public static Func<double, double> A5 = (t) => 864;
        public static Func<double, double> Bb5 = (t) => 910.222;
        public static Func<double, double> B5 = (t) => 972;

        public static Func<double, double> C6 = (t) => 1024;
        public static Func<double, double> Db6 = (t) => 1093.5;
        public static Func<double, double> D6 = (t) => 1152;
        public static Func<double, double> Eb6 = (t) => 1230.18;
        public static Func<double, double> E6 = (t) => 1296;
        public static Func<double, double> F6 = (t) => 1365.22;
        public static Func<double, double> Gb6 = (t) => 1458;
        public static Func<double, double> G6 = (t) => 1536;
        public static Func<double, double> Ab6 = (t) => 1640.25;
        public static Func<double, double> A6 = (t) => 1728;
        public static Func<double, double> Bb6 = (t) => 1820.44;
        public static Func<double, double> B6 = (t) => 1944;

        public static Func<double, double> C7 = (t) => 2048;
        public static Func<double, double> Db7 = (t) => 2187;
        public static Func<double, double> D7 = (t) => 2304;
        public static Func<double, double> Eb7 = (t) => 2460.37;
        public static Func<double, double> E7 = (t) => 2592;
        public static Func<double, double> F7 = (t) => 2730.64;
        public static Func<double, double> Gb7 = (t) => 2916;
        public static Func<double, double> G7 = (t) => 3072;
        public static Func<double, double> Ab7 = (t) => 3280.5;
        public static Func<double, double> A7 = (t) => 3456;
        public static Func<double, double> Bb7 = (t) => 3640.88;
        public static Func<double, double> B7 = (t) => 3888;

        public static Func<double, double> C8 = (t) => 4096;
        public static Func<double, double> Db8 = (t) => 4374;
        public static Func<double, double> D8 = (t) => 4608;
        public static Func<double, double> Eb8 = (t) => 4920.75;
        public static Func<double, double> E8 = (t) => 5184;
        public static Func<double, double> F8 = (t) => 5461.88;
        public static Func<double, double> Gb8 = (t) => 5832;
        public static Func<double, double> G8 = (t) => 6144;
        public static Func<double, double> Ab8 = (t) => 6561;
        public static Func<double, double> A8 = (t) => 6912;
        public static Func<double, double> Bb8 = (t) => 7281.77;
        public static Func<double, double> B8 = (t) => 7776;

        public static Func<double, double> C9 = (t) => 8192;
        public static Func<double, double> Db9 = (t) => 8748;
        public static Func<double, double> D9 = (t) => 9216;
        public static Func<double, double> Eb9 = (t) => 9841.5;
        public static Func<double, double> E9 = (t) => 10368;
        public static Func<double, double> F9 = (t) => 10922.56;
        public static Func<double, double> Gb9 = (t) => 11664;
        public static Func<double, double> G9 = (t) => 12288;
        public static Func<double, double> Ab9 = (t) => 13122;
        public static Func<double, double> A9 = (t) => 13824;
        public static Func<double, double> Bb9 = (t) => 14563.55;
        public static Func<double, double> B9 = (t) => 15552;
    }
}
