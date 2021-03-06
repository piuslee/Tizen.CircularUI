---
uid: Tizen.Wearable.CircularUI.doc.CircleStepper
summary: CircleStepper control guide
---
# CircleStepper

`CircleStepper` is extension of [`Xamarin.Forms.Stepper`](https://developer.xamarin.com/api/type/Xamarin.Forms.Stepper/).
Marker color, MarkerLineWidth, and LabelFormat have been added to the existing [`Xamarin.Forms.Stepper`](https://developer.xamarin.com/api/type/Xamarin.Forms.Stepper/).
You can change value with bezel interaction.
In order to receive bezel interaction, it must be registered as `RotaryEventConsumer` property of [`CirclePage`](xref:Tizen.Wearable.CircularUI.doc.CirclePage).

![](data/CircleStepper.png)

**WARNNING: [CircleListView](xref:Tizen.Wearable.CircularUI.doc.CircleListView), [CircleDateTimeSelector](xref:Tizen.Wearable.CircularUI.doc.CircleDateTimeSelector), [CircleScrollView](xref:Tizen.Wearable.CircularUI.doc.CircleScrollView), [CircleStepper](xref:Tizen.Wearable.CircularUI.doc.CircleStepper) must be contained by `CirclePage` or [CircleSurfaceEffectBehavior](xref:Tizen.Wearable.CircularUI.doc.CircleSurfaceEffectBehavior) should be added in [Behaviors](https://developer.xamarin.com/api/type/Xamarin.Forms.Behavior/) of [Page](https://developer.xamarin.com/api/type/Xamarin.Forms.Page/) that contain these Control. If other `page` contains these control. It may cause exception or control can not be displayed.**

## Adding CircleStepper at CirclePage

You can set CircleStepper at [`CirclePage.Content`](xref:Tizen.Wearable.CircularUI.doc.CirclePage). The following code show CirclePage set content with `CircleStepper`.
`RotaryFocusTargetName` attribute sets the current focused control that is handled by rotating and display the focused control's circle object.
If you don't set this value properly, control can't receive rotary event.

`CircleDateTimeSelector` has the following properties:

- LabelFormat : Gets or sets format in which Value is shown.
- MarkerColor : [`Xamarin.Forms.Color`](https://developer.xamarin.com/api/type/Xamarin.Forms.Color/). Change color of marker to select value.
- MarkerLineWidth : Gets or sets length of marker.

For more information. Please refer to below links

- [CircleStepper API reference](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.CircleStepper.html)
- [Xamarin.Forms.Stepper API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.Stepper/)
- [Xamarin.Forms.Stepper Guide](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/controls/views#stepper)

_This guide's code example use XUIComponent's SpinnerDefault of CircleSpinner code at the sample\XUIComponents\UIComponents\UIComponents\Samples\CircleSpinner\SpinnerViewModel.cs and SpinnerDefault.xaml_

**C# file**

```cs

    public class SpinnerViewModel : INotifyPropertyChanged
    {
        double _value= 9.0;
        ...

        public double Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged();
            }
        }
```

**XAML file**

```xml

<w:CirclePage
    x:Class="UIComponents.Samples.CircleSpinner.SpinnerDefault"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:UIComponents.Samples.CircleSpinner"
    xmlns:sys="clr-namespace:System;assembly=netstandard"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    RotaryFocusTargetName="stepper">
    <w:CirclePage.BindingContext>
        <local:SpinnerViewModel />
    </w:CirclePage.BindingContext>
    <w:CirclePage.Content>
        <StackLayout Padding="0,50,0,0" Orientation="Vertical">
            <Label
                FontAttributes="Bold"
                FontSize="11"
                HorizontalTextAlignment="Center"
                Text="Title"
                TextColor="#0FB4EF" />
            <Label
                FontSize="8"
                HorizontalTextAlignment="Center"
                Text="unit"
                TextColor="White" />
            <w:CircleStepper
                x:Name="stepper"
                HorizontalOptions="CenterAndExpand"
                Increment="7.5"
                LabelFormat="%1.1f"
                MarkerColor="Coral"
                Maximum="99.0"
                Minimum="9.0"
                Value="{Binding Value}" />
        </StackLayout>
    </w:CirclePage.Content>
    <w:CirclePage.ActionButton>
        <w:ActionButtonItem Command="{Binding ButtonPressedExit}" Text="SET" />
    </w:CirclePage.ActionButton>
</w:CirclePage>
```


