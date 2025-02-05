using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace TextRPG
{
    public class AudioManager
    {
        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new AudioManager(); }

                return _instance;
            }
        }

        // private 생성자로 외부에서 new 방지
        private AudioManager() { }

        static IWavePlayer bgmPlayer;
        static AudioFileReader bgmReader;

        static float bgmVolume = 0.2f;
        static bool isBGM_Player = false;

        public static string pathBGM = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music", "bgmusic1.mp3");

        public void PlayBGM(string filePath)
        {
            StopBGM();

            bgmPlayer = new WaveOutEvent();     //배경음악 플레이어 생성
            bgmReader = new AudioFileReader(filePath)       //배경음악 파일 불러오기
            {
                Volume = bgmVolume    //볼륨 조절
            };
            bgmPlayer.Init(bgmReader);      //배경음악 플레이어에 음악 집어넣기

            bgmPlayer.Play();

            isBGM_Player=true;

            bgmPlayer.PlaybackStopped += (sender, arg) =>
            {
                isBGM_Player = false;
            };
        }

        public void StopBGM()
        {
            bgmPlayer?.Stop();
            bgmPlayer?.Dispose();
            bgmReader?.Dispose();
        }

        public async Task BGM_Start()
        {
            while (true)
            {
                if (isBGM_Player == false)
                {
                    PlayBGM(AudioManager.pathBGM);
                }

                await Task.Delay(300);
            }
        }
    }

}
