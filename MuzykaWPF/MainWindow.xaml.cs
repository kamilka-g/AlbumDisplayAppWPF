using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace MuzykaWPF
{
    public partial class MainWindow : Window
    {
        public List<Album> AlbumList = new List<Album>();
        public int CurrentAlbumIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            string filePath = "Data.txt"; // Poprawiono ścieżkę
            AlbumList = LoadAlbums(filePath);
            DisplayAlbum(CurrentAlbumIndex);
        }

        public List<Album> LoadAlbums(string path)
        {
            var albums = new List<Album>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Album album = new Album
                    {
                        Artist = line,
                        Title = sr.ReadLine(),
                        SongCount = Convert.ToInt32(sr.ReadLine()),
                        ReleaseYear = Convert.ToInt32(sr.ReadLine()),
                        Downloads = Convert.ToInt32(sr.ReadLine())
                    };

                    albums.Add(album);
                }
            }

            return albums;
        }

        public void NextButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentAlbumIndex++;
            if (CurrentAlbumIndex >= AlbumList.Count)
                CurrentAlbumIndex = 0;

            DisplayAlbum(CurrentAlbumIndex);
        }

        public void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentAlbumIndex--;
            if (CurrentAlbumIndex < 0)
                CurrentAlbumIndex = AlbumList.Count - 1;

            DisplayAlbum(CurrentAlbumIndex);
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var currentAlbum = AlbumList[CurrentAlbumIndex];
            currentAlbum.Downloads++;
            DownloadsText.Text = currentAlbum.Downloads.ToString();
        }

        private void DisplayAlbum(int index)
        {
            var album = AlbumList[index];
            ArtistText.Text = album.Artist;
            TitleText.Text = album.Title;
            SongCountText.Text = album.SongCount.ToString();
            ReleaseYearText.Text = album.ReleaseYear.ToString();
            DownloadsText.Text = album.Downloads.ToString();
        }
    }
}
