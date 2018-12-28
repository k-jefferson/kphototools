﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KPhotoToolsLibrary;

namespace KPhotoTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsProcessingTask { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();

            TextBox_RawExtension.Text = ".CR2";
            TextBox_JpgExtension.Text = ".JPG";
        }

        private async void Button_StartTask_ClickAsync(object sender, RoutedEventArgs e)
        {
            IsProcessingTask = true;
            try
            {
                // Delete raw from jpg
                if (RadioButton_DeleteRawFromJpg.IsChecked.HasValue && RadioButton_DeleteRawFromJpg.IsChecked.Value)
                {
                    TextBlock_Log.Text = "Start deleting raw from jpg";
                    await DeleteRawFromJpg.Start(
                        TextBox_WorkingDirectory.Text.Trim(),
                        TextBox_JpgExtension.Text.Trim(),
                        TextBox_RawExtension.Text.Trim()
                    );
                }
                // Move raws to a subfolder
                else if (RadioButton_MoveRaws.IsChecked.HasValue && RadioButton_MoveRaws.IsChecked.Value)
                {
                    TextBlock_Log.Text = "Start moving raws in a subdirectory";
                    await IsolateRawFiles.Start(
                        TextBox_WorkingDirectory.Text.Trim(),
                        TextBox_RawExtension.Text.Trim()
                    );
                }
            }
            catch (Exception ex)
            {
                TextBlock_Log.Text += "\r\n" + ex.Message;
            }

            IsProcessingTask = false;
            TextBlock_Log.Text += "\r\nFinished";
        }
    }
}
