using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Faith
{
    /// <summary>
    /// Anything that requires loading
    /// </summary>
    interface IResource//: IDisposable          TODO: when implementing unloading, use IDisposable w/ IDisposable pattern
    {
        bool Loaded { get; set; }
        void Load(ContentManager content);
    }
}
