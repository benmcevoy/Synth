using System;

namespace Synth.Frequency
{
    /// <summary>
    /// Standard equal temperament, nine octaves each starting from C, e.g C4,D4,E4,F4,G4,A4,B4,C5 etc.
    /// </summary>
    public static class PitchTable
    {
        public static Func<Time, double> C0 = (t) => 16.351;
        public static Func<Time, double> Db0 = (t) => 17.324;
        public static Func<Time, double> D0 = (t) => 18.354;
        public static Func<Time, double> Eb0 = (t) => 19.445;
        public static Func<Time, double> E0 = (t) => 20.601;
        public static Func<Time, double> F0 = (t) => 21.827;
        public static Func<Time, double> Gb0 = (t) => 23.124;
        public static Func<Time, double> G0 = (t) => 24.499;
        public static Func<Time, double> Ab0 = (t) => 25.956;
        public static Func<Time, double> A0 = (t) => 27.5;
        public static Func<Time, double> Bb0 = (t) => 29.135;
        public static Func<Time, double> B0 = (t) => 30.868;
        
        public static Func<Time, double> C1 = (t) => 32.703;
        public static Func<Time, double> Db1 = (t) => 34.648;
        public static Func<Time, double> D1 = (t) => 36.708;
        public static Func<Time, double> Eb1 = (t) => 38.891;
        public static Func<Time, double> E1 = (t) => 41.203;
        public static Func<Time, double> F1 = (t) => 43.654;
        public static Func<Time, double> Gb1 = (t) => 46.249;
        public static Func<Time, double> G1 = (t) => 48.999;
        public static Func<Time, double> Ab1 = (t) => 51.913;
        public static Func<Time, double> A1 = (t) => 55;
        public static Func<Time, double> Bb1 = (t) => 58.27;
        public static Func<Time, double> B1 = (t) => 61.735;
        
        public static Func<Time, double> C2 = (t) => 65.406;
        public static Func<Time, double> Db2 = (t) => 69.296;
        public static Func<Time, double> D2 = (t) => 73.416;
        public static Func<Time, double> Eb2 = (t) => 77.782;
        public static Func<Time, double> E2 = (t) => 82.407;
        public static Func<Time, double> F2 = (t) => 87.307;
        public static Func<Time, double> Gb2 = (t) => 92.499;
        public static Func<Time, double> G2 = (t) => 97.999;
        public static Func<Time, double> Ab2 = (t) => 103.826;
        public static Func<Time, double> A2 = (t) => 110;
        public static Func<Time, double> Bb2 = (t) => 116.541;
        public static Func<Time, double> B2 = (t) => 123.471;
        
        public static Func<Time, double> C3 = (t) => 130.813;
        public static Func<Time, double> Db3 = (t) => 138.591;
        public static Func<Time, double> D3 = (t) => 146.832;
        public static Func<Time, double> Eb3 = (t) => 155.563;
        public static Func<Time, double> E3 = (t) => 164.814;
        public static Func<Time, double> F3 = (t) => 174.614;
        public static Func<Time, double> Gb3 = (t) => 184.997;
        public static Func<Time, double> G3 = (t) => 195.998;
        public static Func<Time, double> Ab3 = (t) => 207.652;
        public static Func<Time, double> A3 = (t) => 220;
        public static Func<Time, double> Bb3 = (t) => 233.082;
        public static Func<Time, double> B3 = (t) => 246.942;
        
        public static Func<Time, double> C4 = (t) => 261.626;
        public static Func<Time, double> Db4 = (t) => 277.183;
        public static Func<Time, double> D4 = (t) => 293.665;
        public static Func<Time, double> Eb4 = (t) => 311.127;
        public static Func<Time, double> E4 = (t) => 329.628;
        public static Func<Time, double> F4 = (t) => 349.228;
        public static Func<Time, double> Gb4 = (t) => 369.994;
        public static Func<Time, double> G4 = (t) => 391.995;
        public static Func<Time, double> Ab4 = (t) => 415.305;
        public static Func<Time, double> A4 = (t) => 440;
        public static Func<Time, double> Bb4 = (t) => 466.164;
        public static Func<Time, double> B4 = (t) => 493.883;
        
