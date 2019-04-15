using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checker_Game
{
    
    public partial class CheckerGame : Form
    {
        public bool start = false;
        public bool buttonclick = true;
        PictureBox selected = null;
        int turno = 0;
        List<PictureBox> mavibutonlar = new List<PictureBox>();
        List<PictureBox> qirmizibutonlar = new List<PictureBox>();


        public CheckerGame()
        {
            InitializeComponent();
            listofcheckers();
        }

        public void listofcheckers()
        {
            #region Mavi Butonlar listi

            mavibutonlar.Add(mavi1);
            mavibutonlar.Add(mavi2);
            mavibutonlar.Add(mavi3);
            mavibutonlar.Add(mavi4);
            mavibutonlar.Add(mavi5);
            mavibutonlar.Add(mavi6);
            mavibutonlar.Add(mavi7);
            mavibutonlar.Add(mavi8);
            mavibutonlar.Add(mavi9);
            mavibutonlar.Add(mavi10);
            mavibutonlar.Add(mavi11);
            mavibutonlar.Add(mavi12);
            #endregion
            #region Qirmizi Butonlar listi
            qirmizibutonlar.Add(qirm1);
            qirmizibutonlar.Add(qirm2);
            qirmizibutonlar.Add(qirm3);
            qirmizibutonlar.Add(qirm4);
            qirmizibutonlar.Add(qirm5);
            qirmizibutonlar.Add(qirm6);
            qirmizibutonlar.Add(qirm7);
            qirmizibutonlar.Add(qirm8);
            qirmizibutonlar.Add(qirm9);
            qirmizibutonlar.Add(qirm10);
            qirmizibutonlar.Add(qirm11);
            qirmizibutonlar.Add(qirm12);
            #endregion
        }

        public void selection(object obj)
        {
            
            try
            {
                selected.BackColor = Color.Black;
            }
            catch
            {
            }
            PictureBox ficha = (PictureBox)obj;
            selected = ficha;
            selected.BackColor = Color.Lime;

        }
        private void allclickevent(object sender, MouseEventArgs e)
        {
            movement((PictureBox)sender);
        }
        private void qirmizilar(object sender, MouseEventArgs e)
        {
            selection(sender);
        }
        private void maviler(object sender, MouseEventArgs e)
        {
            selection(sender);
        }
        private void queenchecker(string color)
        {
            if(color == "mavi" && selected.Location.Y==340)
            {
                selected.BackgroundImage = Properties.Resources.FirstOneClass_01;
                selected.Tag = "queen";

            }
            if(color == "qirm" && selected.Location.Y==67)
            {
                selected.BackgroundImage = Properties.Resources.SecondOneClass_01;
                selected.Tag = "queen";
            }
        }



        private bool busybox(Point point, List<PictureBox> box)
        {
            for (int i = 0; i < box.Count; i++)
            {
                if (point == box[i].Location)
                {
                    return true;
                }
            }
            return false;
        }

        private int average(int n1,int n2)
        {
            int result = n1 + n2;
            result = result / 2;
            return Math.Abs(result);
        }

        private bool checkfordest(PictureBox nowlocal,PictureBox futlocal, string color)
        {
            Point pointorigin = nowlocal.Location;
            Point pointdestination = futlocal.Location;
            int xda = pointorigin.Y - pointdestination.Y;
            xda = color == "qirm" ? xda : (xda * -1);  
            xda = selected.Tag == "queen" ? Math.Abs(xda) : xda;
            if (xda == 39)
            {
                return true;
            }
            //if(xda==156 && selected.Tag=="queen")
            //{
            //    return true;
            //}
            //if (xda == 195 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //if(xda== 117 && selected.Tag=="queen")
            //{
            //    return true;
            //}
            //if (xda == 234 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //if (xda == 273 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //---------------------

            //if (xda == -39)
            //{
            //    return true;
            //}
            //if (xda == -156 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //if (xda == -195 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //if (xda == -117 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //if (xda == -234 && selected.Tag == "queen")
            //{
            //    return true;
            //}
            //if (xda == -273 && selected.Tag == "queen")
            //{
            //    return true;
            //}


            else if (xda==78)
            {
                Point pointanother = new Point(average(pointdestination.X, pointorigin.X), average(pointdestination.Y, pointorigin.Y));

                List<PictureBox> listdisappear = color == "qirm" ? mavibutonlar : qirmizibutonlar;
                for (int i = 0; i < listdisappear.Count; i++)
                {
                    if (listdisappear[i].Location == pointanother)
                    {
                        listdisappear[i].Location = new Point(0, 0);
                        listdisappear[i].Visible = false;
                        return true;
                    }
                }
                    }
            else if(xda==-78)
            {
                Point pointanother = new Point(average(pointdestination.X, pointorigin.X), average(pointdestination.Y, pointorigin.Y));

                List<PictureBox> listdisappear = color == "qirm" ? mavibutonlar : qirmizibutonlar;
                for (int i = 0; i < listdisappear.Count; i++)
                {
                    if (listdisappear[i].Location == pointanother)
                    {
                        listdisappear[i].Location = new Point(0, 0);
                        listdisappear[i].Visible = false;
                        return true;
                    }
                }
            }
            return false;
        }

        private void movement(PictureBox itembox)
        {
            if(selected != null)
            {
                string color = selected.Name.ToString().Substring(0, 4);
                if (checkfordest(selected,itembox,color))
                {
                    
                    Point nowselected = selected.Location;
                    selected.Location = itembox.Location;
                    int xda = nowselected.Y - itembox.Location.Y;

                    queenchecker(color);
                    turno++;
                    selected.BackColor = Color.Black;
                    selected = null;
                }
            }
        }

       

    }
}
