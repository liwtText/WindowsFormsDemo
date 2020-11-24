using ConsoleApp6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6.Implements
{
    public class AudioPlayer: MediaPlayer
    {
        MediaAdapter mediaAdapter;

        public void Play(String audioType, String fileName)
        {

            //播放 mp3 音乐文件的内置支持
            if (audioType.Equals("mp3"))
            {
                Console.WriteLine("Playing mp3 file. Name: " + fileName);
            }
            //mediaAdapter 提供了播放其他文件格式的支持
            else if (audioType.Equals("vlc")
               || audioType.Equals("mp4"))
            {
                mediaAdapter = new MediaAdapter(audioType);
                mediaAdapter.Play(audioType, fileName);
            }
            else
            {
                Console.WriteLine("Invalid media. " +
                   audioType + " format not supported");
            }
        }
    }
}
