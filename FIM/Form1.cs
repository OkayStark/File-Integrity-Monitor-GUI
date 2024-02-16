using System;
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
            if (metroSetListBox1.Items.Count != 0)
            {
                if (metroSetListBox2.Items.Count == 0)
                {
                    bool isValidDir = AreValidDirectories(ConvertItemsToStringList(metroSetListBox1.Items));
                    if (isValidDir)
                    {
                        try
                        {
                            EraseBaselineIfAlreadyExists(ConvertItemsToStringList(metroSetListBox1.Items));
                            try
                            {
                                GenerateHashesForFiles(ConvertItemsToStringList(metroSetListBox1.Items));
                                MessageBox.Show($"Successfully Created Baseline", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Unable to Generate File Hashes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Unable to delete baseline.txt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        string invalidPath = GetInvalidDirectory(ConvertItemsToStringList(metroSetListBox1.Items));
                        MessageBox.Show($"Invalid directory in included list: {invalidPath}");
                        return;
                    }
                }
                else
                {
                    bool isValidDir = AreValidDirectories(ConvertItemsToStringList(metroSetListBox1.Items));
                    bool isValidDir1 = AreValidDirectories(ConvertItemsToStringList(metroSetListBox2.Items));
                    if (isValidDir && isValidDir1)
                    {
                        //filter and remove excluded
                        //remove baseline
                        //gen hash for all files
                        //enter path and hash in baseline
                    }
                    else
                    {
                        List<(string Path, string ListName)> invalidPaths = new List<(string Path, string ListName)>();

                        if (!isValidDir)
                        {
                            invalidPaths.AddRange(GetInvalidDirectories(ConvertItemsToStringList(metroSetListBox1.Items), "Included"));
                        }

                        if (!isValidDir1)
                        {
                            invalidPaths.AddRange(GetInvalidDirectories(ConvertItemsToStringList(metroSetListBox2.Items), "Excluded"));
                        }

                        ShowInvalidPathsMessageBox(invalidPaths);
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show($"No directories in the included list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                    string baselineFilePath = Path.Combine(directory, "baseline.txt"); // Adjust the file name as needed

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

        private void CalculateAndStoreFileHashes()
        {

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
                    // Get all file paths in the directory and its subdirectories
                    string[] filePaths = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

                    foreach (string filePath in filePaths)
                    {
                        // Calculate SHA-512 hash for each file
                        string fileHash = CalculateSHA512(filePath);

                        // Get the relative path to the file from the parent directory
                        string relativePath = GetRelativePath(directory, filePath);

                        // Store the path and hash in the baseline file in the parent directory
                        StoreInBaseline(directory, relativePath, fileHash);
                    }
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
            // Calculate the relative path from the parent directory
            Uri parentUri = new Uri(parentDirectory + Path.DirectorySeparatorChar);
            Uri fileUri = new Uri(fullPath);
            return Uri.UnescapeDataString(parentUri.MakeRelativeUri(fileUri).ToString());
        }

        private void StoreInBaseline(string parentDirectory, string relativePath, string fileHash)
        {
            try
            {
                // Construct the baseline file path in the parent directory
                string baselineFilePath = Path.Combine(parentDirectory, "baseline.txt");

                // Append or write the path and hash to the baseline file
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
            // Find and return the first invalid directory path
            return directoryPaths.FirstOrDefault(path => !IsValidDirectory(path));
        }

        private List<(string Path, string ListName)> GetInvalidDirectories(List<string> directoryPaths, string listName)
        {
            // Find and return the invalid directory paths along with the list name
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

        static bool IsExcluded(string directory, List<string> excludeDirectories)
        {
            // Check if the directory is in the list of excluded directories
            return excludeDirectories.Any(excludeDir => directory.StartsWith(excludeDir, StringComparison.OrdinalIgnoreCase));
        }
    }
}