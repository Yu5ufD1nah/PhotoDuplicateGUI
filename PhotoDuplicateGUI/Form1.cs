using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ImageMagick;

namespace PhotoDuplicateGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnOrganize_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text;

            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
            {
                MessageBox.Show("Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OrganizePhotos(folderPath);
        }

        private void OrganizePhotos(string folderPath)
        {
            listBoxLog.Items.Clear();
            var files = Directory.GetFiles(folderPath, "*.jpg"); // Add other formats if needed
            var fileMap = new Dictionary<string, string>(); // Track file locations
            foreach (var file in files)
            {
                fileMap[file] = file; // Initially, the file's location is its original path
            }

            var processedFiles = new HashSet<string>();

            progressBar.Minimum = 0;
            progressBar.Maximum = files.Length;
            progressBar.Value = 0;

            // Disable buttons during processing
            btnOrganize.Enabled = false;
            btnSelectFolder.Enabled = false;

            foreach (var file in fileMap.Keys)
            {
                if (processedFiles.Contains(file)) continue;

                string groupFolder = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(file));
                Directory.CreateDirectory(groupFolder);

                // Move the first file
                MoveFileToFolder(fileMap[file], groupFolder);
                processedFiles.Add(file);
                fileMap[file] = Path.Combine(groupFolder, Path.GetFileName(file)); // Update the file location

                foreach (var otherFile in fileMap.Keys)
                {
                    if (processedFiles.Contains(otherFile)) continue;

                    if (AreImagesSimilar(fileMap[file], fileMap[otherFile]))
                    {
                        MoveFileToFolder(fileMap[otherFile], groupFolder);
                        processedFiles.Add(otherFile);
                        fileMap[otherFile] = Path.Combine(groupFolder, Path.GetFileName(otherFile)); // Update location
                        listBoxLog.Items.Add($"  -> {Path.GetFileName(otherFile)}");
                    }
                }
                progressBar.Value += 1;
                // Refresh UI
                Application.DoEvents();
            }
            btnOrganize.Enabled = true;
            btnSelectFolder.Enabled = true;
            MessageBox.Show("Organizing complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MoveFileToFolder(string filePath, string destinationFolder)
        {
            string destinationPath = Path.Combine(destinationFolder, Path.GetFileName(filePath));
            File.Move(filePath, destinationPath);
        }

        private bool AreImagesSimilar(string filePath1, string filePath2)
        {
            try
            {
                using (var image1 = new MagickImage(filePath1))
                using (var image2 = new MagickImage(filePath2))
                {
                    var diff = image1.Compare(image2, ErrorMetric.RootMeanSquared);
                    return diff < 0.01; // Adjust sensitivity
                }
            }
            catch (Exception ex)
            {
                listBoxLog.Items.Add($"Error comparing images:\n{filePath1} or {filePath2} - {ex.Message}");
                return false;
            }
        }

  
    }
}

