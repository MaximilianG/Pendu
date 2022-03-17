using System;

namespace Pendu
{
    class Program
    {
        // Variables and Enums
        #region GameStates
        public enum GameState
        {
            PLAYING = 0,
            WIN = 1,
            LOOSE = 2,
            OVER = 3,
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

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Le jeu du Pendu commence !");
            m_gameState = GameState.PLAYING;
            m_gameMode = GetGameMode();
            
            GameUpdate();

            /*char letter;
            letter = GetLetter();*/
            
            
        }

        static void GameUpdate()
        {
            while (m_gameState == GameState.PLAYING)
            {
                if (m_gameMode == GameMode.COUNTRY)
                {
                    wordToFind = GetRandomWord(countries);
                }
                else
                {
                    wordToFind = GetRandomWord(countries); // To change when new list of words implemented
                }
                
                HangmanDisplay();
            }

            
        }

        static void HangmanDisplay()
        {

            Console.WriteLine("   +---+");
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
                Console.WriteLine("  /     |");
            else if (healthPoints == 0)
                Console.WriteLine("  / \\  |");
            else
                Console.WriteLine("       |");

            Console.WriteLine("========");
        }

        
        static char GetLetter()
        {
            Console.Write("Veuillez entrer une lettre : ");
            string entry = Console.ReadLine();

            while(entry.Length > 1 || Char.IsLetter(Convert.ToChar(entry)) == false)
            {
                Console.Write("Entrée incorrecte, veuillez entrer une seule lettre entre A et Z : ");
                entry = Console.ReadLine();
            }

            return Convert.ToChar(entry);
        }

        static GameMode GetGameMode()
        {
            Console.Write("1 - Jouer avec des pays.\n2 - Jouer avec des mots compliqués.\nTapez 1 ou 2 : ");

            string entry = Console.ReadLine();

            while(entry != "1" && entry != "2")
            {
                Console.Write("Entrée incorrecte, veuillez entrer 1 ou 2 : ");
                entry = Console.ReadLine();
            }

            if (entry == "1")
                return GameMode.COUNTRY;
            else
                return GameMode.WORDS;
        }

        static void DisplayUnderscores(int nb)
        {
            for (int i = 0; i < nb; i++)
                Console.Write("_ ");
        }

        static string GetRandomWord(string[] wordList)
        {
            Random rnd = new Random();
            int listIndex = rnd.Next(wordList.Length);

            return wordList[listIndex];
        }


    }
}
