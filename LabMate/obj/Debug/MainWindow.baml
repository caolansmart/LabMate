   M S B A M L   `   `   `  ����B  >LabMate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LabMateLabMate  5   	       LabMate.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:LabMate     5   	     � Title$  LabMate��5   	   $	��800��6   $
��1200��6&   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��20px �6   �� 5      $
��45px �6   �� 5      $��* �6   .+�5   
   � $��0��6   ���#00001e.+�5      ]� $��Center=�6   $��Stretch=�63   .+�5      o� 
������ 
�������$��Center=�6C   $��12(�6^    � 	Resources 5       �� 
TargetType �� Property �� Value��%R   'm�       �� 5      " m�6    .�5      ��  � ��$ 12(�69   5      5      .@�5      m� $
��File��6   m� 5      $
��Edit��6   m� 5      $
��View��6   5      5      5!      � 5$   
   $��1��6   ���#00001e.+�5&      ]� $
��Left=�6    K� ZIndex$ 1��63   ���#00001e$��1q�6Y    5'      ��%�   '��       �� 5(      " ��6   .�5)      ��  � ��$ 12(�65   �� 5*       � ��
 �����67   �� 5+       � ��$ Center=�6@   5,      5-      .+�5/      �� $
��55px��6   
������ $��0q�6?   .��50      �� .+�51      �� $��/images/IconLogOut.png��6    $
��22px��6A   �� 52      $
��12px(�6$   $��Center=�64   
�������	Log Off6m   53      54      �� 55      $��/images/ToolbarLine.png��6   $	��5 0q�69   �� 56      -   5        $��homeBtn��56      $
��55px��6(   
������ $��0q�6N   .��57      �� .+�58      �� $��/images/IconHome.png��6    $
��22px��6?   �� 59      Home6*   5:      5;      �� 5<      -   5        $��newRequestBtn��5<      $
��90px��6.   
������ $��0q�6T   .��5=      �� .+�5>      �� $ ��/images/IconNewRequest.png��6    $
��22px��6E   �� 5?      New Request61   5@      5A      �� 5B      -   5        $��amendRequestBtn��5B      $
��90px��60   
������ $��0q�6V   .��5C      �� .+�5D      �� $"��/images/IconAmendRequest.png��6    $
��22px��6G   �� 5E      Amend Request63   5F      5G      �� 5H      -   5        $��findRequestBtn��5H      $��100px��6/   
������ $��0q�6V   .��5I      �� .+�5J      �� $!��/images/IconFindRequest.png��6    $
��22px��6F   �� 5K      Find Request62   5L      5M      �� 5N      $��/images/ToolbarLine.png��6   $	��5 0q�69   �� 5O      $
��55px��6   
������ $��0q�6?   .��5P      �� .+�5Q      �� $��/images/IconSettings.png��6    $
��22px��6C   �� 5R      $
��12px(�6$   $��Center=�64   
�������
Settings6n   5S      5T      �� 5U      $��/images/ToolbarLine.png��6   $	��5 0q�69   �� 5V      $
��55px��6   
������ $��0q�6?   .��5W      �� .+�5X      �� $��/images/IconBack.png��6    $
��22px��6?   �� 5Y      Back6*   5Z      5[      �� 5\      $
��55px��6   
������ $��0q�6?   .��5]      �� .+�5^      �� $��/images/IconForward.png��6    $
��22px��6B   �� 5_      	Forward6-   5`      5a      �� 5b      $��/images/ToolbarLine.png��6   $	��5 0q�69   5c      �� 5e      $(��"/images/LabMateLogoLarge1White.png��6   $��Right=�6@   $��40��6\   $��0 0 10 0q�6h   $ 0��6z   5g      � 5j   
   $��2��6   
������� � ColumnDefinitions 5l      �� 5m      $��100px �6#   �� 5n      $��* �6#   .+�5s      � $��0��6   ���#00001e 5u      �� 5v      $
��30px �6$   �� 5w      $��* �6$   .+�5z      �� $��1��6   $��Center=�6*   .+�5{      �� $��Enter Request��6   $��Center=�65   $	��Top=�6R   $
��30px��6j   $��0,10px,0,10pxq�6x   $
��90px��6�   �� 5|      $��Amend Request��6   $��Center=�65   $	��Top=�6R   $
��30px��6j   $��0,10px,0,10pxq�6x   $
��90px��6�   5}      5      �� 5�      -   5        $��FormContentControl��5�      $��1��67   5�      5�      5�      