   M S B A M L   `   `   `  ����M  IJustbokApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:JustbokApplication.Views.Settings!JustbokApplication.Views.Settings  5      W SMaterialDesignThemes.Wpf, Version=1.2.0.40933, Culture=neutral, PublicKeyToken=nulleHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf 5      9    3JustbokApplication.Views.Settings.AddEditBranchView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:JustbokApplication.Views.Settings     5      XwpfHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf     5      $��Center=�5      $��Center=�6<   .��5	      � $��16q�6     � RowDefinitions  5
   
   �� 5      $��75 �6   �� 5      $��75 �6   �� 5      $��75 �6   �� 5      $��75 �6   �� 5      $��75 �6    � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��20 �6   �� 5      $
��0.5* �6   .+�5   
   �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Branch Code��6    %  !MaterialDesignFloatingHintTextBox#���   64   $��0 6 0 0q�5      $��18(�6#   $��0��61   $	��Top=�6A   $	��250��5      ��6   �� *Item.BranchCode+ �� NotifyOnTargetUpdated .  �� Mode$ TwoWay=�6h   �� 5   
   $ Branch Name��6   #���   64   $��0 6 0 0q�5      $��18(�6#   $��2��61   $	��Top=�6A   $	��250��5      ��6   �� *Item.BranchName+ . $ TwoWay=�6h   �� 5   
   $ Address��6   #���   60   $��0 6 0 0q�5       $��18(�6#   $��0��61   $��1��6A   $	��Top=�6N   $	��250��5!      ��6   �� *Item.Address+ . $ TwoWay=�6e   �� 5#   
   $ Phone Number��6   #���   65   $��0 6 0 0q�5$      $��18(�6#   $��2��61   $��1��6A   $	��Top=�6N   $	��250��5%       �� 	MaxLength$ 10��5&      ��5%      �� *Item.PhoneNo+ . $ TwoWay=�5&   #   �� 5(   
   $
 City��6   #���   6-   $��0 6 0 0q�5)      $��18(�6#   $��0��61   $��2��6A   $	��Top=�6N   $	��250��5*      ��6   �� *	Item.City+ . $ TwoWay=�6b   �� 5,   
   $ State��6   #���   6.   $��0 6 0 0q�5-      $��18(�6#   $��2��61   $��2��6A   $	��Top=�6N   $	��250��5.      $ 10��5/      ��5.      �� *
Item.State+ . $ TwoWay=�5/   #   �� 52   
   -   5         ��Name$ isActive��52      $��0,0,0,8q�6%   $��0��66   $��3��6F   $	��250��6S   $��IsActive��6_   $��Center=�53      $
��Left=�65    d� 	IsChecked 54      �� *Item.IsActive+ . $ TwoWay=�� 56   
    � Orientation$ 
Horizontal=�6   $��Center=�6-   $��Center=�6H   $��3��6e   $��4��6y   .+�57      �� 	 �� 	IsDefault	 .   MaterialDesignFlatButton#���  6&   $
��Save��58      ��6$   �� *SaveDialogCommand+�� 5:      
 �� IsCancel
 . $��10,0,0,0q�6%   #���  67   $��Cancel��5;      ��5:   j   �� *CancelDialogCommand+5;      5<      5=      5>      