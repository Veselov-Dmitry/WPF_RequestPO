   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       Request   Request.ProgramList   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      4sys&clr-namespace:System;assembly=mscorlib    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          >�Name$  ProgLIst��5      $	��900��6   $	��900��6!   .��5      � .+�5   
   �� ���	#90000000$��10,-50,-10,50q�6J   y�6(   �� *
Visibility+._�5	      �� $��	0,100,0,0q�6   
���   �$��1q�5
   %   
������� �� CornerRadius$ 5��5      $	��Top=�5      $��Center=�5      $
��auto��5       J� BitmapEffect 5      F� 5       F� Color$ Black��6-    F� Opacity$	 0.5Q�6;    F� 	Direction$	 270Q�6I    F� ShadowDepth$	 0.7Q�6Y   ._�5      � $��
10,10,10,0q�6    � Orientation$ 
Horizontal=�60   .+�5      � -   5         �Name$ listWrap��5       $ Vertical=�63   .+�5      � $	��280��6$   $ Vertical=�63   .+�5      � $ 
Horizontal=�6(   .+�5   "   �� -   5        $
��Bold"�5   %      Buttons#��[  5   %   $��14(�5   %   $1��+Снять выделение со всех��5   %   5   %   5      � 5      $ 
Horizontal=�6(   .+�5   "   �� "Номер задачи"5    #   5!      5"      �� 5#      -   5        	 ��Name$	 autoListProg��5#   "   $
��Left=�5$   !   $	��400��5%   !   $
��auto��6.   $	��450��5&   "   
 �� AlternationCount$
 2��5'      $��1q�5(   !   
�������
���   �$��Verdana)�5+   !   $��12(�5,   !   $
��Bold"�5-   !    � 	Resources 5.       �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%�  '��       �� 5/   "   " ��6(   .�50   &   ��  � ��
 �2�2�6C   �� 51   &    � ��
 �����6C    52   &   P� 53   *    �@��AlternationIndex$ 1��6[   . �54   .   ��  � ��
 �����6K   �� 55   .    � ��
 �   �6K   56   +   P� 57   *    �@��AlternationIndex$ 0��6[   . �58   .   ��  � ��
 ��İ�6K   �� 59   .    � ��
 �����6K   5:   +   5<   #   5=      .@�5>      �� .��5?   "   � $	��280��6,   $ Vertical=�6;   .+�5@   &   � $ 
Horizontal=�60   .+�5A   *   �� $�� ��63   $��Center=�6>   �� 5B   *   $��0,0,10,0q�5C   -   $��Center=�5D   -   "№ задачи "5F   +   �� 5G   *   -   5        $��	Citrix   ��5G   3   $��Center=�5H   3   5I   3   �� 5J   *   $��Center=�64   $��экспл.��6O   �� 5K   *   -   5        $	��140��5K   3   $��10,0,0,0q�6?   5M   +   5N   '   � 5O   &   $ 
Horizontal=�60   .+�5P   *   �� $��Center=�64   !"Название задачи"5R   +   5S   '   5T   #   5U      5V      5W      � 5Y      $��5q�6    $ Vertical=�6+   $��Right=�6B   $	��Top=�5Z   
   .+�5[      �� -   5         ��Name$ OkButton��5[   !   #��[  5\   !   $��80��5]   !   $��Ok��5_   !   $��12(�5`   !   $
��Bold"�5a   !   $��2q�5b   !   �� 5c      -   5        $ CancelButton��5c   !   #��[  5d   !   $��80��5e   !   $��Cancel��5g   !   $��12(�5h   !   $
��Bold"�5i   !   $��2q�5j   !   5k      5l      5m      5n      5o      5p      