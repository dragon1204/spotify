   M S B A M L   `   `   `  ����H  DAppMediaMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:AppMediaMusicAppMediaMusic  5   	   )    #AppMediaMusic.PlaylistDetailsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:AppMediaMusic     5   	   -   5          � Title$  PlaylistDetailsWindow��5   	   $	��470��6'   $	��800��64    � WindowStartupLocation$ CenterScreen=�5	   	    � 	Resources 5       �� 
TargetType �� Property �� Value �� Padding��%�  '��       '�� �    '�� M    '�� �    �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��$
 10,5q�6(   �� 5       � �� �	#FF2E2E2E6+   �� 5       � ��
 �����6+   �� 5       � �� �	#FF1C1C1C6,   �� 5       � ��$ 1q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 5q�6'   �� 5       � ��$ Center=�64   5      �� 5   
   " ��6   .�5      ��  � ��$ 15��6'   �� 5       � �� �	#FF2E2E2E6+   �� 5       � ��
 �����6+   �� 5       � �� �	#FF1C1C1C6,   5      �� 5    
   " ��6   .�5!      ��  � ��$ 16(�6)   �� 5"       � ��
 �����6+   5#      �� 5%   
   " ��6   .�5&      ��  � ��
 �   �6+   �� 5'       � ��
 �����6+   �� 5(       � �� 5)      �� 5*      .R�5+      �� $ 10q�65   $��14(�6B   ��6$   �� 6B   5,      5.      5/      50      .��52      � ���	#FFACA0A0.+�55   
   r� -   5         r�Name$ mediaPlayer��55      $��41��6,   $	��718��68   $
��Left=�6D   $	��Top=�6_    r� LoadedBehavior$ Manual=�56      	 r� UnloadedBehavior$
	 Stop=�6/   $��	39,10,0,0q�6G   6Z   �� 59   
   -   5        
 ��Name$
 
timeSlider��59      $	��Top=�6%   $��39,367,39,0q�6=    � Minimum$ 0Q�6R    � Maximum$	 100Q�6^   5:      �� 5=   
   -   5         ��Name$ timeDisplay��5=      $
��Left=�6)   $	��Top=�6D   $��
39,340,0,0q�6\   $	��100��6p   $��22��6|   $��00:00��6�   �� 5@   
   -   5         ��Name$ 
track_list��5@      $��Center=�6&   $	��101��6C   $��	0,261,0,0q�6P   $	��Top=�6c   $	��392��6{   ���	#FF7C7575���	#FFDCE29C5A   X   � 5D   
   $��Center=�6   $��Bottom=�61   $��0,0,0,10q�6L   .+�5E      �� -   5         ��Name$ btn_previous��5E   (   $��Previous��6   $	��100��6>   6J   �� 5F      -   5        $ PlayPauseButton��5F      $��⏯ Play��6.   $��75��6?   $��30��6J   6V   �� 5G      -   5        $ btn_next��5G   $   $
��Next��6   $	��100��66   6B   �� 5H      -	   5        $ 
btn_delete��5H   &   $��Delete��6   $	��100��6:   6F   �� 5I      -
   5        $ btn_open_home��5I   .   $��Open from Home��6   $	��100��6E   6Q   �� 5J      -   5        $ btn_open��5J   .   $��Open from file��6   $	��100��6@   6L   5K      &   System.Windows.Controls.DataGrid  5N   
   -   5          Name$ PlaylistSongDataGrid��5N      $��Center=�61   $	��200��6N   $��0,56,0,0q�6[   $	��Top=�6m   $	��722��6�      AutoGenerateColumns$ False��6�      CanUserAddRows$ False��5O      ���	#FFA5BBAF
���   �   Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ PlaylistsongId��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 60 6=   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *PlaylistsongId+  5R      $ 
PlaylistId��6%   $ 60 69    6D   �� *
PlaylistId+  5S      $ Title��6%   $	 200 64    6@   �� *
Song.Title+  5T      $ Artist��6%   $ 70 65    6@   �� *	AddedAt+4  .System.Windows.Controls.DataGridTemplateColumn  5U      $
 Play��6)      CellTemplate 5V      �� 5W      .R�5X      �� -   5        $
��Play��5X   %   $��50��6k   
 � Tag 6M   �� *Song.FilePath+6k   5Y      5[      5]      5^      5_      