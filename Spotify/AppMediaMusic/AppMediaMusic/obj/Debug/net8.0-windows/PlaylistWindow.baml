   M S B A M L   `   `   `  ����H  DAppMediaMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:AppMediaMusicAppMediaMusic  5   	   "    AppMediaMusic.PlaylistWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:AppMediaMusic     5   	     � Title$  Playlist��5   	   $	��450��6   $	��800��6'   .��5
      �� -   5        
������� �� CornerRadius$ 40��5
       ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��220 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��* �6    �� 5      $	��100 �6    .+�5      �� 
�������$ 	40 0 0 40��6(   $��2��6A   ._�5      �� $��0 30 0 0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $
��Left=�6:   $��
63 10 0 10q�6U   .+�5      A� ���#ff6258$��10��61   $��10��6<   A� 5      ���#ffbf2f$��10��61   $��10��6<   $	��7 0q�6H   A� 5      ���#2acb42$��10��61   $��10��6<   5      �� 5      $��LIBRARY��6       	menuTitle#��[  6/   ���	#FF2FB4326R   �� 5!      -   5          
menuButton#��[ 5!      .��5"      � .+�5#      �� $
��Home��6(   $��24��64   $	��Top=�6@   5$      5%      �� 5'      -   5         ��Name$ PlaylistButton��5'      #��[ 65   ���	#FFEE8DB0
�������.��5(      � .+�5)      �� $��	Playlists��6(   $��0 0 0 2q�69   5*      5+      �� 5,      -   5        $��Add Playlist��5,      #��[ 64   6Y   �� 5-      -   5        $��Delete Playlist��5-      #��[ 67   6\   �� 5.      -   5        $��Update Playlist��5.      #��[ 67   6\   5/      50      �� 53      $��1��6   
�������$��40 20 10 10q�6<   $��2��6Q   .+�54      �� $��	Playlists��6     	titelText#��[ 6-   $��0 0 0 20q�6P   �� 5:      -   5         ��Name$ PlayListListView��5:      $��Stretch=�5;      $��Stretch=�5<      $��Disabled=�5=       �� SelectionMode$ Single=�5>      ���#2F3E46 � 	Resources 5A      	 �� 
TargetType
 �� Property �� Value �� Triggers P� Property P� Value��%�  '��       '��     �� 5C      "	 ��6    .�5D      �� 
 � ��
 �����6;   �� 5E      
 � ��
 ���� 6;   �� 5F      
 � ��$ Stretch=�6K   �� 5G      
 � �� �#2F3E466<   �� 5I      
 � ��$ 0,0,0,1q�6@    5K      P� 5L   "    �@J�IsMouseOver$
 True��6A   . �5M   &   �� 
 � �� �#555B636C   �� 5N   &   
 � ��
 �����6C   5O   #   P� 5P   "    �@��
IsSelected$
 True��6@   . �5Q   &   �� 
 � �� �#3A40476C   �� 5R   &   
 � ��
 �����6C   5S   #   5U      �� 5X      "	 ��6    .�5Y      �� 
 � ��
 �����6;   �� 5Z      
 � �� �#2F3E466;   �� 5[      
 � ��$ Center=�6K   5\      5]       �� View 5`      �� 5a      .P�5c      �� $��
PlaylistId��6-    �� Width$ 70��6m    �� DisplayMemberBinding 6A   �� *
PlaylistId+6m   �� 5f      $
��Name��6-   $	 200��6a    6;   �� *Name+6a   �� 5i      $
��Date��6-   $	 150��6f    6;   �� *	CreatedAt+6f   �� 5l      $��Actions��6-   $	 100��6>    �� CellTemplate 5m   "   �� 5n   &   .R�5p   *   �� -   5        $��Center=�5p   1   $��Center=�5q   !   
������ 
�������
�������
 � Tag 5v   !   �� *
PlaylistId+��5w   .   �� 5x   2    �� 
TargetType" ��6B   .R�5y   6   � $��35��6;   $��35��6F   $��Center=�6R   $��Center=�6m   .+�5{   :   F� ����� � p�E���� 0�1��eR  `� �� �`� ��  �� Ӏ�  PS �D ��- �pr ��- ӐL� ��- �� �F @]� P/l ���� �F �+� ��- `��- �`j9��- ��^ PS ��^ �� ���^�`� �+ `� P}� @�1� � p�E	���#FF4081$��Center=�5}   /   $��Center=�5~   /    �� Stretch$ Uniform=�5   /   $��14��5�   /   $��14��6:   5�   7   5�   3   5�   +   5�   '   5�      5�      5�      �� 5�      -	   5         ��Name$ AddPlaylistPopup��5�      ���	#AAFFFFFF
���   �$��1q�6^   $ 8��6r    �� Padding$ 10q�5�      $	��300��6   $	��200��6*   $��Center=�67   $��Center=�6R   $y�	Collapsed=�6o   ._�5�      �� .+�5�      �� $��New Playlist��6$   $
��Bold"�68   $��16(�6J   $��0,0,0,10q�6X   �� 5�      -
   5         ��Name$ PlaylistNameTextBox��5�   "   $��0,0,0,10q�6@   �� 5�      $ 
Horizontal=�6%   $��Right=�6>   .+�5�      �� -   5        $
��Save��5�   %   $��0,0,5,0q�6R   �� 5�      -   5        $��Cancel��5�   %   66   5�      5�      5�      �� 5�      -   5        $ UpdatePlaylistPopup��5�      ���	#AAFFFFFF
���   �$��1q�6a   $ 8��6u   $ 10q�5�      $	��300��6   $	��200��6*   $��Center=�67   $��Center=�6R   $y�	Collapsed=�6o   ._�5�      �� .+�5�      �� $��UpdatePlaylist��6$   $
��Bold"�6:   $��16(�6L   $��0,0,0,10q�6Z   �� 5�      -   5        $ UpdatePlaylistNameTextBox��5�   "   $��0,0,0,10q�6F   �� 5�      $ 
Horizontal=�6%   $��Right=�6>   .+�5�      �� -   5        $
��Save��5�   %   $��0,0,5,0q�6V   �� 5�      -   5        $��Cancel��5�   %   66   5�      5�      5�      �� 5�      $ 20��6   $��0 20 0 0q�6+   ��5�      �� 5�      .��5�      �  � Color$ #30ad9a��6+    � Offset$ 0Q�6;   � 5�      $ #18c274��6+   $ 1Q�6;   5�       J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         
BlurRadius$ 20Q�6+      ShadowDepth$ 0Q�6;      Opacity$	 0.4Q�6K      Color$ #1fae7e��6Y   ._�5�      � .+�5�      ��   J� Opacity$
  0.07Q�6!   $ 20��60   ��5�      �� 5�   "   ! �� ImageSource$! /Images/Music.png��6-   5�      5�      5�      5�      5�      5�      5�      