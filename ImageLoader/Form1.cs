using CsvHelper;
using ImageLoader.Properties;
using MihaZupan;
using Newtonsoft.Json;
using NLog;
using Polly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;
using System.Windows.Forms;
using System.Xml;

namespace ImageLoader
{
    public partial class ImageLoader : Form
    {
        private OpenFileDialog openFileDialog1;
        private List<Model> allObjects;
        private List<HttpToSocks5Proxy> proxyList;
        private List<JsonObject> ftpLists;
        private readonly AutoResetEvent _pause = new AutoResetEvent(true);
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private JsonModel ftpConnenct;
        private List<Model> errorsFiles = new List<Model>();

        public ImageLoader()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;

            var config = new NLog.Config.LoggingConfiguration();
            var logFile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            var logConsole = new NLog.Targets.ConsoleTarget("logconsole");

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logFile);

            NLog.LogManager.Configuration = config;

            ftpLists = new List<JsonObject>();
            allObjects = new List<Model>();
            openFileDialog1 = new OpenFileDialog();
            proxyList = new List<HttpToSocks5Proxy>();

            if (!File.Exists("log.txt"))
                File.Create("log.txt");
            if (!File.Exists("errors.csv"))
                File.Create("errors.csv");
            if (!File.Exists("ftpUrls.csv"))
                File.Create("ftpUrls.csv");

            InitializeComponent();
            pictureBox1.Image = Resources.imgurIco.ToBitmap();
            pictureBox2.Image = Resources.postimages.ToBitmap();
            /*imgur_checkBox.BackgroundImage = Resources.imgurIco.ToBitmap();*/

            puaseLoad_btn.Enabled = false;
            resumeProc_btn.Enabled = false;
            ftpConnenct = new JsonModel();
            if (File.Exists("ftp.txt"))
            {
                var fileText = File.ReadAllText("ftp.txt");

                ftpConnenct = JsonConvert.DeserializeObject<JsonModel>(fileText);
                log_txtBox.Text += "Ftp config success loaded\n";
            }
            else
            {
                ftpLoad_btn.Enabled = false;
            }

