using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{

    public partial class Pexeso : Form
    {
        TableLayoutPanel grid = new TableLayoutPanel();
        List<Button> buttons = new List<Button>();

        enum states { allDown, oneUp, twoUp };
        states currentState = states.allDown;

        Button flippedFirst;
        Button flippedSecond;
        bool flippedSame = false;

        int clickCount;
        int flipCount;

        int currentFieldSize;
        bool alreadyMixed;

        List<int> remainingButtons;

        public Pexeso()
        {
            InitializeComponent();
        }

        void initializeField(int size, bool resize)
        {
            currentFieldSize = size;
            MyRandom random = new MyRandom(size);

            remainingButtons = new List<int>();
            for (int i = 0; i < size * size / 2; i++)
            {
                remainingButtons.Add(i);
            }

            grid.Parent = null;
            grid = new TableLayoutPanel();

            if (resize)
            {
                this.Width = size * 50;
                this.Height = size * 50;
            }

            grid.Name = "grid";
            grid.ColumnCount = size;
            grid.RowCount = size;
            grid.Dock = DockStyle.Fill;
            grid.Parent = this;

            for (int i = 0; i < size; i++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / size));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / size));
            }

            grid.Location = new System.Drawing.Point(0, 0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button b = new Button();
                    b.Name = i.ToString();
                    b.Dock = DockStyle.Fill;
                    b.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Click += new System.EventHandler(this.flip_Click);

                    b.Tag = random.GetRandom();
                    //b.Text = ((int)b.Tag).ToString();

                    grid.Controls.Add(b, i, j);
                    buttons.Add(b);
                }
            }
        }

        int getFieldSize()
        {
            foreach (var control in groupSize.Controls)
            {
                RadioButton radio = control as RadioButton;

                if (radio != null && radio.Checked)
                {
                    return int.Parse((string)radio.Tag);
                }
            }
            return 10;
        }

        private void flip_Click(object sender, EventArgs e)
        {
            clickCount++;

            switch (currentState)
            {
                case states.allDown:
                    currentState = states.oneUp;
                    flipFirst(sender);

                    break;
                case states.oneUp:
                    if ((Button)sender == flippedFirst)
                        return;

                    flipCount++;
                    currentState = states.twoUp;
                    flippedSame = flipSecond(sender);

                    if (remainingButtons.Count == 0)
                        showScore();
                    // divide only by two, because it is like under sqrt
                    else if (!alreadyMixed && zamichat.Checked && remainingButtons.Count == currentFieldSize / 2)
                    {
                        initializeField(currentFieldSize / 2, false);
                        alreadyMixed = true;
                    }

                    break;
                case states.twoUp:
                    unFlipBoth();

                    if ((Button)sender == flippedSecond || (Button)sender == flippedFirst)
                    {
                        currentState = states.allDown;
                        return;
                    }

                    currentState = states.oneUp;
                    flipFirst(sender);

                    break;
            }
        }

        void flipFirst(object sender)
        {
            flippedFirst = ((Button)sender);
            flippedFirst.BackColor = Color.Aqua;
            flippedFirst.Text = ((int)flippedFirst.Tag).ToString();
        }

        bool flipSecond(object sender)
        {
            flippedSecond = ((Button)sender);
            flippedSecond.Text = ((int)flippedSecond.Tag).ToString();

            bool same = (int)flippedFirst.Tag == (int)flippedSecond.Tag;

            if (same)
            {
                remainingButtons.Remove((int)flippedFirst.Tag);
            }

            Color highlight = same ? Color.Green : Color.Red;
            flippedFirst.BackColor = highlight;
            flippedSecond.BackColor = highlight;

            return same;
        }

        void unFlipBoth()
        {
            flippedFirst.BackColor = SystemColors.Control;
            flippedSecond.BackColor = SystemColors.Control;

            flippedFirst.Text = "";
            flippedSecond.Text = "";

            if (!flippedSame)
                return;

            flippedFirst.Hide();
            flippedSecond.Hide();
        }

        void showScore()
        {
            this.Width = 300;
            this.Height = 300;

            grid.Parent = null;

            flipCountLabel.Text = flipCount.ToString();
            clickCountLabel.Text = clickCount.ToString();

            win.Show();
        }

        private void start_Click(object sender, EventArgs e)
        {
            pinit.Hide();
            win.Hide();

            initializeField(getFieldSize(), true);

            clickCount = 0;
            flipCount = 0;
            alreadyMixed = false;

            currentState = states.allDown;
        }

        private void again_Click(object sender, EventArgs e)
        {
            win.Hide();
            pinit.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }

    class MyRandom
    {

        List<int> randomList = new List<int>();
        Random random;

        public MyRandom(int size)
        {
            random = new Random();

            for (int i = 0; i < size * size / 2; i++)
            {
                randomList.Add(i);
                randomList.Add(i);
            }
        }

        public int GetRandom()
        {
            int index = random.Next(randomList.Count - 1);
            int value = randomList[index];

            randomList.RemoveAt(index);

            return value;
        }

    }

}
