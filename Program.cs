using System;
using System.Runtime.InteropServices;

namespace EMMA
{
    public class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            Speech speech = new Speech();
            speech.SetupSpeech(1, 40, System.Speech.Synthesis.VoiceGender.Female);

            SpeechListerner listerner = new SpeechListerner(speech);

            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
