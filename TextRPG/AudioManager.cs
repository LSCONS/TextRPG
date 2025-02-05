using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace TextRPG
{
    public static class AudioManager
    {
        static IWavePlayer bgmPlayer;
        static AudioFileReader bgmReader;

        static IWavePlayer SE_Player;
        static AudioFileReader SE_Reader;

        static float soundVolume = 0.2f;

        static int soundDelayTime = 100; //다른 효과음을 0.1초 지연시켜서 무언가를 입력할 때마다 나오는 movemenu효과음을 무시

        static bool isBGM_Player = false;
        static bool isPlayerDie = false;


        #region 음악 경로 지정 관련 필드 모음
        static string pathMusicFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "music\\");

        static string pathBGM = pathMusicFolder + "BGM.mp3";                            //배경음악
        static string pathMoveMenuSE = pathMusicFolder + "MenuMove.mp3";                //메뉴가 나타날 때마다 나오는 기본 효과음
        static string pathItemEquippedSE = pathMusicFolder + "ItemEquipped.mp3";        //아이템 장착 및 해제시 나오는 효과음
        static string pathItemBuyOrSellSE = pathMusicFolder + "ItemBuyOrSell.mp3";      //아이템 구매 및 판매시 나오는 효과음
        static string pathDungeonClearSE = pathMusicFolder + "DungeonClear.mp3";        //던전 클리어 시 나오는 효과음
        static string pathDungeonFailedSE = pathMusicFolder + "DungeonFailed.mp3";      //던전 실패 시 나오는 효과음
        static string pathLevelUpSE = pathMusicFolder + "PlayerLevelUp.mp3";            //플레이어의 레벨이 오를 경우 나오는 효과음
        static string pathPlayerDieSE = pathMusicFolder + "PlayerDie.mp3";              //플레이어 사망 시 나오는 효과음
        #endregion


        #region 효과음 실행 메서드 모음
        public static void PlayMoveMenuSE()
        {
            PlaySE(pathMoveMenuSE);
        }
        public static async void PlayItemEquippedSE()
        {
            await Task.Delay(soundDelayTime);
            PlaySE(pathItemEquippedSE);
        }
        public static async void PlayItemBuyOrSellSE()
        {
            await Task.Delay(soundDelayTime);
            PlaySE(pathItemBuyOrSellSE);
        }
        public static async void PlayDungeonClearSE()
        {
            await Task.Delay(soundDelayTime);
            PlaySE(pathDungeonClearSE);
        }
        public static async void PlayDungeonFailedSE()
        {
            await Task.Delay(soundDelayTime);
            PlaySE(pathDungeonFailedSE);
        }
        public static async void PlayLevelUpSE()
        {
            await Task.Delay(soundDelayTime);
            PlaySE(pathLevelUpSE);
        }
        public static async void PlayPlayerDieSE()
        {
            await Task.Delay(soundDelayTime);
            PlaySE(pathPlayerDieSE);
        }
        #endregion


        //효과음을 세팅하고 실행하는 메서드
        public static void PlaySE(string filePath)
        {
            StopPlayerAndReader(SE_Player, SE_Reader);

            SE_Player = new WaveOutEvent();     //배경음악 플레이어 생성
            SE_Reader = new AudioFileReader(filePath)       //배경음악 파일 불러오기
            {
                Volume = soundVolume    //볼륨 조절
            };
            SE_Player.Init(SE_Reader);      //배경음악 플레이어에 음악 집어넣기

            SE_Player.Play();
        }


        //배경음악을 집어넣는 메서드
        public static void SettingBGM(string filePath)
        {
            StopPlayerAndReader(bgmPlayer, bgmReader);

            bgmPlayer = new WaveOutEvent();     //배경음악 플레이어 생성
            bgmReader = new AudioFileReader(filePath)       //배경음악 파일 불러오기
            {
                Volume = soundVolume    //볼륨 조절
            };
            bgmPlayer.Init(bgmReader);      //배경음악 플레이어에 음악 집어넣기

            bgmPlayer.Play();

            isBGM_Player=true;

            bgmPlayer.PlaybackStopped += (sender, arg) =>
            {
                isBGM_Player = false;
            };
        }


        //연결된 플레이어와 리더를 해제하는 메서드
        public static void StopPlayerAndReader(IWavePlayer wavePlayer, AudioFileReader audioFileReader)
        {
            wavePlayer?.Stop();
            wavePlayer?.Dispose();
            audioFileReader?.Dispose();
        }


        //배경 음악 실행을 시작하는 메서드
        public static async Task BGM_Start()
        {
            while (true)
            {
                if (isBGM_Player == false && isPlayerDie == false)
                {
                    SettingBGM(AudioManager.pathBGM);
                }
                else if(isPlayerDie == true)
                {
                    StopPlayerAndReader(bgmPlayer, bgmReader);
                }

                await Task.Delay(100);
            }
        }
    }

}
