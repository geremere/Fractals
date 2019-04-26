using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    class Piftree : Fractal
    {
        protected double ang1;
        protected double ang2;
        protected List<double> koef;
        bool flag = true;//флажок чтобы был


        public Piftree(Color startColor, Color lastColor, int depthmax, float leng, List<double> koef, double ang1, double ang2):
            base(startColor,lastColor, depthmax, leng)
        {
            this.koef = koef;
            this.ang1 = ang1;
            this.ang2 = ang2;
        }

        public void CustomDraw(double x, double y, ref Graphics gr, List<Color> colorList, Pen pn, double ang = Math.PI / 2)
        {
            if (depthmax != 0)
            {
                double x1 = Math.Round(x + (depthmax * Math.Cos(ang)) * leng * koef[depthzero]),
                           y1 = Math.Round(y - (depthmax * Math.Sin(ang)) * leng *  koef[depthzero]);

                pn.Color = colorList[depthzero];

                try
                {
                    gr.DrawLine(pn, (float)x, (float)y, (float)x1, (float)y1);//рисуем линию ловим переполнение
                }
                catch (OverflowException e1)//вроде не должно выпадать но на всякий случай))
                {
                    if (flag)
                    {
                        MessageBox.Show(e1.Message, "Exception");
                        flag = false;
                    }
                    return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так!?", "Exception");
                }

                x = x1;
                y = y1;


                depthzero++;

                depthmax--;

                pn.Color = colorList[depthzero];

                ang += ang1;
                CustomDraw(x, y, ref gr, colorList, pn, ang);

                pn.Color = colorList[depthzero];

                ang -= ang1;
                ang -= ang2;


                CustomDraw(x, y, ref gr, colorList, pn, ang);

                depthzero--;

                ang += ang2;

                depthmax++;

            }
        }
        /// <summary>
        /// пришлось запариться, чтобы было красиво передать углы
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="gr"></param>
        /// <param name="colorList"></param>
        /// <param name="pn"></param>
        public override void Draw(double x, double y, ref Graphics gr, List<Color> colorList, Pen pn)
        {
            CustomDraw(x, y, ref gr, colorList, pn);
        }
     
    }

}
