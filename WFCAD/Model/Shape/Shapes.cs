﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WFCAD {
    /// <summary>
    /// 図形群クラス
    /// </summary>
    public class Shapes : IShapes {

        #region 定数

        private static readonly Size C_DefaultMovingSize = new Size(10, 10);

        #endregion 定数

        #region フィールド

        private List<IShape> FShapes = new List<IShape>();
        private List<IShape> FClipBoard = new List<IShape>();
        private bool FVisible = true;

        #endregion フィールド

        #region プロパティ

        /// <summary>
        /// 表示状態
        /// </summary>
        public bool Visible {
            get => FVisible;
            set {
                FVisible = value;
                foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                    wShape.Visible = FVisible;
                }
            }
        }

        #endregion プロパティ

        #region メソッド

        /// <summary>
        /// 描画します
        /// </summary>
        public Bitmap Draw(Bitmap vBitmap) {
            FShapes.ForEach(x => x.Draw(vBitmap));
            return vBitmap;
        }

        /// <summary>
        /// 選択します
        /// </summary>
        public void Select(Point vCoordinate, bool vIsMultiple) {
            if (FShapes.Where(x => x.IsSelected).ToList().Count >= 2) {
                if (FShapes.Any(x => x.IsHit(vCoordinate))) {
                    foreach (IShape wShape in FShapes) {
                        bool wIsHit = wShape.IsHit(vCoordinate);
                        wShape.IsSelected = wShape.IsSelected || wIsHit;
                    }
                } else {
                    FShapes.ForEach(x => x.IsSelected = false);
                }
            } else {
                bool wHasSelected = false;
                foreach (IShape wShape in Enumerable.Reverse(FShapes)) {
                    if (wHasSelected) {
                        wShape.IsSelected = false;
                    } else {
                        bool wIsHit = wShape.IsHit(vCoordinate);
                        if (vIsMultiple) {
                            wShape.IsSelected = wShape.IsSelected || wIsHit;
                        } else {
                            wShape.IsSelected = wIsHit;
                            if (wShape.IsSelected) {
                                wHasSelected = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 追加します
        /// </summary>
        public void Add(IShape vShape) => FShapes.Add(vShape);

        /// <summary>
        /// 移動します
        /// </summary>
        public void Move(Size vSize) {
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                wShape.Move(vSize);
            }
        }

        /// <summary>
        /// 最前面に移動します
        /// </summary>
        public void MoveToFront() => FShapes = FShapes.OrderBy(x => x.IsSelected).ToList();

        /// <summary>
        /// 最背面に移動します
        /// </summary>
        public void MoveToBack() => FShapes = FShapes.OrderByDescending(x => x.IsSelected).ToList();

        /// <summary>
        /// 複製します
        /// </summary>
        public void Clone() {
            var wClonedShapes = new List<IShape>();
            foreach (IShape wShape in FShapes.Where(x => x.IsSelected)) {
                IShape wClone = wShape.DeepClone();

                // 選択状態をスイッチします
                wShape.IsSelected = false;
                wClone.IsSelected = true;

                wClone.Move(C_DefaultMovingSize);
                wClonedShapes.Add(wClone);
            }
            FShapes.AddRange(wClonedShapes);
        }

        /// <summary>
        /// クリップボードにコピーします
        /// </summary>
        public void Copy() {
            var wSelectedShapes = FShapes.Where(x => x.IsSelected).ToList();
            if (wSelectedShapes.Count == 0) return;

            FClipBoard = new List<IShape>();
            foreach (IShape wShape in wSelectedShapes) {
                IShape wCopy = wShape.DeepClone();

                // 選択状態にしておく
                wCopy.IsSelected = true;

                wCopy.Move(C_DefaultMovingSize);
                FClipBoard.Add(wCopy);
            }
        }

        /// <summary>
        /// 貼り付けます
        /// </summary>
        public void Paste() {
            FShapes.ForEach(x => x.IsSelected = false);
            FShapes.AddRange(FClipBoard.Select(x => x.DeepClone()));

            // 貼り付け位置を更新しておく
            FClipBoard.ForEach(x => x.Move(C_DefaultMovingSize));
        }

        /// <summary>
        /// 削除します
        /// </summary>
        public void Remove() => FShapes.RemoveAll(x => x.IsSelected);

        /// <summary>
        /// クリアします
        /// </summary>
        public void Clear() => FShapes.Clear();

        /// <summary>
        /// 自身のインスタンスを複製します
        /// </summary>
        public IShapes DeepClone() {
            var wClone = new Shapes();
            foreach (IShape wShape in FShapes) {
                wClone.Add(wShape.DeepClone());
            }
            return wClone;
        }

        #endregion メソッド

    }
}
