   M S B A M L   `   `   `  ����O  KAI_Website_Generator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:AI_Website_GeneratorAI_Website_Generator  5   	   -    'AI_Website_Generator.ViewRequestsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   4local"clr-namespace:AI_Website_Generator     5   	   -   5          � Title$  View Requests��5   	   $	��600��6   $
��1000��6,    � WindowState$ 	Maximized=�5   	    � 	Resources 5
      1   +AI_Website_Generator.StatusToColorConverter   StatusToColorConverter��%   &	          5   
   6'   5      .��5      � .+�5   
   �� -   5         ��Name$ RequestsList��5      $��20q�6)   ��65   �� *
Requests+ �� View 5      �� 5      .P�5      �� $��Клиент��6%    �� Width$	 150��6]    �� DisplayMemberBinding 65   �� *Client+6]   �� 5      $��Телефон��6%   $	 120��6]    66   �� *Phone+6]   �� 5      $��
Имейл��6%   $	 150��6[    64   �� *Email+6[   �� 5      $ ��Код по НЕИСПУО��6%   $	 120��6c    6=   �� *Code+6c   �� 5      $��
Обект��6%   $	 180��6_    64   �� *	Institute+6_   �� 5      $!��Населено място��6%   $	 100��6c    6=   �� *City+6c   �� 5      $��Стар домейн��6%   $	 120��6f    6:   �� *
PrevDomain+6f   �� 5      $��Нов домейн��6%   $	 120��6d    69   �� *	NewDomain+6d   �� 5      $��Статус��6%   $	 180��65    �� CellTemplate 5      �� 5      .R�5   "   ��  �� CornerRadius$ 10��6)   	 �� Padding$		 6,2q�6;   $��2q�6I   ��5      �� *Status+
 �� 	Converter#
 [  ._�5   &   ��  �� Orientation$ 
Horizontal=�61   $��Center=�6J   .+�5    *   �� 
�������$��SemiBold"�5"       $
��Wrap=�5#       $��0,0,5,0q�5$       ��5    4   �� *Status+ � ToolTip 5%       �� *	Comment+�� 5&   *   -   5        $��16��5&   0   $��16��6;    � Cursor$
 Hand��5'      ��5)   .   �� 5*   2    �� 
TargetType" ��68   .�5,   6   ��  �� Property � �� �� Value$- 'pack://application:,,,/Icons/pencil.png��6O    �� Triggers 5-   6   �� 5.   :    �� Binding 6F   �� *	Comment+ �� Value  6b   .a�5/   >   ��  � ��$+ %pack://application:,,,/Icons/plus.png��6W   50   ;   �� 51   :    6F   �� *	Comment+ 6b   Z� .a�52   >   ��  � ��$+ %pack://application:,,,/Icons/plus.png��6W   53   ;   55   3    57   .   �� 58   2   ��6<   �� *	Comment+ �� TargetNullValue$# Добави коментар��5:   +   5;   '   5<   #   5=      5?      �� 5B      $��Действие��6%   $	 100��67    5C      �� 5D      .R�5E   "   �� -   5        $��🚀 Deploy��5E   )   ���#27AE60
�������$
��Bold"�5J   )   $��4q�5K   )   $��4q�5L   )   $
 Hand��5M   )   $��0q�5N   )   $X RИзвърши автоматично инсталиране на темплейт��5O   )   
 � Tag 5G   )   �� 5O   )   5P      5R      �� 5T      $��Темплейт��6%   $	 100��6a    67   �� *
Template+6a   5U      5W      �� 5Y   
   -   5        $��Обнови заявки��5Y      $��	20,0,0,10q�6)   $��30��6<   $	��130��6H   $
��Left=�5Z      $��Bottom=�6,   5[      5\      5]      