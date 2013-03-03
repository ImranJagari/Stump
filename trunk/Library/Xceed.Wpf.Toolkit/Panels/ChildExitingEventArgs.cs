﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System.Windows;

namespace Xceed.Wpf.Toolkit.Panels
{
  public class ChildExitingEventArgs : RoutedEventArgs
  {
    #region Constructors

    public ChildExitingEventArgs( UIElement child, Rect? exitTo, Rect arrangeRect )
    {
      _child = child;
      _exitTo = exitTo;
      _arrangeRect = arrangeRect;
    }

    #endregion

    #region ArrangeRect Property

    public Rect ArrangeRect
    {
      get
      {
        return _arrangeRect;
      }
    }

    private readonly Rect _arrangeRect;

    #endregion

    #region Child Property

    public UIElement Child
    {
      get
      {
        return _child;
      }
    }

    private readonly UIElement _child;

    #endregion

    #region ExitTo Property

    public Rect? ExitTo
    {
      get
      {
        return _exitTo;
      }
      set
      {
        _exitTo = value;
      }
    }

    private Rect? _exitTo; //null

    #endregion
  }
}
