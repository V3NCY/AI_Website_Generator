   M S B A M L   `   `   `  ����  AI_Website_Generator4    .AI_Website_Generator.user.PasswordChangeDialog   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$"  Смяна на парола��5   	   $	��220��5   	   $	��350��6    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5	   	   
������ .��5      � 
������ .+�5   
   �� 
������� �� CornerRadius$ 12��6$   $	��300��66   $	��180��6B   $��Center=�6O   $��Center=�6j    �� Padding$ 20q�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         ShadowDepth$ 5Q�6#   	   
BlurRadius$	 15Q�63   
   Opacity$	
 0.3Q�6C   ._�5      � .+�5      �� -   5        $	��❌��5      $��Right=�6%   $	��Top=�6A   $��28��6Y   $��28��6d   $��0,-10,-10,0q�5      
������ 
������ ���#073764$
��Bold"�5   .   $��16(�6@    � Cursor$
 Hand��6N   ��5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value 5   "   �� 5   &    �� 
TargetType" ��66   .R�5   *   �� 
������ ._�5   .   �� $��Center=�6?   $��Center=�6\   5   +   5    '   5"       �� Triggers 5#      P� 5$   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �5%   &   ��  � �� �#C0392B6C   5&   #   5(      5*      �� 5,      $��Center=�6   $��0,20,0,0q�68   .+�5-      �� $'��!🔒 Смяна на парола��6    $��16(�6:   $
��Bold"�6H   ���#2C3E50 �� TextAlignment$ Center=�6o   $��0,0,0,10q�6�   �� 5/      $��Нова парола:��6    $��SemiBold"�64   G� 50      -   5         G�Name$ NewPasswordBox��50   "   $��0,4,0,12q�6:   �� 52      -   5        $��Запази��52      ���#27AE60
�������$
��Bold"�53      $��8q�6/   $��0q�6;   $
 Hand��6O   $��Right=�54      $��80��69   ��55      �� 56      " ��6$   .�57   "   ��  � �� �#27AE606?   �� 58   "    � ��
 �����6?   �� 59   "    � �� 5:   &   �� 5;   *   " ��6:   .R�5<   .   �� #��z��65   $ 6��6_   # z��6p   ._�5=   2   �� $��Center=�6C   $��Center=�6`   5>   /   5?   +   5A   #    5B   "   P� 5C   &    �@J�IsMouseOver$
 True��6E   . �5D   *   ��  � �� �#1E84496G   5E   '   5G      5I      5J      5K      5L      5M      5N      