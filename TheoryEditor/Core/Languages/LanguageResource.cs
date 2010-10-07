using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Globalization;

using Protsenko.TheoryEditor.Core.Debug;

namespace Protsenko.TheoryEditor.Core.Languages
{
    /// <summary>
    /// Класс, обеспечивающий поддержку нескольких языков для интерфейса программы.
    /// </summary>
    class LanguageResource
    {
        public static Dictionary<string, string> currentDictionary { get; private set; }

        private static Hashtable languages = new Hashtable();

        public static event Events.Updated Updated;

        static LanguageResource()
        {
            Dictionary<string, string> englishDictionary = new Dictionary<string, string>
            {{"theoryeditor","Theory Editor"},
             {"new","New"},
             {"delete","Delete"},
             {"add","Add"},
             {"save","Save"},
             {"close","Close"},
             {"load","Load"},
             {"exit","Exit"},
             {"open","Open"},
             {"browse","Browse"},
             {"mod","Mod"},
             {"log","Log"},
             {"help","Help"},

             {"ok","Ok"},
             {"cancel", "Cancel"},

             {"basis","Basis"},
             {"proof","Proof"},
             {"formulation","Formulation"},
             {"type", "Type"},
             {"template", "Template"},
             {"edit","Edit"},

             {"modBasic","Basic Mode"},
             {"modQuick","Quick Mode"},
             {"options","Options"},
             {"error", "Error"},
             {"warning", "Warning"},
             {"message", "Message"},
             {"Definition", "Definition"},
             {"Corollary","Corollary"},
             {"Postulate","Postulate"},
             {"Theorem","Theorem"},
             {"Lemma","Lemma"},


             {"createElement","Create Element"},
             {"createSession", "Create Session"},
             {"errorSavingData","Failed to save data. Reason: "},
             {"errorLoadingData","Failed to load data. Reason: "},
             {"errorFileNotExists", "File doesn't exists."},
             {"errorDeletingData", "Couldn't delete saved data. Reason: "},
             {"errorNotInitializedSession", "Can't complete request. Session is not initialized."},
             {"errorAddingElementToTreeAdded", "Unable to add element. It was already added."},
             {"errorAddingElementToTreeNotExists","Unable to add element. It doesn't exists."},
             {"errorReadOnly","Read Only"},
             {"errorCreationAborted","Creation Aborted"},
             {"errorCanceled", "Canceled"},
             {"requestSaveSession","Do want to save session?"},
             {"requestSaveSessionAndCreateNew","Do you really want to close this session and create new?"}};
            

            Dictionary<string, string> russianDictionary = new Dictionary<string, string>
            {{"theoryeditor","Редактор теорий"},
             {"new","Новая сессия"},
             {"delete","Удалить"},
             {"add","Добавить"},
             {"save","Сохранить"},
             {"close","Закрыть"},
             {"load","Загрузить"},
             {"exit","Выйти"},
             {"open","Открыть"},
             {"browse","Обзор"},
             {"mod","Режим"},
             {"log","Лог"},
             {"help","Помощь"},

             {"ok","Ок"},
             {"cancel", "Отмена"},

             {"basis","Базис"},
             {"proof","Доказательство"},
             {"formulation","Формулировка"},
             {"type", "Тип"},
             {"template", "Шаблон"},
             {"edit","Редактировать"},

             {"modBasic","Стандартный режим"},
             {"modQuick","Быстрый режим"},
             {"options","Настройки"},
             {"error", "Ошибка"},
             {"warning", "Предупреждение"},
             {"message", "Сообщение"},
             {"Definition", "Определение"},
             {"Corollary","Следствие"},
             {"Postulate","Постулат"},
             {"Theorem","Теорема"},
             {"Lemma","Лемма"},
             {"createElement","Создать элемент"},
             {"createSession", "Создать сессию"},
             {"errorSavingData","Не удалось сохранить данные. Причина: "},
             {"errorLoadingData","Не удалось загрузить данные. Причина: "},
             {"errorFileNotExists", "Файл не существует."},
             {"errorDeletingData", "Не удалось удалить данные. Причина: "},
             {"errorNotInitializedSession", "Не могу выполнить действие. Сессия не создана."},
             {"errorAddingElementToTreeAdded", "Нельзя добавить элемент. Такой элемент уже добавлен."},
             {"errorAddingElementToTreeNotExists","Нельзя добавить элемент. Элемент не существует."},
             {"errorReadOnly","Только для чтения"},
             {"errorCreationAborted","Создание отменено."},
             {"errorCanceled", "Отменено"},
             {"requestSaveSession","Хотите сохранить сессию?"},
             {"requestSaveSessionAndCreateNew","Вы действительно хотите закрыть текущую сессию и создать новую?"}};

            languages.Add("eng", englishDictionary);
            languages.Add("rus", russianDictionary);

            if (CultureInfo.CurrentCulture.ThreeLetterISOLanguageName == "rus")
                currentDictionary = russianDictionary;
            else
                currentDictionary = englishDictionary;
        }


        /// <summary>
        /// Переключает язык интерфейса. Для обновления элементы должны подписываться на событие Update
        /// и реализовывать IMultiLanguage.
        /// </summary>
        /// <param name="language"></param>
        public static void SwitchLanguage(string language)
        {
            Operation operation = new Operation("Switch Language");
            OperationsHandler.OperationStarts(operation);
            try
            {
                currentDictionary = (Dictionary<string, string>)languages[language];
                Updated(language);
            }
            catch (Exception e)
            {
                ExceptionHandler.exceptionHandler.Add(e);
            }
            OperationsHandler.OperationComlete(operation);
        }

        /// <summary>
        /// Возвращает список загруженных и доступных языков для интерфейса.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAvailableLanguages()
        {
            List<string> availableLng = new List<string>();
            
            foreach(string key in languages.Keys)
            {
                availableLng.Add(key);
            }
            return availableLng;
        }

        /// <summary>
        /// Загружает предварительно сохранённые словари для интерфейса программы из папки Languages.
        /// </summary>
        private static void LoadLanguages()
        {
            Operation operation = new Operation("Load Languages");
            OperationsHandler.OperationStarts(operation);
            try
            {
                
            }
            catch (Exception e)
            {
                ExceptionHandler.exceptionHandler.Add(e);
            }
            OperationsHandler.OperationComlete(operation);
        }
    }
}
