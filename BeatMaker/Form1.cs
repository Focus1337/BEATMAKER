using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            Bass_Interval.TextChanged += bassTextBoxInterval_TextChanged;
            openFileDialogBass.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogHiHats.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogClaps.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogDrumFills.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogFx.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogKicks.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogLoops.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogPercs.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogSnares.Filter = "WAV files(*.wav)|*.wav";
            openFileDialogVocal.Filter = "WAV files(*.wav)|*.wav";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.StyleManager = metroStyleManager1;
            groupBox6.Enabled = false;
            groupBox7.Enabled = false;
            groupBox20.Enabled = false;
            groupBox15.Enabled = false;
            groupBox5.Enabled = false;
            groupBox8.Enabled = false;
            groupBox19.Enabled = false;
            groupBox12.Enabled = false;
            groupBox11.Enabled = false;
            groupBox16.Enabled = false;
        }

        private async void ButtonPlay_Click(object sender, EventArgs e)
        {
            /* Bass bass = new Bass();
             Claps claps = new Claps();
             DrumFills drumFills = new DrumFills();
             Fx fx = new Fx();
             HiHats hiHats = new HiHats();
             Kicks kicks = new Kicks();
             Loops loops = new Loops();
             Percs percs = new Percs();
             Snares snares = new Snares();
             Vocal vocal = new Vocal();
             bass.TryPlay(); claps.TryPlay(); drumFills.TryPlay();
             fx.TryPlay(); hiHats.TryPlay(); kicks.TryPlay(); loops.TryPlay();
             percs.TryPlay(); snares.TryPlay(); vocal.TryPlay();*/
            Bass.TryPlay(); Claps.TryPlay(); DrumFills.TryPlay();
            Fx.TryPlay(); HiHats.TryPlay(); Kicks.TryPlay(); Loops.TryPlay();
            Percs.TryPlay(); Snares.TryPlay(); Vocal.TryPlay();

            #region Bass
            if (Static.BassList.Count > 0 && media_BassList.Count > 0)
            {
                for (int i = 0; i < media_BassList.Count; i++)
                {
                    if (!Static.BassList[i].IsDeleted)
                    {
                        if (int.TryParse(Bass_Interval.Text, out int bass_delay) && bass_delay > 0)
                            await Task.Delay(bass_delay);
                        media_BassList[i].Play();
                    }
                }
            }
            #endregion

            #region Hi Hats
            if (Static.HiHatsList.Count > 0 && media_HiHatsList.Count > 0)
            {
                for (int i = 0; i < media_HiHatsList.Count; i++)
                {
                    if (!Static.HiHatsList[i].IsDeleted)
                    {
                        if (int.TryParse(HiHats_Interval.Text, out int hihats_delay) && hihats_delay > 0)
                            await Task.Delay(hihats_delay);
                        media_HiHatsList[i].Play();
                    }
                }
            }
            #endregion

            #region Loops
            if (Static.LoopsList.Count > 0 && media_LoopsList.Count > 0)
            {
                for (int i = 0; i < media_LoopsList.Count; i++)
                {
                    if (!Static.LoopsList[i].IsDeleted)
                    {
                        if (int.TryParse(Loops_Interval.Text, out int loops_delay) && loops_delay > 0)
                            await Task.Delay(loops_delay);
                        media_LoopsList[i].Play();
                    }
                }
            }
            #endregion

            #region Drum Fills
            if (Static.DrumFillsList.Count > 0 && media_DrumFillsList.Count > 0)
            {
                for (int i = 0; i < media_DrumFillsList.Count; i++)
                {
                    if (!Static.DrumFillsList[i].IsDeleted)
                    {
                        if (int.TryParse(DrumFills_Interval.Text, out int drumfills_delay) && drumfills_delay > 0)
                            await Task.Delay(drumfills_delay);
                        media_DrumFillsList[i].Play();
                    }
                }
            }
            #endregion

            #region Claps
            if (Static.ClapsList.Count > 0 && media_ClapsList.Count > 0)
            {
                for (int i = 0; i < media_ClapsList.Count; i++)
                {
                    if (!Static.ClapsList[i].IsDeleted)
                    {
                        if (int.TryParse(Claps_Interval.Text, out int claps_delay) && claps_delay > 0)
                            await Task.Delay(claps_delay);
                        media_ClapsList[i].Play();
                    }
                }
            }
            #endregion

            #region Kicks
            if (Static.KicksList.Count > 0 && media_KicksList.Count > 0)
            {
                for (int i = 0; i < media_KicksList.Count; i++)
                {
                    if (!Static.KicksList[i].IsDeleted)
                    {
                        if (int.TryParse(Kicks_Interval.Text, out int kicks_delay) && kicks_delay > 0)
                            await Task.Delay(kicks_delay);
                        media_KicksList[i].Play();
                    }
                }
            }
            #endregion

            #region Snares
            if (Static.SnaresList.Count > 0 && media_SnaresList.Count > 0)
            {
                for (int i = 0; i < media_SnaresList.Count; i++)
                {
                    if (!Static.SnaresList[i].IsDeleted)
                    {
                        if (int.TryParse(Snares_Interval.Text, out int snares_delay) && snares_delay > 0)
                            await Task.Delay(snares_delay);
                        media_SnaresList[i].Play();
                    }
                }
            }
            #endregion

            #region Percs
            if (Static.PercsList.Count > 0 && media_PercsList.Count > 0)
            {
                for (int i = 0; i < media_PercsList.Count; i++)
                {
                    if (!Static.PercsList[i].IsDeleted)
                    {
                        if (int.TryParse(Percs_Interval.Text, out int percs_delay) && percs_delay > 0)
                            await Task.Delay(percs_delay);
                        media_PercsList[i].Play();
                    }
                }
            }
            #endregion

            #region Vocal
            if (Static.VocalList.Count > 0 && media_VocalList.Count > 0)
            {
                for (int i = 0; i < media_VocalList.Count; i++)
                {
                    if (!Static.VocalList[i].IsDeleted)
                    {
                        if (int.TryParse(Vocal_Interval.Text, out int vocal_delay) && vocal_delay > 0)
                            await Task.Delay(vocal_delay);
                        media_VocalList[i].Play();
                    }
                }
            }
            #endregion

            #region Fx
            if (Static.FxList.Count > 0 && media_FxList.Count > 0)
            {
                for (int i = 0; i < media_FxList.Count; i++)
                {
                    if (!Static.FxList[i].IsDeleted)
                    {
                        if (int.TryParse(Fx_Interval.Text, out int fx_delay) && fx_delay > 0)
                            await Task.Delay(fx_delay);
                        media_FxList[i].Play();
                    }
                }
            }
            #endregion
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            for (int i = media_BassList.Count - 1; i >= 0; i--)
            {
                media_BassList[i].Stop();
                // media_BassList.RemoveAt(i);
            }

            for (int i = media_HiHatsList.Count - 1; i >= 0; i--)
                media_HiHatsList[i].Stop();

            for (int i = media_LoopsList.Count - 1; i >= 0; i--)
                media_LoopsList[i].Stop();

            for (int i = media_DrumFillsList.Count - 1; i >= 0; i--)
                media_DrumFillsList[i].Stop();

            for (int i = media_ClapsList.Count - 1; i >= 0; i--)
                media_ClapsList[i].Stop();

            for (int i = media_KicksList.Count - 1; i >= 0; i--)
                media_KicksList[i].Stop();

            for (int i = media_SnaresList.Count - 1; i >= 0; i--)
                media_SnaresList[i].Stop();

            for (int i = media_PercsList.Count - 1; i >= 0; i--)
                media_PercsList[i].Stop();

            for (int i = media_VocalList.Count - 1; i >= 0; i--)
                media_VocalList[i].Stop();

            for (int i = media_FxList.Count - 1; i >= 0; i--)
                media_FxList[i].Stop();
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        { }
        private void groupBox6_Enter(object sender, EventArgs e)
        { }
        private void metroTabPage1_Click(object sender, EventArgs e)
        { }
        private void metroTabPage2_Click(object sender, EventArgs e)
        { }
        private void metroTabPage3_Click(object sender, EventArgs e)
        { }
    }


    // СЕКРЕТНЫЕ РАЗРАБОТКИ NASA
    /*
    List<System.Windows.Media.MediaPlayer> sounds = new List<System.Windows.Media.MediaPlayer>();
    private void playSound(string name)
    {
        string url = Application.StartupPath + "\\notes\\" + name + ".wav";
        var sound = new System.Windows.Media.MediaPlayer();
        sound.Open(new Uri(url));
        sound.play();
        sounds.Add(sound);
    }

    private void stopSound()
    {
        for (int i = sounds.Count - 1; i >= 0; i--)
        {
            sounds[i].Stop();
            sounds.RemoveAt(i);
        }
    }*/

    //private async void PlayMediaFunction(List<MediaPlayer> mediaList, int listCount, bool deleted, string intervalText, int delay)
    //{
    //    if (listCount > 0 && mediaList.Count > 0)
    //    {
    //        for (int i = 0; i < mediaList.Count; i++)
    //        {
    //            if (!deleted)
    //            {
    //                if (int.TryParse(intervalText, out delay) && delay > 0)
    //                    await Task.Delay(delay);
    //                mediaList[i].Play();
    //            }
    //        }
    //    }
    //}
}