            openFileDialog1.Filter = "Text files(*.csv)|*.csv|All files(*.*)|*.*";
            proc_pb.Minimum = 0;
            proc_pb.Step = 1;
            proc_pb.Value = 0;
        }

        private async void loadCsv_btn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                await Task.Run(() =>
                {
                    var records = csv.GetRecords<Model>().ToArray();
                    numberLinks_textbox.Invoke(new Action(() => { numberLinks_textbox.Text = records.Count().ToString(); }));
                    foreach (var record in records)
                    {
                        allObjects.Add(record);
                    }
                });
                log_txtBox.Text += "CSV successfully added\n";
            }
        }

        private async void startUpload_btn_Click(object sender, EventArgs e)
        {
            puaseLoad_btn.Enabled = true;

            if (allObjects.Count == 0)
            {
                MessageBox.Show("Upload at least 1 file");
                return;
            }
            if (!imgur_checkBox.Checked && !postimages_checkBox.Checked)
            {
                MessageBox.Show("Please select at least 1 checkbox", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            proc_pb.Visible = true;
            proc_pb.Maximum = allObjects.Count;
            var scheduler = new ThreadPerTaskScheduler();
            var resu = await Task.Run(async () =>
            {
                LoaderImage li = new LoaderImage();
                List<CastModel> listsCastModels = new List<CastModel>();
                var processorCount = Environment.ProcessorCount;
                var semaphore = new SemaphoreSlim(processorCount, processorCount);
                var countdown = new CountdownEvent(allObjects.Count);

                foreach (var record in allObjects)
                {
                    await semaphore.WaitAsync();
                    _ = Task.Factory.StartNew(async () =>
                    {
                        try
                        {
                            if (record.Field3.Length > 0)
                            {
                                string imgurImg = string.Empty;
                                string postimages = string.Empty;
                                if (imgur_checkBox.Checked)
                                {
                                    try
                                    {
                                        var policy = Policy.Handle<Exception>()
                                            .RetryAsync(5);
                                        imgurImg = await policy.ExecuteAsync(() => li.LoadImgOnImgur(record.Field3, clientId_txtBox.Text, proxyList));
                                        log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {record.Field3} successfully loaded on Imgur\n"; }));
                                    }
                                    catch (Exception ex)
                                    {
                                        errorsFiles.Add(record);
                                        log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {record.Field3} has error {ex}\n"; }));
                                        File.AppendAllText("log.txt", ex.ToString());
                                        return;
                                    }
                                }
                                if (postimages_checkBox.Checked)
                                {
                                    try
                                    {
                                        var policy = Policy.Handle<Exception>()
                                            .RetryAsync(5);
                                        postimages = await policy.ExecuteAsync(() => li.LoadImgOnpostimages(record.Field3, clientIdPostImages_txtBox.Text, proxyList));
                                        log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {record.Field3} successfully loaded on PostImages\n"; }));
                                    }
                                    catch (Exception ex)
                                    {
                                        errorsFiles.Add(record);
                                        log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {record.Field3} has error {ex}\n"; }));
                                        File.AppendAllText("log.txt", ex.ToString());
                                        return;
                                    }
                                }

                                var newObj = new CastModel()
                                {
                                    Title = record.Title,
                                    Field1 = record.Field1,
                                    Field2 = record.Field2,
                                    Field3 = record.Field3,
                                    Field4 = record.Field4,
                                    ImgurUrl = imgurImg,
                                    PostImagesUrl = postimages
                                };

                                lock (listsCastModels)
                                    listsCastModels.Add(newObj);
                            }
                            proc_pb.Invoke(new Action(() => { proc_pb.PerformStep(); }));
                            _pause.WaitOne();
                            _pause.Set();
                        }
                        finally
                        {
                            semaphore.Release();
                            countdown.Signal();
                        }
                    }, default, TaskCreationOptions.None, scheduler);
                }


                countdown.Wait();
                countdown.Dispose();
                semaphore.Dispose();

                log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Main loading finished\n"; }));
                log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Success upload: {listsCastModels.Count}\n"; }));


                //Exception Handler 
                #region ErrorHandler
                /*if (errorsFiles.Count > 0)
                {
                    List<CastModel> files = new List<CastModel>();
                    log_txtBox.Invoke(new Action(() => { log_txtBox.Text += "!!!!Error File Handling!!!!\n"; }));
                    int i = 0;
                    int unloadedCount = errorsFiles.Count;
                    string imgur = string.Empty;
                    string postImg = string.Empty;
                    foreach (var file in errorsFiles)
                    {
                        if (imgur_checkBox.Checked)
                        {
                            try
                            {
                                var policy = Policy.Handle<Exception>()
                                    .RetryAsync(3);
                                imgur = await policy.ExecuteAsync(() => li.LoadImgOnImgur(file.Field3, clientId_txtBox.Text, proxyList));
                                log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {file.Field3} successfully loaded on Imgur\n"; }));
                            }
                            catch (NullReferenceException ex)
                            {
                                //log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {file.Field3} CAN'T loaded on Imgur\n"; }));
                                File.AppendAllText("log.txt", $"Problem with {file.Field3}. Exception: {ex}\n");
                            }
                        }
                        if (postimages_checkBox.Checked)
                        {
                            try
                            {
                                var policy = Policy.Handle<Exception>()
                                    .RetryAsync(3);
                                postImg = await policy.ExecuteAsync(()=> li.LoadImgOnpostimages(file.Field3, clientIdPostImages_txtBox.Text));
                                log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {file.Field3} successfully loaded on PostImages\n"; }));
                            }
                            catch (NullReferenceException ex)
                            {
                                //log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Photo {file.Field3} CAN'T loaded on PostImages\n"; }));
                                File.AppendAllText("log.txt", $"Problem with {file.Field3}. Exception: {ex}\n");

                            }
                        }

                        var newObj = new CastModel()
                        {
                            Title = file.Title,
                            Field1 = file.Field1,
                            Field2 = file.Field2,
                            Field3 = file.Field3,
                            Field4 = file.Field4,
                            ImgurUrl = imgur,
                            PostImagesUrl = postImg
                        };
                        files.Add(newObj);

                    }
                    var temp = files.Concat(listsCastModels).ToList();
                    log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Finished\n"; }));
                    return temp;
                }*/
                #endregion

                log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Finished\n"; }));
                return listsCastModels;
            });

            using (var writer = new StreamWriter("errors.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(errorsFiles);
            }
            using (var writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "newFileCSV.csv")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(resu);
            }
            proc_pb.Value = 0;
            proc_pb.Visible = false;
            Task.Run(() => { MessageBox.Show("Loading finished!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); });
        }

        private async void proxyLoad_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (opf.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = opf.FileName;
            await Task.Run(() =>
            {
                var fileRead = File.ReadAllLines(filename);
                proc_pb.Invoke(new Action(() => { proc_pb.Maximum = fileRead.Length; proc_pb.Visible = true; }));
                foreach (var proxy in fileRead)
                {
                    var oneProx = proxy.Split(new char[] { ':' }, 4).Select(t => t.Trim()).ToArray();
                    HttpToSocks5Proxy proxyZupan;
                    if (oneProx.Length > 2)
                    {
                        proxyZupan = new HttpToSocks5Proxy(oneProx[0], Convert.ToInt32(oneProx[1]), oneProx.Length > 2 ? oneProx[2] : "", oneProx.Length > 2 ? oneProx[3] : "");
                    }
                    else
                    {
                        proxyZupan = new HttpToSocks5Proxy(oneProx[0], Convert.ToInt32(oneProx[1]));
                    }
                    //, oneProx.Length > 2 ? oneProx[2] : "", oneProx.Length > 2 ? oneProx[3] : ""
                    /*                    var webProxy = new WebProxy
                                        {
                                            Address = new Uri($"http://{oneProx[0]}:{oneProx[1]}"),
                                            BypassProxyOnLocal = false,
                                            UseDefaultCredentials = false,
                                            Credentials = new NetworkCredential(oneProx.Length > 2 ? oneProx[2] : "", oneProx.Length > 2 ? oneProx[3] : "")
                                        };*/
                    proxyList.Add(proxyZupan);
                    proc_pb.Invoke(new Action(() => { proc_pb.PerformStep(); }));
                }
            });
            log_txtBox.Text += "Proxy successfully added\n";
            countProxy_txtBox.Text = proxyList.Count.ToString();

            proc_pb.Value = 0;
            proc_pb.Visible = false;
        }

        private void clearCSV_btn_Click(object sender, EventArgs e)
        {
            allObjects.Clear();
            numberLinks_textbox.Text = allObjects.Count().ToString();
            log_txtBox.Text += "Clear csv lists\n";
        }

        private void proxyClear_btn_Click(object sender, EventArgs e)
        {
            proxyList.Clear();
            countProxy_txtBox.Text = proxyList.Count.ToString();
            log_txtBox.Text += "Clear proxy lists\n";
        }

        private void puaseLoad_btn_Click(object sender, EventArgs e)
        {
            puaseLoad_btn.Enabled = false;
            resumeProc_btn.Enabled = true;

            _pause.WaitOne();
            log_txtBox.Text += "Pause\n";
        }

        private void resumeProc_btn_Click(object sender, EventArgs e)
        {
            puaseLoad_btn.Enabled = true;
            resumeProc_btn.Enabled = false;

            _pause.Set();
            log_txtBox.Text += "Resume\n";
        }

        private async void ftpLoad_btn_Click(object sender, EventArgs e)
        {
            puaseLoad_btn.Enabled = true;
            log_txtBox.Text += "Start FTP Load\n";
            if (Directory.Exists("image"))
                Directory.Delete("image", true);
            await Task.Delay(2000);
            Directory.CreateDirectory("image");
            await Task.Run(async () =>
            {
                var lists = new List<FTPUrl>();
                foreach (var img in allObjects)
                {
                    var fileName = Path.GetFileName(img.Field3);
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            var policy = Policy.Handle<Exception>().Retry(5);
                            policy.Execute(() => client.DownloadFile(new Uri(img.Field3), Path.Combine("image", fileName)));
                        }
                        catch (Exception ex)
                        {
                            log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Can't download photo {img} Exception: {ex} \n"; }));
                            File.AppendAllText("log.txt", "Problem with download photo: " + ex.ToString() + "\n");
                            _pause.WaitOne();
                            _pause.Set();
                            continue;
                        }
                    }
                    foreach (var ftp in ftpConnenct.FTP)
                    {
                        if (ftp.Login == string.Empty && ftp.Password == string.Empty && ftp.Password == string.Empty)
                            continue;
                        try
                        {
                            FTP ftpObj = new FTP();
                            var policy = Policy.Handle<Exception>().RetryAsync(5);
                            await policy.ExecuteAsync(() => ftpObj.LoadInFtp(ftp.Path,ftp.Server, fileName, Path.Combine("image", fileName), ftp.Password, ftp.Login));
                            string localPath = ftp.Path.Contains("public_html") ? String.Join("/", ftp.Path.Split('/').Where(t => t != "public_html").Select(t=>t).ToList()) : ftp.Path;
                            lists.Add(new FTPUrl { OriginalLink = img.Field3, ConvertedFTPLink = $"{ftp.Url}/{localPath}/{fileName}" });
                            log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Successfully upload {img.Field3} on FTP: {ftp.Server}/{ftp.Path.Trim('/')}\n"; }));
                        }
                        catch (Exception ex)
                        {
                            log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Problem with upload {img.Field3} on FTP: {ftp.Server}/{ftp.Path} Exception: {ex}\n"; }));
                            File.AppendAllText("log.txt", "Problem with upload photo on FTP: " + ex.ToString() + "\n");
                            _pause.WaitOne();
                            _pause.Set();
                        }
                    }
                    _pause.WaitOne();
                    _pause.Set();
                }

                using (var writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "ftpUrls.csv")))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                   await csv.WriteRecordsAsync(lists);
                }
            });
            Task.Run(() => { log_txtBox.Invoke(new Action(() => { log_txtBox.Text += $"Finished loading on FTP\n"; })); });
        }
    }
}
