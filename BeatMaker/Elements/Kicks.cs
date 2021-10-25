using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Kicks : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        if (BeatMaker.Static.KicksList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.KicksList.Count; i++)
                if (!BeatMaker.Static.KicksList[i].IsDeleted && BeatMaker.Static.KicksList[i].FileLocation == null)
                    throw new FileNotFoundException("Неправильный путь к файлу (путь = NULL)");
    }
    public static void TryChangeVolumeOrSpeed(string scrollName)
    { // Если индекс выбранного элемента = -1 и юзер пытается сменить громкосить/скорость, то отлетит
        if (SelectedIndex == -1)
            throw new FileNotFoundException($"Невозможно сменить {scrollName}, файл не выбран! (путь = NULL)");
    }
    public static void TryDelete()
    { // Если индекс выбранного элемента = -1 и юзер пытается удалить его, то отлетит
        if (SelectedIndex == -1)
            throw new FileNotFoundException("Невозможно удалить пустой файл! (ничего не выбрано)");
    }
    public static void TryOpen(int index)
    {
        // Проверка на то, что каким-то образом в списке появился файл с расположением в NULL
        if (Static.KicksList[index].FileLocation == null || Static.KicksList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Kicks ====================
        private void Kicks_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogKicks.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogKicks.FileName;
            Static.KicksList.Add(new Kicks() { Id = Static.KicksCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Kicks_ComboBox1.Items.Add($"[{Static.KicksCount}] " + Static.KicksList[Static.KicksCount].FileName);
            MediaPlayer KicksMP = new MediaPlayer();

            Kicks.TryOpen(Static.KicksCount);


            KicksMP.Open(new Uri(Static.KicksList[Static.KicksCount].FileLocation));
            media_KicksList.Add(KicksMP);
            Static.KicksCount++;
        }
        private void Kicks_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Kicks_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel12.Text = "EDIT: " + Kicks_ComboBox1.SelectedItem.ToString(); // текст группы эдит
                metroLabel52.Text = (media_KicksList[Kicks_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
                metroLabel51.Text = (media_KicksList[Kicks_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
            }

            if (Kicks_ComboBox1.SelectedIndex != -1)
                groupBox8.Enabled = true;

            if (Static.KicksList[Kicks_ComboBox1.SelectedIndex].IsDeleted)
                groupBox8.Enabled = false;

            Kicks.SelectedIndex = Kicks_ComboBox1.SelectedIndex;
        }
        private void Kicks_Button3_Click(object sender, EventArgs e)
        {
            if (Kicks_ComboBox1.SelectedIndex != -1)
            {
                Kicks.TryDelete();
                Static.KicksList[Kicks_ComboBox1.SelectedIndex].FileLocation = null;
                Static.KicksList[Kicks_ComboBox1.SelectedIndex].FileName = null;
                Static.KicksList[Kicks_ComboBox1.SelectedIndex].Id = -1;
                Static.KicksList[Kicks_ComboBox1.SelectedIndex].IsDeleted = true;
                Kicks_ComboBox1.Items[Kicks_ComboBox1.SelectedIndex] = "{DELETED}";
            }
        }
        private void Kicks_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Kicks.TryChangeVolumeOrSpeed("громкость");
            media_KicksList[Kicks_ComboBox1.SelectedIndex].Volume = Kicks_Volume.Value / 100.0f;
            metroLabel52.Text = (media_KicksList[Kicks_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Kicks_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Kicks.TryChangeVolumeOrSpeed("скорость");
            media_KicksList[Kicks_ComboBox1.SelectedIndex].SpeedRatio = Kicks_Speed.Value / 100.0f;
            metroLabel51.Text = (media_KicksList[Kicks_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}
