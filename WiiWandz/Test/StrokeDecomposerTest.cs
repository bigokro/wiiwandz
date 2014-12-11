using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiiWandz.Strokes;

namespace WiiWandz.Test
{
    class StrokeDecomposerTest
    {
        public StrokeDecomposerTest()
        {
        }

        public Boolean testDetermineStroke()
        {
            Boolean pass = true;

            StrokeDecomposer decomposer = new StrokeDecomposer(1024, 1024, 2);

            List<Stroke> strokes = new List<Stroke>();
            strokes.Add(Stroke.Bumbled);

            List<Stroke> toMatch = new List<Stroke>();
            toMatch.Add(Stroke.Right);
            pass = pass && !decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.Right);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.Left);
            pass = pass && !decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Right);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.Right);
            strokes.Add(Stroke.Bumbled);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.Left);
            strokes.Add(Stroke.Right);
            strokes.Add(Stroke.Bumbled);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            toMatch.Clear();
            toMatch.Add(Stroke.UpToTheRight);
            toMatch.Add(Stroke.DownToTheRight);
            toMatch.Add(Stroke.Left);

            strokes.Clear();
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.Right);
            pass = pass && !decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.DownToTheRight);
            strokes.Add(Stroke.Left);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.DownToTheRight);
            strokes.Add(Stroke.Left);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.DownToTheRight);
            strokes.Add(Stroke.Right);
            pass = pass && !decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Up);
            strokes.Add(Stroke.Left);
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.DownToTheRight);
            strokes.Add(Stroke.Left);
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.Down);
            pass = pass && decomposer.strokesMatch(strokes, toMatch);

            strokes.Clear();
            strokes.Add(Stroke.Up);
            strokes.Add(Stroke.Left);
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.Bumbled);
            strokes.Add(Stroke.DownToTheRight);
            strokes.Add(Stroke.Left);
            strokes.Add(Stroke.UpToTheRight);
            strokes.Add(Stroke.Down);
            pass = pass && !decomposer.strokesMatch(strokes, toMatch);

            return pass;
        }

    }
}
