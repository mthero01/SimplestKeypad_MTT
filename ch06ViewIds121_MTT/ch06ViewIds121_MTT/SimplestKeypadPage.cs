/** [!]. ABOUT
 * 
 **/
 /// [I]. HEAD
 ///  A] IMPORTS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Text;
using Windows.UI.Input;
using Windows.UI.ViewManagement;

using Xamarin; //<?!>

/// <summary> </summary>
namespace ch06ViewIds121_MTT {
    public sealed class SimplestKeypadPage : ContentPage {
        /// B] UI elements
        Label display_lbl;
        Button backspace_btn;

        /// C] Constructor
        public SimplestKeypadPage()
        {
            /// 01. Create a (vertical) stack for the entire keypad.
            StackLayout mainStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };// /stackLayout 'mainStack'

            /// 02. Define the label and Add it as the first child.
            display_lbl = new Label
            {
                FontSize = Device.GetNamedSize( NamedSize.Large.typeof(Label) ),
                VerticalOptions = LayoutOptions.Center,
                NormalTextAlignment = TextAlignment.End
            };// /lbl 'displayLabel'
            mainStack.Children.Add(display_lbl);

            /// 03. Define the ←BSp Button and add it.
            backspace_btn = new Button
            {
                Text = "\u21E6", // the ←BSp char
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                /// Disable backspace w/o text.
                IsEnabled = false
            };// /btn 'backspace_btn'
            backspace_btn.Clicked += onBackspaceButtonClicked;
            mainStack.Children.Add(backspace_btn);

            /// 04. Define & add the 10 numeral keys.
            ///  an empty array of UI objects
            StackLayout rowStack = null;

            /// Count up through 10 buttons.
            for (int numeral = 1; numeral <= 10; numeral++)
            {
                /// Determine row by modulus of '3'.
                if (numeral - 1) % 3 == 0 
                {
                    rowStack = newStackLayout 
                    {
                        Orientation = StackOrientation.Horizontal
                    }// /row
                }// /if %3
                mainStack.Children.Add(rowStack);

                /// 
                Button digit_btn = new Button
                {
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),

                    /// Label the button with the current numeral.
                    Text = numeral % 10.ToString(), // 10→0
                    StyleId = (numeral % 10).ToString()
                }// /btn 'digit_btn' 
                digit_btn.Clicked += onDigitButtonClicked;

                /// Let the '0' button have more space.
                if(numeral % 10) //if(numeral==10)??
                {
                    digit_btn.HorizontalOptions = LayoutOptions.FillAndExpand;
                }// /if numeral-10
                rowStack.Children.Add(digit_btn);
            }// /for 'numeral' (1-10)

            this.Content = mainStack;

            /// E] Event Handlers
            /// 01.
            void onDigitButtonClicked(object sender, EventArgs args)
            {
                /// a) Recognize the button.
                Button this_btn = (Button)sender;

                /// b) Display the digit.
                display_lbl.Text += (string)this_btn.StyleId;

                /// c) Allow a backspace, since ther is text.
                backspace_btn.IsEnabled = true;
            }// /hdlr 'onDigitButtonClicked'

            /// 02. 
            void onBackspaceButtonClicked(object sender, EventArgs args)
            {
                string text = display_lbl.Text;
                /// Transfer all characters, except the last one.
                display_lbl.Text = text.Substring(0, text.Length - 1);
                /// If no characters remain, disable the backspace.
                backspace_btn.IsEnabled = display_lbl.Text.Length > 0;
            }// /hdlr 'onBackspaceButtonClicked'
        }// /cxtr 'SimplestKeypadPage'

    }// /cla 'ViewIds'
}// /ns 'ch06ViewIds121_MTT'
