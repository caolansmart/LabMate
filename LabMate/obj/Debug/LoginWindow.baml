   M S B A M L   `   `   `  ����B  >LabMate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LabMateLabMate  5   	       LabMate.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:LabMate     5   	     � Title$  Login��5   	   $	��400��6   $	��300��6$    � WindowStartupLocation$ CenterScreen=�60    � 
ResizeMode$ CanMinimize=�6U   .��5	      � ���#00001e � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $	��250 �6   .+�5   
   �� $��0��6   $��Center=�6   $��Center=�6:   $'��!images/LabMateLogoLarge1White.png��6U   $	��200��6�   �� 5   
   $��0��6   $��Right=�6   $��Bottom=�69   
�������.��6k   Version 1.06x   � 5   
   $��1��6   
�������.+�5      �� $��Center=�6   $��Center=�66   $��0 0 0 0q�6Q   .+�5      �� .��6   
Username6"   �� 5      -   5        $��usernameTxtbox��5      $	��175��60   $��25��6<   $��0 5 0 5q�6H    �� 	MaxLength$ 20��6Y   $��Center=�6h   6�   �� 5      .��6   
Password6"   G� 5      -   5        $��passwordTxtbox��5      $	��175��64   $��25��6@   $��0 5 0 5q�6L    G� 	MaxLength$ 20��6]   $��Center=�6l   6�   �� 5      ���#00001e$��2q�6/    �� CornerRadius$ 5��6C   $	��100��6T   $��0 15 0 0q�6`   ._�5      �� -   5        $��loginBtn��5      $��Login��6-   $��30��6=   ���#00001e
�������$��0q�6q   6�   5      5      5      5       5!      