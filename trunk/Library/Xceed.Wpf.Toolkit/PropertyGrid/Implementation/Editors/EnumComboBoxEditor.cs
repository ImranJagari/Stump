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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
  public class EnumComboBoxEditor : ComboBoxEditor
  {
    protected override IList<object> CreateItemsSource( PropertyItem propertyItem )
    {
      return GetValues( propertyItem.PropertyType );
    }

    private static object[] GetValues( Type enumType )
    {
      List<object> values = new List<object>();

      if( enumType != null )
      {
        var fields = enumType.GetFields().Where( x => x.IsLiteral );
        foreach( FieldInfo field in fields )
        {
          // Get array of BrowsableAttribute attributes
          object[] attrs = field.GetCustomAttributes( typeof( BrowsableAttribute ), false );
          if( attrs.Length == 1 )
          {
            // If attribute exists and its value is false continue to the next field...
            BrowsableAttribute brAttr = ( BrowsableAttribute )attrs[ 0 ];
            if( brAttr.Browsable == false )
              continue;
          }

          values.Add( field.GetValue( enumType ) );
        }
      }

      return values.ToArray();
    }
  }
}
