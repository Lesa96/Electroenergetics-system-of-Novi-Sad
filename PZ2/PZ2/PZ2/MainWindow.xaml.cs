using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PZ2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<LineEntity> lineEntities { get; set; }
        public List<NodeEntity> nodeEntities { get; set; }
        public List<SubstationEntity> substationEntities { get; set; }
        public List<SwitchEntity> switchEntities { get; set; }

        double xPos, yPos;
        private double minLat = 45.2325;
        private double minLon = 19.793909;

        private double maxLat = 45.277031;
        private double maxLon = 19.894459;
        private ArrayList allModels = new ArrayList();
        public List<string> onmap = new List<string>();



        public MainWindow()
        {
            
            InitializeComponent();

            lineEntities = XmlHelper.GetAllLines();
            nodeEntities = XmlHelper.GetNodeEntities();
            substationEntities = XmlHelper.GetSubstationEntities();
            switchEntities = XmlHelper.GetSwitchEntities();

            ImportSubstations(substationEntities);
            ImportNodes(nodeEntities);
            ImportSwitches(switchEntities);
            ImportLines(lineEntities);


        }

        private bool showSub = false;
        private bool showNod = false;
        private bool showSwi = false;
        List<GeometryModel3D> substationModel = new List<GeometryModel3D>();
        List<GeometryModel3D> nodeModel = new List<GeometryModel3D>();
        List<GeometryModel3D> switchModel = new List<GeometryModel3D>();

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            if (showSub)
            {
                ImportSubstations(substationEntities);
                showSub = false;
            }
            else
            {
                foreach (GeometryModel3D sub in substationModel)
                {
                    if(allModels.Contains(sub))
                    {
                        allModels.Remove(sub);  
                        SystemView.Children.Remove(sub); // brise sa slike
                    }
                }

                showSub = true;
            }
        }

        private void Swi_Click(object sender, RoutedEventArgs e)
        {
            if (showSwi)
            {
                ImportSwitches(switchEntities);
                showSwi = false;
            }
            else
            {
                foreach (GeometryModel3D swi in switchModel)
                {
                    if (allModels.Contains(swi))
                    {
                        allModels.Remove(swi);  
                        SystemView.Children.Remove(swi);// brise sa slike
                    }
                }

                showSwi = true;
            }
        }

        private void Nod_Click(object sender, RoutedEventArgs e)
        {
            if (showNod)
            {
                ImportNodes(nodeEntities);
                showNod = false;
            }
            else
            {
                foreach (GeometryModel3D node in nodeModel)
                {
                    if (allModels.Contains(node))
                    {
                        allModels.Remove(node);  
                        SystemView.Children.Remove(node);// brise sa slike (mape)
                    }
                }

                showNod = true;
            }
        }



        private void ImportSubstations(List<SubstationEntity> substations)
        {
            foreach (SubstationEntity s in substations)
            {
               

                if ((s.X > maxLat || s.X < minLat) || (s.Y > maxLon || s.Y < minLon)) // ako izlaze iz opsega ne crtamo ih
                {
                    continue;
                }
                else
                {
                    double latC = maxLat - minLat;  //kolki je opseg
                    double lonC = maxLon - minLon;
                    xPos = Math.Abs(minLat - s.X);
                    yPos = Math.Abs(minLon - s.Y);

                    SolidColorBrush brush = new SolidColorBrush(Colors.Orange);
                    var material = new DiffuseMaterial(brush);
                    var mesh = MakeCube(xPos, yPos, latC, lonC); //pravi objekat (kocku)
                    GeometryModel3D substationObj = new GeometryModel3D(mesh, material);

                    if (substationObj != null)  // ako nije null
                    {
                        substationModel.Add(substationObj); //dodajemo u listu svih substation
                        allModels.Add(substationObj);  // svi  koji postoje na crtezu
                        SystemView.Children.Add(substationObj);// na mapu da doda

                        if (!onmap.Contains(s.ID)) //ako ne sadrzi, dodaj ga
                        {                    
                            onmap.Add(s.ID);
                        }
                    }

                }
            }
        }

        private void ImportNodes(List<NodeEntity> nodes)
        {
            foreach (NodeEntity n in nodes)
            {

                

                if ((n.X > maxLat || n.X < minLat) || (n.Y > maxLon || n.Y < minLon))
                {
                    continue;
                }
                else
                {
                    double latC = maxLat - minLat;  //kolki je opseg
                    double lonC = maxLon - minLon;
                    xPos = Math.Abs(minLat - n.X);
                    yPos = Math.Abs(minLon - n.Y);

                    SolidColorBrush brush = new SolidColorBrush(Colors.Blue);
                    var material = new DiffuseMaterial(brush);
                    var mesh = MakeCube(xPos, yPos, latC, lonC);
                    GeometryModel3D nodeObj = new GeometryModel3D(mesh, material);

                    if (nodeObj != null)  // ako nije null
                    {
                        nodeModel.Add(nodeObj); //dodajemo u listu svih node
                        allModels.Add(nodeObj);  // svi  koji postoje na crtezu
                        SystemView.Children.Add(nodeObj);// na mapu da doda
                        
                        if (!onmap.Contains(n.ID)) //ako ne sadrzi, dodaj ga
                        {
                            onmap.Add(n.ID);
                        }
                    }

                }
            }
        }

        private void ImportSwitches(List<SwitchEntity> switches)
        {
            foreach (SwitchEntity s in switches)
            {

               

                if ((s.X > maxLat || s.X < minLat) || (s.Y > maxLon || s.Y < minLon))
                {
                    continue;
                }
                else
                {
                    double latC = maxLat - minLat;  //kolki je opseg
                    double lonC = maxLon - minLon;
                    xPos = Math.Abs(minLat - s.X);
                    yPos = Math.Abs(minLon - s.Y);

                    SolidColorBrush brush = new SolidColorBrush(Colors.Green);
                    var material = new DiffuseMaterial(brush);
                    var mesh = MakeCube(xPos, yPos, latC, lonC);
                    GeometryModel3D switchObj = new GeometryModel3D(mesh, material);

                    if (switchObj != null)  // ako nije null
                    {
                        switchModel.Add(switchObj); //dodajemo u listu svih switch
                        allModels.Add(switchObj);  // svi  koji postoje na crtezu
                        SystemView.Children.Add(switchObj);// na mapu da doda

                        if (!onmap.Contains(s.ID)) //ako ne sadrzi, dodaj ga
                        {
                            onmap.Add(s.ID);
                        }
                    }

                }
            }
        }

        private SolidColorBrush lineBrush;

        private void ImportLines(List<LineEntity> lines)
        {
            foreach (LineEntity l in lines)
            {
                if (onmap.Contains(l.FirstEnd.ToString()) && onmap.Contains(l.SecondEnd.ToString())) // proveravam da li medju nacrtanim cvorovima postoji linija, medju kojima se linija nalazai ,ako postoji nema potrebe da crtam linuju
                {

                    
                    lineBrush = new SolidColorBrush(Colors.Red);
                    

                    for (int i = 0; i < l.Points.Count; i++) //kada za svaku linju izvucem tacke i izmedju svaka dva para tacaka crtam liniju
                    {
                        if (i != l.Points.Count - 1)
                        {
                            GeometryModel3D lineObj = this.placeLine(l.Points[i].X, l.Points[i].Y, l.Points[i + 1].X, l.Points[i + 1].Y);
                            allModels.Add(lineObj);
                            SystemView.Children.Add(lineObj);
                            onmap.Add(l.ID);
                        }
                    }
                }
            }

        }

        public GeometryModel3D placeLine(double x1, double y1, double x2, double y2)
        {
            double x1Pos, y1Pos, x2Pos, y2Pos;  // ovde imam dve tacke , odakle crta liniju


            double latC = maxLat - minLat;
            double lonC = maxLon - minLon;

            x1Pos = Math.Abs(minLat - x1);
            y1Pos = Math.Abs(minLon - y1);

            x2Pos = Math.Abs(minLat - x2);
            y2Pos = Math.Abs(minLon - y2);

            var material = new DiffuseMaterial(lineBrush);
            var mesh = new MeshGeometry3D();

            Point3D vertex1 = new Point3D(-1 + 2 * y1Pos / lonC, -1 + 2 * x1Pos / latC, 0.005);
            Point3D vertex2 = new Point3D(-0.995 + 2 * y1Pos / lonC, -1 + 2 * x1Pos / latC, 0.005);
            Point3D vertex3 = new Point3D(-1 + 2 * y1Pos / lonC, -0.995 + 2 * x1Pos / latC, 0.005);
            Point3D vertex4 = new Point3D(-0.995 + 2 * y1Pos / lonC, -0.995 + 2 * x1Pos / latC, 0.005);
            Point3D vertex5 = new Point3D(-1 + 2 * y2Pos / lonC, -1 + 2 * x2Pos / latC, 0.005);
            Point3D vertex6 = new Point3D(-0.995 + 2 * y2Pos / lonC, -1 + 2 * x2Pos / latC, 0.005);
            Point3D vertex7 = new Point3D(-1 + 2 * y2Pos / lonC, -0.995 + 2 * x2Pos / latC, 0.005);
            Point3D vertex8 = new Point3D(-0.995 + 2 * y2Pos / lonC, -0.995 + 2 * x2Pos / latC, 0.005);

            mesh.Positions.Add(vertex1);
            mesh.Positions.Add(vertex2);
            mesh.Positions.Add(vertex3);
            mesh.Positions.Add(vertex4);
            mesh.Positions.Add(vertex5);
            mesh.Positions.Add(vertex6);
            mesh.Positions.Add(vertex7);
            mesh.Positions.Add(vertex8);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(5);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(4);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(5);

            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(5);

            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(7);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(6);

            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(4);

            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            return new GeometryModel3D(mesh, material);
        }


        private MeshGeometry3D MakeCube(double xPos, double yPos, double latC, double lonC)
        {
            var mesh = new MeshGeometry3D();
            Point3D vertex1 = new Point3D(-1 + 2 * yPos / lonC, -1 + 2 * xPos / latC, 0);
            Point3D vertex2 = new Point3D(-0.99 + 2 * yPos / lonC, -1 + 2 * xPos / latC, 0);
            Point3D vertex3 = new Point3D(-1 + 2 * yPos / lonC, -0.99 + 2 * xPos / latC, 0);
            Point3D vertex4 = new Point3D(-0.99 + 2 * yPos / lonC, -0.99 + 2 * xPos / latC, 0);
            Point3D vertex5 = new Point3D(-1 + 2 * yPos / lonC, -1 + 2 * xPos / latC, 0.01);  // visina kocke
            Point3D vertex6 = new Point3D(-0.99 + 2 * yPos / lonC, -1 + 2 * xPos / latC, 0.01);
            Point3D vertex7 = new Point3D(-1 + 2 * yPos / lonC, -0.99 + 2 * xPos / latC, 0.01);
            Point3D vertex8 = new Point3D(-0.99 + 2 * yPos / lonC, -0.99 + 2 * xPos / latC, 0.01);

            mesh.Positions.Add(vertex1);
            mesh.Positions.Add(vertex2);
            mesh.Positions.Add(vertex3);
            mesh.Positions.Add(vertex4);
            mesh.Positions.Add(vertex5);
            mesh.Positions.Add(vertex6);
            mesh.Positions.Add(vertex7);
            mesh.Positions.Add(vertex8);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(1);

            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);

            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(3);

            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(1);

            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(7);

            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);

            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(4);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(3);

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(5);

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(4);
            return mesh;
        }


        private GeometryModel3D hitgeo;
        private HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {

            RayHitTestResult rayResult = rawresult as RayHitTestResult;

            if (rayResult != null)
            {
                bool gasit = false;
                for (int i = 0; i < allModels.Count; i++)
                {
                    if ((GeometryModel3D)allModels[i] == rayResult.ModelHit)
                    {
                        hitgeo = (GeometryModel3D)rayResult.ModelHit;
                        gasit = true;
                        foreach (SubstationEntity s in substationEntities)
                        {
                            if (s.ID.Equals(onmap[i]))
                            {
                                MessageBox.Show("SUBSTATION\n--------------\nId: " + s.ID + "\nName: " + s.Name);
                            }
                        }
                        foreach (NodeEntity n in nodeEntities)
                        {
                            if (n.ID.Equals(onmap[i]))
                            {
                                MessageBox.Show("NODE\n--------------\nId: " + n.ID + "\nName: " + n.Name);
                            }
                        }
                        foreach (SwitchEntity sw in switchEntities)
                        {
                            if (sw.ID.Equals(onmap[i]))
                            {
                                MessageBox.Show("SWITCH\n--------------\nId: " + sw.ID + "\nName: " + sw.Name);
                            }
                        }
                        foreach (LineEntity le in lineEntities)
                        {
                            if (le.ID.Equals(onmap[i]))
                            {
                                MessageBox.Show("LINE\n--------------\nId: " + le.ID + "\nName: " + le.Name);
                            }
                        }
                    }
                }
                if (!gasit)
                {
                    hitgeo = null;
                }
            }

            return HitTestResultBehavior.Stop;
        }


        private Point start = new Point();
        private Point diffOffset = new Point(); //pamti pomeraj
        private int zoomMax = 7;
        private int zoomCurent = 1;

        private void viewport1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //panovanje
        {
            viewport1.CaptureMouse();
            start = e.GetPosition(this);
            diffOffset.X = translacija.OffsetX;
            diffOffset.Y = translacija.OffsetY;


            System.Windows.Point mouseposition = e.GetPosition(viewport1);
            Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 10);

            PointHitTestParameters pointparams =
                     new PointHitTestParameters(mouseposition);
            RayHitTestParameters rayparams =
                     new RayHitTestParameters(testpoint3D, testdirection);

            //test for a result in the Viewport3D     
            hitgeo = null;
            VisualTreeHelper.HitTest(viewport1, null, HTResult, pointparams);

        }
        private void viewport1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            viewport1.ReleaseMouseCapture();  // kad pustim mis, da mi ne pamti gde sam kliknuo
        }
        private void viewport1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point p = e.MouseDevice.GetPosition(this);
            double scaleX = 1;
            double scaleY = 1;
            if (e.Delta > 0 && zoomCurent < zoomMax)
            {
                scaleX = skaliranje.ScaleX + 0.1;
                scaleY = skaliranje.ScaleY + 0.1;
                zoomCurent++;
                skaliranje.ScaleX = scaleX;
                skaliranje.ScaleY = scaleY;
            }
            else if (e.Delta <= 0 && zoomCurent > -zoomMax)
            {
                scaleX = skaliranje.ScaleX - 0.1;
                scaleY = skaliranje.ScaleY - 0.1;
                zoomCurent--;
                skaliranje.ScaleX = scaleX;
                skaliranje.ScaleY = scaleY;
            }

        }

        private Point rotationStart = new Point();
        

        private void viewport1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                rotationStart = e.GetPosition(this);
            }
        }

        

        private void viewport1_MouseMove(object sender, MouseEventArgs e)
        {
            if (viewport1.IsMouseCaptured)
            {
                Point end = e.GetPosition(this);
                double offsetX = end.X - start.X;
                double offsetY = end.Y - start.Y;
                double w = this.ActualWidth;
                double h = this.ActualHeight;
                double translateX = (offsetX * 500) / w;
                double translateY = -(offsetY * 500) / h;
                translacija.OffsetX = diffOffset.X + (translateX / (500 * skaliranje.ScaleX));
                translacija.OffsetY = diffOffset.Y + (translateY / (500 * skaliranje.ScaleX)); // translacija zavisi od skaliranja
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                Point end = e.GetPosition(this);
                double offsetX = end.X - rotationStart.X;
                double offsetY = end.Y - rotationStart.Y;
                //if ((myAngleRotationX.Angle + (0.4) * offsetY < 85 && myAngleRotationX.Angle + (0.4) * offsetY > -85))
                //{
                    myAngleRotationX.Angle += (0.4) * offsetY;
                //}
                myAngleRotationY.Angle += (0.4) * offsetX;

                rotationStart = end;
            }
        }







    }
}
