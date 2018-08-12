using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game State
    int level;
    int stage;
    const string passReq = "Please, enter your password: ";

    string[][] passwordBank;

    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;
    string scrambledPass;

    // Use this for initialization
    void Start ()
    {
        SetupPasswords();

        DisplayMainMenu();
    }

    private void SetupPasswords()
    {
        passwordBank = new string[3][];

        //Level 1 MIT (EASY)
        passwordBank[0] = new string[3];
        passwordBank[0][0] = "tech";
        passwordBank[0][1] = "math";
        passwordBank[0][2] = "school";

        //Level 2 NASA (MEDIUM)
        passwordBank[1] = new string[3];
        passwordBank[1][0] = "neptune";
        passwordBank[1][1] = "jupiter";
        passwordBank[1][2] = "saturn";

        //Level 3 NASA (MEDIUM)
        passwordBank[2] = new string[3];
        passwordBank[2][0] = "espionage";
        passwordBank[2][1] = "intelligence";
        passwordBank[2][2] = "kompromat";
    }

    void DisplayMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.WriteLine("Good evening, what site would you like to hack today?");
        Terminal.WriteLine("");
        Terminal.WriteLine("1) MIT (Easy)");
        Terminal.WriteLine("2) NASA (Medium)");
        Terminal.WriteLine("3) CIA (Hard)");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your command:");
    }
    //"Tech", "Math", "School"
    //"Neptune","Jupiter","Saturn"
    //"Espionage","Intelligence,"Kompromat"

    void OnUserInput(string input)
    {
        Terminal.ClearScreen();

        if (input.ToLower() == "menu")
        {
            DisplayMainMenu();
        }
        else
        {
            switch (currentScreen)
            {
                case Screen.MainMenu:
                    RunMainMenu(input.ToLower());
                    break;
                case Screen.Password:
                    CheckPassword(input.ToLower());
                    break;
                case Screen.Win:
                    EndGame(input.ToLower());
                    break;
            }
        }
    }

    private void RunMainMenu(string input)
    {
        switch (input)
        {
            case "1":
                level = 0;
                StartGame();
                break;
            case "2":
                level = 1;
                StartGame();
                break;
            case "3":
                level = 2;
                StartGame();
                break;
            case "007":
                Terminal.WriteLine("Good try... number 2!");
                break;
            case "menu":
                DisplayMainMenu();
                break;
            default:
                Terminal.WriteLine("Please enter a recogenizeable command!");
                DisplayMainMenu();
                break;
        }
    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        stage = 0;//use the level to determine rang//Random:Random.Range(0,passwordBank[level].Length);
        scrambledPass = passwordBank[level][stage].Anagram();
        Terminal.WriteLine("You have chosen to hack " + (level == 0 ? "MIT" : level == 1 ? "the NASA" : "the CIA"));
        Terminal.WriteLine("You can enter \"menu\" at any time.");
        Terminal.WriteLine(passReq);
    }

    void CheckPassword(string input)
    {
        if (input == passwordBank[level][stage])
        {
            WinCheck();
        }
        else
        {
            IncorrectGuess(input);
        }
    }

    private void IncorrectGuess(string input)
    {
        Terminal.WriteLine("Incorrect password detected!");
        Terminal.WriteLine("Hint: " + scrambledPass);
        CorrectCharCount(input);
        Terminal.WriteLine(passReq);
    }

    private void CorrectCharCount(string input)
    {
        List<char> found = new List<char>();
        for (int i = 0; i < scrambledPass.Length && i < input.Length; i++)
        {
            if (input[i] == passwordBank[level][stage][i] && !found.Contains(input[i]))
            {
                found.Add(input[i]);
            }
        }
        Terminal.WriteLine(found.Count + " correct letters identified.");
    }

    private void WinCheck()
    {
        stage++;
        bool stageWinCondition = (stage < passwordBank.Length);
        if (stageWinCondition)
        {
            NextRound();
        }
        else
        {
            DisplayWin();
        }
    }

    private void DisplayWin()
    {
        currentScreen = Screen.Win;

        Terminal.WriteLine("All stages have been cleared!");
        Terminal.WriteLine("Congratulations!");
        Terminal.WriteLine("User access granted! Welcome!");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("To play again,");
        Terminal.WriteLine("enter menu to select a level: ");
    }

    private void NextRound()
    {
        Terminal.WriteLine("Stage " + stage + " cleared!");
        Terminal.WriteLine("Beginning next stage.");
        scrambledPass = passwordBank[level][stage].Anagram();
        Terminal.WriteLine(passReq);
    }

    void EndGame(string input)
    {
        if(input == "menu")
        {
            DisplayMainMenu();
        }
        else
        {
            switch(input)
            {
                default:
                    Terminal.ClearScreen();
                    Terminal.WriteLine("That is not an acceptable input. Please try again!");
                    Terminal.WriteLine("To play again, enter menu to select a level: ");
                    break;
            }
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
