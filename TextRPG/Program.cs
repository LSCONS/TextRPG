using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TextRPG
{
    internal class Program
    {
        //게임 시작
        static void Main(string[] args)
        {
            GameManager gameManager = GameManager.Instance;
            AudioManager.PlayBGM(AudioManager.pathBGM);
            gameManager.StartGame();
        }
    }
}
