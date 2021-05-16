using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class DrumFills : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        if (BeatMaker.Static.DrumFillsList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.DrumFillsList.Count; i++)
                if (!BeatMaker.Static.DrumFillsList[i].IsDeleted && BeatMaker.Static.DrumFillsList[i].FileLocation == null)
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
        if (Static.DrumFillsList[index].FileLocation == null || Static.DrumFillsList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== DrumFills ====================
        private void DrumFills_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogDrumFills.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogDrumFills.FileName;
            Static.DrumFillsList.Add(new DrumFills() { Id = Static.DrumFillsCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            DrumFills_ComboBox1.Items.Add($"[{Static.DrumFillsCount}] " + Static.DrumFillsList[Static.DrumFillsCount].FileName);
            MediaPlayer DrumFillsMP = new MediaPlayer();

            DrumFills.TryOpen(Static.DrumFillsCount);


            DrumFillsMP.Open(new Uri(Static.DrumFillsList[Static.DrumFillsCount].FileLocation));
            media_DrumFillsList.Add(DrumFillsMP);
            Static.DrumFillsCount++;
        }
        private void DrumFills_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DrumFills_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel30.Text = "EDIT: " + DrumFills_ComboBox1.SelectedItem.ToString(); // текст группы эдит
            }

            if (DrumFills_ComboBox1.SelectedIndex != -1)
                groupBox20.Enabled = true;

            if (Static.DrumFillsList[DrumFills_ComboBox1.SelectedIndex].IsDeleted)
                groupBox20.Enabled = false;

            DrumFills.SelectedIndex = DrumFills_ComboBox1.SelectedIndex;
        }
        private void DrumFills_Button3_Click(object sender, EventArgs e)
        {
            if (DrumFills_ComboBox1.SelectedIndex != -1)
            {
                DrumFills.TryDelete();
                Static.DrumFillsList[DrumFills_ComboBox1.SelectedIndex].FileLocation = null;
                Static.DrumFillsList[DrumFills_ComboBox1.SelectedIndex].FileName = null;
                Static.DrumFillsList[DrumFills_ComboBox1.SelectedIndex].Id = -1;
                Static.DrumFillsList[DrumFills_ComboBox1.SelectedIndex].IsDeleted = true;
                DrumFills_ComboBox1.Items[DrumFills_ComboBox1.SelectedIndex] = "{DELETED}";
                // DrumFills_ComboBox1.Items.Remove(DrumFills_ComboBox1.SelectedItem);
            }
        }
        private void DrumFills_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            DrumFills.TryChangeVolumeOrSpeed("громкость");
            media_DrumFillsList[DrumFills_ComboBox1.SelectedIndex].Volume = DrumFills_Volume.Value / 100.0f;
            metroLabel94.Text = (media_DrumFillsList[DrumFills_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void DrumFills_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            DrumFills.TryChangeVolumeOrSpeed("скорость");
            media_DrumFillsList[DrumFills_ComboBox1.SelectedIndex].SpeedRatio = DrumFills_Speed.Value / 100.0f;
            metroLabel93.Text = (media_DrumFillsList[DrumFills_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}
