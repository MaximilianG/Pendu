using System;
using System.Threading;

namespace Pendu
{
    class Program
    {
        // Variables and Enums
        #region Variables and Enums
        public enum GameState
        {
            PLAYING = 0,
            WIN = 1,
            LOOSE = 2,
        }

        public enum GameMode
        {
            COUNTRY = 0,
            WORDS = 1,
        }

        public static int healthPoints = 6;

        public static GameState m_gameState;
        public static GameMode m_gameMode;
        public static string wordToFind;
        public static bool wordFound;

        public static string[] countries =
        {
            "france",
            "allemagne" ,
            "hongrie" ,
            "suisse" ,
            "espagne" ,
            "finlande" ,
            "belgique" ,
            "norvege" ,
            "bresil" ,
            "egypte" ,
            "canada" ,
            "quatar" ,
            "equateur" ,
            "mongolie" ,
            "nepal" ,
            "inde" ,
            "chine" ,
            "japon" ,
            "coree" ,
            "taiwan" ,
            "islande" ,
            "senegal" ,
            "vatican" ,
            "portugal" ,
            "luxembourg" ,
            "ecosse" ,
            "angleterre" ,
            "suede" ,
            "lettonie" ,
            "lutuanie" ,
            "mexique" ,
            "israel" ,
            "turquie" ,
            "algerie" ,
            "maroc" ,
            "tunisie" ,
            "congo" ,
            "antarctique" ,
            "russie" ,
            "ukraine" ,
            "guatemala",
            "honduras" ,
            "nicaragua" ,
            "cuba" ,
            "bahamas",
            "haiti" ,
            "panama" ,
            "venezuela" ,
            "colombie" ,
            "suriname" ,
            "perou" ,
            "bolovie" ,
            "paraguay" ,
            "chili" ,
            "uruguay" ,
            "argentine" ,
            "madagascar" ,
            "botwana" ,
            "namibie" ,
            "zimbabwe" ,
            "mozambique" ,
            "angola" ,
            "tanzanie" ,
            "gabom" ,
            "kenya" ,
            "somalie",
            "ouganda" ,
            "ethipoie" ,
            "djibouti" ,
            "cameroun" ,
            "soudan" ,
            "tchad" ,
            "nigeria" ,
            "ghana" ,
            "guinee" ,
            "gambi" ,
            "mali" ,
            "liby" ,
            "yemen" ,
            "jordanie" ,
            "koweit" ,
            "iran" ,
            "irak" ,
            "syrie" ,
            "liban" ,
            "chypre" ,
            "afganistan" ,
            "pakistan" ,
            "bengladesh" ,
            "tailande" ,
            "cambodge",
            "vietnam" ,
            "malaisie" ,
            "singapour" ,
            "indonesie" ,
            "australie" ,
            "fidji" ,
            "philippine" ,
            "kirghizistan" ,
            "tadjikistan" ,
            "ouzbekistan" ,
            "kazakhstan" ,
            "georgie" ,
            "azerbaidjan" ,
            "armenie" ,
            "estonie" ,
            "bielorussie" ,
            "moldavie" ,
            "roumanie" ,
            "pologne" ,
            "slovaquie" ,
            "hongrie" ,
            "serbi" ,
            "macedoine" ,
            "grece" ,
            "albanie" ,
            "croatie" ,
            "slovenie" ,
            "danemarque" ,
            "italie" ,
            "monaco" ,
            "groenland",
            "autriche" ,
            "montenegro" ,
            "bosnie" ,
            "malte"
        };

        public static char[] lettersNotInTheWord; // bad letters
        public static char[] alreadyGivenLetters; // good and bad letters

        #endregion

        static void Main(string[] args)
        {
            Game();
            
        }

        static void Game()
        {
            Console.WriteLine("The Hangman game shall begin!\n");
            m_gameState = GameState.PLAYING;
            healthPoints = 6;
            wordFound = false;
            m_gameMode = GetGameMode();

            lettersNotInTheWord = new char[0];
            alreadyGivenLetters = new char[0];

            //wordToFind = "france";

            if (m_gameMode == GameMode.COUNTRY)
            {
                wordToFind = GetRandomWord(countries).ToLower(); // To change when new list of words implemented
            }
            else
            {
                wordToFind = GetRandomWord(countries).ToLower(); // To change when new list of words implemented
            }

            Console.Clear();
            Console.WriteLine("Looking for a new word to find...");

            Thread.Sleep(1000);
            Console.Clear();

            GameUpdate();

        }

