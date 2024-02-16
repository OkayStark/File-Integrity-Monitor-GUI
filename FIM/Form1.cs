﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FIM
{
    public partial class Form1 : MetroSet_UI.Forms.MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
            if (!IsAdmin())
            {
                MessageBox.Show("Administrator permissions are required to run this program.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void Browse1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog1.SelectedPath;
                metroSetListBox1.Items.Add(selectedFolderPath);
            }
        }
        private void Browse2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog2.SelectedPath;
                metroSetListBox2.Items.Add(selectedFolderPath);
            }
        }
        private void Close1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void add1_Click(object sender, EventArgs e)
        {
            string enteredPath = metroSetTextBox1.Text;
            if (!string.IsNullOrWhiteSpace(enteredPath))
            {
                metroSetListBox1.Items.Add(enteredPath);
                metroSetTextBox1.Text = string.Empty;
            }
        }
        private void add2_Click(object sender, EventArgs e)
        {
            string enteredPath = metroSetTextBox2.Text;
            if (!string.IsNullOrWhiteSpace(enteredPath))
            {
                metroSetListBox2.Items.Add(enteredPath);
                metroSetTextBox2.Text = string.Empty;
            }
        }
        private void New_Baseline_Click(object sender, EventArgs e)
        {
            List<string> included = KeepParentDirectories(ConvertItemsToStringList(metroSetListBox1.Items));
            List<string> excluded = KeepParentDirectories(ConvertItemsToStringList(metroSetListBox2.Items));
            bool isValidDir = AreValidDirectories(included);
            bool isValidDir1 = AreValidDirectories(excluded);
            if (included.Count == 0)
            {
                MessageBox.Show($"No directories in the Included list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!isValidDir || !isValidDir1)
            {
                List<(string Path, string ListName)> invalidPaths = new List<(string Path, string ListName)>();
                if (!isValidDir)
                {
                    invalidPaths.AddRange(GetInvalidDirectories(included, "Included"));
                }
                if (!isValidDir1)
                {
                    invalidPaths.AddRange(GetInvalidDirectories(excluded, "Excluded"));
                }
                ShowInvalidPathsMessageBox(invalidPaths);
                return;
            }
            if (excluded.Count == 0)
            {
                EraseBaselineIfAlreadyExists(included);
                GenerateHashesForFiles(included);
            }
            else if (excluded.Count != 0)
            {
                if (included.SequenceEqual(excluded, StringComparer.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Included and Excluded lists are exactly the same.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<string> filteredIncludedDirs = KeepParentDirectories(FilterDirectories(included, excluded));
                EraseBaselineIfAlreadyExists(filteredIncludedDirs);
                GenerateHashesForFiles(filteredIncludedDirs);
            }
        }

        private void Verify_Click(object sender, EventArgs e)
        {
            VerifyAgainstBaseline();
        }

        private void Monitor_Click(object sender, EventArgs e)
        {
            MonitorChanges();
        }

        private void EraseBaselineIfAlreadyExists(List<string> directories)
        {
            foreach (var directory in directories)
            {
                string baselineFilePath = Path.Combine(directory, "baseline.txt");
                if (File.Exists(baselineFilePath))
                {
                    try
                    {
                        File.Delete(baselineFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error removing baseline from {directory}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void VerifyAgainstBaseline()
        {
        }

        private void MonitorChanges()
        {
        }

        private bool IsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void GenerateHashesForFiles(List<string> directories)
        {
            foreach (var directory in directories)
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                    foreach (string filePath in filePaths)
                    {
                        string fileHash = CalculateSHA512(filePath);
                        string relativePath = GetRelativePath(directory, filePath);
                        StoreInBaseline(directory, relativePath, fileHash);
                    }
                    MessageBox.Show($"Successfully Created Baseline for: \n{directory}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating hashes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private List<string> ConvertItemsToStringList(MetroSet_UI.Child.MetroSetItemCollection items)
        {
            List<string> directories = new List<string>();
            foreach (var item in items)
            {
                directories.Add(item.ToString());
            }
            return directories;
        }

        private List<string> ConvertItemsToStringList1(IEnumerable<string> items)
        {
            List<string> directories = new List<string>();
            foreach (var item in items)
            {
                directories.Add(item.ToString());
            }
            return directories;
        }

        private bool IsValidDirectory(string directoryPath)
        {
            return !string.IsNullOrWhiteSpace(directoryPath) && Directory.Exists(directoryPath);
        }

        private string CalculateSHA512(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(fs);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string GetRelativePath(string parentDirectory, string fullPath)
        {
            Uri parentUri = new Uri(parentDirectory + Path.DirectorySeparatorChar);
            Uri fileUri = new Uri(fullPath);
            return Uri.UnescapeDataString(parentUri.MakeRelativeUri(fileUri).ToString());
        }

        private void StoreInBaseline(string parentDirectory, string relativePath, string fileHash)
        {
            try
            {
                string baselineFilePath = Path.Combine(parentDirectory, "baseline.txt");
                File.AppendAllText(baselineFilePath, $"{relativePath} {fileHash}\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error storing in baseline: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AreValidDirectories(List<string> directoryPaths)
        {
            return directoryPaths.All(path => !string.IsNullOrWhiteSpace(path) && Directory.Exists(path));
        }

        private string GetInvalidDirectory(List<string> directoryPaths)
        {
            return directoryPaths.FirstOrDefault(path => !IsValidDirectory(path));
        }

        private List<(string Path, string ListName)> GetInvalidDirectories(List<string> directoryPaths, string listName)
        {
            return directoryPaths
                .Where(path => !IsValidDirectory(path))
                .Select(path => (path, listName))
                .ToList();
        }

        private void ShowInvalidPathsMessageBox(List<(string Path, string ListName)> invalidPaths)
        {
            string errorMessage = "Invalid directories:\n";
            foreach (var invalidPath in invalidPaths)
            {
                errorMessage += $"{invalidPath.Path} (in {invalidPath.ListName} list)\n";
            }
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private List<string> FilterDirectories(List<string> included, List<string> excluded)
        {
            // filter
        }

        private bool IsExcluded(string directory, HashSet<string> excludeDirectories)
        {
            return excludeDirectories.Any(excludeDir => string.Equals(directory, excludeDir, StringComparison.OrdinalIgnoreCase));
        }

        private void ShowTestOutputMessageBox(string listName, List<string> items)
        {
            string message = $"{listName}:\n";
            foreach (var item in items)
            {
                message += $"{item}\n";
            }
            MessageBox.Show(message, $"{listName} Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public List<string> KeepParentDirectories(List<string> directoryPaths)
        {
            HashSet<string> resultDirectories = new HashSet<string>(directoryPaths);

            foreach (var directoryPath in directoryPaths)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                // Remove subdirectories
                foreach (var subDirectory in directoryInfo.GetDirectories())
                {
                    resultDirectories.Remove(subDirectory.FullName);
                }
            }

            return resultDirectories.ToList();
        }
    }
}