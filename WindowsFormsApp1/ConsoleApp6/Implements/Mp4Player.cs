using ConsoleApp6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6.Implements
{
    public class Mp4Player : AdvancedMediaPlayer
    {
        public void PlayMp4(string fileName)
        {
            Console.WriteLine("Playing mp4 file. Name: " + fileName);
        }

        public void PlayVlc(string fileName)
        {
           
        }
    }
}
