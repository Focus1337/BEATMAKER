using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Percs : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        // Причём он не был удалён пользователем
        if (BeatMaker.Static.PercsList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.PercsList.Count; i++)
                if (!BeatMaker.Static.PercsList[i].IsDeleted && BeatMaker.Static.PercsList[i].FileLocation == null)
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
        if (Static.PercsList[index].FileLocation == null || Static.PercsList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Percs ====================
        private void Percs_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogPercs.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogPercs.FileName;
            Static.PercsList.Add(new Percs() { Id = Static.PercsCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Percs_ComboBox1.Items.Add($"[{Static.PercsCount}] " + Static.PercsList[Static.PercsCount].FileName);
            MediaPlayer PercsMP = new MediaPlayer();

            Percs.TryOpen(Static.PercsCount);

            PercsMP.Open(new Uri(Static.PercsList[Static.PercsCount].FileLocation));
            media_PercsList.Add(PercsMP);
            Static.PercsCount++;
        }
        private void Percs_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Percs_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel18.Text = "EDIT: " + Percs_ComboBox1.SelectedItem.ToString(); // текст группы эдит
                metroLabel70.Text = (media_PercsList[Percs_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
                metroLabel69.Text = (media_PercsList[Percs_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
            }

            if (Percs_ComboBox1.SelectedIndex != -1)
                groupBox12.Enabled = true;

            if (Static.PercsList[Percs_ComboBox1.SelectedIndex].IsDeleted)
                groupBox12.Enabled = false;

            Percs.SelectedIndex = Percs_ComboBox1.SelectedIndex;
        }
        private void Percs_Button3_Click(object sender, EventArgs e)
        {
            if (Percs_ComboBox1.SelectedIndex != -1)
            {
                Percs.TryDelete();
                Static.PercsList[Percs_ComboBox1.SelectedIndex].FileLocation = null;
                Static.PercsList[Percs_ComboBox1.SelectedIndex].FileName = null;
                Static.PercsList[Percs_ComboBox1.SelectedIndex].Id = -1;
                Static.PercsList[Percs_ComboBox1.SelectedIndex].IsDeleted = true;
                Percs_ComboBox1.Items[Percs_ComboBox1.SelectedIndex] = "{DELETED}";
            }
        }
        private void Percs_Interval_Click(object sender, EventArgs e)
        { }

        private void Percs_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Percs.TryChangeVolumeOrSpeed("громкость");
            media_PercsList[Percs_ComboBox1.SelectedIndex].Volume = Percs_Volume.Value / 100.0f;
            metroLabel70.Text = (media_PercsList[Percs_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Percs_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Percs.TryChangeVolumeOrSpeed("скорость");
            media_PercsList[Percs_ComboBox1.SelectedIndex].SpeedRatio = Percs_Speed.Value / 100.0f;
            metroLabel69.Text = (media_PercsList[Percs_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}
