using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Faith.Graphics.Groups;
using Faith.Graphics.Shapes;

namespace Faith.Graphics.UI
{
    public class ToolbarSelection : Group
    {
        public string Head;
        public string[] Contents; // if we make this a list, also make an update function

        public Vector2 HeadButtonSize = new Vector2(30, 10);

        Button headbutton;
        SelectionMenu selectionBox;

        public float SelectionHeight = 10f;
        public float MenuWidth;

        public event SelectionHandler OnSelect;

        public bool Active { get; private set; }

        public ToolbarSelection(string head, string[] contents, float menuWidth = 80)
        {
            MenuWidth = menuWidth;
            Head = head;
            Contents = contents;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            Add(headbutton = new Button(Head)
            {
                Position = Position,
                Scale = HeadButtonSize
            });
            headbutton.OnClick += Headbutton_OnClick;

            Add(selectionBox = new SelectionMenu(Contents)
            {
                Position = new Vector2(Position.X, Position.Y + HeadButtonSize.Y),
                Scale = new Vector2(MenuWidth, SelectionHeight),
            });

            selectionBox.OnOutsideClick += SelectionBox_OnOutsideClick;
            selectionBox.OnSelect += SelectionBox_OnSelect;
            selectionBox.Visible = false;
        }

        private void SelectionBox_OnOutsideClick(Vector2 position)
        {
            if (Active)
            {
                if (!headbutton.BoundingBox.Contains(position))
                    SetVisible(false);
            }
        }

        private void SelectionBox_OnSelect(string selection)
        {
            OnSelect?.Invoke(selection);
            SetVisible(false);
        }

        private void Headbutton_OnClick(Vector2 position)
        {
            SetVisible(true);
        }

        public void SetVisible(bool vis)
        {
            Active = vis;
            selectionBox.Visible = vis;
        }
    }

    // TODO:
    public class Toolbar : Group
    {
        public List<ToolbarSelection> Selections;

        public Toolbar()
        {
            Fill = true;

            /*
            Add(new SpriteText("File")
            {
                Position = new Vector2(10, 8),
                Colour = Color.Black
            });
            */
            /*
            Add(file = new ToolbarSelection("File", new string[] { "New", "Open", "Save", "Save As.." }, 130f)
            {
                Position = new Vector2(10, 8),
                HeadButtonSize = new Vector2(50, 20)
            });

            Add(new SpriteText("Edit")
            {
                Position = new Vector2(60, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("View")
            {
                Position = new Vector2(110, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("Objects")
            {
                Position = new Vector2(160, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("Help")
            {
                Position = new Vector2(225, 8),
                Colour = Color.Black
            });
            */
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            foreach (ToolbarSelection sel in Selections)
            {
                Add(sel);
            }
        }
    }
}
