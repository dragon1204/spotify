   M S B A M L   `   `   `  ����H  DAppMediaMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:AppMediaMusicAppMediaMusic  5   	       AppMediaMusic.SongDetail   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:AppMediaMusic     5   	     � Title$  
SongDetail��5   	   $	��498��6   $	��800��6)   .��5
      � $��20q�6   .+�5   
   r� -   5         r�Name$ mediaPlayer��5       r� LoadedBehavior$ Manual=�6,    r� UnloadedBehavior$
 Stop=�6D   �� 5   
   $��Center=�6   $	��Top=�62   $��0,10,0,0q�6J   .+�5      �� -   5         ��Name$ AlbumArtImage��5      $	��150��6+   $	��150��67   $��0,0,0,10q�6D   �� 5      -   5         ��Name$ SongTitleText��5      $��24(�6/   $
��Bold"�6=   $��Center=�6O   �� 5      -   5        $ ArtistNameText��5      $��18(�60   $��Center=�6>   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��140,277,140,100q�6.   .+�5      �� -   5        $ CurrentTimeText��5      $
��0:00��61   $��Center=�6=   $��0,0,5,0q�6X   �� 5      -   5         ��Name$ TimelineSlider��5      $	��400��6-    � Minimum$ 0Q�5      	 � Maximum$	 1Q�6   5      �� 5!      -   5        $ TotalTimeText��5!      $
��0:00��6/   $��Center=�6;   $��5,0,0,0q�6V   5"      �� 5&   
   $ 
Horizontal=�6   $��Center=�6.   $��
0,320,0,50q�6K   $��50��6_   .+�5'      �� -   5        
 ��Name$
 PlayPauseButton��5'      $��	⏸ Pause��6.   $��5q�6@   $��75��6K   $��30��6V   6b   �� 5(      -	   5        $	��⏮��5(      $��5q�6!   $��30��6,   67   �� 5)      -
   5        $	��⏭��5)      $��5q�6!   $��30��6,   67   �� 5*      -   5        $��Previous��5*      $��5q�6(   $��75��63   6>   �� 5+      -   5        $
��Next��5+      $��5q�6$   $��75��6/   6:   �� 5,      $��Volume��6   $��Center=�6&   $��10,0,5,0q�6A   �� 5-      -   5        $ VolumeSlider��5-      $	��100��6+   $ 0Q�67   $	 1Q�6C    � Value$	 0.5Q�6O   $��19��6�   5.      5/      50      