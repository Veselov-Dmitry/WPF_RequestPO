   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Request7   1Request.Styles.CustomizedWindow.VS2012WindowStyleW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TitleBarBackgroundBrush  �� Color  VS2012WindowBorderBrush # VS2012WindowBorderBrushInactive   VS2012WindowStatusForeground ( $VS2012WindowStatusForegroundInactive # VS2012WindowStyleTitleBarButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value	 �� 
TargetName  VS2012WindowStyle
 �� Padding J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   Opacity   
BlurRadius   ShadowDepth   	Direction � ColumnDefinitions ��Name �� Path �� Mode �� RelativeSource ��Name J� Opacity ��Name �� Stretch �� EdgeMode �� AncestorType F�Name ��Name � Cursor	 �� X1  � ActualWidth	! �� X2	" �� Y1	# �� Y2$ � ActualHeight% ��Name& �� 	Resources' �� Triggers( �� Binding) �� Value  PhoneForegroundBrush  PhoneRadioCheckBoxBrush 	 PhoneDisabledBrush 
 PhoneAccentBrush ) %PhoneButtonBasePressedForegroundBrush  PhoneFontFamilySemiBold  PhoneFontFamilyNormal  PhoneFontSizeMedium ! PhoneTouchTargetLargeOverhang* u� Left
+ u� Top, u� Right- u� Bottom  PhoneTouchTargetOverhang  PhoneBorderThickness  PhoneButtonBase'  !System.Windows.VisualStateManager.   VisualStateGroups%  System.Windows.VisualStateGroup/   States   System.Windows.VisualState0   
Storyboard1 �� TargetProperty2 �� 
TargetName3 R� KeyTime4 R� Value5 �� CornerRadius6 ��Name   PhoneRadioButtonCheckBoxBase7 �� BasedOn  Win8RadioButton8 g� Duration	9 W� To: �Name; � UseLayoutRounding  NormalBrush  NormalBorderBrush  IsEnabledFalseBrush  MarginThicknessCheckBox  DiscreteCheckBoxStyle< �� Opacity= j� Stretch> !� Visual? �� StrokeStartLineCap@ �� StrokeEndLineCapA �� StrokeLineJoin  BuroTextBlock  HederGridLabelText  RightsHeaderWrapPanel  RightsHeaderTextBlock  TwitterTextBoxStyle-  'System.Windows.Input.KeyboardNavigationB � 	Resources  KeyC g� 	BeginTimeD O� KeyTimeE O� Value  System.Windows.VisibilityF   VisibleG   	CollapsedH J� IsHitTestVisibleI ��Name
J � TagK ��NameL ]� 
ConditionsM �� PropertyN �� ValueO N� ExitActionsP ��Name   
FocusedOffQ N� EnterActions ! 	FocusedOn " 
DisabledOn # DisabledOff $ WarpPOutListBox % UsersListBox & UsersListBoxItem ' Buttons   D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     .sys&clr-namespace:System;assembly=mscorlib  8d2http://schemas.microsoft.com/expression/blend/2008  %�*  &	        &	      &	 &     &	 9     &	 L     &	 _     7  &	 �    7  7   7  7  7  7  7  7  7  7  &	     &	     &		 .    &	
 A    &	 T    &	 g    &	 v    &	 �    &	 �    &	 �    &	 �    &	     7  7  7  7  7  7  7 
 7 	 7 	 7  &	 &    7  7  7  7  7  &	 �    7  7 	 7 	 7  7  7 	 7 
 7 
 7  &	 W    &	 j    &	 }    &	 �    &	 �    7  &	 %    &	 a    &	 �    &	 S    7  &	 �    7   7 ! 7 " 7 # &	$ Z$    &	% 	%    &	& �%    &	' �&    �� $  #EFEFF2���� $  #99cc99���� $  #999999���� $  #FFFFFF���� $  #FFFFFF���� " ��.���  � |� .  ��  � �� �� " ��.R�� .+��� $ PART_border��
