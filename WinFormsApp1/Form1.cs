using System;
using System.Diagnostics;
using System.Windows.Forms;
using NAudio.Wave;
using Vosk;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Runtime.Intrinsics.X86;
using WindowsInput;
using NAudio.CoreAudioApi;
using Newtonsoft.Json;


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private WaveInEvent waveIn;
        private Model voskModel;
        private VoskRecognizer recognizer;
        private string lang = "ru"; // Язык по умолчанию
        private string appPath = @"C:\\Users\\gvyu3\\AppData\\Roaming\\Telegram Desktop\\Telegram.exe";
        private string proksi = @"C:\\Program Files\\Hiddify\\Hiddify.exe";
        private string discord = @"C:\\Users\\gvyu3\\AppData\\Local\\Discord\\app-1.0.9180\\Discord.exe";
        private string taskmgr = @"C:\\Windows\\System32\\taskmgr.exe";
        private enum InputStage { None, WaitingForQuery, WaitingForSearchEngine, WaitingForBrightness, WaitingForSound , WaitingApp, LoadApp, SaveApp, WaitingForAppNumberToLaunch }
        private InputStage currentStage = InputStage.None;
        private string savedQuery = "";

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // Центрирование окна

            btnProfile.Width = 50;
            btnProfile.Height = 50;
            var ellipsePath = new System.Drawing.Drawing2D.GraphicsPath();
            ellipsePath.AddEllipse(0, 0, btnProfile.Width, btnProfile.Height);
            btnProfile.Region = new System.Drawing.Region(ellipsePath);

            comboBox.Items.Add("Русский");
            comboBox.Items.Add("English");
            comboBox.SelectedIndex = 0;

            // Привязываем обработчики кликов для остановки голосового ввода
            listBox1.MouseDown += StopAndClear;
            radioVoiceMode.MouseDown += StopAndClear;
            radioTextMode.MouseDown += StopAndClear;
            panel2.MouseDown += StopAndClear;
        }
        static readonly string jsonFilePath = "applications.json";
        private Dictionary<int, string> ProggrammDirectory = new Dictionary<int, string>();
        private Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>
{
    { "ru", new Dictionary<string, string>
        {
            { "help", "Список доступных команд:" },
            { "open_telegram", "Открыть Telegram" },
            { "open_proxy", "Открыть Proxy" },
            { "open_discord", "Открыть Discord" },
            { "open_browser", "Открыть браузер" },
            { "brightness", "Настроить яркость" },
            { "sound", "Настроить звук" },
            { "task_manager", "Открыть диспетчер задач" },
            { "show_time", "Показать текущее время" },
            { "show_weather", "Показать погоду" },
            { "add_path", "Добавить путь" },
            { "show_apps", "Показать установленные приложения" },
            { "run_saved", "Запустить сохранённую команду" },
            { "work_txt", "Работа с текстовыми файлами" },
            { "work_files", "Работа с файлами" },
            { "password_gen", "Сгенерировать пароль" },
            { "pc_info", "Информация о ПК" }
        }
    },
    { "en", new Dictionary<string, string>
        {
            { "help", "List of available commands:" },
            { "open_telegram", "Open Telegram" },
            { "open_proxy", "Open Proxy" },
            { "open_discord", "Open Discord" },
            { "open_browser", "Open browser" },
            { "brightness", "Adjust brightness" },
            { "sound", "Adjust sound" },
            { "task_manager", "Open Task Manager" },
            { "show_time", "Show current time" },
            { "show_weather", "Show weather" },
            { "add_path", "Add path" },
            { "show_apps", "Show installed apps" },
            { "run_saved", "Run saved command" },
            { "work_txt", "Work with text files" },
            { "work_files", "Work with files" },
            { "password_gen", "Generate password" },
            { "pc_info", "PC information" }
        }
    }
};

        private void LoadApplications()
        {
            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string json = File.ReadAllText(jsonFilePath);
                    ProggrammDirectory = JsonConvert.DeserializeObject<Dictionary<int, string>>(json) ?? new Dictionary<int, string>();
                }
                catch (Exception ex)
                {
                    listBox1.Items.Add($"⚠ Ошибка при загрузке приложений: {ex.Message}");
                    ProggrammDirectory = new Dictionary<int, string>();
                }
            }
            else
            {
                ProggrammDirectory = new Dictionary<int, string>();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadApplications();
            AddUpdate("Лента обновлений загружена.");
        }

        private void AddUpdate(string update)
        {
            listBox.Items.Insert(0, update);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
            AddUpdate("Панель профиля " + (panel1.Visible ? "показана" : "скрыта"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = textBox1.Text.Trim().ToLower(); // Берем команду из текстового поля и переводим в нижний регистр
            ProcessInput(txtCommand.Text.Trim());

            if (!string.IsNullOrWhiteSpace(command)) // Проверяем, что команда не пустая
            {
                ExecuteCommand(command); // Выполняем команду
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Команда не распознана. Введите 'Помощь'.");
            }

            textBox1.Clear(); // Очищаем поле после ввода
        }

        private void ShowHelp()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(translations[lang]["help"]);
            listBox1.Items.Add($"1. {translations[lang]["open_telegram"]}");
            listBox1.Items.Add($"2. {translations[lang]["open_proxy"]}");
            listBox1.Items.Add($"3. {translations[lang]["open_discord"]}");
            listBox1.Items.Add($"4. {translations[lang]["open_browser"]}");
            listBox1.Items.Add($"5. {translations[lang]["brightness"]}");
            listBox1.Items.Add($"6. {translations[lang]["sound"]}");
            listBox1.Items.Add($"7. {translations[lang]["task_manager"]}");
            listBox1.Items.Add($"8. {translations[lang]["show_time"]}");
            listBox1.Items.Add($"9. {translations[lang]["show_weather"]}");
            listBox1.Items.Add($"10. {translations[lang]["add_path"]}");
            listBox1.Items.Add($"11. {translations[lang]["show_apps"]}");
            listBox1.Items.Add($"12. {translations[lang]["run_saved"]}");
            listBox1.Items.Add($"13. {translations[lang]["work_txt"]}");
            listBox1.Items.Add($"14. {translations[lang]["work_files"]}");
            listBox1.Items.Add($"15. {translations[lang]["password_gen"]}");
            listBox1.Items.Add($"16. {translations[lang]["pc_info"]}");
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            lang = comboBox.SelectedIndex == 0 ? "ru" : "en";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (radioVoiceMode.Checked)
            {
                StartVoiceRecognition();
            }
            else if (radioTextMode.Checked)
            {
                StartTextInput();
            }
            else
            {
                MessageBox.Show("Выберите режим перед началом!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StartTextInput()
        {
            panel2.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
        }

        private void StartVoiceRecognition()
        {
            panel2.Visible = true;
            textBox1.Visible = false;
            button1.Visible = false;
            string modelPath = @"C:\Users\gvyu3\OneDrive\Рабочий стол\vosk-model-small-ru-0.22";
            string taskmgr = @"C:\Windows\System32\taskmgr.exe";
            var sim = new InputSimulator();

            if (!System.IO.Directory.Exists(modelPath))
            {
                listBox1.Items.Add("❌ Ошибка: Папка модели не найдена!");
                return;
            }

            try
            {
                voskModel = new Model(modelPath);
                recognizer = new VoskRecognizer(voskModel, 16000);
                waveIn = new WaveInEvent
                {
                    DeviceNumber = 0,
                    WaveFormat = new WaveFormat(16000, 1)
                };

                waveIn.DataAvailable += (sender, e) =>
                {
                    if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
                    {
                        var result = recognizer.Result();
                        var jsonResult = JObject.Parse(result);
                        string text = jsonResult["text"].ToString().Trim();

                        if (string.IsNullOrEmpty(text))
                        {
                            ShowHelp();
                        }
                        else
                        {
                            listBox1.Items.Insert(0, $"🎤 Распознано: {text}");
                            ExecuteCommand(text);
                        }
                    }
                };

                waveIn.StartRecording();
                listBox1.Items.Add("🎤 Говорите команду...");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add($"❌ Ошибка: {ex.Message}");
            }
        }

        private void OpenBrowser(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии браузера: {ex.Message}");
            }
        }

        private void OpenBrowserWithSearch(string searchEngineChoice)
        {
            if (string.IsNullOrWhiteSpace(savedQuery))
            {
                listBox1.Items.Insert(0, "❌ Ошибка: Запрос не был сохранён!");
                return;
            }

            string encodedQuery = Uri.EscapeDataString(savedQuery);
            string searchUrl = "";

            if (searchEngineChoice == "1")
                searchUrl = $"https://www.youtube.com/results?search_query={encodedQuery}";
            else if (searchEngineChoice == "2")
                searchUrl = $"https://www.google.com/search?q={encodedQuery}";
            else if (searchEngineChoice == "3")
                searchUrl = $"https://yandex.ru/search/?text={encodedQuery}";
            else
            {
                listBox1.Items.Insert(0, "❌ Ошибка: Неверный выбор поисковой системы.");
                return;
            }

            try
            {
                listBox1.Items.Insert(0, $"🌐 Открытие браузера с запросом: {savedQuery}");
                Process.Start(new ProcessStartInfo { FileName = searchUrl, UseShellExecute = true });
                currentStage = InputStage.None;
                savedQuery = "";
            }
            catch (Exception ex)
            {
                listBox1.Items.Insert(0, "❌ Ошибка при открытии браузера: " + ex.Message);
            }
        }

        private static readonly string apiKey = "f40069028698c6ea89bcade883ddd7b0";
        private static readonly string city = "Moscow";
        private static readonly string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        private async Task MainPogoda()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Парсим JSON-ответ
                    JObject weatherData = JObject.Parse(responseBody);
                    double temperature = weatherData["main"]["temp"].Value<double>();
                    string description = weatherData["weather"][0]["description"].ToString();

                    listBox1.Items.Clear();
                    listBox1.Items.Add($"🌤 Погода в {city}: {temperature}°C, {description}");
                }
                catch (HttpRequestException e)
                {
                    listBox1.Items.Clear();
                    listBox1.Items.Add($"⚠ Ошибка запроса: {e.Message}");
                }
            }
        }

        string currentFilePath = "";

        private async Task ExecuteCommand(string command)
        {
            command = command.Trim().ToLower();
            listBox1.Items.Add($"Введена команда: {command}");
            var sim = new InputSimulator();

            if (currentStage == InputStage.WaitingForQuery)
            {
                savedQuery = command;
                listBox1.Items.Add($"Сохранённый запрос: {savedQuery}");
                listBox1.Items.Add("Выберите поисковую систему: 1 - YouTube, 2 - Google, 3 - Yandex");
                currentStage = InputStage.WaitingForSearchEngine;
                return;
            }

            if (currentStage == InputStage.WaitingForSearchEngine)
            {
                OpenBrowserWithSearch(command);
                return;
            }

            if (command == "открой браузер" || command == "4")
            {
                listBox1.Items.Add("Введите запрос для поиска в браузере:");
                currentStage = InputStage.WaitingForQuery;
                listBox1.Items.Add("Текущий статус: ожидание запроса");
            }
            else if (command == "5")
            {
                listBox1.Items.Add("Выберите уровень яркости: 1 - 100%, 2 - 50%, 3 - 0%");
                currentStage = InputStage.WaitingForBrightness;
            }
            else if (currentStage == InputStage.WaitingForBrightness)
            {
                AdjustBrightness(command);
                currentStage = InputStage.None;
            }
            else if (command == "6")
            {
                listBox1.Items.Add("Введите громкость (0 - 100):");
                currentStage = InputStage.WaitingForSound;
            }   
            else if (currentStage == InputStage.WaitingForSound)
            {
                Sound(command);
                currentStage = InputStage.None;
            }
            else if (command == "7")
            {
                listBox1.Items.Add("Запуск диспечера задач");
                var taskmgr1 = new ProcessStartInfo
                {
                    FileName = taskmgr,
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(taskmgr1);
                listBox1.Items.Add("Запущен!");
            }
            else if ( command == "8")
            {
                ShowCurrentTime();
            }
            else if ( command == "9")
            {
                await MainPogoda(); // Теперь все работает правильно!
            }
            else if ( command == "10")
            {
                listBox1.Items.Add("Введите путь к приложению:");
                currentStage = InputStage.WaitingApp;
            }
            else if ( currentStage == InputStage.WaitingApp)
            {
                Ap12p(command);
                currentStage = InputStage.None;
            }
            else if ( command == "11")
            {
                listBox1.Items.Add("Сохраненные пути к приложениям:");
                foreach (var entry in ProggrammDirectory)
                {
                    listBox1.Items.Add($"[{entry.Key}] - {entry.Value}");
                }
            }
            else if ( command == "12")
            {
                listBox1.Items.Add("Выберите номер приложения для запуска:");
                foreach (var entry in ProggrammDirectory)
                {
                    listBox1.Items.Add($"[{entry.Key}] - {entry.Value}");
                }
                currentStage = InputStage.WaitingForAppNumberToLaunch;
            }
            else if (command == "13")
            {

            }
            else if (currentStage == InputStage.WaitingForAppNumberToLaunch)
            {
                if (int.TryParse(command, out int selectedKey))
                {
                    if (ProggrammDirectory.TryGetValue(selectedKey, out string path))
                    {
                        if (File.Exists(path))
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = path,
                                UseShellExecute = true
                            });
                            listBox1.Items.Add("Приложение запущено.");
                        }
                        else
                        {
                            listBox1.Items.Add("Ошибка: Указанный путь не существует.");
                        }
                    }
                    else
                    {
                        listBox1.Items.Add("Ошибка: Неверный выбор.");
                    }
                }
                else
                {
                    listBox1.Items.Add("Ошибка: Введите корректный номер.");
                }

                currentStage = InputStage.None;
            }
            else if (command == "стоп")
            {
                if (isTimeShowing)
                {
                    isTimeShowing = false;
                    timeTimer.Stop();
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Отображение времени остановлено.");
                }
                else
                {
                    listBox1.Items.Add("Время уже не отображается.");
                }
            }
            else
            {
                listBox1.Items.Add("Неизвестная команда.");
            }
        }

        private void Ap12p(string appPath)
        {
            if (string.IsNullOrWhiteSpace(appPath))
            {
                listBox1.Items.Add("⚠ Ошибка: путь не может быть пустым.");
                return;
            }

            int key = ProggrammDirectory.Count + 1;
            ProggrammDirectory[key] = appPath;
            SaveApplications();

            listBox1.Items.Add($"✅ Приложение \"{appPath}\" добавлено и сохранено.");
        }
        private void SaveApplications()
        {
            string json = JsonConvert.SerializeObject(ProggrammDirectory, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        private System.Windows.Forms.Timer timeTimer;
        private bool isTimeShowing = false;

        private void ShowCurrentTime()
        {
            if (timeTimer == null)
            {
                timeTimer = new System.Windows.Forms.Timer();
                timeTimer.Interval = 1000; // Обновление каждую секунду
                timeTimer.Tick += (sender, e) =>
                {
                    DateTime currentTime = DateTime.Now;
                    listBox1.Items.Clear();
                    listBox1.Items.Add("Текущее время: " + currentTime.ToString("HH:mm:ss"));
                    listBox1.Items.Add("Полная дата и время: " + currentTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    listBox1.Items.Add("Введите 'стоп' для остановки.");
                };
            }

            if (!isTimeShowing)
            {
                isTimeShowing = true;
                timeTimer.Start();
                listBox1.Items.Clear();
                listBox1.Items.Add("Запущено обновление времени...");
            }
        }

        private void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return;

            listBox1.Items.Add($"> {input} (Этап: {currentStage})"); // Показываем текущий этап

            if (currentStage == InputStage.WaitingForQuery)
            {
                savedQuery = input;
                listBox1.Items.Add($"Сохранённый запрос: {savedQuery}");
                listBox1.Items.Add("Выберите поисковую систему: 1 - YouTube, 2 - Google, 3 - Yandex");
                currentStage = InputStage.WaitingForSearchEngine;
                return;  // Важно! Чтобы не пошло дальше
            }

            if (currentStage == InputStage.WaitingForSearchEngine)
            {
                listBox1.Items.Add($"Выбран поисковик: {input}");
                OpenBrowserWithSearch(input);
                return;
            }

            // Если не в режиме ожидания ввода, выполняем команду
            ExecuteCommand(input);
        }
        private void AdjustBrightness(string level)
        {
            var sim = new InputSimulator();

            if (level == "1")
            {
                listBox1.Items.Add("🔆 Устанавливаю яркость на 100%");
                sim.Mouse.MoveMouseTo(1750 * 65535 / 1920, 1050 * 65535 / 1080);
                Thread.Sleep(500);
                sim.Mouse.LeftButtonClick();
                Thread.Sleep(500);
                sim.Mouse.MoveMouseTo(1830 * 65535 / 1920, 845 * 65535 / 1080);
                Thread.Sleep(500);
                sim.Mouse.LeftButtonClick();
            }
            else if (level == "2")
            {
                listBox1.Items.Add("🔅 Устанавливаю яркость на 50%");
                sim.Mouse.MoveMouseTo(1750 * 65535 / 1920, 1050 * 65535 / 1080);
                Thread.Sleep(500);
                sim.Mouse.LeftButtonClick();
                Thread.Sleep(500);
                sim.Mouse.MoveMouseTo(1685 * 65535 / 1920, 845 * 65535 / 1080);
                Thread.Sleep(500);
                sim.Mouse.LeftButtonClick();
            }
            else if (level == "3")
            {
                listBox1.Items.Add("🌑 Устанавливаю яркость на 0%");
                sim.Mouse.MoveMouseTo(1750 * 65535 / 1920, 1050 * 65535 / 1080);
                Thread.Sleep(500);
                sim.Mouse.LeftButtonClick();
                Thread.Sleep(500);
                sim.Mouse.MoveMouseTo(1535 * 65535 / 1920, 845 * 65535 / 1080);
                Thread.Sleep(500);
                sim.Mouse.LeftButtonClick();
            }
            else
            {
                listBox1.Items.Add("❌ Ошибка: некорректный ввод. Введите 1, 2 или 3.");
            }
        }
        private void Sound(string sond)
        {
            if (float.TryParse(sond, out float volume)) // Используем переданный параметр
            {
                SetVolume(volume);
            }
            else
            {
                listBox1.Items.Add("Некорректный ввод. Введите число от 0 до 100.");
            }
        }
        private void SetVolume(float volume)
        {
            if (volume < 0 || volume > 100)
            {
                listBox1.Items.Add("Значение должно быть от 0 до 100.");
                return;
            }

            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            device.AudioEndpointVolume.MasterVolumeLevelScalar = volume / 100.0f;
            listBox1.Items.Add($"Громкость установлена на {volume}%");
        }


        private void StopVoiceRecognition()
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }

            if (voskModel != null)
            {
                voskModel.Dispose();
                voskModel = null;
            }

            listBox1.Items.Clear();
            listBox1.Items.Add("🛑 Голосовое распознавание остановлено.");
        }

        private void StopAndClear(object sender, MouseEventArgs e)
        {
            StopVoiceRecognition();
            listBox1.Items.Clear();
            textBox1.Visible = true;
            button1.Visible = true;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            string command1 = textBox11.Text.Trim();

            // Добавляем команду как новое обновление в ленту
            AddUpdate("Введена команда: " + command1);
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string command = txtCommand.Text.Trim();
            AvatarName.Text = command;
        }

        private void radioVoiceMode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Убираем системный звук Enter
                ExecuteCommand(textBox1.Text.Trim());
                textBox1.Clear(); // Очищаем поле после ввода
            }
        }
    }
}
