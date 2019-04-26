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
    abstract public class Fractal
    {
        public float leng;                    //по тз
        protected Color startColor;           //по тз
        protected Color lastColor;            //по тз
        protected int depthzero;              //по тз
        public int depthmax;                  //по тз

        public Fractal( Color startColor, Color lastColor, int depthmax, float leng)
        {
            this.leng = leng;                                //ну банально инициализируем
            this.startColor = startColor;                    //ну банально инициализируем
            this.lastColor = lastColor;                      //ну банально инициализируем
            this.depthmax = depthmax;                        //ну банально инициализируем
            this.depthzero = 0;
        }

        /// <summary>
        /// виртуальный метод котрый переопределяем в наследниках
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="gr"></param>
        /// <param name="colorList"></param>
        /// <param name="pn"></param>
        public virtual void Draw(double x, double y,ref Graphics gr, List<Color> colorList, Pen pn)
        {

        }

    }
}
