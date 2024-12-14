﻿using DPFP;
using DPFP.Capture;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricsProject
{
    public partial class capture : Form, DPFP.Capture.EventHandler
    {

        private DPFP.Capture.Capture Capturer;
        public string FirstName = "";
        public string UserID = "";

        public capture()
        {
            InitializeComponent();
        }

        protected void SetPrompt(string prompt)
        {
            this.Invoke(new Function(delegate ()
            {
                Prompt.Text = prompt;
            }));
        }

        /*
        protected void SetStatus(string status)
        {
            this.Invoke(new Function(delegate ()
            {
                StatusLabel.Text = status;

            }));
        }
        */

        protected void SetStatus(string status)
        {
            if (StatusLabel.InvokeRequired)
            {
                if (StatusLabel.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        StatusLabel.Text = status;
                    }));
                }
            }
            else
            {
                StatusLabel.Text = status;
            }
        }


        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate ()
            {
                fImage.Image = new Bitmap(bitmap, fImage.Size);
            }));
        }


        protected void Setfname(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                fname.Text = value;
            }));
        }

        protected void SetUserID(string value)
        {
            this.Invoke(new Function(delegate ()
            {
                userid.Text = value;
            }));
        }


        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();
                if (null != Capturer) 
                { 
                    Capturer.EventHandler = this;
                } 
                else
                {
                    SetPrompt("Can't Initiate Capture Operation");
                }
            }catch{
                MessageBox.Show("Can't Initiate Capture Operation");
            }
        }


        //Process

        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
        }

        

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();
            Bitmap bitmap = null;
            Convertor.ConvertToPicture(Sample, ref bitmap);
            return bitmap;
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    SetPrompt("Using the fingerprint reader, scan your fingerprint.");
                }
                catch
                {
                    SetPrompt("can't initiate capture.");
                }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                    timer1.Dispose();
                    SetPrompt("Capture Terminated.");
                }
                catch
                {
                    SetPrompt("can't terminate capture.");
                }
            }
        }



        protected void MakeReport(string message)
        {
            this.Invoke(new Function(delegate ()
            {
                StatusText.AppendText(message + "\r\n ");
            }));

        }


        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {

            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();
            DPFP.Capture.CaptureFeedback feedback =  DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }






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

        /*
        private void start_scan_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }
        */

        private bool isScanning = false; // Flag to track scan state

        private void start_scan_Click(object sender, EventArgs e)
        {
            if (isScanning)
            {
                // Stop the scan
                timer1.Stop(); // Stop the timer
                Stop(); // Call the Stop method for fingerprint processing
                start_scan.Text = "Start Scan"; // Update button text
                isScanning = false; // Update the state
            }
            else
            {
                // Start the scan
                timer1.Interval = 1000; // Set the timer interval
                timer1.Start(); // Start the timer
                Start(); // Call the Start method for fingerprint processing
                start_scan.Text = "Stop Scan"; // Update button text
                isScanning = true; // Update the state
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

        }
    }
}
