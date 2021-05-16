using BeatMaker;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;

public class Fx : FileInfo
{
    public static int SelectedIndex = -1; // копирует выбранный индекс комбобокса
    public static void TryPlay()
    {
        // Проверка на попытку запустить файл с расположением в NULL
        // Причём он не был удалён пользователем
        if (BeatMaker.Static.FxList.Count > 0)
            for (int i = 0; i < BeatMaker.Static.FxList.Count; i++)
                if (!BeatMaker.Static.FxList[i].IsDeleted && BeatMaker.Static.FxList[i].FileLocation == null)
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
        if (Static.FxList[index].FileLocation == null || Static.FxList[index].FileName == null)
            throw new FileNotFoundException("Невозможно добавить несуществующий файл (путь = NULL)");
    }
}

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //==================== Fx ====================
        private void Fx_Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialogFx.ShowDialog() == DialogResult.Cancel)
                return;
            string fileLocation = openFileDialogFx.FileName;
            Static.FxList.Add(new Fx() { Id = Static.FxCount, FileName = Path.GetFileNameWithoutExtension(fileLocation), FileLocation = fileLocation });
            Fx_ComboBox1.Items.Add($"[{Static.FxCount}] " + Static.FxList[Static.FxCount].FileName);
            MediaPlayer FxMP = new MediaPlayer();

            Fx.TryOpen(Static.FxCount);

            FxMP.Open(new Uri(Static.FxList[Static.FxCount].FileLocation));
            media_FxList.Add(FxMP);
            Static.FxCount++;
        }
        private void Fx_ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Fx_ComboBox1.SelectedItem.ToString()))
            {
                metroLabel23.Text = "EDIT: " + Fx_ComboBox1.SelectedItem.ToString(); // текст группы эдит
            }

            if (Fx_ComboBox1.SelectedIndex != -1)
                groupBox15.Enabled = true;

            if (Static.FxList[Fx_ComboBox1.SelectedIndex].IsDeleted)
                groupBox15.Enabled = false;

            Fx.SelectedIndex = Fx_ComboBox1.SelectedIndex;
        }
        private void Fx_Button3_Click(object sender, EventArgs e)
        {
            if (Fx_ComboBox1.SelectedIndex != -1)
            {
                Fx.TryDelete();
                Static.FxList[Fx_ComboBox1.SelectedIndex].FileLocation = null;
                Static.FxList[Fx_ComboBox1.SelectedIndex].FileName = null;
                Static.FxList[Fx_ComboBox1.SelectedIndex].Id = -1;
                Static.FxList[Fx_ComboBox1.SelectedIndex].IsDeleted = true;
                Fx_ComboBox1.Items[Fx_ComboBox1.SelectedIndex] = "{DELETED}";
                //Fx_CombBox1.Items.Remove(Fx_CombBox1.SelectedItem);
            }
        }
        private void Fx_Volume_Scroll(object sender, ScrollEventArgs e)
        {
            Fx.TryChangeVolumeOrSpeed("громкость");
            media_FxList[Fx_ComboBox1.SelectedIndex].Volume = Fx_Volume.Value / 100.0f;
            metroLabel76.Text = (media_FxList[Fx_ComboBox1.SelectedIndex].Volume * 100).ToString() + "%";
        }
        private void Fx_Speed_Scroll(object sender, ScrollEventArgs e)
        {
            Fx.TryChangeVolumeOrSpeed("скорость");
            media_FxList[Fx_ComboBox1.SelectedIndex].SpeedRatio = Fx_Speed.Value / 100.0f;
            metroLabel75.Text = (media_FxList[Fx_ComboBox1.SelectedIndex].SpeedRatio * 100).ToString() + "%";
        }
    }
}