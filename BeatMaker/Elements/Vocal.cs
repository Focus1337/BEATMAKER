using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Vocal : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        // Причём он не был удалён пользователем
        if (BeatMaker.Static.VocalList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.VocalList.Count; i++)
                if (!BeatMaker.Static.VocalList[i].IsDeleted && BeatMaker.Static.VocalList[i].FileLocation == null)
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
        if (Static.VocalList[index].FileLocation == null || Static.VocalList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Vocal ====================
        private void Vocal_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogVocal.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogVocal.FileName;
            Static.VocalList.Add(new Vocal() { Id = Static.VocalCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Vocal_ComboBox1.Items.Add($"[{Static.VocalCount}] " + Static.VocalList[Static.VocalCount].FileName);
            MediaPlayer VocalMP = new MediaPlayer();

            Vocal.TryOpen(Static.VocalCount);

            VocalMP.Open(new Uri(Static.VocalList[Static.VocalCount].FileLocation));
            media_VocalList.Add(VocalMP);
            Static.VocalCount++;
        }
        private void Vocal_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Vocal_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel24.Text = "EDIT: " + Vocal_ComboBox1.SelectedItem.ToString(); // текст группы эдит
            }

            if (Vocal_ComboBox1.SelectedIndex != -1)
                groupBox16.Enabled = true;

            if (Static.VocalList[Vocal_ComboBox1.SelectedIndex].IsDeleted)
                groupBox16.Enabled = false;

            Vocal.SelectedIndex = Vocal_ComboBox1.SelectedIndex;
        }
        private void Vocal_Button3_Click(object sender, EventArgs e)
        {
            if (Vocal_ComboBox1.SelectedIndex != -1)
            {
                Vocal.TryDelete();
                Static.VocalList[Vocal_ComboBox1.SelectedIndex].FileLocation = null;
                Static.VocalList[Vocal_ComboBox1.SelectedIndex].FileName = null;
                Static.VocalList[Vocal_ComboBox1.SelectedIndex].Id = -1;
                Static.VocalList[Vocal_ComboBox1.SelectedIndex].IsDeleted = true;
                Vocal_ComboBox1.Items[Vocal_ComboBox1.SelectedIndex] = "{DELETED}";
            }
        }
        private void Vocal_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Vocal.TryChangeVolumeOrSpeed("громкость");
            media_VocalList[Vocal_ComboBox1.SelectedIndex].Volume = Vocal_Volume.Value / 100.0f;
            metroLabel82.Text = (media_VocalList[Vocal_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Vocal_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Vocal.TryChangeVolumeOrSpeed("скорость");
            media_VocalList[Vocal_ComboBox1.SelectedIndex].SpeedRatio = Vocal_Speed.Value / 100.0f;
            metroLabel81.Text = (media_VocalList[Vocal_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}
