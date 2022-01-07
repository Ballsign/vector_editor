using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_{
    abstract class Figure {
        protected Point startLocation;
        protected Point endLocation;
        protected bool selected = true;
        protected bool moving = false;
        /*protected*/
        public Rectangle rectangle;
        protected Pen pen;
        public static Point endWindow;
        protected Rectangle[] resizeRect = new Rectangle[4];
        protected int resizeSquare = 8;
        protected Point deltaPoint;
        protected float[] dashValue = new float[] { 1.5f, 1.5f };
        protected int resizeBox;
        private int minSize = 10;

        protected double section(Point a, Point b) {
            return Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
        }

        public Figure(Point p1, Point p2) {
            startLocation.X = p1.X;
            startLocation.Y = p1.Y;

            endLocation.X = p2.X;
            endLocation.Y = p2.Y;

            rectangle = new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y));
            deltaPoint = new Point(rectangle.X - 4, rectangle.Y - 4);

            pen = new Pen(Color.Black, 3);
        }

        abstract public void draw(Graphics g, PictureBox pictureBox1, Bitmap bitmap);
        abstract public bool click(Point e);
        public virtual void setEndLocation(Point p) {
            endLocation.X = p.X;
            endLocation.Y = p.Y;

            rectangle.X = Math.Min(p.X, startLocation.X);
            rectangle.Y = Math.Min(p.Y, startLocation.Y);

            rectangle.Width = Math.Abs(p.X - startLocation.X);
            rectangle.Height = Math.Abs(p.Y - startLocation.Y);

            borderCheck();
        }
        public bool getSelected() {
            return selected;
        }
        public virtual void drawSelection(Graphics g, PictureBox pictureBox, Bitmap bitmap) {
            Pen dashPen = new Pen(Color.Blue, 2);
            //endWindow = new Point(bitmap.Width, bitmap.Height); // не забыть отредачить в линии

            dashPen.DashPattern = dashValue;
            g.DrawRectangle(dashPen, rectangle);

            deltaPoint = new Point(rectangle.X - 4, rectangle.Y - 4);

            resizeRect[0] = new Rectangle(deltaPoint.X, deltaPoint.Y, resizeSquare, resizeSquare);
            resizeRect[1] = new Rectangle(deltaPoint.X + rectangle.Width, deltaPoint.Y, resizeSquare, resizeSquare);
            resizeRect[2] = new Rectangle(deltaPoint.X, deltaPoint.Y + rectangle.Height, resizeSquare, resizeSquare);
            resizeRect[3] = new Rectangle(deltaPoint.X + rectangle.Width, deltaPoint.Y + rectangle.Height, resizeSquare, resizeSquare);

            for (int i = 0; i < 4; i++)
                g.FillRectangle(Brushes.Black, resizeRect[i]);

            pictureBox.Image = bitmap;
        }

        private void blockStartLocation(Point p) {
            if (endLocation.X - p.X < minSize)
                p.X = endLocation.X - minSize;
            if (endLocation.Y - p.Y < minSize)
                p.Y = endLocation.Y - minSize;

            startLocation.X = p.X;
            startLocation.Y = p.Y;

            rectangle.X = startLocation.X;
            rectangle.Y = startLocation.Y;

            rectangle.Width = endLocation.X - startLocation.X;
            rectangle.Height = endLocation.Y - startLocation.Y;

        }
        private void blockEndLocation(Point p) {
            if (p.X - startLocation.X < minSize)
                p.X = startLocation.X + minSize;
            if (p.Y - startLocation.Y < minSize)
                p.Y = startLocation.Y + minSize;

            endLocation.X = p.X;
            endLocation.Y = p.Y;

            rectangle.Width = endLocation.X - startLocation.X;
            rectangle.Height = endLocation.Y - startLocation.Y;

        }
        public virtual bool resizeBoxCheck(Point p) {
            /*if (selected) */
            {
                startLocation.X = rectangle.X;
                startLocation.Y = rectangle.Y;
                endLocation.X = rectangle.X + rectangle.Width;
                endLocation.Y = rectangle.Y + rectangle.Height;

                if (resizeRect[0].Contains(p)) {
                    resizeBox = 0;
                    return true;
                }
                if (resizeRect[1].Contains(p)) {
                    resizeBox = 1;
                    return true;
                }
                if (resizeRect[2].Contains(p)) {
                    resizeBox = 2;
                    return true;
                }
                if (resizeRect[3].Contains(p)) {
                    resizeBox = 3;
                    return true;
                }
            }
            resizeBox = -1;
            return false;
        }
        public virtual void resizeFigure(Point p) {
            switch (resizeBox) {
                case 0:
                    blockStartLocation(p);
                    break;
                case 1:
                    blockStartLocation(new Point(startLocation.X, p.Y));
                    blockEndLocation(new Point(p.X, endLocation.Y));
                    break;
                case 2:
                    blockStartLocation(new Point(p.X, startLocation.Y));
                    blockEndLocation(new Point(endLocation.X, p.Y));
                    break;
                case 3:
                    blockEndLocation(p);
                    break;
                default:
                    break;
            }
            borderCheck();
        }
        public virtual void borderCheck() {

            if (rectangle.X < 0) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Width += rectangle.X;
                rectangle.X = 0;
            }
            if (rectangle.Y < 0) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Height += rectangle.Y;
                rectangle.Y = 0;

            }

            if (rectangle.X + rectangle.Width > endWindow.X) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Width = endWindow.X - rectangle.X;
                rectangle.X = endWindow.X - rectangle.Width;

            }
            if (rectangle.Y + rectangle.Height > endWindow.Y) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Height = endWindow.Y - rectangle.Y;
                rectangle.Y = endWindow.Y - rectangle.Height;

            }
        }

        public Point movingPoint;
        public virtual bool clickOnArea(Point p) {
            if (selected) {
                movingPoint.X = p.X;
                movingPoint.Y = p.Y;
                moving = true;
                return rectangle.Contains(p) ? true : false;
            }
            moving = false;
            return false;
        }
        public virtual void movingFigure(Point p) {
            /*if (moving) */
            {
                rectangle.X += p.X - movingPoint.X;
                rectangle.Y += p.Y - movingPoint.Y;

                startLocation.X = rectangle.X;
                startLocation.Y = rectangle.Y;
                endLocation.X = rectangle.X + rectangle.Width;
                endLocation.Y = rectangle.Y + rectangle.Height;


                movingPoint.X = p.X;
                movingPoint.Y = p.Y;

                borderCheck();
            }
        }
        public virtual void setColor(Color color) {
            pen.Color = color;
        }
        public void setSelected(bool flag) {
            selected = flag;
        }
        public void setMoving(bool flag) {
            moving = flag;
        }
        public virtual bool isSlim() {
            if (rectangle.Height < 6 || rectangle.Width < 6)
                return true;
            return false;
        }
        public void setWindow(Point p) {
            endWindow.X = p.X;
            endWindow.Y = p.Y;
            borderCheck();
        }

        public string name;
        public override string ToString() {
            return (name + " ; " + rectangle.X.ToString() + " ; " + rectangle.Y.ToString() + " ; " + (rectangle.X + rectangle.Width).ToString() + " ; " + (rectangle.Y + rectangle.Height).ToString() + " ; " + pen.Color.ToArgb().ToString());
        }

        public Point getStartLocation() {
            return startLocation;
        }

        public Point getEndRectangle() {
            return new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
        }

        protected virtual void resizeGroup(Point p) {

        }

        public virtual void movingInGroup(Point p) {
            rectangle.X = p.X;
            rectangle.Y = p.Y;
        }

        public virtual void resizeInResizeGroup(Size size) {
            rectangle.Width = size.Width;
            rectangle.Height = size.Height;
        }

        public virtual void recursiveMovingPointSet(Point p) {
            movingPoint = p;
        }
    }

    class Line : Figure {

        public Line(Point p1, Point p2) : base(p1, p2) {
            name = "Line";

            rectangle.Location = p1;
            rectangle.Width = p2.X - p1.X;
            rectangle.Height = p2.Y - p1.Y;

        }

        public override void draw(Graphics g, PictureBox pictureBox1, Bitmap bitmap) {
            g.DrawLine(pen, rectangle.Location, new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
            pictureBox1.Image = bitmap;
            Debug.WriteLine(rectangle.Location + ", " + new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
        }
        public override bool clickOnArea(Point p) {
            if (selected) {
                movingPoint.X = p.X;
                movingPoint.Y = p.Y;
                moving = true;
                return rectangleSelection /*new Rectangle(new Point(Math.Min(startLocation.X, endLocation.X), Math.Min(startLocation.Y, endLocation.Y)), new Size(Math.Abs(startLocation.X - endLocation.X), Math.Abs(startLocation.Y - endLocation.Y)))*/.Contains(p) ? true : false;
            }
            moving = false;
            return false;
        }

        public override bool click(Point e) {
            if (Math.Round(section(e, startLocation) + section(e, endLocation)) == Math.Round(section(startLocation, endLocation))) {
                selected = true;
                return selected;
            }
            if (Control.ModifierKeys != Keys.Control) {
                selected = false;
                return false;
            }
            return false;
        }
        Rectangle rectangleSelection;
        public override void drawSelection(Graphics g, PictureBox pictureBox, Bitmap bitmap) {
            Pen dashPen = new Pen(Color.Blue, 2);
            dashPen.DashPattern = dashValue;

            rectangleSelection = new Rectangle(new Point(Math.Min(rectangle.X, rectangle.X + rectangle.Width), Math.Min(rectangle.Y, rectangle.Y + rectangle.Height)), new Size(Math.Abs(rectangle.Width), Math.Abs(rectangle.Height)));
            g.DrawRectangle(dashPen, rectangleSelection);

            deltaPoint = new Point(-4, -4);

            resizeRect[0] = new Rectangle(rectangle.X + deltaPoint.X, rectangle.Y + deltaPoint.Y, resizeSquare, resizeSquare);
            resizeRect[1] = new Rectangle(rectangle.X + rectangle.Width + deltaPoint.X, rectangle.Y + rectangle.Height + deltaPoint.Y, resizeSquare, resizeSquare);

            g.FillRectangle(Brushes.Black, resizeRect[0]);
            g.FillRectangle(Brushes.Black, resizeRect[1]);

            pictureBox.Image = bitmap;
        }

        public override bool resizeBoxCheck(Point p) {
            if (selected) {
                if (resizeRect[0].Contains(p)) {
                    resizeBox = 0;
                    return true;
                }
                if (resizeRect[1].Contains(p)) {
                    resizeBox = 1;
                    return true;
                }
            }
            resizeBox = -1;
            return false;
        }
        public override void resizeFigure(Point p) {
            switch (resizeBox) {
                case 0:
                    setStartLocation(p);
                    break;
                case 1:
                    setEndLocation(p);
                    break;
                default:
                    break;
            }
            borderCheck();
        }
        public override void setEndLocation(Point p) {
            endLocation.X = p.X;
            endLocation.Y = p.Y;

            rectangle.Width = p.X - startLocation.X;
            rectangle.Height = p.Y - startLocation.Y;


            borderCheck();
        }
        private void setStartLocation(Point p) {
            startLocation.X = p.X;
            startLocation.Y = p.Y;

            rectangle.X = startLocation.X;
            rectangle.Y = startLocation.Y;

            rectangle.Width = endLocation.X - startLocation.X;
            rectangle.Height = endLocation.Y - startLocation.Y;
            /*
                        rectangle.X = Math.Min(startLocation.X, endLocation.X);
                        rectangle.Y = Math.Min(startLocation.Y, endLocation.Y);

                        rectangle.Width = Math.Abs(endLocation.X - startLocation.X);
                        rectangle.Height = Math.Abs(endLocation.Y - startLocation.Y);
            */
            borderCheck();
        }
        public override bool isSlim() {
            return false;
        }

        public override void borderCheck() {
            rectangleSelection = new Rectangle(new Point(Math.Min(rectangle.X, rectangle.X + rectangle.Width), Math.Min(rectangle.Y, rectangle.Y + rectangle.Height)), new Size(Math.Abs(rectangle.Width), Math.Abs(rectangle.Height)));

            if (rectangle.X < 0) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Width += rectangle.X;
                rectangle.X = 0;
            }
            if (rectangle.Y < 0) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Height += rectangle.Y;
                rectangle.Y = 0;

            }
            if (rectangle.X > endWindow.X) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Width -= endWindow.X - rectangle.X;
                rectangle.X = endWindow.X;
            }
            if (rectangle.Y > endWindow.Y) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Height -= endWindow.Y - rectangle.Y;
                rectangle.Y = endWindow.Y;
            }

            if (rectangle.X + rectangle.Width > endWindow.X) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Width = endWindow.X - rectangle.X;
                rectangle.X = endWindow.X - rectangle.Width;

            }
            if (rectangle.Y + rectangle.Height > endWindow.Y) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Height = endWindow.Y - rectangle.Y;
                rectangle.Y = endWindow.Y - rectangle.Height;

            }
            if (rectangle.X + rectangle.Width < 0) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Width = -rectangle.X;
                rectangle.X = -rectangle.Width;
            }
            if (rectangle.Y + rectangle.Height < 0) {
                if (!moving || (selected && resizeBox != -1))
                    rectangle.Height = -rectangle.Y;
                rectangle.Y = -rectangle.Height;
            }
        }
        public override string ToString() {
            return (name + " ; " + rectangle.X.ToString() + " ; " + rectangle.Y.ToString() + " ; " + (rectangle.X + rectangle.Width).ToString() + " ; " + (rectangle.Y + rectangle.Height).ToString() + " ; " + pen.Color.ToArgb().ToString());
        }

    }

    class Circle : Figure {
        public Circle(Point p1, Point p2) : base(p1, p2) {
            name = "Circle";
        }
        public override void draw(Graphics g, PictureBox pictureBox1, Bitmap bitmap) {
            g.DrawEllipse(pen, rectangle);
            pictureBox1.Image = bitmap;
        }

        public override bool click(Point e) {
            Point centre = new Point(rectangle.Width / 2 + rectangle.X, rectangle.Height / 2 + rectangle.Y);
            int semiAxisA = rectangle.Width / 2;
            int semiAxisB = rectangle.Height / 2;

            if ((Math.Pow(e.X - centre.X, 2) / (semiAxisA * semiAxisA)) + (Math.Pow(e.Y - centre.Y, 2) / (semiAxisB * semiAxisB)) <= 1) {
                selected = true;
                return selected;
            }
            if (Control.ModifierKeys != Keys.Control) {
                selected = false;
                return false;
            }
            return false;

        }
    }

    class RRectangle : Figure {
        public RRectangle(Point p1, Point p2) : base(p1, p2) {
            name = "Rectangle";
        }

        public override void draw(Graphics g, PictureBox pictureBox1, Bitmap bitmap) {
            g.DrawRectangle(pen, rectangle);

            pictureBox1.Image = bitmap;
        }
        public override bool click(Point e) {
            if (rectangle.Contains(e)) {
                selected = true;
                return selected;
            }
            if (Control.ModifierKeys != Keys.Control) {
                selected = false;
                return selected;
            }
            return false;
        }
    }

    class Triangle : Figure {
        public Triangle(Point p1, Point p2) : base(p1, p2) {
            name = "Triangle";
        }
        public override void draw(Graphics g, PictureBox pictureBox1, Bitmap bitmap) {
            Point[] points = new Point[3];
            points[0] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            points[1] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            points[2] = new Point(rectangle.X + (rectangle.Width / 2), rectangle.Y);

            g.DrawLine(pen, points[0], points[1]);
            g.DrawLine(pen, points[0], points[2]);
            g.DrawLine(pen, points[1], points[2]);

            pictureBox1.Image = bitmap;
        }

        public override bool click(Point e) {
            Point[] points = new Point[3];
            points[0] = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            points[1] = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            points[2] = new Point(rectangle.X + (rectangle.Width / 2), rectangle.Y);

            int tmp1 = Math.Sign((points[0].X - e.X) * (points[1].Y - points[0].Y) - (points[1].X - points[0].X) * (points[0].Y - e.Y));
            int tmp2 = Math.Sign((points[1].X - e.X) * (points[2].Y - points[1].Y) - (points[2].X - points[1].X) * (points[1].Y - e.Y));
            int tmp3 = Math.Sign((points[2].X - e.X) * (points[0].Y - points[2].Y) - (points[0].X - points[2].X) * (points[2].Y - e.Y));
            if (tmp1 == tmp2 && tmp1 == tmp3) {
                selected = true;
                return selected;
            }
            if (Control.ModifierKeys != Keys.Control) {
                selected = false;
                return false;
            }
            return false;
        }
    }

    class Group : Figure {
        public Group(Point p1, Point p2) : base(p1, p2) {

            groupStorage = new Storage<Figure>();
            name = "Group";
        }
        public Storage<Figure> groupStorage;
        public override void draw(Graphics g, PictureBox pictureBox1, Bitmap bitmap) {
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next())
                groupStorage.getIterator().draw(g, pictureBox1, bitmap);
        }
        public void selectionIntialize() {//запихнуть в конструктор
            Point maxPoint = new Point(0, 0);
            Point minPoint = new Point(endWindow.X, endWindow.Y);
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                Point tmpPoint = groupStorage.getIterator().rectangle.Location;
                minPoint.X = Math.Min(minPoint.X, tmpPoint.X);
                minPoint.Y = Math.Min(minPoint.Y, tmpPoint.Y);
                maxPoint.X = Math.Max(maxPoint.X, tmpPoint.X);
                maxPoint.Y = Math.Max(maxPoint.Y, tmpPoint.Y);

                tmpPoint = groupStorage.getIterator().getEndRectangle();
                minPoint.X = Math.Min(minPoint.X, tmpPoint.X);
                minPoint.Y = Math.Min(minPoint.Y, tmpPoint.Y);
                maxPoint.X = Math.Max(maxPoint.X, tmpPoint.X);
                maxPoint.Y = Math.Max(maxPoint.Y, tmpPoint.Y);

            }
            startLocation = minPoint;
            endLocation = maxPoint;
            rectangle = new Rectangle(startLocation, new Size(endLocation.X - startLocation.X, endLocation.Y - startLocation.Y));

            previousResizeSize = new PointF(rectangle.Width, rectangle.Height);

            deltaPos = new Point[groupStorage.getSize()];
            int i = 0;
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                deltaPos[i].X = groupStorage.getIterator().rectangle.X - rectangle.X;
                deltaPos[i].Y = groupStorage.getIterator().rectangle.Y - rectangle.Y;
                i++;
            }
        }
        public override bool click(Point e) {
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next())
                if (groupStorage.getIterator().click(e)) {
                    selected = true;
                    return selected;
                }
            selected = false;
            return selected;
        }


        public override void recursiveMovingPointSet(Point p) {
            base.recursiveMovingPointSet(p);

            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next())
                groupStorage.getIterator().recursiveMovingPointSet(p);
        }
        public override bool clickOnArea(Point p) {
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                groupStorage.getIterator().recursiveMovingPointSet(p);
                groupStorage.getIterator().setMoving(true);
            }

            return base.clickOnArea(p);
        }
        public override void movingFigure(Point p) {
            base.movingFigure(p);
            int i = 0;
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                groupStorage.getIterator().movingInGroup(new Point(rectangle.X + deltaPos[i].X, rectangle.Y + deltaPos[i].Y));
                i++;

            }
        }

        private PointF previousResizeSize;
        private PointF percent;
        Point[] deltaPos;
        int i;
        public override void resizeFigure(Point p) {
            base.resizeFigure(p);
            percent = new PointF((rectangle.Width) / previousResizeSize.X, (rectangle.Height) / previousResizeSize.Y);
            i = 0;
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                Point tmpPoint = new Point((int)Math.Round((deltaPos[i].X * percent.X) + rectangle.X), (int)Math.Round((deltaPos[i].Y * percent.Y) + rectangle.Y));
                groupStorage.getIterator().movingInGroup(tmpPoint);

                deltaPos[i].X = groupStorage.getIterator().rectangle.X - rectangle.X;
                deltaPos[i].Y = groupStorage.getIterator().rectangle.Y - rectangle.Y;

                /*groupStorage.getIterator().rectangle.X = (int)Math.Round((deltaPos[i].X * previousResizeSize.X) + rectangle.X);
                groupStorage.getIterator().rectangle.Y = (int)Math.Round((deltaPos[i].Y * previousResizeSize.Y) + rectangle.Y);
*/
                /*                groupStorage.getIterator().rectangle.X = (int)Math.Round((deltaPos[i].X * previousResizeSize.X) + rectangle.X);
                                groupStorage.getIterator().rectangle.Y = (int)Math.Round((deltaPos[i].Y * previousResizeSize.Y) + rectangle.Y);

                                deltaPos[i].X = groupStorage.getIterator().rectangle.X - rectangle.X;
                                deltaPos[i].Y = groupStorage.getIterator().rectangle.Y - rectangle.Y;*/
                i++;


                /*                groupStorage.getIterator().rectangle.Width = (int)Math.Round((groupStorage.getIterator().rectangle.Width) * (previousResizeSize.X));
                                groupStorage.getIterator().rectangle.Height = (int)Math.Round((groupStorage.getIterator().rectangle.Height) * (previousResizeSize.Y));
                */

                Size tmpSize = new Size((int)Math.Round((groupStorage.getIterator().rectangle.Width) * (percent.X)), (int)Math.Round((groupStorage.getIterator().rectangle.Height) * (percent.Y)));
                groupStorage.getIterator().resizeInResizeGroup(tmpSize);
            }
            previousResizeSize = new Point(rectangle.Width, rectangle.Height);
        }
        public override void movingInGroup(Point p) {
            base.movingInGroup(p);
            int j = 0;
            percent = new PointF((rectangle.Width) / previousResizeSize.X, (rectangle.Height) / previousResizeSize.Y);

            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                Point tmpPoint = new Point((int)Math.Round((deltaPos[j].X * percent.X) + rectangle.X), (int)Math.Round((deltaPos[j].Y * percent.Y) + rectangle.Y));
                groupStorage.getIterator().movingInGroup(tmpPoint);
                deltaPos[j].X = groupStorage.getIterator().rectangle.X - rectangle.X;
                deltaPos[j].Y = groupStorage.getIterator().rectangle.Y - rectangle.Y;
                j++;
            }
            previousResizeSize = new Point(rectangle.Width, rectangle.Height);
        }
        public override void resizeInResizeGroup(Size size) {
            base.resizeInResizeGroup(size);

            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next()) {
                Size tmpSize = new Size((int)Math.Round((groupStorage.getIterator().rectangle.Width) * (percent.X)), (int)Math.Round((groupStorage.getIterator().rectangle.Height) * (percent.Y)));
                groupStorage.getIterator().resizeInResizeGroup(tmpSize);
            }

        }
        public override void setColor(Color color) {
            for (groupStorage.first(); !groupStorage.eol(); groupStorage.next())
                groupStorage.getIterator().setColor(color);
        }
    }
}
