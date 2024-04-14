using System;
using System.IO;

public class Score {
    private static string saveFilepath = "game.dat";
    private static FileStream saveFile;

    public static int RunHighScore = 0;
    public static int SavedScore = 0;

    public static int LoadScore() {
        int score = 0;
		saveFile = File.Open(saveFilepath, FileMode.OpenOrCreate);
		if (saveFile.Length > 0) {
			using (var r = new BinaryReader(saveFile))
				score = r.ReadInt32();
		}
		else {
			using (var w = new BinaryWriter(saveFile))
				w.Write(0);
		}

		saveFile.Close();
        return score;
    }

    public static void SaveScore(int score) {
        SavedScore = score;

        saveFile = File.Open(saveFilepath, FileMode.Open);
        using (var w = new BinaryWriter(saveFile))
            w.Write(SavedScore);
        
        saveFile.Close();
    }
}