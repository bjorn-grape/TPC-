﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    public class Map
    {
        private char[,] tab_;

        public char[,] Tab => tab_;
        public int pos_x;
        public int pos_y;
        public Vector2 posInit = new Vector2(2, 2);
        public UInt32 height;
        public UInt32 width;
        public UInt32 size_tile = 32;

        public Map(string path)
        {
            tab_ = ParseFromFile(path);
        }


        bool crush(char c)
        {
            switch (c)
            {
                case ' ':
                    return false;
                case 'W':
                    return true;
                case 'D':
                    return false;
                case 'S':
                    return false;
                default:
                    return true;
            }
        }

        public bool IsColliding(float x, float y)
        {
            char br = tab_[Convert.ToInt32(y + 1.2f), Convert.ToInt32(x)];
            char tr = tab_[Convert.ToInt32(y + 1.2f), Convert.ToInt32(x + 0.8f)];
            char bl = tab_[Convert.ToInt32(y), Convert.ToInt32(x)];
            char tl = tab_[Convert.ToInt32(y), Convert.ToInt32(x + 0.8f)];
            // r & l are in case player is bigger than tile he/she crushes
            char l = tab_[Convert.ToInt32(y + 0.6f), Convert.ToInt32(x)];    
            char r = tab_[Convert.ToInt32(y + 0.6f), Convert.ToInt32(x + 0.8f)];
            
            return crush(br) || crush(bl) || crush(tl) || crush(tr)|| crush(r)|| crush(l) ;
        }

        public bool IsGroundForPlayer(float x, float y)
        {
            char bl = tab_[Convert.ToInt32(y), Convert.ToInt32(x + 0.001f)];    
            char br = tab_[Convert.ToInt32(y), Convert.ToInt32(x + 0.799f)];
            return crush(br) || crush(bl);
        }


        public char[,] ParseFromFile(string path)
        {
            IEnumerable<string>lines = File.ReadAllLines(path);
            height = (uint)lines.Count();
            width = (uint)lines.First().Count();
               
            char[,] tab = new char[height, width];
            var file = new StreamReader(path);

            for (int i = 0; i < height; i++)
            {
                string str = file.ReadLine();
                if (str != null && str.Length != width)
                    throw new Exception("Invalid File !");
                for (int j = 0; j < width; j++)
                {
                    if (str[j] == 'S')
                    {
                        Console.WriteLine("found S : " + i + "  " + j);
                        posInit = new Vector2(j, i);
                    }

                    tab[i, j] = str[j];
                }
            }
            return tab;
        }

        public void Print()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    Console.Write(tab_[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}