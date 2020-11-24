using ConsoleApp6.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6.Implements
{
    public class VlcPlayer : AdvancedMediaPlayer
    {
        public void PlayMp4(string fileName)
        {
       
        }

        public void PlayVlc(string fileName)
        {
            Console.WriteLine("Playing vlc file. Name: " + fileName);
        }
    }
}
