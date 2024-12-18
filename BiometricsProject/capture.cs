using DPFP;
using DPFP.Capture;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BiometricsProject
{
    public partial class capture : Form, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture Capturer;
        public string FirstName = "";
        public string UserID = "";
        private bool isScanning = false; // Flag to track scan state

        public capture()
        {
            InitializeComponent();
        }

        // Set the text for the prompt label safely
        protected void SetPrompt(string prompt)
        {
            if (Prompt.InvokeRequired)
            {
                Prompt.Invoke(new Action(() => Prompt.Text = prompt));
            }
            else
            {
                Prompt.Text = prompt;
            }
        }

        // Set the text for the status label safely
        protected void SetStatus(string status)
        {
            if (StatusLabel.InvokeRequired)
            {
                if (StatusLabel.IsHandleCreated)
                {
                    StatusLabel.Invoke(new Action(() => StatusLabel.Text = status));
                }
            }
            else
            {
                StatusLabel.Text = status;
            }
        }

        // Draw the fingerprint image safely
        private void DrawPicture(Bitmap bitmap)
        {
            if (fImage.InvokeRequired)
            {
                fImage.Invoke(new Action(() => fImage.Image = new Bitmap(bitmap, fImage.Size)));
            }
            else
            {
                fImage.Image = new Bitmap(bitmap, fImage.Size);
            }
        }

        // Update First Name text box
        protected void Setfname(string value)
        {
            if (fname.InvokeRequired)
            {
                fname.Invoke(new Action(() => fname.Text = value));
            }
            else
            {
                fname.Text = value;
            }
        }

        // Update User ID text box
        protected void SetUserID(string value)
        {
            if (userid.InvokeRequired)
            {
                userid.Invoke(new Action(() => userid.Text = value));
            }
            else
            {
                userid.Text = value;
            }
        }

        // Initialize the fingerprint capture process
        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                if (Capturer != null)
                {
                    Capturer.EventHandler = this;
                }
                else
                {
                    SetPrompt("Can't Initiate Capture Operation");
                }
            }
            catch
            {
                MessageBox.Show("Can't Initiate Capture Operation");
            }
        }

        // Process a sample and draw the fingerprint image
        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
        }

        // Convert fingerprint sample to a Bitmap
        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        // Start fingerprint capture
        protected void Start()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch
                {
                    SetPrompt("Can't initiate capture.");
                }
            }
        }

        // Stop fingerprint capture
        protected void Stop()
        {
            if (Capturer != null)
            {
                try
                {
                    Capturer.StopCapture();
                    timer1.Dispose();
                    SetPrompt("Capture Terminated.");
                }
                catch
                {
                    SetPrompt("Can't terminate capture.");
                }
            }
        }

        // Append a message to the status text box
        protected void MakeReport(string message)
        {
            if (StatusText.InvokeRequired)
            {
                StatusText.Invoke(new Action(() => StatusText.AppendText(message + "\r\n")));
            }
            else
            {
                StatusText.AppendText(message + "\r\n");
            }
        }

        // Extract features from a fingerprint sample
        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();

            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);

            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        // Event Handlers for DPFP.Capture.EventHandler
        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            MakeReport("The Finger was removed from the fingerprint reader.");
            SetPrompt("Scan the same fingerprint again");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The Finger was removed from the fingerprint reader.");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The Fingerprint reader was touched.");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The Fingerprint reader was connected.");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            MakeReport("The Fingerprint reader was disconnected.");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
                MakeReport("The Quality of the fingerprint sample is Good:");
            else
                MakeReport("The Quality of the fingerprint sample is Poor:");
        }

        // Start/Stop scan with button click
        private void start_scan_Click(object sender, EventArgs e)
        {
            if (isScanning)
            {
                timer1.Stop();
                Stop();
                start_scan.Text = "Start Scan";
                isScanning = false;
            }
            else
            {
                timer1.Interval = 1000;
                timer1.Start();
                Start();
                start_scan.Text = "Stop Scan";
                isScanning = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Start();
        }

        private void capture_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void capture_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void fname_TextChanged(object sender, EventArgs e)
        {
            FirstName = fname.Text;
        }

        private void userid_TextChanged(object sender, EventArgs e)
        {
            UserID = userid.Text;
        }

        private void StatusText_TextChanged(object sender, EventArgs e)
        {
            // Leave this empty or add your desired logic here.
        }

    }
}
