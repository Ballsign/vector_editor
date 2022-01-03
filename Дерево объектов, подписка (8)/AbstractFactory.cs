using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дерево_объектов__подписка__8_;
abstract class AbstractFigureFactory {
    public abstract Figure createFigure(String figure, Point p1, Point p2);
    public abstract Storage<Figure> load(string path);

}

class ConcreteFigureFactory : AbstractFigureFactory {
    public override Figure createFigure(String figure, Point p1, Point p2) {
        switch (figure) {
            case "Line":
                return new Line(p1, p2);
            case "Circle":
                return new Circle(p1, p2);
            case "Rectangle":
                return new RRectangle(p1, p2);
            case "Triangle":
                return new Triangle(p1, p2);
            default:
                return null;
        }
    }

    public override Storage<Figure> load(string path) {
        Storage<Figure> tmp = new Storage<Figure>();
        try {
            using (StreamReader sr = new StreamReader(path))
                while (!sr.EndOfStream) {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(" ; ");

                    string figure = parts[0];
                    Point p1 = new Point(int.Parse(parts[1]), int.Parse(parts[2]));
                    Point p2 = new Point(int.Parse(parts[3]), int.Parse(parts[4]));
                    Color color = Color.FromArgb(int.Parse(parts[5]));//Color.Black;//Color.FromArgb(int.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]), int.Parse(parts[8]));
                    tmp.push_front(createFigure(figure, p1, p2));
                    tmp.getObject(0).setColor(color);
                    tmp.getObject(0).setSelected(false);

                    Debug.WriteLine(color);
                    //Debug.WriteLine("load\n " + figure + " ; " + p1 + " ; " + p2 + " ; ");
                }
        }
        catch { }
        return tmp;
    }
}



