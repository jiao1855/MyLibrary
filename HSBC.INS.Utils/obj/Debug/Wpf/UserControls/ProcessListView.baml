   M S B A M L   `   `   `  ����I  EHSBC.INS.Utils, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:HSBC.INS.Utils.WpfHSBC.INS.Utils.Wpf  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      (    "HSBC.INS.Utils.Wpf.ProcessListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:HSBC.INS.Utils.Wpf     5      4s&clr-namespace:System;assembly=mscorlib     5        � 	Resources  5
       �� 
TargetType �� Property �� Value   processDataTemplate J� SnapsToDevicePixels �� Path �� RelativeSource �� Mode �� AncestorType	 �� Padding
 �� StringFormat � ContextMenu m� Command m� CommandParameter m� InputGestureText#   HSBC.INS.Utils.Wpf.ListScroll �� Triggers P� Property P� Value��%�  'm�       &	  H     '�� P    7   �� 5   
   " m�6   .�5      ��  � ��$ 23��6'   5      �� 5   
   .R�5      ]� ��. $��-2,0,0,0q�6-    . ��6?   �� $ 
Background� �� $ FindAncestor=�$ ListBoxItemL�.+�5      �� $��0,0,6,0q�6l   $	 0,1,0,1q�6}   $
��Left=�6�   ��6   �� *
CreateTime+$
 HH:mm:ss����6O   �� *Color+6�   �� 5      $
��Wrap=�6O   $	 0,1,0,1q�6c   ��6   �� *Text+��62   �� *Color+ 5      �� 5      .@�5      m� $
 Copy��6I    6'   �� *Text+ 6X   Z� 6v   5      5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ Disabled=�6K   6^   �� 5       � ��$ Stretch=�6;   6M   �� 5        �@ 	IsEnabled . 6J   �� 5!       � ��8   6\   �� 5"       � �� 5#      �� 5$      " ��6    5%      P� 5&       �@��
IsSelected . . �5'   "   ��  � ��
 �����6S   5(      5*      5,      5-      5.      .��5/      � .+�50   
   �� ��6   �� $ ProcessModels� �� $ UserControlL�6y   51      52      