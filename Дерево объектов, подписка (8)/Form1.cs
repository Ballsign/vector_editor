using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace Дерево_объектов__подписка__8_;
public partial class Form1 : Form {
    Graphics g;
    Pen pen;
    Bitmap bitmap;
    private string figure;
    private int currentObject = 0;

    Storage<Figure> figureStorage = new Storage<Figure>();
    AbstractFigureFactory figureFactory;
    Observer observer;
    Tree tree;
    public Form1() {
        InitializeComponent();
        bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        g = Graphics.FromImage(bitmap);
        pen = new Pen(Color.Black, 3);
        Figure.endWindow = new Point(pictureBox1.Width, pictureBox1.Height);

        typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, pictureBox1, new object[] { true });
//        typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, treeView1, new object[] { true });

        figure = Tools.Line;
        btColor.BackColor = Color.Black;

        figureFactory = new ConcreteFigureFactory();
        
        tree = new Tree(figureStorage, treeView1);
        observer = new Observer();
        observer.observer += new MouseEventHandler/*new System.EventHandler*/(tree.updateTreeview);
        figureStorage.addObserver(observer);
    }

    private bool mouseIsPressed = false;

    bool resize = false;
    bool moving = false;
    private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
        mouseIsPressed = true;

        resize = false;
        moving = false;
        currentObject = 0;
        Point p = e.Location;
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
            if (figureStorage.getIterator().resizeBoxCheck(p)) {
                resize = true;
                return;
            }
            currentObject++;
        }
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
            figureStorage.getIterator().recursiveMovingPointSet(p);
        }

        currentObject = 0;
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
            if (figureStorage.getIterator().clickOnArea(p)) {
                moving = true;
                return;

            }
            currentObject++;
        }

        if (!resize && !moving) {
            if (figure == Tools.Cursor) {
                bool flag = false;
                bool click = false;
                for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) 
                    figureStorage.getIterator().click(p);


                for (figureStorage.first(); !figureStorage.eol(); figureStorage.next())
                    if (figureStorage.getIterator().getSelected()) {
                        if (!figureStorage.getIterator().getSticky())
                            flag = true;
                        click = true;
                    }
                if (click)
                    if (flag) chSticky.Checked = false;
                    else chSticky.Checked = true;
                else
                    chSticky.Checked = false;

                return;
            }
            figureStorage.first();
            if (!figureStorage.eol())
                figureStorage.getObject(0).setSelected(false);

            figureStorage.push_front(figureFactory.createFigure(figure, p, p));

            //if (figure != Tools.Cursor) {
            figureStorage.getObject(0).setColor(pen.Color);

            chSticky.Checked = false;            
        }
    }
    private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
        if (!mouseIsPressed) return;

        Point p = new Point(e.X, e.Y);
        if (resize) {
            figureStorage.getObject(currentObject).resizeFigure(p);
        }

        if (moving) {

            Figure currentFigure = figureStorage.getObject(currentObject);
            currentFigure.movingFigure(p);

            if (currentFigure.getSticky()) {
                for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
                    if ((figureStorage.getIterator() != currentFigure) && !figureStorage.getIterator().getSticked())
                        if (figureStorage.getIterator().stickyIntersection(currentFigure)) {
                            Observer o = new Observer();
                            o.observer += new MouseEventHandler(figureStorage.getIterator().movingHandler);
                            currentFigure.addObserver(o);
                            figureStorage.getIterator().movingPoint = currentFigure.movingPoint;
                            figureStorage.getIterator().setMoving(true);
                        }
                }
                currentFigure.notifyEveryone(e);
            }
        }

        if (!resize && !moving)
            if (figure != Tools.Cursor)
                figureStorage.getObject(0).setEndLocation(new Point(e.X, e.Y));

    }
    private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
        mouseIsPressed = false;

        figureStorage.first();
        if (!figureStorage.eol())
            if (figureStorage.getObject(0).isSlim()) {
                figureStorage.remove(0);
            }
        Debug.WriteLine("end");

        tree.updateTreeview(sender, e);
    }
    private void pictureBox1_Paint(object sender, PaintEventArgs e) {
        g.Clear(Color.White);
        pictureBox1.Image = bitmap;
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
            figureStorage.getIterator().draw(g, pictureBox1, bitmap);
            if (figureStorage.getIterator().getSelected() && ((Control.MouseButtons & MouseButtons.Left) == 0)) {
                figureStorage.getIterator().drawSelection(g, pictureBox1, bitmap);
                figureStorage.getIterator().setColor(pen.Color);
            }
        }
    }
    private void Form1_ResizeEnd(object sender, EventArgs e) {
        bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        g = Graphics.FromImage(bitmap);

        figureStorage.first();
        if (!figureStorage.eol())
            figureStorage.getObject(0).setWindow(new Point(pictureBox1.Width, pictureBox1.Height));
    }
    private void Form1_KeyDown(object sender, KeyEventArgs e) {
        if (e.KeyCode == Keys.Delete) {
            for (figureStorage.first(); !figureStorage.eol(); figureStorage.next())
                if (figureStorage.getIterator().getSelected())
                    figureStorage.remove();
            pictureBox1.Invalidate();
        }
    }
    private void btCircle_Click(object sender, EventArgs e) {
        removeSelected();
        chSticky.Checked = false;
        figure = Tools.Circle;
    }

    private void btLine_Click(object sender, EventArgs e) {
        removeSelected();
        chSticky.Checked = false;
        figure = Tools.Line;
    }

    private void btRectangle_Click(object sender, EventArgs e) {
        removeSelected();
        chSticky.Checked = false;
        figure = Tools.Rrectangle;

    }

    private void btTriangle_Click(object sender, EventArgs e) {
        removeSelected();
        chSticky.Checked = false;
        figure = Tools.Triangle;
    }

    private void btColor_Click(object sender, EventArgs e) {

        if (colorDialog1.ShowDialog() == DialogResult.OK) {
            pen.Color = colorDialog1.Color;
            btColor.BackColor = pen.Color;
            for (figureStorage.first(); !figureStorage.eol(); figureStorage.next())
                if (figureStorage.getIterator().getSelected())
                    figureStorage.getIterator().setColor(pen.Color);
        }

    }

    private void btCursor_Click(object sender, EventArgs e) {
        removeSelected();
        chSticky.Checked = false;
        figure = Tools.Cursor;
    }
    private void removeSelected() {
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next())
            figureStorage.getIterator().setSelected(false);

        tree.updateTreeview(new object(), new EventArgs());
    }
    private void btSave_Click(object sender, EventArgs e) {
        //figureStorage.save();
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            figureStorage.save(saveFileDialog1.FileName);
    }
    private void btLoad_Click(object sender, EventArgs e) {
        if (openFileDialog1.ShowDialog() == DialogResult.OK) {
            figureStorage = figureFactory.load(openFileDialog1.FileName);
            figureStorage.addObserver(observer);
            tree = new Tree(figureStorage, treeView1);
            pictureBox1.Invalidate();
        }
        tree.updateTreeview(sender, e);
    }

    private void btGroup_Click(object sender, EventArgs e) {
        Group group = new Group(new Point(0, 0), new Point(0, 0));
        for (figureStorage.first();!figureStorage.eol(); figureStorage.next())
            if (figureStorage.getIterator().getSelected()) {
                group.groupStorage.push_front(figureStorage.getIterator());
                figureStorage.remove();
            }
        group.selectionIntialize();
        figureStorage.push_front(group);

    }
    private void btUngroup_Click(object sender, EventArgs e) {
        Storage<Group> group = new Storage<Group>();
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next()) {
            if (figureStorage.getIterator().getSelected() == true) {
                if (figureStorage.getIterator() as Group != null) {
                    group.push_front(figureStorage.getIterator() as Group);

                    figureStorage.remove();
                }
            }
        }

        for (group.first(); !group.eol(); group.next()) {
            for (group.getIterator().groupStorage.first(); !group.getIterator().groupStorage.eol(); group.getIterator().groupStorage.next()) {
                figureStorage.push_front(group.getIterator().groupStorage.getIterator());
                figureStorage.getObject(0).setSelected(true);
                group.getIterator().groupStorage.remove();
            }
            group.remove();
        }
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e) {
        tree.tree_select(treeView1.SelectedNode);
        tree.updateTreeview(sender, e);
    }

    private void Form1_Load(object sender, EventArgs e) {
    }
    private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {

    }

    private void chSticky_CheckedChanged(object sender, EventArgs e) {

    }

    private void chSticky_Click(object sender, EventArgs e) {
        for (figureStorage.first(); !figureStorage.eol(); figureStorage.next())
            if (figureStorage.getIterator().getSelected())
                figureStorage.getIterator().setSticky(chSticky.Checked);
    }
}