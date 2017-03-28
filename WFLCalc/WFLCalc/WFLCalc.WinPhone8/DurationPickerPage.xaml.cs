using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace WFLCalc.WinPhone8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DurationPickerPage : PhoneApplicationPage
    {
        private LoopingSelector _primarySelectorPart;
        private LoopingSelector _secondarySelectorPart;
        private LoopingSelector _tertiarySelectorPart;

        public DurationPickerPage()
        {
            this.InitializeComponent();

            // Hook up the data sources
            PrimarySelector.DataSource = (DataSource)(new TwentyFourHourDataSource());
            SecondarySelector.DataSource = new MinuteDataSource();
            TertiarySelector.DataSource = new SecondsDataSource();

            InitializeDateTimePickerPage(PrimarySelector, SecondarySelector, TertiarySelector);
        }
        /*
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += OnBackPressed;
            viewModel.SelectedTime = (TimeSpan)e.Parameter;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= OnBackPressed;
        }

        private void OnBackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }*/
        /// <summary>
        /// Initializes the DateTimePickerPageBase class; must be called from the subclass's constructor.
        /// </summary>
        /// <param name="primarySelector">Primary selector.</param>
        /// <param name="secondarySelector">Secondary selector.</param>
        /// <param name="tertiarySelector">Tertiary selector.</param>
        protected void InitializeDateTimePickerPage(LoopingSelector primarySelector, LoopingSelector secondarySelector, LoopingSelector tertiarySelector)
        {
            if (null == primarySelector)
            {
                throw new ArgumentNullException("primarySelector");
            }
            if (null == secondarySelector)
            {
                throw new ArgumentNullException("secondarySelector");
            }
            if (null == tertiarySelector)
            {
                throw new ArgumentNullException("tertiarySelector");
            }

            _primarySelectorPart = primarySelector;
            _secondarySelectorPart = secondarySelector;
            _tertiarySelectorPart = tertiarySelector;

            // Hook up to interesting events
            _primarySelectorPart.DataSource.SelectionChanged += OnDataSourceSelectionChanged;
            _secondarySelectorPart.DataSource.SelectionChanged += OnDataSourceSelectionChanged;
            _tertiarySelectorPart.DataSource.SelectionChanged += OnDataSourceSelectionChanged;
            _primarySelectorPart.IsExpandedChanged += OnSelectorIsExpandedChanged;
            _secondarySelectorPart.IsExpandedChanged += OnSelectorIsExpandedChanged;
            _tertiarySelectorPart.IsExpandedChanged += OnSelectorIsExpandedChanged;

            // Hide all selectors
            /*_primarySelectorPart.Visibility = Visibility.Collapsed;
            _secondarySelectorPart.Visibility = Visibility.Collapsed;
            _tertiarySelectorPart.Visibility = Visibility.Collapsed;

            // Position and reveal the culture-relevant selectors
            int column = 0;
            foreach (LoopingSelector selector in GetSelectorsOrderedByCulturePattern())
            {
                Grid.SetColumn(selector, column);
                selector.Visibility = Visibility.Visible;
                column++;
            }

            // Hook up to storyboard(s)
            FrameworkElement templateRoot = VisualTreeHelper.GetChild(this, 0) as FrameworkElement;
            if (null != templateRoot)
            {
                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(templateRoot))
                {
                    if (VisibilityGroupName == group.Name)
                    {
                        foreach (VisualState state in group.States)
                        {
                            if ((ClosedVisibilityStateName == state.Name) && (null != state.Storyboard))
                            {
                                _closedStoryboard = state.Storyboard;
                                _closedStoryboard.Completed += OnClosedStoryboardCompleted;
                            }
                        }
                    }
                }
            }

            // Customize the ApplicationBar Buttons by providing the right text
            if (null != ApplicationBar)
            {
                foreach (object obj in ApplicationBar.Buttons)
                {
                    IApplicationBarIconButton button = obj as IApplicationBarIconButton;
                    if (null != button)
                    {
                        if ("DONE" == button.Text)
                        {
                            button.Text = ControlResources.DateTimePickerDoneText;
                            button.Click += OnDoneButtonClick;
                        }
                        else if ("CANCEL" == button.Text)
                        {
                            button.Text = ControlResources.DateTimePickerCancelText;
                            button.Click += OnCancelButtonClick;
                        }
                    }
                }
            }

            // Play the Open state
            VisualStateManager.GoToState(this, OpenVisibilityStateName, true);*/
        }

        private void OnDataSourceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Push the selected item to all selectors
            DataSource dataSource = (DataSource)sender;
            _primarySelectorPart.DataSource.SelectedItem = dataSource.SelectedItem;
            _secondarySelectorPart.DataSource.SelectedItem = dataSource.SelectedItem;
            _tertiarySelectorPart.DataSource.SelectedItem = dataSource.SelectedItem;
        }

        private void OnSelectorIsExpandedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                // Ensure that only one selector is expanded at a time
                _primarySelectorPart.IsExpanded = (sender == _primarySelectorPart);
                _secondarySelectorPart.IsExpanded = (sender == _secondarySelectorPart);
                _tertiarySelectorPart.IsExpanded = (sender == _tertiarySelectorPart);
            }
        }
    }
}
