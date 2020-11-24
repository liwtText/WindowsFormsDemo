using ConsoleApp6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6.Implements
{
    public class MediaAdapter : MediaPlayer
    {
        AdvancedMediaPlayer advancedMusicPlayer;

        public MediaAdapter(String audioType)
        {
            if (audioType.Equals("vlc"))
            {
                advancedMusicPlayer = new VlcPlayer();
            }
            else if (audioType.Equals("mp4"))
            {
                advancedMusicPlayer = new Mp4Player();
            }
        }

        public void Play(String audioType, String fileName)
        {
            if (audioType.Equals("vlc"))
            {
                advancedMusicPlayer.PlayVlc(fileName);
            }
            else if (audioType.Equals("mp4"))
            {
                advancedMusicPlayer.PlayMp4(fileName);
            }
        }

    }
}
