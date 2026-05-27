
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using Part_2.Services;
using Part_2.Models;

namespace Part_2;

public partial class MainWindow : Window
{
    private AudioPlayer _audioPlayer;
    private RespondingServices _responseService;
    private UserProfile _userProfile;


    public MainWindow()
    {
        _userProfile = new UserProfile();
        _audioPlayer = new AudioPlayer();
        _responseService = new RespondingServices();



        InitializeComponent();
        Loaded += MainWindow_Loaded;

    }



    private async void SendText(object sender, RoutedEventArgs e)
    {
        string userMessage = User_input.Text;


        if (string.IsNullOrWhiteSpace(userMessage))
            return;


        AddMessage(userMessage, true);


        AddMessage("Thinking...", false);

        await Task.Delay(1500);


        if (chatPanel.Children.Count > 0)
            chatPanel.Children.RemoveAt(chatPanel.Children.Count - 1);


        string botReply = _responseService.GetRespond(userMessage);


        AddMessage(botReply, false);

        User_input.Clear();
    }

    private void AddMessage(string message, bool isUser)
    {
        Border bubble = new Border
        {
            Background = isUser ? System.Windows.Media.Brushes.DarkGray : System.Windows.Media.Brushes.Pink,
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(10),
            Margin = new Thickness(0, 5, 0, 5),
            HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
            MaxWidth = 250
        };

        TextBlock text = new TextBlock
        {
            Text = message,
            TextWrapping = TextWrapping.Wrap
        };

        bubble.Child = text;
        chatPanel.Children.Add(bubble);
    }
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        AddMessage($"{_userProfile.GetArt()}", false);
        AddMessage("👋 Welcome to CyberBot! ", false);
        _audioPlayer.PlayNotification();
    }

}
