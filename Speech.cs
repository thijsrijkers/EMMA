using System.Speech.Synthesis;

namespace EMMA
{
    public class Speech
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public void SetupSpeech(int rate, int volume, VoiceGender gender)
        {
            synth.Rate = rate;
            synth.Volume = volume;
            synth.SelectVoiceByHints(gender);
        }

        public void SpeakWord(string value)
        {
            synth.Speak(value);
        }
    }
}
