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
                else if ((metroSetListBox2.Items.Count != 0))
                {

                    bool isValidDir = AreValidDirectories(ConvertItemsToStringList(metroSetListBox1.Items));
                    bool isValidDir1 = AreValidDirectories(ConvertItemsToStringList(metroSetListBox2.Items));
                    if (isValidDir && isValidDir1)
                    {
                        List<string> included = ConvertItemsToStringList(metroSetListBox1.Items);
                        List<string> excluded = ConvertItemsToStringList(metroSetListBox2.Items);
                        if (included.SequenceEqual(excluded, StringComparer.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Included and Excluded lists are exactly the same.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        List<string> commonDirs = included.Intersect(excluded, StringComparer.OrdinalIgnoreCase).ToList();
                        if (commonDirs.Any())
                        {
                            List<string> filteredIncluded;
                            List<string> filteredExcluded;
                            FilterDirectories(included, excluded, out filteredIncluded, out filteredExcluded);
                            try
                            {
                                EraseBaselineIfAlreadyExists(filteredIncluded);
                                try
                                {
                                    List<string> filteredIncludedSubDirs = FilterSubdirectories(filteredIncluded, filteredExcluded);
                                    GenerateHashesForFiles(filteredIncludedSubDirs);
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
                            try
                            {
                                EraseBaselineIfAlreadyExists(included);
                                try
                                {
                                    List<string> filteredIncludedSubDirs = FilterSubdirectories(included, excluded);
                                    GenerateHashesForFiles(filteredIncludedSubDirs);
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

        private void FilterDirectories(List<string> includedDirectories, List<string> excludedDirectories, out List<string> filteredIncludedDirectories, out List<string> filteredExcludedDirectories)
        {
            filteredIncludedDirectories = includedDirectories
                .Where(dir => !excludedDirectories.Any(excludedDir => string.Equals(dir, excludedDir, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            filteredExcludedDirectories = excludedDirectories
                .Where(excludedDir => !includedDirectories.Any(dir => string.Equals(dir, excludedDir, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        private List<string> FilterSubdirectories(List<string> includedDirectories, List<string> excludedDirectories)
        {
            HashSet<string> excludedSet = new HashSet<string>(excludedDirectories, StringComparer.OrdinalIgnoreCase);

            return includedDirectories
                .Where(dir => !IsExcluded(dir, excludedSet))
                .ToList();
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

    }
}