   M S B A M L   `   `   `  ����H  DAppMediaMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AppMediaMusic.UserControlsAppMediaMusic.UserControls  5j   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5k   	       AppMediaMusic.Statistic   5f   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5g   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5h   	   c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5i   	   3uc(clr-namespace:AppMediaMusic.UserControls   5j   	   @lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf   5k   	   -   5        $	��420��5l   	   $
��1010��6     � 
ResizeMode$  NoResize=�6#    � AllowsTransparency$
 True��69    � WindowStartupLocation$ CenterScreen=�5m   	    � WindowStyle$
 None=�6.   
������  � 	Resources 5n         ListViewItemStyle �� 
TargetType �� Property �� Value��%t   &	        �� 5o   
   " ��6*   .�5p      ��  � ��$ 0q�6(   �� 5q       � ��$ 0q�6'   5r      5s      .��5u      �� 
������� �� CornerRadius$ 40��6    ._�5v   
   � 	 � ColumnDefinitions	 5w      �� 5x      $	��220 �6#   �� 5y      $��* �6#   �� 5z      $��* �6#   
 � RowDefinitions
 5}      �� 5~      �� 5      $��12 �6    .+�5�      �� 
�������$ 	40 0 0 40��6(   $��2��6A   ._�5�      �� $��0 30 0 0q�6   .+�5�      ��  �� Orientation$ 
Horizontal=�6!   $
��Left=�6:   $��
63 10 0 10q�6U   .+�5�      A� ���#ff6258$��10��61   $��10��6<   A� 5�      ���#ffbf2f$��10��61   $��10��6<   $	��7 0q�6H   A� 5�      ���#2acb42$��10��61   $��10��6<   5�      �� 5�        
menuButton#��[ 6   ���#02be68
�������.��5�      � .+�5�      �� $��	Statistic��6(   .  (MahApps.Metro.IconPacks.PackIconMaterial  5�        menuButtonIcon#��[ 64   $y�Visible=�6\   5�      5�      �� 5�      -   5        $��Manage User��5�      #��[ 63   6W   �� 5�      -   5        $��Manage Song��5�      #��[ 63   6X   �� 5�      -   5        $
��Quit��5�      #��[ 6,   6Q   �� 5�      -   5        $��Log Out��5�      #��[ 6/   6S   5�      5�      �� 5�      $��1��6   
�������$��29,10,78,10q�6<   $��2��6Q   .+�5�      �� $ 20��6   $	��609��6+   $	��132��67   ��5�      �� 5�      .��5�      �  � Color$ #30ad9a��6+    � Offset$ 0Q�6;   � 5�      $ #18c274��6+   $ 1Q�6;   5�       J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         
BlurRadius$ 20Q�6+      ShadowDepth$ 0Q�6;      Opacity$	 0.4Q�6K      Color$ #1fae7e��6Y   ._�5�      � .+�5�      �� $ Vertical=�6%   $��	0,0,310,0q�6<   .+�5�      �� 
�������$��1q�69   $ 20��6M   
�������$	��216��6r   $��50��6~   $��0,40,0,0q�6�   ._�5�   "   �� $ 
Horizontal=�6-   $��Center=�6F   .+�5�   &        Kind$ Account=�6<   ���	#FF2AB290$��47��6b   $��54��6n   $��20,0,0,0q�6y    J� RenderTransformOrigin$ 
0.86,1.653&�6�   �� 5�   &   -   5         ��Name$ 	lbUserNum��5�   ,   $��00��6?   $��Center=�6L   $��40,0,0,0q�6g   $��40(�6y   $��61��6�   $��57��6�   5�   #   5�      �� 5�      $��Number of accounts��6$   $��Center=�6A   
�������$��25(�6q   $
��Bold"�6   5�      �� 5�      $ Vertical=�6%   $��	343,0,0,0q�6<   .+�5�      �� 
�������$��1q�69   $ 20��6M   
�������$	��206��6r   $��54��6~   $��0,40,0,0q�6�   ._�5�   "   �� $ 
Horizontal=�6-   $��Center=�6F   .+�5�   &     $ Music=�6<   ���	#FF2AB290$��21,0,0,0q�6`   $��48��6r   $��65��6~   $��Center=�6�   �� 5�   &   -   5        $ 	lbSongNum��5�   ,   $��00��6?   $��Center=�6L   $��40,0,0,0q�6g   $��40(�6y   $��58��6�   $��49��6�   5�   #   5�      �� 5�      $��Number of songs��6$   $��Center=�6>   
�������$��25(�6n   $
��Bold"�6|   5�      #  LiveCharts.Wpf.CartesianChart  5�      -   5          Name$ SongsCreatedChart��5�   -   $��0,164,0,-263q�6H   &   LiveCharts.Wpf.Charts.Base.Chart   AxisX 5�   "     LiveCharts.Wpf.Axis     Title$
 Date��6+      Labels 68   �� *Dates+   	Separator 5�   &     LiveCharts.Wpf.Separator  5�   *      Step$ 1Q�68   5�   #      AxisY 5�   "     $ Number of Songs��6+      MinValue$ 0Q�6C    5�   &     5�   *   $ 1Q�68   5�   #   5�      5�      5�      5�      5�      5�      5�      