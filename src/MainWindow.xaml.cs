using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DeadlockCompanion
{
    /// <summary>
    /// Main window for Deadlock Build Companion.
    /// Shows hero statistics, item database, and build presets.
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataService _dataService = new DataService();

        public MainWindow()
        {
            InitializeComponent();
            RefreshHeroes();
            RefreshItems();
            RefreshBuilds();
            UpdateStatus("Ready");
        }

        private void BtnRefreshHeroes_Click(object sender, RoutedEventArgs e)
        {
            RefreshHeroes();
            UpdateStatus("Heroes refreshed");
        }

        private void RefreshHeroes()
        {
            var heroes = _dataService.LoadHeroes();
            lstHeroes.Items.Clear();
            foreach (var hero in heroes)
            {
                lstHeroes.Items.Add($"{hero.Name} | Health: {hero.Health} | WeaponDps: {hero.WeaponDps} | Range: {hero.Range} | Ammo: {hero.Ammo} | Reload: {hero.Reload} | Difficulty: {hero.Difficulty} | Role: {hero.Role}");
            }
        }

        private void BtnRefreshItems_Click(object sender, RoutedEventArgs e)
        {
            RefreshItems();
            UpdateStatus("Items refreshed");
        }

        private void TxtItemFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            var items = _dataService.LoadItems();
            IEnumerable<Item> filtered = items;

            var filter = txtItemFilter.Text.Trim();
            if (!string.IsNullOrEmpty(filter))
            {
                filtered = items.Where(i => i.Name.ToLower().Contains(filter.ToLower()));
            }

            lstItems.Items.Clear();
            foreach (var item in filtered)
            {
                lstItems.Items.Add($"{item.Name} | Category: {item.Category} | Tier: {item.Tier} | Souls: {item.Souls} | Activation: {item.Activation} | Stats: {item.Stats}");
            }
        }

        private void BtnRefreshBuilds_Click(object sender, RoutedEventArgs e)
        {
            RefreshBuilds();
            UpdateStatus("Builds refreshed");
        }

        private void RefreshBuilds()
        {
            var builds = _dataService.LoadBuilds();
            lstBuilds.Items.Clear();
            foreach (var build in builds)
            {
                var itemsText = string.Join(", ", build.Items);
                lstBuilds.Items.Add($"{build.HeroName} | {build.Name} | BuildId: {build.BuildId} | Items: {itemsText}");
            }
        }

        private void UpdateStatus(string message)
        {
            txtStatus.Text = message;
        }
    }
}
