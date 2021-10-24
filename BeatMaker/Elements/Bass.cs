using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Bass : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        if (Static.BassList.Count > 0)
            for (int i = 0; i < Static.BassList.Count; i++)
                if (!Static.BassList[i].IsDeleted && Static.BassList[i].FileLocation == null)
                    throw new FileNotFoundException("Неправильный путь к файлу (путь = NULL)");
    }

    public static void TryChangeVolumeOrSpeed(string scrollName)
    { // Если индекс выбранного элемента = -1 или расположение файла NULL
      // и юзер пытается сменить громкосить/скорость, то отлетит
        if (SelectedIndex == -1 || Static.BassList[SelectedIndex].FileLocation == null)
            throw new FileNotFoundException($"Невозможно сменить {scrollName}, файл не выбран/удален! (путь = NULL)");
    }
    public static void TryDelete()
    { // Если индекс выбранного элемента = -1 и юзер пытается удалить его, то отлетит
        if (SelectedIndex == -1)
            throw new FileNotFoundException("Невозможно удалить пустой файл! (ничего не выбрано)");
    }
    public static void TryOpen(int index)
    {
        // Проверка на то, что каким-то образом в списке появился файл с расположением в NULL
        if (Static.BassList[index].FileLocation == null || Static.BassList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //====================BASS ====================
        private void Bass_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogBass.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string fileLocation = openFileDialogBass.FileName;

            Static.BassList.Add(new Bass() { Id = Static.BassCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Bass_ComboBox1.Items.Add($"[{Static.BassCount}] " + Static.BassList[Static.BassCount].FileName);
            
            MediaPlayer BassMP = new MediaPlayer();

            Bass.TryOpen(Static.BassCount);

            BassMP.Open(new Uri(Static.BassList[Static.BassCount].FileLocation));
            media_BassList.Add(BassMP);
            Static.BassCount++;
        }

        private void Bass_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Bass_ComboBox1.SelectedItem.ToString()))
            {       
                metroLabel10.Text = "EDIT: " + Bass_ComboBox1.SelectedItem.ToString(); // текст группы эдит             
                metroLabel32.Text = (media_BassList[Bass_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
                metroLabel34.Text = (media_BassList[Bass_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
            }

            if (Bass_ComboBox1.SelectedIndex != -1)
                groupBox6.Enabled = true;

            if (Static.BassList[Bass_ComboBox1.SelectedIndex].IsDeleted)
                groupBox6.Enabled = false;

            Bass.SelectedIndex = Bass_ComboBox1.SelectedIndex;
        }

        private void Bass_Button3_Click(object sender, EventArgs e)
        {
            if (Bass_ComboBox1.SelectedIndex != -1)
            {
                Bass.TryDelete();
                Static.BassList[Bass_ComboBox1.SelectedIndex].FileLocation = null;
                Static.BassList[Bass_ComboBox1.SelectedIndex].FileName = null;
                Static.BassList[Bass_ComboBox1.SelectedIndex].Id = -1;
                Static.BassList[Bass_ComboBox1.SelectedIndex].IsDeleted = true;
                Bass_ComboBox1.Items[Bass_ComboBox1.SelectedIndex] = "{DELETED}";
            }

        }

        private void Bass_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Bass.TryChangeVolumeOrSpeed("громкость");
            media_BassList[Bass_ComboBox1.SelectedIndex].Volume = Bass_Volume.Value / 100.0f;
            metroLabel32.Text = (media_BassList[Bass_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }

        private void Bass_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Bass.TryChangeVolumeOrSpeed("скорость");
            media_BassList[Bass_ComboBox1.SelectedIndex].SpeedRatio = Bass_Speed.Value / 100.0f;
            metroLabel34.Text = (media_BassList[Bass_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }

        private void bassTextBoxInterval_TextChanged(object sender, EventArgs e)
        { } 
    }
}