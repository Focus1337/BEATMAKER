using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Snares : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        // Причём он не был удалён пользователем
        if (BeatMaker.Static.SnaresList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.SnaresList.Count; i++)
                if (!BeatMaker.Static.SnaresList[i].IsDeleted && BeatMaker.Static.SnaresList[i].FileLocation == null)
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
        if (Static.SnaresList[index].FileLocation == null || Static.SnaresList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Snares ====================
        private void Snares_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogSnares.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogSnares.FileName;
            Static.SnaresList.Add(new Snares() { Id = Static.SnaresCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Snares_ComboBox1.Items.Add($"[{Static.SnaresCount}] " + Static.SnaresList[Static.SnaresCount].FileName);
            MediaPlayer SnaresMP = new MediaPlayer();

            Snares.TryOpen(Static.SnaresCount);

            SnaresMP.Open(new Uri(Static.SnaresList[Static.SnaresCount].FileLocation));
            media_SnaresList.Add(SnaresMP);
            Static.SnaresCount++;
        }
        private void Snares_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Snares_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel17.Text = "EDIT: " + Snares_ComboBox1.SelectedItem.ToString(); // текст группы эдит
            }

            if (Snares_ComboBox1.SelectedIndex != -1)
                groupBox11.Enabled = true;

            if (Static.SnaresList[Snares_ComboBox1.SelectedIndex].IsDeleted)
                groupBox11.Enabled = false;

            Snares.SelectedIndex = Snares_ComboBox1.SelectedIndex;
        }
        private void Snares_Button3_Click(object sender, EventArgs e)
        {
            if (Snares_ComboBox1.SelectedIndex != -1)
            {
                Snares.TryDelete();
                Static.SnaresList[Snares_ComboBox1.SelectedIndex].FileLocation = null;
                Static.SnaresList[Snares_ComboBox1.SelectedIndex].FileName = null;
                Static.SnaresList[Snares_ComboBox1.SelectedIndex].Id = -1;
                Static.SnaresList[Snares_ComboBox1.SelectedIndex].IsDeleted = true;
                Snares_ComboBox1.Items[Snares_ComboBox1.SelectedIndex] = "{DELETED}";
            }
        }
        private void Snares_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Snares.TryChangeVolumeOrSpeed("громкость");
            media_SnaresList[Snares_ComboBox1.SelectedIndex].Volume = Snares_Volume.Value / 100.0f;
            metroLabel64.Text = (media_SnaresList[Snares_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Snares_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Snares.TryChangeVolumeOrSpeed("скорость");
            media_SnaresList[Snares_ComboBox1.SelectedIndex].SpeedRatio = Snares_Speed.Value / 100.0f;
            metroLabel63.Text = (media_SnaresList[Snares_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}