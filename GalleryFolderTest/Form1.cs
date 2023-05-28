using ImageMagick;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace GalleryFolderTest
{

    public class LocValues
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
    public partial class Form1 : Form
    {
        public string FolderPath { get; set; }
        public string[] ImageFiles { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_openFolder_Click(object sender, EventArgs e)
        {
            var path = GetPath();
            lbl_path.Text = path;
            ImageFiles = GetImageFilesPath(path);
            AddToListbox(ImageFiles);
            ReadAndAddMetaDataToListbox(ImageFiles[0]);
            //GetLocationMetaData(ImageFiles[0]);
            //GetDateMetaData(ImageFiles[0]);
            ReadMetaData(ImageFiles[0]);
        }

        private void ReadMetaData(string imagePath)
        {
            string fileName = Path.GetFileName(imagePath);
            string labelName = GetLabelName(imagePath);
            bool hasLabelFormat = checkLabel(labelName);
            string Date_Taken = "";
            string Event_Name = "";
            LocValues Location = new LocValues();
            string Last_Modified_Date = DateTime.Now.ToString();
            if (!hasLabelFormat)
            {
                // MessageBox.Show(fileName);
                Location = GetLocationMetaData(imagePath);
                Date_Taken = GetDateMetaData(imagePath);
                string data = $"Location - {Location.Latitude} , {Location.Longitude} -- Date_Taken - {Date_Taken} -- Last_Modified_Date - {Last_Modified_Date} -- FileName - {fileName} -- LabelName - {labelName} -- path - {imagePath}";
                MessageBox.Show(data);

            }
            else
            {
                Date_Taken = extractDateFromLabelName(labelName);
                Event_Name = extractEventFromLabelName(labelName);
                MessageBox.Show($"Date Taken {Date_Taken} - Event Name {Event_Name}");
            }
            string connetionString;
            SqlConnection con;
            connetionString = @"Data Source=DESKTOP-LMK4CI3\SQLEXPRESS;Initial Catalog=PhotoGallery;User ID=sa;password=123";
            string photoQuery;
            if (Location.Latitude != null)
            {
               photoQuery = "insert into photo(title,lat,lng,path,date_taken,last_modified_date,label,isSynced) values (@title,@lat,@lng,@path,@date_taken,@last_modified_date,@label,@isSynced);SELECT SCOPE_IDENTITY();";

            }
            else {
               photoQuery = "insert into photo(title,path,date_taken,last_modified_date,label,isSynced) values (@title,@path,@date_taken,@last_modified_date,@label,@isSynced);SELECT SCOPE_IDENTITY();";

            }
            string eventQuery = "INSERT INTO event (name) VALUES (@name); SELECT SCOPE_IDENTITY();";
            con = new SqlConnection(connetionString);
            SqlCommand cmd = new SqlCommand(photoQuery, con);
           
            con.Open();
            cmd.Parameters.AddWithValue("@title", fileName) ;
            if (Location.Latitude != null) {
                cmd.Parameters.AddWithValue("@lat", (object)Location.Latitude ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@lng", (object)Location.Latitude ?? DBNull.Value);
            }
           
            cmd.Parameters.AddWithValue("@path", imagePath);
            cmd.Parameters.AddWithValue("@date_taken", Date_Taken);
            cmd.Parameters.AddWithValue("@last_modified_date", Last_Modified_Date);
            cmd.Parameters.AddWithValue("@label", labelName);
            cmd.Parameters.AddWithValue("@isSynced", 0);
            int insertedPhotoId = Convert.ToInt32(cmd.ExecuteScalar());
            MessageBox.Show("Inserted into Photo Table");
            cmd = new SqlCommand(eventQuery, con);
            cmd.Parameters.AddWithValue("@name", Event_Name);

            // Execute the insert command and retrieve the inserted ID
            int insertedEventId = Convert.ToInt32(cmd.ExecuteScalar());
            MessageBox.Show(insertedPhotoId.ToString());
            MessageBox.Show(insertedEventId.ToString());
            string photoEventQuery = "Insert into PhotoEvent values('"+insertedPhotoId+"','"+insertedEventId+"') ";
            cmd = new SqlCommand(photoEventQuery,con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Inserted");
            con.Close();
        }

        private bool checkLabel(string labelName)
        {
            return labelName.Contains('-');
        }
        private string extractEventFromLabelName(string labelName)
        {
            string[] parts = labelName.Split('-');
            string[] eventArray = parts.Skip(3).ToArray();

            string eventName = string.Join(" ", eventArray);
            return eventName;
        }
        private string extractDateFromLabelName(string labelName)
        {
            string[] parts = labelName.Split('-');

            string[] dateArray = parts.Take(3).ToArray();
            //string[] secondArray = parts.Skip(3).ToArray();

            int year = int.Parse(dateArray[0]);
            int month = int.Parse(dateArray[1]);
            int day = int.Parse(dateArray[2]);

            DateTime randomDate = new DateTime(year, month, day);


            return randomDate.ToString();
        }

        private string GetLabelName(string imagePath)
        {
            string fileName = Path.GetFileName(imagePath);
            string directoryPath = Path.GetDirectoryName(imagePath);
            string[] pathParts = directoryPath.Split(Path.DirectorySeparatorChar);
            string labelName = pathParts[pathParts.Length - 1];
            return labelName;
        }

        private string GetDateMetaData(string imagePath)

        {
            string date = DateTime.Now.ToString();
            MagickImage image = new MagickImage(imagePath);
            // Check if the image contains EXIF data
            if (image.HasProfile("exif"))
            {
                // Get the EXIF profile
                ExifProfile exifProfile = (ExifProfile)image.GetExifProfile();

                // Retrieve the GPS coordinates
                date = exifProfile.GetValue(ExifTag.DateTimeOriginal)?.Value;
                //if (date != null)
                //{
                //    MessageBox.Show($"DateTime {date}");
                //}
            }
            else
            {
                //MessageBox.Show("Image does not contain EXIF data.");
            }
            return date;
        }

        private void ReadAndAddMetaDataToListbox(string imagePath)
        {
            // Read image from file
            MagickImage image = new MagickImage(imagePath);

            // Retrieve the exif information
            var profile = image.GetExifProfile();

            // Check if image contains an exif profile
            if (profile is null)
            {
                MessageBox.Show("Image does not contain exif information.");
            }
            else
            {
                // Write all values to the console
                foreach (var value in profile.Values)
                {
                    string data = $"{value.Tag} - {value.DataType} - {value}";
                    listbox_image_metadata.Items.Add(data);
                }
            }
        }

        private LocValues GetLocationMetaData(string imagePath)
        {
            LocValues location = new LocValues { Longitude = null, Latitude = null };
            MagickImage image = new MagickImage(imagePath);
            // Check if the image contains EXIF data
            if (image.HasProfile("exif"))
            {
                // Get the EXIF profile
                ExifProfile exifProfile = (ExifProfile)image.GetExifProfile();

                // Check if the EXIF profile contains GPS information
                // Retrieve the GPS coordinates
                Rational[] latitude = exifProfile.GetValue(ExifTag.GPSLatitude)?.Value as Rational[];
                Rational[] longitude = exifProfile.GetValue(ExifTag.GPSLongitude)?.Value as Rational[];
                // Process and display the GPS coordinates
                if (latitude != null && longitude != null)
                {
                    double latitudeDegrees = (double)latitude[0].Numerator / latitude[0].Denominator;
                    double latitudeMinutes = (double)latitude[1].Numerator / latitude[1].Denominator;
                    double latitudeSeconds = (double)latitude[2].Numerator / latitude[2].Denominator;
                    double latitudeDecimal = latitudeDegrees + latitudeMinutes / 60 + latitudeSeconds / 3600;

                    double longitudeDegrees = (double)longitude[0].Numerator / longitude[0].Denominator;
                    double longitudeMinutes = (double)longitude[1].Numerator / longitude[1].Denominator;
                    double longitudeSeconds = (double)longitude[2].Numerator / longitude[2].Denominator;
                    double longitudeDecimal = longitudeDegrees + longitudeMinutes / 60 + longitudeSeconds / 3600;

                    location.Latitude = latitudeDecimal;
                    location.Longitude = longitudeDecimal;
                }
            }
            else
            {
                MessageBox.Show("Image does not contain EXIF data.");
            }
            return location;
        }
        private string GetPath()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    string selectedFolderPath = folderDialog.SelectedPath;
                    return selectedFolderPath;
                }
                return null;
            }
        }
        private string[] GetImageFilesPath(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.jpg"); // Replace "*.jpg" with the desired search pattern for image files
                return files;
            }
            return null;
        }
        private void AddToListbox(string[] files)
        {
            foreach (string filePath in files)
            {
                listbox_imageFiles.Items.Add(filePath);
            }
        }

        private void btninsertintodatabase_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=WIN-50GP30FGO75;Initial Catalog=Demodb;User ID=sa;Password=demol23";
            string query = "insert into photo(title,lat,lng,path,date_taken,last_modified_date,label,isSynced) values ()";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Connection Open  !");
            cnn.Close();
        }
    }
}
