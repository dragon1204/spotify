   M S B A M L   `   `   `  ����H  DAppMediaMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AppMediaMusic.UserControlsAppMediaMusic.UserControls  5   	       AppMediaMusic.ManageUser   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   3uc(clr-namespace:AppMediaMusic.UserControls   5   	   -   5        $	��420��5   	   $
��1010��6     � 
ResizeMode$  NoResize=�6#    � AllowsTransparency$
 True��69    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.   
������  � 	Resources 5         ListViewItemStyle �� 
TargetType �� Property �� Value��%t   &	        �� 5	   
   " ��6*   .�5
      ��  � ��$ 0q�6(   �� 5       � ��$ 0q�6'   5      5      .��5      �� -   5        
������� �� CornerRadius$ 40��5       ._�5   
   � 	 � ColumnDefinitions	 5      �� 5      $	��220 �6#   �� 5      $��* �6#   �� 5      $��* �6#   
 � RowDefinitions
 5      �� 5      �� 5      $��12 �6    .+�5      �� 
�������$ 	40 0 0 40��6(   $��2��6A   ._�5      �� $��0 30 0 0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $
��Left=�6:   $��
63 10 0 10q�6U   .+�5       A� ���#ff6258$��10��61   $��10��6<   A� 5!      ���#ffbf2f$��10��61   $��10��6<   $	��7 0q�6H   A� 5"      ���#2acb42$��10��61   $��10��6<   5#      �� 5$        
menuButton#��[ 6   ���#02be68
�������.��5%      � .+�5&      �� $��Users��6(   .  (MahApps.Metro.IconPacks.PackIconMaterial  5'        menuButtonIcon#��[ 64   $y�Visible=�6\   5(      5)      �� 5,      -   5        $��
View Image��5,      #��[ 62   6V   �� 5-      -   5        $��	Add Admin��5-      #��[ 61   6U   �� 5.      -   5        $��Delete��5.      #��[ 6.   6R   �� 50      -   5        $��	Dashboard��50      #��[ 61   6U   �� 51      -   5        $
��Quit��51      #��[ 6,   6P   52      53      �� 56      $��1��6   
�������$��29,10,78,25q�6<   $��2��6Q   .+�58      �� $��User Management System��6     	titelText#��[ 6:   $��51��6]   �� 5:      $ 20��6   $��0 20 0 0q�6+   $	��676��6=   $	��311��6I   ��5;      �� 5<      .��5=      �  � Color$ #30ad9a��6+    � Offset$ 0Q�6;   � 5>      $ #18c274��6+   $ 1Q�6;   5?       J� Effect 5B      3  -System.Windows.Media.Effects.DropShadowEffect  5C         
BlurRadius$ 20Q�6+      ShadowDepth$ 0Q�6;      Opacity$	 0.4Q�6K      Color$ #1fae7e��6Y   ._�5F      � .+�5G      &   System.Windows.Controls.DataGrid  -   5          Name$ UserDataGrid��5G   #   $��Center=�69   $	��255��6V   $��0,30,0,0q�6c   $	��Top=�6u   $	��638��6�      AutoGenerateColumns$ False��6�      Columns 5H      0  *System.Windows.Controls.DataGridTextColumn  5I   "   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��65      Width5  /System.Windows.Controls.DataGridLengthConverter$ 40 6A   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   �� *UserId+  5J   "   $ Username��65   $	 100 6G    6S   �� *
Username+  5K   "   $
 Role��65   $ 50 6C    6N   �� *Role+  5L   "   $ 	Full Name��65   $	 100 6H    6T   �� *UserProfile.FullName+  5M   "   $ Birth��65   $	 100 6D    6P   �� *UserProfile.Birth+  5N   "   $ Phone��65   $	 100 6D    6P   �� *UserProfile.Phone+  5O   "   $ Email��65   $	 150 6D    6P   �� *UserProfile.Email+5Q      5R      5S      5T      5U      5V      5W      