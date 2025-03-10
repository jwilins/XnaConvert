﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Forms;
using Alba.Framework.IO;
using Alba.XnaConvert.Common;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Alba.XnaConvert.Loader.Xna10
{
    [Export (typeof(IContentService))]
    [ExportContentService ("XNA", "1.0")]
    [ExportContentService ("XNA", "1", false)]
    public class ContentService : ContentManager, IContentService
    {
        public ContentService ()
            : base(new GraphicsService())
        {}

        public IAsset LoadTexture2D (string filename)
        {
            return new Texture2DAsset(Load<Texture2D>(filename));
        }

        protected override Stream OpenStream (string assetName)
        {
            return Streams.ReadFile(assetName);
        }

        private class GraphicsService : SelfServiceProvider, IGraphicsDeviceService
        {
            private readonly Form _form;

            public GraphicsDevice GraphicsDevice { get; private set; }

            public event EventHandler DeviceDisposing;
            public event EventHandler DeviceReset;
            public event EventHandler DeviceResetting;
            public event EventHandler DeviceCreated;

            public GraphicsService ()
            {
                _form = new Form();
                GraphicsDevice = new GraphicsDevice(
                    GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, _form.Handle,
                    CreateOptions.HardwareVertexProcessing,
                    new PresentationParameters { IsFullScreen = false });
            }
        }
    }
}