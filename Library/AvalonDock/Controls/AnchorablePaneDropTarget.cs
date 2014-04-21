﻿//Copyright (c) 2007-2012, Adolfo Marinucci
//All rights reserved.

//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
//following conditions are met:

//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following 
//disclaimer in the documentation and/or other materials provided with the distribution.

//* Neither the name of Adolfo Marinucci nor the names of its contributors may be used to endorse or promote products
//derived from this software without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
//STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
//EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using AvalonDock.Layout;

namespace AvalonDock.Controls
{
    internal class AnchorablePaneDropTarget : DropTarget<LayoutAnchorablePaneControl>
    {
        internal AnchorablePaneDropTarget(LayoutAnchorablePaneControl paneControl, Rect detectionRect, DropTargetType type)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
        }

        internal AnchorablePaneDropTarget(LayoutAnchorablePaneControl paneControl, Rect detectionRect, DropTargetType type, int tabIndex)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
            _tabIndex = tabIndex;
        }


        LayoutAnchorablePaneControl _targetPane;

        int _tabIndex = -1;

        protected override void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
            ILayoutAnchorablePane targetModel = _targetPane.Model as ILayoutAnchorablePane;
            LayoutAnchorable anchorableActive = floatingWindow.Descendents().OfType<LayoutAnchorable>().FirstOrDefault();
         
            switch (Type)
            {
                case DropTargetType.AnchorablePaneDockBottom:
                    #region DropTargetType.AnchorablePaneDockBottom
                    {
                        var parentModel = targetModel.Parent as ILayoutGroup;
                        var parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Vertical &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Vertical;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Vertical)
                        {
                            var layoutAnchorablePaneGroup = floatingWindow.RootPanel as LayoutAnchorablePaneGroup;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Vertical))
                            {
                                var anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + 1 + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex + 1, floatingWindow.RootPanel);
                        }
                        else
                        {
                            var targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            var newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Vertical,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Add(floatingWindow.RootPanel);

                        }
                    }
                    break;
                    #endregion
                case DropTargetType.AnchorablePaneDockTop:
                    #region DropTargetType.AnchorablePaneDockTop
                    {
                        var parentModel = targetModel.Parent as ILayoutGroup;
                        var parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Vertical &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Vertical;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Vertical)
                        {
                            var layoutAnchorablePaneGroup = floatingWindow.RootPanel as LayoutAnchorablePaneGroup;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Vertical))
                            {
                                var anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex, floatingWindow.RootPanel);
                        }
                        else
                        {
                            var targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            var newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Vertical,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            //the floating window must be added after the target modal as it could be raise a CollectGarbage call
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Insert(0, floatingWindow.RootPanel);

                        }
                    }
                    break;
                    #endregion
                case DropTargetType.AnchorablePaneDockLeft:
                    #region DropTargetType.AnchorablePaneDockLeft
                    {
                        var parentModel = targetModel.Parent as ILayoutGroup;
                        var parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Horizontal &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Horizontal;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Horizontal)
                        {
                            var layoutAnchorablePaneGroup = floatingWindow.RootPanel as LayoutAnchorablePaneGroup;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Horizontal))
                            {
                                var anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex, floatingWindow.RootPanel);
                        }
                        else
                        {
                            var targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            var newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Horizontal,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            //the floating window must be added after the target modal as it could be raise a CollectGarbage call
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Insert(0, floatingWindow.RootPanel);

                        }
                    }
                    break;
                    #endregion
                case DropTargetType.AnchorablePaneDockRight:
                    #region DropTargetType.AnchorablePaneDockRight
                    {
                        var parentModel = targetModel.Parent as ILayoutGroup;
                        var parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Horizontal &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Horizontal;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Horizontal)
                        {
                            var layoutAnchorablePaneGroup = floatingWindow.RootPanel as LayoutAnchorablePaneGroup;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Horizontal))
                            {
                                var anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + 1 + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex + 1, floatingWindow.RootPanel);
                        }
                        else
                        {
                            var targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            var newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Horizontal,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Add(floatingWindow.RootPanel);

                        }
                    }
                    break;
                    #endregion


                case DropTargetType.AnchorablePaneDockInside:
                    #region DropTargetType.AnchorablePaneDockInside
                    {
                        var paneModel = targetModel as LayoutAnchorablePane;
                        var layoutAnchorablePaneGroup = floatingWindow.RootPanel as LayoutAnchorablePaneGroup;

                        int i = _tabIndex == -1 ? 0 : _tabIndex;
                        foreach (var anchorableToImport in 
                            layoutAnchorablePaneGroup.Descendents().OfType<LayoutAnchorable>().ToArray())
                        {
                            paneModel.Children.Insert(i, anchorableToImport);
                            i++;
                        }

                    }
                    break;
                    #endregion


            }

            anchorableActive.IsActive = true;

            base.Drop(floatingWindow);
        }

        public override System.Windows.Media.Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindowModel)
        {
            //var anchorablePaneDropTarget = target as AnchorablePaneDropTarget;
            var anchorableFloatingWindowModel = floatingWindowModel as LayoutAnchorableFloatingWindow;
            var layoutAnchorablePane = anchorableFloatingWindowModel.RootPanel as ILayoutPositionableElement;
            var layoutAnchorablePaneWithActualSize = anchorableFloatingWindowModel.RootPanel as ILayoutPositionableElementWithActualSize;

            switch (Type)
            {
                case DropTargetType.AnchorablePaneDockBottom:
                    {
                        var targetScreenRect = TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Offset(0.0, targetScreenRect.Height / 2.0);
                        targetScreenRect.Height /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.AnchorablePaneDockTop:
                    {
                        var targetScreenRect = TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Height /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.AnchorablePaneDockLeft:
                    {
                        var targetScreenRect = TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Width /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.AnchorablePaneDockRight:
                    {
                        var targetScreenRect = TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Offset(targetScreenRect.Width / 2.0, 0.0);
                        targetScreenRect.Width /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }
                case DropTargetType.AnchorablePaneDockInside:
                    {
                        var targetScreenRect = TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        if (_tabIndex == -1)
                        {
                            return new RectangleGeometry(targetScreenRect);
                        }
                        else
                        {
                            var translatedDetectionRect = new Rect(DetectionRects[0].TopLeft, DetectionRects[0].BottomRight);
                            translatedDetectionRect.Offset(-overlayWindow.Left, -overlayWindow.Top);
                            var pathFigure = new PathFigure();
                            pathFigure.StartPoint = targetScreenRect.TopLeft;
                            pathFigure.Segments.Add(new LineSegment() { Point = new Point(targetScreenRect.Left, translatedDetectionRect.Top) });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.TopLeft });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.BottomLeft });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.BottomRight });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.TopRight });
                            pathFigure.Segments.Add(new LineSegment() { Point = new Point(targetScreenRect.Right, translatedDetectionRect.Top) });
                            pathFigure.Segments.Add(new LineSegment() { Point = targetScreenRect.TopRight });
                            pathFigure.IsClosed = true;
                            pathFigure.IsFilled = true;
                            pathFigure.Freeze();
                            return new PathGeometry(new PathFigure[] { pathFigure });
                        }
                    }
            }

            return null;
        }
    }
}