using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeatMaker;
using System.IO;

namespace BeatMakerTests
{
    [TestClass]
    public class BeatMakerTests
    {
        [TestMethod]
        public void Play_WhenFileLocationIsNull_ShouldThrowFileNotFoundException()
        {
            // Idea:
            // Создает в каждом List файл с null расположением файла
            // пробует запустить

            // Arrange
            // Можно использовать и в виде нестатичного поля. Но зачем?..
           /* 
            Bass bass = new Bass();
            Claps claps = new Claps();
            DrumFills drumFills = new DrumFills();
            Fx fx = new Fx();
            HiHats hiHats = new HiHats();
            Kicks kicks = new Kicks();
            Loops loops = new Loops();
            Percs percs = new Percs();
            Snares snares = new Snares();
            Vocal vocal = new Vocal();
           */

            // Act
            Static.BassList.Add(new Bass() { FileLocation = null });
            Static.ClapsList.Add(new Claps() { FileLocation = null });
            Static.DrumFillsList.Add(new DrumFills() { FileLocation = null });
            Static.FxList.Add(new Fx() { FileLocation = null });
            Static.HiHatsList.Add(new HiHats() { FileLocation = null });
            Static.KicksList.Add(new Kicks() { FileLocation = null });
            Static.LoopsList.Add(new Loops() { FileLocation = null });
            Static.PercsList.Add(new Percs() { FileLocation = null });
            Static.SnaresList.Add(new Snares() { FileLocation = null });
            Static.VocalList.Add(new Vocal() { FileLocation = null });

            // Assert
            Assert.ThrowsException<FileNotFoundException>(() => Bass.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Claps.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => DrumFills.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Fx.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => HiHats.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Kicks.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Loops.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Percs.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Snares.TryPlay());
            Assert.ThrowsException<FileNotFoundException>(() => Vocal.TryPlay());
        }

        [TestMethod]
        public void ChangeVolume_WhenNoItemSelected_ShouldThrowFileNotFoundException()
        {
            // Idea:
            // Если никакой элемент в комбобоксе не выбран (index == -1), то кнопки не должны быть видны

            // Act
            Static.BassList.Add(new Bass() { FileLocation = null });
            Static.ClapsList.Add(new Claps() { FileLocation = null });
            Static.DrumFillsList.Add(new DrumFills() { FileLocation = null });
            Static.FxList.Add(new Fx() { FileLocation = null });
            Static.HiHatsList.Add(new HiHats() { FileLocation = null });
            Static.KicksList.Add(new Kicks() { FileLocation = null });
            Static.LoopsList.Add(new Loops() { FileLocation = null });
            Static.PercsList.Add(new Percs() { FileLocation = null });
            Static.SnaresList.Add(new Snares() { FileLocation = null });
            Static.VocalList.Add(new Vocal() { FileLocation = null });

            // Assert
            Assert.ThrowsException<FileNotFoundException>(() => Bass.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Claps.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => DrumFills.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Fx.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => HiHats.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Kicks.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Loops.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Percs.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Snares.TryChangeVolumeOrSpeed("громкость"));
            Assert.ThrowsException<FileNotFoundException>(() => Vocal.TryChangeVolumeOrSpeed("громкость"));
        }

        [TestMethod]
        public void ChangeSpeed_WhenNoItemSelected_ShouldThrowFileNotFoundException()
        {
            // Idea:
            // Если никакой элемент в комбобоксе не выбран (index == -1), то кнопки не должны быть видны

            // Act
            Static.BassList.Add(new Bass() { FileLocation = null });
            Static.ClapsList.Add(new Claps() { FileLocation = null });
            Static.DrumFillsList.Add(new DrumFills() { FileLocation = null });
            Static.FxList.Add(new Fx() { FileLocation = null });
            Static.HiHatsList.Add(new HiHats() { FileLocation = null });
            Static.KicksList.Add(new Kicks() { FileLocation = null });
            Static.LoopsList.Add(new Loops() { FileLocation = null });
            Static.PercsList.Add(new Percs() { FileLocation = null });
            Static.SnaresList.Add(new Snares() { FileLocation = null });
            Static.VocalList.Add(new Vocal() { FileLocation = null });

            // Assert
            Assert.ThrowsException<FileNotFoundException>(() => Bass.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Claps.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => DrumFills.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Fx.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => HiHats.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Kicks.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Loops.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Percs.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Snares.TryChangeVolumeOrSpeed("скорость"));
            Assert.ThrowsException<FileNotFoundException>(() => Vocal.TryChangeVolumeOrSpeed("скорость"));
        }

        [TestMethod]
        public void Delete_WhenThereIsNoItem_ShouldThrowFileNotFoundException()
        {
            // Assert
            Assert.ThrowsException<FileNotFoundException>(() => Bass.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Claps.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => DrumFills.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Fx.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => HiHats.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Kicks.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Loops.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Percs.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Snares.TryDelete());
            Assert.ThrowsException<FileNotFoundException>(() => Vocal.TryDelete());
        }

        [TestMethod]
        public void OpenMediaPlayer_WhenFileLocationIsNull_ShouldThrowFileNotFoundException()
        {
            // Idea:
            // Добавляет в каждый список элемент с расположением и именем NULL
            // Далее пробует открыть этот файл в MediaPlayer

            // Act
            Static.BassList.Add(new Bass() { FileLocation = null, FileName = null });
            Static.ClapsList.Add(new Claps() { FileLocation = null, FileName = null });
            Static.DrumFillsList.Add(new DrumFills() { FileLocation = null, FileName = null });
            Static.FxList.Add(new Fx() { FileLocation = null, FileName = null });
            Static.HiHatsList.Add(new HiHats() { FileLocation = null, FileName = null });
            Static.KicksList.Add(new Kicks() { FileLocation = null, FileName = null });
            Static.LoopsList.Add(new Loops() { FileLocation = null, FileName = null });
            Static.PercsList.Add(new Percs() { FileLocation = null, FileName = null });
            Static.SnaresList.Add(new Snares() { FileLocation = null, FileName = null });
            Static.VocalList.Add(new Vocal() { FileLocation = null, FileName = null });

            // Assert
            Assert.ThrowsException<FileNotFoundException>(() => Bass.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Claps.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => DrumFills.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Fx.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => HiHats.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Kicks.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Loops.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Percs.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Snares.TryOpen(0));
            Assert.ThrowsException<FileNotFoundException>(() => Vocal.TryOpen(0));
        }
    }
}
