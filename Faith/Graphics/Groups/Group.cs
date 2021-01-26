using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Faith.Physics;

namespace Faith.Graphics.Groups
{
    /// <summary>
    /// Collections of <see cref="Drawable"/> objects
    /// Drawing and loading is handled automatically
    /// </summary>
    public class Group : Drawable, ICollection<Drawable>
    {
        public List<Drawable> Children { get; private set; }
        
        public int Count => ((ICollection<Drawable>)Children).Count;
        public bool IsReadOnly => ((ICollection<Drawable>)Children).IsReadOnly;

        private ContentManager content;
        public Group()
        {
            Children = new List<Drawable>();
        }

        public override void Load(ContentManager content)
        {
            this.content = content;

            foreach (Drawable child in Children)
            {
                // only load children that haven't been loaded already
                if (!child.Loaded)
                    child.Load(content);
            }

            base.Load(content);
        }

        public override void Update(GameTime time)
        {
            Drawable[] c = Children.ToArray();

            foreach (Drawable child in c)
            {
                if (child.Enabled)
                {
                    // add others
                    // !! probably very inefficient
                    if (child is ICollidableBody)
                    {
                        ICollidableBody pChild = child as ICollidableBody;
                        IEnumerable <ICollidableBody> others = 
                            from other in c
                            where other is ICollidableBody
                            select other as ICollidableBody;

                        pChild.Others = others.ToList();
                    }

                    child.Update(time);
                }
            }
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);
            Drawable[] c = Children.ToArray();

            foreach (Drawable child in c)
            {
                if (child.Enabled && child.Visible)
                    child.Draw(s);
            }
        }

        #region ICollection implementation
        public void Add(Drawable item)
        {
            if (Loaded && !item.Loaded)
                item.Load(content); 

            ((ICollection<Drawable>)Children).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Drawable>)Children).Clear();
        }

        public bool Contains(Drawable item)
        {
            return ((ICollection<Drawable>)Children).Contains(item);
        }

        public void CopyTo(Drawable[] array, int arrayIndex)
        {
            ((ICollection<Drawable>)Children).CopyTo(array, arrayIndex);
        }

        public bool Remove(Drawable item)
        {
            return ((ICollection<Drawable>)Children).Remove(item);
        }

        public IEnumerator<Drawable> GetEnumerator()
        {
            return ((ICollection<Drawable>)Children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<Drawable>)Children).GetEnumerator();
        }
        #endregion
    }
}
