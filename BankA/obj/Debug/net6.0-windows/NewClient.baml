   M S B A M L   `   `   `  ����@  <BankA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BankABankA  5          BankA.NewClient   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:BankA     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5        � Title$  	NewClient��5      $	��500��5	      $	��250��5
       +� FocusedElement 5      ��  �� ElementName$ lastname�� � DataContext 5        BankA.NewClientViewModel  5   
    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��6    �� Command 6!   �� *Ok+.��5      �  � ShowGridLines$ False��6    � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   .+�5   
   � $��0��6   	 � ColumnDefinitions	 5      �� 5      $	��20* �6#   �� 5      $	��30* �6#   .+�5      �� $��0��6   
 � 	Resources
 5        �� 
TargetType �� Property �� Value��%�   '��       �� 5!      " ��6   .�5"      ��  � ��$ 
10,10,10,0q�63   �� 5#       � ��$ Right=�6@   �� 5$       � ��
 �����68   5%      5&      .+�5'      �� .��6   Фамилия6#   �� 5(      .��6   Имя6   �� 5)      .��6   Отчество6$   �� 5*      .��6   Телефон6#   �� 5+      .��6   Паспорт6#   5,      �� 5/      $��1��6   
 50      ��%Z   '��       �� 51      " ��6   .�52      ��  � ��$ 
10,10,50,0q�63   53      54      .+�55      �� -   5         ��Name$ lastname��55   Y   ��6   �� *
LastName+ �� UpdateSourceTrigger$ PropertyChanged=�6Y   �� 56      ��6   �� *	FirstName+$ PropertyChanged=��� 57      ��6   �� *
MiddleName+$ PropertyChanged=��� 58      ��6   �� *Phone+$ PropertyChanged=��� 59      ��6   �� *
Passport+$ PropertyChanged=�5:      5;      � 5<   
   $��1��6   	 5=      �� 5>      �� 5?      $��40 �6#   �� 5@      .+�5C      �� $��0��6   $��50��5D      $��25��6   $��right=�5E      ��6-   �� *Ok+.��6D   Ок5G      �� 5I      $��2��6   $��90��5J      $��25��6   $
��Left=�5K      ��6,   �� *Cansel+.��6H   Отмена5M      5N      5O      5P      