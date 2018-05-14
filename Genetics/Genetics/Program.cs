﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Genetics
{
    internal class Program
    {
        private const string PathForTest = "maybebest.save";
        private const string PathBotToSubmit = "../../save/bot.save";

        public static void Main(string[] args)
        {
            RessourceLoad.InitMap();
            RessourceLoad.SetCurrentMap("long"); //with this line you can set the current map from folder map
            //NewTraining(5);
            Train(1);
            
            //FromTerminalMakeTests(args);             

            // Feel free to use all the function below in order to train your players
        }

        /// <summary>
        /// This function create a whole new population
        /// and trains a population of 200 players by using new players at each generation
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        private static void NewTraining(int n)
        {
            Factory.SetPathSave(PathForTest);
            Factory.Init();
            Factory.TrainWithNew(n);
            Factory.PrintScore();
            Factory.SaveState();
        }


        /// <summary>
        ///  This function trains a population of 200 players by using new players at each generation
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        private static void TrainWithNew(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainWithNew(n);
            Factory.PrintScore();
            Factory.SaveState();
        }

        /// <summary>
        /// This function trains a population of 200 players by duplicating and applying modification to the copy of 
        /// the best players
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        private static void Train(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.Train(n);
            Factory.PrintScore();
            Factory.SaveState();
        }

        /// <summary>
        /// Show the current best player
        /// </summary>
        private static void Showbest()
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.GetBestPlayer().SetStart(RessourceLoad.GetCurrentMap());
            game.SetPlayer(Factory.GetBestPlayer());
            game.Run();
        }

        /// <summary>
        /// Show the nth player sorted by score in increasing order
        /// </summary>
        /// <param name="nth">player you want to access to, should be between 0 and 199</param>
        private static void ShowNth(int nth)
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.PrintScore(true);
            game.SetPlayer(Factory.GetNthPlayer(nth));
            game.Run();
        }

        /// <summary>
        /// This function allows you to try the game
        /// </summary>
        private static void PlayAsHuman()
        {
            Game1 game = new Game1();
            Player player = new Player();
            player.SetStart(RessourceLoad.GetCurrentMap());
            game.SetPlayer(player, true);
            game.Run();
        }

        /// <summary>
        /// save best player in folder save, you will be marked on this, so DON'T forget it!
        /// </summary>
        private static void SaveBest()
        {
            Factory.SetPathLoad(PathForTest);
            Factory.Init();
            var soloList = new List<Player> {Factory.GetBestPlayer()};
            SaveAndLoad.Save(PathBotToSubmit, soloList);
            Console.WriteLine("Saved Best Player");
        }

        private static void FromTerminalMakeTests(string[] args)
        {

            var isValid = args.Length == 1;
            RessourceLoad.SetCurrentMap("long"); //with this line you can set the current map from folder map
            if (isValid)
            {
                try
                {
                    if (File.Exists(args[0]))
                        Factory.SetListPlayer(SaveAndLoad.Load(args[0]));
                    else
                        isValid = false;
                    if (Factory.GetListPlayer().Count != 1)
                        isValid = false;
                }
                catch (Exception e)
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                Console.WriteLine("0");
                return;
            }

            int sum = 0;
            foreach (var tuple in RessourceLoad.MapGet())
            {
                    RessourceLoad.SetCurrentMap(tuple.Key);
                    sum += Factory.test();
            }
            Console.WriteLine(sum);
        }

        private static void MultiTrain(int nb)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainAllMaps(nb);
            Factory.PrintScore();
            Factory.SaveState();
        }
    }
}