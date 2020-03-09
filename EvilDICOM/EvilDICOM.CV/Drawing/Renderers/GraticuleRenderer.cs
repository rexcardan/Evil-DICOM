using OpenCvSharp;
using EvilDICOM.CV.RT;

namespace EvilDICOM.CV.Drawing.Renderers
{
    public class GraticuleRenderer
    {
        public static void Render(Mat image, double collimatorAngleDeg, double mmPerPixel)
        {
            var extent = 200;
            var baseTickSize = 5;
            var iso2D = new Point(image.Cols / 2, image.Rows / 2);

            var x1Grat = new Point(extent / mmPerPixel, 0);
            var x2Grat = new Point(-extent / mmPerPixel, 0);
            var y1Grat = new Point(0, extent / mmPerPixel);
            var y2Grat = new Point(0, -extent / mmPerPixel);

            for (int i = -extent; i <= extent; i += 10)
            {
                int tickSize = baseTickSize;
                if (i % 50 == 0)
                {
                    tickSize = 2 * baseTickSize;
                }

                var tickY1 = new Point(i / mmPerPixel, tickSize / mmPerPixel);
                var tickY2 = new Point(i / mmPerPixel, -tickSize / mmPerPixel);
                var tickX1 = new Point(tickSize / mmPerPixel, i / mmPerPixel);
                var tickX2 = new Point(-tickSize / mmPerPixel, i / mmPerPixel);

                tickY1 = DRR.Rotate(tickY1, collimatorAngleDeg) + iso2D;
                tickY2 = DRR.Rotate(tickY2, collimatorAngleDeg) + iso2D;
                tickX1 = DRR.Rotate(tickX1, collimatorAngleDeg) + iso2D;
                tickX2 = DRR.Rotate(tickX2, collimatorAngleDeg) + iso2D;


                image.Line(tickX1, tickX2,
                    new Scalar(0, 255, 255), 1, LineTypes.Link8);
                image.Line(tickY1, tickY2,
                  new Scalar(0, 255, 255), 1, LineTypes.Link8);

            }

            x1Grat = DRR.Rotate(x1Grat, collimatorAngleDeg) + iso2D;
            x2Grat = DRR.Rotate(x2Grat, collimatorAngleDeg) + iso2D;
            y1Grat = DRR.Rotate(y1Grat, collimatorAngleDeg) + iso2D;
            y2Grat = DRR.Rotate(y2Grat, collimatorAngleDeg) + iso2D;

            image.Line(x1Grat, x2Grat,
                new Scalar(0, 255, 255), 1, LineTypes.Link8);
            image.Line(y1Grat, y2Grat,
              new Scalar(0, 255, 255), 1, LineTypes.Link8);

            image.DrawMarker(new Point(iso2D.X, iso2D.Y), new Scalar(0, 0, 255), MarkerTypes.Cross, (int)(10 * mmPerPixel), 1, LineTypes.Link8);
        }
    }
}
