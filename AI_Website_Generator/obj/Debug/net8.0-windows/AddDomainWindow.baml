   M S B A M L   `   `   `  ����  AI_Website_Generator*    $AI_Website_Generator.AddDomainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$(  "Добави нов уебсайт��5   	   $	��750��6$   $	��900��61   ���#ECF0F1 � WindowStartupLocation$ CenterScreen=�6R    � WindowState$ 	Maximized=�6w    � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect   ShadowEffect   
BlurRadius   ShadowDepth   Opacity��%B   &	          5   
   $ 10Q�60   $ 3Q�6@   $	 0.3Q�6P   5	      .��5      � .+�5   
   �� 
������� �� CornerRadius$ 12��6$    �� Padding$ 20q�66   $��20q�6C   	 J� Effect#	 [  6O   ._�5      �� .+�5      �� $G��A🌐 Добави информация за нов уебсайт��6   $��22(�6G   $
��Bold"�6U   ���#2C3E50$��0,0,0,20q�6|   
 �� TextAlignment$
 Center=�6�   � 5       � ColumnDefinitions 5      �� 5      $��* �6+   �� 5      $��* �6+   .+�5      �� $��0��6!   $��15q�61   .+�5      �� $��Обект:��6$   $��SemiBold"�62   ���#34495E$��10q�6]   �� 5      -   5        $��txtOwner��5   "   $
��auto��62   $��30��6?   $��5q�6K   ���#F7F9F96W   �� 5      $"��Населено място:��6$   $��SemiBold"�6;   ���#34495E$��10q�6f   �� 5      -   5        $��txtCity��5   "   $
��auto��61   $��30��6>   $��5q�6J   ���#F7F9F96V   �� 5!      $!��Код по НЕИСПУО:��6$   $��SemiBold"�6;   ���#34495E$��10q�6f   �� 5"      -   5        $��txtCode��5"   "   $
��auto��61   $��30��6>   $��5q�6J   ���#F7F9F96V   �� 5$      $��МОЛ:��6$   $��SemiBold"�60   ���#34495E$��10q�6[   �� 5%      -   5        $��txtMol��5%   "   $
��auto��60   $��30��6=   $��5q�6I   ���#F7F9F96U   �� 5'      $)��#Телефон за контакт:��6$   $��SemiBold"�6?   ���#34495E$��10q�6j   �� 5(      -   5         ��Name$ txtPhone��5(   "   $
��auto��64   $��30��6A   $��5q�6M   ���#F7F9F96Y   �� 5*      $��Обект имейл:��6$   $��SemiBold"�68   ���#34495E$��10q�6c   �� 5+      -   5        $ txtOwnerEmail��5+   "   $
��auto��69   $��30��6F   $��5q�6R   ���#F7F9F96^   �� 5-      $��Пакет:��6$   $��SemiBold"�62   ���#34495E$��10q�6]   �� 5.      -   5         ��Name$ 
cmbPackage��5.   #   $
��auto��67   $��30��6D   $��5q�6P   ���#F7F9F9.@�5/      �� $��
Старт��6+   �� 50      $��Стандарт��6+   �� 51      $��Стандарт +��6+   52      55      �� 58      $��1��6!   $��15q�61   .+�5;      �� $��Стар домейн:��6$   $��SemiBold"�68   ���#34495E$��10q�6c   �� 5<      -   5        $��txtOldDomainName��5<   "   $
��auto��6:   $��30��6G   $��0q�6S   ���#F7F9F96_   �� 5>      $��Нов домейн:��6$   $��SemiBold"�67   ���#34495E$��10q�6b   �� 5?      -	   5        $��txtNewDomainName��5?   "   $
��auto��6:   $��30��6G   $��0q�6S   ���#F7F9F96_   �� 5A      $��Admin username:��6$   $��SemiBold"�6;   ���#34495E$��10q�6f   �� 5B      -
   5        $��txtAdminUsername��5B   "   $
��auto��6:   $��30��6G   $��5q�6S   ���#F7F9F96_   �� 5D      $��Admin password:��6$   $��SemiBold"�6;   ���#34495E$��10q�6f   �� 5E      -   5        $��txtAdminPassword��5E   "   $
��auto��6:   $��30��6G   $��5q�6S   ���#F7F9F96_   �� 5H      $J��DДата на регистрация на тестов домейн:��6$   $��SemiBold"�6Q   ���#34495E$��10q�6|   (  "System.Windows.Controls.DatePicker  5I      -   5        $��dateRegTestDomain��5I   %   $
��auto��6>   $��30��6K   $��5q�6W   ���#F7F9F96c   �� 5K      $P��JДата на регистрация на официален домейн:��6$   $��SemiBold"�6T   ���#34495E$��10q�6     5L      -   5        $��dateRegOfficialDomain��5L   %   $
��auto��6B   $��30��6O   $��5q�6[   ���#F7F9F96g   �� 5N      $B��<Хостинг пространство по договор:��6$   $��SemiBold"�6L   ���#34495E$��10q�6w   �� 5O      -   5        $ 
cmbHosting��5O   #   $
��auto��67   $��30��6D   $��5q�6P   ���#F7F9F9.@�5P      �� $
��10GB��6+   �� 5Q      $
��25GB��6+   �� 5R      $
��50GB��6+   5S      �� 5V      $��Поел от екипа:��6$   $��SemiBold"�6:   ���#34495E$��10q�6e   �� 5W      -   5        $ cmbRequestTeam��5W   #   $
��auto��5X   #   $��30��5Y   #   $��5q�5Z   #   ���#F7F9F9 �� DisplayMemberPath$
 Name��5\   #    �� SelectedValuePath$
 Name��5]   #   5`      5a      �� 5d      ���#27AE60$ 8��6.   $	��350��6?   $��50��6K   $��0,20,0,0q�6W   $��Center=�6i   ._�5e      �� -   5        $%��✅ Добави уебсайт��5e      
�������$��16(�5f   0   $
��Bold"�6>   $	��350��5g      $��50��6)   $��0q�65   
������ 6I   5h      5i      5j      5k      5l      