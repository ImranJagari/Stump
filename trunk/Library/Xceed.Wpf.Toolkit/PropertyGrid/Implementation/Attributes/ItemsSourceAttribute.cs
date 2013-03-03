﻿/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus edition at http://xceed.com/wpf_toolkit

   Visit http://xceed.com and follow @datagrid on Twitter

  **********************************************************************/

using System;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Attributes
{
  public class ItemsSourceAttribute : Attribute
  {
    public Type Type
    {
      get;
      set;
    }

    public ItemsSourceAttribute( Type type )
    {
      var valueSourceInterface = type.GetInterface( typeof( IItemsSource ).FullName );
      if( valueSourceInterface == null )
        throw new ArgumentException( "Type must implement the IItemsSource interface.", "type" );

      Type = type;
    }
  }
}
