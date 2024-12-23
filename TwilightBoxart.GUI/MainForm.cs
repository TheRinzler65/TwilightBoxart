﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwilightBoxart.GUI
{
    public partial class MainForm : Form
    {
        private readonly BoxartCrawler _crawler;
        private bool _isInitialized;
        private bool _isRunning;
        private readonly BoxartConfig _config = new();

        public MainForm()
        {
            InitializeComponent();
            var progress = new Progress<string>(Log);
            _crawler = new BoxartCrawler(progress);
        }

        private void DetectSd()
        {
            var path = "";
            var allDrives = new List<DriveInfo>();
            try
            {
                allDrives = DriveInfo.GetDrives().Where(c => c.DriveType == DriveType.Removable).ToList();
            }
            catch { }

            if (allDrives.Count > 0)
            {
                path = allDrives[0].RootDirectory.FullName;
                foreach (var drive in allDrives)
                {
                    if (Directory.Exists(Path.Combine(drive.RootDirectory.FullName, BoxartConfig.MagicDir)))
                    {
                        path = drive.RootDirectory.FullName;
                        break;
                    }
                }
            }
            else
            {
                Log("Aucune carte SD trouvée.");
            }

            if (!string.IsNullOrEmpty(path))
            {
                txtSdRoot.Text = path;
                SetUx();
            }
        }

        private void Log(string text)
        {
            this.UIThread(() => txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {text}{Environment.NewLine}"));
        }

        private async Task Go()
        {
            await _crawler.DownloadArt(txtSdRoot.Text, txtBoxart.Text, (int)numWidth.Value, (int)numHeight.Value, chkAspectRatio.Checked);
            _isRunning = false;
            this.UIThread(SetUx);
        }

        // UI STUFF
        private void SetUx()
        {
            if (!_isInitialized)
                return;

            btnBrowseBoxart.Enabled = chkManualBoxartLocation.Checked;

            if (!chkManualBoxartLocation.Checked && !string.IsNullOrEmpty(txtSdRoot.Text))
            {
                txtBoxart.Text = _config.GetBoxartPath(txtSdRoot.Text);
            }

            txtBoxart.ReadOnly = !chkManualBoxartLocation.Checked;

            numHeight.Visible = chkBoxartSize.Checked;
            numWidth.Visible = chkBoxartSize.Checked;
            lblSize1.Visible = chkBoxartSize.Checked;
            lblSize2.Visible = chkBoxartSize.Checked;

            if (!chkBoxartSize.Checked)
            {
                numWidth.Value = _config.BoxartWidth;
                numHeight.Value = _config.BoxartHeight;
            }

            btnStart.Enabled = !string.IsNullOrEmpty(txtSdRoot.Text) && !string.IsNullOrEmpty(txtBoxart.Text) && _isInitialized && !_isRunning;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(BoxartConfig.FileName))
                {
                    _config.Load();
                    chkAspectRatio.Checked = _config.AdjustAspectRatio;
                    Log($"{BoxartConfig.FileName} chargé.");
                }
            }
            catch
            {
                Log($"Erreur lors du chargement de {BoxartConfig.FileName}. Utilisation des paramètres par défaut.");
            }

            if (!string.IsNullOrEmpty(_config.SdRoot))
            {
                txtSdRoot.Text = _config.SdRoot;
            }
            else
            {
                DetectSd();
            }

            Task.Run(() =>
            {
                try
                {
                    _crawler.InitializeDb();
                }
                catch (Exception ex)
                {
                    Log("Avertissement : Impossible d'initialiser la base de données NoIntro ! Seules les ROMs DS fonctionneront. Erreur : " + ex);
                }

                _isInitialized = true;
                this.UIThread(SetUx);
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _isRunning = true;
            SetUx();
            Task.Run(Go);
        }

        private void btnBrowseSd_Click(object sender, EventArgs e)
        {
            if (folderBrowseDlg.ShowDialog() != DialogResult.OK) return;

            txtSdRoot.Text = folderBrowseDlg.SelectedPath;
            SetUx();
        }

        private void btnBrowseBoxart_Click(object sender, EventArgs e)
        {
            if (folderBrowseDlg.ShowDialog() == DialogResult.OK)
            {
                txtBoxart.Text = folderBrowseDlg.SelectedPath;
            }
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            DetectSd();
        }

        private void chkManualBoxartLocation_CheckedChanged(object sender, EventArgs e)
        {
            SetUx();
        }

        private void chkBoxartSize_CheckedChanged(object sender, EventArgs e)
        {
            SetUx();
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(BoxartConfig.Credits + Environment.NewLine + Environment.NewLine + "Visiter GitHub maintenant ?", "Bonjour", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {"https://github.com/TheRinzler65/TwilightBoxart"}") { CreateNoWindow = true });
            }
        }
    }
}
