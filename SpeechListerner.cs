using System;
using System.Speech.Recognition;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace EMMA
{
    public class SpeechListerner
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public bool listining = false;
        private Speech speech;
        private List<string> pronounces = new List<string>();
        private string[] listOfProunces = {"and that","and Matt","M a","N a","and a","at a","of Maya","m at","M at","ms","Anna","Amat","emma","Emma","am a","Ina", "been a", "and not", "end the", "ban and", "and the", "and Maya", "Palma"};

        SpeechRecognitionEngine reconizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
    
        public SpeechListerner(Speech speech)
        {
            this.speech = speech;

            for(int i = 0; i < listOfProunces.Length; i++)
            {
                pronounces.Add(listOfProunces[i]);
            }

            reconizer.LoadGrammarAsync(new DictationGrammar());
            reconizer.SetInputToDefaultAudioDevice();
            reconizer.RecognizeAsync(RecognizeMode.Multiple);
            reconizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(reconizer_reconized);
        }

        private void reconizer_reconized(object sender, SpeechRecognizedEventArgs e)
        {
            if (listining)
            {
                if (e.Result.Text.Contains("never mind") || e.Result.Text.Contains("nothing"))
                {
                    var handle = GetConsoleWindow();
                    ShowWindow(handle, SW_HIDE);
                    speech.SpeakWord("No problem.");
                    listining = false;
                    return;
                }
                return;
            }

            if (!listining)
            {
                if(pronounces.Contains(e.Result.Text))
                {
                    var handle = GetConsoleWindow();
                    ShowWindow(handle, SW_SHOW);
                    speech.SpeakWord("Yes?");
                    listining = true;
                }
            }
        }
    }
}
