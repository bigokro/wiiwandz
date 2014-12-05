using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;

namespace WiiWandz
{
    class Incendio : SpellTrigger
    {
        private List<Position> positions;

        public Incendio(List<Position> positions)
        {
            this.positions = positions;
        }

        public void castSpell()
        {
            using (var client = new WebClient())
            {
                client.Headers.Set("Authorization", "Bearer f44dbe4b28c2f26eb9a10eb7cb3510dd465d3fe34355cefe6fdfddf4ce2c5ae6");
                client.Headers.Set("Accept", "application/vnd.littlebits.v2+json");

                var values = new NameValueCollection();
                values["percent"] = "100";
                values["duration_ms"] = "25000";

                var response = client.UploadValues("https://api-http.littlebitscloud.cc/devices/00e04c223418/output", values);

                var responseString = Encoding.Default.GetString(response);
            }
        }

        public Boolean casting()
        {
            Position lastPoint = positions.Last();

            return triggered() && (DateTime.Now.Subtract(lastPoint.time).TotalSeconds < 25);
        }

        public Boolean triggered()
        {
            Position lastPoint = positions.Last();
            Position currentPoint = lastPoint;
            Position nextPoint = lastPoint;
            Position switchPoint = lastPoint;
            Position startPoint = lastPoint;

            // At least half a second of upward motion
            int i = positions.Count();
            int increment = 10;

            if (i > 10)
            {
                log(positions.ElementAt(i - increment - 1).point.ToString()
                    + " " + positions.ElementAt(i - 1).point.ToString()
                    + " " + (positions.ElementAt(i - increment - 1).point.Y - positions.ElementAt(i - 1).point.Y),
                    increment);
            }
            while (i > 0 && currentPoint.point.Y - nextPoint.point.Y >= 0)
            {
                currentPoint = nextPoint;
                nextPoint = positions.ElementAt(i - 1);
                //log("Next point: " + nextPoint.point.Y 
                //    + " Current point: " + currentPoint.point.Y + " Diff: " 
                //    + (nextPoint.point.Y - currentPoint.point.Y), 
                //    increment);
                i -= increment;
            }

            if (i == 0)
            {
                log("Returning false because there was no switch point", increment);
                return false;
            }

            switchPoint = currentPoint;

            //log("Last point index: " + positions.Count + " Switch point index: " + i, increment);
            if (lastPoint.time.Subtract(switchPoint.time).TotalMilliseconds < 500)
            {
                log("Returning false because time between switch and now less than half a second - "
                    + switchPoint.point.ToString() + " to " + lastPoint.point.ToString() 
                    + lastPoint.time.Subtract(switchPoint.time).Milliseconds + "ms",
                    increment);
                return false;
            }
            if (Math.Abs(lastPoint.point.Y - switchPoint.point.Y) < Math.Abs(lastPoint.point.X - switchPoint.point.X))
            {
                log("Returning false because angle was mostly horizontal - " + switchPoint.point.ToString() + " to " + lastPoint.point.ToString(),
                    increment);
                return false;
            }

            // Find start point
            while (i > 0 && nextPoint.point.Y - currentPoint.point.Y >= 0)
            {
                currentPoint = nextPoint;
                nextPoint = positions.ElementAt(i - 1);
                i -= increment;
            }
            startPoint = currentPoint;

            if (switchPoint.time.Subtract(startPoint.time).TotalMilliseconds < 1000)
            {
                log("Returning false because time between start and switch less than a second", increment);
                return false;
            }
            if (Math.Abs(startPoint.point.Y - switchPoint.point.Y) < Math.Abs(startPoint.point.X - switchPoint.point.X))
            {
                log("Returning false because angle was mostly horizontal - " 
                    + startPoint.point.ToString() + " to " + switchPoint.point.ToString()
                    + " to " + lastPoint.point.ToString(),
                    increment);
                return false;
            }

            Console.WriteLine("Returning true!!!!!");
            return true;
        }

        private void log(String text, int increment)
        {
            if (positions.Count % increment == 0)
            {
                //Console.WriteLine(text);
            }
        }
    }
}