������ ��  P�  �@J�IsMouseOver . . ��� $	 PART_border�� � �� �#FFF�� $	 PART_border�� �@J�Opacity$	 0.7Q�P�  � �� . . ��� $	 PART_border��8    � ���� $	 PART_border�� �@J�Opacity$ 1Q���  � �� �#FFF�� " �.���  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  � �� �� " �.R�� $��7q�.+��� $ PART_Border��$
��Auto��$
��Auto�����#EFEFF28��  $��1q�$
 0q�   $ black��$	 0.5Q�$ 7Q�$ 2Q�$	 315Q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��-   $	��Top=�8�� $��0q�._�� $��32�� �� $��36 ��� �� $��34 ��� $��34 ��� $��34 �.+��� $
 Icon��-   $��0��$��Right=�$��4,-7,0,7q�$��32��$��32������ $
 Icon�$ OneWay=� �� *TemplatedParent+�� $ Caption��$��1��$
��Left=�$��Center=�$��4,0,0,0q�$��Segoe UI)�$��12(�$
 0.66Q����� $ Title�$ OneWay=� �� *TemplatedParent+�� $ 	MinButton��-   $��2��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $��1��$
 None=�$ Aliased=�=����     z � z  z �    ��j � z ��j �    ��[ � z ��[ 	����  �� $ ButtonL�$ 
Foreground��� $ 	MaxButton��-   $��3��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $ PART_MaxButton_Path��$��1��$
 None=�$ Aliased=�X����        � z     � z  z �     z �        �    @B � z @B �    �� � z �� 	����  �� $ ButtonL�$ 
Foreground��� $ CloseButton��-   $��4��$	��Top=�$��34��$��26��8�� .��� $��9��$��9��.+�F� $	��1.5��$
 None=�+����        � z  z �     z � z     	����  �� $ ButtonL�$ 
Foreground��� �� $ lnSizeNorth��-   
������ $ SizeNS��$ 1��#! z  $" 1��$# 1��$��3���� $ lnSizeSouth��-   
������ $��Bottom=�$ SizeNS��$ 1��#! z  #" z$ ## z$ $��3���� $ 
lnSizeWest��-   
������ $ SizeWE��$ 1��$! 1��$" 1��## z$ $��3���� $ 
lnSizeEast��-	   
������ $��Right=�$ SizeWE��# z  #! z  $" 1��## z$ $��3���� $% rectSizeNorthWest��-
   $ SizeNWSE��
������ $��5��$��5��$	��Top=�$
��Left=��� $% rectSizeNorthEast��-   $ SizeNESW��
������ $��5��$��5��$	��Top=�$��Right=��� $% rectSizeSouthWest��-   $ SizeNESW��
������ $��5��$��5��$��Bottom=�$
��Left=��� $% rectSizeSouthEast��-   $ SizeNWSE��
������ $��5��$��5��$��Bottom=�$��Right=� P�  �@�WindowState$ 	Maximized=�. ��� $	 PART_MaxButton_Path�� � ��| ��    ��- �@KL ��- �@KL  z �     z �    ��- �     	= �@KL  	= ���-     � z     � z @KL ���j @KL ���- @B � z @B 	P�  �@�IsActive .  . ��� $	 PART_Border��8   � ��& ���� '��       " ��' �� ( �� *
IsActive+ �� $ WindowL�) True.a���  � ��8  ��  � ��8  �� ( �� *
IsActive+ �� $ WindowL�	) False.a���  � ��8  ��  � ��8 	 �� $  #666633���� $  #ffffff���� $  #cccccc���� $  #99CC99���� $  #ff0000��*�	Arial)�*�	Arial)�X�16Q�u� $* 15Q�$+ 0Q�$, 0Q�$- 0Q�u� $* 0Q�$+ 0Q�$, 0Q�$- 0Q�u� $* 0Q�$+ 0Q�$, 0Q�$- 0Q��� " ��.���  � ��
 ���� ��  � ��8   ��  � ��8  ��  � ��8  ��  � ��8  ��  � ��8  ��  � ��$ 	10,5,10,6q���  � �� �� " ��.R�� 
������ .   CommonStates��./   
Normal��  	MouseOver��  Pressed��.0 �� .��T� $1 
Foreground�$2 ContentContainer��..�i� $3 0��84  T� $1 
Background�$2 ButtonBackground��..�i� $3 0��84    Disabled��.0 �� .��T� $1 
Foreground�$2 ContentContainer��..�i� $3 0��84  T� $1 BorderBrush�$2 ButtonBackground��..�i� $3 0��84  T� $1 
Background�$2 ButtonBackground��..�i� $3 0��$4 Transparent��.+��� $ ButtonBackground��#��z��#��z��#��z��$5 0��8��	 ._��� $6 ContentContainer��#��z��#��z��#��z��#��z��#��z��#��z���� 87   " d�.���  � ��8  ��  � ��8  ��  � ��8  ��  � ��8  ��  � ��$
 Left=���  � ��$ Center=���  � ��$ 0q��� 87   " 	�.���  � �� �� " 	�.R�� 
������ .   CommonStates��./   
Normal��  	MouseOver��  Pressed��.0 �� .��W� $2 PressedBorder��$1 (UIElement.Opacity)�$8 0D�$9 1X�  Disabled��.0 �� .��W� $2 NormalBorder��$1 (UIElement.Opacity)�$8 0D�$9 0X�T� $1 
Foreground�$2 ContentContainer��..�i� $3 0��84    CheckStates��./   Checked��.0 �� .��W� $2 NormalBorder��$1 (UIElement.Opacity)�$8 0D�$9 0X�W� $2 CheckedBorder��$1 (UIElement.Opacity)�$8 0D�$9 1X�W� $2 
CornerPath��$1 (UIElement.Opacity)�$8 0D�$9 1X�W� $2 	CheckPath��$1 (UIElement.Opacity)�$8 0D�$9 1X�  	Unchecked��.0 �� .��T� $1 
Foreground�$2 ContentContainer��..�i� $3 0��84    ValidationStates��  FocusStates��.+�� 8�� .+��� $6 ContentContainer��#��z��#��z��#��z��#��z��#��z��#��z��$��
15,15,30,0q�#��z��#��z��� $: CheckElements��8�� .+��� $ NormalBorder��$��3q�8�� $ 1Q��� $ CheckedBorder��$��3q�8�� $ 0Q��� $ PressedBorder��$��3q�#��z��$ 0Q�F� $ 
CornerPath��"���� ��    р�����р��    	$��Right=�$��50��$��0q�$
 Fill=�8�� ; .  $	��Top=�$��50��$ 0Q�F� $ 	CheckPath��"����XŊ<�	�MЅ{Ѱz�.Ŏ 	$��Right=�$��16.25��$��0,6.375,6,0q�$
 Fill=�; .  $	��Top=�$��18.625��8�� $��4��$ 0Q��� $  #FFFFFF���� $  #FFFFFF���� $  #CCCCCC��u� $* 5Q�$+ 0Q�$, 0Q�$- 0Q��� " ��.���  �@J�SnapsToDevicePixels . ��  �@J�IsHitTestVisible . ��  � ��$ Arual)���  � ��# �  ��  � ��# �  ��  � ��8   ��  � �� �� " ��.R��� ._�� .+��� #��z��#��z��#��z���� ��!� $	< 1.0Q�$
= None=�> F� $
��path��$	 0.1Q�"����    @KL ���- ��� р��     	���	#FF3838BA$	��1.5��$? Round=�$@ Round=�$A Round=� P�  �@d�	IsChecked$
 TrueY�. ��� $
	 path�� �@J�Opacity$	 1.0Q��� $
	 path�� � �� �	#FF70A16F�� $
	 path�� � ��$	 2.5��P�  �@d�	IsChecked$ FalseY�. ��� # �   � ���� " ��.���  � ��$ 16(���  � ��$ Arial)��� " ��.���  � ��$ Arial)���  � ��$
 Bold"���  � ��$ 16(���  � ��$ Center=��� " �.���  � ��$
 auto����  � ��$ Center=���  �@�Orientation$ 
Horizontal=���  � ��$ 1,0,0,1q��� " ��.���  � ��8   ��  � ��$ 14(���  � ��$ 13���� " ��.���  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  �@J�	AllowDrop . ��  �@��TextWrapping$
 Wrap=���  �@��AcceptsReturn . ��  � ��
 �������  � ��$ Stretch=���  � ��$ Stretch=���  � ��$ Trebuchet MS)���  � ��$ 16(���  �@�UseLayoutRounding . ��  � ��$ 4q���  � ��$ 1q���  � ��
 �   ���  � �� �#e3e3e3��  � �� �� " ��B ���� 	FocusedOn .��U� $C 00:00:00X�$2 FocusVisualElement��$1 (UIElement.Opacity)�.\��� $D 00:00:00.1000000��$E 1Q��� 
FocusedOff .��U� $C 00:00:00X�$2 FocusVisualElement��$1 (UIElement.Opacity)�.\��� $D 00:00:00.3000000��$E 0Q��� 
DisabledOn .��T� $C 00:00:00X�$2 DisabledVisualElement��$1 (UIElement.Visibility)�..�i� $3 00:00:00.1000000��#4 ZF �� DisabledOff .��T� $C 00:00:00X�$2 DisabledVisualElement��$1 (UIElement.Visibility)�..�i� $3 00:00:00.1000000��#4 ZG .R�� .+��� $ 
BorderBase��$��3q�
�������#��z��#��z��$5 3���� $ FocusVisualElement��$��0q����	#FFECF5FD���	#FFECF5FD#��z��$5 7��H .  $ 0Q�._��� $ InnerFocusVisualElement�����	#FFE3F1FC���	#FFE3F1FC#��z��$5 5��H .  $ 1Q�._��� $ InnerFocusVisualElement2�����	#FFD9ECFA���	#FFD9ECFA#��z��$5 3��H .  $ 1Q�._��� $ InnerFocusVisualElement3��#��z�����	#FF7DBEF1#��z��$5 3��H .  $ 1Q��� $I 
TextPrompt��
���iii�$��1��$��3q�#��zJ $y�	Collapsed=�|�.  �� $K PART_ContentHost��$��1q�#��z��$y�Visible=��� $ DisabledVisualElement��$��3q����	#A5FFFFFF���	#59C0C0C0#��z��$5 3��H .  $y�	Collapsed=��� $ ReadOnlyVisualElement��$��3q����	#66FFFFFF$
5 2.75��$y�	Collapsed=� ]� L �� M �@J�	IsFocusedN .  �� M � ��$N  ��/��� $	 
TextPrompt�� � y�$ Visible=�P�  �@J�	IsFocused . O �� $ P FocusedOff_BeginStoryboard��8��  Q �� $P FocusedOn_BeginStoryboard��8�� P�  � {� .  Q �� $ P DisabledOn_BeginStoryboard��8�� . ���  � ��
 �����P�  � {� . Q �� 8�� . ���  � �� �	#FF000000�� " ��.���  � ��$ 2q���  � �� �� *usersRow002+��  � ��
 �������  �@��CornerRadius$ 2����  � ��
 �������  � ��$ 1q��� " ��.���  � ��$ 12(���  � ��
 ���� ��  � ��$	 0.1q���  � ��$ Hidden=���  � ��$ Hidden=��� " ��.���  � ��$
 Bold"���  � ��
 �2�2���  � ��
 �����' P�  �@��AlternationIndex$ 1��. ���  � ��
 �������  � ��
 �   �P�  �@��AlternationIndex$ 0��. ���  � �� �#99cc99��  � ��
 ������� " ��.���  � ��$ 18(���  � �� �� " ��.R��� $ bdr_main��$5 1��$��4q�$��1q�
�������
������ ._��� $��Center=�$��Center=�$��8,6,8,6q�$��Content�� P�  �@J�IsMouseOver . . ��� $	 bdr_main�� � �� �#99CC99P�  � �� . . ��� $	 bdr_main�� � ��
 ��  ��� $	 bdr_main�� � �� �#99bb99P�  �@J�	IsFocused . . ��� $	 bdr_main�� � ��
 ��  ��� $	 bdr_main�� � �� �#78dd78