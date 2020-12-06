﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFCAD {
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Rectangle : Shape {
        private System.Drawing.Rectangle FFrameRectangle;

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public override Bitmap Draw(Bitmap vBitmap) {
            using (var wGraphics = Graphics.FromImage(vBitmap)) {
                FFrameRectangle = new System.Drawing.Rectangle(Math.Min(this.StartPoint.X, this.EndPoint.X), Math.Min(this.StartPoint.Y, this.EndPoint.Y), this.Width, this.Height);
                wGraphics.DrawRectangle(this.Option, FFrameRectangle);
            }
            return vBitmap;
        }

        /// <summary>
        /// 指定した座標が図形内に存在するか
        /// </summary>
        public override bool IsHit(Point vMouseLocation) {
            using (var wPath = new GraphicsPath()) {
                wPath.AddRectangle(FFrameRectangle);
                return wPath.IsVisible(vMouseLocation.X, vMouseLocation.Y);
            }
        }

        /// <summary>
        /// 自身のインスタンスを返します
        /// </summary>
        protected override IShape DeepCloneCore() => new Rectangle();

        #endregion　メソッド

    }
}