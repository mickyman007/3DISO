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

        var spaces = new char[rows, cols];

        for (var x = 0; x < rows; x++) {
            var line = new char[cols];
            lines[x].ToCharArray().CopyTo(line, 0);
            for (int y = 0; y < cols; y++) {
                
                spaces[x, y] = line[y];
            }
        }

        return spaces;
    }
}
