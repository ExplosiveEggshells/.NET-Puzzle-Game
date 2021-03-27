using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogersErwin_Assign5
{
    public class StageManager
    {
        public List<Stage> EasyStages;
        public List<Stage> MediumStages;
        public List<Stage> HardStages;

        public StageManager()
        {
            EasyStages = new List<Stage>();
            MediumStages = new List<Stage>();
            HardStages = new List<Stage>();
        }

        public void BuildStageLists()
        {
            List<string> paths = new List<string>();

            using (StreamReader reader = new StreamReader("../../directory.txt"))
            {
                while (!reader.EndOfStream)
                {
                    paths.Add(reader.ReadLine());
                }
            }

            foreach (string path in paths)
            {
                using (StreamReader reader = new StreamReader("../../" + path))
                {
                    List<int> boardValues = new List<int>();
                    List<int> solutionValues = new List<int>();
                    List<Point> lockedCells = new List<Point>();
                    int gameSize;

                    List<int> correctRowSums = new List<int>();
                    List<int> correctColumnSums = new List<int>();
                    int correctDiagonalSum;

                    if (path.Contains("easy")) { gameSize = 3;}
                    else if (path.Contains("medium")) { gameSize = 5;}
                    else { gameSize = 7; }

                    int cellCount = gameSize * gameSize;
                    int count = 0;
                    while (!reader.EndOfStream)
                    {
                        int next = reader.Read() - 48;
                        if (next == -38) { continue; }
                        if (count < cellCount)
                        {
                            boardValues.Add(next);
                        } else
                        {
                            solutionValues.Add(next);
                        }
                        count++;
                    }
                    string tag = path.Substring(path.IndexOf("/")+1, 2);

                    // Get correctRowSums
                    for (int i = 0; i < gameSize; i++)
                    {
                        int rowSum = 0;
                        for (int j = 0; j < gameSize; j++)
                        {
                            rowSum += solutionValues[(i * gameSize) + j];

                            int x = boardValues[(i * gameSize) + j];

                            if (x != 0)
                            {
                                lockedCells.Add(new Point(i, j));
                            }
                        }

                        correctRowSums.Add(rowSum);
                    }

                    // Get correctColumnSums
                    for (int i = 0; i < gameSize; i++)
                    {
                        int columnSum = 0;
                        for (int j = 0; j < gameSize; j++)
                        {
                            columnSum += solutionValues[i + gameSize*j];
                        }
                        correctColumnSums.Add(columnSum);
                    }

                    correctDiagonalSum = 0;
                    for (int i = 0; i < gameSize; i++)
                    {
                        correctDiagonalSum += solutionValues[(i*gameSize) + i];
                    }


                    Stage nextStage = new Stage(boardValues, solutionValues, lockedCells, gameSize, tag, correctRowSums, correctColumnSums, correctDiagonalSum, false);
                    
                    if (gameSize == 3) { EasyStages.Add(nextStage); }
                    else if (gameSize == 5) { MediumStages.Add(nextStage); }
                    else { HardStages.Add(nextStage); }
                }
            }
        }

        public Stage GetNextDifficulty(List<Stage> stageList, out bool exhausted)
        {
            exhausted = false;
            foreach (Stage stage in stageList)
            {
                string path = String.Format("../../saves/{0}.json", stage.stageName);
                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        Stage potentialStage = JsonSerializer.Deserialize<Stage>(reader.ReadToEnd());
                        if (!potentialStage.completed)
                        {
                            return potentialStage;
                        }
                    }
                } else
                {
                    return stage;
                }
            }

            exhausted = true;
            return new Stage();
        }
    }
}
