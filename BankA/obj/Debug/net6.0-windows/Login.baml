   M S B A M L   `   `   `  ����@  <BankA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BankABankA  5          BankA.Login   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:BankA     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  Authorization��5      $	��500��5	      $	��350��5
       +� FocusedElement 5      ��  �� ElementName$ login�� � DataContext 5         BankA.LoginViewModel  5   
    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��6    �� Command 6!   �� *Auth+ � 	Resources 5      ��%    5      .��5       �  � RowDefinitions 5!   
   �� 5"      $��20 �6   �� 5#      $��* �6   �� 5$      $��20 �6   .+�5&   
   �� $��1��6   $��20q�6"   .+�5'      �� Login6   �� 5*      -   5        	 ��Name$	 login��5+      $	��0,5q�5,      

 � Tag$	
 123��5-      ��5.      ��  �� Path$ Login� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� 51      
Password6"   �� 52      $��23��53      $	��0,5q�54      ��55      �� *
Password+$ PropertyChanged=�$ TwoWay=��� 56      $	��200��57      $��0,5,0,30q�58      $��Center=�59      $
��Ок��5;      ��5:      �� *Auth+5;      �� 5<      ,*Пользователи в системе60   �� 5=      A� 5>       A� Columns$ 3��6    A� Rows$ 3��6&   .+�5?      �� $��login��6   �� 5@      $��password��6   �� 5A      $
��role��6   �� 5B      ��6   �� *Employees[0].Login+�� 5C      $	��123��6   �� 5D      ��6   �� *Employees[0].+�� 5E      ��6   �� *Employees[1].Login+�� 5F      $	��789��6   �� 5G      ��6   �� *Employees[1].+5H      5I      5J      5K      