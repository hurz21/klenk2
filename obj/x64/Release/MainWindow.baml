   M S B A M L   `   `   `  ���� D  @klenkGmbh, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:klenkGmbh	klenkGmbh      klenkGmbh.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:klenkGmbh     -   
�������  � WindowStartupLocation$  CenterScreen=� � Title$B <Klenk-Eigentümersuche (c)  Dr. Feinen, Kreis Offenbach 2021��$	��450��$	��800��.��]� .+��� -   $��spTools��$	��Top=�.+��� $��5q�.+��� 
�������.@��� $��	FS suchen��.����  �� Orientation$ 
Horizontal=�$��5q�.+��� $ 
Horizontal=�$��5q�$��25��.+��� $��80��.��FS:�� -   $
��tbFS��$	��190��{�.  � ToolTip$ 21 chars��$��FS0607290100020000000��$��20���� .���� $ 
Horizontal=�.+��� -   $��	btnsuche2��$��Suchen��$��5q��� -   $��5q�$��60��$	 bla��.��
Bürgergis�� -   $��5q�$��60��$	 bla��.��GMaps�� -   $��5q�$��60��$	 bla��.��G3D�� $��Detailsuche��.���� $ 
Horizontal=�$��5q�.+��� $ 
Horizontal=�$��5q�$��25��.+��� $��80��.��
Gemarkung:�� -   $��tbGemarkung��$��86��{�. $�� ��$��20���� -	   $��cmbgemarkung��$��20�� �� DisplayMemberPath$
 buch�� �� SelectedValuePath$ adress��$��20������ �� $ 
Horizontal=�$��25��.+��� $��35�� �� Padding$ 5q�Flur:�� -
   $��tbflur��$��30��{�. $�� ��$��20���� -   $��cmbFlur��$
 buch��$ adress��$��20��$��20������ �� $ 
Horizontal=�$��25��.+��� $��50��.��
Zähler:�� -   $��	tbZaehler��$��38��{�. $�� ��$��20���� -   $��
cmbZaehler��$
 buch��$ adress��$��20��$��20������ �� $ 
Horizontal=�$��25��.+��� $��50��.��	Nenner:�� -   $��tbNenner��$��44��{�. $��20���� -   $��	cmbNenner��$
 buch��$ adress��$��20��$��20������ �� .���� $ 
Horizontal=�.+��� -   $��btnsuche��$��Suchen��$��5q��� -   $��5q�$��60��$	 bla��.��
Bürgergis�� -   $��5q�$��60��$	 bla��.��GMaps�� $��Optionen��.���� $ 
Horizontal=�$��5q�.+��� $ 
Horizontal=�$��5q�$��25��.+��� $��80��.��	Logfiles:�� -   $��btnLogfilesdir��.��Zeige Logfiles�� -    ��Name$ 	spErgenis��.+��� -   $��tbInfo��$��50��$	��600��$��5q�$y�	Collapsed=��� -   $	��scv��$
��Auto=�$
��Auto=�$	��220��$	��760��.��&   System.Windows.Controls.DataGrid  -     Name$	 dg1��$	��200��$	��740��	 %� IsVirtualizing	 . 
   EnableRowVirtualization
 . !   EnableColumnVirtualization .    CanUserAddRows .     SelectionMode$ Single=�   SelectionUnit$ FullRow=�   AutoGenerateColumns .     MinRowHeight$	 100Q�$��Normal"����� * +   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 	Gemarkung��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50  ^� ToolTip$ Quelle��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	gemarkung+  $
 Flur��$ 10 $ Quelle�� �� *flur+  $ Z��$ 30 $ Quelle�� �� *	zaehler+  $ N��$ 30 $ Quelle�� �� *nenner+  $ Adresse��   MaxWidth$	 600Q� �� *adress+   ElementStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value$
 Wrap=���  �@�ToolTip �� *adress+  $ 	Buchblatt��$	 600Q� �� *buch+ �� " ��.���  � ��$ NoWrap=���  �@�ToolTip �� *buch+