namespace Common.Shared
{
    public static class StringsComunicates
    {
        public static string ApiSuccessMessage = "Operacja wykonana prawidłowo";
        public static string AuthorizationFailed = $"Autoryzacja nieudana! ";
        public static string BadToken = "Invalid access token or refresh token.";
        public static string BadUserOrPassword = "Bad user or password.";
        public static string BODY = $"========== BODY ==========";
        public static string CertificateInsertProcesStart = $"Proces dodawania certyfikatu do bazy danych rozpoczęty! ";
        public static string CertificateLoadedFailed = $"Błąd podczas ładowania certyfikatu! ";
        public static string CertificateLoadedSuccessfully = $"Certyfikat załadowany poprawnie! ";
        public static string CertificatePasswordInvald = $"Hasło certyfikatu niepoprawne! ";
        public static string CertificatesAlreadyInDb = "Wszystkie certyfikaty są już w bazie dancyh! ";
        public static string CertificateSuccesfullyInsertedToDb = "Certyfikat poprawnie dodany do bazy danych! ";
        public static string ContactWithDeveloper = $"Proszę skontaktować się z twórcą aplikacji. ";
        public static string DecryptionError = $"Błąd podczas deszyfrowania! ";
        public static string EncryptionError = $"Błąd podczas szyfrowania! ";
        public static string ENDREQUEST = $"========== END REQUEST ==========";
        public static string ENDRESPONSE = $"========== END RESPONSE ==========";
        public static string ErrorVerifySignature = $"Błąd podczas weryfikowania podpisu! ";
        public static string ErrorWhileEnvelopeSigning = $"Błąd podczas podpisywania koperty! ";
        public static string ErrorWhileSigningPDF = $"Błąd podczas podpisywania PDF! ";
        public static string HEADERS = $"========== HEADERS ==========";
        public static string InputTypeIncorrect = $"Niepoprawny typ wejścia! ";
        public static string InvaldPassword = "Hasło niepoprawne! ";
        public static string LogsNotFound = $"Nie znaleziono logów z podanego dnia! ";
        public static string PasswordInsertedCorrectly = "Hasło wprowadzone poprawnie! ";
        public static string REQUEST = $"========== REQUEST ==========";
        public static string RequestBodyReadFail = "Nie udało się odczytać zawartości przesłanego żądania.";
        public static string RESPONSE = $"========== RESPONSE ==========";
        public static string VerifySignature = $"Podpis zweryfikowany poprawnie! ";
        private static string configFileName;

        public static string ConfigFolderName
        {
            get { return configFileName; }
        }

        public static string CurrentFolder => "Aktualny folder: ";

        public static string FileDataRead => "Odczyt z pliku: ";

        public static string FileDataReadOK => "Dane odczytane poprawnie";

        public static string FindFileInFolder => "Szukam pliku w folderze: ";

        public static string Welcome => "Witaj w XyzService\nAplikacja przeprowadzi teraz automatyczną konfiguracje na podstawie danych z mediqusa";

        public static string AutoDbInsert(bool autuDbInsert)
        {
            return $"Automatyczny wpis do bazy danych: {autuDbInsert}";
        }

        public static string CerificateUpdated(string? certificateName)
        {
            return $"Certyfikat {certificateName} został zaktualizowany! ";
        }

        public static string CertificateAlreadyInDb(string certificateKey)
        {
            return $"Certyfikat o kluczu {certificateKey} jest już w bazie danych! ";
        }

        public static string CertificateMatchPatter(string filename)
        {
            return $"Certyfikat {filename} zgadza się z wzorcem! ";
        }

        public static string CertificateNotFoundInCacheAndDb(string certificateId)
        {
            return $"Certyfikat o id {certificateId} nie został znaleziony w bazie danych lub pamięci cache! ";
        }

        public static string CertificateSavedInDb(string? certificateName)
        {
            return $"Certyfikat {certificateName} został zapisany w bazie danych! ";
        }

        public static string CertificateWillBePuttedToDb(string certificateKey)
        {
            return $"Certyfikat o kluczu {certificateKey} zostanie dodany do bazy danych! ";
        }

        public static string CurrentLocalIp(string? localIp)
        {
            return $"====== LOCAL IP ===== \n " +
              $"Aktualne local IP usługi \n" +
              $"{localIp} \n" +
              $"===== EDN LOCAL IP =====";
        }

        public static string DataIncomplete(string? fullName)
        {
            return $"Przesłane dane dla {fullName}, są niekompletne!";
        }

        public static string Db2ConnectionInfo(string v)
        {
            return $"====== DB2 Connection Info ===== \n " +
              $"Podłączam do bazy danych: \n" +
              $"{v} \n" +
              $"===== DB2 Connection Info =====";
        }

        public static string DbQuery(string str)
        {
            return $"====== DB QUERY ===== \n " +
                $"Przygotowano zapytanie do bazy danych \n" +
                $"{str} \n" +
                $"===== EDN DB QUERY =====";
        }

        public static string DesiredFolderPath(string folderPath)
        {
            return $"Folder o ścieżce {folderPath} został wybrany! ";
        }

        public static string ErrorWhileLoadingCertificate(string? certificateName = null)
        {
            return $"Wystąpił błąd podczas ładowania certyfikatu {certificateName}! ";
        }

        public static string ErrorWhileReadingValue(object valueName)
        {
            return $"Wystąpił błąd podczas odczytu wartości {valueName}! ";
        }

        public static string FileNotFound(string fileName)
        {
            return $"Plik {fileName} nie został znaleziony! ";
        }

        public static string FileNotFoundInDirectory(string fileName, string directoryName)
        {
            return $"Plik {fileName} nie został znaleziony w folderze {directoryName}! ";
        }

        public static string KeyNotFoundInCache(string key)
        {
            return $"Klucz {key} nie został znaleziony w pamięci cache! ";
        }

        public static string KeyValue(string key, object value)
        {
            return $"Key: {key} | Value: {value} ";
        }

        public static string LocalCertificateNotFound(string certificateId)
        {
            return $"Nie znaleziono certyfikatu lokalnie o id: {certificateId}";
        }

        public static string MthodPath(string method, string path)
        {
            return $"Method: {method} | Path: {path} ";
        }

        public static string ProcesCompleted(string comunicate)
        {
            return $"Proces zakończony!  {comunicate} ";
        }

        public static string QueryString(object queryString)
        {
            return $"QueryString: {queryString} ";
        }

        public static string RecordNotFoundInTable(object? tableName, object recordKey)
        {
            return $"W tabeli {tableName} nie znaleziono rekordu o kluczu {recordKey}! ";
        }

        public static string RequestIncoming(DateTime date)
        {
            return $"===== Odebrano żądanie o godzinie {date} =====";
        }

        public static string ServiceAlreadyExist(string serviceName)
        {
            return $"Serwis {serviceName} już istnieje w kartotece";
        }

        public static void SetConfigFolderName(string? instanceName)
        {
            if (instanceName != null && instanceName != string.Empty)
            {
                configFileName = $"CloudSignService{instanceName}";
            }
            else
            {
                configFileName = $"CloudSignService";
            }
        }

        public static string TableIsEmpty(string tableName)
        {
            return $"Tabela {tableName} jest pusta! ";
        }

        public static string TypeNotImplemented(string typeName)
        {
            return $"Typ {typeName} nie jest obsługiwany! ";
        }

        public static string UserNotFound(string username)
        {
            return $"Użytkownik {username}, nie został znaleziony! ";
        }
    }
}