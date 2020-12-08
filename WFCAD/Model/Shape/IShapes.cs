﻿using System.Collections.Generic;
using System.Drawing;

namespace WFCAD {
    /// <summary>
    /// 図形群を表すインターフェース
    /// </summary>
    public interface IShapes {

        #region プロパティ

        /// <summary>
        /// 表示状態
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// クリップボード
        /// </summary>
        List<IShape> Clipboard { get; set; }

        #endregion プロパティ

        #region　メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        Bitmap Draw(Bitmap vBitmap);

        /// <summary>
        /// 選択します
        /// </summary>
        void Select(Point vCoordinate, bool vIsMultiple);

        /// <summary>
        /// 追加します
        /// </summary>
        void Add(IShape vShape);

        /// <summary>
        /// 移動します
        /// </summary>
        void Move(Size vSize);

        /// <summary>
        /// 最前面に移動します
        /// </summary>
        void MoveToFront();

        /// <summary>
        /// 最背面に移動します
        /// </summary>
        void MoveToBack();

        /// <summary>
        /// 複製します
        /// </summary>
        void Clone();

        /// <summary>
        /// コピーします
        /// </summary>
        void Copy(bool vIsCut = false);

        /// <summary>
        /// 貼り付けます
        /// </summary>
        void Paste();

        /// <summary>
        /// 削除します
        /// </summary>
        void Remove();

        /// <summary>
        /// クリアします
        /// </summary>
        void Clear();

        /// <summary>
        /// 自身のインスタンスを複製します
        /// </summary>
        IShapes DeepClone();

        #endregion　メソッド

    }
}
