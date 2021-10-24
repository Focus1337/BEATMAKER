using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace BeatMaker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        //============ инициализация всех статических полей ============
        List<MediaPlayer> media_BassList = new List<MediaPlayer>();
        List<MediaPlayer> media_HiHatsList = new List<MediaPlayer>();
        List<MediaPlayer> media_LoopsList = new List<MediaPlayer>();
        List<MediaPlayer> media_DrumFillsList = new List<MediaPlayer>();
        List<MediaPlayer> media_ClapsList = new List<MediaPlayer>();
        List<MediaPlayer> media_KicksList = new List<MediaPlayer>();
        List<MediaPlayer> media_SnaresList = new List<MediaPlayer>();
        List<MediaPlayer> media_PercsList = new List<MediaPlayer>();
        List<MediaPlayer> media_VocalList = new List<MediaPlayer>();
        List<MediaPlayer> media_FxList = new List<MediaPlayer>();
    }

    public class Static
    {
        //============ инициализация всех статических полей ============
        public static List<Bass> BassList = new List<Bass>();
        public static List<HiHats> HiHatsList = new List<HiHats>();
        public static List<Loops> LoopsList = new List<Loops>();
        public static List<DrumFills> DrumFillsList = new List<DrumFills>();
        public static List<Claps> ClapsList = new List<Claps>();
        public static List<Kicks> KicksList = new List<Kicks>();
        public static List<Snares> SnaresList = new List<Snares>();
        public static List<Percs> PercsList = new List<Percs>();
        public static List<Vocal> VocalList = new List<Vocal>();
        public static List<Fx> FxList = new List<Fx>();
        public static int BassCount = 0, HiHatsCount = 0, LoopsCount = 0, DrumFillsCount = 0;
        public static int ClapsCount = 0, KicksCount = 0, SnaresCount = 0, PercsCount = 0;
        public static int VocalCount = 0, FxCount = 0;
    }
}

//============ родительский класс для классов с элементами музыки ============
public class FileInfo
{
    public int Id { get; set; } // сохраняет id элемента, не знаю зачем :D
    public string FileName { get; set; } // сохраняет имя файла
    public string FileLocation { get; set; } // сохраняет локацию файла
    public bool IsDeleted { get; set; } // проверка, удалён ли файл юзером
}