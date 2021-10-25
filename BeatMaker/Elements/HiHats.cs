using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class HiHats : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        // Причём он не был удалён пользователем
        if (BeatMaker.Static.HiHatsList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.HiHatsList.Count; i++)
                if (!BeatMaker.Static.HiHatsList[i].IsDeleted && BeatMaker.Static.HiHatsList[i].FileLocation == null)
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
        if (Static.HiHatsList[index].FileLocation == null || Static.HiHatsList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== HiHats ====================
        private void buttonHiHats1_Click(object sender, EventArgs e)
        {
            if (openFileDialogHiHats.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogHiHats.FileName;
            Static.HiHatsList.Add(new HiHats() { Id = Static.HiHatsCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            HiHats_ComboBox1.Items.Add($"[{Static.HiHatsCount}] " + Static.HiHatsList[Static.HiHatsCount].FileName);
            MediaPlayer HiHatsMP = new MediaPlayer();

            HiHats.TryOpen(Static.HiHatsCount);

            HiHatsMP.Open(new Uri(Static.HiHatsList[Static.HiHatsCount].FileLocation));
            media_HiHatsList.Add(HiHatsMP);
            Static.HiHatsCount++;
        }
        private void HiHats_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HiHats_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel9.Text = "EDIT: " + HiHats_ComboBox1.SelectedItem.ToString(); // текст группы эдит
                metroLabel45.Text = (media_HiHatsList[HiHats_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
                metroLabel43.Text = (media_HiHatsList[HiHats_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
            }

            if (HiHats_ComboBox1.SelectedIndex != -1)
                groupBox5.Enabled = true;

            if (Static.HiHatsList[HiHats_ComboBox1.SelectedIndex].IsDeleted)
                groupBox5.Enabled = false;

            HiHats.SelectedIndex = HiHats_ComboBox1.SelectedIndex;
        }
        private void HiHats_Button3_Click(object sender, EventArgs e)
        {
            if (HiHats_ComboBox1.SelectedIndex != -1)
            {
                HiHats.TryDelete();
                Static.HiHatsList[HiHats_ComboBox1.SelectedIndex].FileLocation = null;
                Static.HiHatsList[HiHats_ComboBox1.SelectedIndex].FileName = null;
                Static.HiHatsList[HiHats_ComboBox1.SelectedIndex].Id = -1;
                Static.HiHatsList[HiHats_ComboBox1.SelectedIndex].IsDeleted = true;
                HiHats_ComboBox1.Items[HiHats_ComboBox1.SelectedIndex] = "{DELETED}";
            }
        }
        private void HiHats_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            HiHats.TryChangeVolumeOrSpeed("громкость");
            if (HiHats_ComboBox1.SelectedIndex == -1)
                return;
            media_HiHatsList[HiHats_ComboBox1.SelectedIndex].Volume = HiHats_Volume.Value / 100.0f;
            metroLabel45.Text = (media_HiHatsList[HiHats_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void HiHats_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            HiHats.TryChangeVolumeOrSpeed("скорость");
            if (HiHats_ComboBox1.SelectedIndex == -1)
                return;
            media_HiHatsList[HiHats_ComboBox1.SelectedIndex].SpeedRatio = HiHats_Speed.Value / 100.0f;
            metroLabel43.Text = (media_HiHatsList[HiHats_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}
