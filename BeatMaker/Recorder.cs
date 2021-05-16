using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using NAudio.Wave;

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        // Create class-level accessible variables to store the audio recorder and capturer instance
        private WaveFileWriter RecordedAudioWriter = null;
        private WasapiLoopbackCapture CaptureInstance = null;

        private void ButtonRecord_Click(object sender, EventArgs e)
        {
            // Define the output wav file of the recorded audio
            string outputFilePath = TextBoxSaveFolder.Text + @"\" + TextBoxFileName.Text + (TextBoxFileName.Text.Contains(".wav") ? "" : ".wav");

            // Redefine the capturer instance with a new instance of the LoopbackCapture class
            this.CaptureInstance = new WasapiLoopbackCapture();

            // Redefine the audio writer instance with the given configuration
            this.RecordedAudioWriter = new WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            // When the capturer receives audio, start writing the buffer into the mentioned file
            this.CaptureInstance.DataAvailable += (s, a) =>
            {
                this.RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            // When the Capturer Stops
            this.CaptureInstance.RecordingStopped += (s, a) =>
            {
                this.RecordedAudioWriter.Dispose();
                this.RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            // Enable "Stop button" and disable "Start Button"
            this.metroButtonRecord.Enabled = false;
            this.metroButtonSave.Enabled = true;

            // Start recording !
            this.CaptureInstance.StartRecording();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Stop recording !
            this.CaptureInstance.StopRecording();

            // Enable "Start button" and disable "Stop Button"
            this.metroButtonRecord.Enabled = true;
            this.metroButtonSave.Enabled = false;
        }

        private void ButtonChooseFolder_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = TextBoxFileName.Text;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Now here's our save folder
                TextBoxSaveFolder.Text = Path.GetDirectoryName(sf.FileName);
                // Do whatever
            }
        }
    }
}