using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        //variable declaration
        static int enemyValue, lives, enemies, level, EXP, weapon;
        static string name, gamerTag, studioName, gameTitle, endMessage;
        static float health, shield;
        static string[] weapons = { "pistol", "shotgun", "spreader", "laser", "sniper", "BFG" };

        static void Main(string[] args)
        {
            //variable declaration
            Random rnd = new Random();
            int i, score;
            float scoreMult;

            //initializing values
            weapon = 0;
            studioName = "Benedict Games";
            gameTitle = "NO GAME QUEST";
            enemies = 10;
            enemyValue = 100;


            name = "Matthieu Benedict";
            gamerTag = "Twitchton";
            lives = 3;
            health = 100.0f;
            shield = 100.0f;
            level = 1;
            EXP = 0;
            score = 0;
            scoreMult = 1.0f;
            i = 0;

            expandedHUD(score, scoreMult);
            Console.WriteLine("Game Start!");
            Console.WriteLine();

            while (enemies > 0)
            {
                if (lives <= 0)
                {
                    die(score);
                }

                i++;

                TakeDamage(50.0f, scoreMult);

                enemies--;
                scoreMult += 0.5f;
                score = calcScore(score, scoreMult);

                AddXP();

                if (i % 2 == 0)
                {
                    ChangeWeapon(rnd.Next(6));
                }

                if (i % 3 == 0)
                {
                    shieldKit();
                }

                if (i % 4 == 0)
                {
                    healthPickUp();
                }

                Console.WriteLine();
                ShowHUD();
            }

            win(score, scoreMult);
        }

        private static void ShowTitle()
        {
            Console.WriteLine("----------------");
            Console.WriteLine(gameTitle + ":");
            Console.WriteLine("----------------");
            Console.WriteLine("By: " + studioName);
            Console.WriteLine();
        }

        private static void ShowHUD()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("GamerTag:\t\t" + gamerTag);
            Console.WriteLine("Shield:\t\t\t" + shield);
            Console.WriteLine("Health:\t\t\t" + health);
            Console.WriteLine("Status:\t\t\t" + healthStatus(health));
            Console.WriteLine("Lives:\t\t\t" + lives);
            Console.WriteLine("Level:\t\t\t" + level);
            Console.WriteLine("EXP:\t\t\t" + EXP);
            Console.WriteLine("Weapon:\t\t\t" + weapons[weapon]);
            Console.WriteLine("----------------");
            Console.WriteLine();
        }

        private static void expandedHUD(int score, float scoreMult)
        {
            ShowTitle();
            ShowHUD();
            Console.WriteLine("----------------");
            Console.WriteLine("Name:\t\t\t" + name);
            Console.WriteLine("GamerTag:\t\t" + gamerTag);
            Console.WriteLine("Score:\t\t\t" + score.ToString());
            Console.WriteLine("Score Multiplier:\t" + scoreMult.ToString());
            Console.WriteLine("----------------");
            Console.WriteLine();
        }

        private static float TakeDamage(float damage, float scoreMult)
        {
            if (shield > 0.0f)
            {
                shield -= damage;
                if (shield <= 0f)
                {
                    shield = 0f;
                    Console.WriteLine("Your Shield took Damage!");
                }
            }
            else
            {
                health -= damage;
                Console.WriteLine("You took Damage!");
            }

            if (health <= 0.0f)
            {
                lives--;
                health = 100.0f;
                shield = 100.0f;
                scoreMult = 1.0f;

                Console.WriteLine("You lost one life.");
            }

            return scoreMult;

        }

        private static void AddXP()
        {
            EXP += enemyValue / level;

            if (EXP >= 100)
            {
                level++;
                EXP = 0;

                Console.WriteLine("You leveled up!");
            }
        }

        private static int calcScore(int score, float scoreMult)
        {
            return score + (int)(enemyValue * scoreMult);
        }

        private static int calcFinalScore(int score, float scoreMult)
        {
            return score + (int)((level + lives * 100) * scoreMult);
        }

        private static void die(int score)
        {
            endMessage = "you died :(";
            printEnd(endMessage, score, 0f);
        }

        private static void win(int score, float scoreMult)
        {
            endMessage = "You win :)";
            printEnd(endMessage, score, scoreMult);
        }

        private static void healthPickUp()
        {
            health += 33.3f;
            if (health > 100.0f)
            {
                health = 100.0f;
            }
            Console.WriteLine("You picked up a HEALTH KIT!");
        }

        private static void shieldKit()
        {
            shield += 50.0f;
            Console.WriteLine("You picked up a SHIELD KIT!");
        }

        private static void printEnd(string endMessage, int score, float scoreMult)
        {
            expandedHUD(score, scoreMult);

            Console.WriteLine("Final Score: " + calcFinalScore(score, scoreMult));

            Console.WriteLine();
            Console.WriteLine(endMessage);
            Console.WriteLine();

            Console.WriteLine("Press any Key to exit.");
            Console.ReadKey();
            System.Environment.Exit(0);
        }

        private static void ChangeWeapon(int weaponPickedUp)
        {
            weapon = weaponPickedUp;
        }

        private static string healthStatus(float health)
        {
            string status = string.Empty;

            if (health == 100f)
            {
                status = "Perfect Health";
            }
            else if (health > 75f && health <100f ){
                status = "Healthy";
            }
            else if (health > 50f && health <=75f)
            {
                status = "Hurt";
            }
            else if (health > 10f && health <=50f)
            {
                status = "Badly Hurt";
            }
            else if (health > 0f && health <10f)
            {
                status = "Imminent Danger";
            }
            else
            {
                status = "???";
            }

            return status;
        }
    }
}
