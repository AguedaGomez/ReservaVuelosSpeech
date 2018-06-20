using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReservaVuelo_Speech_
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("es-ES"));
        InputCheck inputCheck = new InputCheck();
        bool startRecognition = false;

        readonly string buttonTextListening = "<Escuchando...>";
        readonly string buttonTextWaiting = "Pulsa para hablar";
        readonly string buttonTextUnderstand = "Perdona, no te he entendido";

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Grammar grammar = new Grammar("GramaticaVuelos.xml");
            speechRecognizer.LoadGrammar(grammar);
            speechRecognizer.SpeechRecognized += SpeechRecognizer_SpeechRecognized;
            speechRecognizer.SpeechDetected += SpeechRecognizer_SpeechDetected;
            speechRecognizer.SpeechRecognitionRejected += SpeechRecognizer_SpeechRecognitionRejected;
            speechRecognizer.SetInputToDefaultAudioDevice();
            speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void SpeechRecognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if(startRecognition)
                RecognizedButton.Content = buttonTextUnderstand;
        }

        private void SpeechRecognizer_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            
            Console.WriteLine("<Voz detectada>");
        }

        private void SpeechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (startRecognition)
            {
                Console.WriteLine(e.Result.Text);
                Origin.Text = inputCheck.CheckOrigin(e.Result.Semantics["Origen"].Value.ToString());
                Destination.Text = e.Result.Semantics["Destino"].Value.ToString();
                Date.Text = inputCheck.CheckDate(e.Result.Semantics["Fecha"].Value.ToString());
                NTickets.Text = inputCheck.CheckNumTickets(e.Result.Semantics["Cantidad"].Value.ToString());
                string mode = e.Result.Semantics["Modo"].Value.ToString();
                if (mode == "ida" | mode == "")
                    ModeIda.IsChecked = true;
                else
                    ModeIdaVuelta.IsChecked = true;
                RecogText.Text = inputCheck.CheckVoiceText(e.Result.Text);
            }
            
            startRecognition = false;
            RecognizedButton.Content = buttonTextWaiting;

        }

        private void RecognizedButton_Click(object sender, RoutedEventArgs e)
        {
            startRecognition = true;
            RecognizedButton.Content = buttonTextListening;
        }
    }
}
