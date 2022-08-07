using System.Configuration;

Console.BackgroundColor = ConsoleColor.Gray;

// Constants & Configs
string userDir = "userDir";
string enterFolderDirStatement = "Please enter the folder location where your npmrc lives?";
string yesNoStatement = "N for NO || Any Other Character for YES";
string exitStatement = "--- PRESS ENTER TO EXIT ---";
string npmrcName = ".npmrc";
string notInUseNpmrcName = ".notInUseNpmrcName";
string newUserDirValue = String.Empty;
bool isUserDirCorrect = true;
Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
var configEntry = config.AppSettings.Settings[userDir];
bool isConfigEntryInvalid = configEntry == null;
string originalUserFilePath = ConfigurationManager.AppSettings[userDir] ?? String.Empty;
bool isEdited = false;


// Tell user what their current configs are 
Console.ForegroundColor = ConsoleColor.Black;
Console.WriteLine("Current " + npmrcName + "folder path config : " + (isConfigEntryInvalid ? "NONE SET": originalUserFilePath));

if (isConfigEntryInvalid)
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(enterFolderDirStatement);
    newUserDirValue = Console.ReadLine() ?? String.Empty;
    if (newUserDirValue != String.Empty)
    {
        config.AppSettings.Settings.Add(userDir, (newUserDirValue + "\\"));
        isEdited = true;
        config.Save(ConfigurationSaveMode.Modified);
    }
} else
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("Is your npmrc file location correct?");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(yesNoStatement);
    string isUserDirCorrectAnswer = Console.ReadLine() ?? String.Empty;
    isUserDirCorrect = isUserDirCorrectAnswer.Contains("y") || isUserDirCorrectAnswer.Contains("Y") || string.IsNullOrEmpty(isUserDirCorrectAnswer) || string.IsNullOrWhiteSpace(isUserDirCorrectAnswer);

    // if userdir is incorrect, ask for correct one and save it
    if (!isUserDirCorrect) {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(enterFolderDirStatement);

        string editedUserDir = Console.ReadLine() ?? String.Empty;
        if (!String.IsNullOrEmpty(editedUserDir) || !String.IsNullOrWhiteSpace(editedUserDir))
        {
            config.AppSettings.Settings[userDir].Value = isUserDirCorrectAnswer;
            isEdited = true;
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
if (!isEdited)
{
    // Get configs and constants for main logic
    string userFilePath = ConfigurationManager.AppSettings[userDir] ?? String.Empty;
    string npmrcPath = @userFilePath + npmrcName;
    string notInUseNpmrcPath = @userFilePath + notInUseNpmrcName;
    bool isPersonalDev = false;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine("-- INFO --" );
    Console.WriteLine(npmrcName + " File Path ----> " + npmrcPath);
    Console.WriteLine(".notInUseNpmrc File Path ----> " + notInUseNpmrcPath);

    // Get user input for type of development
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("--------------------------------");
    Console.WriteLine("Are you developing for personal use or corporate use (npmjs vs corporate artifacts)?");
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine(yesNoStatement);

    string devTypeAnswer = Console.ReadLine() ?? String.Empty;
    isPersonalDev = devTypeAnswer.Contains("n") || devTypeAnswer.Contains("N");

    // Default to Corporate Development if no input is given. E.g. Pressing enter to skip the question
    if (String.IsNullOrEmpty(devTypeAnswer) || String.IsNullOrWhiteSpace(devTypeAnswer))
    {
        isPersonalDev = false;
    }

    try
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("--- " + (isPersonalDev ? "DeActivating" : "Activating") + " Corporate Development ---");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("--- Reading files ---");

        // Check if file to moved
        if (!File.Exists(isPersonalDev ? npmrcPath : notInUseNpmrcPath))
        {
            throw new FileNotFoundException();
        }

        // If file to be made exists, delete it
        if (File.Exists(notInUseNpmrcPath) && File.Exists(npmrcPath))
        {
            Console.WriteLine("--- " + (isPersonalDev ? notInUseNpmrcName : npmrcName) + " exists. Deleting file before moving ---");
            File.Delete(isPersonalDev ? notInUseNpmrcPath : npmrcPath);
        }

        Console.WriteLine("--- Moving " + (isPersonalDev ? npmrcName : notInUseNpmrcName) + " to " + (isPersonalDev ? notInUseNpmrcName : npmrcName) + " ---");
        if (isPersonalDev)
        {
            File.Move(npmrcPath, notInUseNpmrcPath);
        }
        else
        {
            File.Move(notInUseNpmrcPath, npmrcPath);
        }
        Console.BackgroundColor = ConsoleColor.Green;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("--- Moving complete ---");
        Console.WriteLine(exitStatement);
        Console.ReadLine();
    }
    catch (FileNotFoundException f)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("-- " + (isPersonalDev ? npmrcName : notInUseNpmrcName) + " file not found in the specified location. FileName: " + f.FileName + "--");
    }
    catch (Exception e)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exception: " + e.Message);
    }
    finally
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Executing finally block.");
    }
}
else
{
    Console.BackgroundColor = ConsoleColor.Red;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("--- PROGRAM NEEDS TO CLOSE. PLEASE REOPEN TO USE NEW CONFIG ---");
    Console.ReadLine();
    Console.WriteLine(exitStatement);
}
