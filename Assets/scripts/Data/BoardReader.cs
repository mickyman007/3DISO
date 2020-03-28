using System.IO;
/// <summary>
/// Static class for reading in board data in from file.
/// </summary>
public static class BoardReader {
    public static char[,] GetSymbols(string filePath) {

        var lines = File.ReadAllLines(filePath);
        var rows = lines.Length;
        var cols = 0;

        foreach(var line in lines) {
            cols = line.Length < cols ? cols : line.Length;
        }

        var symbols = new char[rows, cols];

        for (var x = 0; x < rows; x++) {
            var line = new char[cols];
            lines[x].ToCharArray().CopyTo(line, 0);
            for (var y = 0; y < cols; y++) {
                
                symbols[x, y] = line[y];
            }
        }

        return symbols;
    }

    public static string[,] GetSymbols(string filePath, char separator) {
        var lines = File.ReadAllLines(filePath);
        var rows = lines.Length;
        var cols = 0;

        foreach (var line in lines) {
            cols = line.Length < cols ? cols : line.Length;
        }

        var symbols = new string[rows, cols];

        for (var x = 0; x < rows; x++) {
            var line = new string[cols];
            lines[x].Split(separator).CopyTo(line, 0);
            for (var y = 0; y < cols; y++) {

                symbols[x, y] = line[y];
            }
        }

        return symbols;
    }
}