        public static Func<Time, double> C5 = (t) => 523.251;
        public static Func<Time, double> Db5 = (t) => 554.365;
        public static Func<Time, double> D5 = (t) => 587.33;
        public static Func<Time, double> Eb5 = (t) => 622.254;
        public static Func<Time, double> E5 = (t) => 659.255;
        public static Func<Time, double> F5 = (t) => 698.456;
        public static Func<Time, double> Gb5 = (t) => 739.989;
        public static Func<Time, double> G5 = (t) => 783.991;
        public static Func<Time, double> Ab5 = (t) => 830.609;
        public static Func<Time, double> A5 = (t) => 880;
        public static Func<Time, double> Bb5 = (t) => 932.328;
        public static Func<Time, double> B5 = (t) => 987.767;
        
        public static Func<Time, double> C6 = (t) => 1046.502;
        public static Func<Time, double> Db6 = (t) => 1108.731;
        public static Func<Time, double> D6 = (t) => 1174.659;
        public static Func<Time, double> Eb6 = (t) => 1244.508;
        public static Func<Time, double> E6 = (t) => 1318.51;
        public static Func<Time, double> F6 = (t) => 1396.913;
        public static Func<Time, double> Gb6 = (t) => 1479.978;
        public static Func<Time, double> G6 = (t) => 1567.982;
        public static Func<Time, double> Ab6 = (t) => 1661.219;
        public static Func<Time, double> A6 = (t) => 1760;
        public static Func<Time, double> Bb6 = (t) => 1864.655;
        public static Func<Time, double> B6 = (t) => 1975.533;
        
        public static Func<Time, double> C7 = (t) => 2093.005;
        public static Func<Time, double> Db7 = (t) => 2217.461;
        public static Func<Time, double> D7 = (t) => 2349.318;
        public static Func<Time, double> Eb7 = (t) => 2489.016;
        public static Func<Time, double> E7 = (t) => 2637.021;
        public static Func<Time, double> F7 = (t) => 2793.826;
        public static Func<Time, double> Gb7 = (t) => 2959.955;
        public static Func<Time, double> G7 = (t) => 3135.964;
        public static Func<Time, double> Ab7 = (t) => 3322.438;
        public static Func<Time, double> A7 = (t) => 3520;
        public static Func<Time, double> Bb7 = (t) => 3729.31;
        public static Func<Time, double> B7 = (t) => 3951.066;
        
        public static Func<Time, double> C8 = (t) => 4186.009;
        public static Func<Time, double> Db8 = (t) => 4434.922;
        public static Func<Time, double> D8 = (t) => 4698.636;
        public static Func<Time, double> Eb8 = (t) => 4978.032;
        public static Func<Time, double> E8 = (t) => 5274.042;
        public static Func<Time, double> F8 = (t) => 5587.652;
        public static Func<Time, double> Gb8 = (t) => 5919.91;
        public static Func<Time, double> G8 = (t) => 6271.928;
        public static Func<Time, double> Ab8 = (t) => 6644.876;
        public static Func<Time, double> A8 = (t) => 7040;
        public static Func<Time, double> Bb8 = (t) => 7458.62;
        public static Func<Time, double> B8 = (t) => 7902.132;
        
        public static Func<Time, double> C9 = (t) => 8372.018;
        public static Func<Time, double> Db9 = (t) => 8869.844;
        public static Func<Time, double> D9 = (t) => 9397.272;
        public static Func<Time, double> Eb9 = (t) => 9956.064;
        public static Func<Time, double> E9 = (t) => 10548.084;
        public static Func<Time, double> F9 = (t) => 11175.304;
        public static Func<Time, double> Gb9 = (t) => 11839.82;
        public static Func<Time, double> G9 = (t) => 12543.856;
        public static Func<Time, double> Ab9 = (t) => 13289.752;
        public static Func<Time, double> A9 = (t) => 14080;
        public static Func<Time, double> Bb9 = (t) => 14917.24;
        public static Func<Time, double> B9 = (t) => 15804.264;
    }
}
