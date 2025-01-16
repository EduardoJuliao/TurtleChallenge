using TerminalConsole = System.Console;

namespace TurtleChallenge.Console.Helpers
{
    public static class FilePathHelper
    {
        public static void ValidateBoardSetupFilePath(ref string filePath)
        {
            var isFileValid = false;
            do
            {
                if(string.IsNullOrEmpty(filePath))
                {
                    TerminalConsole.WriteLine("Please provide the path for the board setup!");
                    TerminalConsole.WriteLine("If unsure on how to create the json file, there are some examples under ./Data/Setups");
                    filePath = TerminalConsole.ReadLine()!;
                }
                else if (!filePath.EndsWith(".json"))
                {
                    TerminalConsole.WriteLine("The file is not a json file, please provide a valid json!");
                    filePath = TerminalConsole.ReadLine()!;
                }
                else if(!File.Exists(filePath))
                {
                    TerminalConsole.WriteLine("The file does not exist, please provide a valid path!");
                    filePath = TerminalConsole.ReadLine()!;
                }
                else
                {
                    isFileValid = true;
                }
            }while(!isFileValid);
        }

        public static void ValidateMoveInstructionsFilePath(ref string filePath)
        {
            var isFileValid = false;
            do
            {
                if(string.IsNullOrEmpty(filePath))
                {
                    TerminalConsole.WriteLine("Please provide the path for the movement instructions!");
                    TerminalConsole.WriteLine("If unsure on how to create the json file, there are some examples under ./Data/Movements");
                    filePath = TerminalConsole.ReadLine()!;
                }
                else if (!filePath.EndsWith(".json"))
                {
                    TerminalConsole.WriteLine("The file is not a json file, please provide a valid json!");
                    filePath = TerminalConsole.ReadLine()!;
                }
                else if(!File.Exists(filePath))
                {
                    TerminalConsole.WriteLine("The file does not exist, please provide a valid path!");
                    filePath = TerminalConsole.ReadLine()!;
                }
                else
                {
                    isFileValid = true;
                }
            }while(!isFileValid);
        }
    }
}
