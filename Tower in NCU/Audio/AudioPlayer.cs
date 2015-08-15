using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using NAudio.Wave;

namespace Tower_in_NCU.Audio
{
    class AudioPlayer
    {
        private static AudioPlayer _audioPlayer = new AudioPlayer();
        
        private WaveOut _backgroundMusic;
        private DirectSoundOut _soundEffect;
        private WaveFileReader _wfr;

        public enum BackgroundMusic
        {
            Exploration,
        };

        public enum SoundEffect
        {
            Cancel, Select, Confirm, Fail, OpenDoor,
            GetItem, Eliminate, EnemyAttack, PlayerAttack, Move,
        };

        private AudioPlayer() { }

        public static AudioPlayer GetInstance() => _audioPlayer;

        public void Play(BackgroundMusic type)
        {
            _backgroundMusic?.Dispose();
            try
            {
                switch (type)
                {
                    case BackgroundMusic.Exploration:
                        _wfr = new WaveFileReader(Properties.Resources.Exploration);
                        break;
                    default:
                        throw new Exception("Not expected audioType.");
                }
                LoopStream loop = new LoopStream(_wfr);
                _backgroundMusic = new WaveOut();
                _backgroundMusic.Init(loop);
                _backgroundMusic.Volume = 0.5F;
                _backgroundMusic.Play();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Play(SoundEffect type, float volume = 1.0F)
        {
            //_soundEffect?.Dispose();
            switch (type)
            {
                case SoundEffect.Move:
                    _wfr = new WaveFileReader(Properties.Resources.Move);
                    break;
                case SoundEffect.Confirm:
                    _wfr = new WaveFileReader(Properties.Resources.Confirm);
                    break;
                case SoundEffect.Cancel:
                    _wfr = new WaveFileReader(Properties.Resources.Cance);
                    break;
                case SoundEffect.Eliminate:
                    _wfr = new WaveFileReader(Properties.Resources.Eliminate);
                    break;
                case SoundEffect.EnemyAttack:
                    _wfr = new WaveFileReader(Properties.Resources.EnemyAttack);
                    break;
                case SoundEffect.Fail:
                    _wfr = new WaveFileReader(Properties.Resources.Fail);
                    break;
                case SoundEffect.GetItem:
                    _wfr = new WaveFileReader(Properties.Resources.GetItem);
                    break;
                case SoundEffect.OpenDoor:
                    _wfr = new WaveFileReader(Properties.Resources.OpenDoor);
                    break;
                case SoundEffect.PlayerAttack:
                    _wfr = new WaveFileReader(Properties.Resources.PlayerAttack);
                    break;
                case SoundEffect.Select:
                    _wfr = new WaveFileReader(Properties.Resources.Select);
                    break;
                default:
                    throw new Exception("Not expected audioType.");
            }
            WaveChannel32 wc = new WaveChannel32(_wfr) { PadWithZeroes = false };
            _soundEffect = new DirectSoundOut();
            _soundEffect.Init(wc);
            _soundEffect.Volume = volume;
            _soundEffect.Play();
        }

        public void Test()
        {
            var wfr = new WaveFileReader(Properties.Resources.Move);
            WaveChannel32 wc = new WaveChannel32(wfr) { PadWithZeroes = false };
            using (var audioOuput = new DirectSoundOut())
            {
                audioOuput.Init(wc);
                audioOuput.Play();
            }
        }

    }

    /// <summary>
    /// Stream for looping playback
    /// </summary>
    public class LoopStream : WaveStream
    {
        WaveStream sourceStream;

        /// <summary>
        /// Creates a new Loop stream
        /// </summary>
        /// <param name="sourceStream">The stream to read from. Note: the Read method of this stream should return 0 when it reaches the end
        /// or else we will not loop to the start again.</param>
        public LoopStream(WaveStream sourceStream)
        {
            this.sourceStream = sourceStream;
            this.EnableLooping = true;
        }

        /// <summary>
        /// Use this to turn looping on or off
        /// </summary>
        public bool EnableLooping { get; set; }

        /// <summary>
        /// Return source stream's wave format
        /// </summary>
        public override WaveFormat WaveFormat
        {
            get { return sourceStream.WaveFormat; }
        }

        /// <summary>
        /// LoopStream simply returns
        /// </summary>
        public override long Length
        {
            get { return sourceStream.Length; }
        }

        /// <summary>
        /// LoopStream simply passes on positioning to source stream
        /// </summary>
        public override long Position
        {
            get { return sourceStream.Position; }
            set { sourceStream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping)
                    {
                        // something wrong with the source stream
                        break;
                    }
                    // loop
                    sourceStream.Position = 0;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }

}
