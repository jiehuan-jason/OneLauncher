﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using OneLauncher.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using OneLauncher.Codes;
using Avalonia.Threading;

namespace OneLauncher.Views;

public partial class MainWindow : Window
{
    public readonly Home HomePage;
    public readonly version versionPage;
    public readonly download downloadPage;
    public readonly settings settingsPage;
    public readonly account accountPage;
    public readonly ModsBrowser modsBrowserPage;
    public static MainWindow mainwindow;
    public MainWindow()
    {
        InitializeComponent();
        mainwindow = this;
        HomePage = new Home();
        versionPage = new version();
        accountPage = new account();
        modsBrowserPage = new ModsBrowser();
        downloadPage = new download();
        settingsPage = new settings();
        PageContent.Content = HomePage;
    }
    public enum MainPage
    {
        HomePage,VersionPage,AccountPage,DownloadPage,SettingsPage, ModsBrowserPage
    }
    /// <summary>
    /// 手动管理页面切换
    /// </summary>
    /// <param ID="page">主页面</param>
    public void MainPageControl(MainPage page)
    {
        switch(page)
        {
            case MainPage.HomePage:
                PageContent.Content = HomePage;
                break;
            case MainPage.VersionPage:
                PageContent.Content = versionPage;
                break;
            case MainPage.AccountPage:
                PageContent.Content = accountPage;
                break;
            case MainPage.ModsBrowserPage:
                PageContent.Content = modsBrowserPage;
                break;
            case MainPage.DownloadPage:
                PageContent.Content = downloadPage;
                break;
            case MainPage.SettingsPage:
                PageContent.Content = settingsPage;
                break;
        }
    }
    /// <summary>
    /// 在右下角显示提示信息
    /// </summary>
    /// <param ID="text">提示信息内容</param>
    public Task ShowFlyout(string text,bool IsWarning = false)
    {
        return Dispatcher.UIThread.InvokeAsync(async() =>
        {
            FytFkA.Text = text;
            if (IsWarning)
                FytB.Background = new SolidColorBrush(Colors.Red);
            else
                FytB.Background = new SolidColorBrush(Colors.LightBlue);
            FytB.IsVisible = true;
            await Task.Delay(3000);
            FytB.IsVisible = false;
        });
    }
    // 统一事件方法
    private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        var listBox = sender as ListBox;
        if (listBox == null) return;

        var selectedItem = listBox.SelectedItem as ListBoxItem;
        if (selectedItem == null) return;

        switch (selectedItem.Tag)
        {
            case "Home":
                PageContent.Content = HomePage;
                break;
            case "Version":
                PageContent.Content = versionPage;
                break;
            case "Account":
                PageContent.Content = accountPage;
                break;
            case "ModsBrowser":
                PageContent.Content = modsBrowserPage;
                break;
            case "Download":
                PageContent.Content = downloadPage;
                break;
            case "Settings":
                PageContent.Content = settingsPage;
                break;
        }
        
    }
    // 切换侧边栏展开/折叠
    private void MangePaneOpenAndClose(bool IsOpen)
    {
        HomeText.IsVisible = IsOpen;
        VersionText.IsVisible = IsOpen;
        AccountText.IsVisible = IsOpen;
        DownloadText.IsVisible = IsOpen;
        SettingsText.IsVisible = IsOpen;
        ModsBrowserText.IsVisible = IsOpen;
        SidebarSplitView.IsPaneOpen = IsOpen;
    }
    // 鼠标进入事件
    private void Sb_in(object? sender, Avalonia.Input.PointerEventArgs e) => MangePaneOpenAndClose(true);
    // 鼠标离开事件
    private void Sb_out(object? sender, Avalonia.Input.PointerEventArgs e) => MangePaneOpenAndClose(false);
}