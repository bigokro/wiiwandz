using System.Collections.Generic;
using WiiWandz.Strokes;

namespace WiiWandz.Test
{
    class StrokeDecomposerTest
    {
        public StrokeDecomposerTest() {}

        public bool testDetermineStroke()
        {
            bool pass = true;

            var strokes = new List<StrokeDirection>();
            strokes.Add(StrokeDirection.Bumbled);

            var toMatch = new List<StrokeDirection>();
            toMatch.Add(StrokeDirection.Right);
            pass = pass && !StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.Right);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.Left);
            pass = pass && !StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Right);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.Right);
            strokes.Add(StrokeDirection.Bumbled);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.Left);
            strokes.Add(StrokeDirection.Right);
            strokes.Add(StrokeDirection.Bumbled);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            toMatch.Clear();
            toMatch.Add(StrokeDirection.UpToTheRight);
            toMatch.Add(StrokeDirection.DownToTheRight);
            toMatch.Add(StrokeDirection.Left);

            strokes.Clear();
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.Right);
            pass = pass && !StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.DownToTheRight);
            strokes.Add(StrokeDirection.Left);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.DownToTheRight);
            strokes.Add(StrokeDirection.Left);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.DownToTheRight);
            strokes.Add(StrokeDirection.Right);
            pass = pass && !StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Up);
            strokes.Add(StrokeDirection.Left);
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.DownToTheRight);
            strokes.Add(StrokeDirection.Left);
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.Down);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(StrokeDirection.Up);
            strokes.Add(StrokeDirection.Left);
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.DownToTheRight);
            strokes.Add(StrokeDirection.Bumbled);
            strokes.Add(StrokeDirection.Left);
            strokes.Add(StrokeDirection.UpToTheRight);
            strokes.Add(StrokeDirection.Down);
            pass = pass && StrokeDecomposer.strokesMatch(strokes, toMatch);

            return pass;
        }

    }
}
