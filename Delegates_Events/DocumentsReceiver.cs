using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace Delegates_Events
{
    internal class DocumentsReceiver : IDisposable
    {
        FileSystemWatcher _watcher;
        Timer _timer;

        public event Action<string> TimedOut;
        public event Action<DocumentsReceiver> DocumentsReady;


        private List<string> allowedDocuments = new List<string>()
        {
            "Паспорт.jpg",
            "Заявление.txt",
            "Фото.jpg"
        };

        private List<string> notLoadedDocuments = new List<string>()
        {
            "Паспорт.jpg",
            "Заявление.txt",
            "Фото.jpg"
        };


        public void Dispose()
        {
            _timer.Stop();
            _watcher.EnableRaisingEvents = false;

            _timer.Elapsed -= _timer_Elapsed;
            _watcher.Created -= Watcher_Created;
        }

        public void Start(string path, double timeOut)
        {

            allowedDocuments.ForEach(document =>
            {
                if (File.Exists(path + "\\" + document))
                {
                    notLoadedDocuments.Remove(document);
                }
            });

            if (notLoadedDocuments.Count == 0)
            {
                DocumentsReady?.Invoke(this);
                return;
            }

            _watcher = new FileSystemWatcher(path);

            _watcher.Created += Watcher_Created;
            _watcher.Deleted += _watcher_Deleted;

            _timer = new Timer();
            _timer.Interval = timeOut;

            _timer.Elapsed += _timer_Elapsed;

            _watcher.EnableRaisingEvents = true;
            _timer.Start();


        }

        private void _watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (!allowedDocuments.Contains(e.Name))
                return;

            Console.WriteLine($"Файл [{e.Name}] удален из директории");


            notLoadedDocuments.Add(e.Name);

        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (!allowedDocuments.Contains(e.Name))
                return;

            Console.WriteLine($"Файл [{e.Name}] добавлен в директорию");


            notLoadedDocuments.Remove(e.Name);

            if (notLoadedDocuments.Count == 0)
            {
                Dispose();
                DocumentsReady?.Invoke(this);
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispose();
            TimedOut?.Invoke("Таймаут");
        }
    }
}
