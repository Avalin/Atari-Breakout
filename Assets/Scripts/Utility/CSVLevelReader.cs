using UnityEngine;

class CSVLevelReader : MonoBehaviour
{
    public static string[,] Bricks { get; private set; }
    public static int Level { get; set; }
    public static bool BricksAreLoaded;
    private static int width;
    private static int height;
    private static TextAsset CSVData;
    [SerializeField]
    private bool isDebugging = false;

    void Start()
    {
        LoadCSVData(0);
        if(isDebugging) Bricks.Print2DArray();
    }

    public static void LoadCSVData(int level) 
    {
        CSVData = Resources.Load<TextAsset>("Levels/" + level);
        LoadBricksFromCSV(CSVData);
    }

    static void LoadBricksFromCSV(TextAsset CSVData)
    {
        BricksAreLoaded = false;
        string[] columns = CSVData.text.Split(new char[] { '\n' });
        height = columns.Length-1;
        width = columns[0].Split(new char[] { ';' }).Length;
        Bricks = new string[height, width];
        
        for (int x = 0; x < height; x++)
        {
            string[] row = columns[x].Split(new char[] { ';' });
            for (int y = 0; y < width; y++)
            {
                Bricks[x, y] = row[y];
            }
        }
        BricksAreLoaded = true;
    }
}