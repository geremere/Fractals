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
    class H_fractal : Fractal
    {
        bool flag = false;//флажок чтобы был
        public H_fractal(Color startColor, Color lastColor, int depthmax, float leng) : base(startColor, lastColor, depthmax, leng)
        {

        }

        public override void Draw(double x, double y, ref Graphics gr, List<Color> colorList, Pen pn)
        {
            depthzero++;
            leng /= 2;//уменьшаем длину тк так в тз)

            if(depthzero<depthmax)
            {
                try
                {
                    Draw(x - leng, y - leng, ref gr, colorList, pn);       //тут без магии все рисуем и не напрягаемся
                    leng *= 2;                                         //тут без магии все рисуем и не напрягаемся
                    Draw(x - leng, y + leng, ref gr, colorList, pn);       //тут без магии все рисуем и не напрягаемся
                    leng *= 2;                                         //тут без магии все рисуем и не напрягаемся
                    Draw(x + leng, y - leng, ref gr, colorList, pn);       //тут без магии все рисуем и не напрягаемся
                    leng *= 2;                                         //тут без магии все рисуем и не напрягаемся
                    Draw(x + leng, y + leng, ref gr, colorList, pn);       //тут без магии все рисуем и не напрягаемся
                    leng *= 2;
                }
                catch (OverflowException e)//вроде не должно выпадать но на всякий случай))
                {
                    if (flag)
                    {
                        MessageBox.Show(e.Message, "Exception");
                        flag = false;
                    }
                    return;
                }
                catch (Exception)
                {
                    MessageBox.Show("Что-то пошло не так!?", "Exception");
                }
            }

            depthzero--;
            pn.Color = colorList[depthzero];//очевидно цвет линий

            gr.DrawLine(pn, (float)(x - leng), (float)y,(float)( x + leng), (float)y);                             //уходим в рекурсию))
            gr.DrawLine(pn, (float)(x - leng),(float)( y - leng), (float)(x - leng), (float)(y + leng));           //уходим в рекурсию))
            gr.DrawLine(pn, (float)(x + leng), (float)(y - leng), (float)(x + leng), (float)(y + leng));           //уходим в рекурсию))
        }
    }

}
