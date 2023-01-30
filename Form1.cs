namespace MemoryGame
{
  public partial class MainForm : Form
  {

    //randomizer to randomly populate the grid
    Random random = new Random();

    TableLayoutPanel panel = new TableLayoutPanel();
    //to determine the grid size
    int gridSize= 0;

    //score and pair counter
    int score = 0;
    int pairs = 0;

    //list of icons for 6x6 grid
    List<string> IconList = new List<string>()
      {
        "a", "a", "b", "b","c", "c", "d", "d","e", "e","f", "f", "x", "x","y", "y", "u", "u","i", "i","o", "o", "p", "p", "w", "w", "q", "q", "e", "e", "r", "r", "s", "s", "h", "h"
      };

    //storing the 1st and 2nd "icon"
    Label FirstClick, SecondClick;

    public MainForm()
    {
      InitializeComponent();
      CreateGrid();
      PopulateGrid();
      this.Width = gridSize * 123;
      this.Height = gridSize * 123;
      this.AutoSize = false;
    }
    void CreateGrid()
    {
      //Random random2 = new Random();

      //List<string> listGridSize = SixPairs;
      gridSize = random.Next(1,4);

      //panel properties
      panel.Name = "TableLayoutPanel1";
      panel.Size = new System.Drawing.Size(228, 200);
      panel.TabIndex = 0;
      panel.Visible = true;
      panel.CellBorderStyle= TableLayoutPanelCellBorderStyle.Inset;
      panel.Dock = DockStyle.Fill;
      switch (gridSize)
      {  
        case 1:
          //2x2 Grid creation
          IconList.RemoveRange(4, 32);
          panel.RowCount = 2;
          panel.ColumnCount = 2;
          for (int i = 0; i < panel.RowCount; i++)
          {
            for (int j = 0; j < panel.ColumnCount; j++)
            {
              panel.Controls.Add(new Label(), i, j);
            }
          }
          break;
        case 2:
          //4x4 Grid creation
          IconList.RemoveRange(16, 20);
          panel.RowCount = 4;
          panel.ColumnCount = 4;
          for (int i = 0; i < panel.RowCount; i++)
          {
            for (int j = 0; j < panel.ColumnCount; j++)
            {
              panel.Controls.Add(new Label(), i, j);
            }
          }
          break;
        case 3:
          //6x6 Grid creation
          panel.RowCount = 6;
          panel.ColumnCount = 6;
          for (int i = 0; i < panel.RowCount; i++)
          {
            for (int j = 0; j < panel.ColumnCount; j++)
            {
              panel.Controls.Add(new Label(), i, j);
            }
          }
          
          break;
      }
      LabelStyles(panel.Controls);
      Controls.Add(panel);
    }
    //timer to let the 2nd choise be visible if dont match
    private void timer_round(object sender, EventArgs e)
    {
      icon_timer.Stop();
      FirstClick.ForeColor= FirstClick.BackColor;
      SecondClick.ForeColor= SecondClick.BackColor;
      FirstClick = null;
      SecondClick = null;
      


    }
    //filling the labels with a random icon
    void PopulateGrid()
      {
        int randomPossition;
        Label label;
        for (int i = 0; i < panel.Controls.Count; i++)
        {
          label = (Label)panel.Controls[i];
          randomPossition = random.Next(0, IconList.Count);
          label.Text = IconList[randomPossition];
          IconList.RemoveAt(randomPossition);
        }

      }
    //set the style for all labels after grid creation
    private void LabelStyles(TableLayoutPanel.ControlCollection LabelList)
    {
      
      //List<Label> labelsStyle = new List<Label>();
     
        foreach (Label l in LabelList)
        {
          l.Width = 50;
          l.Height = 50;
          l.ForeColor = Color.Maroon;
          l.Font = new Font("Webdings", 24, FontStyle.Bold);
          l.Dock = DockStyle.Fill;
          l.BackColor = Color.Maroon;
          l.Click += new EventHandler(iconClick);
          
        }
      
    }
    //event for label click
    protected void iconClick(object sender, EventArgs e)
    {
      Label ClickedIcon = (Label)sender;
      //check if its null, avoid errors
      if (ClickedIcon == null)
      {
        return;
      }
      if (FirstClick != null && SecondClick != null)
      {
        return;
      }
      //check if it's already pressed
      if (ClickedIcon.ForeColor == Color.Black)
      {
        return;
      }
      //check if its 1st click
      if (FirstClick == null)
      {
        FirstClick = ClickedIcon;
        FirstClick.ForeColor = Color.Black;
        return;
      }
      ////check if its 2nd click
      //if (ClickedIcon.ForeColor == Color.Maroon && FirstClick != null)
      //{  }
      SecondClick = ClickedIcon;
      SecondClick.ForeColor = Color.Black;


      //check if they match, if they match, turn them green
      if (FirstClick.Text == SecondClick.Text)
      {
        FirstClick.BackColor = Color.Green;
        SecondClick.BackColor = Color.Green;
        FirstClick = null;
        SecondClick = null;

        score += 5;
        pairs++;

      }
      //if they dont match, turn them back red
      else
      {
        //FirstClick.ForeColor = Color.Maroon;
        //SecondClick.ForeColor = Color.Maroon;
        //FirstClick = null;
        //SecondClick = null;
        icon_timer.Start();
        score--;
      }

      if (pairs == (2*(gridSize * gridSize)))
      {

        Message Show("Hey! You win!! With the score of " + score);

        
      }


    }

  }
}
