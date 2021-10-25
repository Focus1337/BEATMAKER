using System;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Forms;
using NAudio.Wave;

namespace BeatMaker
{
    public partial class Form1 : MetroForm
    {
        // Create class-level accessible variables to store the audio recorder and capturer instance
        private WaveFileWriter RecordedAudioWriter;
        private WasapiLoopbackCapture CaptureInstance;

        private void ButtonRecord_Click(object sender, EventArgs e)
        {
            // Define the output wav file of the recorded audio
            string outputFilePath = TextBoxSaveFolder.Text + @"\" + TextBoxFileName.Text + (TextBoxFileName.Text.Contains(".wav") ? "" : ".wav");

            // Redefine the capturer instance with a new instance of the LoopbackCapture class
            CaptureInstance = new WasapiLoopbackCapture();

            // Redefine the audio writer instance with the given configuration
            RecordedAudioWriter = new WaveFileWriter(outputFilePath, CaptureInstance.WaveFormat);

            // When the capturer receives audio, start writing the buffer into the mentioned file
            CaptureInstance.DataAvailable += (s, a) =>
            {
                RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
            };

            // When the Capturer Stops
            CaptureInstance.RecordingStopped += (s, a) =>
            {
                RecordedAudioWriter.Dispose();
                RecordedAudioWriter = null;
                CaptureInstance.Dispose();
            };

            // Enable "Stop button" and disable "Start Button"
            metroButtonRecord.Enabled = false;
            metroButtonSave.Enabled = true;

            // Start recording !
            CaptureInstance.StartRecording();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Stop recording !
            CaptureInstance.StopRecording();

            // Enable "Start button" and disable "Stop Button"
            metroButtonRecord.Enabled = true;
            metroButtonSave.Enabled = false;
        }

        private void ButtonChooseFolder_Click(object sender, EventArgs e)
        {
            var sf = new SaveFileDialog();
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