        static void GameUpdate()
        {
            char letter = ' ';
            bool firstDisplay = true;
            bool letterFound;
            int tries = 0; // bad letters
            int totalAttempts = 0; // good or bad letters

            while (m_gameState == GameState.PLAYING)
            {
                if (firstDisplay == true)
                {
                    firstDisplay = false;
                    Console.WriteLine("\nHealthpoints : " + healthPoints);
                    HangmanDisplay();
                    // Display word to find in a hidden way (_ _ _ _ _ _)
                    PrintHiddenWord();
                    letter = GetLetter(totalAttempts);
                    totalAttempts += 1;
                    Console.Clear();
                }
                else
                {
                    if (checkPresence(wordToFind, letter) == true)
                    {
                        letterFound = true;
                    }
                    else
                    {
                        letterFound = false;
                        tries += 1;
                        Array.Resize(ref lettersNotInTheWord, tries);
                        lettersNotInTheWord[tries-1] = letter;                        
                    }

                    if (letterFound == true)
                    {
                        Console.WriteLine("This letter was in the word to guess, good job!");
                    }
                    else
                    {
                        Console.WriteLine("This letter was not in the word to guess, so sad!");
                        healthPoints--;
                    }

                    Console.WriteLine("Healthpoints : " + healthPoints);
           
                    PrintWrongLetters(lettersNotInTheWord);
                    HangmanDisplay();

                    // Display word to find in a hidden way (_ _ _ _ _ _)
                    PrintHiddenWord();

                    CheckGameState();

                    if (m_gameState == GameState.PLAYING)
                    {
                        letter = GetLetter(totalAttempts);
                        totalAttempts += 1;
                        Console.Clear();
                    }
                }
            }

            GetAnswer();         
        }

        static void PrintHiddenWord()
        {

        }

        static bool checkPresence(string p_wordToFind, char p_letter)
        {
            return p_wordToFind.Contains(Char.ToLower(p_letter));
        }

        static bool checkCharPresenceInTab(char[] tab, char letter)
        {
            bool answer = false;

            for (int i=0; i< tab.Length; i++)
            {
                if (Char.ToLower(letter) == tab[i])
                {
                    answer = true;
                }
            }

            return answer;
        }

        static void PrintWrongLetters(char[] tab)
        {
            for (int i=0; i<tab.Length; i++)
            {
                if (i == 0)
                    Console.Write("These letters are not in the word to guess : " + tab[i].ToString().ToUpper());
                else
                    Console.Write(" - " + tab[i].ToString().ToUpper());
            }
        }

        static void GetAnswer()
        {
            if (m_gameState == GameState.WIN)
                Console.WriteLine("You won, congratulations!\n");
            else if (m_gameState == GameState.LOOSE)
                Console.WriteLine("You lost, so sad!\n");


            Console.Write("The game is now over, would you like to play again? (y/n) : ");
            string entry = Console.ReadLine();

            while (entry.ToLower() != "y" && entry.ToLower() != "yes" && entry.ToLower() != "n" && entry.ToLower() != "no")
            {
                Console.Write("Wrong answer, please write \"y\" for \"yes\" OR \"n\" for \"no\" : ");
                entry = Console.ReadLine();
            }

            if (entry == "y" || entry == "yes")
            {
                Console.Clear();
                Game();
            }
            else
            {
                Console.Write("Alright then, thanks for playing, see you soon!");
            }
        }

        static void CheckGameState()
        {
            if (healthPoints == 0)
                m_gameState = GameState.LOOSE;
            else if (wordFound == true)
                m_gameState = GameState.WIN;
        }

        static void HangmanDisplay()
        {

            Console.WriteLine("\n   +---+");
            Console.WriteLine("   |   |");

            if (healthPoints < 6)
                Console.WriteLine("   O   |");
            else
                Console.WriteLine("       |");

            if (healthPoints == 4)
                Console.WriteLine("   |   |");
            else if (healthPoints == 3)
                Console.WriteLine("  /|   |");
            else if (healthPoints < 3)
                Console.WriteLine("  /|\\  |");
            else
                Console.WriteLine("       |");

            if (healthPoints == 1)
                Console.WriteLine("  /    |");
            else if (healthPoints == 0)
                Console.WriteLine("  / \\  |");
            else
                Console.WriteLine("       |");

            Console.WriteLine("========");
        }

        
        static char GetLetter(int p_totalAttempts)
        {
            Console.Write("Give a letter : ");
            string entry = Console.ReadLine();

            while(entry.Length > 1 || Char.IsLetter(Convert.ToChar(entry)) == false || checkCharPresenceInTab(alreadyGivenLetters, Convert.ToChar(entry)) == true)
            {
                if (checkCharPresenceInTab(alreadyGivenLetters, Convert.ToChar(entry)) == true)
                    Console.Write("You already tried this letter, please type another one : ");
                else
                    Console.Write("Wrong answer, please write one single letter between A and Z : ");

                entry = Console.ReadLine();                
            }


            Array.Resize(ref alreadyGivenLetters, p_totalAttempts+1);
            alreadyGivenLetters[p_totalAttempts] = Convert.ToChar(entry);

            return Convert.ToChar(entry);
        }

        static GameMode GetGameMode()
        {
            Console.Write("1 - Play with countries.\n2 - Play with long hard words.\nType 1 or 2 : ");

            string entry = Console.ReadLine();

            while(entry != "1" && entry != "2")
            {
                Console.Write("Wrong answer, please type 1 or 2 : ");
                entry = Console.ReadLine();
            }

            if (entry == "1")
                return GameMode.COUNTRY;
            else
                return GameMode.WORDS;
        }

        static string GetRandomWord(string[] wordList)
        {
            Random rnd = new Random();
            int listIndex = rnd.Next(wordList.Length);

            return wordList[listIndex];
        }


    }
}
