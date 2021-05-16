using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Claps : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        if (BeatMaker.Static.ClapsList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.ClapsList.Count; i++)
                if (!BeatMaker.Static.ClapsList[i].IsDeleted && BeatMaker.Static.ClapsList[i].FileLocation == null)
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
        if (Static.ClapsList[index].FileLocation == null || Static.ClapsList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Claps ====================
        private void buttonClaps1_Click(object sender, EventArgs e)
        {
            if (openFileDialogClaps.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogClaps.FileName;
            Static.ClapsList.Add(new Claps() { Id = Static.ClapsCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Claps_ComboBox1.Items.Add($"[{Static.ClapsCount}] " + Static.ClapsList[Static.ClapsCount].FileName);
            MediaPlayer ClapsMP = new MediaPlayer();

            Claps.TryOpen(Static.ClapsCount);


            ClapsMP.Open(new Uri(Static.ClapsList[Static.ClapsCount].FileLocation));
            media_ClapsList.Add(ClapsMP);
            Static.ClapsCount++;
        }
        private void comboBoxClaps1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Claps_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel11.Text = "EDIT: " + Claps_ComboBox1.SelectedItem.ToString(); // текст группы эдит
            }

            if (Claps_ComboBox1.SelectedIndex != -1)
                groupBox7.Enabled = true;

            if (Static.ClapsList[Claps_ComboBox1.SelectedIndex].IsDeleted)
                groupBox7.Enabled = false;

            Claps.SelectedIndex = Claps_ComboBox1.SelectedIndex;
        }
        private void buttonClaps3_Click(object sender, EventArgs e)
        {
            if (Claps_ComboBox1.SelectedIndex != -1)
            {
                Claps.TryDelete();
                Static.ClapsList[Claps_ComboBox1.SelectedIndex].FileLocation = null;
                Static.ClapsList[Claps_ComboBox1.SelectedIndex].FileName = null;
                Static.ClapsList[Claps_ComboBox1.SelectedIndex].Id = -1;
                Static.ClapsList[Claps_ComboBox1.SelectedIndex].IsDeleted = true;
                Claps_ComboBox1.Items[Claps_ComboBox1.SelectedIndex] = "{DELETED}";
               // comboBoxClaps1.Items.Remove(comboBoxClaps1.SelectedItem);
            }
        }
        private void Claps_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Claps.TryChangeVolumeOrSpeed("громкость");
            media_ClapsList[Claps_ComboBox1.SelectedIndex].Volume = Claps_Volume.Value / 100.0f;
            metroLabel58.Text = (media_ClapsList[Claps_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Claps_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Claps.TryChangeVolumeOrSpeed("скорость");
            media_ClapsList[Claps_ComboBox1.SelectedIndex].SpeedRatio = Claps_Speed.Value / 100.0f;
            metroLabel57.Text = (media_ClapsList[Claps_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}
