using System;
using System.ComponentModel;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace JustbokApplication.Helpers
{
    public class Speak
    {
        public void SpeakAsync(string msg,int volume=50)
        {
            System.Speech.Synthesis.SpeechSynthesizer speechSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
            speechSynthesizer.Volume = volume;
            speechSynthesizer.SpeakAsync(msg);
        }
    }
}
