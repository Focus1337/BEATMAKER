using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Loops : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        // Причём он не был удалён пользователем
        if (BeatMaker.Static.LoopsList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.LoopsList.Count; i++)
                if (!BeatMaker.Static.LoopsList[i].IsDeleted && BeatMaker.Static.LoopsList[i].FileLocation == null)
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
        if (Static.LoopsList[index].FileLocation == null || Static.LoopsList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Loops ====================
        private void Loops_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogLoops.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogLoops.FileName;
            Static.LoopsList.Add(new Loops() { Id = Static.LoopsCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Loops_ComboBox1.Items.Add($"[{Static.LoopsCount}] " + Static.LoopsList[Static.LoopsCount].FileName);
            MediaPlayer LoopsMP = new MediaPlayer();

            Loops.TryOpen(Static.LoopsCount);


            LoopsMP.Open(new Uri(Static.LoopsList[Static.LoopsCount].FileLocation));
            media_LoopsList.Add(LoopsMP);
            Static.LoopsCount++;
        }
        private void Loops_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Loops_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel29.Text = "EDIT: " + Loops_ComboBox1.SelectedItem.ToString(); // текст группы эдит
                metroLabel88.Text = (media_LoopsList[Loops_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
                metroLabel87.Text = (media_LoopsList[Loops_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
            }

            if (Loops_ComboBox1.SelectedIndex != -1)
                groupBox19.Enabled = true;

            if (Static.LoopsList[Loops_ComboBox1.SelectedIndex].IsDeleted)
                groupBox19.Enabled = false;

            Loops.SelectedIndex = Loops_ComboBox1.SelectedIndex;
        }
        private void Loops_Button3_Click(object sender, EventArgs e)
        {
            if (Loops_ComboBox1.SelectedIndex != -1)
            {
                Loops.TryDelete();
                Static.LoopsList[Loops_ComboBox1.SelectedIndex].FileLocation = null;
                Static.LoopsList[Loops_ComboBox1.SelectedIndex].FileName = null;
                Static.LoopsList[Loops_ComboBox1.SelectedIndex].Id = -1;
                Static.LoopsList[Loops_ComboBox1.SelectedIndex].IsDeleted = true;
                Loops_ComboBox1.Items[Loops_ComboBox1.SelectedIndex] = "{DELETED}";
            }
        }
        private void Loops_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Loops.TryChangeVolumeOrSpeed("громкость");
            media_LoopsList[Loops_ComboBox1.SelectedIndex].Volume = Loops_Volume.Value / 100.0f;
            metroLabel88.Text = (media_LoopsList[Loops_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Loops_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Loops.TryChangeVolumeOrSpeed("скорость");
            media_LoopsList[Loops_ComboBox1.SelectedIndex].SpeedRatio = Loops_Speed.Value / 100.0f;
            metroLabel87.Text = (media_LoopsList[Loops_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}