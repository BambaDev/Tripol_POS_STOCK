using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace Pos.Function
{
    internal static class Sound
    {
        private static readonly Dictionary<string, SoundPlayer> soundPlayers = new Dictionary<string, SoundPlayer>();
        private static readonly string soundPath = @"C:\Users\XPRISTO\Pos\Pos\Resources\Sounds\";

        // Static constructor to preload sounds
        static Sound()
        {
            PreloadSounds();
        }

        // Preloading all necessary sounds
        private static void PreloadSounds()
        {
            LoadSound("Added.wav");
            LoadSound("Deleted.wav");
            LoadSound("Selected.wav");
            LoadSound("Denied.wav");
            LoadSound("Wrong.wav");
        }

        // Load individual sound and add to dictionary
        private static void LoadSound(string soundFileName)
        {
            string fullPath = Path.Combine("", soundPath, soundFileName);
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(fullPath);
                soundPlayer.Load();  // Preload sound
                soundPlayers[soundFileName] = soundPlayer;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading sound: " + ex.Message);
            }
        }

        // Play sound by name
        public static void PlaySound(string soundFileName)
        {
            Models.Setting setting = Shared.db.Settings.FirstOrDefault(); // Get settings from the database

            bool canPlay = false;
            switch (soundFileName)
            {
                case "Added.wav":
                    canPlay = setting?.IsSoundAdded ?? false;
                    break;
                case "Deleted.wav":
                    canPlay = setting?.IsSoundDeleted ?? false;
                    break;
                case "Selected.wav":
                    canPlay = setting?.IsSoundSelected ?? false;
                    break;
                case "Denied.wav":
                    canPlay = setting?.IsSoundDenied ?? false;
                    break;
                case "Wrong.wav":
                    canPlay = setting?.IsSoundWrong ?? false;
                    break;
            }

            if (canPlay && soundPlayers.TryGetValue(soundFileName, out SoundPlayer player))
            {
                try
                {
                    player.Play();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error playing sound: " + ex.Message);
                }
            }
        }

        // Asynchronous sound play
        public static void PlaySoundAsync(string soundFileName)
        {
            if (soundPlayers.TryGetValue(soundFileName, out SoundPlayer player))
            {
                try
                {
                    player.PlaySync();  // Asynchronous sound play
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error playing sound asynchronously: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Sound file not found: " + soundFileName);
            }
        }

        // Specific sound methods
        public static void Added() => PlaySound("Added.wav");
        public static void Deleted() => PlaySound("Deleted.wav");
        public static void Selected() => PlaySound("Selected.wav");
        public static void Denied() => PlaySound("Denied.wav");
        public static void Wrong() => PlaySound("Wrong.wav");
    }
}
